using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	internal class SymbolsNavigator
	{
		static public void ExecuteFileNavigatorToolWindowCommand()
		{
			IList<IVsLibrary2> libraries = ObjectManagerHelper.GetLibraries();
			foreach(IVsLibrary2 library in libraries)
			{
				Debug.WriteLine("* library");

				IVsSimpleLibrary2 simpleLibrary = (IVsSimpleLibrary2) library;

				/*
				IVsLiteTreeList globalLibrariesList = ObjectManagerHelper.GetLibraryList(library, LIB_PERSISTTYPE.LPT_GLOBAL);
				if(globalLibrariesList != null)
				{ 
					Debug.WriteLine("\tglobal library list");
					int globalLibrariesListCount = ObjectManagerHelper.GetTreeListItemCount(globalLibrariesList);
					for(int i = 0; i < globalLibrariesListCount; ++i)
					{
						string text = ObjectManagerHelper.GetTreeListItemText(globalLibrariesList, i);
						Debug.WriteLine(string.Format("\t\t{0}", text));
					}
				}
				
				IVsLiteTreeList projectLibrariesList = ObjectManagerHelper.GetLibraryList(library, LIB_PERSISTTYPE.LPT_PROJECT);
				if(projectLibrariesList != null)
				{
					Debug.WriteLine("\tproject library list");

					int projectLibrariesListCount = ObjectManagerHelper.GetTreeListItemCount(globalLibrariesList);
					for(int i = 0; i < projectLibrariesListCount; ++i)
					{
						string text = ObjectManagerHelper.GetTreeListItemText(projectLibrariesList, i);
						Debug.WriteLine(string.Format("\t\t{0}", text));
					}
				}
				 */

				IVsObjectList2 list = ObjectManagerHelper.GetLibraryList(library);
				if(list != null)
				{
					Debug.WriteLine("\tlist");
					int count = ObjectManagerHelper.GetListItemCount(list);
					for(int i = 0; i < count; ++i)
					{
						string text = ObjectManagerHelper.GetListItemText(list, i);
						Debug.WriteLine(string.Format("\t\t{0}", text));
					}
				}
			}
		}
	}
}
