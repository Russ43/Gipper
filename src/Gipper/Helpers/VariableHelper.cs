using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gipper
{
	static internal class VariableHelper
	{
		// constants
		private const string _prefix = "GIPPER";


		// methods
		static public string GetVariable(string featureName, string settingName)
		{
			//SetVariablesForTesting();

			string fullVariableName = GetFullVariableName(featureName, settingName);
			return Environment.GetEnvironmentVariable(fullVariableName);
		}


		// private methods
		static public string GetFullVariableName(string featureName, string settingName)
		{
			return string.Format("{0}_{1}_{2}", _prefix, featureName, settingName);
		}

		static private void SetVariablesForTesting()
		{
			Environment.SetEnvironmentVariable("GIPPER_OPENFILES_EXCLUDES", @".*\\obj\\.*,.*\\bin\\.*");
			Environment.SetEnvironmentVariable("GIPPER_RUNSOLUTION_FILENAME", "PowerShell.exe");
			Environment.SetEnvironmentVariable("GIPPER_RUNSOLUTION_ARGUMENTS", @"-NoExit -Command 'Import-Module blah'");
		}
	}
}
