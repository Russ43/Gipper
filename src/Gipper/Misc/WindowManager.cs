using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	internal class WindowManager
	{
		// fields
		private Dictionary<Window, string> _windowToFullNameDictionary;


		// constructors
		public WindowManager()
		{
			_windowToFullNameDictionary = new Dictionary<Window, string>();

			EventsHelper.WindowCreated += EventsHelper_WindowCreated;
			EventsHelper.WindowClosing += EventsHelper_WindowClosing;
		}


		// event handlers
		private void EventsHelper_WindowCreated(object sender, Window e)
		{
			// by the time the window-closing event is raised, the document object has been lost, so we cache the file name here
			if(e != null && e.Document != null && e.Document.FullName != null)
				_windowToFullNameDictionary.Add(e, e.Document.FullName);
		}

		private void EventsHelper_WindowClosing(object sender, Window e)
		{
			if(e != null)
			{
				string fullName;
				if(_windowToFullNameDictionary.TryGetValue(e, out fullName))
				{
					// Since Visual Studio doesn't allow project files to be edited as text while a project is open, we need to
					// reload a project when its window closes.
					FileInfo file = new FileInfo(fullName);
					if(Helper.IsProjectFile(file))
						Helper.ReloadProject(file);

					_windowToFullNameDictionary.Remove(e);
				}
			}
		}
	}
}
