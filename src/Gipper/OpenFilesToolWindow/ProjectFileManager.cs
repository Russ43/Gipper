using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;

using Company43.Common;


namespace Gipper
{
	internal class ProjectFileManager
	{
		// events
		public event EventHandler RepopulateProgressChanged;


		// fields
		private DirectoryInfo _projectRoot;
		private Func<FileInfo, bool> _includeFileFunc;
		private Trie<ProjectFile> _trie;
		private FileSystemWatcher _watcher;
		BackgroundWorker _repopulateFromRootBackgroundWorker;
		private double _repopulateProgress;


		// constructors
		public ProjectFileManager(string rootPath, Func<FileInfo, bool> includeFileFunc, Trie<ProjectFile> trie)
		{
			_projectRoot = new DirectoryInfo(rootPath);
			_includeFileFunc = includeFileFunc;
			_trie = trie;

			_watcher = new FileSystemWatcher(rootPath);
			_watcher.Created += Watcher_Created;

			FileSystemEventHandler deletedHandler = new FileSystemEventHandler(Watcher_Deleted);			
			EventHandler<FileSystemEventArgs> genericDeletedHandler = (sender, e) => { deletedHandler(sender, e); };
			EventHandler<FileSystemEventArgs> throttledGenericDeletedHandler = new ThrottledEventHandler<FileSystemEventArgs>(genericDeletedHandler, TimeSpan.FromSeconds(15));
			FileSystemEventHandler throttledDeletedHandler = (sender, e) => { throttledGenericDeletedHandler(sender, e); };
			_watcher.Deleted += throttledDeletedHandler;

			RenamedEventHandler renamedHandler = new RenamedEventHandler(Watcher_Renamed);
			EventHandler<RenamedEventArgs> genericRenamedHandler = (sender, e) => { renamedHandler(sender, e); };
			EventHandler<RenamedEventArgs> throttledGenericRenamedHandler = new ThrottledEventHandler<RenamedEventArgs>(genericRenamedHandler, TimeSpan.FromSeconds(15));
			RenamedEventHandler throttledRenamedHandler = (sender, e) => { throttledGenericRenamedHandler(sender, e); };
			_watcher.Renamed += throttledRenamedHandler;

			ErrorEventHandler errorHandler = new ErrorEventHandler(Watcher_Error);
			EventHandler<ErrorEventArgs> genericErrorHandler = (sender, e) => { errorHandler(sender, e); };
			EventHandler<ErrorEventArgs> throttledGenericErrorHandler = new ThrottledEventHandler<ErrorEventArgs>(genericErrorHandler, TimeSpan.FromSeconds(60));
			ErrorEventHandler throttledErrorHandler = (sender, e) => { throttledGenericErrorHandler(sender, e); };
			_watcher.Error += throttledErrorHandler;

			_watcher.IncludeSubdirectories = true;
			_watcher.EnableRaisingEvents = true;

			_repopulateFromRootBackgroundWorker = new BackgroundWorker();
			_repopulateFromRootBackgroundWorker.WorkerSupportsCancellation = true;
			_repopulateFromRootBackgroundWorker.DoWork += RepopulateFromRootWorker_DoWork;

			RepopulateFromRoot(_projectRoot);
		}


		// finalizers
		~ProjectFileManager()
		{
			if(_watcher != null)
				_watcher.Dispose();
		}


		// properties
		public double RepopulateProgress
		{
			get
			{
				return _repopulateProgress;
			}
			private set
			{
				bool wholeNumberPercentileChanged = (Math.Round(_repopulateProgress, 2) != Math.Round(value, 2));
				
				_repopulateProgress = value;
				
				if(wholeNumberPercentileChanged)
				{
					// raise the RepopulateProgressChanged event only when the "whole number percentile" changes
					EventHandler repopulateProgressChanged = RepopulateProgressChanged;
					if(repopulateProgressChanged != null)
						repopulateProgressChanged(this, EventArgs.Empty);
				}
			}
		}


		// private methods
		private void RepopulateFromRoot(DirectoryInfo directory)
		{
			// This is rather time-consuming for large directories, so delegate some of the work to a background thread.

			// cancel any pending work
			while(_repopulateFromRootBackgroundWorker.IsBusy)
			{
				_repopulateFromRootBackgroundWorker.CancelAsync();
				Thread.Sleep(TimeSpan.FromSeconds(0.5));
			}

			// start repopulating on a background thread
			_repopulateFromRootBackgroundWorker.RunWorkerAsync();
		}

		private void AddFile(FileInfo file)
		{
			if(_includeFileFunc(file))
			{
				ProjectFile projectFile = new ProjectFile(file, _projectRoot);
				_trie.AddItem(projectFile);
			}
		}


		// event handlers
		private void Watcher_Created(object sender, FileSystemEventArgs e)
		{
			FileInfo file = new FileInfo(e.FullPath);
			if(file.Exists)
			{
				// add the file to our observable collection if indeed it's a file (could also be a new directory)
				AddFile(file);
			}
		}

		private void Watcher_Deleted(object sender, FileSystemEventArgs e)
		{
			Helper.TrapExceptions(
				() => RepopulateFromRoot(_projectRoot),
				"ProjectFileManager - File deleted"
			);
		}

		private void Watcher_Renamed(object sender, FileSystemEventArgs e)
		{
			Helper.TrapExceptions(
				() => RepopulateFromRoot(_projectRoot),
				"ProjectFileManager - File renamed"
			);
		}

		private void Watcher_Error(object sender, ErrorEventArgs e)
		{
			Helper.TrapExceptions(
				() => RepopulateFromRoot(_projectRoot),
				"ProjectFileManager - Watcher Error"
			);
		}

		private void RepopulateFromRootWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			// on the background thread, recursively add each file to the local projectFiles list

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			_trie.Clear();
			IList<FileInfo> files = _projectRoot.GetFiles("*", SearchOption.AllDirectories);
			RepopulateProgress = 0;
			for(int i = 0; i < files.Count; ++i)
			{
				FileInfo file = files[i];
				if(!_repopulateFromRootBackgroundWorker.CancellationPending)
					AddFile(file);

				RepopulateProgress = ((double) i / (double) files.Count);
			}
			RepopulateProgress = 1;

			stopwatch.Stop();

			if(!_repopulateFromRootBackgroundWorker.CancellationPending)
			{
				// For now, let's monitor this using PerfCounters. I'm a little worried this is getting called too often and I know
				// it's too time consuming.
				PerformanceHelper.IncrementProjectFileManagerRepopulateTimeCounter(stopwatch.Elapsed);
			}
		}
	}
}

