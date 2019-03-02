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
	internal class MemberDefinitionCfd : ClassificationFormatDefinition
	{
		#region constants
		public const string Name = "Gipper.Cfd.MemberDefinition";
		#endregion region

		#region constructors
		public MemberDefinitionCfd()
		{
			DisplayName = "Member Definition";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
		#endregion

		#region exports
#pragma warning disable CS0649
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(MemberDefinitionCfd.Name)]
		internal static ClassificationTypeDefinition MemberDefinition;
#pragma warning restore CS0649
		#endregion
	}
}
