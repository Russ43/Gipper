using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

using Company43.Common;


namespace Gipper
{
	internal class FileNavigatorControl : UserControl
	{
		// fields
		private TrieControl<FileNavigatorItem>	_trie;


		[Import]
		internal IVsEditorAdaptersFactoryService AdapterService = null;


		// constructors
		public FileNavigatorControl()
		{
			Trie<FileNavigatorItem> trie = new Trie<FileNavigatorItem>(false, true, true);
			_trie = new TrieControl<FileNavigatorItem>(trie);
			_trie.Dock = DockStyle.Fill;
			_trie.TextBoxFont = StyleHelper.ToolWindowFont;
			_trie.ListViewFont = StyleHelper.ToolWindowFont;
			_trie.BackBrush1 = StyleHelper.FileNavigatorBackBrush1;
			_trie.BackBrush2 = StyleHelper.FileNavigatorBackBrush2;
			//_trie.SelectedBrush = new SolidBrush(Color.FromArgb(240, 127, 240));
			_trie.TextBrush = StyleHelper.FileNavigatorTextBrush1;
			_trie.SelectedTextBrush = StyleHelper.FileNavigatorTextBrush2;
			_trie.Select += new SelectEventHandler(Trie_Select);
			Controls.Add(_trie);
		}


		// methods
		public void Populate()
		{
			Trie<FileNavigatorItem> trie = _trie.Trie;
			trie.Clear();

			IVsDropdownBar dropdownBar = DropdownBarHelper.GetActiveDropdownBar();
			if(dropdownBar != null)
			{
				IList<GipperDropdownBarEntry> dropdownBarEntries = DropdownBarHelper.GetDropdownBarEntries(dropdownBar);
				foreach(GipperDropdownBarEntry dropdownBarEntry in dropdownBarEntries)
				{
					trie.AddItem(new FileNavigatorItem(dropdownBarEntry));
				}
			}
		}




		// event handlers
		private void Trie_Select(object sender, SelectEventArgs e)
		{
			FileNavigatorItem item = (FileNavigatorItem) e.Item;
			IVsDropdownBar dropdownBar = DropdownBarHelper.GetActiveDropdownBar();
			dropdownBar.SetCurrentSelection(item.Entry.ComboIndex, item.Entry.Index);
			IVsDropdownBarClient client = DropdownBarHelper.GetDropdownBarClient(dropdownBar);
			client.OnItemChosen(item.Entry.ComboIndex, item.Entry.Index);

			_trie.Clear();
		}
	}
}
