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
	internal class FindForm : GipperForm
	{
		// fields
		private TextBox _findTextBox;
		private Button _okButton;
		private Button _cancelButton;


		// constructors
		public FindForm()
		{
			Text = "Gipper Find";
			Width = Screen.PrimaryScreen.WorkingArea.Width / 4;
			Height = (int) (Width / 1.61803398875);

			int margin = 10;

			_findTextBox = new TextBox();
			_findTextBox.Top = margin;
			_findTextBox.Left = margin;
			_findTextBox.Height = (int) (Font.Size * 2);
			_findTextBox.Width = ClientSize.Width - (2 * margin);
			_findTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Controls.Add(_findTextBox);

			_okButton = new Button();
			_okButton.Text = "&OK";
			_okButton.Height = (int) (Font.Size * 6);
			_okButton.Top = ClientSize.Height - margin - _okButton.Height;
			_okButton.Left = margin;
			_okButton.Width = ClientSize.Width / 4;
			_okButton.Click += new EventHandler(OkButton_Click);
			Controls.Add(_okButton);

			_cancelButton = new Button();
			_cancelButton.Text = "&Cancel";
			_cancelButton.Height = _okButton.Height;
			_cancelButton.Top = _okButton.Top;
			_cancelButton.Left = ClientSize.Width - margin - _okButton.Width;
			_cancelButton.Width = _okButton.Width;
			_cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			_cancelButton.Click += new EventHandler(CancelButton_Click);
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


		// event handlers
		private void OkButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Hide();
		}
	}
}

