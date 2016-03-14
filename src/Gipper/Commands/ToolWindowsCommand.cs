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
	internal class ToolWindowsCommand : GipperCommand
	{
		// GipperCommand methods
		protected override void DoExecute()
		{
			ToolWindowsForm form = new ToolWindowsForm();
			form.ShowDialog();

			form.ExecuteChosenCommand();
		}
	}
}
