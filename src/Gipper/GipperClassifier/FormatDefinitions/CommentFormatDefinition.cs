using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = GipperConstants.FormatDefinition_Comment_Name)]
	[Name(GipperConstants.FormatDefinition_Comment_Name)]
	[UserVisible(true)] //this should be visible to the end user
	[Order(Before = Priority.Default)] //set the priority to be after the default classifiers
	internal class CommentFormatDefinition : ClassificationFormatDefinition
	{
		public CommentFormatDefinition()
		{
			DisplayName = GipperConstants.FormatDefinition_Comment_DisplayName;

			//FontTypeface = new Typeface("Lindsey Regular");
			//FontTypeface = new Typeface("Andy");
			//FontTypeface = new Typeface("Segoe Marker Regular");
			//FontTypeface = new Typeface("Segoe Print");
			FontTypeface = new Typeface("Comic Sans MS");
			//FontTypeface = new Typeface("Miramonte");
			//FontTypeface = new Typeface("Maiandra GD");

			//FontRenderingSize = 10;
		}
	}
}
