using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class ClassifiedSpanInfo
	{
		#region constructors
		public ClassifiedSpanInfo(ClassifiedSpan classifiedSpan, SyntaxNode node, ISymbol symbol)
		{
			ClassifiedSpan = classifiedSpan;
			Node = node;
			Symbol = symbol;
		}
		#endregion

		#region properties
		public ClassifiedSpan ClassifiedSpan
		{
			get;
			private set;
		}

		public SyntaxNode Node
		{
			get;
			private set;
		}

		public ISymbol Symbol
		{
			get;
			private set;
		}
		#endregion
	}
}
