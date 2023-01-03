using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Labtt.Communication.MasterControl;
using Labtt.Data;
using MessageBoxExApp;
using Krypton.Toolkit;

namespace AIO_Client
{

	public class ForceCorrectForm : KryptonForm
	{
		private MainForm owner = null;

		private List<ForceSet> forceList = null;

		private ForceSet forceSet = null;

		private IContainer components = null;

		private KryptonButton btnCancel;

		private KryptonButton btnApply;

		private KryptonGroupBox gbForceCoefficient;

		private KryptonTextBox tbLargerForceCoefficient;

		private KryptonLabel lbLargerForceCoefficient;

		private KryptonTextBox tbLesserForceCoefficient;

		private KryptonLabel lbLesserForceCoefficient;

		private KryptonGroupBox gbForceCalibration;

		private KryptonTextBox tbRealForce;

		private KryptonLabel lbActualForce;

		private KryptonLabel lbStandardForce;

		private KryptonComboBox cbForce;

		private KryptonLabel label5;

		private KryptonLabel label4;
        private KryptonPalette kp1;
        private KryptonLabel label7;

		public ForceCorrectForm(MainForm owner)
		{
			InitializeComponent();
			LoadLanguageResources();
			this.owner = owner;

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;

        }

		private void ForceCorrectForm_Load(object sender, EventArgs e)
		{
			if (owner == null)
			{
				return;
			}
			HardnessTesterInfo hardnessTesterInfo = owner.hardnessTesterInfo;
			tbLargerForceCoefficient.Text = hardnessTesterInfo.LargerForceCoefficient.ToString();
			tbLesserForceCoefficient.Text = hardnessTesterInfo.LesserForceCoefficient.ToString();
			forceList = new List<ForceSet>();
			hardnessTesterInfo.ForceList.ForEach(delegate (ForceSet info)
			{
				ForceSet item = new ForceSet
				{
					TestForce = info.TestForce,
					RealForce = info.RealForce,
					Enabled = info.Enabled
				};
				forceList.Add(item);
			});
			cbForce.Items.Clear();
			foreach (ForceSet forceSet in hardnessTesterInfo.ForceList)
			{
				if (forceSet.Enabled)
				{
					cbForce.Items.Add(forceSet.TestForce);
				}
			}
			string scaleName = owner.Force;
			ScaleSet scaleSet = hardnessTesterInfo.ScaleList.FirstOrDefault((ScaleSet x) => x.ScaleName == scaleName);
			if (scaleSet != null)
			{
				cbForce.Text = scaleSet.TestForce;
			}
		}

		private void cbForce_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				forceSet = forceList.First((ForceSet info) => info.TestForce == cbForce.Text);
				tbRealForce.Text = forceSet.RealForce.ToString();
			}
			catch
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_ForceCorrect_Message_NoSuchForce);
			}
		}

		private void tbRealForce_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(tbRealForce.Text))
				{
					forceSet.RealForce = float.Parse(tbRealForce.Text);
				}
			}
			catch
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_ForceCorrect_Message_PleaseEnterNumber);
			}
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				owner.hardnessTesterInfo.LargerForceCoefficient = float.Parse(tbLargerForceCoefficient.Text);
				owner.hardnessTesterInfo.LesserForceCoefficient = float.Parse(tbLesserForceCoefficient.Text);
				owner.hardnessTesterInfo.ForceList.Clear();
				forceList.ForEach(delegate (ForceSet info)
				{
					ForceSet item = new ForceSet
					{
						TestForce = info.TestForce,
						RealForce = info.RealForce,
						Enabled = info.Enabled
					};
					owner.hardnessTesterInfo.ForceList.Add(item);
				});
				owner.hardnessTester.SetForceCoefficient(owner.hardnessTesterInfo.LargerForceCoefficient, owner.hardnessTesterInfo.LesserForceCoefficient);
				if (owner.genericInfo.SoftwareSeries == SoftwareSeries.HV || owner.genericInfo.SoftwareSeries == SoftwareSeries.HK)
				{
					ConfigFileManager.HVSaveHardnessTesterConfigFile(owner.hardnessTesterInfo);
				}
				else
				{
					ConfigFileManager.HBWSaveHardnessTesterConfigFile(owner.hardnessTesterInfo);
				}
				MsgBox.ShowInfo(ResourcesManager.Resources.R_FroceCorrect_Message_SaveSuccessful);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to save force correction data");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_ForceCorrect_Message_SaveFailed);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_ForceCorrect_Title;
			gbForceCoefficient.Text = ResourcesManager.Resources.R_ForceCorrect_ForceCoefficient;
			lbLargerForceCoefficient.Text = ResourcesManager.Resources.R_ForceCorrect_LaggerForceCoefficient;
			lbLesserForceCoefficient.Text = ResourcesManager.Resources.R_ForceCorrect_LesserForceCoefficient;
			gbForceCalibration.Text = ResourcesManager.Resources.R_ForceCorrect_ForceCalibration;
			lbStandardForce.Text = ResourcesManager.Resources.R_ForceCorrect_StandardForce;
			lbActualForce.Text = ResourcesManager.Resources.R_ForceCorrect_ActualForce;
			btnApply.Text = ResourcesManager.Resources.R_ForceCorrect_Apply;
			btnCancel.Text = ResourcesManager.Resources.R_ForceCorrect_Cancel;
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
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnApply = new Krypton.Toolkit.KryptonButton();
            this.gbForceCoefficient = new Krypton.Toolkit.KryptonGroupBox();
            this.label5 = new Krypton.Toolkit.KryptonLabel();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.tbLesserForceCoefficient = new Krypton.Toolkit.KryptonTextBox();
            this.lbLesserForceCoefficient = new Krypton.Toolkit.KryptonLabel();
            this.tbLargerForceCoefficient = new Krypton.Toolkit.KryptonTextBox();
            this.lbLargerForceCoefficient = new Krypton.Toolkit.KryptonLabel();
            this.gbForceCalibration = new Krypton.Toolkit.KryptonGroupBox();
            this.label7 = new Krypton.Toolkit.KryptonLabel();
            this.cbForce = new Krypton.Toolkit.KryptonComboBox();
            this.tbRealForce = new Krypton.Toolkit.KryptonTextBox();
            this.lbActualForce = new Krypton.Toolkit.KryptonLabel();
            this.lbStandardForce = new Krypton.Toolkit.KryptonLabel();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCoefficient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCoefficient.Panel)).BeginInit();
            this.gbForceCoefficient.Panel.SuspendLayout();
            this.gbForceCoefficient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCalibration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCalibration.Panel)).BeginInit();
            this.gbForceCalibration.Panel.SuspendLayout();
            this.gbForceCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbForce)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(240, 319);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(140, 35);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(42, 319);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Palette = this.kp1;
            this.btnApply.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnApply.Size = new System.Drawing.Size(140, 35);
            this.btnApply.TabIndex = 38;
            this.btnApply.Values.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gbForceCoefficient
            // 
            this.gbForceCoefficient.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbForceCoefficient.Location = new System.Drawing.Point(12, 12);
            this.gbForceCoefficient.Name = "gbForceCoefficient";
            this.gbForceCoefficient.Palette = this.kp1;
            this.gbForceCoefficient.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbForceCoefficient.Panel
            // 
            this.gbForceCoefficient.Panel.Controls.Add(this.label5);
            this.gbForceCoefficient.Panel.Controls.Add(this.label4);
            this.gbForceCoefficient.Panel.Controls.Add(this.tbLesserForceCoefficient);
            this.gbForceCoefficient.Panel.Controls.Add(this.lbLesserForceCoefficient);
            this.gbForceCoefficient.Panel.Controls.Add(this.tbLargerForceCoefficient);
            this.gbForceCoefficient.Panel.Controls.Add(this.lbLargerForceCoefficient);
            this.gbForceCoefficient.Size = new System.Drawing.Size(395, 144);
            this.gbForceCoefficient.TabIndex = 40;
            this.gbForceCoefficient.Values.Heading = "Force Coefficient";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(338, 72);
            this.label5.Name = "label5";
            this.label5.Palette = this.kp1;
            this.label5.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label5.Size = new System.Drawing.Size(21, 20);
            this.label5.TabIndex = 17;
            this.label5.Values.Text = "%";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(338, 19);
            this.label4.Name = "label4";
            this.label4.Palette = this.kp1;
            this.label4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label4.Size = new System.Drawing.Size(21, 20);
            this.label4.TabIndex = 16;
            this.label4.Values.Text = "%";
            // 
            // tbLesserForceCoefficient
            // 
            this.tbLesserForceCoefficient.Location = new System.Drawing.Point(201, 72);
            this.tbLesserForceCoefficient.Name = "tbLesserForceCoefficient";
            this.tbLesserForceCoefficient.Palette = this.kp1;
            this.tbLesserForceCoefficient.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLesserForceCoefficient.Size = new System.Drawing.Size(131, 23);
            this.tbLesserForceCoefficient.TabIndex = 15;
            // 
            // lbLesserForceCoefficient
            // 
            this.lbLesserForceCoefficient.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLesserForceCoefficient.Location = new System.Drawing.Point(20, 72);
            this.lbLesserForceCoefficient.Name = "lbLesserForceCoefficient";
            this.lbLesserForceCoefficient.Palette = this.kp1;
            this.lbLesserForceCoefficient.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLesserForceCoefficient.Size = new System.Drawing.Size(44, 20);
            this.lbLesserForceCoefficient.TabIndex = 14;
            this.lbLesserForceCoefficient.Values.Text = "Lesser";
            // 
            // tbLargerForceCoefficient
            // 
            this.tbLargerForceCoefficient.Location = new System.Drawing.Point(201, 19);
            this.tbLargerForceCoefficient.Name = "tbLargerForceCoefficient";
            this.tbLargerForceCoefficient.Palette = this.kp1;
            this.tbLargerForceCoefficient.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLargerForceCoefficient.Size = new System.Drawing.Size(131, 23);
            this.tbLargerForceCoefficient.TabIndex = 13;
            // 
            // lbLargerForceCoefficient
            // 
            this.lbLargerForceCoefficient.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLargerForceCoefficient.Location = new System.Drawing.Point(20, 19);
            this.lbLargerForceCoefficient.Name = "lbLargerForceCoefficient";
            this.lbLargerForceCoefficient.Palette = this.kp1;
            this.lbLargerForceCoefficient.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLargerForceCoefficient.Size = new System.Drawing.Size(48, 20);
            this.lbLargerForceCoefficient.TabIndex = 12;
            this.lbLargerForceCoefficient.Values.Text = "Lagger";
            // 
            // gbForceCalibration
            // 
            this.gbForceCalibration.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbForceCalibration.Location = new System.Drawing.Point(12, 162);
            this.gbForceCalibration.Name = "gbForceCalibration";
            this.gbForceCalibration.Palette = this.kp1;
            this.gbForceCalibration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbForceCalibration.Panel
            // 
            this.gbForceCalibration.Panel.Controls.Add(this.label7);
            this.gbForceCalibration.Panel.Controls.Add(this.cbForce);
            this.gbForceCalibration.Panel.Controls.Add(this.tbRealForce);
            this.gbForceCalibration.Panel.Controls.Add(this.lbActualForce);
            this.gbForceCalibration.Panel.Controls.Add(this.lbStandardForce);
            this.gbForceCalibration.Size = new System.Drawing.Size(395, 144);
            this.gbForceCalibration.TabIndex = 41;
            this.gbForceCalibration.Values.Heading = "Force Calibration";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(338, 71);
            this.label7.Name = "label7";
            this.label7.Palette = this.kp1;
            this.label7.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label7.Size = new System.Drawing.Size(28, 20);
            this.label7.TabIndex = 18;
            this.label7.Values.Text = "kgf";
            // 
            // cbForce
            // 
            this.cbForce.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForce.DropDownWidth = 131;
            this.cbForce.FormattingEnabled = true;
            this.cbForce.IntegralHeight = false;
            this.cbForce.Location = new System.Drawing.Point(201, 17);
            this.cbForce.Name = "cbForce";
            this.cbForce.Palette = this.kp1;
            this.cbForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbForce.Size = new System.Drawing.Size(131, 21);
            this.cbForce.TabIndex = 16;
            this.cbForce.SelectedIndexChanged += new System.EventHandler(this.cbForce_SelectedIndexChanged);
            // 
            // tbRealForce
            // 
            this.tbRealForce.Location = new System.Drawing.Point(201, 71);
            this.tbRealForce.Name = "tbRealForce";
            this.tbRealForce.Palette = this.kp1;
            this.tbRealForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbRealForce.Size = new System.Drawing.Size(131, 23);
            this.tbRealForce.TabIndex = 15;
            this.tbRealForce.TextChanged += new System.EventHandler(this.tbRealForce_TextChanged);
            // 
            // lbActualForce
            // 
            this.lbActualForce.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbActualForce.Location = new System.Drawing.Point(20, 71);
            this.lbActualForce.Name = "lbActualForce";
            this.lbActualForce.Palette = this.kp1;
            this.lbActualForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbActualForce.Size = new System.Drawing.Size(78, 20);
            this.lbActualForce.TabIndex = 14;
            this.lbActualForce.Values.Text = "Actual Force";
            // 
            // lbStandardForce
            // 
            this.lbStandardForce.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStandardForce.Location = new System.Drawing.Point(20, 18);
            this.lbStandardForce.Name = "lbStandardForce";
            this.lbStandardForce.Palette = this.kp1;
            this.lbStandardForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbStandardForce.Size = new System.Drawing.Size(93, 20);
            this.lbStandardForce.TabIndex = 12;
            this.lbStandardForce.Values.Text = "Standard Force";
            // 
            // ForceCorrectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 371);
            this.Controls.Add(this.gbForceCalibration);
            this.Controls.Add(this.gbForceCoefficient);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.MaximizeBox = false;
            this.Name = "ForceCorrectForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Force Correct";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ForceCorrectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCoefficient.Panel)).EndInit();
            this.gbForceCoefficient.Panel.ResumeLayout(false);
            this.gbForceCoefficient.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCoefficient)).EndInit();
            this.gbForceCoefficient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCalibration.Panel)).EndInit();
            this.gbForceCalibration.Panel.ResumeLayout(false);
            this.gbForceCalibration.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbForceCalibration)).EndInit();
            this.gbForceCalibration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbForce)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
