using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

using Company43.Common;


namespace Gipper
{
	internal class ReplaceInFilesForm : GipperForm
	{
		// fields
		private TextBox _findTextBox;
		private TextBox _replaceWithTextBox;
		private Button _okButton;
		private Button _cancelButton;


		// constructors
		public ReplaceInFilesForm()
		{
			Text = "Replace in Files";

			int margin = 10;

			_findTextBox = new TextBox();
			_findTextBox.Top = margin;
			_findTextBox.Left = margin;
			_findTextBox.Height = (int) (Font.Size * 2);
			_findTextBox.Width = ClientSize.Width - (2 * margin);
			_findTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Controls.Add(_findTextBox);

			_replaceWithTextBox = new TextBox();
			_replaceWithTextBox.Top = _findTextBox.Bottom + margin;
			_replaceWithTextBox.Left = _findTextBox.Left;
			_replaceWithTextBox.Height = _findTextBox.Height;
			_replaceWithTextBox.Width = _findTextBox.Width;
			_replaceWithTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Controls.Add(_replaceWithTextBox);

			_okButton = new Button();
			_okButton.Text = "&OK";
			Controls.Add(_okButton);

			_cancelButton = new Button();
			_cancelButton.Text = "&Cancel";
			Controls.Add(_cancelButton);

			AcceptButton = _okButton;
			CancelButton = _cancelButton;
		}


		// properties
		public string FindText
		{
			get
			{
				return _findTextBox.Text;
			}
			set
			{
				_findTextBox.Text = value;
			}
		}

		public string ReplaceWithText
		{
			get
			{
				return _replaceWithTextBox.Text;
			}
			set
			{
				_replaceWithTextBox.Text = value;
			}
		}


		// event handlers
		private void OkButton_Click(object sender, SelectEventArgs e)
		{
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void CancelButton_Click(object sender, KeyPressEventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Hide();
		}
	}
}

