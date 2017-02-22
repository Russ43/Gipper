namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class ClassifierContext
	{
		#region properties
		public ClassifiedSpanInfo Info
		{
			get;
			private set;
		}

		public ClassifiedSpanInfo PreviousInfo
		{
			get;
			private set;
		}
		#endregion

		#region methods
		public void SetNextInfo(ClassifiedSpanInfo info)
		{
			PreviousInfo = Info;
			Info = info;
		}
		#endregion
	}
}
