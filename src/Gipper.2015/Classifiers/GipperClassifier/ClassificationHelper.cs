using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal static class ClassificationHelper
	{
		#region functions
		public static bool IsNamespaceDeclaration(ClassifierContext classifierContext)
		{
			SyntaxNode node = classifierContext.Info.Node;
			ISymbol symbol = classifierContext.Info.Symbol;

			Debug.Assert(node != null);

			bool isNamepsaceDeclaration = false;
			if(symbol != null && symbol.Kind == SymbolKind.Namespace)
			{
				if(node.Parent is NamespaceDeclarationSyntax)
					isNamepsaceDeclaration = true;
			}

			return isNamepsaceDeclaration;
		}

		public static bool IsTypeDeclaration(ClassifierContext classifierContext)
		{
			SyntaxNode node = classifierContext.Info.Node;
			ISymbol symbol = classifierContext.Info.Symbol;

			Debug.Assert(node != null);

			bool isTypeDeclaration = false;
			if(symbol != null && symbol.Kind == SymbolKind.NamedType)
			{
				if(classifierContext.Info.ClassifiedSpan.ClassificationType == "class name")
				{
					if(node is ClassDeclarationSyntax
						|| node is StructDeclarationSyntax
						|| node is InterfaceDeclarationSyntax
						|| node is EnumDeclarationSyntax
						|| node is DelegateDeclarationSyntax)
					{
						isTypeDeclaration = true;
					}
				}
			}

			return isTypeDeclaration;
		}

		public static bool IsMemberDeclaration(ClassifierContext classifierContext)
		{
			SyntaxNode node = classifierContext.Info.Node;
			ISymbol symbol = classifierContext.Info.Symbol;

			Debug.Assert(node != null);

			bool isMemberDeclaration = false;
			if(classifierContext.Info.ClassifiedSpan.ClassificationType == "identifier")
			{
				if(symbol != null)
				{
					if(symbol.Kind == SymbolKind.Event
						|| symbol.Kind == SymbolKind.Field
						|| symbol.Kind == SymbolKind.Method
						|| symbol.Kind == SymbolKind.Property)
					{
						if(node is VariableDeclaratorSyntax
							|| node is ConstructorDeclarationSyntax
							|| node is MethodDeclarationSyntax
							|| node is PropertyDeclarationSyntax)
						{
							if(classifierContext.PreviousInfo?.ClassifiedSpan.ClassificationType != "xml doc comment - attribute quotes")   // ignore XML doc cref indentifiers
								isMemberDeclaration = true;
						}
					}
				}
			}

			return isMemberDeclaration;
		}

		public static bool IsCommentDeclaration(ClassifierContext classifierContext)
		{
			return classifierContext.Info.ClassifiedSpan.ClassificationType.Contains("comment");
		}

		public static bool IsLiteral(ClassifierContext classifierContext)
		{
			return (classifierContext.Info.Node is LiteralExpressionSyntax
				|| classifierContext.Info.Node is InterpolatedStringTextSyntax
				|| classifierContext.Info.Node is InterpolationSyntax);
		}
		#endregion
	}
}
