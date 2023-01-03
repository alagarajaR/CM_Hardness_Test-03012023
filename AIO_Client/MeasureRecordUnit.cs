using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Labtt.Data;

namespace AIO_Client
{

	public class MeasureRecordUnit : UserControl
	{
		private MeasureRecord record = null;

		private IContainer components = null;

		private PictureBox box;

		private Label lbIndex;

		private Label label1;

		private Label lbHardness;

		private Label lbHV;

		private Button btnGo;

		public MeasureRecord Record => record;

		public event EventHandler OnButtonClick;

		public MeasureRecordUnit(MeasureRecord record)
		{
			InitializeComponent();
			if (record == null)
			{
				throw new Exception("测量记录为空！");
			}
			this.record = record;
			lbIndex.Text = record.Index.ToString();
			lbHardness.Text = record.Hardness.ToString("F1");
			box.ImageLocation = record.MeasuredImagePath;
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			if (this.OnButtonClick != null)
			{
				this.OnButtonClick(record, e);
			}
		}

		private void box_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists(record.MeasuredImagePath))
				{
					Process.Start(box.ImageLocation);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Can't view the image！");
			}
		}

		public override void Refresh()
		{
			lbIndex.Text = record.Index.ToString();
			lbHardness.Text = record.Hardness.ToString("F1");
			box.ImageLocation = record.MeasuredImagePath;
			Invalidate();
		}

		public void EnalbleGoButton(bool enabled)
		{
			btnGo.Enabled = enabled;
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
			this.box = new System.Windows.Forms.PictureBox();
			this.lbIndex = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbHardness = new System.Windows.Forms.Label();
			this.lbHV = new System.Windows.Forms.Label();
			this.btnGo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)this.box).BeginInit();
			base.SuspendLayout();
			this.box.Location = new System.Drawing.Point(4, 4);
			this.box.Margin = new System.Windows.Forms.Padding(4);
			this.box.Name = "box";
			this.box.Size = new System.Drawing.Size(272, 204);
			this.box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.box.TabIndex = 1;
			this.box.TabStop = false;
			this.box.DoubleClick += new System.EventHandler(box_DoubleClick);
			this.lbIndex.Location = new System.Drawing.Point(36, 220);
			this.lbIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbIndex.Name = "lbIndex";
			this.lbIndex.Size = new System.Drawing.Size(43, 24);
			this.lbIndex.TabIndex = 9;
			this.lbIndex.Text = "0";
			this.label1.Location = new System.Drawing.Point(4, 220);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 24);
			this.label1.TabIndex = 8;
			this.label1.Text = "#";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbHardness.Location = new System.Drawing.Point(135, 220);
			this.lbHardness.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbHardness.Name = "lbHardness";
			this.lbHardness.Size = new System.Drawing.Size(70, 24);
			this.lbHardness.TabIndex = 7;
			this.lbHardness.Text = "0";
			this.lbHV.Location = new System.Drawing.Point(87, 220);
			this.lbHV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbHV.Name = "lbHV";
			this.lbHV.Size = new System.Drawing.Size(40, 24);
			this.lbHV.TabIndex = 6;
			this.lbHV.Text = "HV";
			this.lbHV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGo.Location = new System.Drawing.Point(224, 215);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(52, 34);
			this.btnGo.TabIndex = 10;
			this.btnGo.Text = "GO";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(btnGo_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 23f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(this.btnGo);
			base.Controls.Add(this.lbIndex);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lbHardness);
			base.Controls.Add(this.lbHV);
			base.Controls.Add(this.box);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5f);
			base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			base.Name = "MeasureRecordUnit";
			base.Size = new System.Drawing.Size(280, 252);
			((System.ComponentModel.ISupportInitialize)this.box).EndInit();
			base.ResumeLayout(false);
		}
	}
}