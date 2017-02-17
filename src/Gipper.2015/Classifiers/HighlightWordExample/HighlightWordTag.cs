﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace Gipper._2015.Classifiers
{
	internal class HighlightWordTag : TextMarkerTag
	{
		public HighlightWordTag()
			: base("MarkerFormatDefinition/HighlightWordFormatDefinition")
		{
		}
	}
}
