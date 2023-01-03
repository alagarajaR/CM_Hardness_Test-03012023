using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Labtt.Data;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class OtherSettingForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonButton btnViewHistoryImage;

		private KryptonGroupBox gbTrimStep;

		private KryptonLabel lbSlow;

		private KryptonLabel lbFast;

		private KryptonLabel lbImageSize;

		private KryptonLabel lbImageCount;

		private KryptonLabel label4;

		private KryptonLabel lbPictures;

		private KryptonButton btnSave;

		private KryptonButton btnCancel;

		private KryptonGroupBox gbHistoryImage;

		private KryptonGroupBox gbLanguage;

		private KryptonLabel lbLanguage;

		private KryptonComboBox cbLanguage;
        private KryptonNumericUpDown nudFast;
        private KryptonNumericUpDown nudSlow;
        private KryptonPalette kp1;
        private StepInfo stepInfo = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnViewHistoryImage = new Krypton.Toolkit.KryptonButton();
            this.gbTrimStep = new Krypton.Toolkit.KryptonGroupBox();
            this.nudSlow = new Krypton.Toolkit.KryptonNumericUpDown();
            this.nudFast = new Krypton.Toolkit.KryptonNumericUpDown();
            this.lbSlow = new Krypton.Toolkit.KryptonLabel();
            this.lbFast = new Krypton.Toolkit.KryptonLabel();
            this.lbImageSize = new Krypton.Toolkit.KryptonLabel();
            this.lbImageCount = new Krypton.Toolkit.KryptonLabel();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.lbPictures = new Krypton.Toolkit.KryptonLabel();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.gbHistoryImage = new Krypton.Toolkit.KryptonGroupBox();
            this.gbLanguage = new Krypton.Toolkit.KryptonGroupBox();
            this.cbLanguage = new Krypton.Toolkit.KryptonComboBox();
            this.lbLanguage = new Krypton.Toolkit.KryptonLabel();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gbTrimStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbTrimStep.Panel)).BeginInit();
            this.gbTrimStep.Panel.SuspendLayout();
            this.gbTrimStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbHistoryImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbHistoryImage.Panel)).BeginInit();
            this.gbHistoryImage.Panel.SuspendLayout();
            this.gbHistoryImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbLanguage.Panel)).BeginInit();
            this.gbLanguage.Panel.SuspendLayout();
            this.gbLanguage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLanguage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnViewHistoryImage
            // 
            this.btnViewHistoryImage.Location = new System.Drawing.Point(279, 26);
            this.btnViewHistoryImage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnViewHistoryImage.Name = "btnViewHistoryImage";
            this.btnViewHistoryImage.Palette = this.kp1;
            this.btnViewHistoryImage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnViewHistoryImage.Size = new System.Drawing.Size(112, 37);
            this.btnViewHistoryImage.TabIndex = 0;
            this.btnViewHistoryImage.Values.Text = "View History";
            this.btnViewHistoryImage.Click += new System.EventHandler(this.btnHistoryImage_Click);
            // 
            // gbTrimStep
            // 
            this.gbTrimStep.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbTrimStep.Location = new System.Drawing.Point(11, 87);
            this.gbTrimStep.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbTrimStep.Name = "gbTrimStep";
            this.gbTrimStep.Palette = this.kp1;
            this.gbTrimStep.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbTrimStep.Panel
            // 
            this.gbTrimStep.Panel.Controls.Add(this.nudSlow);
            this.gbTrimStep.Panel.Controls.Add(this.nudFast);
            this.gbTrimStep.Panel.Controls.Add(this.lbSlow);
            this.gbTrimStep.Panel.Controls.Add(this.lbFast);
            this.gbTrimStep.Size = new System.Drawing.Size(433, 81);
            this.gbTrimStep.TabIndex = 1;
            this.gbTrimStep.Values.Heading = "Trim Measure";
            // 
            // nudSlow
            // 
            this.nudSlow.Location = new System.Drawing.Point(294, 19);
            this.nudSlow.Name = "nudSlow";
            this.nudSlow.Palette = this.kp1;
            this.nudSlow.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.nudSlow.Size = new System.Drawing.Size(45, 22);
            this.nudSlow.TabIndex = 5;
            // 
            // nudFast
            // 
            this.nudFast.Location = new System.Drawing.Point(97, 19);
            this.nudFast.Name = "nudFast";
            this.nudFast.Palette = this.kp1;
            this.nudFast.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.nudFast.Size = new System.Drawing.Size(45, 22);
            this.nudFast.TabIndex = 4;
            // 
            // lbSlow
            // 
            this.lbSlow.Location = new System.Drawing.Point(211, 19);
            this.lbSlow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlow.Name = "lbSlow";
            this.lbSlow.Palette = this.kp1;
            this.lbSlow.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSlow.Size = new System.Drawing.Size(36, 20);
            this.lbSlow.TabIndex = 1;
            this.lbSlow.Values.Text = "Slow";
            // 
            // lbFast
            // 
            this.lbFast.Location = new System.Drawing.Point(12, 18);
            this.lbFast.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFast.Name = "lbFast";
            this.lbFast.Palette = this.kp1;
            this.lbFast.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbFast.Size = new System.Drawing.Size(32, 20);
            this.lbFast.TabIndex = 0;
            this.lbFast.Values.Text = "Fast";
            // 
            // lbImageSize
            // 
            this.lbImageSize.Location = new System.Drawing.Point(183, 34);
            this.lbImageSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbImageSize.Name = "lbImageSize";
            this.lbImageSize.Palette = this.kp1;
            this.lbImageSize.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbImageSize.Size = new System.Drawing.Size(17, 20);
            this.lbImageSize.TabIndex = 7;
            this.lbImageSize.Values.Text = "0";
            // 
            // lbImageCount
            // 
            this.lbImageCount.Location = new System.Drawing.Point(70, 34);
            this.lbImageCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbImageCount.Name = "lbImageCount";
            this.lbImageCount.Palette = this.kp1;
            this.lbImageCount.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbImageCount.Size = new System.Drawing.Size(17, 20);
            this.lbImageCount.TabIndex = 6;
            this.lbImageCount.Values.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(238, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Palette = this.kp1;
            this.label4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label4.Size = new System.Drawing.Size(29, 20);
            this.label4.TabIndex = 5;
            this.label4.Values.Text = "MB";
            // 
            // lbPictures
            // 
            this.lbPictures.Location = new System.Drawing.Point(121, 34);
            this.lbPictures.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPictures.Name = "lbPictures";
            this.lbPictures.Palette = this.kp1;
            this.lbPictures.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbPictures.Size = new System.Drawing.Size(41, 20);
            this.lbPictures.TabIndex = 4;
            this.lbPictures.Values.Text = "Open";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(89, 286);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(112, 37);
            this.btnSave.TabIndex = 8;
            this.btnSave.Values.Text = "Confirm";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(268, 286);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(112, 37);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbHistoryImage
            // 
            this.gbHistoryImage.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbHistoryImage.Location = new System.Drawing.Point(11, 174);
            this.gbHistoryImage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbHistoryImage.Name = "gbHistoryImage";
            this.gbHistoryImage.Palette = this.kp1;
            this.gbHistoryImage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbHistoryImage.Panel
            // 
            this.gbHistoryImage.Panel.Controls.Add(this.btnViewHistoryImage);
            this.gbHistoryImage.Panel.Controls.Add(this.lbImageSize);
            this.gbHistoryImage.Panel.Controls.Add(this.lbPictures);
            this.gbHistoryImage.Panel.Controls.Add(this.label4);
            this.gbHistoryImage.Panel.Controls.Add(this.lbImageCount);
            this.gbHistoryImage.Size = new System.Drawing.Size(433, 106);
            this.gbHistoryImage.TabIndex = 11;
            this.gbHistoryImage.Values.Heading = "History Image";
            // 
            // gbLanguage
            // 
            this.gbLanguage.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbLanguage.Location = new System.Drawing.Point(11, -1);
            this.gbLanguage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gbLanguage.Name = "gbLanguage";
            this.gbLanguage.Palette = this.kp1;
            this.gbLanguage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbLanguage.Panel
            // 
            this.gbLanguage.Panel.Controls.Add(this.cbLanguage);
            this.gbLanguage.Panel.Controls.Add(this.lbLanguage);
            this.gbLanguage.Size = new System.Drawing.Size(433, 82);
            this.gbLanguage.TabIndex = 4;
            this.gbLanguage.Values.Heading = "Language";
            // 
            // cbLanguage
            // 
            this.cbLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.DropDownWidth = 107;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.IntegralHeight = false;
            this.cbLanguage.Location = new System.Drawing.Point(100, 16);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Palette = this.kp1;
            this.cbLanguage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbLanguage.Size = new System.Drawing.Size(107, 21);
            this.cbLanguage.TabIndex = 3;
            // 
            // lbLanguage
            // 
            this.lbLanguage.Location = new System.Drawing.Point(15, 19);
            this.lbLanguage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLanguage.Name = "lbLanguage";
            this.lbLanguage.Palette = this.kp1;
            this.lbLanguage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLanguage.Size = new System.Drawing.Size(64, 20);
            this.lbLanguage.TabIndex = 0;
            this.lbLanguage.Values.Text = "Language";
            // 
            // OtherSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 332);
            this.Controls.Add(this.gbLanguage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbTrimStep);
            this.Controls.Add(this.gbHistoryImage);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OtherSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Other Setting";
            this.Load += new System.EventHandler(this.OtherSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbTrimStep.Panel)).EndInit();
            this.gbTrimStep.Panel.ResumeLayout(false);
            this.gbTrimStep.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbTrimStep)).EndInit();
            this.gbTrimStep.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbHistoryImage.Panel)).EndInit();
            this.gbHistoryImage.Panel.ResumeLayout(false);
            this.gbHistoryImage.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbHistoryImage)).EndInit();
            this.gbHistoryImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbLanguage.Panel)).EndInit();
            this.gbLanguage.Panel.ResumeLayout(false);
            this.gbLanguage.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbLanguage)).EndInit();
            this.gbLanguage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbLanguage)).EndInit();
            this.ResumeLayout(false);

		}

		public OtherSettingForm()
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;

        }

		private void OtherSettingForm_Load(object sender, EventArgs e)
		{
			cbLanguage.Items.Clear();
			GenericInfo genericInfo = ConfigFileManager.GenericInfo;
			foreach (LanguageInfo languageInfo in genericInfo.LanguageInfoList)
			{
				cbLanguage.Items.Add(languageInfo.LanguageName);
			}
			cbLanguage.Text = genericInfo.CurrentLanguageName;
			stepInfo = (StepInfo)FileOperator.DeserializeFromXMLFile(CommonData.TrimMeasureConfigFilepath, typeof(StepInfo));
			if (stepInfo == null)
			{
				stepInfo = new StepInfo();
				stepInfo.StepLengthHigh = 5;
				stepInfo.StepLengthNormal = 1;
			}
			nudFast.Value = stepInfo.StepLengthHigh;
			nudSlow.Value = stepInfo.StepLengthNormal;
			long byteSize = GetDirectorySize(CommonData.OriginalImageDirectoryPath, ".BMP");
			int size = (int)(byteSize / 1024 / 1024);
			int count = GetFileCount(CommonData.OriginalImageDirectoryPath, ".BMP");
			lbImageSize.Text = size.ToString();
			lbImageCount.Text = count.ToString();
		}

		public static long GetDirectorySize(string dirPath, string extensionName)
		{
			if (!Directory.Exists(dirPath))
			{
				return 0L;
			}
			long size = 0L;
			DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				if (extensionName.ToUpper() == "*" || fileInfo.Extension.ToUpper() == extensionName.ToUpper())
				{
					size += fileInfo.Length;
				}
			}
			return size;
		}

		public static int GetFileCount(string dirPath, string extensionName)
		{
			int count = 0;
			if (Directory.Exists(dirPath))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					if (extensionName.ToUpper() == "*" || fileInfo.Extension.ToUpper() == extensionName.ToUpper())
					{
						count++;
					}
				}
			}
			return count;
		}

		private void btnHistoryImage_Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", CommonData.OriginalImageDirectoryPath);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				GenericInfo genericInfo = ConfigFileManager.GenericInfo;
				if (cbLanguage.Text != genericInfo.CurrentLanguageName)
				{
					genericInfo.CurrentLanguageName = cbLanguage.Text;
					ConfigFileManager.SaveGenericInfoConfigFile(genericInfo);
				}
			}
			catch (Exception ex2)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_OtherSetting_SaveFail);
				Logger.Error(ex2, "Failed to save language information！");
				return;
			}
			try
			{
				stepInfo.StepLengthHigh = decimal.ToInt32(nudFast.Value);
				stepInfo.StepLengthNormal = decimal.ToInt32(nudSlow.Value);
				FileOperator.SerializeToXMLFile(stepInfo, CommonData.TrimMeasureConfigFilepath);
			}
			catch (Exception ex2)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_OtherSetting_SaveFail);
				Logger.Error(ex2, "Failed to save step information");
				return;
			}
			MsgBox.ShowInfo(ResourcesManager.Resources.R_OtherSetting_SaveSuccessful);
			Dispose();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_OtherSetting_Title;
			gbLanguage.Text = ResourcesManager.Resources.R_OtherSetting_LanguageSetting;
			lbLanguage.Text = ResourcesManager.Resources.R_OtherSetting_Language;
			gbTrimStep.Text = ResourcesManager.Resources.R_OtherSetting_TrimStep;
			lbFast.Text = ResourcesManager.Resources.R_OtherSetting_Fast;
			lbSlow.Text = ResourcesManager.Resources.R_OtherSetting_Slow;
			gbHistoryImage.Text = ResourcesManager.Resources.R_OtherSetting_HistoryImage;
			lbPictures.Text = ResourcesManager.Resources.R_OtherSetting_Pictures;
			btnViewHistoryImage.Text = ResourcesManager.Resources.R_OtherSetting_View;
			btnSave.Text = ResourcesManager.Resources.R_OtherSetting_Save;
			btnCancel.Text = ResourcesManager.Resources.R_OtherSetting_Cancel;
		}
	}
}
