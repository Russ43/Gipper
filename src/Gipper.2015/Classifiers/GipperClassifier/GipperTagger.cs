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
		public override IClassificationType GetClassificationType(ClassifierContext classifierContext)
		{
			if(ClassificationHelper.IsNamespaceDeclaration(classifierContext))
				return _namespaceType;
			if(ClassificationHelper.IsTypeDeclaration(classifierContext))
				return _typeType;
			if(ClassificationHelper.IsMemberDeclaration(classifierContext))
				return _memberType;

			return null;
		}
		#endregion
	}
}
