using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

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

			// it's not a namespace declaration unless the symbol kind is namespace
			if(!(symbol != null && symbol.Kind == SymbolKind.Namespace))
				return false;

			// it's not a namespace declaration if it's inside a type declaration (eg: a fully qualified type like System.Int32)
			if(node.Ancestors().Any(sn => sn is TypeDeclarationSyntax))
				return false;

			// it's not a namespace declaration unless it's inside a namespace declaration
			if(!(node.Ancestors().Any(sn => sn is NamespaceDeclarationSyntax)))
				return false;

			// technically the dots are part of the namespace declaration, but they look silly in big fonts
			if(classifierContext.Info.ClassifiedSpan.ClassificationType == "operator")
				return false;

			return true;
		}

		public static bool IsTypeDeclaration(ClassifierContext classifierContext)
		{
			SyntaxNode node = classifierContext.Info.Node;
			ISymbol symbol = classifierContext.Info.Symbol;

			Debug.Assert(node != null);

			bool isTypeDeclaration = false;
			if(symbol != null && symbol.Kind == SymbolKind.NamedType)
			{
				if(node is ClassDeclarationSyntax
					|| node is StructDeclarationSyntax
					|| node is InterfaceDeclarationSyntax
					|| node is EnumDeclarationSyntax
					|| node is DelegateDeclarationSyntax
				)
				{
					string classificationType = classifierContext.Info.ClassifiedSpan.ClassificationType;
					if(classificationType == "class name"
						|| classificationType == "struct name"
						|| classificationType == "interface name"
						|| classificationType == "enum name"
						|| classificationType == "delegate name"
					)
					{
						isTypeDeclaration = true;

						// annoyingly, class references inside XML documentation comments (eg: <see cref="object">) 
						// will pass all the tests up to this point
						if(classifierContext.PreviousInfo.ClassifiedSpan.ClassificationType.StartsWith("xml doc comment"))
							isTypeDeclaration = false;
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
			if(symbol != null)
			{
				if(symbol.Kind == SymbolKind.Event
					|| symbol.Kind == SymbolKind.Field
					|| symbol.Kind == SymbolKind.Method
					|| symbol.Kind == SymbolKind.Property)
				{
					if(node is VariableDeclaratorSyntax
						|| node is EventDeclarationSyntax
						|| node is FieldDeclarationSyntax
						|| node is EnumMemberDeclarationSyntax
						|| node is ConstructorDeclarationSyntax
						|| node is PropertyDeclarationSyntax
						|| node is IndexerDeclarationSyntax
						|| node is MethodDeclarationSyntax
					)
					{
						string classificationType = classifierContext.Info.ClassifiedSpan.ClassificationType;
						if(classificationType == "constant name"
							|| classificationType == "event name"
							|| classificationType == "field name"
							|| classificationType == "enum member name"
							|| classificationType == "property name"
							|| classificationType == "method name"
							|| classificationType == "keyword"	// for indexers
						)
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

		public static int GetParenScale(ClassifierContext classifierContext)
		{
			SyntaxNode node = classifierContext.Info.Node;
			ISymbol symbol = classifierContext.Info.Symbol;

			Debug.Assert(node != null);

			if(classifierContext.Info.ClassifiedSpan.ClassificationType == "punctuation")
			{
				if(classifierContext.Info.Text == "(" || classifierContext.Info.Text == ")")
				{
					int currentDepth = node.Ancestors().Count();
					int maxDepth = node.DescendantNodes()
						.Where(sn => sn is ExpressionSyntax)
						.Select(sn => sn.Ancestors().Count())
						.DefaultIfEmpty(currentDepth)
						.Max();

					return (maxDepth - currentDepth) + 1;
				}
			}

			return 0;
		}
		#endregion
	}
}
