using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Media;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	static internal class ObjectManagerHelper
	{
		// private constants
		private const int _enumBatchSize = 32;


		// methods
		static public IList<IVsLibrary2> GetLibraries()
		{
			IList<IVsLibrary2> libraries = new List<IVsLibrary2>();

			IVsObjectManager2 objectManager = GetObjectManager();

			IVsEnumLibraries2 vsEnumLibraries;
			ErrorHandler.Succeeded(objectManager.EnumLibraries(out vsEnumLibraries));

			IVsLibrary2[] array = new IVsLibrary2[_enumBatchSize];
			uint fetched;
			do
			{
				vsEnumLibraries.Next((uint) array.Length, array, out fetched);
				for(int i = 0; i < fetched; ++i)
					libraries.Add(array[i]);
			}
			while(fetched == array.Length);

			return libraries;
		}

		static public IVsLiteTreeList GetLibraryListStupidBrowseContainsers(IVsLibrary2 library, LIB_PERSISTTYPE persistType)
		{
			// TODO: What are browse containers? Do we care about this??
			IVsLiteTreeList list;
			ErrorHandler.Succeeded(library.GetLibList(LIB_PERSISTTYPE.LPT_GLOBAL, out  list));
			
			return list;
		}

		static public IVsObjectList2 GetLibraryList(IVsLibrary2 library)
		{
			IVsObjectList2 list2;
			ErrorHandler.Succeeded(
				library.GetList2(
					(uint) _LIB_LISTTYPE.LLT_CLASSES,
					(uint) _LIB_LISTFLAGS.LLF_USESEARCHFILTER,
					new VSOBSEARCHCRITERIA2[]
					{
						new VSOBSEARCHCRITERIA2()
						{
							eSrchType = VSOBSEARCHTYPE.SO_PRESTRING,
							grfOptions = (uint) _VSOBSEARCHOPTIONS.VSOBSO_CASESENSITIVE,
							szName = "*"
						}
					},
					out list2
				)
			);

			return list2;
		}

		static public int GetTreeListItemCount(IVsLiteTreeList treeList)
		{
			uint count;
			ErrorHandler.Succeeded(treeList.GetItemCount(out count));

			return (int)count;
		}

		static public string GetTreeListItemText(IVsLiteTreeList treeList, int itemIndex)
		{
			string text;
			ErrorHandler.Succeeded(treeList.GetText((uint) itemIndex, VSTREETEXTOPTIONS.TTO_DEFAULT, out text));
			
			return text;
		}

		static public int GetListItemCount(IVsObjectList2 list)
		{
			uint count;
			ErrorHandler.Succeeded(list.GetItemCount(out count));

			return (int) count;
		}

		static public string GetListItemText(IVsObjectList2 list, int itemIndex)
		{
			IVsSimpleObjectList2 simpleList = (IVsSimpleObjectList2) list;

			string text;
			ErrorHandler.Succeeded(
				simpleList.GetTextWithOwnership(
					(uint) itemIndex,
					VSTREETEXTOPTIONS.TTO_DEFAULT, 
					out text
				)
			);

			return text;
		}


		// private methods
		static private IVsObjectManager2 GetObjectManager()
		{
			return (IVsObjectManager2) GipperPackage.GetGlobalService(typeof(SVsObjectManager));
		}
	}
}
