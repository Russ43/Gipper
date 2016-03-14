using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gipper
{
	// http://stackoverflow.com/questions/13302933/how-to-avoid-firing-observablecollection-collectionchanged-multiple-times-when-r
	// http://stackoverflow.com/questions/670577/observablecollection-doesnt-support-addrange-method-so-i-get-notified-for-each
	internal class EnhancedObservableCollection<T> : ObservableCollection<T>
	{
		public EnhancedObservableCollection()
			: base()
		{

		}

		public EnhancedObservableCollection(IEnumerable<T> collection)
			: base(collection)
		{

		}

		public EnhancedObservableCollection(List<T> list)
			: base(list)
		{

		}

		public virtual void AddRange(IEnumerable<T> collection)
		{
			foreach(T item in collection)
			{
				this.Items.Add(item);
			}

			this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			// Cannot use NotifyCollectionChangedAction.Add, because Constructor supports only the 'Reset' action.
		}

		public virtual void RemoveRange(IEnumerable<T> collection)
		{
			bool removed = false;
			foreach(T item in collection)
			{
				if(this.Items.Remove(item))
					removed = true;
			}

			if(removed)
			{
				this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
				// Cannot use NotifyCollectionChangedAction.Remove, because Constructor supports only the 'Reset' action.
			}
		}

		public virtual void Reset(T item)
		{
			this.Reset(new List<T>() { item });
		}

		public virtual void Reset(IEnumerable<T> collection)
		{
			int count = this.Count;

			// Step 1: Clear the old items
			this.Items.Clear();

			// Step 2: Add new items
			foreach(T item in collection)
			{
				this.Items.Add(item);
			}

			// Step 3: Don't forget the event
			if(this.Count != count)
				this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}
}
