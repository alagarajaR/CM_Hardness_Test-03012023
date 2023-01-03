using System;
using Labtt.DrawArea;

namespace AIO_Client
{

	[Serializable]
	public class DataPatternFree
	{
		public int Index { get; set; }

		public float XPos { get; set; }

		public float YPos { get; set; }

		public GraphicsSpot GraphicsSpot { get; set; }
	}
}
