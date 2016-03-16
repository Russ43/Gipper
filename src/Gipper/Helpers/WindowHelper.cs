using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using MsVsShell = Microsoft.VisualStudio.Shell;
using VsConstants = Microsoft.VisualStudio.VSConstants;
using ErrorHandler = Microsoft.VisualStudio.ErrorHandler;

namespace Gipper
{
	static internal class WindowHelper
	{
		// methods
		static public void RefreshList()
		{
			IList<IVsWindowFrame> framesList = new List<IVsWindowFrame>();
			IList<string> toolWindowNames = new List<string>();

			// Get the UI Shell service
			IVsUIShell4 uiShell = (IVsUIShell4) Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsUIShell));
			// Get the tool windows enumerator
			IEnumWindowFrames windowEnumerator;

			uint flags = unchecked(((uint) __WindowFrameTypeFlags.WINDOWFRAMETYPE_Tool | (uint) __WindowFrameTypeFlags.WINDOWFRAMETYPE_Uninitialized));
			ErrorHandler.ThrowOnFailure(uiShell.GetWindowEnum(flags, out windowEnumerator));

			IVsWindowFrame[] frame = new IVsWindowFrame[1];
			uint fetched = 0;
			int hr = VsConstants.S_OK;
			// Note that we get S_FALSE when there is no more item, so only loop while we are getting S_OK
			while(hr == VsConstants.S_OK)
			{
				// For each tool window, add it to the list
				hr = windowEnumerator.Next(1, frame, out fetched);
				ErrorHandler.ThrowOnFailure(hr);
				if(fetched == 1)
				{
					if(frame[0].IsVisible() == VsConstants.S_OK)
					{
						// We successfully retrieved a window frame, update our lists
						string caption = (string) GetProperty(frame[0], (int) __VSFPROPID.VSFPROPID_Caption);
						toolWindowNames.Add(caption);
						framesList.Add(frame[0]);
					}
				}
			}
		}

		static public IList<GipperWindowFrame> GetToolWindows()
		{
			IList<GipperWindowFrame> toolWindows = new List<GipperWindowFrame>();
	
			IVsUIShell4 uiShell = (IVsUIShell4) Package.GetGlobalService(typeof(SVsUIShell));

			IEnumWindowFrames windowEnumerator;
			uint flags = unchecked(((uint) __WindowFrameTypeFlags.WINDOWFRAMETYPE_Tool | (uint) __WindowFrameTypeFlags.WINDOWFRAMETYPE_Uninitialized));
			ErrorHandler.ThrowOnFailure(uiShell.GetWindowEnum(flags, out windowEnumerator));

			int hr = VsConstants.S_OK;
			IVsWindowFrame[] frameBuffer = new IVsWindowFrame[1];
			uint fetched;
			while(hr == VsConstants.S_OK)
			{
				hr = windowEnumerator.Next(1, frameBuffer, out fetched);
				ErrorHandler.ThrowOnFailure(hr);
				if(fetched == 1)
				{
					IVsWindowFrame frame = frameBuffer[0];
					GipperWindowFrame gipperFrame = new GipperWindowFrame(frame);
					toolWindows.Add(gipperFrame);
				}
			}

			return toolWindows;
		}

		static internal object GetProperty(IVsWindowFrame frame, int propertyID)
		{
			object result = null;
			ErrorHandler.ThrowOnFailure(frame.GetProperty(propertyID, out result));
			return result;
		}
	}
}
