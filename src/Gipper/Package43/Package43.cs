using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	public class Package43 : Package
	{
		// static fields
		static private DTE2 _dte;

		// static properties
		static public DTE2 Dte
		{
			get
			{
				if(_dte == null)
					_dte = (DTE2) Package.GetGlobalService(typeof(DTE));

				return _dte;
			}
		}


		// virtual protected methods
		virtual protected void AddCommand(uint cmdId, GipperCommand command)
		{
			OleMenuCommandService menuCommandService = (OleMenuCommandService) GetService(typeof(IMenuCommandService));

			CommandID commandId = new CommandID(GuidList.guidGipperPackageCmdSet, (int) cmdId);
			MenuCommand menuCommand = new MenuCommand(
				(sender, e) => 
				{
					command.Execute();
				}, 
				commandId
			);
			menuCommandService.AddCommand(menuCommand);
		}

		virtual protected void AddToolWindow(uint cmdId, Type toolWindowPaneType)
		{
			OleMenuCommandService menuCommandService = (OleMenuCommandService) GetService(typeof(IMenuCommandService));

			CommandID commandId = new CommandID(GuidList.guidGipperPackageCmdSet, (int) cmdId);
			MenuCommand menuCommand = new MenuCommand(
				(sender, e) =>
				{
					ToolWindowPane pane = FindToolWindow(
						toolWindowPaneType,
						0, // the first, and only, instance of this tool window
						true	// if there are no instances of this tool window, create a new one now
					);
					IVsWindowFrame frame = (IVsWindowFrame) pane.Frame;
					ErrorHandler.ThrowOnFailure(frame.Show());
				},
				commandId
			);
			menuCommandService.AddCommand(menuCommand);
		}
	}
}

