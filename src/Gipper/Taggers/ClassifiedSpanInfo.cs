﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class ClassifiedSpanInfo
	{
		#region constructors
		public ClassifiedSpanInfo(ClassifiedSpan classifiedSpan, SyntaxNode node, ISymbol symbol, string text)
		{
			ClassifiedSpan = classifiedSpan;
			Node = node;
			Symbol = symbol;
			Text = text;
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

		public string Text
		{
			get;
			private set;
		}
		#endregion
	}
}
