using System.Windows.Forms;

namespace AIO_Client
{

	public static class CommonData
	{
		public const string IDENTIFIER_MEASURE_GRAPHICS = "MEASURE_GRAPHICS";

		public const string MULTIPOINTS_MODE_TASK_NAME = "MULTIPOINTS_MODE_TASK_NAME";

		public const string AUTOFOCE_TASK_NAME = "AUTO_FOCUS_TASK_NAME";

		public static string CurrentMeasuredImageFilepath = null;

		public static string MeasuredImageDirectoryPath = Application.StartupPath + "\\MeasuredImages";

		public static string CurrentOriginalImageFilepath = null;

		public static string OriginalImageDirectoryPath = Application.StartupPath + "\\OriginalImages";

		public static string HVCalibrationFilepath = Application.StartupPath + "\\Config\\HVCalibration_Config.xml";

		public static string HKCalibrationFilepath = Application.StartupPath + "\\Config\\HKCalibration_Config.xml";

		public static string HBWCalibrationFilepath = Application.StartupPath + "\\Config\\HBWCalibration_Config.xml";

		public static string TrimMeasureConfigFilepath = Application.StartupPath + "\\Config\\TrimMeasure_Config.xml";

		public static string HardnessConvertTableFilepath = Application.StartupPath + "\\Hardness_Convert_Table.csv";

		public static string HardnessDeepTempFilepath = Application.StartupPath + "\\Deep_Temp.bmp";

		public static string LoginInfoFilepath = Application.StartupPath + "\\srv\\li.data";
	}
}
