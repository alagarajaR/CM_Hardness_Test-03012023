using System;

namespace AIO_Client
{

	[Serializable]
	public class CameraInfo
	{
		public bool ShowCameraState { get; set; }

		public bool ShowFrameRate { get; set; }

		public bool SKIP2InCollect { get; set; }

		public float AnalogGain { get; set; }

		public double ExposureTime { get; set; }
	}
}
