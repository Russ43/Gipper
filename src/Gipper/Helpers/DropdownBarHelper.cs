using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;


namespace Gipper
{
	static internal class DropdownBarHelper
	{
		// methods
		static public IVsDropdownBar GetActiveDropdownBar()
		{
			GipperPackage p = new GipperPackage();

			DTE2 dte = GipperPackage.Dte;
			//Document document = dte.ActiveDocument;
			Window window = dte.ActiveWindow;

			IVsTextManager vsTextManager = GipperPackage.GetGlobalService(typeof(VsTextManagerClass)) as IVsTextManager;
			//IVsEditorAdaptersFactoryService vsEditorAdaptersFactoryService = GipperPackagePackage.GetGlobalService(typeof(IVsEditorAdaptersFactoryService)) as IVsEditorAdaptersFactoryService;

			//var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
			//var editorAdapterFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

			IComponentModel componentModel = Package.GetGlobalService(typeof(SComponentModel)) as IComponentModel;
			IVsEditorAdaptersFactoryService editorFactory = componentModel.GetService<IVsEditorAdaptersFactoryService>();
			IWpfTextView activeTextView = TryGetActiveTextView(vsTextManager, editorFactory).Item2;


			Microsoft.VisualStudio.TextManager.Interop.IVsTextViewEx viewAdapter = (Microsoft.VisualStudio.TextManager.Interop.IVsTextViewEx) editorFactory.GetViewAdapter(activeTextView);

			object frameObject;
			var hr = viewAdapter.GetWindowFrame(out frameObject);
			Microsoft.VisualStudio.Shell.Interop.IVsWindowFrame frame = (Microsoft.VisualStudio.Shell.Interop.IVsWindowFrame) frameObject;

			var iid = typeof(IVsCodeWindow).GUID;
			var ptr = IntPtr.Zero;
			hr = frame.QueryViewInterface(ref iid, out ptr);
			IVsCodeWindow codeWindow = (IVsCodeWindow) System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(ptr);

			IVsDropdownBarManager dropdown = (IVsDropdownBarManager) codeWindow;

			IVsDropdownBar dropdownBar;
			hr = dropdown.GetDropdownBar(out dropdownBar);

			return dropdownBar;
		}

		static public IVsDropdownBarClient GetDropdownBarClient(IVsDropdownBar dropdownBar)
		{
			IVsDropdownBarClient client;
			int hr = dropdownBar.GetClient(out client);
			ErrorHandler.ThrowOnFailure(hr);

			return client;
		}

		static public IList<GipperDropdownBarEntry> GetDropdownBarEntries(IVsDropdownBar dropdownBar)
		{
			IList<GipperDropdownBarEntry> entries = new List<GipperDropdownBarEntry>();

			IVsDropdownBarClient client = GetDropdownBarClient(dropdownBar);
			
			for(int comboIndex = 0; comboIndex < 3; ++comboIndex)
			{
				// HACKHACK: For some reason, we don't always get the full list of members unless we call this first.
				// Without this, we often get just one member--the currently selected one. At least this is the case in
				// Visual Studio 2012. Not sure about 2013.
				int hr = client.OnComboGetFocus(comboIndex);
				ErrorHandler.ThrowOnFailure(hr);

				uint pcEntries;
				uint puEntryType;
				IntPtr phImageList;
				hr = client.GetComboAttributes(comboIndex, out pcEntries, out puEntryType, out phImageList);
				ErrorHandler.ThrowOnFailure(hr);

				for(int index = 0; index < pcEntries; ++index)
				{
					string ppszText = HackHackGetEntryText(client, comboIndex, index);
					ErrorHandler.ThrowOnFailure(hr);

					int pImageIndex;
					hr = client.GetEntryImage(comboIndex, index, out pImageIndex);
					ErrorHandler.ThrowOnFailure(hr);

					uint pAttr;
					hr = client.GetEntryAttributes(comboIndex, index, out pAttr);
					ErrorHandler.ThrowOnFailure(hr);

					GipperDropdownBarEntry entry = new GipperDropdownBarEntry()
					{
						ComboIndex = comboIndex,
						Index = index,
						Text = ppszText.ToString(),
						Image = pImageIndex,
						Attributes = (DROPDOWNFONTATTR) pAttr
					};
					entries.Add(entry);
				}
			}

			return entries;
		}

		static public string HackHackGetEntryText(IVsDropdownBarClient client, int iCombo, int iIndex)
		{
			TextManagerInternal.IVsDropdownBarClient hackHackClient = (TextManagerInternal.IVsDropdownBarClient) client;

			string szText = null;
			IntPtr ppszText = IntPtr.Zero;

			try
			{
				ppszText = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(IntPtr)));
				if(ppszText == IntPtr.Zero)
					throw new Exception("Unable to allocate memory for IVsDropDownBarClient.GetTextEntry string marshalling.");

				hackHackClient.GetEntryText(iCombo, iIndex, ppszText);

				IntPtr pszText = Marshal.ReadIntPtr(ppszText);

				szText = Marshal.PtrToStringUni(pszText);
			}
			finally
			{
				if(ppszText != IntPtr.Zero)
					Marshal.FreeCoTaskMem(ppszText);
			}

			return szText;
		}


		// JaredPar VIM methods from
		// https://github.com/jaredpar/VsVim/blob/3b7fd4712385a2abf00ba01c6c44639211b3af9c/VsVimShared/Extensions.cs
		public static Tuple<bool, IWpfTextView> TryGetActiveTextView(IVsTextManager vsTextManager, IVsEditorAdaptersFactoryService factoryService)
		{
			IVsTextView vsTextView;
			IWpfTextView textView = null;
			if(ErrorHandler.Succeeded(vsTextManager.GetActiveView(0, null, out vsTextView)) && vsTextView != null)
			{
				textView = factoryService.GetWpfTextView(vsTextView);
			}

			return Tuple.Create(textView != null, textView);
		}
	}
}
