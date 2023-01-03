using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Labtt.Data;
using Labtt.DrawArea;

namespace AIO_Client
{

	public class TrimMeasureForm : Form
	{
		private MainForm owner = null;

		private StepInfo stepInfo = null;

		private int stepLength = 1;

		private IContainer components = null;

		private Button btnStepLength;

		private Button btnRightLineLeft;

		private Button btnLeftLineLeft;

		private Button btnBottomLineDown;

		private Button btnBottomLineUp;

		private Button btnTopLineDown;

		private Button btnTopLineUp;

		private Button btnRightLineRight;

		private Button btnLeftLineRight;

		public TrimMeasureForm(MainForm owner)
		{
			InitializeComponent();
			LoadLanguageResources();
			this.owner = owner;
		}

		private void TrimMeasureForm_Load(object sender, EventArgs e)
		{
			stepInfo = (StepInfo)FileOperator.DeserializeFromXMLFile(CommonData.TrimMeasureConfigFilepath, typeof(StepInfo));
			if (stepInfo == null)
			{
				stepInfo = new StepInfo();
				stepInfo.StepLengthHigh = 5;
				stepInfo.StepLengthNormal = 1;
				FileOperator.SerializeToXMLFile(stepInfo, CommonData.TrimMeasureConfigFilepath);
			}
		}

		private void btnStepLength_Click(object sender, EventArgs e)
		{
			if (stepLength == stepInfo.StepLengthHigh)
			{
				stepLength = stepInfo.StepLengthNormal;
				btnStepLength.Text = ResourcesManager.Resources.R_TrimMeasure_Slow;
			}
			else
			{
				stepLength = stepInfo.StepLengthHigh;
				btnStepLength.Text = ResourcesManager.Resources.R_TrimMeasure_Fast;
			}
		}

		private void btnLeftLineLeft_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(-stepLength, 0f, 1);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnLeftLineRight_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(stepLength, 0f, 1);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnRightLineLeft_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(-stepLength, 0f, 2);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnRightLineRight_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(stepLength, 0f, 2);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnTopLineUp_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(0f, -stepLength, 3);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnTopLineDown_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(0f, stepLength, 3);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnBottomLineUp_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(0f, -stepLength, 4);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void btnBottomLineDown_Click(object sender, EventArgs e)
		{
			foreach (GraphicsObject o in owner.imageBox.GraphicsList)
			{
				if (o.GetType() == typeof(GraphicsFourLine))
				{
					GraphicsFourLine g = o as GraphicsFourLine;
					g.MoveHandle(0f, stepLength, 4);
				}
			}
			owner.imageBox.Invalidate();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_TrimMeasure_Title;
			btnStepLength.Text = ResourcesManager.Resources.R_TrimMeasure_Slow;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AIO_Client.TrimMeasureForm));
			this.btnStepLength = new System.Windows.Forms.Button();
			this.btnRightLineLeft = new System.Windows.Forms.Button();
			this.btnLeftLineLeft = new System.Windows.Forms.Button();
			this.btnBottomLineDown = new System.Windows.Forms.Button();
			this.btnBottomLineUp = new System.Windows.Forms.Button();
			this.btnTopLineDown = new System.Windows.Forms.Button();
			this.btnTopLineUp = new System.Windows.Forms.Button();
			this.btnRightLineRight = new System.Windows.Forms.Button();
			this.btnLeftLineRight = new System.Windows.Forms.Button();
			base.SuspendLayout();
			this.btnStepLength.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnStepLength.Font = new System.Drawing.Font("Microsoft YaHei UI", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.btnStepLength.Location = new System.Drawing.Point(169, 175);
			this.btnStepLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnStepLength.Name = "btnStepLength";
			this.btnStepLength.Size = new System.Drawing.Size(60, 60);
			this.btnStepLength.TabIndex = 118;
			this.btnStepLength.Text = "Step";
			this.btnStepLength.UseVisualStyleBackColor = true;
			this.btnStepLength.Click += new System.EventHandler(btnStepLength_Click);
			this.btnRightLineLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnRightLineLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRightLineLeft.Image = (System.Drawing.Image)resources.GetObject("btnRightLineLeft.Image");
			this.btnRightLineLeft.Location = new System.Drawing.Point(247, 175);
			this.btnRightLineLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnRightLineLeft.Name = "btnRightLineLeft";
			this.btnRightLineLeft.Size = new System.Drawing.Size(60, 60);
			this.btnRightLineLeft.TabIndex = 117;
			this.btnRightLineLeft.UseVisualStyleBackColor = true;
			this.btnRightLineLeft.Click += new System.EventHandler(btnRightLineLeft_Click);
			this.btnLeftLineLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnLeftLineLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnLeftLineLeft.Image = (System.Drawing.Image)resources.GetObject("btnLeftLineLeft.Image");
			this.btnLeftLineLeft.Location = new System.Drawing.Point(23, 175);
			this.btnLeftLineLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnLeftLineLeft.Name = "btnLeftLineLeft";
			this.btnLeftLineLeft.Size = new System.Drawing.Size(60, 60);
			this.btnLeftLineLeft.TabIndex = 116;
			this.btnLeftLineLeft.UseVisualStyleBackColor = true;
			this.btnLeftLineLeft.Click += new System.EventHandler(btnLeftLineLeft_Click);
			this.btnBottomLineDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnBottomLineDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnBottomLineDown.Image = (System.Drawing.Image)resources.GetObject("btnBottomLineDown.Image");
			this.btnBottomLineDown.Location = new System.Drawing.Point(169, 325);
			this.btnBottomLineDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnBottomLineDown.Name = "btnBottomLineDown";
			this.btnBottomLineDown.Size = new System.Drawing.Size(60, 60);
			this.btnBottomLineDown.TabIndex = 115;
			this.btnBottomLineDown.UseVisualStyleBackColor = true;
			this.btnBottomLineDown.Click += new System.EventHandler(btnBottomLineDown_Click);
			this.btnBottomLineUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnBottomLineUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnBottomLineUp.Image = (System.Drawing.Image)resources.GetObject("btnBottomLineUp.Image");
			this.btnBottomLineUp.Location = new System.Drawing.Point(169, 255);
			this.btnBottomLineUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnBottomLineUp.Name = "btnBottomLineUp";
			this.btnBottomLineUp.Size = new System.Drawing.Size(60, 60);
			this.btnBottomLineUp.TabIndex = 114;
			this.btnBottomLineUp.UseVisualStyleBackColor = true;
			this.btnBottomLineUp.Click += new System.EventHandler(btnBottomLineUp_Click);
			this.btnTopLineDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnTopLineDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnTopLineDown.Image = (System.Drawing.Image)resources.GetObject("btnTopLineDown.Image");
			this.btnTopLineDown.Location = new System.Drawing.Point(169, 95);
			this.btnTopLineDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTopLineDown.Name = "btnTopLineDown";
			this.btnTopLineDown.Size = new System.Drawing.Size(60, 60);
			this.btnTopLineDown.TabIndex = 113;
			this.btnTopLineDown.UseVisualStyleBackColor = true;
			this.btnTopLineDown.Click += new System.EventHandler(btnTopLineDown_Click);
			this.btnTopLineUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnTopLineUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnTopLineUp.Image = (System.Drawing.Image)resources.GetObject("btnTopLineUp.Image");
			this.btnTopLineUp.Location = new System.Drawing.Point(169, 25);
			this.btnTopLineUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTopLineUp.Name = "btnTopLineUp";
			this.btnTopLineUp.Size = new System.Drawing.Size(60, 60);
			this.btnTopLineUp.TabIndex = 112;
			this.btnTopLineUp.UseVisualStyleBackColor = true;
			this.btnTopLineUp.Click += new System.EventHandler(btnTopLineUp_Click);
			this.btnRightLineRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnRightLineRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRightLineRight.Image = (System.Drawing.Image)resources.GetObject("btnRightLineRight.Image");
			this.btnRightLineRight.Location = new System.Drawing.Point(315, 175);
			this.btnRightLineRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnRightLineRight.Name = "btnRightLineRight";
			this.btnRightLineRight.Size = new System.Drawing.Size(60, 60);
			this.btnRightLineRight.TabIndex = 111;
			this.btnRightLineRight.UseVisualStyleBackColor = true;
			this.btnRightLineRight.Click += new System.EventHandler(btnRightLineRight_Click);
			this.btnLeftLineRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnLeftLineRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnLeftLineRight.Image = (System.Drawing.Image)resources.GetObject("btnLeftLineRight.Image");
			this.btnLeftLineRight.Location = new System.Drawing.Point(91, 175);
			this.btnLeftLineRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnLeftLineRight.Name = "btnLeftLineRight";
			this.btnLeftLineRight.Size = new System.Drawing.Size(60, 60);
			this.btnLeftLineRight.TabIndex = 110;
			this.btnLeftLineRight.UseVisualStyleBackColor = true;
			this.btnLeftLineRight.Click += new System.EventHandler(btnLeftLineRight_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(11f, 25f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(398, 409);
			base.Controls.Add(this.btnStepLength);
			base.Controls.Add(this.btnRightLineLeft);
			base.Controls.Add(this.btnLeftLineLeft);
			base.Controls.Add(this.btnBottomLineDown);
			base.Controls.Add(this.btnBottomLineUp);
			base.Controls.Add(this.btnTopLineDown);
			base.Controls.Add(this.btnTopLineUp);
			base.Controls.Add(this.btnRightLineRight);
			base.Controls.Add(this.btnLeftLineRight);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Margin = new System.Windows.Forms.Padding(6);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TrimMeasureForm";
			base.ShowIcon = false;
			this.Text = "Trim Measure";
			base.TopMost = true;
			base.Load += new System.EventHandler(TrimMeasureForm_Load);
			base.ResumeLayout(false);
		}
	}
}
