using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	internal class AddFileCommand : GipperCommand
	{
		// GipperCommand methods
		protected override void DoExecute()
		{
			DTE2 dte = GipperPackage.Dte;
			Document document = dte.ActiveDocument;
			if(document == null)
				return;

			IList<string> filePaths = DocumentHelper.GetFilePathsFromProjectFileSelectionOrLine(document);
			if(filePaths.Count > 0)
			{
				// the selection specified one or more files, so create each file (add from project file scenario)
				foreach(string filePath in filePaths)
					CreateAndOpenFile(filePath);
			}
			else
			{
				// the selection contained no file information, so prompty the user (general scenario)
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.CheckFileExists = false;
				dialog.CheckPathExists = false;
				if(dte.ActiveDocument != null && dte.ActiveDocument.Path != null)
					dialog.InitialDirectory = Path.GetDirectoryName(dte.ActiveDocument.Path);
				dialog.Title = "New File";
				
				DialogResult result = dialog.ShowDialog();
				if(result == DialogResult.OK)
					CreateAndOpenFile(dialog.FileName);
			}
		}

		// private methods
		private void CreateAndOpenFile(string filePath)
		{
			FileInfo file = new FileInfo(filePath);
			if(!file.Exists)
				Helper.CreateFile(file);

			Helper.OpenFile(file);
		}
	}
}
