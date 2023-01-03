using System;
using System.Collections.Generic;

namespace AIO_Client
{

	[Serializable]
	public class GenericInfo
	{
		public SoftwareSeries SoftwareSeries { get; set; }

		public SoftwareVersion SoftwareVersion { get; set; }

		public bool MicrometerOn { get; set; }

		public bool TurretOn { get; set; }

		public bool IsEncryptedBySecurityDog { get; set; }

		public string CurrentLanguageName { get; set; }

		public List<LanguageInfo> LanguageInfoList { get; set; }
	}
}
