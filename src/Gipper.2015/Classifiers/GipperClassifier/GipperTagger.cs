using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Gipper._2015.Classifiers.GipperClassifier.ClassificationFormatDefinitions;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class GipperTagger : GipperTaggerBase
	{
		#region fields
		private readonly IClassificationType _namespaceType;
		#endregion

		#region constructors
		public GipperTagger(ITextBuffer buffer, IClassificationTypeRegistryService registry)
			: base(buffer, registry)
		{
			_namespaceType = registry.GetClassificationType(NamespaceDefinitionCfd.Name);
		}
		#endregion

		#region GipperTaggerBase methods
		public override IClassificationType GetClassificationType(SyntaxNode node, ISymbol symbol)
		{
			if(ClassificationHelper.IsNamespaceDeclaration(node, symbol))
				return _namespaceType;

			return null;
		}
		#endregion
	}
}
