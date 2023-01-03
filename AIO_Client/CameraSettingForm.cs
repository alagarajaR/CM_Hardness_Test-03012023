using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Labtt.Data;
using Labtt.Service;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class CameraSettingForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonLabel lbAnalogGain;

		private KryptonLabel lbExposureTime;

		private KryptonLabel label2;

		private KryptonButton btnCancel;

		private KryptonButton btnSave;

		private KryptonLabel lbAnalogGainValue;

		private KryptonLabel lbExposureTimeValue;

		private DVPCameraService cameraService = null;

		private CameraInfo cameraInfo = null;

		private float initAnalogGain = 0f;
        private KryptonTrackBar trbAnalogGain;
        private KryptonTrackBar trbExposureTime;
        private KryptonPalette kp1;
        private double initExposureTime = 0.0;

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
            this.lbAnalogGain = new Krypton.Toolkit.KryptonLabel();
            this.lbExposureTime = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.lbAnalogGainValue = new Krypton.Toolkit.KryptonLabel();
            this.lbExposureTimeValue = new Krypton.Toolkit.KryptonLabel();
            this.trbAnalogGain = new Krypton.Toolkit.KryptonTrackBar();
            this.trbExposureTime = new Krypton.Toolkit.KryptonTrackBar();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // lbAnalogGain
            // 
            this.lbAnalogGain.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAnalogGain.Location = new System.Drawing.Point(12, 23);
            this.lbAnalogGain.Name = "lbAnalogGain";
            this.lbAnalogGain.Palette = this.kp1;
            this.lbAnalogGain.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbAnalogGain.Size = new System.Drawing.Size(90, 20);
            this.lbAnalogGain.TabIndex = 11;
            this.lbAnalogGain.Values.Text = "Analog Gain：";
            // 
            // lbExposureTime
            // 
            this.lbExposureTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExposureTime.Location = new System.Drawing.Point(12, 61);
            this.lbExposureTime.Name = "lbExposureTime";
            this.lbExposureTime.Palette = this.kp1;
            this.lbExposureTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbExposureTime.Size = new System.Drawing.Size(103, 20);
            this.lbExposureTime.TabIndex = 14;
            this.lbExposureTime.Values.Text = "Exposure Time：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(381, 65);
            this.label2.Name = "label2";
            this.label2.Palette = this.kp1;
            this.label2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label2.Size = new System.Drawing.Size(26, 20);
            this.label2.TabIndex = 16;
            this.label2.Values.Text = "ms";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(268, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(119, 35);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(87, 121);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(119, 35);
            this.btnSave.TabIndex = 17;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbAnalogGainValue
            // 
            this.lbAnalogGainValue.Location = new System.Drawing.Point(358, 27);
            this.lbAnalogGainValue.Name = "lbAnalogGainValue";
            this.lbAnalogGainValue.Palette = this.kp1;
            this.lbAnalogGainValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbAnalogGainValue.Size = new System.Drawing.Size(40, 20);
            this.lbAnalogGainValue.TabIndex = 19;
            this.lbAnalogGainValue.Values.Text = "1.000";
            // 
            // lbExposureTimeValue
            // 
            this.lbExposureTimeValue.Location = new System.Drawing.Point(358, 65);
            this.lbExposureTimeValue.Name = "lbExposureTimeValue";
            this.lbExposureTimeValue.Palette = this.kp1;
            this.lbExposureTimeValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbExposureTimeValue.Size = new System.Drawing.Size(24, 20);
            this.lbExposureTimeValue.TabIndex = 20;
            this.lbExposureTimeValue.Values.Text = "10";
            // 
            // trbAnalogGain
            // 
            this.trbAnalogGain.BackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.trbAnalogGain.LargeChange = 25;
            this.trbAnalogGain.Location = new System.Drawing.Point(141, 27);
            this.trbAnalogGain.Maximum = 8000;
            this.trbAnalogGain.Minimum = 1000;
            this.trbAnalogGain.Name = "trbAnalogGain";
            this.trbAnalogGain.Palette = this.kp1;
            this.trbAnalogGain.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.trbAnalogGain.Size = new System.Drawing.Size(211, 21);
            this.trbAnalogGain.SmallChange = 25;
            this.trbAnalogGain.TabIndex = 22;
            this.trbAnalogGain.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbAnalogGain.Value = 1000;
            this.trbAnalogGain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbAnalogGain_MouseUp);
            // 
            // trbExposureTime
            // 
            this.trbExposureTime.BackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.trbExposureTime.Location = new System.Drawing.Point(141, 65);
            this.trbExposureTime.Maximum = 200;
            this.trbExposureTime.Minimum = 10;
            this.trbExposureTime.Name = "trbExposureTime";
            this.trbExposureTime.Palette = this.kp1;
            this.trbExposureTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.trbExposureTime.Size = new System.Drawing.Size(211, 21);
            this.trbExposureTime.SmallChange = 5;
            this.trbExposureTime.TabIndex = 23;
            this.trbExposureTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbExposureTime.Value = 10;
            this.trbExposureTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbExposureTime_MouseUp);
            // 
            // CameraSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 179);
            this.Controls.Add(this.trbExposureTime);
            this.Controls.Add(this.trbAnalogGain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbExposureTimeValue);
            this.Controls.Add(this.lbAnalogGainValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbExposureTime);
            this.Controls.Add(this.lbAnalogGain);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera Setting";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraSettingForm_FormClosing);
            this.Load += new System.EventHandler(this.CameraSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public CameraSettingForm(MainForm owner)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;

            cameraService = owner.cameraService;
			cameraInfo = owner.cameraInfo;
		}

		private void CameraSettingForm_Load(object sender, EventArgs e)
		{
			try
			{
				initAnalogGain = cameraService.AnalogGain;
				initExposureTime = cameraService.ExposureTime;
				trbAnalogGain.Value = (int)(initAnalogGain * 1000f);
				lbAnalogGainValue.Text = initAnalogGain.ToString("F3");
				trbExposureTime.Value = (int)(initExposureTime / 1000.0 + 0.5);
				lbExposureTimeValue.Text = ((int)(initExposureTime / 1000.0 + 0.5)).ToString();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "An exception occurred while loading the camera settings interface！");
			}
		}

		private void trbAnalogGain_MouseUp(object sender, MouseEventArgs e)
		{
			float currentAnalogGain = (float)trbAnalogGain.Value / 1000f;
			cameraService.AnalogGain = currentAnalogGain;
			currentAnalogGain = cameraService.AnalogGain;
			trbAnalogGain.Value = (int)(currentAnalogGain * 1000f);
			lbAnalogGainValue.Text = currentAnalogGain.ToString("F3");
		}

		private void trbExposureTime_MouseUp(object sender, MouseEventArgs e)
		{
			int currentExposureTime = trbExposureTime.Value * 1000;
			cameraService.ExposureTime = currentExposureTime;
			currentExposureTime = (int)(cameraService.ExposureTime / 1000.0 + 0.5);
			trbExposureTime.Value = currentExposureTime;
			lbExposureTimeValue.Text = currentExposureTime.ToString();
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

		private void CameraSettingForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				cameraInfo.AnalogGain = cameraService.AnalogGain;
				cameraInfo.ExposureTime = cameraService.ExposureTime;
			}
			else
			{
				cameraService.AnalogGain = initAnalogGain;
				cameraService.ExposureTime = initExposureTime;
			}
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_CameraSetting_Title;
			lbAnalogGain.Text = ResourcesManager.Resources.R_CameraSetting_AnalogGain;
			lbExposureTime.Text = ResourcesManager.Resources.R_CameraSetting_ExposureTime;
			btnSave.Text = ResourcesManager.Resources.R_CameraSetting_Save;
			btnCancel.Text = ResourcesManager.Resources.R_CameraSetting_Cancel;
		}
	}
}
