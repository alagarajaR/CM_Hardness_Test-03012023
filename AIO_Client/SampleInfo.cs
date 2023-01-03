using System;

namespace AIO_Client
{

	[Serializable]
	public class SampleInfo
	{
		public string SampleName { get; set; }

		public string SampleSn { get; set; }

		public double HardnessL { get; set; }

		public double HardnessH { get; set; }

		public string InspectionUnit { get; set; }

		public DateTime InspectionDate { get; set; }

		public string Tester { get; set; }

		public string Reviewer { get; set; }
	}
}