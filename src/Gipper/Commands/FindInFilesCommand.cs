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
	internal class FindInFilesCommand : GipperCommand
	{
		// fields
		static private FindInFilesForm _form;	// static since GipperPackage.ExecuteCommandsToolWindowCommand executes a new command each time


		// properties
		public FindInFilesForm Form
		{
			get
			{
				if(_form == null)
				{
					_form = new FindInFilesForm();
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

