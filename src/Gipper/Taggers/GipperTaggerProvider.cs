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

namespace Gipper._2015.Classifiers.GipperClassifier
{
	[Export(typeof(ITaggerProvider))]
	[ContentType("CSharp")]
	[ContentType("Basic")]
	[TagType(typeof(IClassificationTag))]
	internal class GipperTaggerProvider : ITaggerProvider
	{
#pragma warning disable CS0649
		[Import]
		internal IClassificationTypeRegistryService ClassificationRegistry; // Set via MEF
#pragma warning restore CS0649

		public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
		{
			return (ITagger<T>) new GipperTagger(buffer, ClassificationRegistry);
		}
	}
}
