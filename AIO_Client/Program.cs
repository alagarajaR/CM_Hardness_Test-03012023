using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using Labtt.Data;
using Labtt.Service;
using MessageBoxExApp;

namespace AIO_Client
{

	internal static class Program
	{
		private const string DogSn = "19422C323606420B";

		private static Mutex mutex;

		public static string SERIAL_ID = "31012000";

		public static int CertifiedCameraIndex { get; set; }

		public static Krypton.Toolkit.PaletteMode palete_mode { get; set; }
		public static ComponentFactory.Krypton.Toolkit.PaletteMode palete_mode1 { get; set; }
		[STAThread]
		private static void Main()
		{
			try
			{

				palete_mode = Krypton.Toolkit.PaletteMode.Office2010Blue;
				palete_mode1 = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;


				GenericInfo genericInfo = ConfigFileManager.GenericInfo;
				if (genericInfo.SoftwareSeries == SoftwareSeries.HV || genericInfo.SoftwareSeries == SoftwareSeries.HK)
				{
					if (genericInfo.SoftwareVersion == SoftwareVersion.Full_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Full_Weight || genericInfo.SoftwareVersion == SoftwareVersion.Semi_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Semi_Weight)
					{
						SERIAL_ID = "31011000";
					}
				}
				else if (genericInfo.SoftwareSeries == SoftwareSeries.HBW)
				{
					SERIAL_ID = "31022000";
				}
				LanguageInfo languageInfo = genericInfo.LanguageInfoList.FirstOrDefault((LanguageInfo x) => x.LanguageName == genericInfo.CurrentLanguageName);
				if (languageInfo == null)
				{
					languageInfo = genericInfo.LanguageInfoList.First();
				}
				ResourcesManager.Initialize(languageInfo);
				MsgBox.InfoCaption = ResourcesManager.Resources.R_MsgBox_InfoCaption;
				MsgBox.WarningCaption = ResourcesManager.Resources.R_MsgBox_WarningCaption;
				MsgBox.ErrorCaption = ResourcesManager.Resources.R_MsgBox_ErrorCaption;
				MsgBox.InfoButtonText = ResourcesManager.Resources.R_MsgBox_InfoButtonText;
				MsgBox.WarningButtonText = ResourcesManager.Resources.R_MsgBox_WarningButtonText;
				MsgBox.ErrorButtonText = ResourcesManager.Resources.R_MsgBox_ErrorButtonText;
				MsgBox.YesNoButtonText = new string[2]
				{
				ResourcesManager.Resources.R_MsgBox_Yes,
				ResourcesManager.Resources.R_MsgBox_No
				};
				MsgBox.OKCancelButtonText = new string[2]
				{
				ResourcesManager.Resources.R_MsgBox_OK,
				ResourcesManager.Resources.R_MsgBox_Cancel
				};
				mutex = new Mutex(initiallyOwned: true, "AIOFrame");
				if (!mutex.WaitOne(0, exitContext: false))
				{
					throw new Exception(ResourcesManager.Resources.R_Message_ApplicationIsAlreadyRunning);
				}
				if (genericInfo.IsEncryptedBySecurityDog)
				{
					//CheckSecurityDog();
				}
				else
				{
					Decrypt();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, ex.Message);
				bool enableSound = true;
				MsgBox.ShowError(ex.Message, null, null, enableSound);
				Environment.Exit(0);
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new MainForm());
		}

		private static void Decrypt()
		{
			if (!File.Exists("key_labtt.data"))
			{
				throw new FileNotFoundException(ResourcesManager.Resources.R_Message_CouldNotFoundEncryptedFile);
			}
			StreamReader sreader = new StreamReader("key_labtt.data");
			string authonticationCode_Encrypt = sreader.ReadLine().Trim();
			string softwareNumber_Encrypt = sreader.ReadLine().Trim();
			string lastTimeStartSysDateTime_Encrypt = sreader.ReadLine();
			string endDate_Encrypt = sreader.ReadLine();
			sreader.Close();
			string strAuthonticationCode = DESHelper.DesDecrypt(authonticationCode_Encrypt);
			string strSoftwareNumber = DESHelper.DesDecrypt(softwareNumber_Encrypt);
			string strLastTimeStartSysDateTime = DESHelper.DesDecrypt(lastTimeStartSysDateTime_Encrypt);
			string strEndDateTime = DESHelper.DesDecrypt(endDate_Encrypt);
			string[] strTemp = strAuthonticationCode.Split('-');
			string strSerialNumber = strTemp[0];
			string strMac = strTemp[1];
			string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			int cameraCount = DVPCameraService.ConnectedCameraCount;
			if (cameraCount <= 0)
			{
				throw new Exception(ResourcesManager.Resources.R_Message_CameraNotFound);
			}
			for (int i = 0; i < cameraCount; i++)
			{
				if (strSerialNumber == DVPCameraService.GetCameraSerialNumber(i))
				{
					CertifiedCameraIndex = i;
				}
				else if (i == cameraCount - 1)
				{
					throw new Exception(ResourcesManager.Resources.R_Message_CameraMismatch);
				}
			}
			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			if (interfaces.Length == 0)
			{
				throw new MethodAccessException(ResourcesManager.Resources.R_Message_FailedToReadNetworkCard);
			}
			NetworkInterface networkInterface = null;
			NetworkInterface[] array = interfaces;
			foreach (NetworkInterface ni in array)
			{
				if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
				{
					if (networkInterface == null)
					{
						networkInterface = ni;
					}
					if (ni.Description.ToLower().Contains("realtek"))
					{
						networkInterface = ni;
						break;
					}
				}
			}
			string niMac = BitConverter.ToString(networkInterface.GetPhysicalAddress().GetAddressBytes());
			niMac = niMac.Trim().Replace("-", "");
			for (int i = 0; i < strMac.Length / 12 && !(strMac.Substring(i * 12, 12).ToUpper() == niMac.ToUpper()); i++)
			{
				if (i == strMac.Length / 12 - 1)
				{
					throw new MethodAccessException(ResourcesManager.Resources.R_Message_VerificationCodeOfEncryptedFileIsWrong);
				}
			}
			if (strSoftwareNumber != SERIAL_ID)
			{
				throw new Exception(ResourcesManager.Resources.R_Message_EncryptedFileVersionMismatch);
			}
			DateTime lastTimeStartSysDateTime = Convert.ToDateTime(strLastTimeStartSysDateTime);
			DateTime endDateTime = Convert.ToDateTime(strEndDateTime);
			if (DateTime.Compare(lastTimeStartSysDateTime, DateTime.Now) > 0)
			{
			}
			if (DateTime.Compare(lastTimeStartSysDateTime, endDateTime) > 0)
			{
				throw new Exception(ResourcesManager.Resources.R_Message_SoftwareExpired);
			}
			DateTime dateNow = Convert.ToDateTime(strNow);
			string strNow_Encrypt = DESHelper.DesEncrypt(strNow);
			string strFileName = "key_labtt.data";
			FileStream fs = new FileStream(strFileName, FileMode.Create);
			StreamWriter wr = new StreamWriter(fs);
			wr.WriteLine(authonticationCode_Encrypt);
			wr.WriteLine(softwareNumber_Encrypt);
			wr.WriteLine(strNow_Encrypt);
			wr.WriteLine(endDate_Encrypt);
			wr.Close();
		}

		private static void CheckSecurityDog()
		{
			string hID;
			string uID;
			switch (ServiceFTSecurityDog.CheckSecurityDog("19422C323606420B", SERIAL_ID, out hID, out uID))
			{
				case -1:
					throw new Exception(ResourcesManager.Resources.R_Message_ReadSecurityDogError.Replace("{INFO1}", hID).Replace("{INFO2}", uID));
				case 1:
					throw new Exception(ResourcesManager.Resources.R_Message_SecurityDogWrongHID.Replace("{INFO1}", hID).Replace("{INFO2}", uID));
				case 2:
					throw new Exception(ResourcesManager.Resources.R_Message_SecurityDogWrongUID.Replace("{INFO1}", hID).Replace("{INFO2}", uID));
				case 3:
					throw new Exception(ResourcesManager.Resources.R_Message_SecurityDogExpired.Replace("{INFO1}", hID).Replace("{INFO2}", uID));
			}
		}
	}
}
