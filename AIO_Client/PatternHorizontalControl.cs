using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AIO_Client.Properties;
using Labtt.Data;
using Labtt.DrawArea;

namespace AIO_Client
{

	public class PatternHorizontalControl : UserControl, IPatternControl
	{
		private MainForm owner;

		private ImageBox box;

		private GraphicsLineBySlope graphicsLine;

		private int drawReferencePointIndex;

		private SetString setReferencePointX = null;

		private SetString setReferencePointY = null;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private Label lbReferencePoint;

		private Label lbFirstOffset;

		private Label label1;

		private Label lbNumber;

		private Label lbInterval;

		private Label label2;

		private Label lbOffset;

		private TextBox tbOffsetReferencePoint;

		private TextBox tbNumberOfPoints;

		private TextBox tbOffsetLine;

		private Button btnSetReferencePoint;

		private TextBox tbReferencePointX;

		private TextBox tbReferencePointY;

		private TextBox tbInterval;

		private PointF ReferencePoint
		{
			set
			{
				Invoke(setReferencePointX, value.X.ToString("F5"));
				Invoke(setReferencePointY, value.Y.ToString("F5"));
			}
		}

		public PatternHorizontalControl()
		{
			InitializeComponent();
			setReferencePointX = SetReferencePointX;
			setReferencePointY = SetReferencePointY;
			graphicsLine = new GraphicsLineBySlope();
			graphicsLine.MoveHandleTo(PointF.Empty, 0);
			graphicsLine.MoveHandleTo(new PointF(0f, 0f), 1);
			graphicsLine.OnGraphicsChanged += graphicsLine_OnGraphicsChanged;
		}

		private void referencePoint_TextChanged(object sender, EventArgs e)
		{
			if (!tbReferencePointX.Focused && !tbReferencePointY.Focused)
			{
				return;
			}
			if (float.TryParse(tbReferencePointX.Text, out var referencePointX) && float.TryParse(tbReferencePointY.Text, out var referencePointY))
			{
				PointF physicalLocation = new PointF(referencePointX, referencePointY);
				PointF boxLocation = box.ConvertPhysicalCoordinateToBoxPointF(physicalLocation);
				graphicsLine.MoveHandleTo(boxLocation, 0);
				if (!box.GraphicsList.Contains(graphicsLine))
				{
					box.GraphicsList.Add(graphicsLine);
				}
			}
			else
			{
				box.GraphicsList.Remove(graphicsLine);
			}
			box.Invalidate();
		}

		private void btnSetReferencePoint_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				drawReferencePointIndex = 1;
			}
		}

		private void graphicsLine_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocation = (ReferencePoint = box.ConvertBoxPointFToPhysicalCoordinate(graphicsLine.Location));
		}

		private void box_MouseDown(object sender, MouseEventArgs e)
		{
			if (drawReferencePointIndex == 1)
			{
				graphicsLine.MoveHandleTo(e.Location, 0);
				drawReferencePointIndex = 0;
				if (!box.GraphicsList.Contains(graphicsLine))
				{
					box.GraphicsList.Add(graphicsLine);
				}
			}
		}

		private void LoadLanguageResources()
		{
			lbReferencePoint.Text = ResourcesManager.Resources.R_Pattern_ReferencePoint;
			lbInterval.Text = ResourcesManager.Resources.R_Pattern_Interval;
			lbOffset.Text = ResourcesManager.Resources.R_Pattern_Offset;
			lbFirstOffset.Text = ResourcesManager.Resources.R_Pattern_FirstOffset;
			lbNumber.Text = ResourcesManager.Resources.R_Pattern_Number;
		}

		public void Initialize(MainForm owner)
		{
			this.owner = owner;
			box = this.owner.imageBox;
			box.MouseDown += box_MouseDown;
			LoadLanguageResources();
		}

		public void SetGraphicsVisible(bool visible)
		{
			drawReferencePointIndex = 0;
			graphicsLine.Visible = visible;
			box.Invalidate();
		}

		public void Reset()
		{
			tbReferencePointX.Text = string.Empty;
			tbReferencePointY.Text = string.Empty;
			tbInterval.Text = string.Empty;
			tbOffsetLine.Text = string.Empty;
			tbOffsetReferencePoint.Text = string.Empty;
			tbNumberOfPoints.Text = string.Empty;
			drawReferencePointIndex = 0;
			box.GraphicsList.Remove(graphicsLine);
		}

		public List<PointF> GeneratePoints()
		{
			try
			{
				List<PointF> pointList = new List<PointF>();
				PointF location = new PointF(float.Parse(tbReferencePointX.Text), float.Parse(tbReferencePointY.Text));
				float interval = float.Parse(tbInterval.Text);
				float offsetLine = float.Parse(tbOffsetLine.Text);
				float offsetReferencePoint = float.Parse(tbOffsetReferencePoint.Text);
				float number = float.Parse(tbNumberOfPoints.Text);
				for (int i = 1; (float)i <= number; i++)
				{
					float y = location.Y;
					if (i % 4 == 0)
					{
						y -= offsetLine;
					}
					else if (i % 2 == 0)
					{
						y += offsetLine;
					}
					float x = location.X + offsetReferencePoint;
					PointF point = new PointF(x + (float)(i - 1) * interval, y);
					pointList.Add(point);
				}
				return pointList;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to generate point set！");
				throw ex;
			}
		}

		public void GeneratePointsAndDepth(out List<PointF> pointList, out List<float> depthList)
		{
			try
			{
				pointList = new List<PointF>();
				depthList = new List<float>();
				PointF location = new PointF(float.Parse(tbReferencePointX.Text), float.Parse(tbReferencePointY.Text));
				float interval = float.Parse(tbInterval.Text);
				float offsetLine = float.Parse(tbOffsetLine.Text);
				float offsetReferencePoint = float.Parse(tbOffsetReferencePoint.Text);
				float number = float.Parse(tbNumberOfPoints.Text);
				for (int i = 1; (float)i <= number; i++)
				{
					float y = location.Y;
					if (i % 4 == 0)
					{
						y -= offsetLine;
					}
					else if (i % 2 == 0)
					{
						y += offsetLine;
					}
					float x = location.X + offsetReferencePoint;
					PointF point = new PointF(x + (float)(i - 1) * interval, y);
					pointList.Add(point);
					depthList.Add(offsetReferencePoint + (float)(i - 1) * interval);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to generate point set！");
				throw ex;
			}
		}

		private void SetReferencePointX(string value)
		{
			if (!tbReferencePointX.Focused)
			{
				tbReferencePointX.Text = value;
			}
		}

		private void SetReferencePointY(string value)
		{
			if (!tbReferencePointY.Focused)
			{
				tbReferencePointY.Text = value;
			}
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbReferencePoint = new System.Windows.Forms.Label();
            this.lbFirstOffset = new System.Windows.Forms.Label();
            this.tbOffsetReferencePoint = new System.Windows.Forms.TextBox();
            this.tbNumberOfPoints = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbNumber = new System.Windows.Forms.Label();
            this.lbInterval = new System.Windows.Forms.Label();
            this.tbOffsetLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbOffset = new System.Windows.Forms.Label();
            this.btnSetReferencePoint = new System.Windows.Forms.Button();
            this.tbReferencePointX = new System.Windows.Forms.TextBox();
            this.tbReferencePointY = new System.Windows.Forms.TextBox();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.Controls.Add(this.lbReferencePoint, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbFirstOffset, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbOffsetReferencePoint, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbNumberOfPoints, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbNumber, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbInterval, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbOffsetLine, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbOffset, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferencePoint, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePointX, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePointY, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbInterval, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.90664F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.43244F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.66093F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 200);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // lbReferencePoint
            // 
            this.lbReferencePoint.AutoSize = true;
            this.lbReferencePoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferencePoint.Location = new System.Drawing.Point(2, 0);
            this.lbReferencePoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferencePoint.Name = "lbReferencePoint";
            this.lbReferencePoint.Size = new System.Drawing.Size(96, 67);
            this.lbReferencePoint.TabIndex = 0;
            this.lbReferencePoint.Text = "Reference Point";
            this.lbReferencePoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFirstOffset
            // 
            this.lbFirstOffset.AutoSize = true;
            this.lbFirstOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFirstOffset.Location = new System.Drawing.Point(102, 131);
            this.lbFirstOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFirstOffset.Name = "lbFirstOffset";
            this.lbFirstOffset.Size = new System.Drawing.Size(81, 69);
            this.lbFirstOffset.TabIndex = 22;
            this.lbFirstOffset.Text = "First Offset";
            this.lbFirstOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbOffsetReferencePoint
            // 
            this.tbOffsetReferencePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOffsetReferencePoint.BackColor = System.Drawing.SystemColors.Window;
            this.tbOffsetReferencePoint.Location = new System.Drawing.Point(187, 150);
            this.tbOffsetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbOffsetReferencePoint.Name = "tbOffsetReferencePoint";
            this.tbOffsetReferencePoint.Size = new System.Drawing.Size(96, 30);
            this.tbOffsetReferencePoint.TabIndex = 5;
            // 
            // tbNumberOfPoints
            // 
            this.tbNumberOfPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNumberOfPoints.BackColor = System.Drawing.SystemColors.Window;
            this.tbNumberOfPoints.Location = new System.Drawing.Point(372, 150);
            this.tbNumberOfPoints.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbNumberOfPoints.Name = "tbNumberOfPoints";
            this.tbNumberOfPoints.Size = new System.Drawing.Size(96, 30);
            this.tbNumberOfPoints.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(102, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 67);
            this.label1.TabIndex = 27;
            this.label1.Text = "X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNumber.Location = new System.Drawing.Point(287, 131);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(81, 69);
            this.lbNumber.TabIndex = 24;
            this.lbNumber.Text = "Number";
            this.lbNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInterval
            // 
            this.lbInterval.AutoSize = true;
            this.lbInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInterval.Location = new System.Drawing.Point(102, 67);
            this.lbInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInterval.Name = "lbInterval";
            this.lbInterval.Size = new System.Drawing.Size(81, 64);
            this.lbInterval.TabIndex = 20;
            this.lbInterval.Text = "Interval";
            this.lbInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbOffsetLine
            // 
            this.tbOffsetLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOffsetLine.BackColor = System.Drawing.SystemColors.Window;
            this.tbOffsetLine.Location = new System.Drawing.Point(372, 84);
            this.tbOffsetLine.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbOffsetLine.Name = "tbOffsetLine";
            this.tbOffsetLine.Size = new System.Drawing.Size(96, 30);
            this.tbOffsetLine.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(287, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 67);
            this.label2.TabIndex = 13;
            this.label2.Text = "Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbOffset
            // 
            this.lbOffset.AutoSize = true;
            this.lbOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOffset.Location = new System.Drawing.Point(287, 67);
            this.lbOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOffset.Name = "lbOffset";
            this.lbOffset.Size = new System.Drawing.Size(81, 64);
            this.lbOffset.TabIndex = 21;
            this.lbOffset.Text = "Offset";
            this.lbOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetReferencePoint
            // 
            this.btnSetReferencePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferencePoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetReferencePoint.Image = global::AIO_Client_Properties_Resources.select;
            this.btnSetReferencePoint.Location = new System.Drawing.Point(472, 20);
            this.btnSetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetReferencePoint.Name = "btnSetReferencePoint";
            this.btnSetReferencePoint.Size = new System.Drawing.Size(26, 26);
            this.btnSetReferencePoint.TabIndex = 29;
            this.btnSetReferencePoint.UseVisualStyleBackColor = true;
            this.btnSetReferencePoint.Click += new System.EventHandler(this.btnSetReferencePoint_Click);
            // 
            // tbReferencePointX
            // 
            this.tbReferencePointX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePointX.Location = new System.Drawing.Point(187, 18);
            this.tbReferencePointX.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePointX.Name = "tbReferencePointX";
            this.tbReferencePointX.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePointX.TabIndex = 41;
            this.tbReferencePointX.TextChanged += new System.EventHandler(this.referencePoint_TextChanged);
            // 
            // tbReferencePointY
            // 
            this.tbReferencePointY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePointY.Location = new System.Drawing.Point(372, 18);
            this.tbReferencePointY.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePointY.Name = "tbReferencePointY";
            this.tbReferencePointY.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePointY.TabIndex = 42;
            this.tbReferencePointY.TextChanged += new System.EventHandler(this.referencePoint_TextChanged);
            // 
            // tbInterval
            // 
            this.tbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInterval.Location = new System.Drawing.Point(187, 84);
            this.tbInterval.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(96, 30);
            this.tbInterval.TabIndex = 43;
            // 
            // PatternHorizontalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PatternHorizontalControl";
            this.Size = new System.Drawing.Size(500, 200);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		void IPatternControl.BringToFront()
		{
			BringToFront();
		}
	}
}
