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
	internal class TypeDefinitionCfd : ClassificationFormatDefinition
	{
		#region constants
		public const string Name = "Gipper.Cfd.TypeDefinition";
		#endregion region

		#region constructors
		public TypeDefinitionCfd()
		{
			DisplayName = "Type Definition";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.TypeFontRenderingSize;
		}
		#endregion

		#region exports
#pragma warning disable CS0649
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(TypeDefinitionCfd.Name)]
		internal static ClassificationTypeDefinition TypeDefinition;
#pragma warning restore CS0649
		#endregion
	}
}
