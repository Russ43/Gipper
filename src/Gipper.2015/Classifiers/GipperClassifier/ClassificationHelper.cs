using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal static class ClassificationHelper
	{
		#region functions
		public static bool IsNamespaceDeclaration(SyntaxNode node, ISymbol symbol)
		{
			Debug.Assert(node != null);
			Debug.Assert(symbol != null);

			bool isNamepsaceDeclaration = false;
			if(symbol.Kind == SymbolKind.Namespace)
			{
				if(node.Parent is NamespaceDeclarationSyntax)
					isNamepsaceDeclaration = true;
			}

			return isNamepsaceDeclaration;
		}
		#endregion
	}
}
