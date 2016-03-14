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
	internal class ExploreToCommand : GipperCommand
	{
		// fields
		private OpenShellTarget _target;


		// constructors
		public ExploreToCommand(OpenShellTarget target)
		{
			_target = target;
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
			psi.FileName = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.System), 
				"explorer.exe"
			);
			psi.Arguments = directory;
			Process.Start(psi);
		}
	}
}
