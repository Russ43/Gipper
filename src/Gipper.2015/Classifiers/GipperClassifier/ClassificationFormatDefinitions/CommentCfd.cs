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
	internal class CommentDefinitionCfd : ClassificationFormatDefinition
	{
		#region constants
		public const string Name = "Gipper.Cfd.CommentDefinition";
		#endregion region

		#region constructors
		public CommentDefinitionCfd()
		{
			DisplayName = "Comment Definition";
			FontTypeface = StyleHelper.CommentFontFace;
		}
		#endregion

		#region exports
#pragma warning disable CS0649
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(CommentDefinitionCfd.Name)]
		internal static ClassificationTypeDefinition CommentDefinition;
#pragma warning restore CS0649
		#endregion
	}
}
