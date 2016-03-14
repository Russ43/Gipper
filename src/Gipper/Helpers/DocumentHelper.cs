using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Media;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	static internal class DocumentHelper
	{
		// methods
		static public Document GetActiveDocument()
		{
			return GipperPackage.Dte.ActiveDocument;
		}

		static public TextSelection GetActiveSelection()
		{
			TextSelection activeSelection = null;
			Document activeDocument = GetActiveDocument();
			if(activeDocument != null)
				activeSelection = (TextSelection) activeDocument.Selection;

			return activeSelection;
		}

		static public string GetSelectedText(Document document)
		{
			string selectedText = null;
			TextSelection selection = (TextSelection) document.Selection;
			if(!selection.IsEmpty)
				selectedText = selection.Text;

			return selectedText;
		}

		static public IList<TextRange> GetActiveTextRanges()
		{
			IList<TextRange> activeTextRanges = new List<TextRange>();
			TextSelection textSelection = GetActiveSelection();
			if(textSelection != null)
			{
				foreach(TextRange textRange in textSelection.TextRanges)
					activeTextRanges.Add(textRange);
			}

			return activeTextRanges;
		}

		static public bool AreTextRangesEqual(TextRange left, TextRange right)
		{
			if(left == null && right == null)
				return true;

			if(left == null || right == null)
				return false;

			bool areEqual = false;
			if(left.StartPoint.AbsoluteCharOffset == right.StartPoint.AbsoluteCharOffset)
			{
				if(left.EndPoint.AbsoluteCharOffset == right.EndPoint.AbsoluteCharOffset)
					areEqual = true;
			}

			return areEqual;
		}

		static public string GetCurrentLine(Document document)
		{
			TextSelection selection = (TextSelection) document.Selection;

			int currentLine = selection.ActivePoint.Line;

			TextDocument textDocument = (TextDocument) document.Object();
			EditPoint editPoint = textDocument.CreateEditPoint();
			string line = editPoint.GetLines(currentLine, currentLine + 1);

			return line;
		}

		static public string GetSelectedTextOrCurrentLine(Document document)
		{
			string result = null;

			string selectedText = GetSelectedText(document);
			if(selectedText != null)
			{
				result = selectedText;
			}
			else
			{
				result = GetCurrentLine(document);
			}

			return result;
		}

		static public IList<string> GetFilePathsFromProjectFileSelectionOrLine(Document document)
		{
			IList<string> paths = new List<string>();

			string selection = GetSelectedTextOrCurrentLine(document);

			string projectDirectory = Path.GetDirectoryName(document.Path);

			Regex regex = new Regex(@"Include\s*=\s*""(.*)""", RegexOptions.IgnoreCase);
			MatchCollection matches = regex.Matches(selection);
			foreach(Match match in matches)
			{
				if(match.Groups.Count == 2)
				{
					string path = match.Groups[1].Value;
					path = Path.Combine(projectDirectory, path);
					paths.Add(path);
				}
			}

			return paths;
		}

		static public string GetStringInsideQuotes(Document document)
		{
			TextSelection selection = (TextSelection) document.Selection;

			string line = GetCurrentLine(document);

			int currentPosition = selection.ActivePoint.LineCharOffset - 1;	// adjust for 1-based offsets

			int leadingQuoteIndex = Math.Min(currentPosition, line.Length - 1);
			while(leadingQuoteIndex >= 0)
			{
				if(line[leadingQuoteIndex] == '"')
					break;

				--leadingQuoteIndex;
			}

			int trailingQuoteIndex = currentPosition;
			while(trailingQuoteIndex < line.Length)
			{
				if(line[trailingQuoteIndex] == '"')
					break;

				++trailingQuoteIndex;
			}

			string result = null;
			if(leadingQuoteIndex >= 0 && trailingQuoteIndex < line.Length && leadingQuoteIndex < trailingQuoteIndex)
				result = line.Substring(leadingQuoteIndex + 1, trailingQuoteIndex - (leadingQuoteIndex + 1));

			return result;
		}

		static private IVsEditorAdaptersFactoryService GetEditorAdaptersFactoryService()
		{
			IComponentModel componentModel = GipperPackage.ComponentModel;
			IVsEditorAdaptersFactoryService editorFactory = componentModel.GetService<IVsEditorAdaptersFactoryService>();
			return editorFactory;
		}

		static public IWpfTextView GetActiveTextView()
		{
			IVsTextView vsTextView;
			IWpfTextView textView = null;
			int hr = GipperPackage.TextManager.GetActiveView(0, null, out vsTextView);
			if(ErrorHandler.Succeeded(hr) && vsTextView != null)
				textView = GetEditorAdaptersFactoryService().GetWpfTextView(vsTextView);

			return textView;
		}

		static public IVsWindowFrame GetActiveWindowFrame()
		{
			IVsEditorAdaptersFactoryService editorFactory = GetEditorAdaptersFactoryService();

			IWpfTextView activeTextView = GetActiveTextView();
			Microsoft.VisualStudio.TextManager.Interop.IVsTextViewEx viewAdapter = (Microsoft.VisualStudio.TextManager.Interop.IVsTextViewEx) editorFactory.GetViewAdapter(activeTextView);

			object frameObject;
			int hr = viewAdapter.GetWindowFrame(out frameObject);
			ErrorHandler.ThrowOnFailure(hr);

			IVsWindowFrame frame = (Microsoft.VisualStudio.Shell.Interop.IVsWindowFrame) frameObject;
			return frame;
		}

		static public ITextBuffer GetActiveTextBuffer()
		{
			var componentModel = GipperPackage.ComponentModel;
			var editorAdapterFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

			IVsWindowFrame windowFrame = GetActiveWindowFrame();
			IVsTextView view = VsShellUtilities.GetTextView(windowFrame);
			IVsTextLines lines;
			int hr = view.GetBuffer(out lines);
			ErrorHandler.ThrowOnFailure(hr);

			IVsTextBuffer buffer = (IVsTextBuffer) lines;
			return editorAdapterFactoryService.GetDataBuffer(buffer);
		}
	}
}
