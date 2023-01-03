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

	public class PatternMatrixControl : UserControl, IPatternControl
	{
		private MainForm owner;

		private ImageBox box;

		private GraphicsSpot graphicsSpot;

		private int drawReferencePointIndex;

		private SetString setReferencePointX = null;

		private SetString setReferencePointY = null;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private Label lbReferencePoint;

		private Label label1;

		private Label label2;

		private Label label5;

		private Label lbInterval;

		private Label label8;

		private Label lbRows;

		private Label lbNumber;

		private Label lbColumns;

		private Button btnSetReferencePoint;

		private TextBox tbReferencePointX;

		private TextBox tbReferencePointY;

		private TextBox tbIntervalX;

		private TextBox tbIntervalY;

		private TextBox tbNumberX;

		private TextBox tbNumberY;

		private PointF ReferencePoint
		{
			set
			{
				Invoke(setReferencePointX, value.X.ToString("F5"));
				Invoke(setReferencePointY, value.Y.ToString("F5"));
			}
		}

		public PatternMatrixControl()
		{
			InitializeComponent();
			setReferencePointX = SetReferencePointX;
			setReferencePointY = SetReferencePointY;
			graphicsSpot = new GraphicsSpot(0f, 0f, 3f);
			graphicsSpot.OnGraphicsChanged += graphicsSpot_OnGraphicsChanged;
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
				graphicsSpot.MoveHandleTo(boxLocation, 0);
				if (!box.GraphicsList.Contains(graphicsSpot))
				{
					box.GraphicsList.Add(graphicsSpot);
				}
			}
			else
			{
				box.GraphicsList.Remove(graphicsSpot);
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

		private void graphicsSpot_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocation = (ReferencePoint = box.ConvertBoxPointFToPhysicalCoordinate(graphicsSpot.Location));
		}

		private void box_MouseDown(object sender, MouseEventArgs e)
		{
			if (drawReferencePointIndex == 1)
			{
				graphicsSpot.MoveHandleTo(e.Location, 0);
				drawReferencePointIndex = 0;
				if (!box.GraphicsList.Contains(graphicsSpot))
				{
					box.GraphicsList.Add(graphicsSpot);
				}
			}
		}

		private void LoadLanguageResources()
		{
			lbReferencePoint.Text = ResourcesManager.Resources.R_Pattern_ReferencePoint;
			lbInterval.Text = ResourcesManager.Resources.R_Pattern_Interval;
			lbNumber.Text = ResourcesManager.Resources.R_Pattern_Number;
			lbRows.Text = ResourcesManager.Resources.R_Pattern_Rows;
			lbColumns.Text = ResourcesManager.Resources.R_Pattern_Columns;
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
			graphicsSpot.Visible = visible;
			box.Invalidate();
		}

		public void Reset()
		{
			tbReferencePointX.Text = string.Empty;
			tbReferencePointY.Text = string.Empty;
			tbIntervalX.Text = string.Empty;
			tbIntervalY.Text = string.Empty;
			tbNumberX.Text = string.Empty;
			tbNumberY.Text = string.Empty;
			drawReferencePointIndex = 0;
			box.GraphicsList.Remove(graphicsSpot);
		}

		public List<PointF> GeneratePoints()
		{
			try
			{
				List<PointF> pointList = new List<PointF>();
				PointF location = new PointF(float.Parse(tbReferencePointX.Text), float.Parse(tbReferencePointY.Text));
				float intervalX = float.Parse(tbIntervalX.Text);
				float intervalY = float.Parse(tbIntervalY.Text);
				float numberX = float.Parse(tbNumberX.Text);
				float numberY = float.Parse(tbNumberY.Text);
				for (int i = 0; (float)i < numberY; i++)
				{
					for (int j = 0; (float)j < numberX; j++)
					{
						PointF point = new PointF(location.X + (float)i * intervalX, location.Y + (float)j * intervalY);
						pointList.Add(point);
					}
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
				float intervalX = float.Parse(tbIntervalX.Text);
				float intervalY = float.Parse(tbIntervalY.Text);
				float numberX = float.Parse(tbNumberX.Text);
				float numberY = float.Parse(tbNumberY.Text);
				for (int i = 0; (float)i < numberY; i++)
				{
					for (int j = 0; (float)j < numberX; j++)
					{
						PointF point = new PointF(location.X + (float)i * intervalX, location.Y + (float)j * intervalY);
						pointList.Add(point);
						depthList.Add(0f);
					}
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbInterval = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lbRows = new System.Windows.Forms.Label();
			this.lbNumber = new System.Windows.Forms.Label();
			this.lbColumns = new System.Windows.Forms.Label();
			this.btnSetReferencePoint = new System.Windows.Forms.Button();
			this.tbReferencePointX = new System.Windows.Forms.TextBox();
			this.tbReferencePointY = new System.Windows.Forms.TextBox();
			this.tbIntervalX = new System.Windows.Forms.TextBox();
			this.tbIntervalY = new System.Windows.Forms.TextBox();
			this.tbNumberX = new System.Windows.Forms.TextBox();
			this.tbNumberY = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6f));
			this.tableLayoutPanel1.Controls.Add(this.lbReferencePoint, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label5, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbInterval, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label8, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbRows, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.lbNumber, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lbColumns, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnSetReferencePoint, 5, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbReferencePointX, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbReferencePointY, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbIntervalX, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbIntervalY, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.tbNumberX, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.tbNumberY, 4, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 200);
			this.tableLayoutPanel1.TabIndex = 29;
			this.lbReferencePoint.AutoSize = true;
			this.lbReferencePoint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbReferencePoint.Location = new System.Drawing.Point(2, 0);
			this.lbReferencePoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbReferencePoint.Name = "lbReferencePoint";
			this.lbReferencePoint.Size = new System.Drawing.Size(96, 66);
			this.lbReferencePoint.TabIndex = 0;
			this.lbReferencePoint.Text = "Reference Point";
			this.lbReferencePoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(102, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 66);
			this.label1.TabIndex = 27;
			this.label1.Text = "X";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(287, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 66);
			this.label2.TabIndex = 13;
			this.label2.Text = "Y";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(287, 66);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(81, 66);
			this.label5.TabIndex = 21;
			this.label5.Text = "Y";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbInterval.AutoSize = true;
			this.lbInterval.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbInterval.Location = new System.Drawing.Point(2, 66);
			this.lbInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbInterval.Name = "lbInterval";
			this.lbInterval.Size = new System.Drawing.Size(96, 66);
			this.lbInterval.TabIndex = 20;
			this.lbInterval.Text = "Interval";
			this.lbInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Location = new System.Drawing.Point(102, 66);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(81, 66);
			this.label8.TabIndex = 29;
			this.label8.Text = "X";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbRows.AutoSize = true;
			this.lbRows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbRows.Location = new System.Drawing.Point(102, 132);
			this.lbRows.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbRows.Name = "lbRows";
			this.lbRows.Size = new System.Drawing.Size(81, 68);
			this.lbRows.TabIndex = 24;
			this.lbRows.Text = "Rows";
			this.lbRows.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbNumber.AutoSize = true;
			this.lbNumber.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbNumber.Location = new System.Drawing.Point(2, 132);
			this.lbNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbNumber.Name = "lbNumber";
			this.lbNumber.Size = new System.Drawing.Size(96, 68);
			this.lbNumber.TabIndex = 22;
			this.lbNumber.Text = "Number";
			this.lbNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbColumns.AutoSize = true;
			this.lbColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbColumns.Location = new System.Drawing.Point(287, 132);
			this.lbColumns.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbColumns.Name = "lbColumns";
			this.lbColumns.Size = new System.Drawing.Size(81, 68);
			this.lbColumns.TabIndex = 30;
			this.lbColumns.Text = "Columns";
			this.lbColumns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnSetReferencePoint.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.btnSetReferencePoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnSetReferencePoint.Image = AIO_Client.Properties.Resources.select;
			this.btnSetReferencePoint.Location = new System.Drawing.Point(472, 20);
			this.btnSetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.btnSetReferencePoint.Name = "btnSetReferencePoint";
			this.btnSetReferencePoint.Size = new System.Drawing.Size(26, 26);
			this.btnSetReferencePoint.TabIndex = 31;
			this.btnSetReferencePoint.UseVisualStyleBackColor = true;
			this.btnSetReferencePoint.Click += new System.EventHandler(btnSetReferencePoint_Click);
			this.tbReferencePointX.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tbReferencePointX.Location = new System.Drawing.Point(187, 20);
			this.tbReferencePointX.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.tbReferencePointX.Name = "tbReferencePointX";
			this.tbReferencePointX.Size = new System.Drawing.Size(96, 25);
			this.tbReferencePointX.TabIndex = 42;
			this.tbReferencePointX.TextChanged += new System.EventHandler(referencePoint_TextChanged);
			this.tbReferencePointY.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tbReferencePointY.Location = new System.Drawing.Point(372, 20);
			this.tbReferencePointY.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.tbReferencePointY.Name = "tbReferencePointY";
			this.tbReferencePointY.Size = new System.Drawing.Size(96, 25);
			this.tbReferencePointY.TabIndex = 43;
			this.tbReferencePointY.TextChanged += new System.EventHandler(referencePoint_TextChanged);
			this.tbIntervalX.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tbIntervalX.Location = new System.Drawing.Point(187, 86);
			this.tbIntervalX.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.tbIntervalX.Name = "tbIntervalX";
			this.tbIntervalX.Size = new System.Drawing.Size(96, 25);
			this.tbIntervalX.TabIndex = 44;
			this.tbIntervalY.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tbIntervalY.Location = new System.Drawing.Point(372, 86);
			this.tbIntervalY.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.tbIntervalY.Name = "tbIntervalY";
			this.tbIntervalY.Size = new System.Drawing.Size(96, 25);
			this.tbIntervalY.TabIndex = 45;
			this.tbNumberX.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tbNumberX.Location = new System.Drawing.Point(187, 153);
			this.tbNumberX.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.tbNumberX.Name = "tbNumberX";
			this.tbNumberX.Size = new System.Drawing.Size(96, 25);
			this.tbNumberX.TabIndex = 46;
			this.tbNumberY.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.tbNumberY.Location = new System.Drawing.Point(372, 153);
			this.tbNumberY.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.tbNumberY.Name = "tbNumberY";
			this.tbNumberY.Size = new System.Drawing.Size(96, 25);
			this.tbNumberY.TabIndex = 47;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 20f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			base.Name = "PatternMatrixControl";
			base.Size = new System.Drawing.Size(500, 200);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}

		void IPatternControl.BringToFront()
		{
			BringToFront();
		}
	}
}
