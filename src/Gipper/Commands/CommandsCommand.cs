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
	internal class CommandsCommand : GipperCommand
	{
		// fields
		static private CommandsForm _form;	// static since GipperPackagePackage.ExecuteCommandsToolWindowCommand executes a new command each time


		// properties
		public CommandsForm Form
		{
			get
			{
				if(_form == null)
				{
					_form = new CommandsForm();
				}

				return _form;
			}
		}


		// GipperCommand methods
		protected override void DoExecute()
		{
			DialogResult result = Form.ShowDialog();
			if(result == DialogResult.OK)
				Form.ExecuteChosenCommand();
		}
	}
}

