using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AIO_Client.Properties;
using Labtt.Data;
using Labtt.DrawArea;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class PatternCircleExtensionControl : UserControl, IPatternControl
	{
		private MainForm owner;

		private ImageBox box;

		private GraphicsLineByTwoPoint graphicsReferenceLine = null;

		private GraphicsCircle graphicsCircle = null;

		private GraphicsSpot graphicsSpot1 = null;

		private GraphicsSpot graphicsSpot2 = null;

		private GraphicsSpot graphicsSpot3 = null;

		private GraphicsLineBySlope graphicsLine;

		private int drawReferencePointIndex;

		private bool isDrawingReferenceLine;

		private float slope = 0f;

		private SetString setReferenceLinePoint1X = null;

		private SetString setReferenceLinePoint1Y = null;

		private SetString setReferenceLinePoint2X = null;

		private SetString setReferenceLinePoint2Y = null;

		private SetString setReferenceCirclePoint1X = null;

		private SetString setReferenceCirclePoint1Y = null;

		private SetString setReferenceCirclePoint2X = null;

		private SetString setReferenceCirclePoint2Y = null;

		private SetString setReferenceCirclePoint3X = null;

		private SetString setReferenceCirclePoint3Y = null;

		private SetString setCenterX = null;

		private SetString setCenterY = null;

		private GetString getAngle = null;

		private IContainer components = null;
        private KryptonTableLayoutPanel tableLayoutPanel1;
        private KryptonLabel lbReferenceLine;

		private KryptonLabel label1;

		private KryptonLabel label2;

		private KryptonLabel label10;

		private KryptonLabel lbReferenceCircle1;

		private KryptonLabel label11;

		private KryptonLabel label12;

		private KryptonLabel label13;

		private KryptonLabel lbFirstOffset;

		private KryptonLabel lbAngle;

		private KryptonLabel lbInterval;

		private KryptonLabel lbNumber;

		private KryptonLabel lbOffset;

		private KryptonButton btnSetReferenceLine;

		private KryptonButton btnSetReferenceCircle1;

		private KryptonTextBox tbReferenceLine1X;

		private KryptonTextBox tbReferenceLine1Y;

		private KryptonTextBox tbReferenceLine2X;

		private KryptonTextBox tbReferenceLine2Y;

		private KryptonTextBox tbReferenceCircle1X;

		private KryptonTextBox tbReferenceCircle1Y;

		private KryptonTextBox tbOffsetReferencePoint;

		private KryptonTextBox tbAngle;

		private KryptonTextBox tbInterval;

		private KryptonTextBox tbOffsetLine;

		private KryptonTextBox tbNumberOfPoints;

		private KryptonLabel label3;

		private KryptonLabel label4;

		private KryptonLabel label5;

		private KryptonLabel label6;

		private KryptonLabel label8;

		private KryptonLabel label7;

		private KryptonTextBox tbReferenceCircle2X;

		private KryptonTextBox tbReferenceCircle2Y;

		private KryptonTextBox tbReferenceCircle3X;

		private KryptonTextBox tbReferenceCircle3Y;

		private KryptonTextBox tbCenterX;

		private KryptonTextBox tbCenterY;

		private KryptonLabel lbCenter;

		private KryptonLabel lbReferenceCircle3;

		private KryptonLabel lbReferenceCircle2;

		private KryptonButton btnSetReferenceCircle2;
        private KryptonPalette kp1;
        private KryptonButton btnSetReferenceCircle3;

		private PointF ReferenceLinePoint1
		{
			set
			{
				Invoke(setReferenceLinePoint1X, value.X.ToString("F5"));
				Invoke(setReferenceLinePoint1Y, value.Y.ToString("F5"));
			}
		}

		private PointF ReferenceLinePoint2
		{
			set
			{
				Invoke(setReferenceLinePoint2X, value.X.ToString("F5"));
				Invoke(setReferenceLinePoint2Y, value.Y.ToString("F5"));
			}
		}

		private PointF ReferenceCirclePoint1
		{
			set
			{
				Invoke(setReferenceCirclePoint1X, value.X.ToString("F5"));
				Invoke(setReferenceCirclePoint1Y, value.Y.ToString("F5"));
			}
		}

		private PointF ReferenceCirclePoint2
		{
			set
			{
				Invoke(setReferenceCirclePoint2X, value.X.ToString("F5"));
				Invoke(setReferenceCirclePoint2Y, value.Y.ToString("F5"));
			}
		}

		private PointF ReferenceCirclePoint3
		{
			set
			{
				Invoke(setReferenceCirclePoint3X, value.X.ToString("F5"));
				Invoke(setReferenceCirclePoint3Y, value.Y.ToString("F5"));
			}
		}

		private PointF Center
		{
			set
			{
				Invoke(setCenterX, value.X.ToString("F5"));
				Invoke(setCenterY, value.Y.ToString("F5"));
			}
		}

		private string AngleText => Invoke(getAngle).ToString();

		public PatternCircleExtensionControl()
		{
			InitializeComponent();
			setReferenceLinePoint1X = SetReferenceLinePoint1X;
			setReferenceLinePoint1Y = SetReferenceLinePoint1Y;
			setReferenceLinePoint2X = SetReferenceLinePoint2X;
			setReferenceLinePoint2Y = SetReferenceLinePoint2Y;
			setReferenceCirclePoint1X = SetReferenceCirclePoint1X;
			setReferenceCirclePoint1Y = SetReferenceCirclePoint1Y;
			setReferenceCirclePoint2X = SetReferenceCirclePoint2X;
			setReferenceCirclePoint2Y = SetReferenceCirclePoint2Y;
			setReferenceCirclePoint3X = SetReferenceCirclePoint3X;
			setReferenceCirclePoint3Y = SetReferenceCirclePoint3Y;
			setCenterX = SetCenterX;
			setCenterY = SetCenterY;
			getAngle = GetAngle;
			graphicsReferenceLine = new GraphicsLineByTwoPoint();
			graphicsReferenceLine.OnGraphicsChanged += graphicsReferenceLine_OnGraphicsChanged;
			graphicsCircle = new GraphicsCircle();
			graphicsCircle.Selectable = false;
			graphicsCircle.OnGraphicsChanged += graphicsCircle_OnGraphicsChanged;
			graphicsSpot1 = new GraphicsSpot(0f, 0f, 3f);
			graphicsSpot1.OnGraphicsChanged += graphicsSpot1_OnGraphicsChanged;
			graphicsSpot2 = new GraphicsSpot(0f, 0f, 3f);
			graphicsSpot2.OnGraphicsChanged += graphicsSpot2_OnGraphicsChanged;
			graphicsSpot3 = new GraphicsSpot(0f, 0f, 3f);
			graphicsSpot3.OnGraphicsChanged += graphicsSpot3_OnGraphicsChanged;
			graphicsLine = new GraphicsLineBySlope();
			graphicsLine.Selectable = false;
			graphicsLine.Color = Color.RoyalBlue;
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

		private void btnSetReferenceCircle1_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				drawReferencePointIndex = 2;
			}
		}

		private void btnSetReferenceCircle2_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				drawReferencePointIndex = 3;
			}
		}

		private void btnSetReferenceCircle3_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				drawReferencePointIndex = 4;
			}
		}

		private void graphicsReferenceLine_OnGraphicsChanged(object sender, EventArgs e)
		{
			if (graphicsReferenceLine.StartPoint == graphicsReferenceLine.EndPoint)
			{
				ReferenceLinePoint1 = PointF.Empty;
				ReferenceLinePoint2 = PointF.Empty;
				return;
			}
			PointF physicalStart = box.ConvertBoxPointFToPhysicalCoordinate(graphicsReferenceLine.StartPoint);
			PointF physicalEnd = box.ConvertBoxPointFToPhysicalCoordinate(graphicsReferenceLine.EndPoint);
			ReferenceLinePoint1 = physicalStart;
			ReferenceLinePoint2 = physicalEnd;
			float slope = (graphicsReferenceLine.EndPoint.Y - graphicsReferenceLine.StartPoint.Y) / (graphicsReferenceLine.EndPoint.X - graphicsReferenceLine.StartPoint.X);
			if (this.slope != slope)
			{
				this.slope = slope;
				string angleText = AngleText;
				float.TryParse(angleText, out var angle);
				float rotatedAngle = (float)(Math.Atan(this.slope) / Math.PI * 180.0) + angle;
				float rotatedSlope = (float)Math.Tan(rotatedAngle * (float)Math.PI / 180f);
				if (Math.Abs(graphicsLine.Slope - rotatedSlope) > float.Epsilon)
				{
					graphicsLine.MoveHandleTo(new PointF(rotatedSlope, 0f), 1);
				}
			}
		}

		private void graphicsCircle_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocation = (Center = box.ConvertBoxPointFToPhysicalCoordinate(graphicsCircle.Location));
		}

		private void graphicsSpot1_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocationSpot1 = (ReferenceCirclePoint1 = box.ConvertBoxPointFToPhysicalCoordinate(graphicsSpot1.Location));
			if (box.GraphicsList.Contains(graphicsSpot2) && box.GraphicsList.Contains(graphicsSpot3))
			{
				UpdateCircleInfo(graphicsSpot1.Location, graphicsSpot2.Location, graphicsSpot3.Location);
			}
		}

		private void graphicsSpot2_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocationSpot2 = (ReferenceCirclePoint2 = box.ConvertBoxPointFToPhysicalCoordinate(graphicsSpot2.Location));
			if (box.GraphicsList.Contains(graphicsSpot1) && box.GraphicsList.Contains(graphicsSpot3))
			{
				UpdateCircleInfo(graphicsSpot1.Location, graphicsSpot2.Location, graphicsSpot3.Location);
			}
		}

		private void graphicsSpot3_OnGraphicsChanged(object sender, EventArgs e)
		{
			PointF physicalLocationSpot3 = (ReferenceCirclePoint3 = box.ConvertBoxPointFToPhysicalCoordinate(graphicsSpot3.Location));
			if (box.GraphicsList.Contains(graphicsSpot1) && box.GraphicsList.Contains(graphicsSpot2))
			{
				UpdateCircleInfo(graphicsSpot1.Location, graphicsSpot2.Location, graphicsSpot3.Location);
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
			}
			else
			{
				if (drawReferencePointIndex <= 1)
				{
					return;
				}
				GraphicsSpot graphicsSpot = null;
				if (drawReferencePointIndex == 2)
				{
					graphicsSpot = graphicsSpot1;
				}
				else if (drawReferencePointIndex == 3)
				{
					graphicsSpot = graphicsSpot2;
				}
				else if (drawReferencePointIndex == 4)
				{
					graphicsSpot = graphicsSpot3;
				}
				drawReferencePointIndex++;
				if (graphicsSpot != null)
				{
					graphicsSpot.MoveHandleTo(e.Location, 0);
					if (drawReferencePointIndex > 4)
					{
						drawReferencePointIndex = 0;
					}
					if (!box.GraphicsList.Contains(graphicsSpot))
					{
						box.GraphicsList.Add(graphicsSpot);
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

		private void tbReferenceLine_TextChanged(object sender, EventArgs e)
		{
			if (!tbReferenceLine1X.Focused && !tbReferenceLine1Y.Focused && !tbReferenceLine2X.Focused && !tbReferenceLine2Y.Focused)
			{
				return;
			}
			if (float.TryParse(tbReferenceLine1X.Text, out var referenceLine1X) && float.TryParse(tbReferenceLine1Y.Text, out var referenceLine1Y) && float.TryParse(tbReferenceLine2X.Text, out var referenceLine2X) && float.TryParse(tbReferenceLine2Y.Text, out var referenceLine2Y) && (referenceLine1X != referenceLine2X || referenceLine1Y != referenceLine2Y))
			{
				PointF physicalStart = new PointF(referenceLine1X, referenceLine1Y);
				PointF physicalEnd = new PointF(referenceLine2X, referenceLine2Y);
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
			}
			else
			{
				box.GraphicsList.Remove(graphicsReferenceLine);
			}
			box.Invalidate();
		}

		private void tbReferenceCircle_TextChanged(object sender, EventArgs e)
		{
			if (sender == tbReferenceCircle1X || sender == tbReferenceCircle1Y)
			{
				if (!tbReferenceCircle1X.Focused && !tbReferenceCircle1Y.Focused)
				{
					return;
				}
				if (float.TryParse(tbReferenceCircle1X.Text, out var referenceCircle1X) && float.TryParse(tbReferenceCircle1Y.Text, out var referenceCircle1Y))
				{
					PointF physicalCircle1 = new PointF(referenceCircle1X, referenceCircle1Y);
					PointF boxCircle1 = box.ConvertPhysicalCoordinateToBoxPointF(physicalCircle1);
					if (graphicsSpot1.Location != boxCircle1)
					{
						graphicsSpot1.MoveHandleTo(boxCircle1, 0);
					}
					if (!box.GraphicsList.Contains(graphicsSpot1))
					{
						box.GraphicsList.Add(graphicsSpot1);
					}
				}
				else
				{
					box.GraphicsList.Remove(graphicsSpot1);
				}
			}
			else if (sender == tbReferenceCircle2X || sender == tbReferenceCircle2Y)
			{
				if (!tbReferenceCircle2X.Focused && !tbReferenceCircle2Y.Focused)
				{
					return;
				}
				if (float.TryParse(tbReferenceCircle2X.Text, out var referenceCircle2X) && float.TryParse(tbReferenceCircle2Y.Text, out var referenceCircle2Y))
				{
					PointF physicalCircle2 = new PointF(referenceCircle2X, referenceCircle2Y);
					PointF boxCircle2 = box.ConvertPhysicalCoordinateToBoxPointF(physicalCircle2);
					if (graphicsSpot2.Location != boxCircle2)
					{
						graphicsSpot2.MoveHandleTo(boxCircle2, 0);
					}
					if (!box.GraphicsList.Contains(graphicsSpot2))
					{
						box.GraphicsList.Add(graphicsSpot2);
					}
				}
				else
				{
					box.GraphicsList.Remove(graphicsSpot2);
				}
			}
			else
			{
				if ((sender != tbReferenceCircle3X && sender != tbReferenceCircle3Y) || (!tbReferenceCircle3X.Focused && !tbReferenceCircle3Y.Focused))
				{
					return;
				}
				if (float.TryParse(tbReferenceCircle3X.Text, out var referenceCircle3X) && float.TryParse(tbReferenceCircle3Y.Text, out var referenceCircle3Y))
				{
					PointF physicalCircle3 = new PointF(referenceCircle3X, referenceCircle3Y);
					PointF boxCircle3 = box.ConvertPhysicalCoordinateToBoxPointF(physicalCircle3);
					if (graphicsSpot3.Location != boxCircle3)
					{
						graphicsSpot3.MoveHandleTo(boxCircle3, 0);
					}
					if (!box.GraphicsList.Contains(graphicsSpot3))
					{
						box.GraphicsList.Add(graphicsSpot3);
					}
				}
				else
				{
					box.GraphicsList.Remove(graphicsSpot3);
				}
			}
			box.Invalidate();
		}

		private void tbAngle_TextChanged(object sender, EventArgs e)
		{
			float.TryParse(tbAngle.Text, out var angle);
			float rotatedAngle = (float)(Math.Atan(slope) / Math.PI * 180.0) + angle;
			float rotatedSlope = (float)Math.Tan(rotatedAngle * (float)Math.PI / 180f);
			if (Math.Abs(graphicsLine.Slope - rotatedSlope) > float.Epsilon)
			{
				graphicsLine.MoveHandleTo(new PointF(rotatedSlope, 0f), 1);
			}
			box.Invalidate();
		}

		private void LoadLanguageResources()
		{
			lbReferenceLine.Text = ResourcesManager.Resources.R_Pattern_ReferenceLine;
			lbReferenceCircle1.Text = ResourcesManager.Resources.R_Pattern_ReferenceCircle;
			lbReferenceCircle2.Text = ResourcesManager.Resources.R_Pattern_ReferenceCircle;
			lbReferenceCircle3.Text = ResourcesManager.Resources.R_Pattern_ReferenceCircle;
			lbCenter.Text = ResourcesManager.Resources.R_Pattern_CircleCenter;
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

		private void UpdateCircleInfo(PointF p1, PointF p2, PointF p3)
		{
			PointF center = PointF.Empty;
			float radius = 0f;
			FunctionSet.GetCircular(p1, p2, p3, ref radius, ref center);
			if (radius == 0f)
			{
				box.GraphicsList.Remove(graphicsCircle);
				box.GraphicsList.Remove(graphicsLine);
				Center = Point.Empty;
				return;
			}
			graphicsCircle.MoveHandleTo(center, 0);
			graphicsCircle.MoveHandleTo(new PointF(center.X + radius, center.Y), 1);
			graphicsLine.MoveHandleTo(center, 0);
			if (!box.GraphicsList.Contains(graphicsCircle))
			{
				box.GraphicsList.Add(graphicsCircle);
				box.GraphicsList.Add(graphicsLine);
			}
		}

		public void SetGraphicsVisible(bool visible)
		{
			graphicsReferenceLine.Visible = visible;
			graphicsCircle.Visible = visible;
			graphicsSpot1.Visible = visible;
			graphicsSpot2.Visible = visible;
			graphicsSpot3.Visible = visible;
			graphicsLine.Visible = visible;
			box.Invalidate();
		}

		public void Reset()
		{
			tbReferenceLine1X.Text = string.Empty;
			tbReferenceLine1Y.Text = string.Empty;
			tbReferenceLine2X.Text = string.Empty;
			tbReferenceLine2Y.Text = string.Empty;
			tbReferenceCircle1X.Text = string.Empty;
			tbReferenceCircle1Y.Text = string.Empty;
			tbReferenceCircle2X.Text = string.Empty;
			tbReferenceCircle2Y.Text = string.Empty;
			tbReferenceCircle3X.Text = string.Empty;
			tbReferenceCircle3Y.Text = string.Empty;
			tbCenterX.Text = string.Empty;
			tbCenterY.Text = string.Empty;
			tbOffsetReferencePoint.Text = string.Empty;
			tbInterval.Text = string.Empty;
			tbAngle.Text = string.Empty;
			tbOffsetLine.Text = string.Empty;
			tbNumberOfPoints.Text = string.Empty;
			drawReferencePointIndex = 0;
			box.GraphicsList.Remove(graphicsLine);
			box.GraphicsList.Remove(graphicsReferenceLine);
			box.GraphicsList.Remove(graphicsCircle);
			box.GraphicsList.Remove(graphicsSpot1);
			box.GraphicsList.Remove(graphicsSpot2);
			box.GraphicsList.Remove(graphicsSpot3);
		}

		public List<PointF> GeneratePoints()
		{
			try
			{
				List<PointF> pointList = new List<PointF>();
				float centerX = float.Parse(tbCenterX.Text);
				float centerY = float.Parse(tbCenterY.Text);
				float angle = (float)Math.Atan(slope) + float.Parse(tbAngle.Text) / 180f * (float)Math.PI;
				float interval = float.Parse(tbInterval.Text);
				float offsetLine = float.Parse(tbOffsetLine.Text);
				float offsetReferencePoint = float.Parse(tbOffsetReferencePoint.Text);
				float number = float.Parse(tbNumberOfPoints.Text);
				float circlePoint1X = float.Parse(tbReferenceCircle1X.Text);
				float circlePoint1Y = float.Parse(tbReferenceCircle1Y.Text);
				float radius = (float)Math.Sqrt((circlePoint1X - centerX) * (circlePoint1X - centerX) + (circlePoint1Y - centerY) * (circlePoint1Y - centerY));
				offsetReferencePoint += radius;
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
					float x = centerX + offsetReferencePointX + (float)(i - 1) * intervalX;
					float y = centerY + offsetReferencePointY + (float)(i - 1) * intervalY;
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
				float centerX = float.Parse(tbCenterX.Text);
				float centerY = float.Parse(tbCenterY.Text);
				float angle = (float)Math.Atan(slope) + float.Parse(tbAngle.Text) / 180f * (float)Math.PI;
				float interval = float.Parse(tbInterval.Text);
				float offsetLine = float.Parse(tbOffsetLine.Text);
				float offsetReferencePoint = float.Parse(tbOffsetReferencePoint.Text);
				float number = float.Parse(tbNumberOfPoints.Text);
				float circlePoint1X = float.Parse(tbReferenceCircle1X.Text);
				float circlePoint1Y = float.Parse(tbReferenceCircle1Y.Text);
				float radius = (float)Math.Sqrt((circlePoint1X - centerX) * (circlePoint1X - centerX) + (circlePoint1Y - centerY) * (circlePoint1Y - centerY));
				offsetReferencePoint += radius;
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
					float x = centerX + offsetReferencePointX + (float)(i - 1) * intervalX;
					float y = centerY + offsetReferencePointY + (float)(i - 1) * intervalY;
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
					depthList.Add(offsetReferencePoint + (float)(i - 1) * interval - radius);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to generate point set！");
				throw ex;
			}
		}

		private void SetReferenceLinePoint1X(string value)
		{
			if (!tbReferenceLine1X.Focused)
			{
				tbReferenceLine1X.Text = value;
			}
		}

		private void SetReferenceLinePoint1Y(string value)
		{
			if (!tbReferenceLine1Y.Focused)
			{
				tbReferenceLine1Y.Text = value;
			}
		}

		private void SetReferenceLinePoint2X(string value)
		{
			if (!tbReferenceLine2X.Focused)
			{
				tbReferenceLine2X.Text = value;
			}
		}

		private void SetReferenceLinePoint2Y(string value)
		{
			if (!tbReferenceLine2Y.Focused)
			{
				tbReferenceLine2Y.Text = value;
			}
		}

		private void SetReferenceCirclePoint1X(string value)
		{
			if (!tbReferenceCircle1X.Focused)
			{
				tbReferenceCircle1X.Text = value;
			}
		}

		private void SetReferenceCirclePoint1Y(string value)
		{
			if (!tbReferenceCircle1Y.Focused)
			{
				tbReferenceCircle1Y.Text = value;
			}
		}

		private void SetReferenceCirclePoint2X(string value)
		{
			if (!tbReferenceCircle2X.Focused)
			{
				tbReferenceCircle2X.Text = value;
			}
		}

		private void SetReferenceCirclePoint2Y(string value)
		{
			if (!tbReferenceCircle2Y.Focused)
			{
				tbReferenceCircle2Y.Text = value;
			}
		}

		private void SetReferenceCirclePoint3X(string value)
		{
			if (!tbReferenceCircle3X.Focused)
			{
				tbReferenceCircle3X.Text = value;
			}
		}

		private void SetReferenceCirclePoint3Y(string value)
		{
			if (!tbReferenceCircle3Y.Focused)
			{
				tbReferenceCircle3Y.Text = value;
			}
		}

		private void SetCenterX(string value)
		{
			if (!tbCenterX.Focused)
			{
				tbCenterX.Text = value;
			}
		}

		private void SetCenterY(string value)
		{
			if (!tbCenterY.Focused)
			{
				tbCenterY.Text = value;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternCircleExtensionControl));
            this.tableLayoutPanel1 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.lbCenter = new Krypton.Toolkit.KryptonLabel();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.lbReferenceCircle3 = new Krypton.Toolkit.KryptonLabel();
            this.lbReferenceCircle2 = new Krypton.Toolkit.KryptonLabel();
            this.lbReferenceLine = new Krypton.Toolkit.KryptonLabel();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.label10 = new Krypton.Toolkit.KryptonLabel();
            this.lbReferenceCircle1 = new Krypton.Toolkit.KryptonLabel();
            this.label11 = new Krypton.Toolkit.KryptonLabel();
            this.label12 = new Krypton.Toolkit.KryptonLabel();
            this.label13 = new Krypton.Toolkit.KryptonLabel();
            this.btnSetReferenceLine = new Krypton.Toolkit.KryptonButton();
            this.btnSetReferenceCircle1 = new Krypton.Toolkit.KryptonButton();
            this.tbReferenceLine1X = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceLine1Y = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceLine2X = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceLine2Y = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceCircle1X = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceCircle1Y = new Krypton.Toolkit.KryptonTextBox();
            this.lbNumber = new Krypton.Toolkit.KryptonLabel();
            this.lbFirstOffset = new Krypton.Toolkit.KryptonLabel();
            this.lbInterval = new Krypton.Toolkit.KryptonLabel();
            this.tbOffsetReferencePoint = new Krypton.Toolkit.KryptonTextBox();
            this.tbInterval = new Krypton.Toolkit.KryptonTextBox();
            this.tbNumberOfPoints = new Krypton.Toolkit.KryptonTextBox();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.label5 = new Krypton.Toolkit.KryptonLabel();
            this.label6 = new Krypton.Toolkit.KryptonLabel();
            this.label8 = new Krypton.Toolkit.KryptonLabel();
            this.label7 = new Krypton.Toolkit.KryptonLabel();
            this.tbReferenceCircle2X = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceCircle2Y = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceCircle3X = new Krypton.Toolkit.KryptonTextBox();
            this.tbReferenceCircle3Y = new Krypton.Toolkit.KryptonTextBox();
            this.tbCenterX = new Krypton.Toolkit.KryptonTextBox();
            this.tbCenterY = new Krypton.Toolkit.KryptonTextBox();
            this.btnSetReferenceCircle2 = new Krypton.Toolkit.KryptonButton();
            this.btnSetReferenceCircle3 = new Krypton.Toolkit.KryptonButton();
            this.tbOffsetLine = new Krypton.Toolkit.KryptonTextBox();
            this.tbAngle = new Krypton.Toolkit.KryptonTextBox();
            this.lbOffset = new Krypton.Toolkit.KryptonLabel();
            this.lbAngle = new Krypton.Toolkit.KryptonLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel1.BackgroundImage")));
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.lbCenter, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbReferenceCircle3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbReferenceCircle2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbReferenceLine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbReferenceCircle1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferenceLine, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferenceCircle1, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceLine1X, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceLine1Y, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceLine2X, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceLine2Y, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceCircle1X, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceCircle1Y, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbNumber, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbFirstOffset, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbInterval, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbOffsetReferencePoint, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbInterval, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbNumberOfPoints, 7, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceCircle2X, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceCircle2Y, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceCircle3X, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbReferenceCircle3Y, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbCenterX, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbCenterY, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferenceCircle2, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferenceCircle3, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbOffsetLine, 7, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbAngle, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbOffset, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbAngle, 6, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 200);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // lbCenter
            // 
            this.lbCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCenter.Location = new System.Drawing.Point(2, 165);
            this.lbCenter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCenter.Name = "lbCenter";
            this.lbCenter.Palette = this.kp1;
            this.lbCenter.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCenter.Size = new System.Drawing.Size(96, 35);
            this.lbCenter.TabIndex = 70;
            this.lbCenter.Values.Text = "Center";
            // 
            // lbReferenceCircle3
            // 
            this.lbReferenceCircle3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferenceCircle3.Location = new System.Drawing.Point(2, 132);
            this.lbReferenceCircle3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferenceCircle3.Name = "lbReferenceCircle3";
            this.lbReferenceCircle3.Palette = this.kp1;
            this.lbReferenceCircle3.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbReferenceCircle3.Size = new System.Drawing.Size(96, 33);
            this.lbReferenceCircle3.TabIndex = 69;
            this.lbReferenceCircle3.Values.Text = "Reference Circle3";
            // 
            // lbReferenceCircle2
            // 
            this.lbReferenceCircle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferenceCircle2.Location = new System.Drawing.Point(2, 99);
            this.lbReferenceCircle2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferenceCircle2.Name = "lbReferenceCircle2";
            this.lbReferenceCircle2.Palette = this.kp1;
            this.lbReferenceCircle2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbReferenceCircle2.Size = new System.Drawing.Size(96, 33);
            this.lbReferenceCircle2.TabIndex = 68;
            this.lbReferenceCircle2.Values.Text = "Reference Circle2";
            // 
            // lbReferenceLine
            // 
            this.lbReferenceLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferenceLine.Location = new System.Drawing.Point(2, 0);
            this.lbReferenceLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferenceLine.Name = "lbReferenceLine";
            this.lbReferenceLine.Palette = this.kp1;
            this.lbReferenceLine.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbReferenceLine.Size = new System.Drawing.Size(96, 33);
            this.lbReferenceLine.TabIndex = 0;
            this.lbReferenceLine.Values.Text = "Reference Line";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(102, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Palette = this.kp1;
            this.label1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label1.Size = new System.Drawing.Size(31, 33);
            this.label1.TabIndex = 27;
            this.label1.Values.Text = "X";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(212, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Palette = this.kp1;
            this.label2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label2.Size = new System.Drawing.Size(31, 33);
            this.label2.TabIndex = 13;
            this.label2.Values.Text = "Y";
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(212, 33);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Palette = this.kp1;
            this.label10.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label10.Size = new System.Drawing.Size(31, 33);
            this.label10.TabIndex = 31;
            this.label10.Values.Text = "Y";
            // 
            // lbReferenceCircle1
            // 
            this.lbReferenceCircle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbReferenceCircle1.Location = new System.Drawing.Point(2, 66);
            this.lbReferenceCircle1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbReferenceCircle1.Name = "lbReferenceCircle1";
            this.lbReferenceCircle1.Palette = this.kp1;
            this.lbReferenceCircle1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbReferenceCircle1.Size = new System.Drawing.Size(96, 33);
            this.lbReferenceCircle1.TabIndex = 30;
            this.lbReferenceCircle1.Values.Text = "Reference Circle1";
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(102, 33);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Palette = this.kp1;
            this.label11.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label11.Size = new System.Drawing.Size(31, 33);
            this.label11.TabIndex = 32;
            this.label11.Values.Text = "X";
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(212, 66);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Palette = this.kp1;
            this.label12.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label12.Size = new System.Drawing.Size(31, 33);
            this.label12.TabIndex = 33;
            this.label12.Values.Text = "Y";
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(102, 66);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Palette = this.kp1;
            this.label13.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label13.Size = new System.Drawing.Size(31, 33);
            this.label13.TabIndex = 34;
            this.label13.Values.Text = "X";
            // 
            // btnSetReferenceLine
            // 
            this.btnSetReferenceLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferenceLine.Location = new System.Drawing.Point(322, 5);
            this.btnSetReferenceLine.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnSetReferenceLine.Name = "btnSetReferenceLine";
            this.btnSetReferenceLine.Palette = this.kp1;
            this.btnSetReferenceLine.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSetReferenceLine.Size = new System.Drawing.Size(26, 23);
            this.btnSetReferenceLine.TabIndex = 41;
            this.btnSetReferenceLine.Click += new System.EventHandler(this.btnSetReferenceLine_Click);
            // 
            // btnSetReferenceCircle1
            // 
            this.btnSetReferenceCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferenceCircle1.Location = new System.Drawing.Point(322, 71);
            this.btnSetReferenceCircle1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnSetReferenceCircle1.Name = "btnSetReferenceCircle1";
            this.btnSetReferenceCircle1.Palette = this.kp1;
            this.btnSetReferenceCircle1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSetReferenceCircle1.Size = new System.Drawing.Size(26, 23);
            this.btnSetReferenceCircle1.TabIndex = 42;
            this.btnSetReferenceCircle1.Click += new System.EventHandler(this.btnSetReferenceCircle1_Click);
            // 
            // tbReferenceLine1X
            // 
            this.tbReferenceLine1X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceLine1X.Location = new System.Drawing.Point(137, 6);
            this.tbReferenceLine1X.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceLine1X.Name = "tbReferenceLine1X";
            this.tbReferenceLine1X.Palette = this.kp1;
            this.tbReferenceLine1X.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceLine1X.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceLine1X.TabIndex = 45;
            this.tbReferenceLine1X.TextChanged += new System.EventHandler(this.tbReferenceLine_TextChanged);
            // 
            // tbReferenceLine1Y
            // 
            this.tbReferenceLine1Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceLine1Y.Location = new System.Drawing.Point(247, 6);
            this.tbReferenceLine1Y.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceLine1Y.Name = "tbReferenceLine1Y";
            this.tbReferenceLine1Y.Palette = this.kp1;
            this.tbReferenceLine1Y.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceLine1Y.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceLine1Y.TabIndex = 46;
            this.tbReferenceLine1Y.TextChanged += new System.EventHandler(this.tbReferenceLine_TextChanged);
            // 
            // tbReferenceLine2X
            // 
            this.tbReferenceLine2X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceLine2X.Location = new System.Drawing.Point(137, 39);
            this.tbReferenceLine2X.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceLine2X.Name = "tbReferenceLine2X";
            this.tbReferenceLine2X.Palette = this.kp1;
            this.tbReferenceLine2X.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceLine2X.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceLine2X.TabIndex = 47;
            this.tbReferenceLine2X.TextChanged += new System.EventHandler(this.tbReferenceLine_TextChanged);
            // 
            // tbReferenceLine2Y
            // 
            this.tbReferenceLine2Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceLine2Y.Location = new System.Drawing.Point(247, 39);
            this.tbReferenceLine2Y.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceLine2Y.Name = "tbReferenceLine2Y";
            this.tbReferenceLine2Y.Palette = this.kp1;
            this.tbReferenceLine2Y.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceLine2Y.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceLine2Y.TabIndex = 48;
            this.tbReferenceLine2Y.TextChanged += new System.EventHandler(this.tbReferenceLine_TextChanged);
            // 
            // tbReferenceCircle1X
            // 
            this.tbReferenceCircle1X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceCircle1X.Location = new System.Drawing.Point(137, 72);
            this.tbReferenceCircle1X.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceCircle1X.Name = "tbReferenceCircle1X";
            this.tbReferenceCircle1X.Palette = this.kp1;
            this.tbReferenceCircle1X.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceCircle1X.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceCircle1X.TabIndex = 49;
            this.tbReferenceCircle1X.TextChanged += new System.EventHandler(this.tbReferenceCircle_TextChanged);
            // 
            // tbReferenceCircle1Y
            // 
            this.tbReferenceCircle1Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceCircle1Y.Location = new System.Drawing.Point(247, 72);
            this.tbReferenceCircle1Y.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceCircle1Y.Name = "tbReferenceCircle1Y";
            this.tbReferenceCircle1Y.Palette = this.kp1;
            this.tbReferenceCircle1Y.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceCircle1Y.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceCircle1Y.TabIndex = 50;
            this.tbReferenceCircle1Y.TextChanged += new System.EventHandler(this.tbReferenceCircle_TextChanged);
            // 
            // lbNumber
            // 
            this.lbNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNumber.Location = new System.Drawing.Point(352, 132);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Palette = this.kp1;
            this.lbNumber.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbNumber.Size = new System.Drawing.Size(71, 33);
            this.lbNumber.TabIndex = 24;
            this.lbNumber.Values.Text = "Number";
            // 
            // lbFirstOffset
            // 
            this.lbFirstOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFirstOffset.Location = new System.Drawing.Point(352, 0);
            this.lbFirstOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFirstOffset.Name = "lbFirstOffset";
            this.lbFirstOffset.Palette = this.kp1;
            this.lbFirstOffset.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbFirstOffset.Size = new System.Drawing.Size(71, 33);
            this.lbFirstOffset.TabIndex = 22;
            this.lbFirstOffset.Values.Text = "First Offset";
            // 
            // lbInterval
            // 
            this.lbInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInterval.Location = new System.Drawing.Point(352, 33);
            this.lbInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInterval.Name = "lbInterval";
            this.lbInterval.Palette = this.kp1;
            this.lbInterval.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbInterval.Size = new System.Drawing.Size(71, 33);
            this.lbInterval.TabIndex = 20;
            this.lbInterval.Values.Text = "Interval";
            // 
            // tbOffsetReferencePoint
            // 
            this.tbOffsetReferencePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOffsetReferencePoint.Location = new System.Drawing.Point(427, 6);
            this.tbOffsetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbOffsetReferencePoint.Name = "tbOffsetReferencePoint";
            this.tbOffsetReferencePoint.Palette = this.kp1;
            this.tbOffsetReferencePoint.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbOffsetReferencePoint.Size = new System.Drawing.Size(71, 23);
            this.tbOffsetReferencePoint.TabIndex = 55;
            // 
            // tbInterval
            // 
            this.tbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInterval.Location = new System.Drawing.Point(427, 39);
            this.tbInterval.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Palette = this.kp1;
            this.tbInterval.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbInterval.Size = new System.Drawing.Size(71, 23);
            this.tbInterval.TabIndex = 56;
            // 
            // tbNumberOfPoints
            // 
            this.tbNumberOfPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNumberOfPoints.Location = new System.Drawing.Point(427, 138);
            this.tbNumberOfPoints.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbNumberOfPoints.Name = "tbNumberOfPoints";
            this.tbNumberOfPoints.Palette = this.kp1;
            this.tbNumberOfPoints.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbNumberOfPoints.Size = new System.Drawing.Size(71, 23);
            this.tbNumberOfPoints.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(102, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Palette = this.kp1;
            this.label3.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label3.Size = new System.Drawing.Size(31, 33);
            this.label3.TabIndex = 56;
            this.label3.Values.Text = "X";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(212, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Palette = this.kp1;
            this.label4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label4.Size = new System.Drawing.Size(31, 33);
            this.label4.TabIndex = 57;
            this.label4.Values.Text = "Y";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(102, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Palette = this.kp1;
            this.label5.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label5.Size = new System.Drawing.Size(31, 33);
            this.label5.TabIndex = 58;
            this.label5.Values.Text = "X";
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(212, 132);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Palette = this.kp1;
            this.label6.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label6.Size = new System.Drawing.Size(31, 33);
            this.label6.TabIndex = 59;
            this.label6.Values.Text = "Y";
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(212, 165);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Palette = this.kp1;
            this.label8.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label8.Size = new System.Drawing.Size(31, 35);
            this.label8.TabIndex = 61;
            this.label8.Values.Text = "Y";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(102, 165);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Palette = this.kp1;
            this.label7.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label7.Size = new System.Drawing.Size(31, 35);
            this.label7.TabIndex = 60;
            this.label7.Values.Text = "X";
            // 
            // tbReferenceCircle2X
            // 
            this.tbReferenceCircle2X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceCircle2X.Location = new System.Drawing.Point(137, 105);
            this.tbReferenceCircle2X.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceCircle2X.Name = "tbReferenceCircle2X";
            this.tbReferenceCircle2X.Palette = this.kp1;
            this.tbReferenceCircle2X.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceCircle2X.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceCircle2X.TabIndex = 51;
            this.tbReferenceCircle2X.TextChanged += new System.EventHandler(this.tbReferenceCircle_TextChanged);
            // 
            // tbReferenceCircle2Y
            // 
            this.tbReferenceCircle2Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceCircle2Y.Location = new System.Drawing.Point(247, 105);
            this.tbReferenceCircle2Y.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceCircle2Y.Name = "tbReferenceCircle2Y";
            this.tbReferenceCircle2Y.Palette = this.kp1;
            this.tbReferenceCircle2Y.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceCircle2Y.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceCircle2Y.TabIndex = 52;
            this.tbReferenceCircle2Y.TextChanged += new System.EventHandler(this.tbReferenceCircle_TextChanged);
            // 
            // tbReferenceCircle3X
            // 
            this.tbReferenceCircle3X.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceCircle3X.Location = new System.Drawing.Point(137, 138);
            this.tbReferenceCircle3X.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceCircle3X.Name = "tbReferenceCircle3X";
            this.tbReferenceCircle3X.Palette = this.kp1;
            this.tbReferenceCircle3X.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceCircle3X.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceCircle3X.TabIndex = 53;
            this.tbReferenceCircle3X.TextChanged += new System.EventHandler(this.tbReferenceCircle_TextChanged);
            // 
            // tbReferenceCircle3Y
            // 
            this.tbReferenceCircle3Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReferenceCircle3Y.Location = new System.Drawing.Point(247, 138);
            this.tbReferenceCircle3Y.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbReferenceCircle3Y.Name = "tbReferenceCircle3Y";
            this.tbReferenceCircle3Y.Palette = this.kp1;
            this.tbReferenceCircle3Y.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbReferenceCircle3Y.Size = new System.Drawing.Size(71, 23);
            this.tbReferenceCircle3Y.TabIndex = 54;
            this.tbReferenceCircle3Y.TextChanged += new System.EventHandler(this.tbReferenceCircle_TextChanged);
            // 
            // tbCenterX
            // 
            this.tbCenterX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCenterX.Location = new System.Drawing.Point(137, 171);
            this.tbCenterX.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbCenterX.Name = "tbCenterX";
            this.tbCenterX.Palette = this.kp1;
            this.tbCenterX.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbCenterX.ReadOnly = true;
            this.tbCenterX.Size = new System.Drawing.Size(71, 23);
            this.tbCenterX.TabIndex = 66;
            // 
            // tbCenterY
            // 
            this.tbCenterY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCenterY.Location = new System.Drawing.Point(247, 171);
            this.tbCenterY.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbCenterY.Name = "tbCenterY";
            this.tbCenterY.Palette = this.kp1;
            this.tbCenterY.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbCenterY.ReadOnly = true;
            this.tbCenterY.Size = new System.Drawing.Size(71, 23);
            this.tbCenterY.TabIndex = 67;
            // 
            // btnSetReferenceCircle2
            // 
            this.btnSetReferenceCircle2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferenceCircle2.Location = new System.Drawing.Point(322, 104);
            this.btnSetReferenceCircle2.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnSetReferenceCircle2.Name = "btnSetReferenceCircle2";
            this.btnSetReferenceCircle2.Palette = this.kp1;
            this.btnSetReferenceCircle2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSetReferenceCircle2.Size = new System.Drawing.Size(26, 23);
            this.btnSetReferenceCircle2.TabIndex = 43;
            this.btnSetReferenceCircle2.Click += new System.EventHandler(this.btnSetReferenceCircle2_Click);
            // 
            // btnSetReferenceCircle3
            // 
            this.btnSetReferenceCircle3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferenceCircle3.Location = new System.Drawing.Point(322, 137);
            this.btnSetReferenceCircle3.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.btnSetReferenceCircle3.Name = "btnSetReferenceCircle3";
            this.btnSetReferenceCircle3.Palette = this.kp1;
            this.btnSetReferenceCircle3.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSetReferenceCircle3.Size = new System.Drawing.Size(26, 23);
            this.btnSetReferenceCircle3.TabIndex = 44;
            this.btnSetReferenceCircle3.Click += new System.EventHandler(this.btnSetReferenceCircle3_Click);
            // 
            // tbOffsetLine
            // 
            this.tbOffsetLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOffsetLine.Location = new System.Drawing.Point(427, 105);
            this.tbOffsetLine.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbOffsetLine.Name = "tbOffsetLine";
            this.tbOffsetLine.Palette = this.kp1;
            this.tbOffsetLine.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbOffsetLine.Size = new System.Drawing.Size(71, 23);
            this.tbOffsetLine.TabIndex = 58;
            // 
            // tbAngle
            // 
            this.tbAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAngle.Location = new System.Drawing.Point(427, 72);
            this.tbAngle.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.tbAngle.Name = "tbAngle";
            this.tbAngle.Palette = this.kp1;
            this.tbAngle.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbAngle.Size = new System.Drawing.Size(71, 23);
            this.tbAngle.TabIndex = 57;
            this.tbAngle.TextChanged += new System.EventHandler(this.tbAngle_TextChanged);
            // 
            // lbOffset
            // 
            this.lbOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOffset.Location = new System.Drawing.Point(352, 99);
            this.lbOffset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOffset.Name = "lbOffset";
            this.lbOffset.Palette = this.kp1;
            this.lbOffset.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbOffset.Size = new System.Drawing.Size(71, 33);
            this.lbOffset.TabIndex = 21;
            this.lbOffset.Values.Text = "Offset";
            // 
            // lbAngle
            // 
            this.lbAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAngle.Location = new System.Drawing.Point(352, 66);
            this.lbAngle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAngle.Name = "lbAngle";
            this.lbAngle.Palette = this.kp1;
            this.lbAngle.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbAngle.Size = new System.Drawing.Size(71, 33);
            this.lbAngle.TabIndex = 41;
            this.lbAngle.Values.Text = "Angle";
            // 
            // PatternCircleExtensionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PatternCircleExtensionControl";
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
