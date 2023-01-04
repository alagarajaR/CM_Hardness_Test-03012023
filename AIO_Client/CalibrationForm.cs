using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Labtt.Data;
using Labtt.DrawArea;
using MessageBoxExApp;
using Krypton.Toolkit;
using ComponentFactory.Krypton;

namespace AIO_Client
{

	public class CalibrationForm : KryptonForm
	{
		private static bool isCalibrationFormOpened = false;

		private MainForm owner = null;

		private BindingList<CalibrationInfo> calibrationList = null;

		private string filepath = null;

		private IContainer components = null;

		private KryptonGroupBox gbAddCalibration;

		private KryptonTextBox tbHardnessValue;

		private KryptonTextBox tbHYPixel;

		private KryptonTextBox tbHXPixel;

		private KryptonButton btnAutoMeasure;

		private KryptonButton btnManualMeasure;

		private KryptonLabel lbHHardness;

		private KryptonButton btnAddHardnessCalib;

		private KryptonLabel label6;

		private KryptonLabel label7;

		private KryptonLabel lbHPixelLegnth;

		private KryptonComboBox cbHZoomTime;

		private KryptonLabel lbHZoomTime;

		private KryptonTextBox tbLYDistance;

		private KryptonTextBox tbLXDistance;

		private KryptonTextBox tbLYPixel;

		private KryptonTextBox tbLXPixel;

		private KryptonRadioButton rbPlaneCalib;

		private KryptonRadioButton rbLinearCalib;

		private KryptonButton btnAddLengthCalib;

		private KryptonLabel label5;

		private KryptonLabel label4;

		private KryptonLabel lbLRealDistance;

		private KryptonLabel lbLPixelLength;

		private KryptonComboBox cbLZoomTime;

		private KryptonLabel lbLZoomTime;

		private KryptonLabel label12;

		private KryptonLabel label13;

		private KryptonComboBox cbHForce;

		private KryptonLabel lbHForce;

		private KryptonComboBox cbHHardnessLevel;

		private KryptonLabel lbHHardnessLevel;

		private KryptonComboBox cbLHardnessLevel;

		private KryptonLabel lbLHardnessLevel;

		private KryptonComboBox cbLForce;

		private KryptonLabel lbLForce;

		private KryptonButton btnDelete;

		private KryptonButton btnClear;

		private KryptonButton btnExport;

		private KryptonButton btnImport;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator buildCalibTab;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpHardnessCalibration;
        private KryptonDataGridView dgvCalibrations;
        private DataGridViewTextBoxColumn dgvhIndex;
        private DataGridViewTextBoxColumn dgvhZoomTime;
        private DataGridViewTextBoxColumn dgvhForce;
        private DataGridViewTextBoxColumn dgvhHardnessLevel;
        private DataGridViewTextBoxColumn dgvhXPixelLength;
        private DataGridViewTextBoxColumn dgvhYPixelLength;
        private KryptonPalette kp1;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpLengthCalibration;

		public static bool IsCalibrationFormOpened => isCalibrationFormOpened;

		public CalibrationForm(MainForm owner, BindingList<CalibrationInfo> calibrationList, string filepath)
		{
			InitializeComponent();

           kp1.ResetToDefaults(true);
           kp1.BasePaletteMode = Program.palete_mode;
           buildCalibTab.PaletteMode = Program.palete_mode1;



            try
			{
				if (isCalibrationFormOpened)
				{
					throw new Exception("Calibration window has already been opened, please do not open the calibration window repeatedly！");
				}
				isCalibrationFormOpened = true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, ex.Message);
				throw;
			}
			LoadLanguageResources();
			this.owner = owner;
			this.calibrationList = calibrationList;
			this.filepath = filepath;
			dgvCalibrations.DataSource = this.calibrationList;
			this.owner.imageBox.OnFourLineChanged += ImageBox_OnFourLineChanged;
			this.owner.imageBox.OnRangeFinderChanged += ImageBox_OnRangeFinderChanged;
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_Calibration_Title;
			dgvhZoomTime.HeaderText = ResourcesManager.Resources.R_Calibration_ZoomTime;
			dgvhForce.HeaderText = ResourcesManager.Resources.R_Calibration_Force;
			dgvhHardnessLevel.HeaderText = ResourcesManager.Resources.R_Calibration_HardnessLevel;
			dgvhXPixelLength.HeaderText = ResourcesManager.Resources.R_Calibration_XPixelLength;
			dgvhYPixelLength.HeaderText = ResourcesManager.Resources.R_Calibration_YPixelLength;
			btnImport.Text = ResourcesManager.Resources.R_Calibration_Import;
			btnExport.Text = ResourcesManager.Resources.R_Calibration_Export;
			btnDelete.Text = ResourcesManager.Resources.R_Calibration_Delete;
			btnClear.Text = ResourcesManager.Resources.R_Calibration_Clear;
			gbAddCalibration.Text = ResourcesManager.Resources.R_Calibration_AddCalibration;
			tpHardnessCalibration.Text = ResourcesManager.Resources.R_Calibration_HardnessCalibration;
			tpLengthCalibration.Text = ResourcesManager.Resources.R_Calibration_LengthCalibration;
			lbHZoomTime.Text = ResourcesManager.Resources.R_Calibration_ZoomTime;
			lbHForce.Text = ResourcesManager.Resources.R_Calibration_Force;
			lbHHardnessLevel.Text = ResourcesManager.Resources.R_Calibration_HardnessLevel;
			lbHPixelLegnth.Text = ResourcesManager.Resources.R_Calibration_PixelLength;
			lbHHardness.Text = ResourcesManager.Resources.R_Calibration_Hardness;
			btnManualMeasure.Text = ResourcesManager.Resources.R_Calibration_ManualMeasure;
			btnAutoMeasure.Text = ResourcesManager.Resources.R_Calibration_AutoMeasure;
			btnAddHardnessCalib.Text = ResourcesManager.Resources.R_Calibration_AddCalibration;
			rbLinearCalib.Text = ResourcesManager.Resources.R_Calibration_LinearCalibration;
			rbPlaneCalib.Text = ResourcesManager.Resources.R_Calibration_PlaneCalibration;
			lbLZoomTime.Text = ResourcesManager.Resources.R_Calibration_ZoomTime;
			lbLForce.Text = ResourcesManager.Resources.R_Calibration_Force;
			lbLHardnessLevel.Text = ResourcesManager.Resources.R_Calibration_HardnessLevel;
			lbLPixelLength.Text = ResourcesManager.Resources.R_Calibration_PixelLength;
			lbLRealDistance.Text = ResourcesManager.Resources.R_Calibration_RealDistance;
			btnAddLengthCalib.Text = ResourcesManager.Resources.R_Calibration_AddCalibration;
		}

		public void SetZoomTimeBox(object[] items)
		{
			cbHZoomTime.Items.Clear();
			cbHZoomTime.Items.AddRange(items);
			cbLZoomTime.Items.Clear();
			cbLZoomTime.Items.AddRange(items);
		}

		public void SetForceBox(object[] items)
		{
			cbHForce.Items.Clear();
			cbHForce.Items.AddRange(items);
			cbLForce.Items.Clear();
			cbLForce.Items.AddRange(items);
		}

		public void SetHardnessLevelBox(object[] items)
		{
			cbHHardnessLevel.Items.Clear();
			cbHHardnessLevel.Items.AddRange(items);
			cbLHardnessLevel.Items.Clear();
			cbLHardnessLevel.Items.AddRange(items);
		}

		public void SetCurrentCalibrationOption(string zoomTime, string force, string hardnessLevel)
		{
			cbHZoomTime.Text = zoomTime;
			cbHForce.Text = force;
			cbHHardnessLevel.Text = hardnessLevel;
			cbLZoomTime.Text = zoomTime;
			cbLForce.Text = force;
			cbLHardnessLevel.Text = hardnessLevel;
		}

		private bool CheckLengthCalibInput()
		{
			if (cbLZoomTime.SelectedIndex == -1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseSelectZoomTime);
				cbLZoomTime.Focus();
				return false;
			}
			if (cbLForce.SelectedIndex == -1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseSelectCorrectForce);
				cbLForce.Focus();
				return false;
			}
			if (cbLHardnessLevel.SelectedIndex == -1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseSelectCorrectHardnessLevel);
				cbLHardnessLevel.Focus();
				return false;
			}
			if (tbLXPixel.Text == null || tbLXPixel.Text == "")
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseDrawLineInImage);
				tbLXPixel.Focus();
				return false;
			}
			if (rbPlaneCalib.Checked && (tbLYPixel.Text == null || tbLYPixel.Text == ""))
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseDrawLineInImage);
				tbLYPixel.Focus();
				return false;
			}
			float distance = 0f;
			if (!float.TryParse(tbLXDistance.Text, out distance))
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_RealDistanceValueInvalid);
				tbLXDistance.Focus();
				return false;
			}
			if (rbPlaneCalib.Checked && !float.TryParse(tbLYDistance.Text, out distance))
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_RealDistanceValueInvalid);
				tbLYDistance.Focus();
				return false;
			}
			return true;
		}

		private bool CheckHardnessCalibInput()
		{
			if (cbHZoomTime.SelectedIndex == -1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseSelectZoomTime);
				cbHZoomTime.Focus();
				return false;
			}
			if (cbHForce.SelectedIndex == -1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseSelectCorrectForce);
				cbHForce.Focus();
				return false;
			}
			if (cbHHardnessLevel.SelectedIndex == -1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseSelectCorrectHardnessLevel);
				cbHHardnessLevel.Focus();
				return false;
			}
			if (tbHXPixel.Text == null || tbHXPixel.Text == "")
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseDrawLineInImage);
				tbHXPixel.Focus();
				return false;
			}
			if (tbHYPixel.Text == null || tbHYPixel.Text == "")
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_PleaseDrawLineInImage);
				tbHYPixel.Focus();
				return false;
			}
			float hardness = 0f;
			if (!float.TryParse(tbHardnessValue.Text, out hardness))
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_HardnessValueInvalid);
				tbHardnessValue.Focus();
				return false;
			}
			return true;
		}

		private void RenumberCalibrationList()
		{
			for (int i = 0; i < calibrationList.Count; i++)
			{
				calibrationList[i].Index = i + 1;
			}
			dgvCalibrations.Invalidate();
		}

		private void ImageBox_OnFourLineChanged(object sender, MeasuringGraphicsEventArgs e)
		{
			tbHXPixel.Text = e.XImagePixel.ToString();
			tbHYPixel.Text = e.YImagePixel.ToString();
			tbLXPixel.Text = e.XImagePixel.ToString();
			tbLYPixel.Text = e.YImagePixel.ToString();
		}

		private void ImageBox_OnRangeFinderChanged(object sender, MeasuringGraphicsEventArgs e)
		{
			string pixel = Math.Sqrt(e.XImagePixel * e.XImagePixel + e.YImagePixel * e.YImagePixel).ToString();
			tbLXPixel.Text = pixel;
			tbLYPixel.Text = pixel;
		}

		private void buildCalibTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (buildCalibTab.SelectedIndex == 0)
			{
				owner.imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
				owner.imageBox.ActiveTool = DrawToolType.FourLine;
				owner.imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
			}
			else if (buildCalibTab.SelectedIndex == 1)
			{
				rbLinearCalib_CheckedChanged(sender, e);
				rbPlaneCalib_CheckedChanged(sender, e);
			}
		}

		private void rbLinearCalib_CheckedChanged(object sender, EventArgs e)
		{
			if (rbLinearCalib.Checked)
			{
				tbLYPixel.Enabled = false;
				tbLYDistance.Enabled = false;
				owner.imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
				owner.imageBox.ActiveTool = DrawToolType.RangeFinder;
				owner.imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
			}
		}

		private void rbPlaneCalib_CheckedChanged(object sender, EventArgs e)
		{
			if (rbPlaneCalib.Checked)
			{
				tbLYPixel.Enabled = true;
				tbLYDistance.Enabled = true;
				owner.imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
				owner.imageBox.ActiveTool = DrawToolType.FourLine;
				owner.imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
			}
		}

		private void btnAddLengthCalib_Click(object sender, EventArgs e)
		{
			try
			{
				if (!CheckLengthCalibInput())
				{
					return;
				}
				string zoomTime = cbLZoomTime.Text;
				string force = cbLForce.Text;
				string hardnessLevel = cbLHardnessLevel.Text;
				CalibrationInfo oldCalibrationInfo = calibrationList.FirstOrDefault((CalibrationInfo x) => x.ZoomTime == zoomTime && x.Force == force && x.HardnessLevel == hardnessLevel);
				if (oldCalibrationInfo == null || MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Calibration_Message_TargetCalibrationAlreadExist.Replace("{INFO1}", zoomTime).Replace("{INFO2}", force).Replace("{INFO3}", hardnessLevel), ResourcesManager.Resources.R_Calibration_AddCalibration) != DialogResult.Cancel)
				{
					float xPixel = float.Parse(tbLXPixel.Text);
					float xDistance = float.Parse(tbLXDistance.Text);
					float xPixelLength = xDistance / xPixel;
					float yPixelLength = (rbLinearCalib.Checked ? xPixelLength : (float.Parse(tbLYDistance.Text) / float.Parse(tbLYPixel.Text)));
					CalibrationInfo calibrationInfo = new CalibrationInfo(calibrationList.Count + 1, zoomTime, force, hardnessLevel, xPixelLength, yPixelLength);
					calibrationList.Add(calibrationInfo);
					if (oldCalibrationInfo != null)
					{
						calibrationList.Remove(oldCalibrationInfo);
					}
					RenumberCalibrationList();
					ConfigFileManager.SaveCalibrationConfigFile(calibrationList, filepath);
					try
					{
						owner.UpdateCalibrationInfo();
					}
					catch
					{
					}
					owner.imageBox.Invalidate();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to add length calibration information");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Calibration_Message_FailedToAddCalibration);
			}
		}

		private void btnAddHardnessCalib_Click(object sender, EventArgs e)
		{
			if (!CheckHardnessCalibInput())
			{
				return;
			}
			string zoomTime = cbHZoomTime.Text;
			string force = cbHForce.Text;
			string hardnessLevel = cbHHardnessLevel.Text;
			CalibrationInfo oldCalibrationInfo = calibrationList.FirstOrDefault((CalibrationInfo x) => x.ZoomTime == zoomTime && x.Force == force && x.HardnessLevel == hardnessLevel);
			if (oldCalibrationInfo == null || MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Calibration_Message_TargetCalibrationAlreadExist.Replace("{INFO1}", zoomTime).Replace("{INFO2}", force).Replace("{INFO3}", hardnessLevel), ResourcesManager.Resources.R_Calibration_AddCalibration) != DialogResult.Cancel)
			{
				float.TryParse(tbHXPixel.Text, out var xPixel);
				float.TryParse(tbHYPixel.Text, out var yPixel);
				float.TryParse(tbHardnessValue.Text, out var hardness);
				float pixelLength = 0f;
				if (owner.genericInfo.SoftwareSeries == SoftwareSeries.HV)
				{
					float kgf = float.Parse(force.Replace("kgf", ""));
					float physicalDiagonal = (float)Math.Sqrt(0.1891 * (double)kgf * 9.80665 / (double)hardness);
					pixelLength = physicalDiagonal / (xPixel + yPixel) * 2f * 1000f;
				}
				else if (owner.genericInfo.SoftwareSeries == SoftwareSeries.HK)
				{
					float kgf = float.Parse(force.Replace("kgf", ""));
					float physicalDiagonal = (float)Math.Sqrt(1.451 * (double)kgf * 9.80665 / (double)hardness);
					pixelLength = physicalDiagonal / xPixel * 1000f;
				}
				else if (owner.genericInfo.SoftwareSeries == SoftwareSeries.HBW)
				{
					string[] tempArr = force.Substring(3).Split('/');
					float diameter = float.Parse(tempArr[0]);
					float kgf = float.Parse(tempArr[1]);
					float temp = diameter - 2.00056f * kgf / hardness / (float)(Math.PI * (double)diameter);
					float realDavg = 1000f * (float)Math.Sqrt(diameter * diameter - temp * temp);
					pixelLength = realDavg / (xPixel + yPixel) * 2f;
				}
				CalibrationInfo calibrationInfo = new CalibrationInfo(calibrationList.Count + 1, zoomTime, force, hardnessLevel, pixelLength, pixelLength);
				calibrationList.Add(calibrationInfo);
				if (oldCalibrationInfo != null)
				{
					calibrationList.Remove(oldCalibrationInfo);
				}
				RenumberCalibrationList();
				ConfigFileManager.SaveCalibrationConfigFile(calibrationList, filepath);
				try
				{
					owner.UpdateCalibrationInfo();
				}
				catch
				{
				}
				owner.imageBox.Invalidate();
			}
		}

		private void btnManualMeasure_Click(object sender, EventArgs e)
		{
			owner.imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
			owner.imageBox.ActiveTool = DrawToolType.FourLine;
			owner.imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
		}

		private void btnAutoMeasure_Click(object sender, EventArgs e)
		{
			owner.AutoMeasure();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Calibration_Message_SureToImport, ResourcesManager.Resources.R_Calibration_Import);
			if (result == DialogResult.Cancel)
			{
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Config File|*.xml";
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			BindingList<CalibrationInfo> newCalibrationList = ConfigFileManager.LoadCalibrationConfigFile(openFileDialog.FileName);
			if (newCalibrationList == null)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Calibration_Message_FailedToImport);
				return;
			}
			calibrationList = newCalibrationList;
			owner.calibrationList = newCalibrationList;
			dgvCalibrations.DataSource = calibrationList;
			dgvCalibrations.Invalidate();
			try
			{
				owner.UpdateCalibrationInfo();
			}
			catch
			{
			}
			owner.imageBox.Invalidate();
			ConfigFileManager.SaveCalibrationConfigFile(calibrationList, filepath);
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Config File|*.xml";
			saveFileDialog.FileName = "Calibration" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".xml";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				ConfigFileManager.SaveCalibrationConfigFile(calibrationList, saveFileDialog.FileName);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			int rowCount = dgvCalibrations.SelectedRows.Count;
			if (rowCount == 0)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Calibration_Message_NoSelectedLine);
				return;
			}
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Calibration_Message_SureToDeleteThoseLine.Replace("{INFO}", rowCount.ToString()), ResourcesManager.Resources.R_Calibration_Delete);
			if (result == DialogResult.Cancel)
			{
				return;
			}
			for (int i = dgvCalibrations.SelectedRows.Count - 1; i >= 0; i--)
			{
				int index = (int)dgvCalibrations.SelectedRows[i].Cells[0].Value;
				calibrationList.Remove(calibrationList.FirstOrDefault((CalibrationInfo x) => x.Index == index));
			}
			RenumberCalibrationList();
			dgvCalibrations.Invalidate();
			ConfigFileManager.SaveCalibrationConfigFile(calibrationList, filepath);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Calibration_Message_SureToClear, ResourcesManager.Resources.R_Calibration_Clear);
			if (result != DialogResult.Cancel)
			{
				calibrationList.Clear();
				dgvCalibrations.Invalidate();
				ConfigFileManager.SaveCalibrationConfigFile(calibrationList, filepath);
			}
		}

		private void CalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			owner.imageBox.OnFourLineChanged -= ImageBox_OnFourLineChanged;
			owner.imageBox.OnRangeFinderChanged -= ImageBox_OnRangeFinderChanged;
			isCalibrationFormOpened = false;
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
            this.gbAddCalibration = new Krypton.Toolkit.KryptonGroupBox();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.buildCalibTab = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.tpHardnessCalibration = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.cbHHardnessLevel = new Krypton.Toolkit.KryptonComboBox();
            this.lbHZoomTime = new Krypton.Toolkit.KryptonLabel();
            this.lbHHardnessLevel = new Krypton.Toolkit.KryptonLabel();
            this.cbHZoomTime = new Krypton.Toolkit.KryptonComboBox();
            this.cbHForce = new Krypton.Toolkit.KryptonComboBox();
            this.lbHPixelLegnth = new Krypton.Toolkit.KryptonLabel();
            this.lbHForce = new Krypton.Toolkit.KryptonLabel();
            this.label7 = new Krypton.Toolkit.KryptonLabel();
            this.tbHardnessValue = new Krypton.Toolkit.KryptonTextBox();
            this.label6 = new Krypton.Toolkit.KryptonLabel();
            this.tbHYPixel = new Krypton.Toolkit.KryptonTextBox();
            this.btnAddHardnessCalib = new Krypton.Toolkit.KryptonButton();
            this.tbHXPixel = new Krypton.Toolkit.KryptonTextBox();
            this.lbHHardness = new Krypton.Toolkit.KryptonLabel();
            this.btnAutoMeasure = new Krypton.Toolkit.KryptonButton();
            this.btnManualMeasure = new Krypton.Toolkit.KryptonButton();
            this.tpLengthCalibration = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.cbLHardnessLevel = new Krypton.Toolkit.KryptonComboBox();
            this.rbLinearCalib = new Krypton.Toolkit.KryptonRadioButton();
            this.lbLHardnessLevel = new Krypton.Toolkit.KryptonLabel();
            this.lbLZoomTime = new Krypton.Toolkit.KryptonLabel();
            this.cbLForce = new Krypton.Toolkit.KryptonComboBox();
            this.cbLZoomTime = new Krypton.Toolkit.KryptonComboBox();
            this.lbLForce = new Krypton.Toolkit.KryptonLabel();
            this.lbLPixelLength = new Krypton.Toolkit.KryptonLabel();
            this.label13 = new Krypton.Toolkit.KryptonLabel();
            this.lbLRealDistance = new Krypton.Toolkit.KryptonLabel();
            this.label12 = new Krypton.Toolkit.KryptonLabel();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.tbLYDistance = new Krypton.Toolkit.KryptonTextBox();
            this.label5 = new Krypton.Toolkit.KryptonLabel();
            this.tbLXDistance = new Krypton.Toolkit.KryptonTextBox();
            this.btnAddLengthCalib = new Krypton.Toolkit.KryptonButton();
            this.tbLYPixel = new Krypton.Toolkit.KryptonTextBox();
            this.rbPlaneCalib = new Krypton.Toolkit.KryptonRadioButton();
            this.tbLXPixel = new Krypton.Toolkit.KryptonTextBox();
            this.btnDelete = new Krypton.Toolkit.KryptonButton();
            this.btnClear = new Krypton.Toolkit.KryptonButton();
            this.btnExport = new Krypton.Toolkit.KryptonButton();
            this.btnImport = new Krypton.Toolkit.KryptonButton();
            this.dgvCalibrations = new Krypton.Toolkit.KryptonDataGridView();
            this.dgvhIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvhZoomTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvhForce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvhHardnessLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvhXPixelLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvhYPixelLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gbAddCalibration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbAddCalibration.Panel)).BeginInit();
            this.gbAddCalibration.Panel.SuspendLayout();
            this.gbAddCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildCalibTab)).BeginInit();
            this.buildCalibTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpHardnessCalibration)).BeginInit();
            this.tpHardnessCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbHHardnessLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHZoomTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpLengthCalibration)).BeginInit();
            this.tpLengthCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLHardnessLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLZoomTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalibrations)).BeginInit();
            this.SuspendLayout();
            // 
            // gbAddCalibration
            // 
            this.gbAddCalibration.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbAddCalibration.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbAddCalibration.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbAddCalibration.Location = new System.Drawing.Point(0, 288);
            this.gbAddCalibration.Margin = new System.Windows.Forms.Padding(6);
            this.gbAddCalibration.Name = "gbAddCalibration";
            this.gbAddCalibration.Palette = this.kp1;
            this.gbAddCalibration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbAddCalibration.Panel
            // 
            this.gbAddCalibration.Panel.Controls.Add(this.buildCalibTab);
            this.gbAddCalibration.Size = new System.Drawing.Size(515, 369);
            this.gbAddCalibration.TabIndex = 33;
            this.gbAddCalibration.Values.Heading = "Add Calibration";
            // 
            // buildCalibTab
            // 
            this.buildCalibTab.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.buildCalibTab.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.buildCalibTab.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.buildCalibTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildCalibTab.Location = new System.Drawing.Point(0, 0);
            this.buildCalibTab.Name = "buildCalibTab";
            this.buildCalibTab.PageBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.buildCalibTab.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.tpHardnessCalibration,
            this.tpLengthCalibration});
            this.buildCalibTab.SelectedIndex = 1;
            this.buildCalibTab.Size = new System.Drawing.Size(511, 341);
            this.buildCalibTab.TabIndex = 44;
            this.buildCalibTab.Text = "kryptonNavigator1";
            // 
            // tpHardnessCalibration
            // 
            this.tpHardnessCalibration.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpHardnessCalibration.Controls.Add(this.cbHHardnessLevel);
            this.tpHardnessCalibration.Controls.Add(this.lbHZoomTime);
            this.tpHardnessCalibration.Controls.Add(this.lbHHardnessLevel);
            this.tpHardnessCalibration.Controls.Add(this.cbHZoomTime);
            this.tpHardnessCalibration.Controls.Add(this.cbHForce);
            this.tpHardnessCalibration.Controls.Add(this.lbHPixelLegnth);
            this.tpHardnessCalibration.Controls.Add(this.lbHForce);
            this.tpHardnessCalibration.Controls.Add(this.label7);
            this.tpHardnessCalibration.Controls.Add(this.tbHardnessValue);
            this.tpHardnessCalibration.Controls.Add(this.label6);
            this.tpHardnessCalibration.Controls.Add(this.tbHYPixel);
            this.tpHardnessCalibration.Controls.Add(this.btnAddHardnessCalib);
            this.tpHardnessCalibration.Controls.Add(this.tbHXPixel);
            this.tpHardnessCalibration.Controls.Add(this.lbHHardness);
            this.tpHardnessCalibration.Controls.Add(this.btnAutoMeasure);
            this.tpHardnessCalibration.Controls.Add(this.btnManualMeasure);
            this.tpHardnessCalibration.Flags = 65534;
            this.tpHardnessCalibration.LastVisibleSet = true;
            this.tpHardnessCalibration.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpHardnessCalibration.Name = "tpHardnessCalibration";
            this.tpHardnessCalibration.Size = new System.Drawing.Size(509, 310);
            this.tpHardnessCalibration.Text = "Hardness Calibration";
            this.tpHardnessCalibration.ToolTipTitle = "Page ToolTip";
            this.tpHardnessCalibration.UniqueName = "FA01BA7CC73649C374A160B0A497B996";
            // 
            // cbHHardnessLevel
            // 
            this.cbHHardnessLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbHHardnessLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHHardnessLevel.DropDownWidth = 178;
            this.cbHHardnessLevel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbHHardnessLevel.FormattingEnabled = true;
            this.cbHHardnessLevel.IntegralHeight = false;
            this.cbHHardnessLevel.Items.AddRange(new object[] {
            "高块",
            "中块",
            "低块"});
            this.cbHHardnessLevel.Location = new System.Drawing.Point(182, 107);
            this.cbHHardnessLevel.Margin = new System.Windows.Forms.Padding(6);
            this.cbHHardnessLevel.Name = "cbHHardnessLevel";
            this.cbHHardnessLevel.Palette = this.kp1;
            this.cbHHardnessLevel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbHHardnessLevel.Size = new System.Drawing.Size(178, 25);
            this.cbHHardnessLevel.TabIndex = 39;
            // 
            // lbHZoomTime
            // 
            this.lbHZoomTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHZoomTime.Location = new System.Drawing.Point(20, 26);
            this.lbHZoomTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbHZoomTime.Name = "lbHZoomTime";
            this.lbHZoomTime.Palette = this.kp1;
            this.lbHZoomTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHZoomTime.Size = new System.Drawing.Size(89, 24);
            this.lbHZoomTime.TabIndex = 14;
            this.lbHZoomTime.Values.Text = "Zoom Time";
            // 
            // lbHHardnessLevel
            // 
            this.lbHHardnessLevel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHHardnessLevel.Location = new System.Drawing.Point(20, 108);
            this.lbHHardnessLevel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbHHardnessLevel.Name = "lbHHardnessLevel";
            this.lbHHardnessLevel.Palette = this.kp1;
            this.lbHHardnessLevel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHHardnessLevel.Size = new System.Drawing.Size(114, 24);
            this.lbHHardnessLevel.TabIndex = 38;
            this.lbHHardnessLevel.Values.Text = "Hardness Level";
            // 
            // cbHZoomTime
            // 
            this.cbHZoomTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbHZoomTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHZoomTime.DropDownWidth = 178;
            this.cbHZoomTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbHZoomTime.FormattingEnabled = true;
            this.cbHZoomTime.IntegralHeight = false;
            this.cbHZoomTime.Items.AddRange(new object[] {
            "5X",
            "10X",
            "20X",
            "40X",
            "50X"});
            this.cbHZoomTime.Location = new System.Drawing.Point(182, 25);
            this.cbHZoomTime.Margin = new System.Windows.Forms.Padding(6);
            this.cbHZoomTime.Name = "cbHZoomTime";
            this.cbHZoomTime.Palette = this.kp1;
            this.cbHZoomTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbHZoomTime.Size = new System.Drawing.Size(178, 25);
            this.cbHZoomTime.TabIndex = 15;
            // 
            // cbHForce
            // 
            this.cbHForce.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbHForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHForce.DropDownWidth = 178;
            this.cbHForce.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbHForce.FormattingEnabled = true;
            this.cbHForce.IntegralHeight = false;
            this.cbHForce.Items.AddRange(new object[] {
            "0.01kgf",
            "0.015kgf",
            "0.02kgf",
            "0.025kgf",
            "0.05kgf",
            "0.1kgf",
            "0.2kgf",
            "0.3kgf",
            "0.5kgf",
            "1kgf",
            "2kgf",
            "2.5kgf",
            "3kgf",
            "5kgf",
            "10kgf",
            "15kgf",
            "20kgf",
            "30kgf",
            "50kgf",
            "60kgf",
            "80kgf",
            "100kgf",
            "120kgf"});
            this.cbHForce.Location = new System.Drawing.Point(182, 66);
            this.cbHForce.Margin = new System.Windows.Forms.Padding(6);
            this.cbHForce.Name = "cbHForce";
            this.cbHForce.Palette = this.kp1;
            this.cbHForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbHForce.Size = new System.Drawing.Size(178, 25);
            this.cbHForce.TabIndex = 37;
            // 
            // lbHPixelLegnth
            // 
            this.lbHPixelLegnth.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHPixelLegnth.Location = new System.Drawing.Point(20, 149);
            this.lbHPixelLegnth.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbHPixelLegnth.Name = "lbHPixelLegnth";
            this.lbHPixelLegnth.Palette = this.kp1;
            this.lbHPixelLegnth.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHPixelLegnth.Size = new System.Drawing.Size(94, 24);
            this.lbHPixelLegnth.TabIndex = 16;
            this.lbHPixelLegnth.Values.Text = "Pixel Length";
            // 
            // lbHForce
            // 
            this.lbHForce.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHForce.Location = new System.Drawing.Point(20, 67);
            this.lbHForce.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbHForce.Name = "lbHForce";
            this.lbHForce.Palette = this.kp1;
            this.lbHForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHForce.Size = new System.Drawing.Size(49, 24);
            this.lbHForce.TabIndex = 36;
            this.lbHForce.Values.Text = "Force";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(177, 151);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Palette = this.kp1;
            this.label7.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label7.Size = new System.Drawing.Size(21, 24);
            this.label7.TabIndex = 23;
            this.label7.Values.Text = "X";
            // 
            // tbHardnessValue
            // 
            this.tbHardnessValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbHardnessValue.Location = new System.Drawing.Point(182, 188);
            this.tbHardnessValue.Margin = new System.Windows.Forms.Padding(6);
            this.tbHardnessValue.Name = "tbHardnessValue";
            this.tbHardnessValue.Palette = this.kp1;
            this.tbHardnessValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbHardnessValue.Size = new System.Drawing.Size(178, 27);
            this.tbHardnessValue.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(275, 151);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Palette = this.kp1;
            this.label6.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label6.Size = new System.Drawing.Size(20, 24);
            this.label6.TabIndex = 24;
            this.label6.Values.Text = "Y";
            // 
            // tbHYPixel
            // 
            this.tbHYPixel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbHYPixel.Location = new System.Drawing.Point(310, 148);
            this.tbHYPixel.Margin = new System.Windows.Forms.Padding(6);
            this.tbHYPixel.Name = "tbHYPixel";
            this.tbHYPixel.Palette = this.kp1;
            this.tbHYPixel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbHYPixel.ReadOnly = true;
            this.tbHYPixel.Size = new System.Drawing.Size(50, 27);
            this.tbHYPixel.TabIndex = 34;
            // 
            // btnAddHardnessCalib
            // 
            this.btnAddHardnessCalib.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddHardnessCalib.Location = new System.Drawing.Point(182, 232);
            this.btnAddHardnessCalib.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddHardnessCalib.Name = "btnAddHardnessCalib";
            this.btnAddHardnessCalib.Palette = this.kp1;
            this.btnAddHardnessCalib.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnAddHardnessCalib.Size = new System.Drawing.Size(150, 40);
            this.btnAddHardnessCalib.TabIndex = 21;
            this.btnAddHardnessCalib.Values.Text = "Add Hardness";
            this.btnAddHardnessCalib.Click += new System.EventHandler(this.btnAddHardnessCalib_Click);
            // 
            // tbHXPixel
            // 
            this.tbHXPixel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbHXPixel.Location = new System.Drawing.Point(213, 148);
            this.tbHXPixel.Margin = new System.Windows.Forms.Padding(6);
            this.tbHXPixel.Name = "tbHXPixel";
            this.tbHXPixel.Palette = this.kp1;
            this.tbHXPixel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbHXPixel.ReadOnly = true;
            this.tbHXPixel.Size = new System.Drawing.Size(50, 27);
            this.tbHXPixel.TabIndex = 33;
            // 
            // lbHHardness
            // 
            this.lbHHardness.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHHardness.Location = new System.Drawing.Point(20, 189);
            this.lbHHardness.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbHHardness.Name = "lbHHardness";
            this.lbHHardness.Palette = this.kp1;
            this.lbHHardness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHHardness.Size = new System.Drawing.Size(75, 24);
            this.lbHHardness.TabIndex = 29;
            this.lbHHardness.Values.Text = "Hardness";
            // 
            // btnAutoMeasure
            // 
            this.btnAutoMeasure.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoMeasure.Location = new System.Drawing.Point(372, 164);
            this.btnAutoMeasure.Margin = new System.Windows.Forms.Padding(6);
            this.btnAutoMeasure.Name = "btnAutoMeasure";
            this.btnAutoMeasure.Palette = this.kp1;
            this.btnAutoMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnAutoMeasure.Size = new System.Drawing.Size(122, 40);
            this.btnAutoMeasure.TabIndex = 32;
            this.btnAutoMeasure.Values.Text = "Auto Measure";
            this.btnAutoMeasure.Click += new System.EventHandler(this.btnAutoMeasure_Click);
            // 
            // btnManualMeasure
            // 
            this.btnManualMeasure.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnManualMeasure.Location = new System.Drawing.Point(372, 107);
            this.btnManualMeasure.Margin = new System.Windows.Forms.Padding(6);
            this.btnManualMeasure.Name = "btnManualMeasure";
            this.btnManualMeasure.Palette = this.kp1;
            this.btnManualMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnManualMeasure.Size = new System.Drawing.Size(122, 40);
            this.btnManualMeasure.TabIndex = 31;
            this.btnManualMeasure.Values.Text = "Manual Measure";
            this.btnManualMeasure.Click += new System.EventHandler(this.btnManualMeasure_Click);
            // 
            // tpLengthCalibration
            // 
            this.tpLengthCalibration.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpLengthCalibration.Controls.Add(this.cbLHardnessLevel);
            this.tpLengthCalibration.Controls.Add(this.rbLinearCalib);
            this.tpLengthCalibration.Controls.Add(this.lbLHardnessLevel);
            this.tpLengthCalibration.Controls.Add(this.lbLZoomTime);
            this.tpLengthCalibration.Controls.Add(this.cbLForce);
            this.tpLengthCalibration.Controls.Add(this.cbLZoomTime);
            this.tpLengthCalibration.Controls.Add(this.lbLForce);
            this.tpLengthCalibration.Controls.Add(this.lbLPixelLength);
            this.tpLengthCalibration.Controls.Add(this.label13);
            this.tpLengthCalibration.Controls.Add(this.lbLRealDistance);
            this.tpLengthCalibration.Controls.Add(this.label12);
            this.tpLengthCalibration.Controls.Add(this.label4);
            this.tpLengthCalibration.Controls.Add(this.tbLYDistance);
            this.tpLengthCalibration.Controls.Add(this.label5);
            this.tpLengthCalibration.Controls.Add(this.tbLXDistance);
            this.tpLengthCalibration.Controls.Add(this.btnAddLengthCalib);
            this.tpLengthCalibration.Controls.Add(this.tbLYPixel);
            this.tpLengthCalibration.Controls.Add(this.rbPlaneCalib);
            this.tpLengthCalibration.Controls.Add(this.tbLXPixel);
            this.tpLengthCalibration.Flags = 65534;
            this.tpLengthCalibration.LastVisibleSet = true;
            this.tpLengthCalibration.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpLengthCalibration.Name = "tpLengthCalibration";
            this.tpLengthCalibration.Size = new System.Drawing.Size(509, 310);
            this.tpLengthCalibration.Text = "Length Calibration";
            this.tpLengthCalibration.ToolTipTitle = "Page ToolTip";
            this.tpLengthCalibration.UniqueName = "0D3566BDBD3E4068DBA2646848AE19F3";
            // 
            // cbLHardnessLevel
            // 
            this.cbLHardnessLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLHardnessLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLHardnessLevel.DropDownWidth = 198;
            this.cbLHardnessLevel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLHardnessLevel.FormattingEnabled = true;
            this.cbLHardnessLevel.IntegralHeight = false;
            this.cbLHardnessLevel.Items.AddRange(new object[] {
            "High Block",
            "Middle Block",
            "Low Block"});
            this.cbLHardnessLevel.Location = new System.Drawing.Point(210, 136);
            this.cbLHardnessLevel.Margin = new System.Windows.Forms.Padding(6);
            this.cbLHardnessLevel.Name = "cbLHardnessLevel";
            this.cbLHardnessLevel.Palette = this.kp1;
            this.cbLHardnessLevel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbLHardnessLevel.Size = new System.Drawing.Size(198, 25);
            this.cbLHardnessLevel.TabIndex = 43;
            // 
            // rbLinearCalib
            // 
            this.rbLinearCalib.Checked = true;
            this.rbLinearCalib.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.rbLinearCalib.Location = new System.Drawing.Point(107, 29);
            this.rbLinearCalib.Margin = new System.Windows.Forms.Padding(6);
            this.rbLinearCalib.Name = "rbLinearCalib";
            this.rbLinearCalib.Palette = this.kp1;
            this.rbLinearCalib.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbLinearCalib.Size = new System.Drawing.Size(143, 24);
            this.rbLinearCalib.TabIndex = 25;
            this.rbLinearCalib.Values.Text = "Linear Calibration";
            this.rbLinearCalib.CheckedChanged += new System.EventHandler(this.rbLinearCalib_CheckedChanged);
            // 
            // lbLHardnessLevel
            // 
            this.lbLHardnessLevel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLHardnessLevel.Location = new System.Drawing.Point(48, 134);
            this.lbLHardnessLevel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbLHardnessLevel.Name = "lbLHardnessLevel";
            this.lbLHardnessLevel.Palette = this.kp1;
            this.lbLHardnessLevel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLHardnessLevel.Size = new System.Drawing.Size(114, 24);
            this.lbLHardnessLevel.TabIndex = 42;
            this.lbLHardnessLevel.Values.Text = "Hardness Level";
            // 
            // lbLZoomTime
            // 
            this.lbLZoomTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLZoomTime.Location = new System.Drawing.Point(48, 55);
            this.lbLZoomTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbLZoomTime.Name = "lbLZoomTime";
            this.lbLZoomTime.Palette = this.kp1;
            this.lbLZoomTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLZoomTime.Size = new System.Drawing.Size(89, 24);
            this.lbLZoomTime.TabIndex = 14;
            this.lbLZoomTime.Values.Text = "Zoom Time";
            // 
            // cbLForce
            // 
            this.cbLForce.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLForce.DropDownWidth = 198;
            this.cbLForce.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLForce.FormattingEnabled = true;
            this.cbLForce.IntegralHeight = false;
            this.cbLForce.Items.AddRange(new object[] {
            "0.01kgf",
            "0.015kgf",
            "0.02kgf",
            "0.025kgf",
            "0.05kgf",
            "0.1kgf",
            "0.2kgf",
            "0.3kgf",
            "0.5kgf",
            "1kgf",
            "2kgf",
            "2.5kgf",
            "3kgf",
            "5kgf",
            "10kgf",
            "15kgf",
            "20kgf",
            "30kgf",
            "50kgf",
            "60kgf",
            "80kgf",
            "100kgf",
            "120kgf"});
            this.cbLForce.Location = new System.Drawing.Point(210, 95);
            this.cbLForce.Margin = new System.Windows.Forms.Padding(6);
            this.cbLForce.Name = "cbLForce";
            this.cbLForce.Palette = this.kp1;
            this.cbLForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbLForce.Size = new System.Drawing.Size(198, 25);
            this.cbLForce.TabIndex = 41;
            // 
            // cbLZoomTime
            // 
            this.cbLZoomTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbLZoomTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLZoomTime.DropDownWidth = 198;
            this.cbLZoomTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLZoomTime.FormattingEnabled = true;
            this.cbLZoomTime.IntegralHeight = false;
            this.cbLZoomTime.Items.AddRange(new object[] {
            "10X",
            "20X",
            "40X",
            "50X"});
            this.cbLZoomTime.Location = new System.Drawing.Point(210, 54);
            this.cbLZoomTime.Margin = new System.Windows.Forms.Padding(6);
            this.cbLZoomTime.Name = "cbLZoomTime";
            this.cbLZoomTime.Palette = this.kp1;
            this.cbLZoomTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbLZoomTime.Size = new System.Drawing.Size(198, 25);
            this.cbLZoomTime.TabIndex = 15;
            // 
            // lbLForce
            // 
            this.lbLForce.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLForce.Location = new System.Drawing.Point(48, 93);
            this.lbLForce.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbLForce.Name = "lbLForce";
            this.lbLForce.Palette = this.kp1;
            this.lbLForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLForce.Size = new System.Drawing.Size(49, 24);
            this.lbLForce.TabIndex = 40;
            this.lbLForce.Values.Text = "Force";
            // 
            // lbLPixelLength
            // 
            this.lbLPixelLength.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLPixelLength.Location = new System.Drawing.Point(48, 178);
            this.lbLPixelLength.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbLPixelLength.Name = "lbLPixelLength";
            this.lbLPixelLength.Palette = this.kp1;
            this.lbLPixelLength.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLPixelLength.Size = new System.Drawing.Size(94, 24);
            this.lbLPixelLength.TabIndex = 16;
            this.lbLPixelLength.Values.Text = "Pixel Length";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(380, 218);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Palette = this.kp1;
            this.label13.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label13.Size = new System.Drawing.Size(34, 24);
            this.label13.TabIndex = 32;
            this.label13.Values.Text = "μm";
            // 
            // lbLRealDistance
            // 
            this.lbLRealDistance.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLRealDistance.Location = new System.Drawing.Point(50, 216);
            this.lbLRealDistance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbLRealDistance.Name = "lbLRealDistance";
            this.lbLRealDistance.Palette = this.kp1;
            this.lbLRealDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLRealDistance.Size = new System.Drawing.Size(103, 24);
            this.lbLRealDistance.TabIndex = 19;
            this.lbLRealDistance.Values.Text = "Real Distance";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(266, 218);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Palette = this.kp1;
            this.label12.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label12.Size = new System.Drawing.Size(34, 24);
            this.label12.TabIndex = 31;
            this.label12.Values.Text = "μm";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(210, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Palette = this.kp1;
            this.label4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label4.Size = new System.Drawing.Size(21, 24);
            this.label4.TabIndex = 23;
            this.label4.Values.Text = "X";
            // 
            // tbLYDistance
            // 
            this.tbLYDistance.Enabled = false;
            this.tbLYDistance.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLYDistance.Location = new System.Drawing.Point(327, 217);
            this.tbLYDistance.Margin = new System.Windows.Forms.Padding(6);
            this.tbLYDistance.Name = "tbLYDistance";
            this.tbLYDistance.Palette = this.kp1;
            this.tbLYDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLYDistance.Size = new System.Drawing.Size(50, 27);
            this.tbLYDistance.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(323, 180);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Palette = this.kp1;
            this.label5.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label5.Size = new System.Drawing.Size(20, 24);
            this.label5.TabIndex = 24;
            this.label5.Values.Text = "Y";
            // 
            // tbLXDistance
            // 
            this.tbLXDistance.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLXDistance.Location = new System.Drawing.Point(212, 217);
            this.tbLXDistance.Margin = new System.Windows.Forms.Padding(6);
            this.tbLXDistance.Name = "tbLXDistance";
            this.tbLXDistance.Palette = this.kp1;
            this.tbLXDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLXDistance.Size = new System.Drawing.Size(50, 27);
            this.tbLXDistance.TabIndex = 29;
            // 
            // btnAddLengthCalib
            // 
            this.btnAddLengthCalib.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddLengthCalib.Location = new System.Drawing.Point(167, 255);
            this.btnAddLengthCalib.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddLengthCalib.Name = "btnAddLengthCalib";
            this.btnAddLengthCalib.Palette = this.kp1;
            this.btnAddLengthCalib.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnAddLengthCalib.Size = new System.Drawing.Size(150, 40);
            this.btnAddLengthCalib.TabIndex = 21;
            this.btnAddLengthCalib.Values.Text = "Length Calibration";
            this.btnAddLengthCalib.Click += new System.EventHandler(this.btnAddLengthCalib_Click);
            // 
            // tbLYPixel
            // 
            this.tbLYPixel.Enabled = false;
            this.tbLYPixel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLYPixel.Location = new System.Drawing.Point(358, 177);
            this.tbLYPixel.Margin = new System.Windows.Forms.Padding(6);
            this.tbLYPixel.Name = "tbLYPixel";
            this.tbLYPixel.Palette = this.kp1;
            this.tbLYPixel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLYPixel.ReadOnly = true;
            this.tbLYPixel.Size = new System.Drawing.Size(50, 27);
            this.tbLYPixel.TabIndex = 28;
            // 
            // rbPlaneCalib
            // 
            this.rbPlaneCalib.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.rbPlaneCalib.Location = new System.Drawing.Point(270, 29);
            this.rbPlaneCalib.Margin = new System.Windows.Forms.Padding(6);
            this.rbPlaneCalib.Name = "rbPlaneCalib";
            this.rbPlaneCalib.Palette = this.kp1;
            this.rbPlaneCalib.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbPlaneCalib.Size = new System.Drawing.Size(139, 24);
            this.rbPlaneCalib.TabIndex = 26;
            this.rbPlaneCalib.Values.Text = "Plane Calibration";
            this.rbPlaneCalib.CheckedChanged += new System.EventHandler(this.rbPlaneCalib_CheckedChanged);
            // 
            // tbLXPixel
            // 
            this.tbLXPixel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLXPixel.Location = new System.Drawing.Point(242, 177);
            this.tbLXPixel.Margin = new System.Windows.Forms.Padding(6);
            this.tbLXPixel.Name = "tbLXPixel";
            this.tbLXPixel.Palette = this.kp1;
            this.tbLXPixel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLXPixel.ReadOnly = true;
            this.tbLXPixel.Size = new System.Drawing.Size(50, 27);
            this.tbLXPixel.TabIndex = 27;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(259, 236);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Palette = this.kp1;
            this.btnDelete.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnDelete.Size = new System.Drawing.Size(110, 40);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Values.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(381, 236);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Palette = this.kp1;
            this.btnClear.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnClear.Size = new System.Drawing.Size(110, 40);
            this.btnClear.TabIndex = 41;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(137, 236);
            this.btnExport.Margin = new System.Windows.Forms.Padding(6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Palette = this.kp1;
            this.btnExport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnExport.Size = new System.Drawing.Size(110, 40);
            this.btnExport.TabIndex = 42;
            this.btnExport.Values.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImport.Location = new System.Drawing.Point(15, 236);
            this.btnImport.Margin = new System.Windows.Forms.Padding(6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Palette = this.kp1;
            this.btnImport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnImport.Size = new System.Drawing.Size(110, 40);
            this.btnImport.TabIndex = 43;
            this.btnImport.Values.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dgvCalibrations
            // 
            this.dgvCalibrations.ColumnHeadersHeight = 36;
            this.dgvCalibrations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvhIndex,
            this.dgvhZoomTime,
            this.dgvhForce,
            this.dgvhHardnessLevel,
            this.dgvhXPixelLength,
            this.dgvhYPixelLength});
            this.dgvCalibrations.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvCalibrations.Location = new System.Drawing.Point(0, 0);
            this.dgvCalibrations.Name = "dgvCalibrations";
            this.dgvCalibrations.Palette = this.kp1;
            this.dgvCalibrations.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.dgvCalibrations.RowHeadersWidth = 51;
            this.dgvCalibrations.Size = new System.Drawing.Size(515, 202);
            this.dgvCalibrations.TabIndex = 44;
            // 
            // dgvhIndex
            // 
            this.dgvhIndex.DataPropertyName = "Index";
            this.dgvhIndex.HeaderText = "#";
            this.dgvhIndex.MinimumWidth = 6;
            this.dgvhIndex.Name = "dgvhIndex";
            this.dgvhIndex.ReadOnly = true;
            this.dgvhIndex.Width = 50;
            // 
            // dgvhZoomTime
            // 
            this.dgvhZoomTime.DataPropertyName = "ZoomTime";
            this.dgvhZoomTime.HeaderText = "Zoom Time";
            this.dgvhZoomTime.MinimumWidth = 6;
            this.dgvhZoomTime.Name = "dgvhZoomTime";
            this.dgvhZoomTime.ReadOnly = true;
            this.dgvhZoomTime.Width = 80;
            // 
            // dgvhForce
            // 
            this.dgvhForce.DataPropertyName = "Force";
            this.dgvhForce.HeaderText = "Force";
            this.dgvhForce.MinimumWidth = 6;
            this.dgvhForce.Name = "dgvhForce";
            this.dgvhForce.ReadOnly = true;
            this.dgvhForce.Width = 80;
            // 
            // dgvhHardnessLevel
            // 
            this.dgvhHardnessLevel.DataPropertyName = "HardnessLevel";
            this.dgvhHardnessLevel.HeaderText = "Hardness Level";
            this.dgvhHardnessLevel.MinimumWidth = 6;
            this.dgvhHardnessLevel.Name = "dgvhHardnessLevel";
            this.dgvhHardnessLevel.ReadOnly = true;
            this.dgvhHardnessLevel.Width = 80;
            // 
            // dgvhXPixelLength
            // 
            this.dgvhXPixelLength.DataPropertyName = "XPixelLength";
            this.dgvhXPixelLength.HeaderText = "X Pixel Length(μm/px)";
            this.dgvhXPixelLength.MinimumWidth = 6;
            this.dgvhXPixelLength.Name = "dgvhXPixelLength";
            this.dgvhXPixelLength.ReadOnly = true;
            this.dgvhXPixelLength.Width = 125;
            // 
            // dgvhYPixelLength
            // 
            this.dgvhYPixelLength.DataPropertyName = "YPixelLength";
            this.dgvhYPixelLength.HeaderText = "Y Pixel Length(μm/px)";
            this.dgvhYPixelLength.MinimumWidth = 6;
            this.dgvhYPixelLength.Name = "dgvhYPixelLength";
            this.dgvhYPixelLength.ReadOnly = true;
            this.dgvhYPixelLength.Width = 125;
            // 
            // CalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 657);
            this.Controls.Add(this.dgvCalibrations);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gbAddCalibration);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "CalibrationForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Calibration";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalibrationForm_FormClosing);
            this.Load += new System.EventHandler(this.CalibrationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbAddCalibration.Panel)).EndInit();
            this.gbAddCalibration.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbAddCalibration)).EndInit();
            this.gbAddCalibration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buildCalibTab)).EndInit();
            this.buildCalibTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tpHardnessCalibration)).EndInit();
            this.tpHardnessCalibration.ResumeLayout(false);
            this.tpHardnessCalibration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbHHardnessLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHZoomTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpLengthCalibration)).EndInit();
            this.tpLengthCalibration.ResumeLayout(false);
            this.tpLengthCalibration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbLHardnessLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLZoomTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalibrations)).EndInit();
            this.ResumeLayout(false);

		}

        private void CalibrationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
