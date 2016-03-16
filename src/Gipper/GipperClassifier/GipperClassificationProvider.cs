using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	[Export(typeof(IClassifierProvider))]
	[ContentType("CSharp")]
	[ContentType("TypeScript")]
	[ContentType("C")]
	[ContentType("JavaScript")]
	internal class GipperClassificationProvider : IClassifierProvider
	{
		static private bool _ignoreRequest = false;


		[Import]
		internal IClassificationTypeRegistryService ClassificationRegistry = null; // Set via MEF
		[Import]
		internal IClassifierAggregatorService AggregatorService = null;

		public IClassifier GetClassifier(ITextBuffer buffer)
		{
			// HACKHACK: For some reason we sometimes get a StackOverflowException if this classifier handles the Content
			// Type "CSharp Signature Help". In fact, I think it also sometimes crashes in the immediate window, so we need to
			// either explicitly define content type in this class's ContentType CA or else filter lots more here.
			Debug.WriteLine(string.Format("Loading Classifier '{0}'...", buffer.ContentType.DisplayName));
			if(!(buffer.ContentType.DisplayName == "CSharp" || buffer.ContentType.DisplayName == "TypeScript"))
				return null;

			// BUGBUG: And actually, in Visual Studio 2015, it crashes (also StackOverflowException??) for even plain "CSharp."
			if(Gipper.GipperPackage.Dte.Version == "14.0")
				return null;

			if(VariableHelper.GetVariable("CLASSIFIER", "DISABLED") == "1")
				return null;
			
			IClassifier classifier = null;

			// TODO: Figure out why this ignore request pattern (from http://ceyhunciper.wordpress.com/category/vs-editor/)
			// is needed. I tried without it and got a StackOverflowException, so that's a big clue.
			try
			{
				if(!_ignoreRequest)
				{
					_ignoreRequest = true;
					classifier = buffer.Properties.GetOrCreateSingletonProperty<GipperClassifier>(
						delegate { return new GipperClassifier(ClassificationRegistry, AggregatorService.GetClassifier(buffer)); }
					);
				}
			}
			finally
			{
				_ignoreRequest = false;
			}

			return classifier;
		}
	}
}
