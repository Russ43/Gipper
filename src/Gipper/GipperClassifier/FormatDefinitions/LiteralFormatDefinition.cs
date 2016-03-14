﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = GipperConstants.FormatDefinition_Literal_Name)]
	[Name(GipperConstants.FormatDefinition_Literal_Name)]
	[UserVisible(true)] //this should be visible to the end user
	[Order(Before = Priority.Default)] //set the priority to be after the default classifiers
	internal class LiteralFormatDefinition : ClassificationFormatDefinition
	{
		public LiteralFormatDefinition()
		{
			DisplayName = GipperConstants.FormatDefinition_Literal_DisplayName;

			FontTypeface = new Typeface("Courier New");
			IsBold = true;
		}
	}
}
