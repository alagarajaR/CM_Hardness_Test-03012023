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

	public class PatternSlashControl : UserControl, IPatternControl
	{
		private MainForm owner;

		private ImageBox box;

		private GraphicsLineByTwoPoint graphicsReferenceLine;

		private GraphicsLineBySlope graphicsLine;

		private GraphicsLineBySlope graphicsRotatedLine;

		private int drawReferencePointIndex;

		private bool isDrawingReferenceLine;

		private float slope = 0f;

		private SetString setReferencePoint1X = null;

		private SetString setReferencePoint1Y = null;

		private SetString setReferencePoint2X = null;

		private SetString setReferencePoint2Y = null;

		private SetString setReferencePoint3X = null;

		private SetString setReferencePoint3Y = null;

		private GetString getAngle = null;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private Label lbReferenceLine;

		private Label label1;

		private Label label2;

		private Label label10;

		private Label lbReferencePoint;

		private Label label11;

		private Label label12;

		private Label label13;

		private Label lbFirstOffset;

		private Label lbAngle;

		private Label lbInterval;

		private Label lbNumber;

		private Label lbOffset;

		private Button btnSetReferenceLine;

		private Button btnSetReferencePoint;

		private TextBox tbReferencePoint1X;

		private TextBox tbReferencePoint1Y;

		private TextBox tbReferencePoint2X;

		private TextBox tbReferencePoint2Y;

		private TextBox tbReferencePoint3X;

		private TextBox tbReferencePoint3Y;

		private TextBox tbOffsetReferencePoint;

		private TextBox tbAngle;

		private TextBox tbInterval;

		private TextBox tbOffsetLine;

		private TextBox tbNumberOfPoints;

		private PointF ReferencePoint1
		{
			set
			{
				Invoke(setReferencePoint1X, value.X.ToString("F5"));
				Invoke(setReferencePoint1Y, value.Y.ToString("F5"));
			}
		}

		private PointF ReferencePoint2
		{
			set
			{
				Invoke(setReferencePoint2X, value.X.ToString("F5"));
				Invoke(setReferencePoint2Y, value.Y.ToString("F5"));
			}
		}

		private PointF ReferencePoint3
		{
			set
			{
				Invoke(setReferencePoint3X, value.X.ToString("F5"));
				Invoke(setReferencePoint3Y, value.Y.ToString("F5"));
			}
		}

		private string AngleText => Invoke(getAngle).ToString();

		public PatternSlashControl()
		{
			InitializeComponent();
			setReferencePoint1X = SetReferencePoint1X;
			setReferencePoint1Y = SetReferencePoint1Y;
			setReferencePoint2X = SetReferencePoint2X;
			setReferencePoint2Y = SetReferencePoint2Y;
			setReferencePoint3X = SetReferencePoint3X;
			setReferencePoint3Y = SetReferencePoint3Y;
			getAngle = GetAngle;
			graphicsReferenceLine = new GraphicsLineByTwoPoint();
			graphicsReferenceLine.OnGraphicsChanged += graphicsReferenceLine_OnGraphicsChanged;
			graphicsLine = new GraphicsLineBySlope();
			graphicsLine.OnGraphicsChanged += graphicsLine_OnGraphicsChanged;
			graphicsRotatedLine = new GraphicsLineBySlope();
			graphicsRotatedLine.Color = Color.RoyalBlue;
			graphicsRotatedLine.OnGraphicsChanged += graphicsRotatedLine_OnGraphicsChanged;
		}

		private void btnSetReferenceLine_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				graphicsReferenceLine.MoveHandleTo(PointF.Empty, 1);
				graphicsReferenceLine.MoveHandleTo(PointF.Empty, 2);
				drawReferencePointIndex = 1;
				isDrawingReferenceLine = false;
				box.Invalidate();
			}
		}

		private void btnSetReferencePoint_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				drawReferencePointIndex = 2;
			}
		}

		private void referenceLine_TextChanged(object sender, EventArgs e)
		{
			if (!tbReferencePoint1X.Focused && !tbReferencePoint1Y.Focused && !tbReferencePoint2X.Focused && !tbReferencePoint2Y.Focused)
			{
				return;
			}
			if (float.TryParse(tbReferencePoint1X.Text, out var referencePoint1X) && float.TryParse(tbReferencePoint1Y.Text, out var referencePoint1Y) && float.TryParse(tbReferencePoint2X.Text, out var referencePoint2X) && float.TryParse(tbReferencePoint2Y.Text, out var referencePoint2Y) && (referencePoint1X != referencePoint2X || referencePoint1Y != referencePoint2Y))
			{
				PointF physicalStart = new PointF(referencePoint1X, referencePoint1Y);
				PointF physicalEnd = new PointF(referencePoint2X, referencePoint2Y);
				PointF boxStart = box.ConvertPhysicalCoordinateToBoxPointF(physicalStart);
				PointF boxEnd = box.ConvertPhysicalCoordinateToBoxPointF(physicalEnd);
				if (graphicsReferenceLine.StartPoint != boxStart)
				{
					graphicsReferenceLine.MoveHandleTo(boxStart, 1);
				}
				if (graphicsReferenceLine.EndPoint != boxEnd)
				{
					graphicsReferenceLine.MoveHandleTo(boxEnd, 2);
				}
				if (!box.GraphicsList.Contains(graphicsReferenceLine))
				{
					box.GraphicsList.Add(graphicsReferenceLine);
				}
				if (!box.GraphicsList.Contains(graphicsLine))
				{
					box.GraphicsList.Add(graphicsLine);
					graphicsLine.MoveHandleTo(new PointF((boxStart.X + boxEnd.X) / 2f, (boxStart.Y + boxEnd.Y) / 2f), 0);
					if (!box.GraphicsList.Contains(graphicsRotatedLine))
					{
						box.GraphicsList.Add(graphicsRotatedLine);
					}
				}
			}
			else
			{
				box.GraphicsList.Remove(graphicsReferenceLine);
				box.GraphicsList.Remove(graphicsLine);
				box.GraphicsList.Remove(graphicsRotatedLine);
			}
			box.Invalidate();
		}

		private void referencePoint_TextChanged(object sender, EventArgs e)
		{
			if (!tbReferencePoint3X.Focused && !tbReferencePoint3Y.Focused)
			{
				return;
			}
			if (float.TryParse(tbReferencePoint3X.Text, out var referencePoint3X) && float.TryParse(tbReferencePoint3Y.Text, out var referencePoint3Y))
			{
				PointF physicalLocation = new PointF(referencePoint3X, referencePoint3Y);
				PointF boxLocation = box.ConvertPhysicalCoordinateToBoxPointF(physicalLocation);
				graphicsLine.MoveHandleTo(boxLocation, 0);
				if (box.GraphicsList.Contains(graphicsReferenceLine) && !box.GraphicsList.Contains(graphicsLine))
				{
					box.GraphicsList.Add(graphicsLine);
					if (!box.GraphicsList.Contains(graphicsRotatedLine))
					{
						box.GraphicsList.Add(graphicsRotatedLine);
					}
				}
			}
			else
			{
				box.GraphicsList.Remove(graphicsLine);
				box.GraphicsList.Remove(graphicsRotatedLine);
			}
			box.Invalidate();
		}

		private void tbAngle_TextChanged(object sender, EventArgs e)
		{
			if (float.TryParse(tbAngle.Text, out var angle))
			{
				float rotatedAngle = (float)(Math.Atan(slope) / Math.PI * 180.0) + angle;
				float rotatedSlope = (float)Math.Tan(rotatedAngle * (float)Math.PI / 180f);
				if (Math.Abs(graphicsRotatedLine.Slope - rotatedSlope) > float.Epsilon)
				{
					graphicsRotatedLine.MoveHandleTo(new PointF(rotatedSlope, 0f), 1);
				}
			}
			else if (Math.Abs(graphicsRotatedLine.Slope - graphicsLine.Slope) > float.Epsilon)
			{
				graphicsRotatedLine.MoveHandleTo(new PointF(graphicsLine.Slope, 0f), 1);
			}
			box.Invalidate();
		}

		private void graphicsReferenceLine_OnGraphicsChanged(object sender, EventArgs e)
		{
			if (graphicsReferenceLine.StartPoint == graphicsReferenceLine.EndPoint)
			{
				ReferencePoint1 = PointF.Empty;
				ReferencePoint2 = PointF.Empty;
				return;
			}
			PointF physicalStart = box.ConvertBoxPointFToPhysicalCoordinate(graphicsReferenceLine.StartPoint);
			PointF physicalEnd = box.ConvertBoxPointFToPhysicalCoordinate(graphicsReferenceLine.EndPoint);
			ReferencePoint1 = physicalStart;
			ReferencePoint2 = physicalEnd;
			float slope = (graphicsReferenceLine.EndPoint.Y - graphicsReferenceLine.StartPoint.Y) / (graphicsReferenceLine.EndPoint.X - graphicsReferenceLine.StartPoint.X);
			if (this.slope != slope)
			{
				this.slope = slope;
				graphicsLine.MoveHandleTo(new PointF(this.slope, 0f), 1);
			}
		}

		private void graphicsLine_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocation = (ReferencePoint3 = box.ConvertBoxPointFToPhysicalCoordinate(graphicsLine.Location));
			if (graphicsRotatedLine.Location != graphicsLine.Location)
			{
				graphicsRotatedLine.MoveHandleTo(graphicsLine.Location, 0);
			}
			string angleText = AngleText;
			float.TryParse(angleText, out var angle);
			float rotatedAngle = (float)(Math.Atan(slope) / Math.PI * 180.0) + angle;
			float rotatedSlope = (float)Math.Tan(rotatedAngle * (float)Math.PI / 180f);
			if (Math.Abs(graphicsRotatedLine.Slope - rotatedSlope) > float.Epsilon)
			{
				graphicsRotatedLine.MoveHandleTo(new PointF(rotatedSlope, 0f), 1);
			}
		}

		private void graphicsRotatedLine_OnGraphicsChanged(object sender, EventArgs e)
		{
			if (graphicsRotatedLine.SelectedHandle != -1 && graphicsLine.Location != graphicsRotatedLine.Location)
			{
				graphicsLine.MoveHandleTo(graphicsRotatedLine.Location, 0);
			}
		}

		private void box_MouseDown(object sender, MouseEventArgs e)
		{
			if (drawReferencePointIndex == 1)
			{
				if (graphicsReferenceLine.StartPoint != graphicsReferenceLine.EndPoint)
				{
					drawReferencePointIndex = 0;
					isDrawingReferenceLine = false;
					return;
				}
				graphicsReferenceLine.MoveHandleTo(e.Location, 1);
				graphicsReferenceLine.MoveHandleTo(e.Location, 2);
				isDrawingReferenceLine = true;
				if (!box.GraphicsList.Contains(graphicsReferenceLine))
				{
					box.GraphicsList.Add(graphicsReferenceLine);
				}
				if (!box.GraphicsList.Contains(graphicsLine))
				{
					box.GraphicsList.Add(graphicsLine);
					graphicsLine.MoveHandleTo(e.Location, 0);
					if (!box.GraphicsList.Contains(graphicsRotatedLine))
					{
						box.GraphicsList.Add(graphicsRotatedLine);
					}
				}
			}
			else
			{
				if (drawReferencePointIndex != 2)
				{
					return;
				}
				drawReferencePointIndex = 0;
				graphicsLine.MoveHandleTo(e.Location, 0);
				if (box.GraphicsList.Contains(graphicsReferenceLine) && !box.GraphicsList.Contains(graphicsLine))
				{
					box.GraphicsList.Add(graphicsLine);
					if (!box.GraphicsList.Contains(graphicsRotatedLine))
					{
						box.GraphicsList.Add(graphicsRotatedLine);
					}
				}
			}
		}

		private void box_MouseMove(object sender, MouseEventArgs e)
		{
			if (drawReferencePointIndex == 1 && isDrawingReferenceLine)
			{
				graphicsReferenceLine.MoveHandleTo(e.Location, 2);
			}
		}

		private void box_MouseUp(object sender, MouseEventArgs e)
		{
			if (drawReferencePointIndex == 1 && graphicsReferenceLine.StartPoint != graphicsReferenceLine.EndPoint)
			{
				graphicsReferenceLine.MoveHandleTo(e.Location, 2);
				drawReferencePointIndex = 0;
				isDrawingReferenceLine = false;
			}
		}

		private void LoadLanguageResources()
		{
			lbReferenceLine.Text = ResourcesManager.Resources.R_Pattern_ReferenceLine;
			lbReferencePoint.Text = ResourcesManager.Resources.R_Pattern_ReferencePoint;
			lbFirstOffset.Text = ResourcesManager.Resources.R_Pattern_FirstOffset;
			lbAngle.Text = ResourcesManager.Resources.R_Pattern_Angle;
			lbInterval.Text = ResourcesManager.Resources.R_Pattern_Interval;
			lbOffset.Text = ResourcesManager.Resources.R_Pattern_Offset;
			lbNumber.Text = ResourcesManager.Resources.R_Pattern_Number;
		}

		public void Initialize(MainForm owner)
		{
			this.owner = owner;
			box = this.owner.imageBox;
			box.MouseDown += box_MouseDown;
			box.MouseMove += box_MouseMove;
			box.MouseUp += box_MouseUp;
			LoadLanguageResources();
		}

		public void SetGraphicsVisible(bool visible)
		{
			graphicsReferenceLine.Visible = visible;
			graphicsLine.Visible = visible;
			graphicsRotatedLine.Visible = visible;
			box.Invalidate();
		}

		public void Reset()
		{
			tbReferencePoint1X.Text = string.Empty;
			tbReferencePoint1Y.Text = string.Empty;
			tbReferencePoint2X.Text = string.Empty;
			tbReferencePoint2Y.Text = string.Empty;
			tbReferencePoint3X.Text = string.Empty;
			tbReferencePoint3Y.Text = string.Empty;
			tbOffsetReferencePoint.Text = string.Empty;
			tbAngle.Text = string.Empty;
			tbInterval.Text = string.Empty;
			tbOffsetLine.Text = string.Empty;
			tbNumberOfPoints.Text = string.Empty;
			box.GraphicsList.Remove(graphicsReferenceLine);
			box.GraphicsList.Remove(graphicsLine);
			box.GraphicsList.Remove(graphicsRotatedLine);
			box.Invalidate();
		}

		public List<PointF> GeneratePoints()
		{
			try
			{
				List<PointF> pointList = new List<PointF>();
				PointF location = box.ConvertBoxPointFToPhysicalCoordinate(graphicsLine.Location);
				float angle = (float)Math.Atan(slope) + float.Parse(tbAngle.Text) / 180f * (float)Math.PI;
				float interval = float.Parse(tbInterval.Text);
				float offsetLine = float.Parse(tbOffsetLine.Text);
				float offsetReferencePoint = float.Parse(tbOffsetReferencePoint.Text);
				float number = float.Parse(tbNumberOfPoints.Text);
				float sinA = (float)Math.Sin(angle);
				float cosA = (float)Math.Cos(angle);
				float offsetReferencePointX = offsetReferencePoint * cosA;
				float offsetReferencePointY = offsetReferencePoint * sinA;
				float intervalX = interval * cosA;
				float intervalY = interval * sinA;
				float offsetLineX = 0f - offsetLine * sinA;
				float offsetLineY = offsetLine * cosA;
				for (int i = 1; (float)i <= number; i++)
				{
					float x = location.X + offsetReferencePointX + (float)(i - 1) * intervalX;
					float y = location.Y + offsetReferencePointY + (float)(i - 1) * intervalY;
					if (i % 4 == 0)
					{
						x -= offsetLineX;
						y -= offsetLineY;
					}
					else if (i % 2 == 0)
					{
						x += offsetLineX;
						y += offsetLineY;
					}
					PointF point = new PointF(x, y);
					pointList.Add(point);
				}
				return pointList;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to generate point set！");
				throw;
			}
		}

		public void GeneratePointsAndDepth(out List<PointF> pointList, out List<float> depthList)
		{
			try
			{
				pointList = new List<PointF>();
				depthList = new List<float>();
				PointF location = box.ConvertBoxPointFToPhysicalCoordinate(graphicsLine.Location);
				float angle = (float)Math.Atan(slope) + float.Parse(tbAngle.Text) / 180f * (float)Math.PI;
				float interval = float.Parse(tbInterval.Text);
				float offsetLine = float.Parse(tbOffsetLine.Text);
				float offsetReferencePoint = float.Parse(tbOffsetReferencePoint.Text);
				float number = float.Parse(tbNumberOfPoints.Text);
				float sinA = (float)Math.Sin(angle);
				float cosA = (float)Math.Cos(angle);
				float offsetReferencePointX = offsetReferencePoint * cosA;
				float offsetReferencePointY = offsetReferencePoint * sinA;
				float intervalX = interval * cosA;
				float intervalY = interval * sinA;
				float offsetLineX = 0f - offsetLine * sinA;
				float offsetLineY = offsetLine * cosA;
				for (int i = 1; (float)i <= number; i++)
				{
					float x = location.X + offsetReferencePointX + (float)(i - 1) * intervalX;
					float y = location.Y + offsetReferencePointY + (float)(i - 1) * intervalY;
					if (i % 4 == 0)
					{
						x -= offsetLineX;
						y -= offsetLineY;
					}
					else if (i % 2 == 0)
					{
						x += offsetLineX;
						y += offsetLineY;
					}
					PointF point = new PointF(x, y);
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

		private void SetReferencePoint1X(string value)
		{
			if (!tbReferencePoint1X.Focused)
			{
				tbReferencePoint1X.Text = value;
			}
		}

		private void SetReferencePoint1Y(string value)
		{
			if (!tbReferencePoint1Y.Focused)
			{
				tbReferencePoint1Y.Text = value;
			}
		}

		private void SetReferencePoint2X(string value)
		{
			if (!tbReferencePoint2X.Focused)
			{
				tbReferencePoint2X.Text = value;
			}
		}

		private void SetReferencePoint2Y(string value)
		{
			if (!tbReferencePoint2Y.Focused)
			{
				tbReferencePoint2Y.Text = value;
			}
		}

		private void SetReferencePoint3X(string value)
		{
			if (!tbReferencePoint3X.Focused)
			{
				tbReferencePoint3X.Text = value;
			}
		}

		private void SetReferencePoint3Y(string value)
		{
			if (!tbReferencePoint3Y.Focused)
			{
				tbReferencePoint3Y.Text = value;
			}
		}

		private string GetAngle()
		{
			return tbAngle.Text;
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
            this.lbReferenceLine = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbReferencePoint = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbFirstOffset = new System.Windows.Forms.Label();
            this.lbAngle = new System.Windows.Forms.Label();
            this.lbInterval = new System.Windows.Forms.Label();
            this.lbNumber = new System.Windows.Forms.Label();
            this.lbOffset = new System.Windows.Forms.Label();
            this.btnSetReferenceLine = new System.Windows.Forms.Button();
            this.btnSetReferencePoint = new System.Windows.Forms.Button();
            this.tbReferencePoint1X = new System.Windows.Forms.TextBox();
            this.tbReferencePoint1Y = new System.Windows.Forms.TextBox();
            this.tbReferencePoint2X = new System.Windows.Forms.TextBox();
            this.tbReferencePoint2Y = new System.Windows.Forms.TextBox();
            this.tbReferencePoint3X = new System.Windows.Forms.TextBox();
            this.tbReferencePoint3Y = new System.Windows.Forms.TextBox();
            this.tbOffsetReferencePoint = new System.Windows.Forms.TextBox();
            this.tbAngle = new System.Windows.Forms.TextBox();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.tbOffsetLine = new System.Windows.Forms.TextBox();
            this.tbNumberOfPoints = new System.Windows.Forms.TextBox();
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
            this.tableLayoutPanel1.Controls.Add(this.lbReferenceLine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbReferencePoint, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbFirstOffset, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbAngle, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbInterval, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbNumber, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbOffset, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferenceLine, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferencePoint, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePoint1X, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePoint1Y, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePoint2X, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePoint2Y, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePoint3X, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbReferencePoint3Y, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbOffsetReferencePoint, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbAngle, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbInterval, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbOffsetLine, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbNumberOfPoints, 2, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 200);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // lbReferenceLine
            // 
            this.lbReferenceLine.AutoSize = true;
            this.lbReferenceLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferenceLine.Location = new System.Drawing.Point(2, 0);
            this.lbReferenceLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferenceLine.Name = "lbReferenceLine";
            this.lbReferenceLine.Size = new System.Drawing.Size(96, 33);
            this.lbReferenceLine.TabIndex = 0;
            this.lbReferenceLine.Text = "Reference Line";
            this.lbReferenceLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(102, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 33);
            this.label1.TabIndex = 27;
            this.label1.Text = "X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(287, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 33);
            this.label2.TabIndex = 13;
            this.label2.Text = "Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(287, 33);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 33);
            this.label10.TabIndex = 31;
            this.label10.Text = "Y";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbReferencePoint
            // 
            this.lbReferencePoint.AutoSize = true;
            this.lbReferencePoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferencePoint.Location = new System.Drawing.Point(2, 66);
            this.lbReferencePoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferencePoint.Name = "lbReferencePoint";
            this.lbReferencePoint.Size = new System.Drawing.Size(96, 33);
            this.lbReferencePoint.TabIndex = 30;
            this.lbReferencePoint.Text = "Reference Point";
            this.lbReferencePoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(102, 33);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 33);
            this.label11.TabIndex = 32;
            this.label11.Text = "X";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(287, 66);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 33);
            this.label12.TabIndex = 33;
            this.label12.Text = "Y";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(102, 66);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 33);
            this.label13.TabIndex = 34;
            this.label13.Text = "X";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFirstOffset
            // 
            this.lbFirstOffset.AutoSize = true;
            this.lbFirstOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFirstOffset.Location = new System.Drawing.Point(102, 99);
            this.lbFirstOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFirstOffset.Name = "lbFirstOffset";
            this.lbFirstOffset.Size = new System.Drawing.Size(81, 33);
            this.lbFirstOffset.TabIndex = 22;
            this.lbFirstOffset.Text = "First Offset";
            this.lbFirstOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAngle
            // 
            this.lbAngle.AutoSize = true;
            this.lbAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAngle.Location = new System.Drawing.Point(287, 99);
            this.lbAngle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAngle.Name = "lbAngle";
            this.lbAngle.Size = new System.Drawing.Size(81, 33);
            this.lbAngle.TabIndex = 41;
            this.lbAngle.Text = "Angle";
            this.lbAngle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbInterval
            // 
            this.lbInterval.AutoSize = true;
            this.lbInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInterval.Location = new System.Drawing.Point(102, 132);
            this.lbInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInterval.Name = "lbInterval";
            this.lbInterval.Size = new System.Drawing.Size(81, 33);
            this.lbInterval.TabIndex = 20;
            this.lbInterval.Text = "Interval";
            this.lbInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNumber.Location = new System.Drawing.Point(102, 165);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(81, 35);
            this.lbNumber.TabIndex = 24;
            this.lbNumber.Text = "Number";
            this.lbNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbOffset
            // 
            this.lbOffset.AutoSize = true;
            this.lbOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOffset.Location = new System.Drawing.Point(287, 132);
            this.lbOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOffset.Name = "lbOffset";
            this.lbOffset.Size = new System.Drawing.Size(81, 33);
            this.lbOffset.TabIndex = 21;
            this.lbOffset.Text = "Offset";
            this.lbOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetReferenceLine
            // 
            this.btnSetReferenceLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferenceLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetReferenceLine.Location = new System.Drawing.Point(472, 3);
            this.btnSetReferenceLine.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetReferenceLine.Name = "btnSetReferenceLine";
            this.btnSetReferenceLine.Size = new System.Drawing.Size(26, 26);
            this.btnSetReferenceLine.TabIndex = 42;
            this.btnSetReferenceLine.UseVisualStyleBackColor = true;
            this.btnSetReferenceLine.Click += new System.EventHandler(this.btnSetReferenceLine_Click);
            // 
            // btnSetReferencePoint
            // 
            this.btnSetReferencePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferencePoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetReferencePoint.Location = new System.Drawing.Point(472, 69);
            this.btnSetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetReferencePoint.Name = "btnSetReferencePoint";
            this.btnSetReferencePoint.Size = new System.Drawing.Size(26, 26);
            this.btnSetReferencePoint.TabIndex = 44;
            this.btnSetReferencePoint.UseVisualStyleBackColor = true;
            this.btnSetReferencePoint.Click += new System.EventHandler(this.btnSetReferencePoint_Click);
            // 
            // tbReferencePoint1X
            // 
            this.tbReferencePoint1X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePoint1X.Location = new System.Drawing.Point(187, 4);
            this.tbReferencePoint1X.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePoint1X.Name = "tbReferencePoint1X";
            this.tbReferencePoint1X.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePoint1X.TabIndex = 45;
            this.tbReferencePoint1X.TextChanged += new System.EventHandler(this.referenceLine_TextChanged);
            // 
            // tbReferencePoint1Y
            // 
            this.tbReferencePoint1Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePoint1Y.Location = new System.Drawing.Point(372, 4);
            this.tbReferencePoint1Y.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePoint1Y.Name = "tbReferencePoint1Y";
            this.tbReferencePoint1Y.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePoint1Y.TabIndex = 46;
            this.tbReferencePoint1Y.TextChanged += new System.EventHandler(this.referenceLine_TextChanged);
            // 
            // tbReferencePoint2X
            // 
            this.tbReferencePoint2X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePoint2X.Location = new System.Drawing.Point(187, 37);
            this.tbReferencePoint2X.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePoint2X.Name = "tbReferencePoint2X";
            this.tbReferencePoint2X.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePoint2X.TabIndex = 47;
            this.tbReferencePoint2X.TextChanged += new System.EventHandler(this.referenceLine_TextChanged);
            // 
            // tbReferencePoint2Y
            // 
            this.tbReferencePoint2Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePoint2Y.Location = new System.Drawing.Point(372, 37);
            this.tbReferencePoint2Y.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePoint2Y.Name = "tbReferencePoint2Y";
            this.tbReferencePoint2Y.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePoint2Y.TabIndex = 48;
            this.tbReferencePoint2Y.TextChanged += new System.EventHandler(this.referenceLine_TextChanged);
            // 
            // tbReferencePoint3X
            // 
            this.tbReferencePoint3X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePoint3X.Location = new System.Drawing.Point(187, 70);
            this.tbReferencePoint3X.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePoint3X.Name = "tbReferencePoint3X";
            this.tbReferencePoint3X.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePoint3X.TabIndex = 49;
            this.tbReferencePoint3X.TextChanged += new System.EventHandler(this.referencePoint_TextChanged);
            // 
            // tbReferencePoint3Y
            // 
            this.tbReferencePoint3Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferencePoint3Y.Location = new System.Drawing.Point(372, 70);
            this.tbReferencePoint3Y.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbReferencePoint3Y.Name = "tbReferencePoint3Y";
            this.tbReferencePoint3Y.Size = new System.Drawing.Size(96, 30);
            this.tbReferencePoint3Y.TabIndex = 50;
            // 
            // tbOffsetReferencePoint
            // 
            this.tbOffsetReferencePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOffsetReferencePoint.Location = new System.Drawing.Point(187, 103);
            this.tbOffsetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbOffsetReferencePoint.Name = "tbOffsetReferencePoint";
            this.tbOffsetReferencePoint.Size = new System.Drawing.Size(96, 30);
            this.tbOffsetReferencePoint.TabIndex = 51;
            // 
            // tbAngle
            // 
            this.tbAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAngle.Location = new System.Drawing.Point(372, 103);
            this.tbAngle.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbAngle.Name = "tbAngle";
            this.tbAngle.Size = new System.Drawing.Size(96, 30);
            this.tbAngle.TabIndex = 52;
            this.tbAngle.TextChanged += new System.EventHandler(this.tbAngle_TextChanged);
            // 
            // tbInterval
            // 
            this.tbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInterval.Location = new System.Drawing.Point(187, 136);
            this.tbInterval.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(96, 30);
            this.tbInterval.TabIndex = 53;
            // 
            // tbOffsetLine
            // 
            this.tbOffsetLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOffsetLine.Location = new System.Drawing.Point(372, 136);
            this.tbOffsetLine.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbOffsetLine.Name = "tbOffsetLine";
            this.tbOffsetLine.Size = new System.Drawing.Size(96, 30);
            this.tbOffsetLine.TabIndex = 54;
            // 
            // tbNumberOfPoints
            // 
            this.tbNumberOfPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNumberOfPoints.Location = new System.Drawing.Point(187, 169);
            this.tbNumberOfPoints.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbNumberOfPoints.Name = "tbNumberOfPoints";
            this.tbNumberOfPoints.Size = new System.Drawing.Size(96, 30);
            this.tbNumberOfPoints.TabIndex = 55;
            // 
            // PatternSlashControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PatternSlashControl";
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
