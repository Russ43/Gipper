using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Gipper._2015.Classifiers.GipperClassifier;

namespace SemanticColorizer
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.FieldFormat)]
	[Name(Constants.FieldFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticFieldFormat : ClassificationFormatDefinition
	{

		public SemanticFieldFormat()
		{
			DisplayName = "Semantic Field";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.EnumFieldFormat)]
	[Name(Constants.EnumFieldFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticEnumFieldFormat : ClassificationFormatDefinition
	{
		public SemanticEnumFieldFormat()
		{
			DisplayName = "Semantic Enum Field";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.ExtensionMethodFormat)]
	[Name(Constants.ExtensionMethodFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticExtensionMethodFormat : ClassificationFormatDefinition
	{
		public SemanticExtensionMethodFormat()
		{
			DisplayName = "Semantic Extension Method";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.StaticMethodFormat)]
	[Name(Constants.StaticMethodFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticStaticMethodFormat : ClassificationFormatDefinition
	{
		public SemanticStaticMethodFormat()
		{
			DisplayName = "Semantic Static Method";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.NormalMethodFormat)]
	[Name(Constants.NormalMethodFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticNormalMethodFormat : ClassificationFormatDefinition
	{
		public SemanticNormalMethodFormat()
		{
			DisplayName = "Semantic Normal Method";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.ConstructorFormat)]
	[Name(Constants.ConstructorFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticConstructorFormat : ClassificationFormatDefinition
	{
		public SemanticConstructorFormat()
		{
			DisplayName = "Semantic Constructor";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.TypeParameterFormat)]
	[Name(Constants.TypeParameterFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticTypeParameterFormat : ClassificationFormatDefinition
	{
		public SemanticTypeParameterFormat()
		{
			DisplayName = "Semantic Type Parameter";
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.ParameterFormat)]
	[Name(Constants.ParameterFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticParameterFormat : ClassificationFormatDefinition
	{
		public SemanticParameterFormat()
		{
			DisplayName = "Semantic Parameter";
		}
	}

	//[Export(typeof(EditorFormatDefinition))]
	//[ClassificationType(ClassificationTypeNames = Constants.NamespaceFormat)]
	//[Name(Constants.NamespaceFormat)]
	//[UserVisible(true)]
	//[Order(After = Priority.Default)]
	//internal sealed class SemanticNamespaceFormat : ClassificationFormatDefinition
	//{
	//	public SemanticNamespaceFormat()
	//	{
	//		DisplayName = "Semantic Namespace";
	//		FontTypeface = StyleHelper.DefinitionFontFace;
	//		FontRenderingSize = StyleHelper.NamespaceFontRenderingSize;
	//	}
	//}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.PropertyFormat)]
	[Name(Constants.PropertyFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticPropertyFormat : ClassificationFormatDefinition
	{
		public SemanticPropertyFormat()
		{
			DisplayName = "Semantic Property";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.MemberFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.LocalFormat)]
	[Name(Constants.LocalFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticLocalFormat : ClassificationFormatDefinition
	{
		public SemanticLocalFormat()
		{
			DisplayName = "Semantic Local";
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.TypeSpecialFormat)]
	[Name(Constants.TypeSpecialFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticTypeSpecialFormat : ClassificationFormatDefinition
	{
		public SemanticTypeSpecialFormat()
		{
			DisplayName = "Semantic Special Type";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.TypeFontRenderingSize;
		}
	}

	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Constants.TypeNormalFormat)]
	[Name(Constants.TypeNormalFormat)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal sealed class SemanticTypeNormalFormat : ClassificationFormatDefinition
	{
		public SemanticTypeNormalFormat()
		{
			DisplayName = "Semantic Normal Type";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.TypeFontRenderingSize;
		}
	}
}
