using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Labtt.Data;
using MessageBoxExApp;
using Microsoft.Office.Interop.Word;
using Krypton.Toolkit;

namespace AIO_Client
{

	public class ExportReportForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonRadioButton rbNormalReport;

		private KryptonRadioButton rbReportWithImage;

		private KryptonRadioButton rbReportWithDeepHardness;

		private KryptonRadioButton rbFullReport;

		private KryptonButton btnCancel;

		private KryptonButton btnExport;

		private KryptonRadioButton rbCSVReport;

		private MainForm owner;

		private BindingList<MeasureRecord> recordList;

		private SampleInfo sampleInfo;

		private string force;

		private string loadTime;

		private string number;

		private string max;

		private string min;

		private string avg;

		private string variance;

		private string stdDev;

		private string cp;

		private string cpk;

		private float deepHardness;
        private KryptonPalette kp1;
        private int exportWay = 0;

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
            this.rbNormalReport = new Krypton.Toolkit.KryptonRadioButton();
            this.rbReportWithImage = new Krypton.Toolkit.KryptonRadioButton();
            this.rbReportWithDeepHardness = new Krypton.Toolkit.KryptonRadioButton();
            this.rbFullReport = new Krypton.Toolkit.KryptonRadioButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnExport = new Krypton.Toolkit.KryptonButton();
            this.rbCSVReport = new Krypton.Toolkit.KryptonRadioButton();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // rbNormalReport
            // 
            this.rbNormalReport.Checked = true;
            this.rbNormalReport.Location = new System.Drawing.Point(12, 48);
            this.rbNormalReport.Name = "rbNormalReport";
            this.rbNormalReport.Palette = this.kp1;
            this.rbNormalReport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbNormalReport.Size = new System.Drawing.Size(103, 20);
            this.rbNormalReport.TabIndex = 0;
            this.rbNormalReport.Values.Text = "Normal Report";
            // 
            // rbReportWithImage
            // 
            this.rbReportWithImage.Location = new System.Drawing.Point(12, 84);
            this.rbReportWithImage.Name = "rbReportWithImage";
            this.rbReportWithImage.Palette = this.kp1;
            this.rbReportWithImage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbReportWithImage.Size = new System.Drawing.Size(119, 20);
            this.rbReportWithImage.TabIndex = 1;
            this.rbReportWithImage.Values.Text = "Word With Image";
            // 
            // rbReportWithDeepHardness
            // 
            this.rbReportWithDeepHardness.Location = new System.Drawing.Point(12, 120);
            this.rbReportWithDeepHardness.Name = "rbReportWithDeepHardness";
            this.rbReportWithDeepHardness.Palette = this.kp1;
            this.rbReportWithDeepHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbReportWithDeepHardness.Size = new System.Drawing.Size(168, 20);
            this.rbReportWithDeepHardness.TabIndex = 2;
            this.rbReportWithDeepHardness.Values.Text = "Word With Deep Hardness";
            // 
            // rbFullReport
            // 
            this.rbFullReport.Location = new System.Drawing.Point(12, 156);
            this.rbFullReport.Name = "rbFullReport";
            this.rbFullReport.Palette = this.kp1;
            this.rbFullReport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbFullReport.Size = new System.Drawing.Size(231, 20);
            this.rbFullReport.TabIndex = 3;
            this.rbFullReport.Values.Text = "Word With Image And Deep Hardness";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(119, 35);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(35, 201);
            this.btnExport.Name = "btnExport";
            this.btnExport.Palette = this.kp1;
            this.btnExport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnExport.Size = new System.Drawing.Size(119, 35);
            this.btnExport.TabIndex = 19;
            this.btnExport.Values.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // rbCSVReport
            // 
            this.rbCSVReport.Location = new System.Drawing.Point(12, 12);
            this.rbCSVReport.Name = "rbCSVReport";
            this.rbCSVReport.Palette = this.kp1;
            this.rbCSVReport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbCSVReport.Size = new System.Drawing.Size(85, 20);
            this.rbCSVReport.TabIndex = 21;
            this.rbCSVReport.Values.Text = "CSV Report";
            // 
            // ExportReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 248);
            this.Controls.Add(this.rbCSVReport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rbFullReport);
            this.Controls.Add(this.rbReportWithDeepHardness);
            this.Controls.Add(this.rbReportWithImage);
            this.Controls.Add(this.rbNormalReport);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportReportForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Report";
            this.Load += new System.EventHandler(this.ExportReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public ExportReportForm(MainForm owner, BindingList<MeasureRecord> recordList)
		{
			InitializeComponent();
			LoadLanguageResources();

			kp1.ResetToDefaults(true);
			kp1.BasePaletteMode = Program.palete_mode;

			this.owner = owner;
			this.recordList = recordList;
			sampleInfo = this.owner.sampleInfo;
			force = this.owner.cbForce.Text;
			//loadTime = this.owner.nudLoadTime.Text;
			number = this.owner.lbNumberValue.Text;
			max = this.owner.lbMaxValue.Text;
			min = this.owner.lbMinValue.Text;
			avg = this.owner.lbAvgValue.Text;
			variance = this.owner.lbVarianceValue.Text;
			stdDev = this.owner.lbStdDevValue.Text;
			cp = this.owner.lbCPValue.Text;
			cpk = this.owner.lbCPKValue.Text;
			deepHardness = this.owner.savedData.DeepHardness;
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			if (rbCSVReport.Checked)
			{
				string fileName = (string.IsNullOrEmpty(sampleInfo.SampleSn) ? "" : (sampleInfo.SampleSn + "-")) + (string.IsNullOrEmpty(sampleInfo.SampleName) ? "" : sampleInfo.SampleName);
				if (string.IsNullOrEmpty(fileName))
				{
					fileName = "Report";
				}
				SaveFileDialog dialog = new SaveFileDialog();
				dialog.Filter = "Report|*.csv";
				dialog.FileName = fileName + "-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".csv";
				if (dialog.ShowDialog() == DialogResult.Cancel)
				{
					return;
				}
				ExportCSV(dialog.FileName);
			}
			else
			{
				string fileName = (string.IsNullOrEmpty(sampleInfo.SampleSn) ? "" : (sampleInfo.SampleSn + "-")) + (string.IsNullOrEmpty(sampleInfo.SampleName) ? "" : sampleInfo.SampleName);
				if (string.IsNullOrEmpty(fileName))
				{
					fileName = "Report";
				}
				SaveFileDialog dialog = new SaveFileDialog();
				dialog.Filter = "Report|*.doc";
				dialog.FileName = fileName + "-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".doc";
				if (dialog.ShowDialog() == DialogResult.Cancel)
				{
					return;
				}
				if (rbNormalReport.Checked)
				{
					exportWay = 1;
				}
				else if (rbReportWithImage.Checked)
				{
					exportWay = 2;
				}
				else if (rbReportWithDeepHardness.Checked)
				{
					exportWay = 3;
				}
				else if (rbFullReport.Checked)
				{
					exportWay = 4;
				}
				Thread exportThread = new Thread(ExportToWord);
				exportThread.Start(dialog.FileName);
			}
			Dispose();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void ExportToWord(object obj)
		{
			string fileName = obj as string;
			string templateFilepath = string.Empty;
			if (exportWay == 1)
			{
				templateFilepath = ResourcesManager.LanguageInfo.SimpleReportTemplateFilepath;
			}
			else if (exportWay == 2)
			{
				templateFilepath = ResourcesManager.LanguageInfo.ReportWithImageTemplateFilepath;
			}
			else if (exportWay == 3)
			{
				templateFilepath = ResourcesManager.LanguageInfo.ReportWithDeepHardnessTemplateFilepath;
			}
			else
			{
				if (exportWay != 4)
				{
					return;
				}
				templateFilepath = ResourcesManager.LanguageInfo.FullReportTemplateFilepath;
			}
			templateFilepath = System.Windows.Forms.Application.StartupPath + "\\" + templateFilepath;
			MSWord msWord = null;
			_Application wordApp = null;
			_Document wordDoc = null;
			try
			{
				msWord = new MSWord();
				wordApp = msWord.OpenWordApp(isVisible: false);
				if (wordApp == null)
				{
					msWord.CloseWordApp(wordApp);
					throw new IOException("Failed to load export module！");
				}
				wordDoc = msWord.OpenWordDocument(wordApp, templateFilepath);
				if (wordDoc == null)
				{
					msWord.CloseWordDocument(wordDoc, isSaveChange: false);
					msWord.CloseWordApp(wordApp);
					throw new IOException("Failed to load template file！");
				}
				Table table = msWord.GetTable(wordDoc, 1);
				if (table == null)
				{
					msWord.CloseWordDocument(wordDoc, isSaveChange: false);
					msWord.CloseWordApp(wordApp);
					throw new IOException("Template file format error！");
				}
				msWord.AddStringInTable(table, 1u, 2u, sampleInfo.SampleName);
				msWord.AddStringInTable(table, 1u, 4u, sampleInfo.SampleSn);
				msWord.AddStringInTable(table, 2u, 2u, sampleInfo.HardnessL.ToString("F1"));
				msWord.AddStringInTable(table, 2u, 4u, sampleInfo.HardnessH.ToString("F1"));
				msWord.AddStringInTable(table, 3u, 2u, sampleInfo.InspectionUnit);
				msWord.AddStringInTable(table, 3u, 4u, sampleInfo.InspectionDate.ToString("yyyy/MM/dd"));
				msWord.AddStringInTable(table, 4u, 2u, sampleInfo.Tester);
				msWord.AddStringInTable(table, 4u, 4u, sampleInfo.Reviewer);
				msWord.AddStringInTable(table, 5u, 2u, force);
				msWord.AddStringInTable(table, 5u, 4u, loadTime);
				msWord.AddStringInTable(table, 9u, 1u, number);
				msWord.AddStringInTable(table, 9u, 2u, max);
				msWord.AddStringInTable(table, 9u, 3u, min);
				msWord.AddStringInTable(table, 9u, 4u, avg);
				msWord.AddStringInTable(table, 9u, 5u, variance);
				msWord.AddStringInTable(table, 9u, 6u, stdDev);
				msWord.AddStringInTable(table, 9u, 7u, cp);
				msWord.AddStringInTable(table, 9u, 8u, cpk);
				if (exportWay == 3 || exportWay == 4)
				{
					int deepHardnessStartRow = ((exportWay == 3) ? 16 : 19);
					double validDeep = 0.0;
					Bitmap deepHardnessBmp = GetHardnessDeepChart(out validDeep);
					if (deepHardnessBmp != null)
					{
						deepHardnessBmp.Save(CommonData.HardnessDeepTempFilepath);
						msWord.AddStringInTable(table, (uint)deepHardnessStartRow, 2u, deepHardness.ToString("F1"));
						msWord.AddStringInTable(table, (uint)deepHardnessStartRow, 4u, validDeep.ToString("F3"));
						msWord.AddPictureInTable(wordApp, table, (uint)(deepHardnessStartRow + 1), 1u, CommonData.HardnessDeepTempFilepath, 400, 300);
					}
				}
				if (exportWay == 2 || exportWay == 4)
				{
					int picStartRow = 16;
					int picRowCount = (recordList.Count + 1) / 2;
					msWord.InsertTableRow(table, picStartRow - 1, picRowCount - 1);
					for (int i = 0; i < picRowCount; i++)
					{
						int imageIndex = i * 2;
						if (recordList.Count > imageIndex && File.Exists(recordList[imageIndex].MeasuredImagePath))
						{
							msWord.AddStringInTable(table, (uint)(i + picStartRow), 1u, (imageIndex + 1).ToString());
							msWord.AddPictureInTable(wordApp, table, (uint)(i + picStartRow), 2u, recordList[imageIndex].MeasuredImagePath, 160, 120);
						}
						if (recordList.Count > imageIndex + 1 && File.Exists(recordList[imageIndex + 1].MeasuredImagePath))
						{
							msWord.AddStringInTable(table, (uint)(i + picStartRow), 3u, (imageIndex + 2).ToString());
							msWord.AddPictureInTable(wordApp, table, (uint)(i + picStartRow), 4u, recordList[imageIndex + 1].MeasuredImagePath, 160, 120);
						}
					}
				}
				int recordStartRow = 13;
				msWord.InsertTableRow(table, recordStartRow - 1, recordList.Count - 1);
				for (int i = 0; i < recordList.Count; i++)
				{
					MeasureRecord record = recordList[i];
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 1u, (i + 1).ToString());
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 2u, record.D1.ToString("F2"));
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 3u, record.D2.ToString("F2"));
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 4u, record.DAvg.ToString("F2"));
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 5u, record.HardnessType);
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 6u, record.Hardness.ToString("F1"));
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 7u, record.ConvertType);
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 8u, record.ConvertValue.ToString("F1"));
					msWord.AddStringInTable(table, (uint)(recordStartRow + i), 9u, record.Qualified);
					if (exportWay == 3 || exportWay == 4)
					{
						msWord.AddStringInTable(table, (uint)(recordStartRow + i), 10u, record.Depth.ToString("F3"));
					}
				}
				msWord.SaveCopyAs(wordDoc, fileName);
				msWord.CloseWordDocument(wordDoc, isSaveChange: false);
				msWord.CloseWordApp(wordApp);
				Process.Start(fileName);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_ExportReport_ExportReportFailed);
			}
			finally
			{
				try
				{
					if (msWord != null && wordDoc != null)
					{
						msWord.CloseWordDocument(wordDoc, isSaveChange: false);
					}
				}
				catch
				{
				}
				try
				{
					if (msWord != null && wordApp != null)
					{
						msWord.CloseWordApp(wordApp);
					}
				}
				catch
				{
				}
			}
		}

		public Bitmap GetHardnessDeepChart(out double validDeep)
		{
			BindingList<MeasureRecord> recordList = this.recordList;
			validDeep = 0.0;
			if (recordList.Count < 1)
			{
				return null;
			}
			int chartWidth = 800;
			int chartHeight = 600;
			int leftIndent = 100;
			int rightIndent = 70;
			int upIndent = 70;
			int downIndent = 100;
			int xAxisIndent = 30;
			int yAxisIndent = 30;
			int xMarkNum = 10;
			int yMarkNum = 10;
			int markLength = 5;
			double maxHardness = 0.0;
			double maxDepth = 0.0;
			foreach (MeasureRecord record in recordList)
			{
				double currentHardness = record.Hardness;
				double currentDepth = record.Depth;
				if (currentHardness > maxHardness)
				{
					maxHardness = currentHardness;
				}
				if (currentDepth > maxDepth)
				{
					maxDepth = currentDepth;
				}
			}
			double hvPerPixel = maxHardness / (double)(chartHeight - upIndent - downIndent - yAxisIndent);
			double xPerPixel = maxDepth / (double)(chartWidth - leftIndent - rightIndent - xAxisIndent);
			if (hvPerPixel < 2.0)
			{
				hvPerPixel = 2.0;
			}
			List<PointF> paintPoints = new List<PointF>();
			foreach (MeasureRecord record in recordList)
			{
				double currentHardness = record.Hardness;
				double currentDepth = record.Depth;
				if (!(currentDepth < 0.0))
				{
					float x = ((currentDepth == 0.0) ? ((float)leftIndent) : ((float)leftIndent + (float)(currentDepth / xPerPixel)));
					float y = (float)(chartHeight - downIndent) - (float)(currentHardness / hvPerPixel);
					PointF point = new PointF(x, y);
					paintPoints.Add(point);
				}
			}
			paintPoints = (from p in paintPoints
						   orderby p.X, p.Y
						   select p).ToList();
			float deepHardnessY = (float)(chartHeight - downIndent) - (float)((double)deepHardness / hvPerPixel);
			float deepHardnessX = leftIndent;
			for (int i = 0; i < paintPoints.Count - 1; i++)
			{
				if ((paintPoints[i].Y >= deepHardnessY && paintPoints[i + 1].Y <= deepHardnessY) || (paintPoints[i].Y <= deepHardnessY && paintPoints[i + 1].Y >= deepHardnessY))
				{
					deepHardnessX = paintPoints[i].X + (paintPoints[i + 1].X - paintPoints[i].X) * (deepHardnessY - paintPoints[i].Y) / (paintPoints[i + 1].Y - paintPoints[i].Y);
					validDeep = (double)(deepHardnessX - (float)leftIndent) * xPerPixel;
					break;
				}
			}
			Bitmap hardnessDeepChart = new Bitmap(chartWidth, chartHeight);
			Graphics g = Graphics.FromImage(hardnessDeepChart);
			Pen drawPen = new Pen(SystemColors.ControlDarkDark);
			drawPen.Width = 2f;
			System.Drawing.Font drawFont = new System.Drawing.Font("Microsoft YaHei UI", 13f);
			g.DrawRectangle(drawPen, new System.Drawing.Rectangle(0, 0, chartWidth, chartHeight));
			PointF originPoint = new PointF(leftIndent, chartHeight - downIndent);
			PointF xAxisEndPoint = new PointF(chartWidth - rightIndent, originPoint.Y);
			PointF yAxisEndPoint = new PointF(originPoint.X, upIndent);
			g.DrawLine(drawPen, originPoint, xAxisEndPoint);
			g.DrawLine(drawPen, originPoint, yAxisEndPoint);
			xAxisEndPoint.X -= xAxisIndent;
			yAxisEndPoint.Y += yAxisIndent;
			float xAxisLength = Math.Abs(xAxisEndPoint.X - originPoint.X);
			float xMarkDist = xAxisLength / (float)xMarkNum;
			PointF[] xMarkLinePoint = new PointF[xMarkNum];
			for (int i = 0; i < xMarkNum; i++)
			{
				PointF markStartPosition = new PointF(originPoint.X + xMarkDist * (float)(i + 1), originPoint.Y);
				PointF markEndPosition = new PointF(originPoint.X + xMarkDist * (float)(i + 1), originPoint.Y + (float)markLength);
				g.DrawLine(drawPen, markStartPosition, markEndPosition);
				string markString = ((double)(i + 1) * xPerPixel * (double)xAxisLength / 10.0).ToString("F3");
				SizeF stringSize = g.MeasureString(markString, drawFont);
				g.DrawString(markString, drawFont, new SolidBrush(SystemColors.ControlDarkDark), new PointF(markEndPosition.X - stringSize.Width / 2f, markEndPosition.Y + (float)markLength));
			}
			float yAxisLength = Math.Abs(yAxisEndPoint.Y - originPoint.Y);
			float yMarkDist = yAxisLength / (float)yMarkNum;
			PointF[] yMarkLinePoint = new PointF[yMarkNum];
			for (int i = 0; i < yMarkNum; i++)
			{
				PointF markStartPosition = new PointF(originPoint.X, originPoint.Y - yMarkDist * (float)(i + 1));
				PointF markEndPosition = new PointF(originPoint.X - (float)markLength, originPoint.Y - yMarkDist * (float)(i + 1));
				g.DrawLine(drawPen, markStartPosition, markEndPosition);
				string markString = ((double)(i + 1) * hvPerPixel * (double)yAxisLength / 10.0).ToString("F0");
				SizeF stringSize = g.MeasureString(markString, drawFont);
				g.DrawString(markString, drawFont, new SolidBrush(SystemColors.ControlDarkDark), new PointF(markEndPosition.X - stringSize.Width - (float)markLength, markEndPosition.Y - stringSize.Height / 2f));
			}
			for (int i = 0; i < paintPoints.Count; i++)
			{
				g.DrawRectangle(drawPen, paintPoints[i].X - 3f, paintPoints[i].Y - 3f, 5f, 5f);
				if (i != paintPoints.Count - 1)
				{
					g.DrawLine(drawPen, paintPoints[i], paintPoints[i + 1]);
				}
			}
			drawPen.DashStyle = DashStyle.Dash;
			g.DrawLine(drawPen, originPoint.X, deepHardnessY, deepHardnessX, deepHardnessY);
			g.DrawLine(drawPen, deepHardnessX, deepHardnessY, deepHardnessX, originPoint.Y);
			return hardnessDeepChart;
		}

		private static string ReplaceCharacter(string str)
		{
			if (str == null)
			{
				return "";
			}
			str = str.Replace("\"", "\"\"");
			if (str.Contains(',') || str.Contains('"') || str.Contains('\r') || str.Contains('\n'))
			{
				str = $"\"{str}\"";
			}
			return str;
		}

		public void ExportCSV(string fileName)
		{
			FileStream stream = null;
			StreamWriter writer = null;
			try
			{
				FileInfo file = new FileInfo(fileName);
				if (!file.Directory.Exists)
				{
					file.Directory.Create();
				}
				stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				writer = new StreamWriter(stream, Encoding.GetEncoding("gb2312"));
				string dataToWrite = string.Empty;
				dataToWrite += "Serial Number,";
				dataToWrite += "Hardness value,";
				dataToWrite += "Hardness Type,";
				dataToWrite += "Qualified,";
				dataToWrite += "D1(mm),";
				dataToWrite += "D2(mm),";
				dataToWrite += "Davg(mm),";
				dataToWrite += "Conversion Type,";
				dataToWrite += "Convert Value,";
				dataToWrite += "Measure Time,";
				writer.WriteLine(dataToWrite);
				foreach (MeasureRecord record in recordList)
				{
					dataToWrite = string.Empty;
					dataToWrite = dataToWrite + record.Index.ToString() + ',';
					dataToWrite = dataToWrite + record.Hardness.ToString("F1") + ',';
					dataToWrite = dataToWrite + ReplaceCharacter(record.HardnessType) + ',';
					dataToWrite = dataToWrite + ReplaceCharacter(record.Qualified) + ',';
					dataToWrite = dataToWrite + record.D1.ToString("F3") + ',';
					dataToWrite = dataToWrite + record.D2.ToString("F3") + ',';
					dataToWrite = dataToWrite + record.DAvg.ToString("F3") + ',';
					dataToWrite = dataToWrite + ReplaceCharacter(record.ConvertType) + ',';
					dataToWrite = dataToWrite + record.ConvertValue.ToString("F1") + ',';
					dataToWrite = dataToWrite + ReplaceCharacter(record.MeasureTime.ToString("yyyy-MM-dd HH:mm:ss")) + ',';
					writer.WriteLine(dataToWrite);
				}
				writer.Close();
				stream.Close();
				Process.Start(fileName);
			}
			catch (Exception ex)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_ExportReport_ExportReportFailed);
				Logger.Error(ex, "Failed to export CSV file!");
			}
			finally
			{
				try
				{
					stream?.Close();
				}
				catch
				{
				}
				try
				{
					stream?.Close();
				}
				catch
				{
				}
			}
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_ExportReport_Title;
			rbCSVReport.Text = ResourcesManager.Resources.R_ExportReport_CSV;
			rbNormalReport.Text = ResourcesManager.Resources.R_ExportReport_WordWithDataOnly;
			rbReportWithImage.Text = ResourcesManager.Resources.R_ExportReport_WordWithImage;
			rbReportWithDeepHardness.Text = ResourcesManager.Resources.R_ExportReport_WordWithDeepHardness;
			rbFullReport.Text = ResourcesManager.Resources.R_ExportReport_WordWithImageAndDeepHardness;
			btnExport.Text = ResourcesManager.Resources.R_ExportReport_Export;
			btnCancel.Text = ResourcesManager.Resources.R_ExportReport_Cancel;
		}

        private void ExportReportForm_Load(object sender, EventArgs e)
        {

        }
    }
}