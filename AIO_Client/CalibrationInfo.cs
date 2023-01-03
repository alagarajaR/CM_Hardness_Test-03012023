using System;

namespace AIO_Client
{

	[Serializable]
	public class CalibrationInfo
	{
		public int Index { get; set; }

		public string ZoomTime { get; set; }

		public string Force { get; set; }

		public string HardnessLevel { get; set; }

		public float XPixelLength { get; set; }

		public float YPixelLength { get; set; }

		public CalibrationInfo()
		{
		}

		public CalibrationInfo(int index, string zoomTime, string force, string hardnessLevel, float xPixelLength, float yPixelLength)
		{
			Index = index;
			ZoomTime = zoomTime;
			Force = force;
			HardnessLevel = hardnessLevel;
			XPixelLength = xPixelLength;
			YPixelLength = yPixelLength;
		}

		public CalibrationInfo Clone(int index)
		{
			CalibrationInfo calibrationInfo = new CalibrationInfo();
			calibrationInfo.Index = index;
			calibrationInfo.ZoomTime = ZoomTime;
			calibrationInfo.Force = Force;
			calibrationInfo.HardnessLevel = HardnessLevel;
			calibrationInfo.XPixelLength = XPixelLength;
			calibrationInfo.YPixelLength = YPixelLength;
			return calibrationInfo;
		}
	}
}
