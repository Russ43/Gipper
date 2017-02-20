using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using CSharp = Microsoft.CodeAnalysis.CSharp;
using VB = Microsoft.CodeAnalysis.VisualBasic;
using System.Diagnostics;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal static class ClassificationHelper
	{
		#region functions
		public static string GetClassificationType(SyntaxNode node, ISymbol symbol)
		{
			Debug.Assert(node != null);
			Debug.Assert(symbol != null);

			if(symbol.Kind == SymbolKind.Namespace)
			{
				if(node.FullSpan == null)
					return "6";

				if(node.Parent is Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax)
					return "namespace";
			}

			return null;
		}
		#endregion
	}
}
