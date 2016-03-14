using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

using Company43.Common;


namespace Gipper
{
	abstract internal class GipperForm : Form
	{
		// constructors
		protected GipperForm()
		{
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			ShowInTaskbar = false;

			// by default, create a perfectly-shaped form centered on the primary screen
			Height = Screen.PrimaryScreen.WorkingArea.Height / 2;
			Width = (int) Math.Min(Height * Helper.GoldenRatio, Screen.PrimaryScreen.WorkingArea.Width * .90);
			StartPosition = FormStartPosition.CenterScreen;

			Font = StyleHelper.FormFont;

			KeyPress += GipperForm_KeyPress;
		}


		// event handlers
		private void GipperForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char) Keys.Escape)
				Close();
		}
	}
}
