using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

using Company43.Common;


namespace Gipper
{
	internal class OpenFilesControl : UserControl
	{
		// fields
		private ProjectFileManager _projectFileManager;
		private TrieControl<ProjectFile> _trie;
		private string _solutionFullName;
		private Func<FileInfo, bool> _includeFileFunc;


		// events
		public event EventHandler RepopulateProgressChanged;


		// constructors
		public OpenFilesControl()
		{
			// Be sure to call this from inside the constructor so that any bad input exceptions
			// are handled in the ToolPane43 constructor.
			CreateIncludeFileFunc();

			Trie<ProjectFile> trie = new Trie<ProjectFile>(false, true, true);

			_trie = new TrieControl<ProjectFile>(trie);
			_trie.Dock = DockStyle.Fill;
			_trie.TextBoxFont = new Font("Segoe UI Semibold", 10f);
			_trie.ListViewFont = _trie.TextBoxFont;
			_trie.BackBrush1 = new SolidBrush(Color.FromArgb(0, 43, 0));
			_trie.BackBrush2 = new SolidBrush(Color.FromArgb(0, 43, 0));
			//_trie.SelectedBrush = new SolidBrush(Color.FromArgb(240, 127, 240));
			_trie.TextBrush = new SolidBrush(Color.FromArgb(227, 227, 227));
			_trie.SelectedTextBrush = new SolidBrush(Color.FromArgb(227, 227, 227));
			_trie.Select += new SelectEventHandler(OpenFilesControl_Select);
			_trie.GotFocus2 += Trie_GotFocus2;
			_trie.LostFocus2 += Trie_LostFocus2;
			Controls.Add(_trie);
		}


		// properties
		public FileInfo HighlightedFile
		{
			get
			{
				return _trie.SelectedItem.FileInfo;
			}
		}

		public double RepopulateProgress
		{
			get;
			private set;
		}


		// private methods
		private void PopulateForSolution()
		{
			_trie.Trie.Clear();

			if(!string.IsNullOrEmpty(_solutionFullName))
			{
				_projectFileManager = new ProjectFileManager(GetRootDirectory(), _includeFileFunc, _trie.Trie);
				_projectFileManager.RepopulateProgressChanged += ProjectFileManager_RepopulateProgressChanged;
			}
		}

		private string GetRootDirectory()
		{
			string rootDirectory = VariableHelper.GetVariable("OPENFILES", "ROOT");
			if(rootDirectory == null)
				rootDirectory = Path.GetDirectoryName(_solutionFullName);

			return rootDirectory;
		}

		private void CreateIncludeFileFunc()
		{
			string excludes = VariableHelper.GetVariable("OPENFILES", "EXCLUDES");
			if(excludes != null)
			{
				// TODO: Comma-splitting isn't the best without escaping. Semi-colon was even worse thought because of
				// MyKey OpenIdeCommand EnvironmentVariables collision.
				string[] excludesArray = excludes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				Regex[] regexes = new Regex[excludesArray.Length];
				for(int i = 0; i < excludesArray.Length; ++i)
				{
					try
					{
						Regex regex = new Regex(excludesArray[i], RegexOptions.IgnoreCase);
						regexes[i] = regex;
					}
					catch(ArgumentException ae)
					{
						throw new BadInputException()
						{
							ErrorMessage = string.Format(
								"The {0} environment variable contains bad input.\r\n\r\n{1}", 
								VariableHelper.GetFullVariableName("OPENFILES", "EXCLUDES"),
								ae.Message
							),
							BadInput = excludes
						};
					}
				}

				_includeFileFunc = (
					(FileInfo fileInfo) =>
					{
						bool include = true;
						string path = fileInfo.FullName;
						foreach(Regex regex in regexes)
						{
							if(regex.IsMatch(path))
							{
								include = false;
								break;
							}
						}

						return include;
					}
				);
			}
			else
			{
				_includeFileFunc = (
					(FileInfo FileInfo) =>
					{
						return true;
					}
				);
			}
		}


		// event handlers
		private void OpenFilesControl_Select(object sender, SelectEventArgs e)
		{
			ProjectFile file = (ProjectFile) e.Item;

			_trie.Clear();

			Helper.OpenFile(file.FileInfo);
		}

		private void Trie_GotFocus2(object sender, EventArgs e)
		{
			_trie.Clear();
			if(_solutionFullName != GipperPackage.Dte.Solution.FullName)
			{
				_solutionFullName = GipperPackage.Dte.Solution.FullName;
				PopulateForSolution();
			}
		}

		private void Trie_LostFocus2(object sender, EventArgs e)
		{
			// TODO: Figure out why hitting Esc doesn't clear trie.
			_trie.Clear();
		}
		
		private void ProjectFileManager_RepopulateProgressChanged(object sender, EventArgs e)
		{
			RepopulateProgress = _projectFileManager.RepopulateProgress;

			EventHandler repopulateProgressChanged = RepopulateProgressChanged;
			if(repopulateProgressChanged != null)
			{
				this.Invoke(
					(MethodInvoker)(
						() => 
						{
							repopulateProgressChanged(this, EventArgs.Empty); 
						}
					)
				);
			}
		}
	}
}
