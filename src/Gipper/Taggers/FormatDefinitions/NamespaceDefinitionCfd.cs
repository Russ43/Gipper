using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Gipper._2015.Classifiers.GipperClassifier.ClassificationFormatDefinitions
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Name)]
	[Name(Name)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal class NamespaceDefinitionCfd : ClassificationFormatDefinition
	{
		#region constants
		public const string Name = "Gipper.Cfd.NamespaceDefinition";
		#endregion region

		#region constructors
		public NamespaceDefinitionCfd()
		{
			DisplayName = "Namespace Definition";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.NamespaceFontRenderingSize;
		}
		#endregion

		#region exports
#pragma warning disable CS0649
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(NamespaceDefinitionCfd.Name)]
		internal static ClassificationTypeDefinition NamespaceDefinition;
#pragma warning restore CS0649
		#endregion
	}
}
