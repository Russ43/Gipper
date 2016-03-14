using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = GipperConstants.FormatDefinition_Comment_ToDo_Name)]
	[Name(GipperConstants.FormatDefinition_Comment_ToDo_Name)]
	[UserVisible(true)] //this should be visible to the end user
	[Order(Before = Priority.Default)] //set the priority to be after the default classifiers
	internal class TodoCommentFormatDefinition : ClassificationFormatDefinition
	{
		public TodoCommentFormatDefinition()
		{
			DisplayName = GipperConstants.FormatDefinition_Comment_ToDo_DisplayName;
			BackgroundColor = Colors.Yellow;
			ForegroundColor = Colors.DarkGreen;
			IsBold = true;
		}
	}
}
