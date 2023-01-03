using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Labtt.Communication.ZAxis;
using Labtt.Data;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class ZAxisSettingForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonLabel lbPulsePerMM;

		private KryptonTextBox tbPulsePerMM;

		private KryptonCheckBox cbReverseDirection;

		private KryptonCheckBox cbHasEmptyTrip;

		private KryptonLabel lbStepDistance;

		private KryptonTextBox tbStepDistance;

		private KryptonLabel lbUpwardEmptyTrip;

		private KryptonTextBox tbUpwardEmptyTrip;

		private KryptonLabel lbDownwardEmptyTrip;

		private KryptonTextBox tbDownwardEmptyTrip;

		private KryptonButton btnCancel;

		private KryptonButton btnSave;
        private KryptonPalette kp1;
        private ZAxisInfo zAxisInfo;

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
            this.lbPulsePerMM = new Krypton.Toolkit.KryptonLabel();
            this.tbPulsePerMM = new Krypton.Toolkit.KryptonTextBox();
            this.cbReverseDirection = new Krypton.Toolkit.KryptonCheckBox();
            this.cbHasEmptyTrip = new Krypton.Toolkit.KryptonCheckBox();
            this.lbStepDistance = new Krypton.Toolkit.KryptonLabel();
            this.tbStepDistance = new Krypton.Toolkit.KryptonTextBox();
            this.lbUpwardEmptyTrip = new Krypton.Toolkit.KryptonLabel();
            this.tbUpwardEmptyTrip = new Krypton.Toolkit.KryptonTextBox();
            this.lbDownwardEmptyTrip = new Krypton.Toolkit.KryptonLabel();
            this.tbDownwardEmptyTrip = new Krypton.Toolkit.KryptonTextBox();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // lbPulsePerMM
            // 
            this.lbPulsePerMM.Location = new System.Drawing.Point(16, 57);
            this.lbPulsePerMM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPulsePerMM.Name = "lbPulsePerMM";
            this.lbPulsePerMM.Palette = this.kp1;
            this.lbPulsePerMM.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbPulsePerMM.Size = new System.Drawing.Size(148, 20);
            this.lbPulsePerMM.TabIndex = 38;
            this.lbPulsePerMM.Values.Text = "Pulse Per MM(pulse/mm)";
            // 
            // tbPulsePerMM
            // 
            this.tbPulsePerMM.Location = new System.Drawing.Point(216, 54);
            this.tbPulsePerMM.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbPulsePerMM.Name = "tbPulsePerMM";
            this.tbPulsePerMM.Palette = this.kp1;
            this.tbPulsePerMM.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbPulsePerMM.Size = new System.Drawing.Size(87, 23);
            this.tbPulsePerMM.TabIndex = 39;
            // 
            // cbReverseDirection
            // 
            this.cbReverseDirection.Location = new System.Drawing.Point(30, 25);
            this.cbReverseDirection.Margin = new System.Windows.Forms.Padding(2);
            this.cbReverseDirection.Name = "cbReverseDirection";
            this.cbReverseDirection.Palette = this.kp1;
            this.cbReverseDirection.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbReverseDirection.Size = new System.Drawing.Size(119, 20);
            this.cbReverseDirection.TabIndex = 40;
            this.cbReverseDirection.Values.Text = "Reverse Direction";
            // 
            // cbHasEmptyTrip
            // 
            this.cbHasEmptyTrip.Location = new System.Drawing.Point(30, 135);
            this.cbHasEmptyTrip.Margin = new System.Windows.Forms.Padding(2);
            this.cbHasEmptyTrip.Name = "cbHasEmptyTrip";
            this.cbHasEmptyTrip.Palette = this.kp1;
            this.cbHasEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbHasEmptyTrip.Size = new System.Drawing.Size(106, 20);
            this.cbHasEmptyTrip.TabIndex = 41;
            this.cbHasEmptyTrip.Values.Text = "Has Empty Trip";
            this.cbHasEmptyTrip.CheckedChanged += new System.EventHandler(this.cbHasEmptyTrip_CheckedChanged);
            // 
            // lbStepDistance
            // 
            this.lbStepDistance.Location = new System.Drawing.Point(16, 88);
            this.lbStepDistance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStepDistance.Name = "lbStepDistance";
            this.lbStepDistance.Palette = this.kp1;
            this.lbStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbStepDistance.Size = new System.Drawing.Size(114, 20);
            this.lbStepDistance.TabIndex = 42;
            this.lbStepDistance.Values.Text = "Step Distance(mm)";
            // 
            // tbStepDistance
            // 
            this.tbStepDistance.Location = new System.Drawing.Point(216, 85);
            this.tbStepDistance.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbStepDistance.Name = "tbStepDistance";
            this.tbStepDistance.Palette = this.kp1;
            this.tbStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbStepDistance.Size = new System.Drawing.Size(87, 23);
            this.tbStepDistance.TabIndex = 43;
            // 
            // lbUpwardEmptyTrip
            // 
            this.lbUpwardEmptyTrip.Location = new System.Drawing.Point(16, 167);
            this.lbUpwardEmptyTrip.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbUpwardEmptyTrip.Name = "lbUpwardEmptyTrip";
            this.lbUpwardEmptyTrip.Palette = this.kp1;
            this.lbUpwardEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbUpwardEmptyTrip.Size = new System.Drawing.Size(144, 20);
            this.lbUpwardEmptyTrip.TabIndex = 44;
            this.lbUpwardEmptyTrip.Values.Text = "Upward Empty Trip(mm)";
            // 
            // tbUpwardEmptyTrip
            // 
            this.tbUpwardEmptyTrip.Location = new System.Drawing.Point(216, 164);
            this.tbUpwardEmptyTrip.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbUpwardEmptyTrip.Name = "tbUpwardEmptyTrip";
            this.tbUpwardEmptyTrip.Palette = this.kp1;
            this.tbUpwardEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbUpwardEmptyTrip.Size = new System.Drawing.Size(87, 23);
            this.tbUpwardEmptyTrip.TabIndex = 45;
            // 
            // lbDownwardEmptyTrip
            // 
            this.lbDownwardEmptyTrip.Location = new System.Drawing.Point(16, 199);
            this.lbDownwardEmptyTrip.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDownwardEmptyTrip.Name = "lbDownwardEmptyTrip";
            this.lbDownwardEmptyTrip.Palette = this.kp1;
            this.lbDownwardEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbDownwardEmptyTrip.Size = new System.Drawing.Size(168, 20);
            this.lbDownwardEmptyTrip.TabIndex = 46;
            this.lbDownwardEmptyTrip.Values.Text = "Downward Empty Trip>(mm)";
            // 
            // tbDownwardEmptyTrip
            // 
            this.tbDownwardEmptyTrip.Location = new System.Drawing.Point(216, 196);
            this.tbDownwardEmptyTrip.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbDownwardEmptyTrip.Name = "tbDownwardEmptyTrip";
            this.tbDownwardEmptyTrip.Palette = this.kp1;
            this.tbDownwardEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbDownwardEmptyTrip.Size = new System.Drawing.Size(87, 23);
            this.tbDownwardEmptyTrip.TabIndex = 47;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(191, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 49;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(30, 246);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 48;
            this.btnSave.Values.Text = "Confirm";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ZAxisSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 303);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbUpwardEmptyTrip);
            this.Controls.Add(this.tbUpwardEmptyTrip);
            this.Controls.Add(this.lbDownwardEmptyTrip);
            this.Controls.Add(this.tbDownwardEmptyTrip);
            this.Controls.Add(this.lbStepDistance);
            this.Controls.Add(this.tbStepDistance);
            this.Controls.Add(this.cbHasEmptyTrip);
            this.Controls.Add(this.cbReverseDirection);
            this.Controls.Add(this.lbPulsePerMM);
            this.Controls.Add(this.tbPulsePerMM);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZAxisSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ZAxis Setting";
            this.Load += new System.EventHandler(this.ZAxisSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public ZAxisSettingForm(ZAxisInfo zAxisInfo)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;


            this.zAxisInfo = zAxisInfo;
			if (this.zAxisInfo == null)
			{
				throw new NullReferenceException("自动聚焦信息为空！");
			}
		}

		private void ZAxisSettingForm_Load(object sender, EventArgs e)
		{
			cbReverseDirection.Checked = zAxisInfo.IsReverseDirection;
			tbPulsePerMM.Text = zAxisInfo.PulsePerMM.ToString();
			tbStepDistance.Text = zAxisInfo.StepDistance.ToString();
			cbHasEmptyTrip.Checked = zAxisInfo.HasEmptyTrip;
			tbUpwardEmptyTrip.Text = zAxisInfo.UpwardEmptyTrip.ToString();
			tbDownwardEmptyTrip.Text = zAxisInfo.DownwardEmptyTrip.ToString();
			tbUpwardEmptyTrip.Enabled = zAxisInfo.HasEmptyTrip;
			tbDownwardEmptyTrip.Enabled = zAxisInfo.HasEmptyTrip;
		}

		private void cbHasEmptyTrip_CheckedChanged(object sender, EventArgs e)
		{
			tbUpwardEmptyTrip.Enabled = cbHasEmptyTrip.Checked;
			tbDownwardEmptyTrip.Enabled = cbHasEmptyTrip.Checked;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				int pulsePerMM = int.Parse(tbPulsePerMM.Text);
				float stepDistance = float.Parse(tbStepDistance.Text);
				float upwardEmptyTrip = float.Parse(tbUpwardEmptyTrip.Text);
				float downwardEmptyTrip = float.Parse(tbDownwardEmptyTrip.Text);
				zAxisInfo.HasEmptyTrip = cbHasEmptyTrip.Checked;
				zAxisInfo.IsReverseDirection = cbReverseDirection.Checked;
				zAxisInfo.PulsePerMM = pulsePerMM;
				zAxisInfo.StepDistance = stepDistance;
				zAxisInfo.UpwardEmptyTrip = upwardEmptyTrip;
				zAxisInfo.DownwardEmptyTrip = downwardEmptyTrip;
				ConfigFileManager.SaveZAxisConfigFile(zAxisInfo);
				Dispose();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to save Z-axis data!");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_ZAxisSetting_Message_InputValueInvalid);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_ZAxisSetting_Title;
			cbReverseDirection.Text = ResourcesManager.Resources.R_ZAxisSetting_ReverseDirection;
			lbPulsePerMM.Text = ResourcesManager.Resources.R_ZAxisSetting_PulsePerMM;
			lbStepDistance.Text = ResourcesManager.Resources.R_ZAxisSetting_StepDistance;
			cbHasEmptyTrip.Text = ResourcesManager.Resources.R_ZAxisSetting_HasEmptyTrip;
			lbUpwardEmptyTrip.Text = ResourcesManager.Resources.R_ZAxisSetting_UpwardEmptyTrip;
			lbDownwardEmptyTrip.Text = ResourcesManager.Resources.R_ZAxisSetting_DownwardEmptyTrip;
			btnSave.Text = ResourcesManager.Resources.R_ZAxisSetting_Title;
			btnCancel.Text = ResourcesManager.Resources.R_ZAxisSetting_Cancel;
		}
	}
}
