using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Labtt.Communication.MasterControl;
using Labtt.Meterage;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class AutoMeasureSettingForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonGroupBox gbAutoMeasureCorrect;

		private KryptonLabel lbThresholdValue;

		private KryptonLabel lbThreshold;

		private KryptonLabel lbMedianRadius;

		private KryptonLabel lbSmoothing;

		private KryptonGroupBox gbAutoMeasure;

		private KryptonCheckBox cbTurretAfterImpress;

		private KryptonCheckBox cbMeasureAfterImpress;

		private KryptonLabel lbObjectiveForMeasure;

		internal KryptonComboBox cbTurret;

		private KryptonPanel panel1;

		private KryptonButton btnDefault;

		private KryptonButton btnSave;

		private KryptonButton btnCancel;
        private KryptonTrackBar trbMedianRadius;
        private KryptonTrackBar trbThreshold;
        private KryptonPalette kp1;
        private MainForm owner = null;

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
            this.gbAutoMeasureCorrect = new Krypton.Toolkit.KryptonGroupBox();
            this.trbThreshold = new Krypton.Toolkit.KryptonTrackBar();
            this.trbMedianRadius = new Krypton.Toolkit.KryptonTrackBar();
            this.lbMedianRadius = new Krypton.Toolkit.KryptonLabel();
            this.lbSmoothing = new Krypton.Toolkit.KryptonLabel();
            this.lbThresholdValue = new Krypton.Toolkit.KryptonLabel();
            this.lbThreshold = new Krypton.Toolkit.KryptonLabel();
            this.gbAutoMeasure = new Krypton.Toolkit.KryptonGroupBox();
            this.cbTurret = new Krypton.Toolkit.KryptonComboBox();
            this.cbTurretAfterImpress = new Krypton.Toolkit.KryptonCheckBox();
            this.cbMeasureAfterImpress = new Krypton.Toolkit.KryptonCheckBox();
            this.lbObjectiveForMeasure = new Krypton.Toolkit.KryptonLabel();
            this.panel1 = new Krypton.Toolkit.KryptonPanel();
            this.btnDefault = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasureCorrect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasureCorrect.Panel)).BeginInit();
            this.gbAutoMeasureCorrect.Panel.SuspendLayout();
            this.gbAutoMeasureCorrect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasure.Panel)).BeginInit();
            this.gbAutoMeasure.Panel.SuspendLayout();
            this.gbAutoMeasure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTurret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAutoMeasureCorrect
            // 
            this.gbAutoMeasureCorrect.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAutoMeasureCorrect.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbAutoMeasureCorrect.Location = new System.Drawing.Point(0, 0);
            this.gbAutoMeasureCorrect.Name = "gbAutoMeasureCorrect";
            this.gbAutoMeasureCorrect.Palette = this.kp1;
            this.gbAutoMeasureCorrect.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbAutoMeasureCorrect.Panel
            // 
            this.gbAutoMeasureCorrect.Panel.Controls.Add(this.trbThreshold);
            this.gbAutoMeasureCorrect.Panel.Controls.Add(this.trbMedianRadius);
            this.gbAutoMeasureCorrect.Panel.Controls.Add(this.lbMedianRadius);
            this.gbAutoMeasureCorrect.Panel.Controls.Add(this.lbSmoothing);
            this.gbAutoMeasureCorrect.Panel.Controls.Add(this.lbThresholdValue);
            this.gbAutoMeasureCorrect.Panel.Controls.Add(this.lbThreshold);
            this.gbAutoMeasureCorrect.Size = new System.Drawing.Size(453, 140);
            this.gbAutoMeasureCorrect.TabIndex = 0;
            this.gbAutoMeasureCorrect.Values.Heading = "Auto Measure Correct";
            // 
            // trbThreshold
            // 
            this.trbThreshold.BackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.trbThreshold.Location = new System.Drawing.Point(168, 87);
            this.trbThreshold.Maximum = 250;
            this.trbThreshold.Minimum = 10;
            this.trbThreshold.Name = "trbThreshold";
            this.trbThreshold.Palette = this.kp1;
            this.trbThreshold.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.trbThreshold.Size = new System.Drawing.Size(191, 21);
            this.trbThreshold.TabIndex = 22;
            this.trbThreshold.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbThreshold.Value = 10;
            this.trbThreshold.ValueChanged += new System.EventHandler(this.trbThreshold_ValueChanged);
            this.trbThreshold.Scroll += new System.EventHandler(this.trbThreshold_Scroll);
            this.trbThreshold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbThreshold_MouseUp);
            // 
            // trbMedianRadius
            // 
            this.trbMedianRadius.BackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.trbMedianRadius.LargeChange = 1;
            this.trbMedianRadius.Location = new System.Drawing.Point(168, 45);
            this.trbMedianRadius.Maximum = 20;
            this.trbMedianRadius.Minimum = 1;
            this.trbMedianRadius.Name = "trbMedianRadius";
            this.trbMedianRadius.Palette = this.kp1;
            this.trbMedianRadius.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.trbMedianRadius.Size = new System.Drawing.Size(191, 21);
            this.trbMedianRadius.TabIndex = 21;
            this.trbMedianRadius.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbMedianRadius.Value = 1;
            this.trbMedianRadius.Scroll += new System.EventHandler(this.trbMedianRadius_Scroll);
            this.trbMedianRadius.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbMedianRadius_MouseUp);
            // 
            // lbMedianRadius
            // 
            this.lbMedianRadius.Location = new System.Drawing.Point(365, 45);
            this.lbMedianRadius.Name = "lbMedianRadius";
            this.lbMedianRadius.Size = new System.Drawing.Size(17, 20);
            this.lbMedianRadius.TabIndex = 20;
            this.lbMedianRadius.Values.Text = "1";
            // 
            // lbSmoothing
            // 
            this.lbSmoothing.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSmoothing.Location = new System.Drawing.Point(12, 45);
            this.lbSmoothing.Name = "lbSmoothing";
            this.lbSmoothing.Palette = this.kp1;
            this.lbSmoothing.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSmoothing.Size = new System.Drawing.Size(71, 20);
            this.lbSmoothing.TabIndex = 19;
            this.lbSmoothing.Values.Text = "Smoothing";
            // 
            // lbThresholdValue
            // 
            this.lbThresholdValue.Location = new System.Drawing.Point(365, 87);
            this.lbThresholdValue.Name = "lbThresholdValue";
            this.lbThresholdValue.Size = new System.Drawing.Size(30, 20);
            this.lbThresholdValue.TabIndex = 17;
            this.lbThresholdValue.Values.Text = "110";
            // 
            // lbThreshold
            // 
            this.lbThreshold.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbThreshold.Location = new System.Drawing.Point(12, 87);
            this.lbThreshold.Name = "lbThreshold";
            this.lbThreshold.Palette = this.kp1;
            this.lbThreshold.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbThreshold.Size = new System.Drawing.Size(64, 20);
            this.lbThreshold.TabIndex = 16;
            this.lbThreshold.Values.Text = "Threshold";
            // 
            // gbAutoMeasure
            // 
            this.gbAutoMeasure.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAutoMeasure.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbAutoMeasure.Location = new System.Drawing.Point(0, 140);
            this.gbAutoMeasure.Name = "gbAutoMeasure";
            this.gbAutoMeasure.Palette = this.kp1;
            this.gbAutoMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbAutoMeasure.Panel
            // 
            this.gbAutoMeasure.Panel.Controls.Add(this.cbTurret);
            this.gbAutoMeasure.Panel.Controls.Add(this.cbTurretAfterImpress);
            this.gbAutoMeasure.Panel.Controls.Add(this.cbMeasureAfterImpress);
            this.gbAutoMeasure.Panel.Controls.Add(this.lbObjectiveForMeasure);
            this.gbAutoMeasure.Size = new System.Drawing.Size(453, 158);
            this.gbAutoMeasure.TabIndex = 5;
            this.gbAutoMeasure.Values.Heading = "Auto Measure";
            // 
            // cbTurret
            // 
            this.cbTurret.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbTurret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTurret.DropDownWidth = 150;
            this.cbTurret.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTurret.FormattingEnabled = true;
            this.cbTurret.IntegralHeight = false;
            this.cbTurret.Items.AddRange(new object[] {
            "10gf",
            "15gf",
            "20gf",
            "25gf",
            "50gf",
            "100gf",
            "200gf",
            "300gf",
            "500gf",
            "1000gf",
            "2000gf",
            "2500gf",
            "3000gf",
            "5000gf",
            "10000gf",
            "15000gf",
            "20000gf",
            "30000gf",
            "50000gf",
            "60000gf",
            "80000gf",
            "100000gf",
            "120000gf"});
            this.cbTurret.Location = new System.Drawing.Point(230, 89);
            this.cbTurret.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbTurret.Name = "cbTurret";
            this.cbTurret.Palette = this.kp1;
            this.cbTurret.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbTurret.Size = new System.Drawing.Size(150, 21);
            this.cbTurret.TabIndex = 206;
            // 
            // cbTurretAfterImpress
            // 
            this.cbTurretAfterImpress.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTurretAfterImpress.Location = new System.Drawing.Point(12, 41);
            this.cbTurretAfterImpress.Name = "cbTurretAfterImpress";
            this.cbTurretAfterImpress.Palette = this.kp1;
            this.cbTurretAfterImpress.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbTurretAfterImpress.Size = new System.Drawing.Size(132, 20);
            this.cbTurretAfterImpress.TabIndex = 23;
            this.cbTurretAfterImpress.Values.Text = "Turret After Impress";
            // 
            // cbMeasureAfterImpress
            // 
            this.cbMeasureAfterImpress.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMeasureAfterImpress.Location = new System.Drawing.Point(230, 41);
            this.cbMeasureAfterImpress.Name = "cbMeasureAfterImpress";
            this.cbMeasureAfterImpress.Palette = this.kp1;
            this.cbMeasureAfterImpress.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbMeasureAfterImpress.Size = new System.Drawing.Size(146, 20);
            this.cbMeasureAfterImpress.TabIndex = 22;
            this.cbMeasureAfterImpress.Values.Text = "Measure After Impress";
            // 
            // lbObjectiveForMeasure
            // 
            this.lbObjectiveForMeasure.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbObjectiveForMeasure.Location = new System.Drawing.Point(12, 89);
            this.lbObjectiveForMeasure.Name = "lbObjectiveForMeasure";
            this.lbObjectiveForMeasure.Palette = this.kp1;
            this.lbObjectiveForMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbObjectiveForMeasure.Size = new System.Drawing.Size(133, 20);
            this.lbObjectiveForMeasure.TabIndex = 21;
            this.lbObjectiveForMeasure.Values.Text = "Objective For Measure";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDefault);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 298);
            this.panel1.Name = "panel1";
            this.panel1.Palette = this.kp1;
            this.panel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.panel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.panel1.Size = new System.Drawing.Size(453, 58);
            this.panel1.TabIndex = 7;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(15, 16);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Palette = this.kp1;
            this.btnDefault.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnDefault.Size = new System.Drawing.Size(134, 37);
            this.btnDefault.TabIndex = 1;
            this.btnDefault.Values.Text = "Default";
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(155, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(134, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(295, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(134, 37);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AutoMeasureSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 372);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbAutoMeasure);
            this.Controls.Add(this.gbAutoMeasureCorrect);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "AutoMeasureSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.Text = "Auto Measure Correct";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoMeasureSettingForm_FormClosing);
            this.Load += new System.EventHandler(this.AutoMeasureSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasureCorrect.Panel)).EndInit();
            this.gbAutoMeasureCorrect.Panel.ResumeLayout(false);
            this.gbAutoMeasureCorrect.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasureCorrect)).EndInit();
            this.gbAutoMeasureCorrect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasure.Panel)).EndInit();
            this.gbAutoMeasure.Panel.ResumeLayout(false);
            this.gbAutoMeasure.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbAutoMeasure)).EndInit();
            this.gbAutoMeasure.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbTurret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		public AutoMeasureSettingForm(MainForm owner)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;

            this.owner = owner;
			trbMedianRadius.Value = this.owner.autoMeasureInfo.MeadianRadius;
			lbMedianRadius.Text = this.owner.autoMeasureInfo.MeadianRadius.ToString();
			trbThreshold.Value = this.owner.autoMeasureInfo.MaxGray;
			lbThresholdValue.Text = this.owner.autoMeasureInfo.MaxGray.ToString();
			HardnessTesterInfo hardnessTesterInfo = this.owner.hardnessTesterInfo;
			cbMeasureAfterImpress.Checked = hardnessTesterInfo.MeasureAfterImpress;
			cbTurretAfterImpress.Checked = hardnessTesterInfo.TurretAfterImpress;
			cbTurret.Items.Clear();
			foreach (TurretInfo turretInfo in hardnessTesterInfo.TurretInfoList)
			{
				if (!string.IsNullOrEmpty(turretInfo.Object))
				{
					cbTurret.Items.Add(turretInfo.Object);
				}
			}
			cbTurret.Text = hardnessTesterInfo.ObjectiveForMeasure;
		}

		private void trbThreshold_Scroll(object sender, EventArgs e)
		{
			lbThresholdValue.Text = trbThreshold.Value.ToString();
		}

		private void trbThreshold_MouseUp(object sender, MouseEventArgs e)
		{
			HVMeasure.MaxGray = trbThreshold.Value;
			HBMeasure.MaxGray = trbThreshold.Value;
			owner.AutoMeasure();
		}

		private void trbMedianRadius_Scroll(object sender, EventArgs e)
		{
			lbMedianRadius.Text = trbMedianRadius.Value.ToString();
		}

		private void trbMedianRadius_MouseUp(object sender, MouseEventArgs e)
		{
			HVMeasure.MeadianRadius = trbMedianRadius.Value;
			HBMeasure.MeadianRadius = trbMedianRadius.Value;
			owner.AutoMeasure();
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			if (owner.genericInfo.SoftwareSeries == SoftwareSeries.HBW)
			{
				trbMedianRadius.Value = 5;
				lbMedianRadius.Text = "5";
				trbThreshold.Value = 80;
				lbThresholdValue.Text = "80";
			}
			else
			{
				trbMedianRadius.Value = 1;
				lbMedianRadius.Text = "1";
				trbThreshold.Value = 110;
				lbThresholdValue.Text = "110";
			}
			HVMeasure.MaxGray = trbThreshold.Value;
			HBMeasure.MaxGray = trbThreshold.Value;
			HVMeasure.MeadianRadius = trbMedianRadius.Value;
			HBMeasure.MeadianRadius = trbMedianRadius.Value;
			owner.AutoMeasure();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void AutoMeasureSettingForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				owner.autoMeasureInfo.MaxGray = trbThreshold.Value;
				owner.autoMeasureInfo.MeadianRadius = trbMedianRadius.Value;
				ConfigFileManager.SaveAutoMeasureInfoConfigFile(owner.autoMeasureInfo);
				owner.hardnessTesterInfo.MeasureAfterImpress = cbMeasureAfterImpress.Checked;
				owner.hardnessTesterInfo.TurretAfterImpress = cbTurretAfterImpress.Checked;
				owner.hardnessTesterInfo.ObjectiveForMeasure = cbTurret.Text;
				if (owner.genericInfo.SoftwareSeries == SoftwareSeries.HV || owner.genericInfo.SoftwareSeries == SoftwareSeries.HK)
				{
					ConfigFileManager.HVSaveHardnessTesterConfigFile(owner.hardnessTesterInfo);
				}
				else
				{
					ConfigFileManager.HBWSaveHardnessTesterConfigFile(owner.hardnessTesterInfo);
				}
			}
			else
			{
				HVMeasure.MaxGray = owner.autoMeasureInfo.MaxGray;
				HBMeasure.MaxGray = trbThreshold.Value;
				HVMeasure.MeadianRadius = owner.autoMeasureInfo.MeadianRadius;
				HBMeasure.MeadianRadius = trbMedianRadius.Value;
			}
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_AutoMeasureSetting_Title;
			gbAutoMeasureCorrect.Text = ResourcesManager.Resources.R_AutoMeasureSetting_AutoMeasureCorrect;
			lbSmoothing.Text = ResourcesManager.Resources.R_AutoMeasureSetting_Smoothing;
			lbThreshold.Text = ResourcesManager.Resources.R_AutoMeasureSetting_Threshold;
			gbAutoMeasure.Text = ResourcesManager.Resources.R_AutoMeasureSetting_AutoMeasure;
			cbTurretAfterImpress.Text = ResourcesManager.Resources.R_AutoMeasureSetting_TurretAfterImpress;
			cbMeasureAfterImpress.Text = ResourcesManager.Resources.R_AutoMeasureSetting_MeasureAfterImpress;
			lbObjectiveForMeasure.Text = ResourcesManager.Resources.R_AutoMeasureSetting_ObjectiveForMeasure;
			btnDefault.Text = ResourcesManager.Resources.R_AutoMeasureSetting_Default;
			btnSave.Text = ResourcesManager.Resources.R_AutoMeasureSetting_Save;
			btnCancel.Text = ResourcesManager.Resources.R_AutoMeasureSetting_Cancel;
		}

        private void AutoMeasureSettingForm_Load(object sender, EventArgs e)
        {

        }

        private void trbThreshold_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
