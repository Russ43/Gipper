using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = GipperConstants.FormatDefinition_Declaration_Member_Name)]
	[Name(GipperConstants.FormatDefinition_Declaration_Member_Name)]
	[UserVisible(true)] //this should be visible to the end user
	[Order(Before = Priority.Default)] //set the priority to be after the default classifiers
	internal class MemberDeclarationFormatDefinition : ClassificationFormatDefinition
	{
		public MemberDeclarationFormatDefinition()
		{
			DisplayName = GipperConstants.FormatDefinition_Declaration_Member_DisplayName;
			FontRenderingSize = 17;
			IsBold = true;
		}
	}
}
