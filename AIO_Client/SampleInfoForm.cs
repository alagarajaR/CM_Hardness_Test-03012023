using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class SampleInfoForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonButton btnCancel;

		private KryptonButton btnSave;

		private KryptonTextBox tbInspectionCompany;

		private KryptonLabel lbInspectionDate;

		private KryptonDateTimePicker dtpInspectionDate;

		private KryptonTextBox tbReviewer;

		private KryptonLabel lbReviewer;

		private KryptonTextBox tbMinValue;

		private KryptonLabel lbMinValue;

		private KryptonTextBox tbSampleSn;

		private KryptonLabel lbSampleSn;

		private KryptonTextBox tbTester;

		private KryptonLabel lbTester;

		private KryptonTextBox tbMaxValue;

		private KryptonLabel lbMaxValue;

		private KryptonTextBox tbSampleName;

		private KryptonLabel lbSampleName;
        private KryptonPalette kp1;
        private KryptonLabel lbInspectionCompany;

		public SampleInfo SampleInfo { get; set; }

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
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.tbInspectionCompany = new Krypton.Toolkit.KryptonTextBox();
            this.lbInspectionDate = new Krypton.Toolkit.KryptonLabel();
            this.dtpInspectionDate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.tbReviewer = new Krypton.Toolkit.KryptonTextBox();
            this.lbReviewer = new Krypton.Toolkit.KryptonLabel();
            this.tbMinValue = new Krypton.Toolkit.KryptonTextBox();
            this.lbMinValue = new Krypton.Toolkit.KryptonLabel();
            this.tbSampleSn = new Krypton.Toolkit.KryptonTextBox();
            this.lbSampleSn = new Krypton.Toolkit.KryptonLabel();
            this.tbTester = new Krypton.Toolkit.KryptonTextBox();
            this.lbTester = new Krypton.Toolkit.KryptonLabel();
            this.tbMaxValue = new Krypton.Toolkit.KryptonTextBox();
            this.lbMaxValue = new Krypton.Toolkit.KryptonLabel();
            this.tbSampleName = new Krypton.Toolkit.KryptonTextBox();
            this.lbSampleName = new Krypton.Toolkit.KryptonLabel();
            this.lbInspectionCompany = new Krypton.Toolkit.KryptonLabel();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(423, 203);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(154, 42);
            this.btnCancel.TabIndex = 55;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(142, 203);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(154, 42);
            this.btnSave.TabIndex = 54;
            this.btnSave.Values.Text = "Confirm";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbInspectionCompany
            // 
            this.tbInspectionCompany.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbInspectionCompany.Location = new System.Drawing.Point(188, 110);
            this.tbInspectionCompany.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbInspectionCompany.Name = "tbInspectionCompany";
            this.tbInspectionCompany.Palette = this.kp1;
            this.tbInspectionCompany.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbInspectionCompany.Size = new System.Drawing.Size(144, 23);
            this.tbInspectionCompany.TabIndex = 53;
            // 
            // lbInspectionDate
            // 
            this.lbInspectionDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInspectionDate.Location = new System.Drawing.Point(338, 110);
            this.lbInspectionDate.Name = "lbInspectionDate";
            this.lbInspectionDate.Palette = this.kp1;
            this.lbInspectionDate.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbInspectionDate.Size = new System.Drawing.Size(96, 20);
            this.lbInspectionDate.TabIndex = 52;
            this.lbInspectionDate.Values.Text = "Inspection Date";
            // 
            // dtpInspectionDate
            // 
            this.dtpInspectionDate.CustomFormat = "yyyy-MM-dd";
            this.dtpInspectionDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.dtpInspectionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInspectionDate.Location = new System.Drawing.Point(512, 110);
            this.dtpInspectionDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpInspectionDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtpInspectionDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpInspectionDate.Name = "dtpInspectionDate";
            this.dtpInspectionDate.Palette = this.kp1;
            this.dtpInspectionDate.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.dtpInspectionDate.Size = new System.Drawing.Size(144, 21);
            this.dtpInspectionDate.TabIndex = 51;
            // 
            // tbReviewer
            // 
            this.tbReviewer.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbReviewer.Location = new System.Drawing.Point(512, 150);
            this.tbReviewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbReviewer.Name = "tbReviewer";
            this.tbReviewer.Palette = this.kp1;
            this.tbReviewer.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReviewer.Size = new System.Drawing.Size(144, 23);
            this.tbReviewer.TabIndex = 50;
            // 
            // lbReviewer
            // 
            this.lbReviewer.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbReviewer.Location = new System.Drawing.Point(338, 149);
            this.lbReviewer.Name = "lbReviewer";
            this.lbReviewer.Palette = this.kp1;
            this.lbReviewer.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbReviewer.Size = new System.Drawing.Size(59, 20);
            this.lbReviewer.TabIndex = 49;
            this.lbReviewer.Values.Text = "Reviewer";
            // 
            // tbMinValue
            // 
            this.tbMinValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbMinValue.Location = new System.Drawing.Point(188, 70);
            this.tbMinValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMinValue.Name = "tbMinValue";
            this.tbMinValue.Palette = this.kp1;
            this.tbMinValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbMinValue.Size = new System.Drawing.Size(144, 23);
            this.tbMinValue.TabIndex = 48;
            // 
            // lbMinValue
            // 
            this.lbMinValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMinValue.Location = new System.Drawing.Point(14, 68);
            this.lbMinValue.Name = "lbMinValue";
            this.lbMinValue.Palette = this.kp1;
            this.lbMinValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMinValue.Size = new System.Drawing.Size(65, 20);
            this.lbMinValue.TabIndex = 47;
            this.lbMinValue.Values.Text = "Min Value";
            // 
            // tbSampleSn
            // 
            this.tbSampleSn.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbSampleSn.Location = new System.Drawing.Point(512, 30);
            this.tbSampleSn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbSampleSn.Name = "tbSampleSn";
            this.tbSampleSn.Palette = this.kp1;
            this.tbSampleSn.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbSampleSn.Size = new System.Drawing.Size(144, 23);
            this.tbSampleSn.TabIndex = 46;
            // 
            // lbSampleSn
            // 
            this.lbSampleSn.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSampleSn.Location = new System.Drawing.Point(338, 30);
            this.lbSampleSn.Name = "lbSampleSn";
            this.lbSampleSn.Palette = this.kp1;
            this.lbSampleSn.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSampleSn.Size = new System.Drawing.Size(68, 20);
            this.lbSampleSn.TabIndex = 45;
            this.lbSampleSn.Values.Text = "Sample Sn";
            // 
            // tbTester
            // 
            this.tbTester.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbTester.Location = new System.Drawing.Point(188, 150);
            this.tbTester.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbTester.Name = "tbTester";
            this.tbTester.Palette = this.kp1;
            this.tbTester.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbTester.Size = new System.Drawing.Size(144, 23);
            this.tbTester.TabIndex = 44;
            // 
            // lbTester
            // 
            this.lbTester.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTester.Location = new System.Drawing.Point(14, 149);
            this.lbTester.Name = "lbTester";
            this.lbTester.Palette = this.kp1;
            this.lbTester.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbTester.Size = new System.Drawing.Size(44, 20);
            this.lbTester.TabIndex = 43;
            this.lbTester.Values.Text = "Tester";
            // 
            // tbMaxValue
            // 
            this.tbMaxValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbMaxValue.Location = new System.Drawing.Point(512, 70);
            this.tbMaxValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMaxValue.Name = "tbMaxValue";
            this.tbMaxValue.Palette = this.kp1;
            this.tbMaxValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbMaxValue.Size = new System.Drawing.Size(144, 23);
            this.tbMaxValue.TabIndex = 42;
            // 
            // lbMaxValue
            // 
            this.lbMaxValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMaxValue.Location = new System.Drawing.Point(338, 68);
            this.lbMaxValue.Name = "lbMaxValue";
            this.lbMaxValue.Palette = this.kp1;
            this.lbMaxValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMaxValue.Size = new System.Drawing.Size(67, 20);
            this.lbMaxValue.TabIndex = 41;
            this.lbMaxValue.Values.Text = "Max Value";
            // 
            // tbSampleName
            // 
            this.tbSampleName.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.tbSampleName.Location = new System.Drawing.Point(188, 30);
            this.tbSampleName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbSampleName.Name = "tbSampleName";
            this.tbSampleName.Palette = this.kp1;
            this.tbSampleName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbSampleName.Size = new System.Drawing.Size(144, 23);
            this.tbSampleName.TabIndex = 40;
            // 
            // lbSampleName
            // 
            this.lbSampleName.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSampleName.Location = new System.Drawing.Point(14, 30);
            this.lbSampleName.Name = "lbSampleName";
            this.lbSampleName.Palette = this.kp1;
            this.lbSampleName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSampleName.Size = new System.Drawing.Size(87, 20);
            this.lbSampleName.TabIndex = 39;
            this.lbSampleName.Values.Text = "Sample Name";
            // 
            // lbInspectionCompany
            // 
            this.lbInspectionCompany.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInspectionCompany.Location = new System.Drawing.Point(14, 110);
            this.lbInspectionCompany.Name = "lbInspectionCompany";
            this.lbInspectionCompany.Palette = this.kp1;
            this.lbInspectionCompany.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbInspectionCompany.Size = new System.Drawing.Size(122, 20);
            this.lbInspectionCompany.TabIndex = 38;
            this.lbInspectionCompany.Values.Text = "Inspection Company";
            // 
            // SampleInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbInspectionCompany);
            this.Controls.Add(this.lbInspectionDate);
            this.Controls.Add(this.dtpInspectionDate);
            this.Controls.Add(this.tbReviewer);
            this.Controls.Add(this.lbReviewer);
            this.Controls.Add(this.tbMinValue);
            this.Controls.Add(this.lbMinValue);
            this.Controls.Add(this.tbSampleSn);
            this.Controls.Add(this.lbSampleSn);
            this.Controls.Add(this.tbTester);
            this.Controls.Add(this.lbTester);
            this.Controls.Add(this.tbMaxValue);
            this.Controls.Add(this.lbMaxValue);
            this.Controls.Add(this.tbSampleName);
            this.Controls.Add(this.lbSampleName);
            this.Controls.Add(this.lbInspectionCompany);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SampleInfoForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sample Info";
            this.Load += new System.EventHandler(this.SampleInfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public SampleInfoForm(SampleInfo sampleInfo)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;


            SampleInfo = sampleInfo;
		}

		private void SampleInfoForm_Load(object sender, EventArgs e)
		{
			if (SampleInfo != null)
			{
				tbSampleName.Text = SampleInfo.SampleName;
				tbSampleSn.Text = SampleInfo.SampleSn;
				tbMaxValue.Text = SampleInfo.HardnessH.ToString();
				tbMinValue.Text = SampleInfo.HardnessL.ToString();
				tbInspectionCompany.Text = SampleInfo.InspectionUnit;
				dtpInspectionDate.Value = SampleInfo.InspectionDate;
				tbTester.Text = SampleInfo.Tester;
				tbReviewer.Text = SampleInfo.Reviewer;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (!double.TryParse(tbMaxValue.Text, out var hardnessH) || !double.TryParse(tbMinValue.Text, out var hardnessL))
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_SampleInfo_Message_MinValueAndMaxValueMustBeNumber);
				return;
			}
			if (SampleInfo == null)
			{
				SampleInfo = new SampleInfo();
			}
			SampleInfo.SampleName = tbSampleName.Text;
			SampleInfo.SampleSn = tbSampleSn.Text;
			SampleInfo.HardnessH = hardnessH;
			SampleInfo.HardnessL = hardnessL;
			SampleInfo.InspectionUnit = tbInspectionCompany.Text;
			SampleInfo.InspectionDate = dtpInspectionDate.Value;
			SampleInfo.Tester = tbTester.Text;
			SampleInfo.Reviewer = tbReviewer.Text;
			base.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_SampleInfo_Title;
			lbSampleName.Text = ResourcesManager.Resources.R_SampleInfo_SampleName;
			lbSampleSn.Text = ResourcesManager.Resources.R_SampleInfo_SampleSn;
			lbMinValue.Text = ResourcesManager.Resources.R_SampleInfo_MinValue;
			lbMaxValue.Text = ResourcesManager.Resources.R_SampleInfo_MaxValue;
			lbInspectionCompany.Text = ResourcesManager.Resources.R_SampleInfo_InspectionCompany;
			lbInspectionDate.Text = ResourcesManager.Resources.R_SampleInfo_InspectionData;
			lbTester.Text = ResourcesManager.Resources.R_SampleInfo_Tester;
			lbReviewer.Text = ResourcesManager.Resources.R_SampleInfo_Reviewer;
			btnSave.Text = ResourcesManager.Resources.R_SampleInfo_Save;
			btnCancel.Text = ResourcesManager.Resources.R_SampleInfo_Cancel;
		}
	}
}
