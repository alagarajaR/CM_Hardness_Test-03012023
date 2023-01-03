using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using Labtt.Communication;
using Labtt.Data;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class SerialPortSettingForm : KryptonForm
	{
		private MainForm owner = null;

		private List<SerialPortInfo> portsInfo = null;

		private SerialPortInfo mainPortInfo = null;

		private SerialPortInfo platformPortInfo = null;

		private SerialPortInfo zAxisPortInfo = null;

		private SerialPortInfo micrometerPortInfo = null;

		private IContainer components = null;

		private KryptonPanel panel1;

		private KryptonButton btnSave;

		private KryptonButton btnCancel;

		private KryptonGroupBox gbMicrometer;

		private KryptonLabel lbMicrometerPortName;

		private KryptonComboBox cbMicrometerPortName;

		private KryptonGroupBox gbZAxis;

		private KryptonLabel lbZAxisPortName;

		private KryptonComboBox cbZAxisPortName;

		private KryptonGroupBox gbPlatform;

		private KryptonLabel lbPlatformPortName;

		private KryptonComboBox cbPlatformPortName;

		private KryptonGroupBox gbMain;

		private KryptonLabel lbMainPortName;
        private KryptonPalette kp1;
        private KryptonComboBox cbMainPortName;

		public SerialPortSettingForm(MainForm owner)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;


            this.owner = owner;
			portsInfo = this.owner.portsInfo;
		}

		private void SerialPortSettingForm_Load(object sender, EventArgs e)
		{
			bool micrometerOn = owner.genericInfo.MicrometerOn;
			switch (owner.genericInfo.SoftwareVersion)
			{
				case SoftwareVersion.SE:
					if (!micrometerOn)
					{
						Dispose();
					}
					gbMain.Visible = false;
					gbPlatform.Visible = false;
					gbZAxis.Visible = false;
					gbMicrometer.Visible = true;
					base.Height = gbMicrometer.Height + panel1.Height + 40;
					break;
				case SoftwareVersion.Basic_Weight:
				case SoftwareVersion.Basic_Sensor:
					gbMain.Enabled = true;
					gbPlatform.Visible = false;
					gbZAxis.Visible = false;
					gbMicrometer.Visible = micrometerOn;
					base.Height = gbMain.Height + (micrometerOn ? gbMicrometer.Height : 0) + panel1.Height + 40;
					break;
				case SoftwareVersion.Semi_Weight:
				case SoftwareVersion.Semi_Sensor:
					gbMain.Enabled = true;
					gbPlatform.Visible = true;
					gbZAxis.Visible = false;
					gbMicrometer.Visible = false;
					base.Height = gbMain.Height + gbPlatform.Height + panel1.Height + 40;
					break;
				case SoftwareVersion.Full_Weight:
				case SoftwareVersion.Full_Sensor:
					gbMain.Enabled = true;
					gbPlatform.Visible = true;
					gbZAxis.Visible = true;
					gbMicrometer.Visible = false;
					base.Height = gbMain.Height + gbPlatform.Height + gbZAxis.Height + panel1.Height + 40;
					break;
			}
			string[] portNames = null;
			try
			{
				portNames = SerialPort.GetPortNames();
				cbMainPortName.Items.AddRange(portNames);
				cbPlatformPortName.Items.AddRange(portNames);
				cbZAxisPortName.Items.AddRange(portNames);
				cbMicrometerPortName.Items.AddRange(portNames);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to get serial port list！");
			}
			mainPortInfo = owner.portsInfo.First((SerialPortInfo x) => x.Identifier == "Main");
			cbMainPortName.Text = mainPortInfo.PortName;
			platformPortInfo = owner.portsInfo.First((SerialPortInfo x) => x.Identifier == "X/Y");
			cbPlatformPortName.Text = platformPortInfo.PortName;
			zAxisPortInfo = owner.portsInfo.First((SerialPortInfo x) => x.Identifier == "Z");
			cbZAxisPortName.Text = zAxisPortInfo.PortName;
			micrometerPortInfo = owner.portsInfo.First((SerialPortInfo x) => x.Identifier == "Micrometer");
			cbMicrometerPortName.Text = micrometerPortInfo.PortName;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				mainPortInfo.PortName = cbMainPortName.Text;
				platformPortInfo.PortName = cbPlatformPortName.Text;
				zAxisPortInfo.PortName = cbZAxisPortName.Text;
				micrometerPortInfo.PortName = cbMicrometerPortName.Text;
				ConfigFileManager.SaveSerialPortConfigFile(portsInfo);
			}
			catch (Exception ex)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_SerialPortSetting_Message_SaveFailed);
				Logger.Error(ex, "Failed to save serial port information !");
				return;
			}
			MsgBox.ShowInfo(ResourcesManager.Resources.R_SerialPortSetting_Message_SaveSuccessful);
			Dispose();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_SerialPortSetting_Title;
			lbMainPortName.Text = ResourcesManager.Resources.R_SerialPortSetting_PortName;
			lbPlatformPortName.Text = ResourcesManager.Resources.R_SerialPortSetting_PortName;
			lbZAxisPortName.Text = ResourcesManager.Resources.R_SerialPortSetting_PortName;
			gbMicrometer.Text = ResourcesManager.Resources.R_SerialPortSetting_Micrometer;
			lbMicrometerPortName.Text = ResourcesManager.Resources.R_SerialPortSetting_PortName;
			btnSave.Text = ResourcesManager.Resources.R_SerialPortSetting_Save;
			btnCancel.Text = ResourcesManager.Resources.R_SerialPortSetting_Cancel;
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
            this.panel1 = new Krypton.Toolkit.KryptonPanel();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.gbMicrometer = new Krypton.Toolkit.KryptonGroupBox();
            this.lbMicrometerPortName = new Krypton.Toolkit.KryptonLabel();
            this.cbMicrometerPortName = new Krypton.Toolkit.KryptonComboBox();
            this.gbZAxis = new Krypton.Toolkit.KryptonGroupBox();
            this.lbZAxisPortName = new Krypton.Toolkit.KryptonLabel();
            this.cbZAxisPortName = new Krypton.Toolkit.KryptonComboBox();
            this.gbPlatform = new Krypton.Toolkit.KryptonGroupBox();
            this.lbPlatformPortName = new Krypton.Toolkit.KryptonLabel();
            this.cbPlatformPortName = new Krypton.Toolkit.KryptonComboBox();
            this.gbMain = new Krypton.Toolkit.KryptonGroupBox();
            this.lbMainPortName = new Krypton.Toolkit.KryptonLabel();
            this.cbMainPortName = new Krypton.Toolkit.KryptonComboBox();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbMicrometer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMicrometer.Panel)).BeginInit();
            this.gbMicrometer.Panel.SuspendLayout();
            this.gbMicrometer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbMicrometerPortName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis.Panel)).BeginInit();
            this.gbZAxis.Panel.SuspendLayout();
            this.gbZAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbZAxisPortName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform.Panel)).BeginInit();
            this.gbPlatform.Panel.SuspendLayout();
            this.gbPlatform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbPlatformPortName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMain.Panel)).BeginInit();
            this.gbMain.Panel.SuspendLayout();
            this.gbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbMainPortName)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 316);
            this.panel1.Name = "panel1";
            this.panel1.Palette = this.kp1;
            this.panel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.panel1.Size = new System.Drawing.Size(391, 58);
            this.panel1.TabIndex = 21;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(39, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.TabIndex = 12;
            this.btnSave.Values.Text = "Confirm";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbMicrometer
            // 
            this.gbMicrometer.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMicrometer.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbMicrometer.Location = new System.Drawing.Point(0, 237);
            this.gbMicrometer.Name = "gbMicrometer";
            this.gbMicrometer.Palette = this.kp1;
            this.gbMicrometer.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbMicrometer.Panel
            // 
            this.gbMicrometer.Panel.Controls.Add(this.lbMicrometerPortName);
            this.gbMicrometer.Panel.Controls.Add(this.cbMicrometerPortName);
            this.gbMicrometer.Size = new System.Drawing.Size(391, 79);
            this.gbMicrometer.TabIndex = 20;
            this.gbMicrometer.Values.Heading = "Micrometer";
            // 
            // lbMicrometerPortName
            // 
            this.lbMicrometerPortName.Location = new System.Drawing.Point(17, 18);
            this.lbMicrometerPortName.Name = "lbMicrometerPortName";
            this.lbMicrometerPortName.Palette = this.kp1;
            this.lbMicrometerPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMicrometerPortName.Size = new System.Drawing.Size(69, 20);
            this.lbMicrometerPortName.TabIndex = 10;
            this.lbMicrometerPortName.Values.Text = "Port Name";
            // 
            // cbMicrometerPortName
            // 
            this.cbMicrometerPortName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMicrometerPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMicrometerPortName.DropDownWidth = 134;
            this.cbMicrometerPortName.FormattingEnabled = true;
            this.cbMicrometerPortName.IntegralHeight = false;
            this.cbMicrometerPortName.Location = new System.Drawing.Point(216, 19);
            this.cbMicrometerPortName.Name = "cbMicrometerPortName";
            this.cbMicrometerPortName.Palette = this.kp1;
            this.cbMicrometerPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbMicrometerPortName.Size = new System.Drawing.Size(134, 21);
            this.cbMicrometerPortName.TabIndex = 11;
            // 
            // gbZAxis
            // 
            this.gbZAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbZAxis.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbZAxis.Location = new System.Drawing.Point(0, 158);
            this.gbZAxis.Name = "gbZAxis";
            this.gbZAxis.Palette = this.kp1;
            this.gbZAxis.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbZAxis.Panel
            // 
            this.gbZAxis.Panel.Controls.Add(this.lbZAxisPortName);
            this.gbZAxis.Panel.Controls.Add(this.cbZAxisPortName);
            this.gbZAxis.Size = new System.Drawing.Size(391, 79);
            this.gbZAxis.TabIndex = 19;
            this.gbZAxis.Values.Heading = "Z";
            // 
            // lbZAxisPortName
            // 
            this.lbZAxisPortName.Location = new System.Drawing.Point(17, 15);
            this.lbZAxisPortName.Name = "lbZAxisPortName";
            this.lbZAxisPortName.Palette = this.kp1;
            this.lbZAxisPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbZAxisPortName.Size = new System.Drawing.Size(69, 20);
            this.lbZAxisPortName.TabIndex = 10;
            this.lbZAxisPortName.Values.Text = "Port Name";
            // 
            // cbZAxisPortName
            // 
            this.cbZAxisPortName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbZAxisPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZAxisPortName.DropDownWidth = 134;
            this.cbZAxisPortName.FormattingEnabled = true;
            this.cbZAxisPortName.IntegralHeight = false;
            this.cbZAxisPortName.Location = new System.Drawing.Point(216, 16);
            this.cbZAxisPortName.Name = "cbZAxisPortName";
            this.cbZAxisPortName.Palette = this.kp1;
            this.cbZAxisPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbZAxisPortName.Size = new System.Drawing.Size(134, 21);
            this.cbZAxisPortName.TabIndex = 11;
            // 
            // gbPlatform
            // 
            this.gbPlatform.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPlatform.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbPlatform.Location = new System.Drawing.Point(0, 79);
            this.gbPlatform.Name = "gbPlatform";
            this.gbPlatform.Palette = this.kp1;
            this.gbPlatform.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbPlatform.Panel
            // 
            this.gbPlatform.Panel.Controls.Add(this.lbPlatformPortName);
            this.gbPlatform.Panel.Controls.Add(this.cbPlatformPortName);
            this.gbPlatform.Size = new System.Drawing.Size(391, 79);
            this.gbPlatform.TabIndex = 17;
            this.gbPlatform.Values.Heading = "X/Y";
            // 
            // lbPlatformPortName
            // 
            this.lbPlatformPortName.Location = new System.Drawing.Point(17, 16);
            this.lbPlatformPortName.Name = "lbPlatformPortName";
            this.lbPlatformPortName.Palette = this.kp1;
            this.lbPlatformPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbPlatformPortName.Size = new System.Drawing.Size(119, 20);
            this.lbPlatformPortName.TabIndex = 10;
            this.lbPlatformPortName.Values.Text = "Platform Port Name";
            // 
            // cbPlatformPortName
            // 
            this.cbPlatformPortName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbPlatformPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlatformPortName.DropDownWidth = 134;
            this.cbPlatformPortName.FormattingEnabled = true;
            this.cbPlatformPortName.IntegralHeight = false;
            this.cbPlatformPortName.Location = new System.Drawing.Point(216, 17);
            this.cbPlatformPortName.Name = "cbPlatformPortName";
            this.cbPlatformPortName.Palette = this.kp1;
            this.cbPlatformPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbPlatformPortName.Size = new System.Drawing.Size(134, 21);
            this.cbPlatformPortName.TabIndex = 11;
            // 
            // gbMain
            // 
            this.gbMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMain.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbMain.Location = new System.Drawing.Point(0, 0);
            this.gbMain.Name = "gbMain";
            this.gbMain.Palette = this.kp1;
            this.gbMain.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbMain.Panel
            // 
            this.gbMain.Panel.Controls.Add(this.lbMainPortName);
            this.gbMain.Panel.Controls.Add(this.cbMainPortName);
            this.gbMain.Size = new System.Drawing.Size(391, 79);
            this.gbMain.TabIndex = 18;
            this.gbMain.Values.Heading = "Main";
            // 
            // lbMainPortName
            // 
            this.lbMainPortName.Location = new System.Drawing.Point(17, 16);
            this.lbMainPortName.Name = "lbMainPortName";
            this.lbMainPortName.Palette = this.kp1;
            this.lbMainPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMainPortName.Size = new System.Drawing.Size(100, 20);
            this.lbMainPortName.TabIndex = 10;
            this.lbMainPortName.Values.Text = "Main Port Name";
            // 
            // cbMainPortName
            // 
            this.cbMainPortName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMainPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMainPortName.DropDownWidth = 134;
            this.cbMainPortName.FormattingEnabled = true;
            this.cbMainPortName.IntegralHeight = false;
            this.cbMainPortName.Location = new System.Drawing.Point(216, 17);
            this.cbMainPortName.Name = "cbMainPortName";
            this.cbMainPortName.Palette = this.kp1;
            this.cbMainPortName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbMainPortName.Size = new System.Drawing.Size(134, 21);
            this.cbMainPortName.TabIndex = 11;
            // 
            // SerialPortSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 377);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbMicrometer);
            this.Controls.Add(this.gbZAxis);
            this.Controls.Add(this.gbPlatform);
            this.Controls.Add(this.gbMain);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialPortSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial Port Setting";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SerialPortSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbMicrometer.Panel)).EndInit();
            this.gbMicrometer.Panel.ResumeLayout(false);
            this.gbMicrometer.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbMicrometer)).EndInit();
            this.gbMicrometer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbMicrometerPortName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis.Panel)).EndInit();
            this.gbZAxis.Panel.ResumeLayout(false);
            this.gbZAxis.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis)).EndInit();
            this.gbZAxis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbZAxisPortName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform.Panel)).EndInit();
            this.gbPlatform.Panel.ResumeLayout(false);
            this.gbPlatform.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform)).EndInit();
            this.gbPlatform.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbPlatformPortName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMain.Panel)).EndInit();
            this.gbMain.Panel.ResumeLayout(false);
            this.gbMain.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbMain)).EndInit();
            this.gbMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbMainPortName)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
