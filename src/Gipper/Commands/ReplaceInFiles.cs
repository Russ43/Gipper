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
	internal class ReplaceInFilesCommand : GipperCommand
	{
		// fields
		static private ReplaceInFilesForm _form;	// static since GipperPackagePackage.ExecuteCommandsToolWindowCommand executes a new command each time


		// properties
		public ReplaceInFilesForm Form
		{
			get
			{
				if(_form == null)
				{
					_form = new ReplaceInFilesForm();
				}

				return _form;
			}
		}


		// GipperCommand methods
		protected override void DoExecute()
		{
			DialogResult result = Form.ShowDialog();
			if(result == DialogResult.OK)
			{
				throw new NotImplementedException();
			}
		}
	}
}

