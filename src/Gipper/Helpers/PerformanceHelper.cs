using System;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Gipper
{
	static internal class PerformanceHelper
	{
		// fields
		static public PerformanceCounter _projectFileManagerRepopulateTimePerformanceCounter;
		static public PerformanceCounter _projectFileManagerRepopulateTimeBasePerformanceCounter;


		// methods
		static public void CreatePerformanceCounters()
		{
			if(!PerformanceCounterCategory.Exists("Gipper"))
			{
				CounterCreationDataCollection collection = new CounterCreationDataCollection();

				// ProjectFileManager counters
				CounterCreationData projectFileManagerRepopulateTime = new CounterCreationData(
					"Project File Manager Repopulate Time",
					"The time spent repopulating the Project File Manager's list of files.",
					PerformanceCounterType.AverageTimer32
				);
				collection.Add(projectFileManagerRepopulateTime);

				CounterCreationData projectFileManagerRepopulateTimeBase = new CounterCreationData(
					"Project File Manager Repopluate Time Base", 
					"The number of times the Project File Manager has repopulated its list of files.",
					PerformanceCounterType.AverageBase
				);
				collection.Add(projectFileManagerRepopulateTimeBase);

				PerformanceCounterCategory.Create(
					"Gipper",
					"Gipper perfomance counters.",
					PerformanceCounterCategoryType.SingleInstance,
					collection
				);
			}

			_projectFileManagerRepopulateTimePerformanceCounter = new PerformanceCounter(
				"Gipper",
				"Project File Manager Repopulate Time",
				false
			);
			_projectFileManagerRepopulateTimeBasePerformanceCounter = new PerformanceCounter(
				"Gipper",
				"Project File Manager Repopluate Time Base",
				false
			);

			_projectFileManagerRepopulateTimePerformanceCounter.RawValue = 0;
			_projectFileManagerRepopulateTimeBasePerformanceCounter.RawValue = 0;
		}

		static public void IncrementProjectFileManagerRepopulateTimeCounter(TimeSpan time)
		{
			_projectFileManagerRepopulateTimePerformanceCounter.IncrementBy(time.Ticks);
			_projectFileManagerRepopulateTimeBasePerformanceCounter.Increment();
		}
	}
}

