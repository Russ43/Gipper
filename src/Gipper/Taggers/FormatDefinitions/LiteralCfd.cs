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
	internal class LiteralDefinitionCfd : ClassificationFormatDefinition
	{
		#region constants
		public const string Name = "Gipper.Cfd.LiteralDefinition";
		#endregion region

		#region constructors
		public LiteralDefinitionCfd()
		{
			DisplayName = "Literal Definition";
			FontTypeface = StyleHelper.LiteralFontFace;
			IsBold = true;
		}
		#endregion

		#region exports
#pragma warning disable CS0649
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(LiteralDefinitionCfd.Name)]
		internal static ClassificationTypeDefinition LiteralDefinition;
#pragma warning restore CS0649
		#endregion
	}
}
