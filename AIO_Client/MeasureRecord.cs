using System;

namespace AIO_Client
{

	public class MeasureRecord
	{
		public int Index { get; set; }

		public float XPos { get; set; }

		public float YPos { get; set; }

		public string ButtonText { get; set; }

		public double Hardness { get; set; }

		public string HardnessType { get; set; }

		public string Qualified { get; set; }

		public double D1 { get; set; }

		public double D2 { get; set; }

		public double DAvg { get; set; }

		public string ConvertType { get; set; }

		public double ConvertValue { get; set; }

		public float Depth { get; set; }

		public DateTime MeasureTime { get; set; }

		public string OriginalImagePath { get; set; }

		public string MeasuredImagePath { get; set; }
	}
}