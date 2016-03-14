using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Gipper
{
	internal class ProjectFile
	{
		// fields
		private DirectoryInfo _rootDirectory;


		// constructors
		public ProjectFile(FileInfo file, DirectoryInfo rootDirectory)
		{
			FileInfo = file;
			_rootDirectory = rootDirectory;
		}


		// properties
		public FileInfo FileInfo
		{
			get;
			private set;
		}

		// object methods
		public override string ToString()
		{
			string directory = FileInfo.Directory.FullName;
			if(directory == _rootDirectory.FullName)
				directory = "";
			else if(directory.StartsWith(_rootDirectory.FullName))
				directory = " (" + directory.Substring(_rootDirectory.FullName.Length + 1) + ")";
			else
				directory = " (" + directory + ")";

			return string.Format("{0}{1}", FileInfo.Name, directory);
		}
	}
}
