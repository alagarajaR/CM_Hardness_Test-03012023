using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIO_Client.Properties;
using Labtt.Data;
using Labtt.DrawArea;
using MessageBoxExApp;

namespace AIO_Client
{

	public class PatternFreeControl : UserControl, IPatternControl
	{
		private const string IDENTIFIER_REFERENCE_POINT = "PATTERN_FREE_REFERENCE_POINT";

		private MainForm owner;

		private ImageBox box;

		private BindingList<DataPatternFree> dataList;

		private VoidDelegate refreshPointList = null;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private DataGridView dgvPoints;

		private Button btnClear;

		private Button btnDelete;

		private Button btnSetReferencePoint;

		private DataGridViewTextBoxColumn ColNo;

		private DataGridViewTextBoxColumn ColX;

		private DataGridViewTextBoxColumn ColY;

		private DataGridViewTextBoxColumn GraphicsSpot;

		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				dgvPoints.ColumnHeadersDefaultCellStyle.Font = value;
				dgvPoints.DefaultCellStyle.Font = value;
				dgvPoints.RowHeadersDefaultCellStyle.Font = value;
				dgvPoints.RowsDefaultCellStyle.Font = value;
			}
		}

		public PatternFreeControl()
		{
			InitializeComponent();
			dataList = new BindingList<DataPatternFree>();
			dgvPoints.DataSource = dataList;
			refreshPointList = dgvPoints.Invalidate;
		}

		private void btnSetReferencePoint_Click(object sender, EventArgs e)
		{
			if (box.Image != null)
			{
				box.ActiveTool = DrawToolType.Spot;
				box.ActiveToolIdentifier = "PATTERN_FREE_REFERENCE_POINT";
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int rowCount = dgvPoints.SelectedRows.Count;
			if (rowCount == 0)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Pattern_Free_NoSelectedLine);
				return;
			}
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Pattern_Free_SureToDelete.Replace("{INFO}", rowCount.ToString()), ResourcesManager.Resources.R_Pattern_Free_Delete);
			if (result == DialogResult.Cancel)
			{
				return;
			}
			for (int i = dgvPoints.SelectedRows.Count - 1; i >= 0; i--)
			{
				int index = (int)dgvPoints.SelectedRows[i].Cells[0].Value;
				DataPatternFree data = dataList.FirstOrDefault((DataPatternFree x) => x.Index == index);
				if (data != null)
				{
					box.GraphicsList.Remove(data.GraphicsSpot);
					dataList.Remove(data);
				}
			}
			for (int i = 0; i < dataList.Count; i++)
			{
				dataList[i].Index = i + 1;
			}
			dgvPoints.Invalidate();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (dataList.Count > 0)
			{
				DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Pattern_Free_SureToClear, ResourcesManager.Resources.R_Pattern_Free_Clear);
				if (result != DialogResult.Cancel)
				{
					Reset();
				}
			}
		}

		private void box_OnSpotChanged(object sender, PointGraphicsEventArgs e)
		{
			GraphicsSpot graphicsSpot = sender as GraphicsSpot;
			if (!(graphicsSpot.Identifier == "PATTERN_FREE_REFERENCE_POINT"))
			{
				return;
			}
			Invoke(refreshPointList);
			foreach (DataPatternFree data in dataList)
			{
				if (graphicsSpot == data.GraphicsSpot)
				{
					data.XPos = e.PhysicalCoordinate.X;
					data.YPos = e.PhysicalCoordinate.Y;
					return;
				}
			}
			DataPatternFree newData = new DataPatternFree();
			newData.Index = dataList.Count + 1;
			newData.XPos = graphicsSpot.Location.X;
			newData.YPos = graphicsSpot.Location.Y;
			newData.GraphicsSpot = graphicsSpot;
			dataList.Add(newData);
		}

		private void LoadLanguageResources()
		{
			btnDelete.Text = ResourcesManager.Resources.R_Pattern_Free_Delete;
			btnClear.Text = ResourcesManager.Resources.R_Pattern_Free_Clear;
		}

		public void Initialize(MainForm owner)
		{
			this.owner = owner;
			box = this.owner.imageBox;
			box.OnSpotChanged += box_OnSpotChanged;
			LoadLanguageResources();
		}

		public void SetGraphicsVisible(bool visible)
		{
			foreach (DataPatternFree data in dataList)
			{
				data.GraphicsSpot.Visible = visible;
			}
		}

		public void Reset()
		{
			box.GraphicsList.RemoveByIdentifier("PATTERN_FREE_REFERENCE_POINT");
			dataList.Clear();
			box.Invalidate();
		}

		public List<PointF> GeneratePoints()
		{
			try
			{
				List<PointF> pointList = new List<PointF>();
				foreach (DataPatternFree data in dataList)
				{
					PointF point = new PointF(data.XPos, data.YPos);
					pointList.Add(point);
				}
				Reset();
				return pointList;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to generate free point set！");
				throw;
			}
		}

		public void GeneratePointsAndDepth(out List<PointF> pointList, out List<float> depthList)
		{
			try
			{
				pointList = new List<PointF>();
				depthList = new List<float>();
				foreach (DataPatternFree data in dataList)
				{
					PointF point = new PointF(data.XPos, data.YPos);
					pointList.Add(point);
					depthList.Add(0f);
				}
				Reset();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to generate free point set！");
				throw;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetReferencePoint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.ColNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GraphicsSpot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel1.Controls.Add(this.btnSetReferencePoint, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 35);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // btnSetReferencePoint
            // 
            this.btnSetReferencePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetReferencePoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetReferencePoint.Image = global::AIO_Client_Properties_Resources.select;
            this.btnSetReferencePoint.Location = new System.Drawing.Point(2, 4);
            this.btnSetReferencePoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetReferencePoint.Name = "btnSetReferencePoint";
            this.btnSetReferencePoint.Size = new System.Drawing.Size(26, 26);
            this.btnSetReferencePoint.TabIndex = 30;
            this.btnSetReferencePoint.UseVisualStyleBackColor = true;
            this.btnSetReferencePoint.Click += new System.EventHandler(this.btnSetReferencePoint_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(157, 3);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(121, 29);
            this.btnClear.TabIndex = 31;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(32, 3);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(121, 29);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvPoints
            // 
            this.dgvPoints.AllowUserToAddRows = false;
            this.dgvPoints.AllowUserToDeleteRows = false;
            this.dgvPoints.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPoints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPoints.ColumnHeadersHeight = 25;
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNo,
            this.ColX,
            this.ColY,
            this.GraphicsSpot});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPoints.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPoints.EnableHeadersVisualStyles = false;
            this.dgvPoints.Location = new System.Drawing.Point(0, 35);
            this.dgvPoints.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.ReadOnly = true;
            this.dgvPoints.RowHeadersWidth = 25;
            this.dgvPoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPoints.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPoints.RowTemplate.Height = 23;
            this.dgvPoints.ShowCellToolTips = false;
            this.dgvPoints.Size = new System.Drawing.Size(500, 165);
            this.dgvPoints.TabIndex = 22;
            // 
            // ColNo
            // 
            this.ColNo.DataPropertyName = "Index";
            this.ColNo.HeaderText = "No.";
            this.ColNo.MinimumWidth = 6;
            this.ColNo.Name = "ColNo";
            this.ColNo.ReadOnly = true;
            this.ColNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColNo.Width = 76;
            // 
            // ColX
            // 
            this.ColX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColX.DataPropertyName = "XPos";
            dataGridViewCellStyle2.Format = "N5";
            dataGridViewCellStyle2.NullValue = null;
            this.ColX.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColX.HeaderText = "X(mm)";
            this.ColX.MinimumWidth = 6;
            this.ColX.Name = "ColX";
            this.ColX.ReadOnly = true;
            this.ColX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColY
            // 
            this.ColY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColY.DataPropertyName = "YPos";
            dataGridViewCellStyle3.Format = "N5";
            dataGridViewCellStyle3.NullValue = null;
            this.ColY.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColY.HeaderText = "Y(mm)";
            this.ColY.MinimumWidth = 6;
            this.ColY.Name = "ColY";
            this.ColY.ReadOnly = true;
            this.ColY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GraphicsSpot
            // 
            this.GraphicsSpot.DataPropertyName = "GraphicsSpot";
            this.GraphicsSpot.HeaderText = "GraphicsSpot";
            this.GraphicsSpot.MinimumWidth = 6;
            this.GraphicsSpot.Name = "GraphicsSpot";
            this.GraphicsSpot.ReadOnly = true;
            this.GraphicsSpot.Visible = false;
            this.GraphicsSpot.Width = 125;
            // 
            // PatternFreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPoints);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PatternFreeControl";
            this.Size = new System.Drawing.Size(500, 200);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            this.ResumeLayout(false);

		}

		void IPatternControl.BringToFront()
		{
			BringToFront();
		}
	}
}
