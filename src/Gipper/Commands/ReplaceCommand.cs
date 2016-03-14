using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	internal class ReplaceCommand : GipperCommand
	{
		// static fields
		static private ReplaceForm _form;	// static since GipperPackage.ExecuteCommandsToolWindowCommand executes a new command each time


		// fields
		private bool _searchInSelection;
		private Find2 _find;
		private TextRange _firstSkipTextRange;


		// properties
		public ReplaceForm Form
		{
			get
			{
				if(_form == null)
				{
					_form = new ReplaceForm();
					_form.FindClicked += Form_FindClicked;
					_form.ReplaceClicked += Form_ReplaceClicked;
					_form.SkipClicked += Form_SkipClicked;
					_form.ReplaceAllClicked += Form_ReplaceAllClicked;
				}

				return _form;
			}
		}


		// GipperCommand methods
		protected override void DoExecute()
		{
			Document activeDocument = DocumentHelper.GetActiveDocument();
			if(activeDocument == null)
				return;

			string defaultFindText;

			string selectedText = DocumentHelper.GetSelectedText(activeDocument);
			if(selectedText == null)
			{
				// no text is selected, so clear the find text box and search the whole document
				defaultFindText = string.Empty;
				_searchInSelection = false;
			}
			else if(selectedText.IndexOf('\r') == -1 && selectedText.IndexOf('\n') == -1)
			{
				// text within a single line is selected, so use it as the default find text and search the whole document
				defaultFindText = selectedText;
				_searchInSelection = false;
			}
			else
			{
				// multiple lines are selected, so clear the find text box and search within the selected text only
				defaultFindText = string.Empty;
				_searchInSelection = true;
			}

			Form.FindText = defaultFindText;
			Form.ReplaceWithText = string.Empty;
			Form.IsReadyToReplace = false;
			Form.FocusFindTextBox();
			DialogResult result = Form.ShowDialog();
		}


		// private methods
		private void CreateFind()
		{
			_find = (Find2) GipperPackage.Dte.Find;
			_find.FindWhat = Form.FindText;
			_find.ReplaceWith = Form.ReplaceWithText;
			_find.MatchCase = false;
			_find.MatchInHiddenText = true;
			_find.Target = _searchInSelection ? vsFindTarget.vsFindTargetCurrentDocumentSelection : vsFindTarget.vsFindTargetCurrentDocument;
		}


		// event handlers
		private void Form_FindClicked(object sender, EventArgs e)
		{
			if(Form.FindText.Trim() == string.Empty)
				return;

			// When the initial "Find" button is clicked, try to find (but not yet replace) the first instance.
			CreateFind();
			_find.Action = vsFindAction.vsFindActionFind;
			vsFindResult result = _find.Execute();
			if(result == vsFindResult.vsFindResultFound)
			{
				// instance found, switch to replace mode
				Form.IsReadyToReplace = true;
				Form.FocusReplaceButton();
			}
			else
			{
				// no instances to replace, so hide the form
				Form.Hide();
			}			

			// Clear the skip text range
			_firstSkipTextRange = null;
		}

		private void Form_ReplaceClicked(object sender, EventArgs e)
		{
			// When the "Replace" button is clicked, replace the selected instance and look for the next one.
			_find.Action = vsFindAction.vsFindActionReplace;
			vsFindResult result = _find.Execute();
			if(result == vsFindResult.vsFindResultReplaceAndNotFound)
			{
				// no more instances to replace
				Form.Hide();
			}
		}

		private void Form_SkipClicked(object sender, EventArgs e)
		{
			// see if we've gone full circle
			if(DocumentHelper.AreTextRangesEqual(_firstSkipTextRange, DocumentHelper.GetActiveTextRanges()[0]))
			{
				Form.Hide();
				return;
			}

			// find the next instance.
			_find.Action = vsFindAction.vsFindActionFind;
			vsFindResult result = _find.Execute();
			if(result == vsFindResult.vsFindResultFound)
			{
				if(_firstSkipTextRange == null)
				{
					// cache the location so we know when we're back to the beginning
					_firstSkipTextRange = DocumentHelper.GetActiveTextRanges()[0];
				}
			}
			else
			{
				// no more instances to replace, so hide the form
				Form.Hide();
			}
		}

		private void Form_ReplaceAllClicked(object sender, EventArgs e)
		{
			// When the initial "Find" button is clicked, try to find (but not yet replace) the first instance.
			_find.Action = vsFindAction.vsFindActionReplaceAll;
			_find.Execute();
			Form.Hide();
		}
	}
}

