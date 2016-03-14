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
	internal class ReplaceForm : GipperForm
	{
		// events
		public event EventHandler FindClicked;
		public event EventHandler ReplaceClicked;
		public event EventHandler SkipClicked;
		public event EventHandler ReplaceAllClicked;


		// fields
		private TextBox _findTextBox;
		private TextBox _replaceWithTextBox;
		private Button _findOrReplaceButton;
		private Button _skipButton;
		private Button _replaceAllButton;
		private Button _cancelButton;


		// constructors
		public ReplaceForm()
		{
			Text = "Gipper Replace";
			Width = Screen.PrimaryScreen.WorkingArea.Width / 4;
			Height = (int) (Width / 1.61803398875);

			int margin = 10;

			Label findLabel = new Label();
			findLabel.Text = "Find:";
			findLabel.Top = margin;
			findLabel.Left = margin;
			findLabel.Height = (int) (Font.Size * 7);
			findLabel.Width = (int) (0.25 * (ClientSize.Width - (3 * margin)));
			findLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			Controls.Add(findLabel);

			Label replaceWithLabel = new Label();
			replaceWithLabel.Text = "Replace With:";
			replaceWithLabel.Top = findLabel.Bottom + margin;
			replaceWithLabel.Left = findLabel.Left;
			replaceWithLabel.Height = findLabel.Height;
			replaceWithLabel.Width = findLabel.Width;
			replaceWithLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			Controls.Add(replaceWithLabel);

			_findTextBox = new TextBox();
			_findTextBox.Top = findLabel.Top;
			_findTextBox.Left = findLabel.Right + margin;
			_findTextBox.Height = findLabel.Height;
			_findTextBox.Width = ClientSize.Width - (3 * margin) - findLabel.Width;
			_findTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Controls.Add(_findTextBox);

			_replaceWithTextBox = new TextBox();
			_replaceWithTextBox.Top = replaceWithLabel.Top;	// label seem to be tall than text boxes (maybe single-line text boxes ignore height??)
			_replaceWithTextBox.Left = _findTextBox.Left;
			_replaceWithTextBox.Height = _findTextBox.Height;
			_replaceWithTextBox.Width = _findTextBox.Width;
			_replaceWithTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Controls.Add(_replaceWithTextBox);

			_findOrReplaceButton = new Button();
			_findOrReplaceButton.Text = "&Find";
			_findOrReplaceButton.Height = (int) (Font.Size * 7);
			_findOrReplaceButton.Top = ClientSize.Height - margin - _findOrReplaceButton.Height;
			_findOrReplaceButton.Left = margin;
			_findOrReplaceButton.Width = (ClientSize.Width - (5 * margin)) / 4;
			_findOrReplaceButton.Click += new EventHandler(FindOrReplaceButton_Clicked);
			Controls.Add(_findOrReplaceButton);

			_skipButton = new Button();
			_skipButton.Text = "&Skip";
			_skipButton.Height = _findOrReplaceButton.Height;
			_skipButton.Top = _findOrReplaceButton.Top;
			_skipButton.Left = _findOrReplaceButton.Right + margin;
			_skipButton.Width = _findOrReplaceButton.Width;
			_skipButton.Click +=SkipButton_Click;
			Controls.Add(_skipButton);

			_replaceAllButton = new Button();
			_replaceAllButton.Text = "Replace &All";
			_replaceAllButton.Height = _findOrReplaceButton.Height;
			_replaceAllButton.Top = _findOrReplaceButton.Top;
			_replaceAllButton.Left = _skipButton.Right + margin;
			_replaceAllButton.Width = _findOrReplaceButton.Width;
			_replaceAllButton.Click += ReplaceAllButton_Click;
			Controls.Add(_replaceAllButton);

			_cancelButton = new Button();
			_cancelButton.Text = "&Cancel";
			_cancelButton.Height = _findOrReplaceButton.Height;
			_cancelButton.Top = _findOrReplaceButton.Top;
			_cancelButton.Left = _replaceAllButton.Right + margin;
			_cancelButton.Width = _findOrReplaceButton.Width;
			_cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			_cancelButton.Click += new EventHandler(CancelButton_Click);
			Controls.Add(_cancelButton);

			AcceptButton = _findOrReplaceButton;
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

		public bool IsReadyToReplace
		{
			get
			{
				return (_findOrReplaceButton.Text == "&Find");
			}
			set
			{
				_findTextBox.Enabled = !value;
				_replaceWithTextBox.Enabled = !value;
				_findOrReplaceButton.Text = value ? "&Replace" : "&Find";
				_skipButton.Enabled = value;
				_replaceAllButton.Enabled = value;
			}
		}


		// methods
		public void FocusFindTextBox()
		{
			_findTextBox.SelectAll();
			ActiveControl = _findTextBox;
		}

		public void FocusReplaceButton()
		{
			_findOrReplaceButton.Focus();
		}


		// event handlers
		private void FindOrReplaceButton_Clicked(object sender, EventArgs e)
		{
			if(IsReadyToReplace)
			{
				EventHandler findClicked = FindClicked;
				if(findClicked != null)
					findClicked(this, EventArgs.Empty);
			}
			else
			{
				EventHandler replaceClicked = ReplaceClicked;
				if(replaceClicked != null)
					replaceClicked(this, EventArgs.Empty);
			}
		}

		private void SkipButton_Click(object sender, EventArgs e)
		{
 			EventHandler handler = SkipClicked;
			if(handler != null)
				handler(this, EventArgs.Empty);
		}

		private void ReplaceAllButton_Click(object sender, EventArgs e)
		{
			EventHandler handler = ReplaceAllClicked;
			if(handler != null)
				handler(this, EventArgs.Empty);
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Hide();
		}
	}
}

