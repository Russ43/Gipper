using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

using Company43.Common;


namespace Gipper
{
	internal class CommandsForm : GipperForm
	{
		// fields
		private TrieControl<string> _trie;
		private DteCommand _chosenCommand;
		private BackgroundWorker _backgroundWorker;


		// constructors
		public CommandsForm()
		{
			Text = "Commands";

			Trie<string> trie = new Trie<string>(false, true, true);

			_trie = new TrieControl<string>(trie);
			_trie.Dock = DockStyle.Fill;
			_trie.TextBoxFont = new Font("Segoe UI Semibold", 10f);
			_trie.ListViewFont = _trie.TextBoxFont;
			_trie.BackBrush1 = new SolidBrush(Color.FromArgb(0, 43, 0));
			_trie.BackBrush2 = new SolidBrush(Color.FromArgb(0, 43, 0));
			//_trie.SelectedBrush = new SolidBrush(Color.FromArgb(240, 127, 240));
			_trie.TextBrush = new SolidBrush(Color.FromArgb(227, 227, 227));
			_trie.SelectedTextBrush = new SolidBrush(Color.FromArgb(227, 227, 227));
			_trie.Select += new SelectEventHandler(Trie_Select);
			_trie.KeyPress += new KeyPressEventHandler(Trie_KeyPress);
			Controls.Add(_trie);

			_backgroundWorker = new BackgroundWorker();
			_backgroundWorker.DoWork += BackgroundWorker_DoWork;

			Populate();
		}


		// methods
		public void ExecuteChosenCommand()
		{
			// NOTE: This is a separate command (rather than inside of KeyList_Select) because it must be executed
			// after the dialog is closed so the target tool window maintains focus.

			if(_chosenCommand != null)
				_chosenCommand.Execute();
		}


		// private methods
		private void Populate()
		{
			_backgroundWorker.RunWorkerAsync();
		}


		// event handlers
		private void Trie_Select(object sender, SelectEventArgs e)
		{
			string command = (string) e.Item;

			_trie.Clear();

			_chosenCommand = new DteCommand(null, Keys.None, command, string.Empty);

			DialogResult = DialogResult.OK;
			Hide();
		}

		private void Trie_KeyPress(object sender, KeyPressEventArgs e)
		{
			if((int) e.KeyChar == 27)
			{
				DialogResult = DialogResult.Cancel;
				Hide();
			}
		}

		
		// background thread event handlers
		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			_trie.Trie.Clear();

			List<string> commandNames = new List<string>();
			foreach(Command command in GipperPackage.Dte.Commands)
			{
				commandNames.Add(command.Name);
			}

			_trie.Trie.AddItems(commandNames);
		}
	}
}

