using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Gipper
{
	internal class User32Helper
	{
		// interop methods
		[DllImport("user32.dll", SetLastError = true)]
		static public extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		static public extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
	}
}
