using System;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;


namespace Gipper
{
	[Guid("A34E6909-850A-4EA9-9791-4137CD3C4C59")]
	public class FileNavigatorToolWindowPane : ToolWindowPane43
	{
		// fields
		private FileNavigatorControl _fileNavigatorControl;


		// constructors
		public FileNavigatorToolWindowPane()
		{
			Caption = "File Navigator";

			GotFocus += FileNavigatorToolWindowPane_GotFocus;
		}


		// ToolWindowPane43 methods
		override protected Control DoCreateRootControl()
		{
			_fileNavigatorControl = new FileNavigatorControl();
			return _fileNavigatorControl;
		}


		// event handlers
		private void FileNavigatorToolWindowPane_GotFocus(object sender, EventArgs e)
		{
			if(_fileNavigatorControl != null)
				_fileNavigatorControl.Populate();
		}
	}
}
