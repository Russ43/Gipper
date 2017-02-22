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
using Gipper._2015.Classifiers.GipperClassifier.ClassificationFormatDefinitions;
using SemanticColorizer;
using System.Diagnostics;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	/// <summary>
	/// This is a modified version of the semantic colorizer from:
	/// https://github.com/hicknhack-software/semantic-colorizer
	/// </summary>
	internal abstract class GipperTaggerBase : ITagger<IClassificationTag>
	{
		private readonly ITextBuffer _theBuffer;
		private Cache _cache;
#pragma warning disable CS0067
		public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore CS0067

		public GipperTaggerBase(ITextBuffer buffer, IClassificationTypeRegistryService registry)
		{
			_theBuffer = buffer;
		}

		public abstract IClassificationType GetClassificationType(ClassifierContext classifierContext);

		public IEnumerable<ITagSpan<IClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
		{
			if(spans.Count == 0)
			{
				return Enumerable.Empty<ITagSpan<IClassificationTag>>();
			}
			if(_cache == null || _cache.Snapshot != spans[0].Snapshot)
			{
				// this makes me feel dirty, but otherwise it will not
				// work reliably, as TryGetSemanticModel() often will return false
				// should make this into a completely async process somehow
				var task = Cache.Resolve(_theBuffer, spans[0].Snapshot);
				try
				{
					task.Wait();
				}
				catch(Exception)
				{
					// TODO: report this to someone.
					return Enumerable.Empty<ITagSpan<IClassificationTag>>();
				}
				_cache = task.Result;
				if(_cache == null)
				{
					// TODO: report this to someone.
					return Enumerable.Empty<ITagSpan<IClassificationTag>>();
				}
			}
			return GetTagsImpl(_cache, spans);
		}

		private IEnumerable<ITagSpan<IClassificationTag>> GetTagsImpl(
				Cache doc,
				NormalizedSnapshotSpanCollection spans)
		{
			var snapshot = spans[0].Snapshot;

			IEnumerable<ClassifiedSpan> classifiedSpans =
				GetClassifiedSpansInSpans(doc.Workspace, doc.SemanticModel, spans);

			ClassifierContext classifierContext = new ClassifierContext();
			foreach(var id in classifiedSpans)
			{
				var node = GetExpression(doc.SyntaxRoot.FindNode(id.TextSpan));
				var symbol = doc.SemanticModel.GetSymbolInfo(node).Symbol;
				if(symbol == null)
					symbol = doc.SemanticModel.GetDeclaredSymbol(node);
				if(symbol == null)
				{
					continue;
				}
				ClassifiedSpanInfo classifiedSpanInfo = new ClassifiedSpanInfo(id, node, symbol);
				classifierContext.SetNextInfo(classifiedSpanInfo);
				IClassificationType classificationType = GetClassificationType(classifierContext);
				//Debug.WriteLine($"Node={node.GetType().Name},Parent={node.Parent?.GetType().Name},ClassificationType={id.ClassificationType},Symbol={symbol?.Kind},TextRng={id.TextSpan.ToString()},Text={snapshot.GetText(id.TextSpan)},CT={classificationType}");
				if(classificationType != null)
					yield return id.TextSpan.ToTagSpan(snapshot, classificationType);
			}
		}

		private bool IsSpecialType(ISymbol symbol)
		{
			var type = (INamedTypeSymbol) symbol;
			return type.SpecialType != SpecialType.None;
		}

		private bool IsExtensionMethod(ISymbol symbol)
		{
			var method = (IMethodSymbol) symbol;
			return method.IsExtensionMethod;
		}

		private SyntaxNode GetExpression(SyntaxNode node)
		{
			if(node.CSharpKind() == CSharp.SyntaxKind.Argument)
			{
				return ((CSharp.Syntax.ArgumentSyntax) node).Expression;
			}
			else if(node.CSharpKind() == CSharp.SyntaxKind.AttributeArgument)
			{
				return ((CSharp.Syntax.AttributeArgumentSyntax) node).Expression;
			}
			else if(node.VbKind() == VB.SyntaxKind.SimpleArgument)
			{
				return ((VB.Syntax.SimpleArgumentSyntax) node).Expression;
			}
			return node;
		}

		private IEnumerable<ClassifiedSpan> GetClassifiedSpansInSpans(
				Workspace workspace, SemanticModel model,
				NormalizedSnapshotSpanCollection spans)
		{
			var comparer = StringComparer.InvariantCultureIgnoreCase;
			var classifiedSpans =
				spans.SelectMany(span =>
				{
					var textSpan = TextSpan.FromBounds(span.Start, span.End);
					return Classifier.GetClassifiedSpans(model, textSpan, workspace);
				});

			return classifiedSpans;
		}

		private class Cache
		{
			public Workspace Workspace { get; private set; }
			public Document Document { get; private set; }
			public SemanticModel SemanticModel { get; private set; }
			public SyntaxNode SyntaxRoot { get; private set; }
			public ITextSnapshot Snapshot { get; private set; }

			private Cache() { }

			public static async Task<Cache> Resolve(ITextBuffer buffer, ITextSnapshot snapshot)
			{
				var workspace = buffer.GetWorkspace();
				var document = snapshot.GetOpenDocumentInCurrentContextWithChanges();
				if(document == null)
				{
					// Razor cshtml returns a null document for some reason.
					return null;
				}

				// the ConfigureAwait() calls are important,
				// otherwise we'll deadlock VS
				var semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);
				var syntaxRoot = await document.GetSyntaxRootAsync().ConfigureAwait(false);
				return new Cache
				{
					Workspace = workspace,
					Document = document,
					SemanticModel = semanticModel,
					SyntaxRoot = syntaxRoot,
					Snapshot = snapshot
				};
			}
		}
	}
}