using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Gipper._2015.Classifiers.GipperClassifier.ClassificationFormatDefinitions;
using System.Diagnostics;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class GipperTagger : GipperTaggerBase
	{
		#region fields
		private readonly IClassificationType _namespaceType;
		private readonly IClassificationType _typeType;
		private readonly IClassificationType _memberType;
		#endregion

		#region constructors
		public GipperTagger(ITextBuffer buffer, IClassificationTypeRegistryService registry)
			: base(buffer, registry)
		{
			_namespaceType = registry.GetClassificationType(NamespaceDefinitionCfd.Name);
			_typeType = registry.GetClassificationType(TypeDefinitionCfd.Name);
			_memberType = registry.GetClassificationType(MemberDefinitionCfd.Name);
		}
		#endregion

		#region GipperTaggerBase methods
		public override IClassificationType GetClassificationType(SyntaxNode node, ISymbol symbol)
		{
			if(ClassificationHelper.IsNamespaceDeclaration(node, symbol))
				return _namespaceType;
			if(ClassificationHelper.IsTypeDeclaration(node, symbol))
				return _typeType;
			if(ClassificationHelper.IsMemberDeclaration(node, symbol))
				return _memberType;

			return null;
		}
		#endregion
	}
}
