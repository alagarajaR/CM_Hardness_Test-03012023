using System;

namespace AIO_Client
{

	[Serializable]
	public class DataPatternHorizontal
	{
		public float ReferencePointX { get; set; }

		public float ReferencePointY { get; set; }

		public float Interval { get; set; }

		public float Offset { get; set; }

		public float FirstOffset { get; set; }

		public int NumberOfPoints { get; set; }
	}
}