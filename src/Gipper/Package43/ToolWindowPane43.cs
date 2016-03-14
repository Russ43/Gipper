using System;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;


namespace Gipper
{
	abstract public class ToolWindowPane43 : ToolWindowPane
	{
		// events
		public event EventHandler GotFocus;
		public event EventHandler LostFocus;


		// fields
		private EnvDTE.WindowEvents _windowEvents;
		private Control _rootControl;
		private bool _hasFocus;


		// constructor
		protected ToolWindowPane43()
			: base(null)
		{
			BitmapResourceID = 301;
			BitmapIndex = 1;

			try
			{
				_rootControl = DoCreateRootControl();
			}
			catch(BadInputException bie)
			{
				BadInputForm.ShowBadInputException(bie);
				_rootControl = new BadInputControl();
			}
		}


		// properties
		public bool HasFocus
		{
			get
			{
				return _hasFocus;
			}
		}


		// ToolWindowPane properties
		public override IWin32Window Window
		{
			get
			{
				return (IWin32Window) _rootControl;
			}
		}


		// abstract protected methods
		abstract protected Control DoCreateRootControl();


		// ToolWindowPane methods
		public override void OnToolWindowCreated()
		{
			base.OnToolWindowCreated();

			EnvDTE.DTE dte = (EnvDTE.DTE) GetService(typeof(EnvDTE.DTE));
			EnvDTE80.Events2 events = (EnvDTE80.Events2) dte.Events;
			this._windowEvents = (EnvDTE.WindowEvents) events.get_WindowEvents();
			this._windowEvents.WindowActivated += WindowEvents_WindowActivated;
		}


		// event handlers
		private void WindowEvents_WindowActivated(EnvDTE.Window gotFocusWindow, EnvDTE.Window lostFocusWindow)
		{
			Type type = this.GetType();
			GuidAttribute guidAttribute = (GuidAttribute) type.GetCustomAttribute(typeof(GuidAttribute));
			Guid classGuid = new Guid(guidAttribute.Value);


			if(gotFocusWindow != null)
			{
				Guid gotFocusWindowGuid;
				if(Guid.TryParse(gotFocusWindow.ObjectKind, out gotFocusWindowGuid))
				{
					if(gotFocusWindowGuid == classGuid)
					{
						_hasFocus = true;

						EventHandler gotFocusEvent = GotFocus;
						if(gotFocusEvent != null)
							gotFocusEvent(this, EventArgs.Empty);
					}
				}
			}

			if(lostFocusWindow != null)
			{
				Guid lostFocusWindowGuid;
				if(Guid.TryParse(lostFocusWindow.ObjectKind, out lostFocusWindowGuid))
				{
					if(lostFocusWindowGuid == classGuid)
					{
						_hasFocus = false;

						EventHandler lostFocusEvent = LostFocus;
						if(lostFocusEvent != null)
							lostFocusEvent(this, EventArgs.Empty);
					}
				}
			}
		}
	}
}
