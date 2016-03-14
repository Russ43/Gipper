using System;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;


namespace Gipper
{
	[Guid("2A23676C-4E53-4AE2-AAEF-09D527BBFCB4")]
	public class OpenFilesToolWindowPane : ToolWindowPane43
	{
		// fields
		private OpenFilesControl _openFilesControl;


		// constructors
		public OpenFilesToolWindowPane()
		{
			SetCaption(1);
		}


		// properties
		public FileInfo HighlightedFile
		{
			get
			{
				FileInfo highlightedFile = null;
				if(_openFilesControl != null)
					highlightedFile = _openFilesControl.HighlightedFile;

				return highlightedFile;
			}
		}


		// ToolWindowPane43 methods
		override protected Control DoCreateRootControl()
		{
			_openFilesControl = new OpenFilesControl();
			_openFilesControl.RepopulateProgressChanged += OpenFilesControl_RepopulateProgressChanged;
			return _openFilesControl;
		}


		// private methods
		private void SetCaption(double repopulateProgress)
		{
			string suffix = string.Empty;
			if(repopulateProgress < 1)
			{
				suffix = string.Format(" ({0:N0}% populated)", repopulateProgress * 100);
			}

			this.Caption = "List of Files" + suffix;
		}


		// event handlers
		private void OpenFilesControl_RepopulateProgressChanged(object sender, EventArgs e)
		{
			SetCaption(_openFilesControl.RepopulateProgress);
		}
	}
}
