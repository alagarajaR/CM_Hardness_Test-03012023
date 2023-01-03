using System;

namespace AIO_Client
{

	[Serializable]
	public class SavedData
	{
		public string Force { get; set; }

		public string ZoomTime { get; set; }

		public string ConvertType { get; set; }

		public string HardnessLevel { get; set; }

		public int LoadTime { get; set; }

		public int Light { get; set; }

		public float DeepHardness { get; set; }
	}
}
