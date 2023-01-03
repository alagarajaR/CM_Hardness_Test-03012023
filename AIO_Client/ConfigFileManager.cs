using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using Labtt.Communication;
using Labtt.Communication.MasterControl;
using Labtt.Communication.SYJKPlatform;
using Labtt.Communication.ZAxis;
using Labtt.Data;

namespace AIO_Client
{

	public static class ConfigFileManager
	{
		public const string LanguageFilepath = "Config\\Language_Config.xml";

		public const string GenericInfoFilepath = "Config\\Generic_Config.xml";

		private const string SerialPortConfigFilepath = "Config\\SerialPort_Config.xml";

		public const string HVHardnessTesterFilepath = "Config\\HVHardnessTester_Config.xml";

		public const string HBWHardnessTesterFilepath = "Config\\HBWHardnessTester_Config.xml";

		public const string SYJKPlatformConfigFilepath = "Config\\SYJKPlatform_Config.xml";

		public const string ZAxisConfigFilepath = "Config\\ZAxis_Config.xml";

		public const string CalibrationConfigFilepath = "Config\\Calibration_Config.xml";

		public const string HKCalibrationConfigFilepath = "Config\\HKCalibration_Config.xml";

		public const string CameraConfigFilepath = "Config\\Camera_Config.xml";

		public const string SampleInfoFilepath = "Config\\SampleInfo_Config.xml";

		public const string PanelDateFilepath = "Config\\PanelData_Config.xml";

		public const string AutoMeasureConfigFilepath = "Config\\AutoMeasure_Config.xml";

		public static GenericInfo GenericInfo { get; set; }

		static ConfigFileManager()
		{
			GenericInfo = LoadGenericInfoConfigFile();
		}

		public static TextResources LoadTextResourcesConfig(string filepath = "Config\\Language_Config.xml")
		{
			return (TextResources)FileOperator.DeserializeFromXMLFile(filepath, typeof(TextResources));
		}

		public static void SaveTextResourcesConfig(TextResources textResources, string filepath = "Config\\Language_Config.xml")
		{
			FileOperator.SerializeToXMLFile(textResources, filepath);
		}

		public static GenericInfo LoadGenericInfoConfigFile(string filepath = "Config\\Generic_Config.xml")
		{
			GenericInfo genericInfo = (GenericInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(GenericInfo));
			if (genericInfo == null && filepath == "Config\\Generic_Config.xml")
			{
				genericInfo = new GenericInfo();
				genericInfo.SoftwareSeries = SoftwareSeries.HV;
				genericInfo.SoftwareVersion = SoftwareVersion.SE;
				genericInfo.MicrometerOn = false;
				genericInfo.TurretOn = false;
				genericInfo.IsEncryptedBySecurityDog = false;
				genericInfo.CurrentLanguageName = "中文";
				genericInfo.LanguageInfoList = new List<LanguageInfo>();
				LanguageInfo languageInfo = new LanguageInfo();
				languageInfo.LanguageName = "中文";
				languageInfo.ResourcesFilepath = "Config\\Language_Config_CN.xml";
				languageInfo.SimpleReportTemplateFilepath = "Template\\SimpleReportTemplate.doc";
				languageInfo.ReportWithImageTemplateFilepath = "Template\\ReportWithImageTemplate.doc";
				languageInfo.ReportWithDeepHardnessTemplateFilepath = "Template\\ReportWithDeepHardnessTemplate.doc";
				languageInfo.FullReportTemplateFilepath = "Template\\FullReportTemplate.doc";
				genericInfo.LanguageInfoList.Add(languageInfo);
				FileOperator.SerializeToXMLFile(genericInfo, filepath);
			}
			return genericInfo;
		}

		public static void SaveGenericInfoConfigFile(GenericInfo genericInfo, string filepath = "Config\\Generic_Config.xml")
		{
			FileOperator.SerializeToXMLFile(genericInfo, filepath);
		}

		public static List<SerialPortInfo> LoadSerialPortConfigFile(string filepath = "Config\\SerialPort_Config.xml")
		{
			List<SerialPortInfo> portInfoList = (List<SerialPortInfo>)FileOperator.DeserializeFromXMLFile(filepath, typeof(List<SerialPortInfo>));
			if (portInfoList == null && filepath == "Config\\SerialPort_Config.xml")
			{
				portInfoList = new List<SerialPortInfo>();
				SerialPortInfo mainPortInfo = new SerialPortInfo();
				mainPortInfo.Identifier = "Main";
				mainPortInfo.PortName = "COM1";
				mainPortInfo.BaudRate = 9600;
				mainPortInfo.DataBits = 8;
				mainPortInfo.Parity = Parity.None;
				mainPortInfo.StopBits = StopBits.One;
				SerialPortInfo platformPortInfo = new SerialPortInfo();
				platformPortInfo.Identifier = "X/Y";
				platformPortInfo.PortName = "COM2";
				platformPortInfo.BaudRate = 57600;
				platformPortInfo.DataBits = 8;
				platformPortInfo.Parity = Parity.None;
				platformPortInfo.StopBits = StopBits.One;
				SerialPortInfo zAxisPortInfo = new SerialPortInfo();
				zAxisPortInfo.Identifier = "Z";
				zAxisPortInfo.PortName = "COM3";
				zAxisPortInfo.BaudRate = 57600;
				zAxisPortInfo.DataBits = 8;
				zAxisPortInfo.Parity = Parity.None;
				zAxisPortInfo.StopBits = StopBits.One;
				SerialPortInfo micrometerPortInfo = new SerialPortInfo();
				micrometerPortInfo.Identifier = "Micrometer";
				micrometerPortInfo.PortName = "COM4";
				micrometerPortInfo.BaudRate = 2400;
				micrometerPortInfo.DataBits = 8;
				micrometerPortInfo.Parity = Parity.None;
				micrometerPortInfo.StopBits = StopBits.One;
				portInfoList.Add(platformPortInfo);
				portInfoList.Add(mainPortInfo);
				portInfoList.Add(zAxisPortInfo);
				portInfoList.Add(micrometerPortInfo);
				FileOperator.SerializeToXMLFile(portInfoList, filepath);
			}
			return portInfoList;
		}

		public static void SaveSerialPortConfigFile(List<SerialPortInfo> portInfoList, string filepath = "Config\\SerialPort_Config.xml")
		{
			FileOperator.SerializeToXMLFile(portInfoList, filepath);
		}

		public static HardnessTesterInfo LoadHVHardnessTesterConfigFile(string filepath = "Config\\HVHardnessTester_Config.xml")
		{
			HardnessTesterInfo hardnessTesterInfo = (HardnessTesterInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(HardnessTesterInfo));
			if (hardnessTesterInfo == null && filepath == "Config\\HVHardnessTester_Config.xml")
			{
				hardnessTesterInfo = new HardnessTesterInfo();
				hardnessTesterInfo.IsForceCoefficientOn = false;
				hardnessTesterInfo.IsHandShakeOn = true;
				hardnessTesterInfo.LargerForceCoefficient = 1f;
				hardnessTesterInfo.LesserForceCoefficient = 1f;
				hardnessTesterInfo.TurretAfterImpress = false;
				hardnessTesterInfo.MeasureAfterImpress = false;
				hardnessTesterInfo.ObjectiveForMeasure = "10X";
				hardnessTesterInfo.TurretInfoList = new List<TurretInfo>();
				TurretInfo info = new TurretInfo();
				info.TurretDirection = TurretDirection.Left;
				info.Number = 1;
				info.Object = "10X";
				hardnessTesterInfo.TurretInfoList.Add(info);
				hardnessTesterInfo.ScaleList = new List<ScaleSet>();
				ScaleSet scaleSet = new ScaleSet();
				scaleSet.ScaleName = "1kgf";
				scaleSet.Scale = "00";
				scaleSet.TestForce = "1kgf";
				scaleSet.Enabled = true;
				hardnessTesterInfo.ScaleList.Add(scaleSet);
				hardnessTesterInfo.ForceList = new List<ForceSet>();
				ForceSet forceSet = new ForceSet();
				forceSet.TestForce = "1kgf";
				forceSet.RealForce = 1f;
				forceSet.Enabled = true;
				hardnessTesterInfo.ForceList.Add(forceSet);
				FileOperator.SerializeToXMLFile(hardnessTesterInfo, filepath);
			}
			return hardnessTesterInfo;
		}

		public static void HVSaveHardnessTesterConfigFile(HardnessTesterInfo hardnessTesterInfo, string filepath = "Config\\HVHardnessTester_Config.xml")
		{
			FileOperator.SerializeToXMLFile(hardnessTesterInfo, filepath);
		}

		public static HardnessTesterInfo LoadHBWHardnessTesterConfigFile(string filepath = "Config\\HBWHardnessTester_Config.xml")
		{
			HardnessTesterInfo hardnessTesterInfo = (HardnessTesterInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(HardnessTesterInfo));
			if (hardnessTesterInfo == null && filepath == "Config\\HBWHardnessTester_Config.xml")
			{
				hardnessTesterInfo = new HardnessTesterInfo();
				hardnessTesterInfo.IsForceCoefficientOn = false;
				hardnessTesterInfo.IsHandShakeOn = true;
				hardnessTesterInfo.LargerForceCoefficient = 1f;
				hardnessTesterInfo.LesserForceCoefficient = 1f;
				hardnessTesterInfo.TurretInfoList = new List<TurretInfo>();
				TurretInfo info = new TurretInfo();
				info.TurretDirection = TurretDirection.Left;
				info.Number = 1;
				info.Object = "10X";
				hardnessTesterInfo.TurretInfoList.Add(info);
				hardnessTesterInfo.ScaleList = new List<ScaleSet>();
				ScaleSet scaleSet = new ScaleSet();
				scaleSet.ScaleName = "HBW1/1";
				scaleSet.Scale = "00";
				scaleSet.TestForce = "1kgf";
				scaleSet.Enabled = true;
				hardnessTesterInfo.ScaleList.Add(scaleSet);
				hardnessTesterInfo.ForceList = new List<ForceSet>();
				ForceSet forceSet = new ForceSet();
				forceSet.TestForce = "1kgf";
				forceSet.RealForce = 1f;
				forceSet.Enabled = true;
				hardnessTesterInfo.ForceList.Add(forceSet);
				FileOperator.SerializeToXMLFile(hardnessTesterInfo, filepath);
			}
			return hardnessTesterInfo;
		}

		public static void HBWSaveHardnessTesterConfigFile(HardnessTesterInfo hardnessTesterInfo, string filepath = "Config\\HBWHardnessTester_Config.xml")
		{
			FileOperator.SerializeToXMLFile(hardnessTesterInfo, filepath);
		}

		public static SYJKPlatformInfo LoadSYJKPlatformConfigFile(string filepath = "Config\\SYJKPlatform_Config.xml")
		{
			SYJKPlatformInfo syjkPlatformInfo = (SYJKPlatformInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(SYJKPlatformInfo));
			if (syjkPlatformInfo == null && filepath == "Config\\SYJKPlatform_Config.xml")
			{
				syjkPlatformInfo = new SYJKPlatformInfo();
				syjkPlatformInfo.PulsePerMM = 1600;
				syjkPlatformInfo.IsReverseHorizontalAxis = true;
				syjkPlatformInfo.IsReverseVertivalAxis = false;
				syjkPlatformInfo.CurrentRate = SpeedRate.Fast;
				syjkPlatformInfo.CenterLocation = new PointF(25f, 25f);
				syjkPlatformInfo.RateList = new List<SpeedRateInfo>();
				SpeedRateInfo slowInfo = new SpeedRateInfo();
				slowInfo.RateName = SpeedRate.Fast;
				slowInfo.StepDistance = 0.5f;
				slowInfo.BeginSpeed = 0.5f;
				slowInfo.Acceleration = 1f;
				slowInfo.FinalSpeed = 2f;
				SpeedRateInfo mediumInfo = new SpeedRateInfo();
				mediumInfo.RateName = SpeedRate.Fast;
				mediumInfo.StepDistance = 0.5f;
				mediumInfo.BeginSpeed = 0.5f;
				mediumInfo.Acceleration = 1f;
				mediumInfo.FinalSpeed = 2f;
				SpeedRateInfo fastInfo = new SpeedRateInfo();
				fastInfo.RateName = SpeedRate.Fast;
				fastInfo.StepDistance = 0.5f;
				fastInfo.BeginSpeed = 0.5f;
				fastInfo.Acceleration = 1f;
				fastInfo.FinalSpeed = 2f;
				SpeedRateInfo veryFastInfo = new SpeedRateInfo();
				veryFastInfo.RateName = SpeedRate.Fast;
				veryFastInfo.StepDistance = 0.5f;
				veryFastInfo.BeginSpeed = 0.5f;
				veryFastInfo.Acceleration = 1f;
				veryFastInfo.FinalSpeed = 2f;
				syjkPlatformInfo.RateList.Add(slowInfo);
				syjkPlatformInfo.RateList.Add(mediumInfo);
				syjkPlatformInfo.RateList.Add(fastInfo);
				syjkPlatformInfo.RateList.Add(veryFastInfo);
				FileOperator.SerializeToXMLFile(syjkPlatformInfo, filepath);
			}
			return syjkPlatformInfo;
		}

		public static void SaveSYJKPlatformConfigFile(SYJKPlatformInfo syjkPlatformInfo, string filepath = "Config\\SYJKPlatform_Config.xml")
		{
			FileOperator.SerializeToXMLFile(syjkPlatformInfo, filepath);
		}

		public static ZAxisInfo LoadZAxisConfigFile(string filepath = "Config\\ZAxis_Config.xml")
		{
			ZAxisInfo zAxisInfo = (ZAxisInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(ZAxisInfo));
			if (zAxisInfo == null && filepath == "Config\\ZAxis_Config.xml")
			{
				zAxisInfo = new ZAxisInfo();
				zAxisInfo.IsReverseDirection = false;
				zAxisInfo.PulsePerMM = 6400;
				zAxisInfo.StepDistance = 0.0003f;
				zAxisInfo.HasEmptyTrip = false;
				zAxisInfo.UpwardEmptyTrip = 0.0005f;
				zAxisInfo.DownwardEmptyTrip = 0.0005f;
				FileOperator.SerializeToXMLFile(zAxisInfo, filepath);
			}
			return zAxisInfo;
		}

		public static void SaveZAxisConfigFile(ZAxisInfo zAxisInfo, string filepath = "Config\\ZAxis_Config.xml")
		{
			FileOperator.SerializeToXMLFile(zAxisInfo, filepath);
		}

		public static BindingList<CalibrationInfo> LoadCalibrationConfigFile(string filepath)
		{
			BindingList<CalibrationInfo> calibrationList = (BindingList<CalibrationInfo>)FileOperator.DeserializeFromXMLFile(filepath, typeof(BindingList<CalibrationInfo>));
			if (calibrationList == null && (filepath == "Config\\Calibration_Config.xml" || filepath == "Config\\HKCalibration_Config.xml"))
			{
				calibrationList = new BindingList<CalibrationInfo>();
				FileOperator.SerializeToXMLFile(calibrationList, filepath);
			}
			return calibrationList;
		}

		public static void SaveCalibrationConfigFile(BindingList<CalibrationInfo> calibrationList, string filepath)
		{
			FileOperator.SerializeToXMLFile(calibrationList, filepath);
		}

		public static CameraInfo LoadCameraConfigFile(string filepath = "Config\\Camera_Config.xml")
		{
			CameraInfo cameraInfo = (CameraInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(CameraInfo));
			if (cameraInfo == null && filepath == "Config\\Camera_Config.xml")
			{
				cameraInfo = new CameraInfo();
				cameraInfo.ShowCameraState = false;
				cameraInfo.ShowFrameRate = false;
				cameraInfo.SKIP2InCollect = true;
				cameraInfo.AnalogGain = 1f;
				cameraInfo.ExposureTime = 50.0;
				FileOperator.SerializeToXMLFile(cameraInfo, filepath);
			}
			return cameraInfo;
		}

		public static void SaveCameraConfigFile(CameraInfo cameraInfo, string filepath = "Config\\Camera_Config.xml")
		{
			FileOperator.SerializeToXMLFile(cameraInfo, filepath);
		}

		public static SampleInfo LoadSampleInfoConfigFile(string filepath = "Config\\SampleInfo_Config.xml")
		{
			SampleInfo sampleInfo = (SampleInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(SampleInfo));
			if (sampleInfo == null && filepath == "Config\\SampleInfo_Config.xml")
			{
				sampleInfo = new SampleInfo();
				sampleInfo.SampleName = "Sample Name";
				sampleInfo.SampleSn = "Sample Sn";
				sampleInfo.HardnessH = 600.0;
				sampleInfo.HardnessL = 500.0;
				sampleInfo.InspectionUnit = "Inspection Unit";
				sampleInfo.InspectionDate = DateTime.Now;
				sampleInfo.Tester = "Tester";
				sampleInfo.Reviewer = "Revierer";
				FileOperator.SerializeToXMLFile(sampleInfo, filepath);
			}
			sampleInfo.InspectionDate = DateTime.Now;
			return sampleInfo;
		}

		public static void SaveSampleConfigFile(SampleInfo sampleInfo, string filepath = "Config\\SampleInfo_Config.xml")
		{
			FileOperator.SerializeToXMLFile(sampleInfo, filepath);
		}

		public static SavedData LoadSavedDataConfigFile(string filepath = "Config\\PanelData_Config.xml")
		{
			SavedData savedData = (SavedData)FileOperator.DeserializeFromXMLFile(filepath, typeof(SavedData));
			if (savedData == null && filepath == "Config\\PanelData_Config.xml")
			{
				savedData = new SavedData();
				savedData.Light = 10;
				savedData.LoadTime = 8;
				savedData.ConvertType = "HRC";
				savedData.HardnessLevel = "中块";
				savedData.DeepHardness = 550f;
				FileOperator.SerializeToXMLFile(savedData, filepath);
			}
			return savedData;
		}

		public static void SaveSavedDataConfigFile(SavedData savedData, string filepath = "Config\\PanelData_Config.xml")
		{
			FileOperator.SerializeToXMLFile(savedData, filepath);
		}

		public static AutoMeasureInfo LoadAutoMeasureConfigFile(string filepath = "Config\\AutoMeasure_Config.xml")
		{
			AutoMeasureInfo autoMeasureInfo = (AutoMeasureInfo)FileOperator.DeserializeFromXMLFile(filepath, typeof(AutoMeasureInfo));
			if (autoMeasureInfo == null && filepath == "Config\\AutoMeasure_Config.xml")
			{
				autoMeasureInfo = new AutoMeasureInfo();
				autoMeasureInfo.MaxGray = 110;
				autoMeasureInfo.MeadianRadius = 1;
				FileOperator.SerializeToXMLFile(autoMeasureInfo, filepath);
			}
			return autoMeasureInfo;
		}

		public static void SaveAutoMeasureInfoConfigFile(AutoMeasureInfo autoMeasureInfo, string filepath = "Config\\AutoMeasure_Config.xml")
		{
			FileOperator.SerializeToXMLFile(autoMeasureInfo, filepath);
		}
	}
}
