using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
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
	static internal class Helper
	{
		// properties
		static public double GoldenRatio
		{
			get
			{
				return 1.61803398875;
			}
		}


		// methods
		static public void ShowInformationDialog(string caption, string message)
		{
			MessageBox.Show(message, string.Format("Gipper Visual Studio Package - {0}", caption), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		static public void ShowWarningDialog(string caption, string message)
		{
			MessageBox.Show(message, string.Format("Gipper Visual Studio Package - {0}", caption), MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	
		static public void ShowErrorDialog(string caption, string message)
		{
			MessageBox.Show(message, string.Format("Gipper Visual Studio Package - {0}", caption), MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		static public void TrapExceptions(Action action, string actionDescription)
		{
			// Visual Studio crashes whenenver our package generates an unhandled exception, so for now, we'll try to
			// log these exceptions as MessageBox popups instead.

			// TODO: Decide how to better handle this.

			try
			{
				action();
			}
			catch(GipperException ge)
			{
				Helper.ShowErrorDialog(actionDescription, ge.Message);
			}
			catch(Exception e)
			{
				Helper.ShowErrorDialog(actionDescription, e.ToString());
			}
		}

		static public TResult TrapExceptions<TResult>(Func<TResult> function, TResult resultIfException, string functionDescription)
		{
			// Visual Studio crashes whenenver our package generates an unhandled exception, so for now, we'll try to
			// log these exceptions as MessageBox popups instead.

			// TODO: Decide how to better handle this.

			TResult result = default(TResult);
			try
			{
				result = function();
			}
			catch(GipperException ge)
			{
				Helper.ShowErrorDialog(functionDescription, ge.Message);
			}
			catch(Exception e)
			{
				result = resultIfException;
				Helper.ShowErrorDialog(functionDescription, e.ToString());
			}

			return result;
		}

		static public bool IsProjectFile(FileInfo file)
		{
			bool isProjectFile = false;
			switch(file.Extension.ToLower())
			{
				case ".csproj":
				case ".vbproj":
					isProjectFile = true;
					break;
			}

			return isProjectFile;
		}

		static public void OpenFile(FileInfo file)
		{
			try
			{
				// Visual Studio won't allow you to edit project files as text while a project is open. So, we need to unload the project
				// first. Then, when the project window is eventually closed, we will re-open the project (so Intellisense and such works 
				// again). See WindowManager.
				if(IsProjectFile(file))
				{
					SelectProjectInSolutionExplorer(file);
					try
					{
						UnloadSelectedProject();
					}
					catch(COMException)
					{
						// If the project was already unloaded (or failed to load), the unload operation will E_FAIL. I can't figure out
						// how to detect if a project is loaded or not, so just swallow this exception and assume the project was already
						// unloaded.
					}
				}
				
				Window window = GipperPackage.Dte.ItemOperations.OpenFile(file.FullName, Constants.vsViewKindTextView);
			}
			catch(InvalidCastException ice)
			{
				ShowErrorDialog("Cannot open file", ice.ToString());
			}
		}

		static public void CreateFile(FileInfo file)
		{
			DirectoryInfo dir = file.Directory;
			if(!dir.Exists)
				CreateDirectory(dir);

			using(FileStream stream = file.Create())
			{
			}

			return;
		}

		static public void CreateDirectory(DirectoryInfo dir)
		{
			DirectoryInfo parentDir = dir.Parent;
			if(parentDir != null && !dir.Exists)
				CreateDirectory(parentDir);

			dir.Create();
		}

		static public void ReloadProject(FileInfo file)
		{
			SelectProjectInSolutionExplorer(file);
			ReloadSelectedProject();
		}

		static public void WriteClassifierSnapshotToDebug(SnapshotSpan span, IList<ClassificationSpan> existingSpans)
		{
#if DEBUG
			Debug.WriteLine(string.Format("*** SnapshotSpan {0} ***", span));

			IList<ClassificationSpan> newSpans = new List<ClassificationSpan>();

			IList<SnapshotSpan> existingSpans2 = existingSpans.Select(cs => cs.Span).ToList();
			NormalizedSnapshotSpanCollection norm = new NormalizedSnapshotSpanCollection(existingSpans2);
			foreach(SnapshotSpan normSpan in norm)
				Debug.WriteLine(normSpan);

			foreach(ClassificationSpan existingSpan in existingSpans)
				Debug.WriteLine(existingSpan.Span.ToString() + " | " + existingSpan.Span.GetText() + " |  " + FormatClassificationType(existingSpan.ClassificationType));

			Debug.WriteLine("");
#endif
		}

		static public string FormatClassificationType(IClassificationType type)
		{
			StringBuilder result = new StringBuilder();
			if(type != null)
			{
				foreach(IClassificationType baseType in type.BaseTypes)
				{
					result.Append(FormatClassificationType(baseType));
					result.Append(" > ");
				}

				result.Append(type.Classification);
			}

			return result.ToString();
		}

		static public string GetSolutionDirectory()
		{
			DTE2 dte = GipperPackage.Dte;

			string directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			if(dte.Solution != null && dte.Solution.FullName.Length > 0)
				directory = Path.GetDirectoryName(dte.Solution.FullName);

			return directory;
		}

		static public string GetCurrentFileDirectory()
		{
			string directory = null;

			DTE2 dte = GipperPackage.Dte;
			if(dte.ActiveDocument != null)
				directory = Path.GetDirectoryName(dte.ActiveDocument.Path);
			else
				directory = GetSolutionDirectory();

			return directory;
		}

		static public UIHierarchyItem FindSelectedSolutionExplorerItem()
		{
			UIHierarchyItem rootItem = GipperPackage.Dte.ToolWindows.SolutionExplorer.UIHierarchyItems.Item(1);
			return FindSelectedItems(rootItem).FirstOrDefault();
		}

		static public void TileWindowsOnSecondaryScreens()
		{
			DTE2 dte = GipperPackage.Dte;
			foreach(Window window in dte.Windows)
			{
				Debug.WriteLine(window.Caption);
			}

			//Debugger4 dteDebugger = (Debugger4) dte.Debugger;
			Microsoft.VisualStudio.Shell.Interop.IVsDebugger4 debugger = (Microsoft.VisualStudio.Shell.Interop.IVsDebugger4) dte.Debugger;
			

		}
		
		static public float GetScreenDpiScalingFactor()
		{
			// http://stackoverflow.com/questions/5977445/how-to-get-windows-display-settings

			float screenScalingFactor;
			using(Graphics g = Graphics.FromHwnd(IntPtr.Zero))
			{
				IntPtr desktop = g.GetHdc();
				int logPixelsY = Gdi32Helper.GetDeviceCaps(desktop, (int) Gdi32Helper.DeviceCap.LOGPIXELSY);
				g.ReleaseHdc(desktop);

				screenScalingFactor = (float) logPixelsY / (float) 96;
			}

			return screenScalingFactor; // 1.25 = 125%
		}


		// private methods
		static private IList<UIHierarchyItem> FindSelectedItems(UIHierarchyItem item)
		{
			List<UIHierarchyItem> selectedItems = new List<UIHierarchyItem>(1);
			FindSelectedItems(item, selectedItems);
			return selectedItems;
		}

		static private void FindSelectedItems(UIHierarchyItem item, IList<UIHierarchyItem> selectedItems)
		{
			if(item.IsSelected)
			{
				selectedItems.Add(item);
			}
			else
			{
				foreach(UIHierarchyItem subitem in item.UIHierarchyItems)
				{
					FindSelectedItems(subitem, selectedItems);
				}
			}
		}

		static private void SelectProjectInSolutionExplorer(FileInfo projectFile)
		{
			DTE2 dte = GipperPackage.Dte;

			// activate the solution explorer window
			Window solutionExplorerWindow = (Window) dte.ToolWindows.SolutionExplorer.Parent;
			solutionExplorerWindow.Activate();

			// iterate over the project hierarchy items
			UIHierarchyItem solutionHierarchyItem = dte.ToolWindows.SolutionExplorer.UIHierarchyItems.Item(1);
			bool foundProject = false;
			foreach(UIHierarchyItem projectHierarchyItem in solutionHierarchyItem.UIHierarchyItems)
			{
				if(projectHierarchyItem.Name.ToLower() + projectFile.Extension.ToLower() == projectFile.Name.ToLower())
				{
					// select the specified project
					projectHierarchyItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
					foundProject = true;
					break;
				}
			}

			if(!foundProject)
				throw new InvalidOperationException(string.Format("A project for '{0}' was not found in Solution Explorer.", projectFile.FullName));
		}

		static private void UnloadSelectedProject()
		{
			DTE2 dte = GipperPackage.Dte;
			dte.ExecuteCommand("Project.UnloadProject", "");
		}

		static private void ReloadSelectedProject()
		{
			DTE2 dte = GipperPackage.Dte;
			dte.ExecuteCommand("Project.ReloadProject", "");
		}
	}
}
