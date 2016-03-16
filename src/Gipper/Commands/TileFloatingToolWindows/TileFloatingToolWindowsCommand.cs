using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;


namespace Gipper
{
	internal class TileFloatingToolWindowsCommand : GipperCommand
	{
		// GipperCommand methods
		protected override void DoExecute()
		{
			// get the visible tool windows that are floating
			IList<GipperWindowFrame> toolWindows = WindowHelper.GetToolWindows()
				.Where(
					w =>
					{
						w.InvokeGetFramePos();
						bool isVisible = w.IsVisible();
						bool isFloating = ((w.SetFramePosition & VSSETFRAMEPOS.SFP_maskFrameMode) == VSSETFRAMEPOS.SFP_fFloat);
						return (isVisible && isFloating);
					}
				).ToList();

			// TODO: Don't assume 3x2 tiling
			// tile them
			Screen screen = Screen.AllScreens[Screen.AllScreens.Length - 1];
			WindowFrameTiler tiler = new WindowFrameTiler(screen, 3, 2);
			tiler.Tile(toolWindows);
		}
	}
}

