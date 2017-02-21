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

		public static bool IsTypeDeclaration(SyntaxNode node, ISymbol symbol)
		{
			Debug.Assert(node != null);
			Debug.Assert(symbol != null);

			bool isTypeDeclaration = false;

			if(symbol.Kind == SymbolKind.NamedType)
			{
				if(node is VariableDeclaratorSyntax)
					isTypeDeclaration = true;
			}

			return isTypeDeclaration;
		}

		public static bool IsMemberDeclaration(SyntaxNode node, ISymbol symbol)
		{
			Debug.Assert(node != null);
			Debug.Assert(symbol != null);

			bool isMemberDeclaration = false;
			if(symbol.Kind == SymbolKind.Event 
				|| symbol.Kind == SymbolKind.Field
				|| symbol.Kind == SymbolKind.Method
				|| symbol.Kind == SymbolKind.Property)
			{
				if(node is VariableDeclaratorSyntax)
					isMemberDeclaration = true;
			}

			return isMemberDeclaration;
		}
		#endregion
	}
}
