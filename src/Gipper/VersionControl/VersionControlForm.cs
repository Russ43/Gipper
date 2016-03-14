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
	internal class VersionControlForm : GipperForm
	{
		// fields
		private KeyList<ShellCommand> _keyList;
		private TextBox _outputTextBox;
		private Button _okButton;


		// constructors
		public VersionControlForm()
		{
			Text = "Version Control";

			TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
			tableLayoutPanel.Dock = DockStyle.Fill;
			tableLayoutPanel.Padding = new Padding(21);
			Controls.Add(tableLayoutPanel);

			_keyList = new KeyList<ShellCommand>();
			PopulateKeyList();
			_keyList.BackBrush1 = new SolidBrush(Color.Black);
			_keyList.BackBrush2 = new SolidBrush(Color.Black);
			_keyList.TextBrush = new SolidBrush(Color.Silver);
			_keyList.TextFont = StyleHelper.KeyListTextFont;
			_keyList.KeyFont = StyleHelper.KeyListKeyFont;
			_keyList.Select += KeyList_Select;
			_keyList.Cancel += KeyList_Cancel;
			_keyList.Dock = DockStyle.Fill;
			tableLayoutPanel.Controls.Add(_keyList, 0, 0);

			_outputTextBox = new TextBox();
			_outputTextBox.Dock = DockStyle.Fill;
			_outputTextBox.BorderStyle = BorderStyle.FixedSingle;
			_outputTextBox.ReadOnly = true;
			_outputTextBox.Multiline = true;
			_outputTextBox.BackColor = Color.Black;
			_outputTextBox.ForeColor = Color.Silver;
			_outputTextBox.Font = StyleHelper.ConsoleFont;
			tableLayoutPanel.Controls.Add(_outputTextBox, 0, 1);
			
			_okButton = new Button();
			_okButton.Dock = DockStyle.Right;
			_okButton.Text = "&OK";
			_okButton.Height = StyleHelper.ButtonHeight;
			_okButton.Width = 4 * _okButton.Width;
			_okButton.Click += OkButton_Click;
			tableLayoutPanel.Controls.Add(_okButton, 0, 2);

			AcceptButton = _okButton;

			_keyList.Focus();
		}


		// private methods
		private void PopulateKeyList()
		{
			// TODO: Try to get the selected file form Solution Explorer if it is open.
			/*
			// check if an item is selected in the Solution Explorer
			UIHierarchyItem item = Helper.FindSelectedSolutionExplorerItem();
			if(item != null)
			{
				activeDocumentPath = item.Name;
			}
			 */

			string activeDocumentPath = null;
			string workingDirectory = null;
			if(GipperPackage.ToolWindows.OpenFilesToolWindowPane != null && GipperPackage.ToolWindows.OpenFilesToolWindowPane.HasFocus)
			{
				// the Open Files tool window has focus, so use whatever document it has highlighted
				FileInfo activeFile = GipperPackage.ToolWindows.OpenFilesToolWindowPane.HighlightedFile;
				if(activeFile != null)
					activeDocumentPath = activeFile.FullName;
			}
			else if(GipperPackage.Dte.ActiveDocument != null && GipperPackage.Dte.ActiveDocument.FullName != null)
			{
				// a document is open (which may or may not have focus)
				activeDocumentPath = GipperPackage.Dte.ActiveDocument.FullName;
			}

			if(activeDocumentPath != null)
			{
				workingDirectory = Path.GetDirectoryName(activeDocumentPath);

				Text += ": " + Path.GetFileName(activeDocumentPath) + "(" + workingDirectory + ")";

				string runPsPath = VariableHelper.GetVariable("VERSIONCONTROL", "RUNPSPATH");
				_keyList.Items.Add(new ShellCommand("Add File", Keys.A, runPsPath, string.Format("IDE AddFile \"{0}\"", activeDocumentPath), workingDirectory));
				_keyList.Items.Add(new ShellCommand("Check Out", Keys.E, runPsPath, string.Format("IDE CheckOut \"{0}\"", activeDocumentPath), workingDirectory));
				_keyList.Items.Add(new ShellCommand("Diff All", Keys.R, runPsPath, "IDE DiffAll", workingDirectory));
				_keyList.Items.Add(new ShellCommand("Diff File", Keys.D, runPsPath, string.Format("IDE DiffFile \"{0}\"", activeDocumentPath), workingDirectory));
				_keyList.Items.Add(new ShellCommand("File History", Keys.H, runPsPath, string.Format("IDE FileHistory \"{0}\"", activeDocumentPath), workingDirectory));
			}
			else
			{
				Close();
			}
		}


		// event handlers
		private void KeyList_Select(object sender, SelectEventArgs e)
		{
			ShellCommand command = (ShellCommand) e.Item;
			command.Completed += Command_Completed;
			command.OutputReceived += Command_OutputReceived;
			command.Execute();
		}

		private void KeyList_Cancel(object sender, EventArgs e)
		{
			Close();
		}

		private void Command_OutputReceived(object sender, EventArgs e)
		{
			BeginInvoke(
				(Action)
				(
					() =>
					{
						ShellCommand command = (ShellCommand) sender;
						_outputTextBox.Text = command.Output;
					}
				)
			);	
		}

		private void Command_Completed(object sender, EventArgs e)
		{
			BeginInvoke(
				(Action)
				(
					() =>
					{
						ShellCommand command = (ShellCommand) sender;
						if(command.ExitCode == 0)
						{
							//System.Threading.Thread.Sleep(TimeSpan.FromSeconds(7));
							//Close();
						}
					}
				)
			);
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
