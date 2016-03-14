using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Company43.Common;


namespace Gipper
{
	internal class ShellCommand : IKeyed
	{
		// events
		public event EventHandler Completed;
		public event EventHandler OutputReceived;


		// fields
		private Process _process;


		// constructors
		public ShellCommand(string title, Keys key, string command, string arguments, string workingDirectory)
		{
			Title = title;
			Key = key;
			Command = command;
			Arguments = arguments;
			WorkingDirectory = workingDirectory;
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

		public string Command
		{
			get;
			set;
		}

		public string Arguments
		{
			get;
			set;
		}

		public string WorkingDirectory
		{
			get;
			set;
		}

		public string Output
		{
			get;
			private set;
		}

		public int ExitCode
		{
			get;
			private set;
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
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.WorkingDirectory = WorkingDirectory;
			startInfo.FileName = Command;
			startInfo.Arguments = Arguments;
			startInfo.UseShellExecute = false;	// required for hidden window and stdout/err to work
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;

			_process = new Process();
			_process.StartInfo = startInfo;
			_process.EnableRaisingEvents = true;
			_process.Exited += Process_Exited;
			_process.OutputDataReceived += Process_OutputDataReceived;
			_process.ErrorDataReceived += Process_ErrorDataReceived;
			Output = string.Empty;
			ExitCode = -1;
			_process.Start();
			_process.BeginOutputReadLine();
			_process.BeginErrorReadLine();
		}


		// event handlers
		private void Process_Exited(object sender, EventArgs e)
		{
			ExitCode = _process.ExitCode;

			EventHandler completed = Completed;
			if(completed != null)
				completed(this, EventArgs.Empty);
		}
		
		private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			Output += e.Data;

			EventHandler outputReceived = OutputReceived;
			if(outputReceived != null)
				outputReceived(this, EventArgs.Empty);
		}

		private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			Output += e.Data;

			EventHandler outputReceived = OutputReceived;
			if(outputReceived != null)
				outputReceived(this, EventArgs.Empty);
		}
	}
}
