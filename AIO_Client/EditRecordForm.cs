using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Labtt.Data;
using Labtt.Meterage;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class EditRecordForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonButton btnApply;

		private KryptonTextBox tbHardness;

		private KryptonLabel lbHardness;

		private KryptonTextBox tbDAvg;

		private KryptonLabel label4;

		private KryptonTextBox tbD2;

		private KryptonLabel label3;

		private KryptonTextBox tbD1;

		private KryptonLabel label2;

		private KryptonButton btnCancel;

		private KryptonTextBox tbConvertValue;

		private KryptonLabel lbConvertValue;

		private KryptonLabel lbConvertType;

		private KryptonTextBox tbQualified;

		private KryptonLabel lbQualified;

		private KryptonComboBox cbConvertType;

		private KryptonLabel lbMeasureTime;

		private KryptonDateTimePicker dtpMeaTime;

		private KryptonTextBox tbIndex;

		private KryptonLabel lbIndex;

		private KryptonTextBox tbHardnessType;

		private KryptonLabel lbHardnessType;

		private MainForm owner;
        private KryptonPalette kp1;
        private MeasureRecord record = null;

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
            this.btnApply = new Krypton.Toolkit.KryptonButton();
            this.tbHardness = new Krypton.Toolkit.KryptonTextBox();
            this.lbHardness = new Krypton.Toolkit.KryptonLabel();
            this.tbDAvg = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.tbD2 = new Krypton.Toolkit.KryptonTextBox();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.tbD1 = new Krypton.Toolkit.KryptonTextBox();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.tbConvertValue = new Krypton.Toolkit.KryptonTextBox();
            this.lbConvertValue = new Krypton.Toolkit.KryptonLabel();
            this.lbConvertType = new Krypton.Toolkit.KryptonLabel();
            this.tbQualified = new Krypton.Toolkit.KryptonTextBox();
            this.lbQualified = new Krypton.Toolkit.KryptonLabel();
            this.cbConvertType = new Krypton.Toolkit.KryptonComboBox();
            this.lbMeasureTime = new Krypton.Toolkit.KryptonLabel();
            this.dtpMeaTime = new Krypton.Toolkit.KryptonDateTimePicker();
            this.tbIndex = new Krypton.Toolkit.KryptonTextBox();
            this.lbIndex = new Krypton.Toolkit.KryptonLabel();
            this.tbHardnessType = new Krypton.Toolkit.KryptonTextBox();
            this.lbHardnessType = new Krypton.Toolkit.KryptonLabel();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cbConvertType)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(219, 237);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Palette = this.kp1;
            this.btnApply.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnApply.Size = new System.Drawing.Size(120, 35);
            this.btnApply.TabIndex = 36;
            this.btnApply.Values.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tbHardness
            // 
            this.tbHardness.Location = new System.Drawing.Point(608, 62);
            this.tbHardness.Margin = new System.Windows.Forms.Padding(4);
            this.tbHardness.Name = "tbHardness";
            this.tbHardness.Palette = this.kp1;
            this.tbHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbHardness.ReadOnly = true;
            this.tbHardness.Size = new System.Drawing.Size(160, 23);
            this.tbHardness.TabIndex = 35;
            this.tbHardness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHardness.TextChanged += new System.EventHandler(this.CalculateConvertHardness);
            // 
            // lbHardness
            // 
            this.lbHardness.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHardness.Location = new System.Drawing.Point(400, 65);
            this.lbHardness.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHardness.Name = "lbHardness";
            this.lbHardness.Palette = this.kp1;
            this.lbHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHardness.Size = new System.Drawing.Size(61, 20);
            this.lbHardness.TabIndex = 34;
            this.lbHardness.Values.Text = "Hardness";
            // 
            // tbDAvg
            // 
            this.tbDAvg.Location = new System.Drawing.Point(232, 182);
            this.tbDAvg.Margin = new System.Windows.Forms.Padding(4);
            this.tbDAvg.Name = "tbDAvg";
            this.tbDAvg.Palette = this.kp1;
            this.tbDAvg.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbDAvg.ReadOnly = true;
            this.tbDAvg.Size = new System.Drawing.Size(160, 23);
            this.tbDAvg.TabIndex = 33;
            this.tbDAvg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(24, 185);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Palette = this.kp1;
            this.label4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 32;
            this.label4.Values.Text = "Davg(um)";
            // 
            // tbD2
            // 
            this.tbD2.Location = new System.Drawing.Point(232, 142);
            this.tbD2.Margin = new System.Windows.Forms.Padding(4);
            this.tbD2.Name = "tbD2";
            this.tbD2.Palette = this.kp1;
            this.tbD2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbD2.Size = new System.Drawing.Size(160, 23);
            this.tbD2.TabIndex = 31;
            this.tbD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbD2.TextChanged += new System.EventHandler(this.tbDValue_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Palette = this.kp1;
            this.label3.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 30;
            this.label3.Values.Text = "D2(um)";
            // 
            // tbD1
            // 
            this.tbD1.Location = new System.Drawing.Point(232, 102);
            this.tbD1.Margin = new System.Windows.Forms.Padding(4);
            this.tbD1.Name = "tbD1";
            this.tbD1.Palette = this.kp1;
            this.tbD1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbD1.Size = new System.Drawing.Size(160, 23);
            this.tbD1.TabIndex = 29;
            this.tbD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbD1.TextChanged += new System.EventHandler(this.tbDValue_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Palette = this.kp1;
            this.label2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 28;
            this.label2.Values.Text = "D1(um)";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(444, 237);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // tbConvertValue
            // 
            this.tbConvertValue.Location = new System.Drawing.Point(608, 182);
            this.tbConvertValue.Margin = new System.Windows.Forms.Padding(4);
            this.tbConvertValue.Name = "tbConvertValue";
            this.tbConvertValue.Palette = this.kp1;
            this.tbConvertValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbConvertValue.ReadOnly = true;
            this.tbConvertValue.Size = new System.Drawing.Size(160, 23);
            this.tbConvertValue.TabIndex = 43;
            this.tbConvertValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbConvertValue
            // 
            this.lbConvertValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbConvertValue.Location = new System.Drawing.Point(400, 185);
            this.lbConvertValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbConvertValue.Name = "lbConvertValue";
            this.lbConvertValue.Palette = this.kp1;
            this.lbConvertValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbConvertValue.Size = new System.Drawing.Size(87, 20);
            this.lbConvertValue.TabIndex = 42;
            this.lbConvertValue.Values.Text = "Convert Value";
            // 
            // lbConvertType
            // 
            this.lbConvertType.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbConvertType.Location = new System.Drawing.Point(400, 145);
            this.lbConvertType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbConvertType.Name = "lbConvertType";
            this.lbConvertType.Palette = this.kp1;
            this.lbConvertType.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbConvertType.Size = new System.Drawing.Size(83, 20);
            this.lbConvertType.TabIndex = 40;
            this.lbConvertType.Values.Text = "Convert Type";
            // 
            // tbQualified
            // 
            this.tbQualified.Location = new System.Drawing.Point(608, 102);
            this.tbQualified.Margin = new System.Windows.Forms.Padding(4);
            this.tbQualified.Name = "tbQualified";
            this.tbQualified.Palette = this.kp1;
            this.tbQualified.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbQualified.Size = new System.Drawing.Size(160, 23);
            this.tbQualified.TabIndex = 39;
            this.tbQualified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbQualified
            // 
            this.lbQualified.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQualified.Location = new System.Drawing.Point(400, 105);
            this.lbQualified.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbQualified.Name = "lbQualified";
            this.lbQualified.Palette = this.kp1;
            this.lbQualified.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbQualified.Size = new System.Drawing.Size(60, 20);
            this.lbQualified.TabIndex = 38;
            this.lbQualified.Values.Text = "Qualified";
            // 
            // cbConvertType
            // 
            this.cbConvertType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbConvertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConvertType.DropDownWidth = 160;
            this.cbConvertType.FormattingEnabled = true;
            this.cbConvertType.IntegralHeight = false;
            this.cbConvertType.Items.AddRange(new object[] {
            "HV",
            "HK",
            "HBW",
            "HRA",
            "HRB",
            "HRC",
            "HRD",
            "HRF",
            "HR15N",
            "HR30N",
            "HR45N",
            "HR15T",
            "HR30T",
            "HR45T"});
            this.cbConvertType.Location = new System.Drawing.Point(607, 142);
            this.cbConvertType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbConvertType.Name = "cbConvertType";
            this.cbConvertType.Palette = this.kp1;
            this.cbConvertType.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbConvertType.Size = new System.Drawing.Size(160, 21);
            this.cbConvertType.TabIndex = 46;
            this.cbConvertType.SelectedIndexChanged += new System.EventHandler(this.CalculateConvertHardness);
            // 
            // lbMeasureTime
            // 
            this.lbMeasureTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMeasureTime.Location = new System.Drawing.Point(24, 65);
            this.lbMeasureTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMeasureTime.Name = "lbMeasureTime";
            this.lbMeasureTime.Palette = this.kp1;
            this.lbMeasureTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMeasureTime.Size = new System.Drawing.Size(87, 20);
            this.lbMeasureTime.TabIndex = 26;
            this.lbMeasureTime.Values.Text = "Measure Time";
            // 
            // dtpMeaTime
            // 
            this.dtpMeaTime.CustomFormat = "HH:mm:ss";
            this.dtpMeaTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMeaTime.Location = new System.Drawing.Point(232, 62);
            this.dtpMeaTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpMeaTime.Name = "dtpMeaTime";
            this.dtpMeaTime.Palette = this.kp1;
            this.dtpMeaTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.dtpMeaTime.Size = new System.Drawing.Size(160, 21);
            this.dtpMeaTime.TabIndex = 27;
            // 
            // tbIndex
            // 
            this.tbIndex.Location = new System.Drawing.Point(232, 21);
            this.tbIndex.Margin = new System.Windows.Forms.Padding(4);
            this.tbIndex.Name = "tbIndex";
            this.tbIndex.Palette = this.kp1;
            this.tbIndex.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbIndex.ReadOnly = true;
            this.tbIndex.Size = new System.Drawing.Size(160, 23);
            this.tbIndex.TabIndex = 48;
            this.tbIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbIndex
            // 
            this.lbIndex.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbIndex.Location = new System.Drawing.Point(24, 25);
            this.lbIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Palette = this.kp1;
            this.lbIndex.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbIndex.Size = new System.Drawing.Size(40, 20);
            this.lbIndex.TabIndex = 47;
            this.lbIndex.Values.Text = "Index";
            // 
            // tbHardnessType
            // 
            this.tbHardnessType.Location = new System.Drawing.Point(608, 21);
            this.tbHardnessType.Margin = new System.Windows.Forms.Padding(4);
            this.tbHardnessType.Name = "tbHardnessType";
            this.tbHardnessType.Palette = this.kp1;
            this.tbHardnessType.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbHardnessType.ReadOnly = true;
            this.tbHardnessType.Size = new System.Drawing.Size(160, 23);
            this.tbHardnessType.TabIndex = 50;
            this.tbHardnessType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbHardnessType
            // 
            this.lbHardnessType.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHardnessType.Location = new System.Drawing.Point(400, 25);
            this.lbHardnessType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbHardnessType.Name = "lbHardnessType";
            this.lbHardnessType.Palette = this.kp1;
            this.lbHardnessType.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHardnessType.Size = new System.Drawing.Size(91, 20);
            this.lbHardnessType.TabIndex = 49;
            this.lbHardnessType.Values.Text = "Hardness Type";
            // 
            // EditRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 298);
            this.Controls.Add(this.tbHardnessType);
            this.Controls.Add(this.lbHardnessType);
            this.Controls.Add(this.tbIndex);
            this.Controls.Add(this.lbIndex);
            this.Controls.Add(this.cbConvertType);
            this.Controls.Add(this.tbConvertValue);
            this.Controls.Add(this.lbConvertValue);
            this.Controls.Add(this.lbConvertType);
            this.Controls.Add(this.tbQualified);
            this.Controls.Add(this.lbQualified);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.tbHardness);
            this.Controls.Add(this.lbHardness);
            this.Controls.Add(this.tbDAvg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbD2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbD1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpMeaTime);
            this.Controls.Add(this.lbMeasureTime);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditRecordForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Record";
            this.Load += new System.EventHandler(this.EditRecordForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbConvertType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public EditRecordForm(MainForm owner, MeasureRecord record)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;


            this.owner = owner;
			this.record = record;
		}

		private void EditRecordForm_Load(object sender, EventArgs e)
		{
			tbIndex.Text = record.Index.ToString();
			tbHardnessType.Text = record.HardnessType;
			if (record.MeasureTime > dtpMeaTime.MinDate && record.MeasureTime < dtpMeaTime.MaxDate)
			{
				dtpMeaTime.Value = record.MeasureTime;
			}
			tbD1.Text = record.D1.ToString("F3");
			tbD2.Text = record.D2.ToString("F3");
			tbDAvg.Text = record.DAvg.ToString("F3");
			tbHardness.Text = record.Hardness.ToString("F2");
			tbQualified.Text = record.Qualified;
			cbConvertType.Text = record.ConvertType;
			tbConvertValue.Text = record.ConvertValue.ToString("F2");
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				DateTime time = dtpMeaTime.Value;
				double d1 = double.Parse(tbD1.Text);
				double d2 = double.Parse(tbD2.Text);
				double davg = double.Parse(tbDAvg.Text);
				double hardness = double.Parse(tbHardness.Text);
				double convertHardness = double.Parse(tbConvertValue.Text);
				record.MeasureTime = time;
				record.D1 = d1;
				record.D2 = d2;
				record.DAvg = davg;
				record.HardnessType = tbHardnessType.Text;
				record.Hardness = hardness;
				record.Qualified = tbQualified.Text;
				record.ConvertType = cbConvertType.Text;
				record.ConvertValue = convertHardness;
			}
			catch (Exception ex)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_EditRecord_Message_InputDataInvalid);
				Logger.Error(ex, "An error occurred while modifying the measurement record");
			}
			base.DialogResult = DialogResult.OK;
		}

		private void tbDValue_TextChanged(object sender, EventArgs e)
		{
			try
			{
				double d1 = double.Parse(tbD1.Text);
				double d2 = double.Parse(tbD2.Text);
				double davg = (d1 + d2) / 2.0;
				tbDAvg.Text = davg.ToString("F3");
				double hardness = 0.0;
				if (tbHardnessType.Text == "HV")
				{
					float kgf = float.Parse(owner.Force.Replace("kgf", ""));
					hardness = HardnessFormula.CalculateHVHardness(kgf, davg / 1000.0);
				}
				else if (tbHardnessType.Text == "HK")
				{
					float kgf = float.Parse(owner.Force.Replace("kgf", ""));
					hardness = HardnessFormula.CalculateHKHardness(kgf, d1 / 1000.0);
				}
				else if (tbHardnessType.Text == "HBW")
				{
					string forceAndDiameter = owner.Force.Substring(3);
					string[] tempArr = forceAndDiameter.Split('/');
					float diameter = float.Parse(tempArr[0]);
					float kgf = float.Parse(tempArr[1]);
					hardness = HardnessFormula.CalculateHBWHardness(kgf, diameter, davg / 1000.0);
				}
				tbHardness.Text = hardness.ToString("F2");
			}
			catch (Exception)
			{
				tbDAvg.Text = "--";
			}
		}

		private void CalculateConvertHardness(object sender, EventArgs e)
		{
			try
			{
				double hardness = double.Parse(tbHardness.Text);
				double convertHardness = HardnessConverter.Convert(hardness, "HBW", cbConvertType.Text);
				tbConvertValue.Text = convertHardness.ToString("F2");
			}
			catch (Exception)
			{
				tbConvertValue.Text = "--";
			}
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_EditRecord_Title;
			lbIndex.Text = ResourcesManager.Resources.R_EditRecord_Index;
			lbHardnessType.Text = ResourcesManager.Resources.R_EditRecord_HardnessType;
			lbMeasureTime.Text = ResourcesManager.Resources.R_EditRecord_MeasureTime;
			lbHardness.Text = ResourcesManager.Resources.R_EditRecord_Hardness;
			lbQualified.Text = ResourcesManager.Resources.R_EditRecord_Qualified;
			lbConvertType.Text = ResourcesManager.Resources.R_EditRecord_ConvertType;
			lbConvertValue.Text = ResourcesManager.Resources.R_EditRecord_ConvertValue;
			btnApply.Text = ResourcesManager.Resources.R_EditRecord_Apply;
			btnCancel.Text = ResourcesManager.Resources.R_EditRecord_Cancel;
		}
	}
}
