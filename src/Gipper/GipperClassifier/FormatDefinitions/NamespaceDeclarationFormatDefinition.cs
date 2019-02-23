using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = GipperConstants.FormatDefinition_Declaration_Namespace_Name)]
	[Name(GipperConstants.FormatDefinition_Declaration_Namespace_Name)]
	[UserVisible(true)] //this should be visible to the end user
	[Order(Before = Priority.Default)] //set the priority to be after the default classifiers
	internal class NamespaceDeclarationFormatDefinition : ClassificationFormatDefinition
	{
		public NamespaceDeclarationFormatDefinition()
		{
			DisplayName = GipperConstants.FormatDefinition_Declaration_Namespace_DisplayName;
			FontTypeface = new Typeface("Segoe UI Bold");
			IsBold = true;
			FontRenderingSize = 34;
		}
	}
}
