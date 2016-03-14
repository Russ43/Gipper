using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Company43.Common;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	internal class DteCommand : IKeyed
	{
		// constructors
		public DteCommand(string title, Keys key, string commandName, string commandArgs)
		{
			Title = title;
			Key = key;
			CommandName = commandName;
			CommandArgs = commandArgs;
		}


		// properties
		public string Title
		{
			get;
			set;
		}

		public Keys Key
		{
			get;
			set;
		}

		public string CommandName
		{
			get;
			set;
		}

		public string CommandArgs
		{
			get;
			set;
		}


		// IKeyed methods
		public Keys GetKey()
		{
			return Key;
		}

		public string GetText()
		{
			return Title;
		}


		// methods
		public void Execute()
		{
			DTE2 dte = GipperPackage.Dte;

			try
			{
				dte.ExecuteCommand(CommandName, CommandArgs);
			}
			catch(System.Runtime.InteropServices.COMException exception)
			{
				Helper.ShowErrorDialog("Gipper Package", "Cannot execute command. " + exception.Message);
			}
		}
	}
}
