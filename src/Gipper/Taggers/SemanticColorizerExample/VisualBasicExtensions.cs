﻿using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis;

namespace SemanticColorizer
{
	public static class VbExtensions
	{
		public static SyntaxKind VbKind(this SyntaxNode node)
		{
			return node.Kind();
		}
	}
}
