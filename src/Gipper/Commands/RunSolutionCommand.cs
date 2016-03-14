using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	internal class RunSolutionCommand : GipperCommand
	{
		// GipperCommand methods
		protected override void DoExecute()
		{
			string fileName = VariableHelper.GetVariable("RUNSOLUTION", "FILENAME");
			if(fileName != null)
			{
				// if present, invoke the process specified via RUNSOLUTION environment variables
				string arguments = VariableHelper.GetVariable("RUNSOLUTION", "ARGUMENTS");
				try
				{
					if(arguments == null)
						System.Diagnostics.Process.Start(fileName);
					else
						System.Diagnostics.Process.Start(fileName, arguments);
				}
				catch(Win32Exception)
				{
					string command = fileName + ((arguments == null) ? string.Empty : " " + arguments);
					string message = string.Format("Unable to execute command \"{0}\".", command);
					throw new GipperException(message);
				}
			}
			else
			{
				// otherwise, default to executing the standard Debug.Start command
				GipperPackage.Dte.ExecuteCommand("Debug.Start");
			}
		}
	}
}

