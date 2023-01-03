using System;

namespace AIO_Client
{

	[Serializable]
	public class DataSlash
	{
		public float ReferencePoint1X { get; set; }

		public float ReferencePoint1Y { get; set; }

		public float ReferencePoint2X { get; set; }

		public float ReferencePoint2Y { get; set; }

		public float ReferencePoint3X { get; set; }

		public float ReferencePoint3Y { get; set; }

		public float FirstOffset { get; set; }

		public float Angle { get; set; }

		public float Interval { get; set; }

		public float Offset { get; set; }

		public int NumberOfPoints { get; set; }
	}
}
