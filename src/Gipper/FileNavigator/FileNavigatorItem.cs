using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

using Company43.Common;


namespace Gipper
{
	internal class FileNavigatorItem
	{
		// contstructors
		public FileNavigatorItem( GipperDropdownBarEntry entry)
		{
			Entry = entry;
		}


		// properties
		public GipperDropdownBarEntry Entry
		{
			get;
			private set;
		}


		// object methods
		public override string ToString()
		{
			return Entry.Text;
		}
	}
}

