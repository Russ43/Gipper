using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Company43.Common;


namespace Gipper
{
	internal class BadInputControl : GipperControl
	{
		// constructors
		public BadInputControl()
		{
			Label label = new Label();
			label.Text = "Bad Input";
			label.TextAlign = ContentAlignment.MiddleCenter;
			label.Dock = DockStyle.Fill;
			Controls.Add(label);
		}
	}
}

