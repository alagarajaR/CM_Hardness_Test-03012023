using System;

namespace AIO_Client
{

	[Serializable]
	public class DataMatrix
	{
		public float ReferencePointX { get; set; }

		public float ReferencePointY { get; set; }

		public float XInterval { get; set; }

		public float YInterval { get; set; }

		public int XNumberOfPoints { get; set; }

		public int YNumberOfPoints { get; set; }
	}
}
