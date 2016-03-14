using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using Company43.Common;


namespace Gipper
{
	 internal class BadInputForm : GipperForm
	{
		// fields
		private Label _errorMessageLabel;
		private TextBox _badInputTextBox;
		private Button _okButton;


		// constructors
		private BadInputForm()
		{
			FormBorderStyle = FormBorderStyle.FixedToolWindow;

			Text = "Bad Input";

			_errorMessageLabel = new Label();
			Controls.Add(_errorMessageLabel);

			_badInputTextBox = new TextBox();
			_badInputTextBox.Multiline = true;
			_badInputTextBox.ReadOnly = true;
			Controls.Add(_badInputTextBox);

			_okButton = new Button();
			_okButton.Text = "&OK";
			_okButton.Click += OkButton_Click;
			Controls.Add(_okButton);

			AcceptButton = _okButton;
		}


		// control methods
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			int border = 10;
			int buttonHeight = 100;

			_errorMessageLabel.Top = border;
			_errorMessageLabel.Left = border;
			_errorMessageLabel.Height = ClientSize.Height / 2 - (5 * border) - buttonHeight;
			_errorMessageLabel.Width = ClientSize.Width - (2 * border);

			_badInputTextBox.Top = _errorMessageLabel.Bottom + border;
			_badInputTextBox.Left = _errorMessageLabel.Left;
			_badInputTextBox.Height = ClientSize.Height - _errorMessageLabel.Height - (5 * border) - buttonHeight;
			_badInputTextBox.Width = _errorMessageLabel.Width;

			_okButton.Top = _badInputTextBox.Bottom + border;
			_okButton.Height = buttonHeight;
			_okButton.Width = Math.Min(300, _errorMessageLabel.Width);
			_okButton.Left = _errorMessageLabel.Right - _okButton.Width;

			_errorMessageLabel.Text = Exception.ErrorMessage;
			_badInputTextBox.Text = Exception.BadInput;

			// This prevents the bad input text box from taking focus and selecting all its text.
			// BUGBUG: Well, it should. But it doesn't
			_okButton.Focus();
		}


		// properties
		public BadInputException Exception
		{
			get;
			set;
		}


		// static methods
		static public void ShowBadInputException(BadInputException exception)
		{
			BadInputForm form = new BadInputForm();
			form.Exception = exception;
			form.ShowDialog();
		}


		// event handlers
		private void OkButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
