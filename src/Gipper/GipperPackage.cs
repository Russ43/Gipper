using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.TextManager.Interop;

using EnvDTE;
using EnvDTE80;


namespace Gipper
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the 
	/// IVsPackage interface and uses the registration attributes defined in the framework to 
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	// This attribute is needed to let the shell know that this package exposes some menus.
	[ProvideMenuResource("Menus.ctmenu", 1)]
	// This attribute registers a tool window exposed by this package.
	[ProvideToolWindow(typeof(MyToolWindow))]
	[ProvideToolWindow(typeof(OpenFilesToolWindowPane))]
	[ProvideToolWindow(typeof(FileNavigatorToolWindowPane))]
	[Guid(GuidList.guidGipperPackagePkgString)]
	public sealed class GipperPackage : Package43
	{
		// static private fields
		static private GipperPackage _instance;


		/// <summary>
		/// Default constructor of the package.
		/// Inside this method you can place any initialization code that does not require 
		/// any Visual Studio service because at this point the package object is created but 
		/// not sited yet inside Visual Studio environment. The place to do all the other 
		/// initialization is the Initialize method.
		/// </summary>
		public GipperPackage()
		{
			Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));

			_instance = this;
		}

		/////////////////////////////////////////////////////////////////////////////
		// Overridden Package Implementation
		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
			base.Initialize();

			// register performance counters
			PerformanceHelper.CreatePerformanceCounters();

			// Initialize helper classes
			EventsHelper.Initialize(Dte);

			// Create window manager
			new WindowManager();

			// Add our commands
			AddCommand(PkgCmdIDList.cmdidGipperAddFile, new AddFileCommand());
			AddCommand(PkgCmdIDList.cmdidGipperVersionControl, new VersionControlCommand());
			AddCommand(PkgCmdIDList.cmdidGipperToolWindows, new ToolWindowsCommand());
			AddCommand(PkgCmdIDList.cmdidCommands, new CommandsCommand());
			AddCommand(PkgCmdIDList.cmdidShellToCurrentFileDirectory, new OpenShellCommand(OpenShellTarget.CurrentFileDirectory));
			AddCommand(PkgCmdIDList.cmdidShellToSolutionDirectory, new OpenShellCommand(OpenShellTarget.SolutionDirectory));
			AddCommand(PkgCmdIDList.cmdidExploreCurrentFileDirectory, new ExploreToCommand(OpenShellTarget.CurrentFileDirectory));
			AddCommand(PkgCmdIDList.cmdidExploreSolutionDirectory, new ExploreToCommand(OpenShellTarget.SolutionDirectory));
			AddCommand(PkgCmdIDList.cmdidFind, new FindCommand());
			AddCommand(PkgCmdIDList.cmdidFindInFiles, new FindInFilesCommand());
			AddCommand(PkgCmdIDList.cmdidReplace, new ReplaceCommand());
			AddCommand(PkgCmdIDList.cmdidReplaceInFiles, new ReplaceInFilesCommand());
			AddCommand(PkgCmdIDList.cmdidRunSolution, new RunSolutionCommand());
			AddCommand(PkgCmdIDList.cmdidShowClassifications, new ShowClassificationsCommand());
			AddCommand(PkgCmdIDList.cmdidTileFloatingToolWindows, new TileFloatingToolWindowsCommand());

			// Add our tool windows
			AddToolWindow(PkgCmdIDList.cmdidGipperOpenFilesToolWindow, typeof(OpenFilesToolWindowPane));
			AddToolWindow(PkgCmdIDList.cmdidGipperFileNavigatorWindow, typeof(FileNavigatorToolWindowPane));
		}
		#endregion


		// static fields
		static private ToolWindowManager _toolWindows;
		static private IComponentModel _componentModel;
		static private IVsTextManager _textManager;


		// static properties
		static public ToolWindowManager ToolWindows
		{
			get
			{
				if(_toolWindows == null)
					_toolWindows = new ToolWindowManager(_instance);

				return _toolWindows;
			}
		}

		static public IComponentModel ComponentModel
		{
			get
			{
				if(_componentModel == null)
					_componentModel = (IComponentModel) _instance.GetService(typeof(SComponentModel));

				return _componentModel;
			}
		}

		static public IVsTextManager TextManager
		{
			get
			{
				if(_textManager == null)
					_textManager = GipperPackage.GetGlobalService(typeof(VsTextManagerClass)) as IVsTextManager;

				return _textManager;
			}
		}

		static public IClassifier ClassifierAggregatorService
		{
			get;
			set;	// assignmed from GipperClassifier.ctor since it seems to require MEF Import
		}
	}
}

