using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

using EnvDTE;


namespace Gipper
{
	internal class ShowClassificationsCommand : GipperCommand
	{
		// constructors
		public ShowClassificationsCommand()
		{
		}


		// GipperCommand methods
		protected override void DoExecute()
		{
			TextSelection selection = DocumentHelper.GetActiveSelection();
			if(selection == null)
				return;

			TextRange textRange = selection.TextRanges.Item(1);
			int startOffset = textRange.StartPoint.AbsoluteCharOffset;
			int length = textRange.EndPoint.AbsoluteCharOffset - startOffset;

			ITextBuffer buffer = DocumentHelper.GetActiveTextBuffer();
			IList<Microsoft.VisualStudio.Text.Classification.ClassificationSpan> existingSpans = GipperPackage.ClassifierAggregatorService.GetClassificationSpans(
				new Microsoft.VisualStudio.Text.SnapshotSpan(buffer.CurrentSnapshot, new Microsoft.VisualStudio.Text.Span(startOffset, length))
			);

			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < existingSpans.Count; ++i)
			{
				if(i > 0)
					sb.Append("; ");

				sb.Append(Helper.FormatClassificationType(existingSpans[i].ClassificationType));
			}

			Helper.ShowInformationDialog("Classification", sb.ToString());
		}
	}
}

