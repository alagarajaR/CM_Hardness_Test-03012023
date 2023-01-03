using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class GenericSettingForm : KryptonForm
	{
		private MainForm owner = null;

		private SavedData savedData = null;

		private GenericInfo genericInfo = null;

		private IContainer components = null;

		private KryptonGroupBox gbDeepHardness;

		private KryptonTextBox tbDeepHardness;

		private KryptonLabel lbHardness;

		private KryptonButton btnCancel;

		private KryptonButton btnSave;

		private KryptonGroupBox gbHardnessTestMode;

		private KryptonRadioButton radioHV;
        private KryptonPalette kp1;
        private KryptonRadioButton radioHK;

		public GenericSettingForm(MainForm owner)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;

            this.owner = owner;
			savedData = owner.savedData;
			genericInfo = owner.genericInfo;
		}

		private void GenericSettingForm_Load(object sender, EventArgs e)
		{
			tbDeepHardness.Text = savedData.DeepHardness.ToString();
			radioHV.Checked = genericInfo.SoftwareSeries == SoftwareSeries.HV;
			radioHK.Checked = genericInfo.SoftwareSeries == SoftwareSeries.HK;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				savedData.DeepHardness = float.Parse(tbDeepHardness.Text);
			}
			catch
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_GenericSetting_Message_EnteredDeepHardnessInvalid);
				return;
			}
			try
			{
				if (genericInfo.SoftwareSeries == SoftwareSeries.HV && radioHK.Checked)
				{
					owner.calibrationList = ConfigFileManager.LoadCalibrationConfigFile("Config\\HKCalibration_Config.xml");
					try
					{
						owner.UpdateCalibrationInfo();
					}
					catch
					{
					}
				}
				if (genericInfo.SoftwareSeries == SoftwareSeries.HK && radioHV.Checked)
				{
					owner.calibrationList = ConfigFileManager.LoadCalibrationConfigFile("Config\\Calibration_Config.xml");
					try
					{
						owner.UpdateCalibrationInfo();
					}
					catch
					{
					}
				}
				genericInfo.SoftwareSeries = ((!radioHV.Checked) ? SoftwareSeries.HK : SoftwareSeries.HV);
				ConfigFileManager.SaveGenericInfoConfigFile(genericInfo);
			}
			catch
			{
			}
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_GenericSetting_Title;
			gbDeepHardness.Text = ResourcesManager.Resources.R_GenericSetting_DeepHardness;
			lbHardness.Text = ResourcesManager.Resources.R_GenericSetting_Hardness;
			gbHardnessTestMode.Text = ResourcesManager.Resources.R_GenericSetting_HardnessTestMode;
			btnSave.Text = ResourcesManager.Resources.R_GenericSetting_Save;
			btnCancel.Text = ResourcesManager.Resources.R_GenericSetting_Cancel;
		}

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
            this.gbDeepHardness = new Krypton.Toolkit.KryptonGroupBox();
            this.tbDeepHardness = new Krypton.Toolkit.KryptonTextBox();
            this.lbHardness = new Krypton.Toolkit.KryptonLabel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.gbHardnessTestMode = new Krypton.Toolkit.KryptonGroupBox();
            this.radioHK = new Krypton.Toolkit.KryptonRadioButton();
            this.radioHV = new Krypton.Toolkit.KryptonRadioButton();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gbDeepHardness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbDeepHardness.Panel)).BeginInit();
            this.gbDeepHardness.Panel.SuspendLayout();
            this.gbDeepHardness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbHardnessTestMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbHardnessTestMode.Panel)).BeginInit();
            this.gbHardnessTestMode.Panel.SuspendLayout();
            this.gbHardnessTestMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDeepHardness
            // 
            this.gbDeepHardness.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbDeepHardness.Location = new System.Drawing.Point(12, 13);
            this.gbDeepHardness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbDeepHardness.Name = "gbDeepHardness";
            this.gbDeepHardness.Palette = this.kp1;
            this.gbDeepHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbDeepHardness.Panel
            // 
            this.gbDeepHardness.Panel.Controls.Add(this.tbDeepHardness);
            this.gbDeepHardness.Panel.Controls.Add(this.lbHardness);
            this.gbDeepHardness.Size = new System.Drawing.Size(349, 96);
            this.gbDeepHardness.TabIndex = 14;
            this.gbDeepHardness.Values.Heading = "Deep Hardness";
            // 
            // tbDeepHardness
            // 
            this.tbDeepHardness.Location = new System.Drawing.Point(175, 18);
            this.tbDeepHardness.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbDeepHardness.Name = "tbDeepHardness";
            this.tbDeepHardness.Palette = this.kp1;
            this.tbDeepHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbDeepHardness.Size = new System.Drawing.Size(110, 23);
            this.tbDeepHardness.TabIndex = 13;
            // 
            // lbHardness
            // 
            this.lbHardness.Location = new System.Drawing.Point(17, 22);
            this.lbHardness.Name = "lbHardness";
            this.lbHardness.Palette = this.kp1;
            this.lbHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHardness.Size = new System.Drawing.Size(61, 20);
            this.lbHardness.TabIndex = 12;
            this.lbHardness.Values.Text = "Hardness";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(207, 229);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(154, 46);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 229);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(154, 46);
            this.btnSave.TabIndex = 15;
            this.btnSave.Values.Text = "Confirm";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbHardnessTestMode
            // 
            this.gbHardnessTestMode.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbHardnessTestMode.Location = new System.Drawing.Point(12, 117);
            this.gbHardnessTestMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbHardnessTestMode.Name = "gbHardnessTestMode";
            this.gbHardnessTestMode.Palette = this.kp1;
            this.gbHardnessTestMode.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbHardnessTestMode.Panel
            // 
            this.gbHardnessTestMode.Panel.Controls.Add(this.radioHK);
            this.gbHardnessTestMode.Panel.Controls.Add(this.radioHV);
            this.gbHardnessTestMode.Size = new System.Drawing.Size(349, 104);
            this.gbHardnessTestMode.TabIndex = 15;
            this.gbHardnessTestMode.Values.Heading = "Hardness Test Mode";
            // 
            // radioHK
            // 
            this.radioHK.Location = new System.Drawing.Point(175, 28);
            this.radioHK.Name = "radioHK";
            this.radioHK.Palette = this.kp1;
            this.radioHK.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.radioHK.Size = new System.Drawing.Size(38, 20);
            this.radioHK.TabIndex = 1;
            this.radioHK.Values.Text = "HK";
            // 
            // radioHV
            // 
            this.radioHV.Checked = true;
            this.radioHV.Location = new System.Drawing.Point(59, 28);
            this.radioHV.Name = "radioHV";
            this.radioHV.Palette = this.kp1;
            this.radioHV.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.radioHV.Size = new System.Drawing.Size(39, 20);
            this.radioHV.TabIndex = 0;
            this.radioHV.Values.Text = "HV";
            // 
            // GenericSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 289);
            this.Controls.Add(this.gbHardnessTestMode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbDeepHardness);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenericSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generic Setting";
            this.Load += new System.EventHandler(this.GenericSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbDeepHardness.Panel)).EndInit();
            this.gbDeepHardness.Panel.ResumeLayout(false);
            this.gbDeepHardness.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbDeepHardness)).EndInit();
            this.gbDeepHardness.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbHardnessTestMode.Panel)).EndInit();
            this.gbHardnessTestMode.Panel.ResumeLayout(false);
            this.gbHardnessTestMode.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbHardnessTestMode)).EndInit();
            this.gbHardnessTestMode.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}
