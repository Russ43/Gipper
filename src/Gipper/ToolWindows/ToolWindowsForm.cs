using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
	internal class ToolWindowsForm : GipperForm
	{
		// fields
		private KeyList<DteCommand> _keyList;
		private DteCommand _chosenCommand;


		// constructors
		public ToolWindowsForm()
		{
			Text = "Tool Windows";

			_keyList = new KeyList<DteCommand>();
			PopulateKeyList();
			_keyList.BackBrush1 = new SolidBrush(Color.Black);
			_keyList.BackBrush2 = new SolidBrush(Color.Black);
			_keyList.TextBrush = new SolidBrush(Color.Silver);
			_keyList.TextFont = StyleHelper.KeyListTextFont;
			_keyList.KeyFont = StyleHelper.KeyListKeyFont;
			_keyList.Select += KeyList_Select;
			_keyList.Cancel += KeyList_Cancel;
			_keyList.Dock = DockStyle.Fill;
			Controls.Add(_keyList);

			_keyList.Focus();
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
		private void PopulateKeyList()
		{
			_keyList.Items.Add(new DteCommand("Code Analysis", Keys.A, "View.CodeAnalysis", ""));
			_keyList.Items.Add(new DteCommand("Object Browser", Keys.B, "View.ObjectBrowser", ""));
			_keyList.Items.Add(new DteCommand("Command Window", Keys.C, "View.CommandWindow", ""));
			_keyList.Items.Add(new DteCommand("Error List", Keys.E, "View.ErrorList", ""));
			_keyList.Items.Add(new DteCommand("Find Results 1", Keys.F, "View.FindResults1", ""));
			_keyList.Items.Add(new DteCommand("Find Results 2", Keys.I, "View.FindResults2", ""));
			_keyList.Items.Add(new DteCommand("Output", Keys.O, "View.Output", ""));
			_keyList.Items.Add(new DteCommand("Properties Window", Keys.P, "View.PropertiesWindow", ""));
			_keyList.Items.Add(new DteCommand("Test Results", Keys.R, "View.TestResults", ""));
			_keyList.Items.Add(new DteCommand("Solution Explorer", Keys.S, "View.SolutionExplorer", ""));
			_keyList.Items.Add(new DteCommand("Test Explorer", Keys.T, "TestExplorer.ShowTestExplorer", ""));
			_keyList.Items.Add(new DteCommand("Class View", Keys.V, "View.ClassView", ""));
		}


		// event handlers
		private void KeyList_Select(object sender, SelectEventArgs e)
		{
			Close();

			_chosenCommand = (DteCommand) e.Item;
		}

		private void KeyList_Cancel(object sender, EventArgs e)
		{
			Close();
		}
	}
}
