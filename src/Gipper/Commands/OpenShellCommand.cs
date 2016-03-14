using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gipper
{
	internal class OpenShellCommand : GipperCommand
	{
		// fields
		private OpenShellTarget _target;
		private string _fileName;
		private string _arguments;


		// constructors
		public OpenShellCommand(OpenShellTarget target)
		{
			_target = target;
			CreateCommand();
		}


		// GipperCommand methods
		protected override void DoExecute()
		{
			string directory = null;
			switch(_target)
			{
				case OpenShellTarget.CurrentFileDirectory:
					directory = Helper.GetCurrentFileDirectory();
					break;
				case OpenShellTarget.SolutionDirectory:
					directory = Helper.GetSolutionDirectory();
					break;
			}

			ProcessStartInfo psi = new ProcessStartInfo();
			psi.UseShellExecute = false;
			psi.WorkingDirectory = directory;
			psi.FileName = _fileName;
			psi.Arguments = _arguments;
			Process.Start(psi);
		}


		// private methods
		private void CreateCommand()
		{
			string shellKind = VariableHelper.GetVariable("OPENSHELL", "KIND");
			if(shellKind == null || shellKind.ToLower() == "powershell" || shellKind.ToLower() == "ps")
				shellKind = "PowerShell";
			else if(shellKind.ToLower() == "cmd")
				shellKind = "cmd";
			else
				throw new InvalidOperationException("Invalid SHELLKIND. Use 'PowerShell' or 'cmd'.");

			string shellScript = VariableHelper.GetVariable("OPENSHELL", "SCRIPT");

			if(shellKind == "PowerShell")
			{
				_fileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.System), 
					@"WindowsPowerShell\v1.0",
					"PowerShell.exe"
				);
				if(!string.IsNullOrEmpty(shellScript))
					_arguments = "-NoExit -Command \". '" + shellScript + "'\"";
				else
					_arguments = "-NoExit";
			}
			else
			{
				_fileName = "cmd.exe";
				if(!string.IsNullOrEmpty(shellScript))
					_arguments = "/s /k \"" + shellScript + "\"";
				else
					_arguments = string.Empty;
			}
		}
	}
}
