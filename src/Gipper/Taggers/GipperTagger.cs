using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Gipper._2015.Classifiers.GipperClassifier.ClassificationFormatDefinitions;
using System.Diagnostics;
using System.Linq;
using System;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class GipperTagger : GipperTaggerBase
	{
		#region fields
		private readonly IClassificationType _namespaceType;
		private readonly IClassificationType _typeType;
		private readonly IClassificationType _memberType;
		private readonly IClassificationType _commentType;
		private readonly IClassificationType _literalType;
		private readonly IClassificationType[] _parenTypes;
		#endregion

		#region constructors
		public GipperTagger(ITextBuffer buffer, IClassificationTypeRegistryService registry)
			: base(buffer, registry)
		{
			_namespaceType = registry.GetClassificationType(NamespaceDefinitionCfd.Name);
			_typeType = registry.GetClassificationType(TypeDefinitionCfd.Name);
			_memberType = registry.GetClassificationType(MemberDefinitionCfd.Name);
			_commentType = registry.GetClassificationType(CommentDefinitionCfd.Name);
			_literalType = registry.GetClassificationType(LiteralDefinitionCfd.Name);
			_parenTypes = Enumerable.Range(0, 16)
				.Select(idx => registry.GetClassificationType(ParenDefinitionCfd.Name + "_" + idx.ToString()))
				.ToArray();
		}
		#endregion

		#region GipperTaggerBase methods
		public override IClassificationType GetClassificationType(ClassifierContext classifierContext)
		{
			if(ClassificationHelper.IsNamespaceDeclaration(classifierContext))
				return _namespaceType;
			if(ClassificationHelper.IsTypeDeclaration(classifierContext))
				return _typeType;
			if(ClassificationHelper.IsMemberDeclaration(classifierContext))
				return _memberType;
			if(ClassificationHelper.IsCommentDeclaration(classifierContext))
				return _commentType;
			if(ClassificationHelper.IsLiteral(classifierContext))
				return _literalType;

			int parenScale = ClassificationHelper.GetParenScale(classifierContext);
			if(parenScale > 0)
				return _parenTypes[Math.Min(parenScale, 15)];

			return null;
		}
		#endregion
	}
}
