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
	internal class FindCommand : GipperCommand
	{
		// fields
		static private FindForm _form;	// static since GipperPackage.ExecuteCommandsToolWindowCommand executes a new command each time


		// properties
		public FindForm Form
		{
			get
			{
				if(_form == null)
				{
					_form = new FindForm();
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
			bool searchInSelection;

			string selectedText = DocumentHelper.GetSelectedText(activeDocument);
			if(selectedText == null)
			{
				// no text is selected, so clear the find text box and search the whole document
				defaultFindText = string.Empty;
				searchInSelection = false;
			}
			else if(selectedText.IndexOf('\r') == -1 && selectedText.IndexOf('\n') == -1)
			{
				// text within a single line is selected, so use it as the default find text and search the whole document
				defaultFindText = selectedText;
				searchInSelection = false;
			}
			else
			{
				// multiple lines are selected, so clear the find text box and search within the selected text only
				defaultFindText = string.Empty;
				searchInSelection = true;
			}

			Form.FindText = defaultFindText;
			DialogResult result = Form.ShowDialog();
			if(result == DialogResult.OK)
			{
				if(Form.FindText.Trim() != string.Empty)
				{
					Find2 find = (Find2) GipperPackage.Dte.Find;
					find.Action = vsFindAction.vsFindActionFind;
					find.FindWhat = Form.FindText;
					find.MatchCase = false;
					find.MatchInHiddenText = true;
					find.Target = searchInSelection ? vsFindTarget.vsFindTargetCurrentDocumentSelection : vsFindTarget.vsFindTargetCurrentDocument;
					find.Execute();
				}
			}
		}
	}
}

