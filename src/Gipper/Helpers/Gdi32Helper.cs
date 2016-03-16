using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Gipper
{
	static public class Gdi32Helper
	{
		// interop methods
		[DllImport("gdi32.dll")]
		static public extern int GetDeviceCaps(IntPtr hdc, int nIndex);
	

		// nested enums
		public enum DeviceCap
		{
			VERTRES = 10,
			LOGPIXELSX = 88,
			LOGPIXELSY = 90,
			DESKTOPVERTRES = 117
		}
	}
}

