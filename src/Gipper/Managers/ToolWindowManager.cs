using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	public class ToolWindowManager
	{
		// fields
		private Package43 _package;
		private OpenFilesToolWindowPane _openFilesToolWindowPane;
		private FileNavigatorToolWindowPane _fileNavigatorToolWindowPane;


		// constructors
		public ToolWindowManager(Package43 package)
		{
			_package = package;
		}


		// properties
		public OpenFilesToolWindowPane OpenFilesToolWindowPane
		{
			get
			{
				if(_openFilesToolWindowPane == null)
					_openFilesToolWindowPane = (OpenFilesToolWindowPane) GetOrCreateToolWindowPane43(typeof(OpenFilesToolWindowPane));

				return _openFilesToolWindowPane;
			}
		}

		public FileNavigatorToolWindowPane FileNavigatorToolWindowPane
		{
			get
			{
				if(_fileNavigatorToolWindowPane == null)
					_fileNavigatorToolWindowPane = (FileNavigatorToolWindowPane) GetOrCreateToolWindowPane43(typeof(FileNavigatorToolWindowPane));

				return _fileNavigatorToolWindowPane;
			}
		}


		// private methods
		private ToolWindowPane43 GetOrCreateToolWindowPane43(Type toolWindowPaneType)
		{
			ToolWindowPane43 pane = null;
			try
			{
				pane = (ToolWindowPane43) _package.FindToolWindow(
					toolWindowPaneType,
					0, // the first, and only, instance of this tool window
					true	// if there are no instances of this tool window, create a new one now
				);
			}
			catch(Exception)
			{
				// HACKHACK: There's a weird bug on GOTHAM62 VS2013, where sometimes when you press Alt+V for
				// the VersionControlCommand, this method throws a System.Exception trying to load the OpenFileToolWindowPane
				// even though it clearly exists and works.
				pane = null;
			}

			return pane;
		}
	}
}

