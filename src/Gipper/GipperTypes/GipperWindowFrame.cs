using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;


namespace Gipper
{
	internal class GipperWindowFrame
	{
		// constructors
		public GipperWindowFrame(IVsWindowFrame windowFrame)
		{
			Debug.Assert(windowFrame != null);

			WindowFrame = windowFrame;
		}


		// properties
		public IVsWindowFrame WindowFrame
		{
			get;
			private set;
		}

		public VSSETFRAMEPOS SetFramePosition
		{
			get;
			set;
		}

		public int X
		{
			get;
			set;
		}

		public int Y
		{
			get;
			set;
		}

		public int CX
		{
			get;
			set;
		}

		public int CY
		{
			get;
			set;
		}


		// methods
		public bool IsVisible()
		{
			return (WindowFrame.IsVisible() == VSConstants.S_OK);
		}

		public void InvokeGetFramePos()
		{
			VSSETFRAMEPOS[] pdwSFP = new VSSETFRAMEPOS[1];
			Guid pgguidRelativeTo = Guid.Empty;
			int x;
			int y;
			int cx;
			int cy;
			
			int hr = WindowFrame.GetFramePos(pdwSFP, out pgguidRelativeTo, out x, out y, out cx, out cy);
			ErrorHandler.ThrowOnFailure(hr);

			SetFramePosition = pdwSFP[0];
			X = x;
			Y = y;
			CX = cx;
			CY = cy;
		}

		public void InvokeSetFramePos()
		{
			VSSETFRAMEPOS dwSFP = VSSETFRAMEPOS.SFP_fMove | VSSETFRAMEPOS.SFP_fSize;
			Guid pgguidRelativeTo = Guid.Empty;
			WindowFrame.SetFramePos(dwSFP, ref pgguidRelativeTo, X, Y, CX, CY);
		}


		// object methods
		override public string ToString()
		{
			return WindowFrame.ToString();
		}
	}
}

