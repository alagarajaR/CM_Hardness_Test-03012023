using System;

namespace AIO_Client
{

	[Serializable]
	public class LanguageInfo
	{
		public string LanguageName { get; set; }

		public string ResourcesFilepath { get; set; }

		public string SimpleReportTemplateFilepath { get; set; }

		public string ReportWithImageTemplateFilepath { get; set; }

		public string ReportWithDeepHardnessTemplateFilepath { get; set; }

		public string FullReportTemplateFilepath { get; set; }
	}
}