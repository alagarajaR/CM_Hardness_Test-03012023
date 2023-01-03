using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using AIO_Client.Properties;
using Labtt.Communication;
using Labtt.Communication.MasterControl;
using Labtt.Communication.SYJKPlatform;
using Labtt.Communication.ZAxis;
using Labtt.Data;
using Labtt.DrawArea;
using Labtt.Meterage;
using Labtt.Service;
using MessageBoxExApp;
using Krypton.Toolkit;

namespace AIO_Client
{

	public class MainForm : KryptonForm
	{
		private CalibrationInfo currentCalibrationInfo = null;

		internal BindingList<CalibrationInfo> calibrationList = null;

		internal SampleInfo sampleInfo = null;

		internal CameraInfo cameraInfo = null;

		internal GenericInfo genericInfo = null;

		internal SavedData savedData = null;

		internal List<SerialPortInfo> portsInfo = null;

		internal HardnessTesterInfo hardnessTesterInfo = null;

		internal SYJKPlatformInfo syjkPlatformInfo = null;

		internal ZAxisInfo zAxisInfo = null;

		internal AutoMeasureInfo autoMeasureInfo = null;

		private MeasureRecord currentMeasureRecord = null;

		internal BindingList<MeasureRecord> singlePointRecordList = null;

		internal BindingList<MeasureRecord> multiPointRecordList = null;

		internal BindingList<PatternSet> patternList = null;

		private TaskManager taskManager = null;

		internal DVPCameraService cameraService = null;

		private CameraService camera = null;

		internal HardnessTester hardnessTester = null;

		private SYJKPlatform syjkPlatform = null;

		private ZAxis zAxis = null;

		private Serial micrometerSerialPort = null;

		private IPatternControl[] allPatternsControl;

		private bool isPlatformLocateInformed = false;

		private SetString setSystemStatus = null;

		private SetString setCameraStatus = null;

		private SetString setForce = null;

		private GetString getForce = null;

		private SetString setZoomTime = null;

		private GetString getZoomTime = null;

		private SetInt setLoadTime = null;

		private GetInt getLoadTime = null;

		private SetInt setLightness = null;

		private GetInt getLightness = null;

		private SetFloat setMicrometer = null;

		private GetFloat getMicrometer = null;

		private SetString setSYJKXPos = null;

		private SetString setSYJKYPos = null;

		private SetInt setSelectedRecordRow = null;

		private GetInt getSelectedRecordRow = null;

		private SetBool setHardnessTesterComponent = null;

		private SetBool setPlatformComponent = null;

		private SetBool setZAxisComponent = null;

		private VoidDelegate responseWhenTaskStart = null;

		private VoidDelegate responseWhenTaskPause = null;

		private VoidDelegate responseWhenTaskAborted = null;

		private VoidDelegate responseWhenTaskFinish = null;

		private SetDrawToolType setDrawToolComponent = null;

		private MeasuringGraphicsEventHandler fourLineChanged = null;

		private IContainer components = null;

		private StatusStrip statusStrip1;

		private MenuStrip menuStrip;

		private ToolStripStatusLabel statusSystem;

		private ToolStripMenuItem tsmiFile;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem tsmiDevice;

		private ToolStripMenuItem tsmiData;

		private ToolStripMenuItem tsmiTools;

		private ToolStripMenuItem tsmiConfiguration;

		private ToolStripMenuItem tsmiHelp;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem tsmiAbout;

		private ToolStripStatusLabel statusCamera;

		private ToolStripSeparator toolStripSeparator2;

		private ToolTip toolTip;

		private SplitContainer splitContainer1;

		private KryptonButton btnAutoMeasure;

		private KryptonButton btnOpen;

		private KryptonButton btnResumeImage;

		private KryptonButton btnMagnifier;

		private KryptonButton btnSave;

		private KryptonButton btnClearGraphics;

		private KryptonButton btnCameraStart;

		private KryptonButton btnMeasureAngle;

		private KryptonButton btnCameraPause;

		private KryptonLabel lbYPos;

		private KryptonButton btnMeasureLength;

		private KryptonLabel label6;

		private KryptonButton btnPointer;

		private KryptonLabel lbXPos;

		private KryptonButton btnManualMeasure;

		private KryptonLabel label2;

		private KryptonLabel lbMicrometer;

		private KryptonLabel lbForce;

		private KryptonComboBox cbHardnessLevel;

		internal KryptonLabel lbCPValue;

		private KryptonLabel lbCP;

		internal KryptonLabel lbCPKValue;

		private KryptonLabel lbCPK;

		internal KryptonLabel lbVarianceValue;

		private KryptonLabel lbVariance;

		private KryptonLabel lbNumber;

		internal KryptonLabel lbStdDevValue;

		internal KryptonLabel lbAvgValue;

		internal KryptonLabel lbMinValue;

		internal KryptonLabel lbMaxValue;

		internal KryptonLabel lbNumberValue;

		private KryptonLabel lbStdDev;

		private KryptonLabel lbAvg;

		private KryptonLabel lbMin;

		private KryptonLabel lbMax;
        private KryptonTableLayoutPanel tableLayoutPanel2;
        private KryptonGroupBox gbZAxis;

		private KryptonButton btnAutoFocus;

		private KryptonButton btnZAxisUnlock;

		private KryptonButton btnZAxisLock;

		private KryptonButton btnZAxisDownward;

		private KryptonButton btnZAxisUpward;

		private KryptonGroupBox gbPlatform;
        private KryptonTableLayoutPanel tableLayoutPanel4;
        private KryptonLabel lbSYJKYPos;

		private KryptonLabel label7;

		private KryptonButton btnLockMotor;

		private KryptonButton btnXYLocalCenter;

		private KryptonLabel lbSYJKXPos;

		private KryptonButton btnPLCLeftForward;

		private KryptonLabel label4;

		private KryptonButton btnPLCRightward;

		private KryptonButton btnPLCCenter;

		private KryptonButton btnPLCRightBackward;

		private KryptonButton btnPLCLeftBackward;

		private KryptonButton btnUnlockMotor;

		private KryptonButton btnPLCBackward;

		private KryptonRadioButton rbSlow;

		private KryptonRadioButton rbMedium;

		private KryptonRadioButton rbFast;

		private KryptonButton btnPLCForward;

		private KryptonRadioButton rbVeryFast;

		private KryptonButton btnPLCRightForward;

		private KryptonButton btnPLCLeftward;

		private TableLayoutPanel tableLayoutPanel5;

		private KryptonButton btnPauseOrResumeMultiPointMode;

		private KryptonRadioButton rbImpressThenMeasure;

		private KryptonRadioButton rbImpressAndMeasure;

		private KryptonButton btnStart;

		private KryptonButton btnAddPattern;

		private KryptonCheckBox cbMultiLines;

		private KryptonRadioButton rbImpressOnly;

		private KryptonCheckBox cbFocusAll;

		private KryptonButton btnResetPattern;

		private KryptonButton btnFinishMultiPointMode;

		private TableLayoutPanel tableLayoutPanel3;

		private KryptonComboBox cbMultiPointsMode;

		private KryptonLabel lbPattern;

		private KryptonDataGridView dgvPatterns;

		private KryptonLabel lbHardnessLevel;

		private KryptonLabel lbConvertValue;

		private KryptonButton btnStatistics;

		private KryptonLabel lbHardnessType;

		private KryptonLabel lbTurret;

		private KryptonLabel lbHardnessValue;

		private KryptonLabel lbMicrometerValue;

		private KryptonLabel lbLightness;

		private KryptonButton btnTurretCenter;

		internal KryptonComboBox cbForce;

		private KryptonLabel lbLoadTime;

		private KryptonDataGridView dgvRecords;

		private KryptonComboBox cbZoomTime;

		private KryptonButton btnExportReport;

		private KryptonLabel lbObjective;

		private KryptonButton btnClearRecord;

		private KryptonButton btnImpress;

		private KryptonButton btnDeleteRecord;

		private KryptonButton btnEditRecord;

		private KryptonButton btnTurretLeft;

		private KryptonComboBox cbConvertType;

		private KryptonButton btnTurretRight;

		private PatternVerticalControl patternVertical;

		private PatternSlashControl patternSlash;

		private PatternMatrixControl patternMatrix;

		private PatternFreeControl patternFree;

		private PatternHorizontalControl patternHorizontal;

		private KryptonDataGridViewTextBoxColumn dgvhRecordIndex;

		private KryptonDataGridViewTextBoxColumn dgvhRecordXPos;

		private KryptonDataGridViewTextBoxColumn dgvhRecordYPos;

		private KryptonDataGridViewButtonColumn dgvhRecordMoveTo;

		private KryptonDataGridViewTextBoxColumn dgvhRecordHardness;

		private KryptonDataGridViewTextBoxColumn dgvhRecordHardnessType;

		private KryptonDataGridViewTextBoxColumn dgvhRecordQualified;

		private KryptonDataGridViewTextBoxColumn dgvhRecordD1;

		private KryptonDataGridViewTextBoxColumn dgvhRecordD2;

		private KryptonDataGridViewTextBoxColumn dgvhRecordDavg;

		private KryptonDataGridViewTextBoxColumn dgvhRecordConvertType;

		private KryptonDataGridViewTextBoxColumn dgvhRecordConvertValue;

		private KryptonDataGridViewTextBoxColumn dgvhRecordDepth;

		private KryptonDataGridViewTextBoxColumn dgvhRecordMeasureTime;

		private KryptonDataGridViewTextBoxColumn dgvhOriginalImagePath;

		private KryptonDataGridViewTextBoxColumn dgvhRecordImagePath;

		private MeasureRecordFlowLayout measureRecordFlowLayout;

		private KryptonDataGridViewTextBoxColumn dgvhPatternIndex;

		private KryptonDataGridViewTextBoxColumn Identifier;

		private KryptonDataGridViewTextBoxColumn dgvhPatternName;

		private KryptonDataGridViewTextBoxColumn dgvhPatternPointsCount;

		private KryptonDataGridViewCheckBoxColumn dgvhPatternSelected;

		private KryptonButton btnCenterCrossLine;

		private ToolStripSeparator toolStripSeparator3;
        private PatternCircleExtensionControl patternCircleExtension;
        private KryptonPalette kp1;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown nudLoadTime;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown nudLightness;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator tcFuncArea;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpMachineControl;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpXYZ;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpMultiPoints;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpPatternList;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpStatistics;
        private ComponentFactory.Krypton.Navigator.KryptonPage tpAlbum;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbon kribbon_Main;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Open;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Save;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Play;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Pause;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_AutoMeasure;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_ManualMeasure;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Cursor;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Angle;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_Clear;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_ZoomIn;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_ZoomOut;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton kOATBtn_CrossLine;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab krtab_File;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup krgrp_File;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple krbntrip_File;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiOpenImage;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiSaveImage;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiSaveOriginalImage;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple krbntrip_Exit;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiExit;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab krtab_Device;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab krtab_Data;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab krtab_Tools;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab krtab_Config;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiCameraStart;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiCameraStop;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiSampleInfo;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup3;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple3;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiAutoMeasure;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiManualMeasure;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple4;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiPointer;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiMeasureLength;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiMeasureAngle;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple5;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiMagnifier;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiResumeImage;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple6;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiClearGraphics;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiTrimMeasure;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup4;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple7;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton kryptonRibbonGroupButton6;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup kryptonRibbonGroup5;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple8;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiCalibration;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiForceCorrect;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiAutoMeasureSetting;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple9;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiCameraSetting;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiSerialPortSetting;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiXYPlatformSetting;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple10;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiZAxisSetting;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiGenericSetting;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiOtherSetting;
        private KryptonGroupBox kryptonGroupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private KryptonButton btnClearPatternSet;
        private KryptonButton btnDeletePatternSet;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private KryptonButton btnplcrightward2;
        private KryptonButton btnplcbackward2;
        private KryptonButton btnplcforward2;
        private KryptonButton btnplcleftward2;
        internal ImageBox imageBox;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton tsmiCenterCrossLine;

		internal string SystemStatus
		{
			set
			{
				Invoke(setSystemStatus, value);
			}
		}

		internal string CameraStatus
		{
			set
			{
				try
				{
					Invoke(setCameraStatus, value);
				}
				catch
				{
				}
			}
		}

		internal string Force
		{
			get
			{
				return Invoke(getForce).ToString();
			}
			set
			{
				Invoke(setForce, value);
			}
		}

		internal string ZoomTime
		{
			get
			{
				return Invoke(getZoomTime).ToString();
			}
			set
			{
				Invoke(setZoomTime, value);
			}
		}

		internal float MicrometerValue
		{
			get
			{
				return (float)Invoke(getMicrometer);
			}
			set
			{
				Invoke(setMicrometer, value);
			}
		}

		internal int LoadTime
		{
			get
			{
				return (int)Invoke(getLoadTime);
			}
			set
			{
				Invoke(setLoadTime, value);
			}
		}

		internal int Lightness
		{
			get
			{
				return (int)Invoke(getLightness);
			}
			set
			{
				Invoke(setLightness, value);
			}
		}

		private PointF SYJKLocation
		{
			set
			{
				Invoke(setSYJKXPos, value.X.ToString("F3"));
				Invoke(setSYJKYPos, value.Y.ToString("F3"));
			}
		}

		private int SelectedRecordRow
		{
			get
			{
				return (int)Invoke(getSelectedRecordRow);
			}
			set
			{
				Invoke(setSelectedRecordRow, value);
			}
		}

		internal bool EnableHardnessTesterComponent
		{
			set
			{
				Invoke(setHardnessTesterComponent, value);
			}
		}

		internal bool EnablePlatformComponent
		{
			set
			{
				Invoke(setPlatformComponent, value);
			}
		}

		internal bool EnableZAxisComponent
		{
			set
			{
				Invoke(setZAxisComponent, value);
			}
		}

		internal bool EnableSerialPortComponent
		{
			set
			{
				if (hardnessTester != null)
				{
					EnableHardnessTesterComponent = value;
				}
				if (syjkPlatform != null)
				{
					EnablePlatformComponent = value;
				}
				if (zAxis != null)
				{
					EnableZAxisComponent = value;
				}
			}
		}

		internal DrawToolType EnableActiveTool
		{
			set
			{
				Invoke(setDrawToolComponent, value);
			}
		}

		public MainForm()
		{
			InitializeComponent();

			kp1.ResetToDefaults(true);
			kp1.BasePaletteMode=Program.palete_mode;

			tcFuncArea.PaletteMode = Program.palete_mode1;

            kribbon_Main.PaletteMode = Program.palete_mode1;

            camera = new CameraService();
			cameraService = new DVPCameraService();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			LoadLanguageResources();
            int sw = Screen.GetWorkingArea(this).Width;
            int sh = Screen.GetWorkingArea(this).Height;

            kryptonGroupBox1.Height = sh-100;



            if (Program.palete_mode == Krypton.Toolkit.PaletteMode.Office2010Blue)
			{
				menuStrip.BackColor = Color.FromArgb(192, 192, 255);
				
			}
			else if(Program.palete_mode == Krypton.Toolkit.PaletteMode.Office2010Silver)
			{
				menuStrip.BackColor = Color.FromArgb(241, 241, 241);
			}
			else if (Program.palete_mode == Krypton.Toolkit.PaletteMode.Office2010Black)
			{
				menuStrip.BackColor = Color.Silver;
			}

			//splitContainer1.Panel1.BackColor = menuStrip.BackColor;
			//splitContainer1.Panel2.BackColor = menuStrip.BackColor;



			int screenWidth = Screen.PrimaryScreen.Bounds.Width;
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			if (screenWidth >= 1920 && screenWidth >= 1080)
			{
				Font = new Font("Microsoft YaHei UI", 15f);
				menuStrip.Font = new Font("Microsoft YaHei UI", 15f);
				statusStrip1.Font = new Font("Microsoft YaHei UI", 15f);
				imageBox.Width = (int)((double)imageBox.Height / 0.75);
				btnplcleftward2.Location = new Point(btnplcleftward2.Location.X, imageBox.Location.Y + imageBox.Height / 2 - btnplcleftward2.Height / 2);
				btnplcrightward2.Location = new Point(imageBox.Location.X + imageBox.Width, imageBox.Location.Y + imageBox.Height / 2 - btnplcrightward2.Height / 2);
				btnplcforward2.Location = new Point(imageBox.Location.X + imageBox.Width / 2 - btnplcforward2.Width / 2, btnplcforward2.Location.Y);
				btnplcbackward2.Location = new Point(imageBox.Location.X + imageBox.Width / 2 - btnplcbackward2.Width / 2, btnplcbackward2.Location.Y);
				splitContainer1.SplitterDistance = btnplcrightward2.Location.X + btnplcrightward2.Width + btnplcleftward2.Location.X + 35;
				base.Width = splitContainer1.SplitterDistance + dgvRecords.Width + 20;
				lbHardnessValue.Font = new Font("Microsoft YaHei UI", 18f, FontStyle.Bold);
				lbConvertValue.Font = new Font("Microsoft YaHei UI", 18f, FontStyle.Bold);
				btnImpress.Font = new Font("Microsoft YaHei UI", 18f, FontStyle.Bold);
				patternCircleExtension.Font = Font;
				patternFree.Font = Font;
				patternHorizontal.Font = Font;
				patternMatrix.Font = Font;
				patternSlash.Font = Font;
				patternVertical.Font = Font;
				int x = (screenWidth - base.Width) / 2;
				int y = (screenHeight - base.Height) / 2 - 10;
				base.Location = new Point(x, y);
			}
			splitContainer1.Panel2MinSize = splitContainer1.Panel2.Width;
			imageBox.MouseDown += ImageBox_MouseDown;
			imageBox.MouseMove += ImageBox_MouseMove;
			imageBox.MouseUp += ImageBox_MouseUp;
			imageBox.OnToolChanged += imageBox_OnToolChanged;
			imageBox.OnFourLineChanged += ImageBox_OnFourLineChanged;
			imageBox.OnSpotChanged += imageBox_OnSpotChanged;
			setSystemStatus = SetSystemStatus;
			setCameraStatus = SetCameraStatus;
			setForce = SetForce;
			getForce = GetForce;
			setZoomTime = SetZoomTime;
			getZoomTime = GetZoomTime;
			setMicrometer = SetMicrometer;
			getMicrometer = GetMicrometer;
			setLoadTime = SetLoadTime;
			getLoadTime = GetLoadTime;
			setLightness = SetLightness;
			getLightness = GetLightness;
			setSYJKXPos = SetSYJKXPos;
			setSYJKYPos = SetSYJKYPos;
			setSelectedRecordRow = SetSelectedRecordRow;
			getSelectedRecordRow = GetSelectedRecordRow;
			fourLineChanged = FourLineChanged;
			setHardnessTesterComponent = SetHardnessTesterComponent;
			setPlatformComponent = SetPlatformComponent;
			setZAxisComponent = SetZAxisComponent;
			responseWhenTaskStart = ResponseWhenTaskStart;
			responseWhenTaskPause = ResponseWhenTaskPause;
			responseWhenTaskAborted = ResponseWhenTaskAborted;
			responseWhenTaskFinish = ResponseWhenTaskFinish;
			setDrawToolComponent = SetDrawToolComponent;
			genericInfo = ConfigFileManager.GenericInfo;
			portsInfo = ConfigFileManager.LoadSerialPortConfigFile();
			syjkPlatformInfo = ConfigFileManager.LoadSYJKPlatformConfigFile();
			zAxisInfo = ConfigFileManager.LoadZAxisConfigFile();
			cameraInfo = ConfigFileManager.LoadCameraConfigFile();
			sampleInfo = ConfigFileManager.LoadSampleInfoConfigFile();
			savedData = ConfigFileManager.LoadSavedDataConfigFile();
			autoMeasureInfo = ConfigFileManager.LoadAutoMeasureConfigFile();
			if (genericInfo.SoftwareSeries == SoftwareSeries.HV || genericInfo.SoftwareSeries == SoftwareSeries.HK)
			{
				hardnessTesterInfo = ConfigFileManager.LoadHVHardnessTesterConfigFile();
			}
			else
			{
				hardnessTesterInfo = ConfigFileManager.LoadHBWHardnessTesterConfigFile();
			}
			if (genericInfo.SoftwareSeries == SoftwareSeries.HK)
			{
				calibrationList = ConfigFileManager.LoadCalibrationConfigFile("Config\\HKCalibration_Config.xml");
			}
			else
			{
				calibrationList = ConfigFileManager.LoadCalibrationConfigFile("Config\\Calibration_Config.xml");
			}
			HVMeasure.MaxGray = autoMeasureInfo.MaxGray;
			HBMeasure.MaxGray = autoMeasureInfo.MaxGray;
			HVMeasure.MeadianRadius = autoMeasureInfo.MeadianRadius;
			HBMeasure.MeadianRadius = autoMeasureInfo.MeadianRadius;
			if (!HardnessConverter.LoadHardnessTable(CommonData.HardnessConvertTableFilepath))
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_HardnessConvertTableFileIsCorrupted);
			}
			cbForce.Items.Clear();
			foreach (ScaleSet scaleSet in hardnessTesterInfo.ScaleList)
			{
				if (scaleSet.Enabled)
				{
					cbForce.Items.Add(scaleSet.ScaleName);
				}
			}
			cbForce.Text = savedData.Force;
			cbZoomTime.Text = savedData.ZoomTime;
			nudLoadTime.Value = savedData.LoadTime;
			nudLightness.Value = savedData.Light;
			cbConvertType.Text = savedData.ConvertType;
			cbHardnessLevel.Text = savedData.HardnessLevel;
			if (genericInfo.SoftwareSeries == SoftwareSeries.HV || genericInfo.SoftwareSeries == SoftwareSeries.HK)
			{
				tsmiGenericSetting.Visible = true;
				dgvhRecordDepth.Visible = true;
				lbHardnessType.Text = ((genericInfo.SoftwareSeries == SoftwareSeries.HV) ? "HV" : "HK");
				switch (genericInfo.SoftwareVersion)
				{
					case SoftwareVersion.SE:
						SetForceCorrectInterface(visible: false);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: false);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: false);
						SetTurretInterface(visible: false);
						SetMicrometerFunction(genericInfo.MicrometerOn);
						break;
					case SoftwareVersion.Basic_Weight:
						SetForceCorrectInterface(visible: false);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: false);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(genericInfo.TurretOn);
						SetMicrometerFunction(genericInfo.MicrometerOn);
						break;
					case SoftwareVersion.Basic_Sensor:
						SetForceCorrectInterface(visible: true);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: false);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(genericInfo.TurretOn);
						SetMicrometerFunction(genericInfo.MicrometerOn);
						break;
					case SoftwareVersion.Semi_Weight:
						SetForceCorrectInterface(visible: false);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: true);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(visible: true);
						SetMicrometerFunction(isMicrometerOn: false);
						break;
					case SoftwareVersion.Semi_Sensor:
						SetForceCorrectInterface(visible: true);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: true);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(visible: true);
						SetMicrometerFunction(isMicrometerOn: false);
						break;
					case SoftwareVersion.Full_Weight:
						SetForceCorrectInterface(visible: false);
						SetZAxisFunction(isZAxisOn: true);
						SetPlatformFunction(isPlatformOn: true);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(visible: true);
						SetMicrometerFunction(isMicrometerOn: false);
						break;
					case SoftwareVersion.Full_Sensor:
						SetForceCorrectInterface(visible: true);
						SetZAxisFunction(isZAxisOn: true);
						SetPlatformFunction(isPlatformOn: true);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(visible: true);
						SetMicrometerFunction(isMicrometerOn: false);
						break;
				}
			}
			else
			{
				tsmiGenericSetting.Visible = false;
				dgvhRecordDepth.Visible = false;
				lbHardnessType.Text = "HBW";
				switch (genericInfo.SoftwareVersion)
				{
					case SoftwareVersion.SE:
						SetForceCorrectInterface(visible: false);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: false);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: false);
						SetTurretInterface(visible: false);
						SetMicrometerFunction(isMicrometerOn: false);
						break;
					case SoftwareVersion.Basic_Sensor:
						SetForceCorrectInterface(visible: true);
						SetZAxisFunction(isZAxisOn: false);
						SetPlatformFunction(isPlatformOn: false);
						SetHardnessTesterFunction(isHardnessTesterFunctionOn: true);
						SetTurretInterface(genericInfo.TurretOn);
						SetMicrometerFunction(isMicrometerOn: false);
						btnTurretCenter.Visible = false;
						lbLightness.Visible = false;
						nudLightness.Visible = false;
						break;
				}
			}
			singlePointRecordList = new BindingList<MeasureRecord>();
			dgvRecords.DataSource = singlePointRecordList;
			measureRecordFlowLayout.SetRecordSource(singlePointRecordList);
			taskManager = new TaskManager();
			taskManager.OnTaskStarted += taskManager_OnTaskStarted;
			taskManager.OnTaskPaused += taskManager_OnTaskPaused;
			taskManager.OnTaskAborted += taskManager_OnTaskAborted;
			taskManager.OnTaskFinished += taskManager_OnTaskFinished;
			taskManager.OnSingleTaskDone += taskManager_OnSingleTaskDone;
			taskManager.OnTaskFailed += taskManager_OnTaskFailed;
			try
			{
				cameraService.Open(Program.CertifiedCameraIndex);
				cameraService.AnalogGain = cameraInfo.AnalogGain;
				cameraService.ExposureTime = cameraInfo.ExposureTime;
				cameraService.ImageGrabbed += CameraService_ImageGrabbed;
				if (cameraInfo.ShowCameraState)
				{
					statusCamera.Visible = true;
					if (cameraInfo.ShowFrameRate)
					{
						System.Timers.Timer showFrameTimer = new System.Timers.Timer(1000.0);
						showFrameTimer.Elapsed += ShowFrameTimer_Elapsed;
						showFrameTimer.Enabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToLoadCamera);
				Logger.Error(ex, "Camera failed to turn on！");
				CameraStatus = ResourcesManager.Resources.R_Main_StatusCameraNotOpen;
			}
			Point p1 = Point.Empty;
			Point p2 = Point.Empty;
			Point p3 = Point.Empty;
			Point p4 = Point.Empty;
			HVMeasure.Measure(ref p1, ref p2, ref p3, ref p4, Application.StartupPath + "\\Benchmark.bmp");
			if (Directory.Exists(CommonData.MeasuredImageDirectoryPath))
			{
				Directory.Delete(CommonData.MeasuredImageDirectoryPath, recursive: true);
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (syjkPlatform != null)
				{
					syjkPlatform.XYLoosen();
				}
			}
			catch
			{
			}
			try
			{
				if (zAxis != null)
				{
					zAxis.Loosen();
				}
			}
			catch
			{
			}
			try
			{
				if (cameraService != null)
				{
					cameraService.Close();
				}
			}
			catch
			{
			}
			try
			{
				savedData.Force = cbForce.Text;
				savedData.ZoomTime = cbZoomTime.Text;
				savedData.ConvertType = cbConvertType.Text;
				savedData.HardnessLevel = cbHardnessLevel.Text;
				savedData.Light = decimal.ToInt32(nudLightness.Value);
				savedData.LoadTime = decimal.ToInt32(nudLoadTime.Value);
				ConfigFileManager.SaveSavedDataConfigFile(savedData);
			}
			catch
			{
			}
			try
			{
				taskManager.Abort();
			}
			catch
			{
			}
		}

		private void ImageBox_MouseDown(object sender, MouseEventArgs e)
		{
		}

		private void ImageBox_MouseMove(object sender, MouseEventArgs e)
		{
			lbXPos.Text = e.Location.X.ToString();
			lbYPos.Text = e.Location.Y.ToString();
		}

		private void ImageBox_MouseUp(object sender, MouseEventArgs e)
		{
			if (string.IsNullOrEmpty(CommonData.CurrentMeasuredImageFilepath) || !(currentMeasureRecord.MeasuredImagePath == CommonData.CurrentMeasuredImageFilepath) || File.Exists(CommonData.CurrentMeasuredImageFilepath))
			{
				return;
			}
			foreach (GraphicsObject graphics in imageBox.GraphicsList["MEASURE_GRAPHICS"])
			{
				if (graphics.GetType() == typeof(GraphicsFourLine))
				{
					imageBox.SaveImage(CommonData.CurrentMeasuredImageFilepath);
					measureRecordFlowLayout.UpdateRecord(currentMeasureRecord);
					break;
				}
			}
		}

		private void tsmiOpen_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Static image|*.bmp;*.jpeg;*.jpg;*.png";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					OpenImage(openFileDialog.FileName);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Fail to open the file！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToOpenImage);
			}
		}

		private void tsmiSaveImage_Click(object sender, EventArgs e)
		{
			if (imageBox.Image != null)
			{
				SetCamera(toStart: false);
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "Static image|*.bmp;";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					imageBox.SaveImage(saveFileDialog.FileName);
					Process.Start(saveFileDialog.FileName);
				}
			}
		}

		private void tsmiSaveOriginalImage_Click(object sender, EventArgs e)
		{
			SetCamera(toStart: false);
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Static image|*.bmp;";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				imageBox.SaveOriginalImage(saveFileDialog.FileName);
				Process.Start(saveFileDialog.FileName);
			}
		}

		private void tsmiExit_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void tsmiCameraStart_Click(object sender, EventArgs e)
		{
            try
            {
                cameraService.Open(Program.CertifiedCameraIndex);
                cameraService.AnalogGain = cameraInfo.AnalogGain;
                cameraService.ExposureTime = cameraInfo.ExposureTime;
                cameraService.ImageGrabbed += CameraService_ImageGrabbed;
                if (cameraInfo.ShowCameraState)
                {
                    statusCamera.Visible = true;
                    if (cameraInfo.ShowFrameRate)
                    {
                        System.Timers.Timer showFrameTimer = new System.Timers.Timer(1000.0);
                        showFrameTimer.Elapsed += ShowFrameTimer_Elapsed;
                        showFrameTimer.Enabled = true;
                    }
                }
                SetCamera(toStart: true);
            }
            catch (Exception ex)
            {
                MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToLoadCamera);
                Logger.Error(ex, "Camera failed to turn on！");
                CameraStatus = ResourcesManager.Resources.R_Main_StatusCameraNotOpen;
            }
        }

		private void tsmiCameraStop_Click(object sender, EventArgs e)
		{
			SetCamera(toStart: false);
		}

		private void tsmiCalibration_Click(object sender, EventArgs e)
		{
			try
			{
				string calibrationFilePath = ((genericInfo.SoftwareSeries == SoftwareSeries.HK) ? "Config\\HKCalibration_Config.xml" : "Config\\Calibration_Config.xml");
				CalibrationForm calibrationForm = new CalibrationForm(this, calibrationList, calibrationFilePath);
				string[] zoomTimeArray = new string[cbZoomTime.Items.Count];
				for (int i = 0; i < cbZoomTime.Items.Count; i++)
				{
					zoomTimeArray[i] = cbZoomTime.Items[i].ToString();
				}
				calibrationForm.SetZoomTimeBox(zoomTimeArray);
				string[] forceArray = new string[cbForce.Items.Count];
				for (int i = 0; i < cbForce.Items.Count; i++)
				{
					forceArray[i] = cbForce.Items[i].ToString();
				}
				calibrationForm.SetForceBox(forceArray);
				string[] hardnessLevelArray = new string[cbHardnessLevel.Items.Count];
				for (int i = 0; i < cbHardnessLevel.Items.Count; i++)
				{
					hardnessLevelArray[i] = cbHardnessLevel.Items[i].ToString();
				}
				calibrationForm.SetHardnessLevelBox(hardnessLevelArray);
				calibrationForm.SetCurrentCalibrationOption(cbZoomTime.Text, cbForce.Text, cbHardnessLevel.Text);
				AutoMeasure();
				calibrationForm.Show();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Do not open the calibration page repeatedly！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_CanNotOpenCalibrationForm);
			}
		}

		private void tsmiSampleInfo_Click(object sender, EventArgs e)
		{
			SampleInfoForm sampleForm = new SampleInfoForm(sampleInfo);
			if (sampleForm.ShowDialog() == DialogResult.OK)
			{
				sampleInfo = sampleForm.SampleInfo;
				ConfigFileManager.SaveSampleConfigFile(sampleInfo);
				imageBox.Invalidate();
			}
		}

		private void tsmiForceCorrect_Click(object sender, EventArgs e)
		{
			ForceCorrectForm form = new ForceCorrectForm(this);
			form.Show();
		}

		internal void tsmiAutoMeasure_Click(object sender, EventArgs e)
		{
			try
			{
				UpdateCalibrationInfo();
			}
			catch
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToFetchCalibrationInfo);
			}
			imageBox.ActiveTool = DrawToolType.Pointer;
			AutoMeasure();
		}

		private void tsmiPointer_Click(object sender, EventArgs e)
		{
			imageBox.ActiveTool = DrawToolType.Pointer;
		}

		private void tsmiManualMeasure_Click(object sender, EventArgs e)
		{
			SetCamera(toStart: false);
			imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
			imageBox.ActiveTool = DrawToolType.FourLine;
			imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
		}

		private void tsmiMeasureLength_Click(object sender, EventArgs e)
		{
			imageBox.ActiveTool = DrawToolType.RangeFinder;
			imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
		}

		private void tsmiMeasureAngle_Click(object sender, EventArgs e)
		{
			imageBox.ActiveTool = DrawToolType.Protractor;
			imageBox.ActiveToolIdentifier = "MEASURE_GRAPHICS";
		}

		private void tsmiMagnifier_Click(object sender, EventArgs e)
		{
			imageBox.ActiveTool = DrawToolType.Magnifier;
		}

		private void tsmiResumeImage_Click(object sender, EventArgs e)
		{
			imageBox.ZoomOut();
		}

		private void tsmiClearGraphics_Click(object sender, EventArgs e)
		{
			imageBox.ClearGraphics();
		}

		private void tsmiTrimMeasure_Click(object sender, EventArgs e)
		{
			if (camera.Collecting)
			{
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_CanNotTrimMeasureLine);
				return;
			}
			TrimMeasureForm trimMeasureForm = new TrimMeasureForm(this);
			trimMeasureForm.Show();
		}

		private void tsmiCenterCrossLine_Click(object sender, EventArgs e)
		{
			imageBox.ShowCenterCrossLine = !imageBox.ShowCenterCrossLine;
			imageBox.Invalidate();
		}

		private void tsmiAutoMeasureSetting_Click(object sender, EventArgs e)
		{
			AutoMeasureSettingForm form = new AutoMeasureSettingForm(this);
			form.Show();
		}

		private void tsmiCameraSetting_Click(object sender, EventArgs e)
		{
			CameraSettingForm settingForm = new CameraSettingForm(this);
			if (settingForm.ShowDialog() == DialogResult.OK)
			{
				ConfigFileManager.SaveCameraConfigFile(cameraInfo);
			}
		}

		private void tsmiSerialPortSetting_Click(object sender, EventArgs e)
		{
			SerialPortSettingForm settingForm = new SerialPortSettingForm(this);
			settingForm.ShowDialog();
		}

		private void tsmiXYPlatformSetting_Click(object sender, EventArgs e)
		{
			PlatformSettingForm settingForm = new PlatformSettingForm(syjkPlatformInfo);
			settingForm.ShowDialog();
			if (syjkPlatform != null)
			{
				syjkPlatform.SetMoveSpeed(syjkPlatformInfo.CurrentRate);
			}
		}

		private void tsmiZAxisSetting_Click(object sender, EventArgs e)
		{
			ZAxisSettingForm settingForm = new ZAxisSettingForm(zAxisInfo);
			settingForm.ShowDialog();
		}

		private void tsmiGenericSetting_Click(object sender, EventArgs e)
		{
			if (CalibrationForm.IsCalibrationFormOpened)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseCloseCalibrationWindow);
				return;
			}
			GenericSettingForm settingForm = new GenericSettingForm(this);
			settingForm.ShowDialog();
			try
			{
				UpdateCalibrationInfo();
			}
			catch
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_FailedToFetchCalibrationInfo);
			}
		}

		private void tsmiOtherSetting_Click(object sender, EventArgs e)
		{
			OtherSettingForm settingForm = new OtherSettingForm();
			settingForm.ShowDialog();
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			AboutForm aboutForm = new AboutForm();
			aboutForm.ShowDialog();
		}

		private void cbForce_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbForce.SelectedIndex == -1)
			{
				return;
			}
			if (hardnessTester != null && cbForce.Focused && (genericInfo.SoftwareVersion == SoftwareVersion.Basic_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Semi_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Full_Sensor))
			{
				TaskUpdateForce updateForceTask = new TaskUpdateForce(this, hardnessTester.SetScale, cbForce.Text);
				taskManager.Enqueue(updateForceTask);
				taskManager.Run();
			}
			try
			{
				UpdateCalibrationInfo();
			}
			catch (KeyNotFoundException)
			{
			}
		}

		private void cbZoomTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbZoomTime.SelectedIndex == -1)
			{
				return;
			}
			try
			{
				UpdateCalibrationInfo();
			}
			catch (KeyNotFoundException)
			{
			}
			try
			{
				TurretInfo turretInfo = hardnessTesterInfo.TurretInfoList.FirstOrDefault((TurretInfo x) => x.Object == cbZoomTime.Text);
				if (turretInfo != null && turretInfo.XPixelLength > 0f && turretInfo.YPixelLength > 0f)
				{
					imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
					imageBox.UpdateMovingCalibrationInfo(turretInfo.XPixelLength, turretInfo.YPixelLength);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to update image calibration information！");
			}
		}

		private void cbHardnessLevel_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbHardnessLevel.SelectedIndex == -1)
			{
				return;
			}
			try
			{
				UpdateCalibrationInfo();
			}
			catch (KeyNotFoundException)
			{
			}
		}

		private void nudLoadTime_ValueChanged(object sender, EventArgs e)
		{
			if (hardnessTester != null && nudLoadTime.Focused)
			{
				TaskUpdateLoadTime updateLoadTime = new TaskUpdateLoadTime(this, hardnessTester.SetLoadTime, decimal.ToInt32(nudLoadTime.Value));
				taskManager.Enqueue(updateLoadTime);
				taskManager.Run();
			}
		}

		private void nudLightness_ValueChanged(object sender, EventArgs e)
		{
			if (hardnessTester != null && nudLightness.Focused)
			{
				TaskUpdateLightness updateLightness = new TaskUpdateLightness(this, hardnessTester.SetLightness, decimal.ToInt32(nudLightness.Value));
				taskManager.Enqueue(updateLightness);
				taskManager.Run();
			}
		}

		private void cbConvertType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbConvertType.SelectedIndex != -1)
			{
				imageBox.Invalidate();
			}
		}

		private void btnImpress_Click(object sender, EventArgs e)
		{
			string scaleName = cbForce.Text;
			if (string.IsNullOrEmpty(scaleName))
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectAForce);
				return;
			}
			int loadTime = decimal.ToInt32(nudLoadTime.Value);
			if (loadTime < 1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectLoadTime);
				return;
			}
			TurretInfo turretInfo = null;
			if (hardnessTesterInfo.TurretAfterImpress)
			{
				string objectForMeasure = hardnessTesterInfo.ObjectiveForMeasure;
				turretInfo = hardnessTesterInfo.TurretInfoList.FirstOrDefault((TurretInfo x) => x.Object == objectForMeasure);
				if (turretInfo == null)
				{
					MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectObjectiveForMeasuring);
					return;
				}
			}
			TaskImpress impressTask = new TaskImpress(this, hardnessTester.Impress, cbForce.Text, decimal.ToInt32(nudLoadTime.Value));
			taskManager.Enqueue(impressTask);
			if (hardnessTesterInfo.TurretAfterImpress)
			{
				TaskTurret turretTask = new TaskTurret(this, hardnessTester.Turret, turretInfo.TurretDirection);
				taskManager.Enqueue(turretTask);
				if (hardnessTesterInfo.MeasureAfterImpress)
				{
					TaskMeasure measureTask = new TaskMeasure(this, AutoMeasure);
					taskManager.Enqueue(measureTask);
				}
			}
			taskManager.Run();
		}

		private void btnTurretLeft_Click(object sender, EventArgs e)
		{
			if (hardnessTester != null)
			{
				SetCamera(toStart: true);
				TaskTurret turretTask = new TaskTurret(this, hardnessTester.Turret, TurretDirection.Left);
				taskManager.Enqueue(turretTask);
				taskManager.Run();
			}
		}

		private void btnTurretCenter_Click(object sender, EventArgs e)
		{
			if (hardnessTester != null)
			{
				SetCamera(toStart: true);
				TaskTurret turretTask = new TaskTurret(this, hardnessTester.Turret, TurretDirection.Front);
				taskManager.Enqueue(turretTask);
				taskManager.Run();
			}
		}

		private void btnTurretRight_Click(object sender, EventArgs e)
		{
			if (hardnessTester != null)
			{
				SetCamera(toStart: true);
				TaskTurret turretTask = new TaskTurret(this, hardnessTester.Turret, TurretDirection.Right);
				taskManager.Enqueue(turretTask);
				taskManager.Run();
			}
		}

		private void btnEditRecord_Click(object sender, EventArgs e)
		{
			try
			{
				if (dgvRecords.SelectedRows.Count == 0)
				{
					MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_NoSelectedLine);
					return;
				}
				BindingList<MeasureRecord> recordList = dgvRecords.DataSource as BindingList<MeasureRecord>;
				MeasureRecord record = recordList.FirstOrDefault((MeasureRecord x) => x.Index == (int)dgvRecords.SelectedRows[0].Cells[0].Value);
				EditRecordForm aboutForm = new EditRecordForm(this, record);
				if (aboutForm.ShowDialog() == DialogResult.OK)
				{
					measureRecordFlowLayout.UpdateRecord(record);
					dgvRecords.Invalidate();
					imageBox.Invalidate();
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Unable to edit measurement record");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_CanNotEditRecord);
			}
		}

		private void btnDeleteRecord_Click(object sender, EventArgs e)
		{
			int rowCount = dgvRecords.SelectedRows.Count;
			if (rowCount == 0)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_NoSelectedLine);
				return;
			}
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Main_Message_SureToDeleteThoseLine.Replace("{INFO}", rowCount.ToString()), ResourcesManager.Resources.R_Main_Delete);
			if (result == DialogResult.Cancel)
			{
				return;
			}
			BindingList<MeasureRecord> recordList = dgvRecords.DataSource as BindingList<MeasureRecord>;
			if (recordList == singlePointRecordList)
			{
				for (int i = dgvRecords.SelectedRows.Count - 1; i >= 0; i--)
				{
					int index2 = (int)dgvRecords.SelectedRows[i].Cells[0].Value;
					MeasureRecord record = recordList.FirstOrDefault((MeasureRecord x) => x.Index == index2);
					recordList.Remove(record);
					measureRecordFlowLayout.Remove(record);
				}
				RenumberSinglePointRecordList();
				measureRecordFlowLayout.RefreshRecord();
			}
			else
			{
				if (recordList != multiPointRecordList)
				{
					return;
				}
				for (int i = dgvRecords.SelectedRows.Count - 1; i >= 0; i--)
				{
					int index = (int)dgvRecords.SelectedRows[i].Cells[0].Value;
					MeasureRecord record = recordList.FirstOrDefault((MeasureRecord x) => x.Index == index);
					foreach (PatternSet patternSet in patternList)
					{
						RemoveGraphicsFromPatternSet(patternSet);
						if (!patternSet.Checked)
						{
							continue;
						}
						bool found = false;
						foreach (PointAndGraphicsPair pair in patternSet.PointAndGraphicsPairList)
						{
							if (pair.MeasureRecord == record)
							{
								patternSet.PointAndGraphicsPairList.Remove(pair);
								patternSet.PointCount--;
								found = true;
								break;
							}
						}
						if (patternSet.PointCount <= 0)
						{
							patternList.Remove(patternSet);
						}
						if (!found)
						{
							continue;
						}
						break;
					}
					if (patternList.Count <= 0)
					{
						dgvRecords.DataSource = singlePointRecordList;
						measureRecordFlowLayout.SetRecordSource(singlePointRecordList);
						dgvRecords.Columns[dgvhRecordMoveTo.Name].Visible = false;
						taskManager.Abort();
						btnPauseOrResumeMultiPointMode.Visible = false;
						btnFinishMultiPointMode.Visible = false;
					}
					else
					{
						measureRecordFlowLayout.ReloadContents();
					}
				}
				RenumberPatternList();
				RefreshMultipointRecordList();
				measureRecordFlowLayout.ReloadContents();
			}
		}

		private void btnClearRecord_Click(object sender, EventArgs e)
		{
			BindingList<MeasureRecord> recordList = dgvRecords.DataSource as BindingList<MeasureRecord>;
			if (recordList == singlePointRecordList)
			{
				if (recordList.Count != 0)
				{
					bool enableSound = true;
					DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Main_Message_SureToClear, ResourcesManager.Resources.R_Main_Clear, null, enableSound);
					if (result != DialogResult.Cancel)
					{
						recordList.Clear();
						measureRecordFlowLayout.Clear();
					}
				}
			}
			else
			{
				if (recordList != multiPointRecordList || recordList.Count == 0)
				{
					return;
				}
				bool enableSound = true;
				DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Main_Message_SureToClear, ResourcesManager.Resources.R_Main_Clear, null, enableSound);
				if (result == DialogResult.Cancel)
				{
					return;
				}
				for (int i = patternList.Count - 1; i >= 0; i--)
				{
					if (patternList[i].Checked)
					{
						RemoveGraphicsFromPatternSet(patternList[i]);
						patternList.Remove(patternList[i]);
					}
				}
				RenumberPatternList();
				RefreshMultipointRecordList();
				if (patternList.Count <= 0)
				{
					dgvRecords.DataSource = singlePointRecordList;
					measureRecordFlowLayout.SetRecordSource(singlePointRecordList);
					dgvRecords.Columns[dgvhRecordMoveTo.Name].Visible = false;
					taskManager.Abort();
					btnPauseOrResumeMultiPointMode.Visible = false;
					btnFinishMultiPointMode.Visible = false;
				}
				else
				{
					measureRecordFlowLayout.ReloadContents();
				}
			}
		}

		private void btnStatistics_Click(object sender, EventArgs e)
		{
			if (!(dgvRecords.DataSource is BindingList<MeasureRecord> recordList) || recordList.Count == 0)
			{
				return;
			}
			double hardnessH = sampleInfo.HardnessH;
			double hardnessL = sampleInfo.HardnessL;
			int number = recordList.Count;
			double max = 0.0;
			double min = 0.0;
			double avg = 0.0;
			double variance = 0.0;
			double stdDev = 0.0;
			if (number == 0)
			{
				return;
			}
			try
			{
				min = 9999.0;
				foreach (MeasureRecord singleRecord in recordList)
				{
					double recordHardness = singleRecord.Hardness;
					if (min > recordHardness)
					{
						min = recordHardness;
					}
					if (max < recordHardness)
					{
						max = recordHardness;
					}
					avg += recordHardness;
				}
				avg /= (double)number;
				foreach (MeasureRecord singleRecord in recordList)
				{
					double recordHardness = singleRecord.Hardness;
					variance += (recordHardness - avg) * (recordHardness - avg);
				}
				variance /= (double)number;
				stdDev = (float)Math.Sqrt(variance);
				double cp = Math.Abs(hardnessH - hardnessL) / 6.0 / stdDev;
				double cpk = cp * (1.0 - Math.Abs(avg - (hardnessH + hardnessL) / 2.0) / Math.Abs(hardnessH - hardnessL) * 2.0);
				lbNumberValue.Text = number.ToString();
				lbMaxValue.Text = (double.IsInfinity(max) ? "INF" : max.ToString("F2"));
				lbMinValue.Text = (double.IsInfinity(min) ? "INF" : min.ToString("F2"));
				lbAvgValue.Text = (double.IsInfinity(avg) ? "INF" : avg.ToString("F2"));
				lbVarianceValue.Text = (double.IsInfinity(variance) ? "INF" : variance.ToString("F2"));
				lbStdDevValue.Text = (double.IsInfinity(stdDev) ? "INF" : stdDev.ToString("F2"));
				lbCPValue.Text = (double.IsInfinity(cp) ? "INF" : cp.ToString("F2"));
				lbCPKValue.Text = (double.IsInfinity(cpk) ? "INF" : cpk.ToString("F2"));
				tcFuncArea.SelectedPage = tpStatistics;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error calculating statistics！");
			}
		}

		private void btnExportReport_Click(object sender, EventArgs e)
		{
			if (!(dgvRecords.DataSource is BindingList<MeasureRecord> recordList) || recordList.Count == 0)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_NoDataAvailable);
				return;
			}
			btnStatistics_Click(sender, e);
			try
			{
				ExportReportForm form = new ExportReportForm(this, dgvRecords.DataSource as BindingList<MeasureRecord>);
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, ex.Message);
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_ExportFailed);
			}
		}

		private void measureRecordFlowLayout_OnRecordButtonClick(object sender, EventArgs e)
		{
			if (!(sender is MeasureRecord record))
			{
				return;
			}
			BindingList<MeasureRecord> recordList = dgvRecords.DataSource as BindingList<MeasureRecord>;
			if (recordList == multiPointRecordList)
			{
				if (syjkPlatform != null)
				{
					MovePlatformTo(record);
				}
			}
			else
			{
				OpenImage(record.OriginalImagePath);
			}
		}

		private void btnPLCForward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveYTo(-25f);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCRightForward_MouseDown(object sender, MouseEventArgs e)
		{
			syjkPlatform.NonBlockingMoveTo(new Point(25, -25));
		}

		private void btnPLCRightward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveXTo(25f);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCRightBackward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveTo(new Point(25, 25));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCBackward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveYTo(25f);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCLeftBackward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveTo(new Point(-25, 25));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCLeftward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveXTo(-25f);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCLeftForward_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.NonBlockingMoveTo(new Point(-25, -25));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void PLC_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				syjkPlatform.Stop();
				syjkPlatform.UpdateCurrentLocation();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error panning！");
			}
		}

		private void btnPLCCenter_Click(object sender, EventArgs e)
		{
			try
			{
				rbVeryFast.Checked = true;
				syjkPlatform.MoveToCenter();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Return to origin failed！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToLocateCenter);
			}
		}

		private void btnLockMotor_Click(object sender, EventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.XYLock();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Motor enable failed！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToLockMotor);
			}
		}

		private void btnUnlockMotor_Click(object sender, EventArgs e)
		{
			try
			{
				SetCamera(toStart: true);
				syjkPlatform.XYLoosen();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Motor unlock failed！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToUnlockMotor);
			}
		}

		private void btnXYLocalCenter_Click(object sender, EventArgs e)
		{
			rbVeryFast.Checked = true;
			TaskLocateCenter locateTask = new TaskLocateCenter(this, LocateCenter);
			taskManager.Enqueue(locateTask);
			taskManager.Run();
		}

		private void syjkSpeedRate_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (rbSlow.Checked)
				{
					syjkPlatform.SetMoveSpeed(SpeedRate.Slow);
				}
				else if (rbMedium.Checked)
				{
					syjkPlatform.SetMoveSpeed(SpeedRate.Medium);
				}
				else if (rbFast.Checked)
				{
					syjkPlatform.SetMoveSpeed(SpeedRate.Fast);
				}
				else if (rbVeryFast.Checked)
				{
					syjkPlatform.SetMoveSpeed(SpeedRate.VeryFast);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to set rate！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToSetPlatformSpeed);
			}
		}

		private void btnZAxisUnlock_Click(object sender, EventArgs e)
		{
			zAxis.Loosen();
		}

		private void btnZAxisLock_Click(object sender, EventArgs e)
		{
			zAxis.Lock();
		}

		private void btnZAxisUpward_Click(object sender, EventArgs e)
		{
			SetCamera(toStart: true);
			btnZAxisUpward.Enabled = false;
			btnZAxisUpward.Refresh();
			btnZAxisDownward.Enabled = false;
			btnZAxisDownward.Refresh();
			zAxis.MoveUpward();
			Application.DoEvents();
			btnZAxisUpward.Enabled = true;
			Invalidate();
			btnZAxisDownward.Enabled = true;
			Invalidate();
		}

		private void btnZAxisDownward_Click(object sender, EventArgs e)
		{
			SetCamera(toStart: true);
			btnZAxisUpward.Enabled = false;
			btnZAxisUpward.Refresh();
			btnZAxisDownward.Enabled = false;
			btnZAxisDownward.Refresh();
			zAxis.MoveDownward();
			Application.DoEvents();
			btnZAxisUpward.Enabled = true;
			Invalidate();
			btnZAxisDownward.Enabled = true;
			Invalidate();
		}

		private void btnAutoFocus_Click(object sender, EventArgs e)
		{
			SetCamera(toStart: true);
			TaskAutoFocus focusTask = new TaskAutoFocus(this, AutoFocus);
			taskManager.Enqueue(focusTask);
			taskManager.Run("AUTO_FOCUS_TASK_NAME");
		}

		private void cbMultiPointsMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbMultiPointsMode.SelectedIndex != -1)
			{
				for (int i = 0; i < allPatternsControl.Length; i++)
				{
					allPatternsControl[i].SetGraphicsVisible(visible: false);
				}
				allPatternsControl[cbMultiPointsMode.SelectedIndex].BringToFront();
				allPatternsControl[cbMultiPointsMode.SelectedIndex].SetGraphicsVisible(visible: true);
			}
		}

		private void btnResetPattern_Click(object sender, EventArgs e)
		{
			allPatternsControl[cbMultiPointsMode.SelectedIndex].Reset();
		}

		private void btnAddPattern_Click(object sender, EventArgs e)
		{
			try
			{
				if (!cbMultiLines.Checked)
				{
					foreach (PatternSet set in patternList)
					{
						RemoveGraphicsFromPatternSet(set);
					}
					patternList.Clear();
				}
				allPatternsControl[cbMultiPointsMode.SelectedIndex].GeneratePointsAndDepth(out var pointList, out var depthList);
				if (pointList == null || pointList.Count == 0 || pointList.Count != depthList.Count)
				{
					MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_CanNotGeneratePointSet);
					return;
				}
				PatternSet patternSet = new PatternSet();
				patternSet.Index = patternList.Count + 1;
				patternSet.Identifier = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				patternSet.PatternName = cbMultiPointsMode.Text;
				patternSet.PointCount = pointList.Count;
				patternSet.Checked = true;
				patternSet.PointAndGraphicsPairList = new List<PointAndGraphicsPair>();
				for (int i = 0; i < pointList.Count; i++)
				{
					PointF point = pointList[i];
					float depth = depthList[i];
					PointAndGraphicsPair pair = new PointAndGraphicsPair();
					patternSet.PointAndGraphicsPairList.Add(pair);
					MeasureRecord record = new MeasureRecord();
					record.XPos = point.X;
					record.YPos = point.Y;
					record.Depth = depth;
					record.ButtonText = "GO";
					pair.MeasureRecord = record;
					PointF boxPoint = imageBox.ConvertPhysicalCoordinateToBoxPointF(point);
					GraphicsSpot graphicsSpot = new GraphicsSpot(boxPoint.X, boxPoint.Y, 3f);
					graphicsSpot.Color = Color.Red;
					graphicsSpot.Identifier = patternSet.Identifier;
					pair.Graphics = graphicsSpot;
				}
				patternList.Add(patternSet);
				RefreshMultipointRecordList();
				dgvRecords.DataSource = multiPointRecordList;
				measureRecordFlowLayout.SetRecordSource(multiPointRecordList);
				dgvRecords.Columns[dgvhRecordMoveTo.Name].Visible = true;
				dgvRecords.Invalidate();
			}
			catch (Exception ex)
			{
				MsgBox.ShowWarning(ex.Message);
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			string scaleName = cbForce.Text;
			if (string.IsNullOrEmpty(scaleName))
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectAForce);
				return;
			}
			int loadTime = decimal.ToInt32(nudLoadTime.Value);
			if (loadTime < 1)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectLoadTime);
				return;
			}
			string objectForMeasure = hardnessTesterInfo.ObjectiveForMeasure;
			TurretInfo turretInfo = hardnessTesterInfo.TurretInfoList.FirstOrDefault((TurretInfo x) => x.Object == objectForMeasure);
			if (turretInfo == null)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectObjectiveForMeasuring);
				return;
			}
			for (int i = 0; i < multiPointRecordList.Count; i++)
			{
				MeasureRecord record = multiPointRecordList[i];
				TaskMovePlatformByRecord moveTask = new TaskMovePlatformByRecord(this, MovePlatformTo, record);
				taskManager.Enqueue(moveTask);
				TaskImpress impressTask = new TaskImpress(this, hardnessTester.Impress, cbForce.Text, decimal.ToInt32(nudLoadTime.Value));
				taskManager.Enqueue(impressTask);
				if (rbImpressAndMeasure.Checked || i == multiPointRecordList.Count - 1)
				{
					TaskTurret turretTask = new TaskTurret(this, hardnessTester.Turret, turretInfo.TurretDirection);
					taskManager.Enqueue(turretTask);
				}
				if (rbImpressAndMeasure.Checked)
				{
					if (cbFocusAll.Checked || ((genericInfo.SoftwareVersion == SoftwareVersion.Full_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Full_Weight) && i == 0))
					{
						TaskAutoFocus focusTask = new TaskAutoFocus(this, AutoFocus);
						taskManager.Enqueue(focusTask);
					}
					TaskMeasure measureTask = new TaskMeasure(this, AutoMeasure);
					taskManager.Enqueue(measureTask);
				}
			}
			if (rbImpressThenMeasure.Checked)
			{
				for (int i = 0; i < multiPointRecordList.Count; i++)
				{
					MeasureRecord record = multiPointRecordList[i];
					TaskMovePlatformByRecord moveTask = new TaskMovePlatformByRecord(this, MovePlatformTo, record);
					taskManager.Enqueue(moveTask);
					if (cbFocusAll.Checked || ((genericInfo.SoftwareVersion == SoftwareVersion.Full_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Full_Weight) && i == 0))
					{
						TaskAutoFocus focusTask = new TaskAutoFocus(this, AutoFocus);
						taskManager.Enqueue(focusTask);
					}
					TaskMeasure measureTask = new TaskMeasure(this, AutoMeasure);
					taskManager.Enqueue(measureTask);
				}
			}
			taskManager.Run("MULTIPOINTS_MODE_TASK_NAME");
		}

		private void btnPauseOrResumeMultiPointMode_Click(object sender, EventArgs e)
		{
			btnPauseOrResumeMultiPointMode.Enabled = false;
			if (taskManager.IsRunning)
			{
				taskManager.Pause();
			}
			else
			{
				taskManager.Resume();
			}
		}

		private void btnFinishMultiPointMode_Click(object sender, EventArgs e)
		{
			bool enableSound = true;
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Main_Message_SureToBreakOff, ResourcesManager.Resources.R_Main_BreakOff, null, enableSound);
			if (result != DialogResult.Cancel)
			{
				multiPointRecordList.Clear();
				patternList.Clear();
				IPatternControl[] array = allPatternsControl;
				foreach (IPatternControl control in array)
				{
					control.Reset();
				}
				imageBox.GraphicsList.Clear();
				dgvRecords.DataSource = singlePointRecordList;
				measureRecordFlowLayout.SetRecordSource(singlePointRecordList);
				dgvRecords.Columns[dgvhRecordMoveTo.Name].Visible = false;
				taskManager.Abort();
				btnPauseOrResumeMultiPointMode.Visible = false;
				btnFinishMultiPointMode.Visible = false;
			}
		}

		private void btnDeletePatternSet_Click(object sender, EventArgs e)
		{
			if (dgvPatterns.SelectedRows.Count == 0)
			{
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_PleaseSelectLineFirst);
				return;
			}
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Main_Message_SureToDeletePatternSet, ResourcesManager.Resources.R_Main_Delete);
			if (result == DialogResult.Cancel)
			{
				return;
			}
			for (int i = dgvPatterns.SelectedRows.Count - 1; i >= 0; i--)
			{
				int index = (int)dgvPatterns.SelectedRows[i].Cells[0].Value;
				PatternSet patternSet = patternList.FirstOrDefault((PatternSet x) => x.Index == index);
				RemoveGraphicsFromPatternSet(patternSet);
				patternList.Remove(patternSet);
			}
			RenumberPatternList();
			RefreshMultipointRecordList();
			measureRecordFlowLayout.ReloadContents();
			if (dgvPatterns.Rows.Count == 0)
			{
				dgvRecords.DataSource = singlePointRecordList;
				measureRecordFlowLayout.SetRecordSource(singlePointRecordList);
				dgvRecords.Columns[dgvhRecordMoveTo.Name].Visible = false;
				taskManager.Abort();
				btnPauseOrResumeMultiPointMode.Visible = false;
				btnFinishMultiPointMode.Visible = false;
			}
		}

		private void btnClearPatternSet_Click(object sender, EventArgs e)
		{
			if (patternList.Count == 0)
			{
				return;
			}
			bool enableSound = true;
			DialogResult result = MsgBox.ShowOKCancel(ResourcesManager.Resources.R_Main_Message_SureToClear, ResourcesManager.Resources.R_Main_Delete, null, enableSound);
			if (result == DialogResult.Cancel)
			{
				return;
			}
			foreach (PatternSet patternSet in patternList)
			{
				RemoveGraphicsFromPatternSet(patternSet);
			}
			patternList.Clear();
			RefreshMultipointRecordList();
			dgvRecords.DataSource = singlePointRecordList;
			measureRecordFlowLayout.SetRecordSource(singlePointRecordList);
			dgvRecords.Columns[dgvhRecordMoveTo.Name].Visible = false;
			taskManager.Abort();
			btnPauseOrResumeMultiPointMode.Visible = false;
			btnFinishMultiPointMode.Visible = false;
		}

		private void dgvAllRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			if (rowIndex >= 0 && columnIndex >= 0 && dgvRecords.Rows[rowIndex].Cells[columnIndex].GetType() == typeof(DataGridViewButtonCell) && dgvRecords.DataSource is BindingList<MeasureRecord> list)
			{
				MovePlatformTo(list[rowIndex]);
			}
		}

		private void dgvRecords_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				if (dgvRecords.SelectedRows.Count > 0 && dgvRecords.DataSource == multiPointRecordList)
				{
					currentMeasureRecord = multiPointRecordList[(int)dgvRecords.SelectedRows[0].Cells[0].Value - 1];
				}
			}
			catch
			{
			}
		}

		private void dgvPatterns_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			if (rowIndex >= 0 && columnIndex >= 0 && dgvPatterns.Rows[rowIndex].Cells[columnIndex].GetType() == typeof(DataGridViewCheckBoxCell))
			{
				int index = (int)dgvPatterns.Rows[rowIndex].Cells[0].Value;
				PatternSet patternSet = patternList[index - 1];
				dgvPatterns.CommitEdit(DataGridViewDataErrorContexts.Commit);
				RefreshMultipointRecordList();
				measureRecordFlowLayout.ReloadContents();
			}
		}

		private void hardnessTester_OnScaleChanged(object sender, SerialDataReceivedEventArgs e)
		{
			ScaleSet scaleSet = sender as ScaleSet;
			Force = scaleSet.ScaleName;
		}

		private void hardnessTester_OnTurretChanged(object sender, SerialDataReceivedEventArgs e)
		{
			if (sender is TurretInfo turretInfo && turretInfo.Object.EndsWith("X"))
			{
				ZoomTime = turretInfo.Object;
			}
		}

		private void hardnessTester_OnLoadTimeChanged(object sender, SerialDataReceivedEventArgs e)
		{
			LoadTime = (int)sender;
		}

		private void hardnessTester_OnLightnessChanged(object sender, SerialDataReceivedEventArgs e)
		{
			Lightness = (int)sender;
		}

		private void CameraService_ImageGrabbed(Bitmap bitmap)
		{
			imageBox.Image = bitmap;
		}

		private void imageBox_OnToolChanged(object sender, EventArgs e)
		{
			try
			{
				EnableActiveTool = (DrawToolType)sender;
			}
			catch
			{
			}
		}

		private void imageBox_OnSpotChanged(object sender, PointGraphicsEventArgs e)
		{
			GraphicsSpot graphicsSpot = sender as GraphicsSpot;
			PatternSet patternSet = null;
			foreach (PatternSet set in patternList)
			{
				if (graphicsSpot.Identifier == set.Identifier)
				{
					patternSet = set;
					break;
				}
			}
			if (patternSet == null)
			{
				return;
			}
			foreach (PointAndGraphicsPair pair in patternSet.PointAndGraphicsPairList)
			{
				if (pair.Graphics == graphicsSpot)
				{
					MeasureRecord record = pair.MeasureRecord;
					pair.MeasureRecord.XPos = e.PhysicalCoordinate.X;
					pair.MeasureRecord.YPos = e.PhysicalCoordinate.Y;
					if (graphicsSpot.SelectedHandle >= 0)
					{
						SelectedRecordRow = record.Index;
					}
				}
			}
		}

		private void ImageBox_OnFourLineChanged(object sender, MeasuringGraphicsEventArgs e)
		{
			Invoke(fourLineChanged, sender, e);
		}

		private void ShowFrameTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (cameraService.Collecting)
			{
				CameraStatus = ResourcesManager.Resources.R_Main_StatusCameraOn + ", " + ResourcesManager.Resources.R_Main_StatusCameraFrame + ": " + cameraService.FrameRate.ToString("F2");
			}
		}

		private void taskManager_OnTaskStarted(object sender, EventArgs e)
		{
			string taskName = sender as string;
			Invoke(responseWhenTaskStart);
			if (taskName == "MULTIPOINTS_MODE_TASK_NAME")
			{
				Console.WriteLine("Multi-point automatic measurement task starts！");
			}
		}

		private void taskManager_OnTaskPaused(object sender, EventArgs e)
		{
			Invoke(responseWhenTaskPause);
		}

		private void taskManager_OnTaskAborted(object sender, EventArgs e)
		{
			Invoke(responseWhenTaskAborted);
		}

		private void taskManager_OnTaskFinished(object sender, EventArgs e)
		{
			string taskName = sender as string;
			if (taskName == "MULTIPOINTS_MODE_TASK_NAME")
			{
				bool enableSound = true;
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_FullyAutoMeasureTaskFinished, null, null, enableSound);
			}
			else if (taskName == "AUTO_FOCUS_TASK_NAME")
			{
				bool enableSound = true;
				MsgBox.ShowInfo(ResourcesManager.Resources.R_Main_Message_AutoFocusFinished, null, null, enableSound);
			}
			Invoke(responseWhenTaskFinish);
		}

		private void taskManager_OnSingleTaskDone(object sender, EventArgs e)
		{
		}

		private void taskManager_OnTaskFailed(object sender, Exception ex)
		{
			MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_TaskException);
			Logger.Error(ex, sender.GetType().Name);
		}

		private void micrometerSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string receivedData = micrometerSerialPort.ReadLine().Replace(" ", "").Replace("\r", "")
				.Replace("\n", "");
			Console.WriteLine("Micrometer Received: {0}", receivedData);
			float result = 0f;
			if (float.TryParse(receivedData, out result))
			{
				MicrometerValue = result;
			}
			Console.WriteLine("float.TryParse(receivedData, out result): {0}, result: {1}", float.TryParse(receivedData, out result), result);
			Console.WriteLine();
		}

		private void syjkPlatform_OnLocationChanged(PointF location)
		{
			imageBox.UpdatePhysicalLocation(syjkPlatform.Location);
			SYJKLocation = location;
		}

		private void platformRelatedControl_MouseEnter(object sender, EventArgs e)
		{
			if (!isPlatformLocateInformed && syjkPlatform != null)
			{
				DialogResult dialogResult = MsgBox.ShowYesNo(ResourcesManager.Resources.R_Main_Message_PlatformUninitialize, ResourcesManager.Resources.R_Main_LocateCenter);
				if (dialogResult == DialogResult.Yes)
				{
					btnXYLocalCenter_Click(null, null);
				}
				isPlatformLocateInformed = true;
			}
		}

		private void LoadLanguageResources()
		{
			tsmiFile.Text = ResourcesManager.Resources.R_Main_File;
			tsmiOpenImage.TextLine1 = ResourcesManager.Resources.R_Main_OpenImage;
			tsmiSaveImage.TextLine1 = ResourcesManager.Resources.R_Main_SaveImage;
			tsmiSaveOriginalImage.TextLine1 = ResourcesManager.Resources.R_Main_SaveOriginalImage;
			tsmiExit.TextLine1 = ResourcesManager.Resources.R_Main_Exit;
			tsmiDevice.Text = ResourcesManager.Resources.R_Main_Device;
			tsmiCameraStart.TextLine1 = ResourcesManager.Resources.R_Main_OpenCamera;
			tsmiCameraStop.TextLine1 = ResourcesManager.Resources.R_Main_CloseCamera;
			tsmiData.Text = ResourcesManager.Resources.R_Main_Data;
			tsmiSampleInfo.TextLine1 = ResourcesManager.Resources.R_Main_SampleInfo;
			tsmiForceCorrect.TextLine1 = ResourcesManager.Resources.R_Main_ForceCorrect;
			tsmiTools.Text = ResourcesManager.Resources.R_Main_Tools;
			tsmiAutoMeasure.TextLine1 = ResourcesManager.Resources.R_Main_AutoMeasure;
			tsmiManualMeasure.TextLine1 = ResourcesManager.Resources.R_Main_ManualMeasure;
			tsmiPointer.TextLine1 = ResourcesManager.Resources.R_Main_Pointer;
			tsmiMeasureLength.TextLine1 = ResourcesManager.Resources.R_Main_MeasureLength;
			tsmiMeasureAngle.TextLine1 = ResourcesManager.Resources.R_Main_MeasureAngle;
			tsmiMagnifier.TextLine1 = ResourcesManager.Resources.R_Main_Magnifier;
			tsmiResumeImage.TextLine1 = ResourcesManager.Resources.R_Main_ResumeImage;
			tsmiClearGraphics.TextLine1 = ResourcesManager.Resources.R_Main_ClearGraphics;
			tsmiTrimMeasure.TextLine1 = ResourcesManager.Resources.R_Main_TrimMeasure;
			tsmiCenterCrossLine.TextLine1 = ResourcesManager.Resources.R_Main_CenterCrossLine;
			tsmiConfiguration.Text = ResourcesManager.Resources.R_Main_Configuration;
			tsmiCalibration.TextLine1 = ResourcesManager.Resources.R_Main_Calibration;
			tsmiAutoMeasureSetting.TextLine1 = ResourcesManager.Resources.R_Main_AutoMeasureSetting;
			tsmiCameraSetting.TextLine1 = ResourcesManager.Resources.R_Main_CameraSetting;
			tsmiSerialPortSetting.TextLine1 = ResourcesManager.Resources.R_Main_SerialPortSetting;
			tsmiXYPlatformSetting.TextLine1 = ResourcesManager.Resources.R_Main_XYPlatformSetting;
			tsmiZAxisSetting.TextLine1 = ResourcesManager.Resources.R_Main_ZAxisSetting;
			tsmiGenericSetting.TextLine1 = ResourcesManager.Resources.R_Main_GenericSetting;
			tsmiOtherSetting.TextLine1 = ResourcesManager.Resources.R_Main_OtherSetting;
			tsmiHelp.Text = ResourcesManager.Resources.R_Main_Help;
			tsmiAbout.Text = ResourcesManager.Resources.R_Main_About;
			toolTip.SetToolTip(btnOpen, ResourcesManager.Resources.R_Main_OpenImage);
			toolTip.SetToolTip(btnSave, ResourcesManager.Resources.R_Main_SaveImage);
			toolTip.SetToolTip(btnCameraStart, ResourcesManager.Resources.R_Main_OpenCamera);
			toolTip.SetToolTip(btnCameraPause, ResourcesManager.Resources.R_Main_CloseCamera);
			toolTip.SetToolTip(btnAutoMeasure, ResourcesManager.Resources.R_Main_AutoMeasure);
			toolTip.SetToolTip(btnManualMeasure, ResourcesManager.Resources.R_Main_ManualMeasure);
			toolTip.SetToolTip(btnPointer, ResourcesManager.Resources.R_Main_Pointer);
			toolTip.SetToolTip(btnMeasureLength, ResourcesManager.Resources.R_Main_MeasureLength);
			toolTip.SetToolTip(btnMeasureAngle, ResourcesManager.Resources.R_Main_MeasureAngle);
			toolTip.SetToolTip(btnClearGraphics, ResourcesManager.Resources.R_Main_ClearGraphics);
			toolTip.SetToolTip(btnMagnifier, ResourcesManager.Resources.R_Main_Magnifier);
			toolTip.SetToolTip(btnResumeImage, ResourcesManager.Resources.R_Main_ResumeImage);
			toolTip.SetToolTip(btnCenterCrossLine, ResourcesManager.Resources.R_Main_CenterCrossLine);
			lbMicrometer.Text = ResourcesManager.Resources.R_Main_Micrometer;
			btnEditRecord.Text = ResourcesManager.Resources.R_Main_Edit;
			btnDeleteRecord.Text = ResourcesManager.Resources.R_Main_Delete;
			btnClearRecord.Text = ResourcesManager.Resources.R_Main_Clear;
			btnStatistics.Text = ResourcesManager.Resources.R_Main_Statistics;
			btnExportReport.Text = ResourcesManager.Resources.R_Main_Report;
			dgvhRecordMoveTo.HeaderText = ResourcesManager.Resources.R_Main_Move;
			dgvhRecordHardness.HeaderText = ResourcesManager.Resources.R_Main_Hardness;
			dgvhRecordHardnessType.HeaderText = ResourcesManager.Resources.R_Main_HardnessType;
			dgvhRecordQualified.HeaderText = ResourcesManager.Resources.R_Main_Qualified;
			dgvhRecordConvertType.HeaderText = ResourcesManager.Resources.R_Main_ConvertType;
			dgvhRecordConvertValue.HeaderText = ResourcesManager.Resources.R_Main_ConvertValue;
			dgvhRecordDepth.HeaderText = ResourcesManager.Resources.R_Main_Depth;
			dgvhRecordMeasureTime.HeaderText = ResourcesManager.Resources.R_Main_Time;
			tpMachineControl.Text = ResourcesManager.Resources.R_Main_MachineControl;
			lbForce.Text = ResourcesManager.Resources.R_Main_Force;
			lbObjective.Text = ResourcesManager.Resources.R_Main_Objective;
			lbHardnessLevel.Text = ResourcesManager.Resources.R_Main_HardnessLevel;
			lbLightness.Text = ResourcesManager.Resources.R_Main_Lightness;
			lbLoadTime.Text = ResourcesManager.Resources.R_Main_LoadTime;
			lbTurret.Text = ResourcesManager.Resources.R_Main_Turret;
			btnImpress.Text = ResourcesManager.Resources.R_Main_Impress;
			tpStatistics.Text = ResourcesManager.Resources.R_Main_StatisticsInfo;
			lbNumber.Text = ResourcesManager.Resources.R_Main_Number;
			lbMin.Text = ResourcesManager.Resources.R_Main_Min;
			lbMax.Text = ResourcesManager.Resources.R_Main_Max;
			lbAvg.Text = ResourcesManager.Resources.R_Main_Average;
			lbVariance.Text = ResourcesManager.Resources.R_Main_Variance;
			lbStdDev.Text = ResourcesManager.Resources.R_Main_StdDev;
			tpAlbum.Text = ResourcesManager.Resources.R_Main_Album;
			tpXYZ.Text = ResourcesManager.Resources.R_Main_XYZPlatFormControl;
			rbSlow.Text = ResourcesManager.Resources.R_Main_Slow;
			rbMedium.Text = ResourcesManager.Resources.R_Main_Medium;
			rbFast.Text = ResourcesManager.Resources.R_Main_Fast;
			rbVeryFast.Text = ResourcesManager.Resources.R_Main_VeryFast;
			btnLockMotor.Text = ResourcesManager.Resources.R_Main_LockMotor;
			btnUnlockMotor.Text = ResourcesManager.Resources.R_Main_UnlockMotor;
			btnXYLocalCenter.Text = ResourcesManager.Resources.R_Main_LocateCenter;
			btnAutoFocus.Text = ResourcesManager.Resources.R_Main_AutoFocus;
			btnZAxisLock.Text = ResourcesManager.Resources.R_Main_LockZAxis;
			btnZAxisUnlock.Text = ResourcesManager.Resources.R_Main_UnlockZAxis;
			tpMultiPoints.Text = ResourcesManager.Resources.R_Main_Multipoint;
			lbPattern.Text = ResourcesManager.Resources.R_Main_Pattern;
			btnStart.Text = ResourcesManager.Resources.R_Main_StartImpress;
			btnAddPattern.Text = ResourcesManager.Resources.R_Main_Generate;
			cbMultiLines.Text = ResourcesManager.Resources.R_Main_Multiset;
			btnPauseOrResumeMultiPointMode.Text = ResourcesManager.Resources.R_Main_Pause;
			btnFinishMultiPointMode.Text = ResourcesManager.Resources.R_Main_BreakOff;
			btnResetPattern.Text = ResourcesManager.Resources.R_Main_Reset;
			rbImpressOnly.Text = ResourcesManager.Resources.R_Main_ImpressOnly;
			rbImpressAndMeasure.Text = ResourcesManager.Resources.R_Main_ImpressAndMeasure;
			rbImpressThenMeasure.Text = ResourcesManager.Resources.R_Main_ImpressThenMeasure;
			cbFocusAll.Text = ResourcesManager.Resources.R_Main_FocusAll;
			tpPatternList.Text = ResourcesManager.Resources.R_Main_PatternList;
			dgvhPatternName.HeaderText = ResourcesManager.Resources.R_Main_PatternName;
			dgvhPatternPointsCount.HeaderText = ResourcesManager.Resources.R_Main_NumberOfPoints;
			dgvhPatternSelected.HeaderText = ResourcesManager.Resources.R_Main_Checked;
			btnDeletePatternSet.Text = ResourcesManager.Resources.R_Main_Delete;
			btnClearPatternSet.Text = ResourcesManager.Resources.R_Main_Clear;
			statusSystem.Text = ResourcesManager.Resources.R_Main_SystemStatus + ": " + ResourcesManager.Resources.R_Main_StatusNormal;
			statusCamera.Text = ResourcesManager.Resources.R_Main_CameraStatus + ": " + ResourcesManager.Resources.R_Main_StatusCameraNotOpen;
		}

		private void OpenImage(string filename)
		{
			try
			{
				SetCamera(toStart: false);
				imageBox.ActiveTool = DrawToolType.Pointer;
				imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
				imageBox.OpenImage(filename);
				CommonData.CurrentOriginalImageFilepath = filename;
				if (!Directory.Exists(CommonData.MeasuredImageDirectoryPath))
				{
					Directory.CreateDirectory(CommonData.MeasuredImageDirectoryPath);
				}
				CommonData.CurrentMeasuredImageFilepath = CommonData.MeasuredImageDirectoryPath + "\\Camera" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + ".bmp";
				if (dgvRecords.DataSource is BindingList<MeasureRecord> recordList && recordList == singlePointRecordList)
				{
					MeasureRecord record = recordList.FirstOrDefault((MeasureRecord x) => x.OriginalImagePath == CommonData.CurrentOriginalImageFilepath);
					if (record == null)
					{
						record = new MeasureRecord();
					}
					currentMeasureRecord = record;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to open image file！");
				throw new Exception("Failed to open image file！", ex);
			}
		}

		public void LocateCenter()
		{
			syjkPlatform.LocateCenter();
			imageBox.ClearGraphics();
			imageBox.InitPhysicalLocation(new Point(0, 0));
		}

		public void AutoFocus()
		{
			try
			{
				AutoFocus(60, 2);
			}
			catch
			{
				throw;
			}
		}

		public void AutoFocus(int startPulse, int endPulse, int direction = 1, int minPulse = -10000, int maxPulse = 10000, int currentPulse = 0, double oldRating = 0.0)
		{
			try
			{
				if (startPulse < 1 || startPulse < endPulse || currentPulse < minPulse || currentPulse > maxPulse)
				{
					return;
				}
				Bitmap bitmap = cameraService.GrabSingleImage();
				double value = 0.0;
				double deviation = 0.0;
				HalconAlgorithm.Brenner(bitmap, out value, out deviation);
				if (value < oldRating)
				{
					startPulse /= 2;
					direction *= -1;
					if (startPulse < endPulse)
					{
						zAxis.Move(startPulse * 2 * direction);
						return;
					}
				}
				zAxis.Move(startPulse * direction);
				currentPulse += startPulse * direction;
				Thread.Sleep(20);
				AutoFocus(startPulse, endPulse, direction, minPulse, maxPulse, currentPulse, value);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Auto focus error！");
				throw;
			}
		}

		public void AutoMeasure()
		{
			try
			{
				SetCamera(toStart: false);
				Thread.Sleep(50);
				if (!File.Exists(CommonData.CurrentOriginalImageFilepath))
				{
					imageBox.SaveOriginalImage(CommonData.CurrentOriginalImageFilepath);
				}
				imageBox.ZoomOut();
				imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
				if (genericInfo.SoftwareSeries == SoftwareSeries.HBW)
				{
					Point centre = Point.Empty;
					float radius = 0f;
					HBMeasure.Measure(ref centre, ref radius, CommonData.CurrentOriginalImageFilepath);
					Point imageTopLeft = new Point((int)((float)centre.X - radius), (int)((float)centre.Y - radius));
					Point imageBottomRight = new Point((int)((float)centre.X + radius), (int)((float)centre.Y + radius));
					PointF boxTopLeft = imageBox.ConvertImagePointFToBoxPointF(imageTopLeft);
					PointF boxBottomRight = imageBox.ConvertImagePointFToBoxPointF(imageBottomRight);
					GraphicsFourLine g = new GraphicsFourLine(boxTopLeft.X, boxBottomRight.X, boxTopLeft.Y, boxBottomRight.Y);
					g.Identifier = "MEASURE_GRAPHICS";
					imageBox.GraphicsList.Add(g);
				}
				else
				{
					Point[] imagePointArray = new Point[4]
					{
					new Point(0, 0),
					new Point(0, 0),
					new Point(0, 0),
					new Point(0, 0)
					};
					HVMeasure.Measure(ref imagePointArray[0], ref imagePointArray[1], ref imagePointArray[2], ref imagePointArray[3], CommonData.CurrentOriginalImageFilepath);
					PointF[] boxPointArray = new PointF[4];
					for (int i = 0; i < boxPointArray.Length; i++)
					{
						ref PointF reference = ref boxPointArray[i];
						reference = imageBox.ConvertImagePointFToBoxPointF(imagePointArray[i]);
					}
					GraphicsFourLine g = new GraphicsFourLine(boxPointArray[0].X, boxPointArray[1].X, boxPointArray[2].Y, boxPointArray[3].Y);
					g.Identifier = "MEASURE_GRAPHICS";
					imageBox.GraphicsList.Add(g);
				}
				imageBox.Invalidate();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Automatic measurement error！");
			}
		}

		internal void UpdateCalibrationInfo()
		{
			string zoomTime = cbZoomTime.Text;
			string force = cbForce.Text;
			string hardnessLevel = cbHardnessLevel.Text;
			if (!string.IsNullOrEmpty(zoomTime) && !string.IsNullOrEmpty(force) && !string.IsNullOrEmpty(hardnessLevel))
			{
				currentCalibrationInfo = calibrationList.FirstOrDefault((CalibrationInfo x) => x.ZoomTime == zoomTime && x.Force == force && x.HardnessLevel == hardnessLevel);
				if (currentCalibrationInfo == null)
				{
					imageBox.SetMeasureCalibrationInfo(-1f, -1f);
					throw new KeyNotFoundException("Calibration information not found！");
				}
				imageBox.SetMeasureCalibrationInfo(currentCalibrationInfo.XPixelLength, currentCalibrationInfo.YPixelLength);
			}
		}

		private void SetCamera(bool toStart)
		{
			try
			{
				bool isCameraCollecting = cameraService.Collecting;
				if (isCameraCollecting && !toStart)
				{
					if (cameraInfo.SKIP2InCollect)
					{
						cameraService.ResetResolution();
						imageBox.Image = cameraService.GrabSingleImage();
						cameraService.Stop();
					}
					else
					{
						cameraService.Stop();
					}
					CameraStatus = ResourcesManager.Resources.R_Main_StatusCameraOff;
					if (!Directory.Exists(CommonData.OriginalImageDirectoryPath))
					{
						Directory.CreateDirectory(CommonData.OriginalImageDirectoryPath);
					}
					CommonData.CurrentOriginalImageFilepath = CommonData.OriginalImageDirectoryPath + "\\Camera" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + ".bmp";
					if (!Directory.Exists(CommonData.MeasuredImageDirectoryPath))
					{
						Directory.CreateDirectory(CommonData.MeasuredImageDirectoryPath);
					}
					CommonData.CurrentMeasuredImageFilepath = CommonData.MeasuredImageDirectoryPath + "\\Camera" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + ".bmp";
					if (dgvRecords.DataSource is BindingList<MeasureRecord> recordList && recordList == singlePointRecordList)
					{
						MeasureRecord record = (currentMeasureRecord = new MeasureRecord());
					}
				}
				else if (!isCameraCollecting && toStart)
				{
					imageBox.GraphicsList.RemoveByIdentifier("MEASURE_GRAPHICS");
					if (imageBox.ActiveTool == DrawToolType.FourLine)
					{
						imageBox.ActiveTool = DrawToolType.Pointer;
					}
					if (cameraInfo.SKIP2InCollect)
					{
						cameraService.SetResolutionToSKIP2();
					}
					cameraService.Start();
					CameraStatus = ResourcesManager.Resources.R_Main_StatusCameraOn;
				}
				isCameraCollecting = cameraService.Collecting;
				btnCameraStart.Enabled = !isCameraCollecting;
				btnCameraPause.Enabled = isCameraCollecting;
				tsmiCameraStart.Enabled = !isCameraCollecting;
				tsmiCameraStop.Enabled = isCameraCollecting;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Change camera state error");
				CameraStatus = ResourcesManager.Resources.R_Main_StatusCameraError;
			}
		}

		private void RenumberSinglePointRecordList()
		{
			for (int i = 0; i < singlePointRecordList.Count; i++)
			{
				singlePointRecordList[i].Index = i + 1;
			}
		}

		public void RemoveGraphicsFromPatternSet(PatternSet patternSet)
		{
			foreach (PointAndGraphicsPair pair in patternSet.PointAndGraphicsPairList)
			{
				imageBox.GraphicsList.Remove(pair.Graphics);
			}
		}

		private void RefreshMultipointRecordList()
		{
			multiPointRecordList.Clear();
			int count = 1;
			foreach (PatternSet patternSet in patternList)
			{
				imageBox.GraphicsList.RemoveByIdentifier(patternSet.Identifier);
				foreach (PointAndGraphicsPair pair in patternSet.PointAndGraphicsPairList)
				{
					if (patternSet.Checked)
					{
						multiPointRecordList.Add(pair.MeasureRecord);
						pair.MeasureRecord.Index = count++;
					}
					pair.Graphics.Visible = patternSet.Checked;
					imageBox.GraphicsList.Add(pair.Graphics);
				}
			}
		}

		private void RenumberPatternList()
		{
			for (int i = 0; i < patternList.Count; i++)
			{
				patternList[i].Index = i + 1;
			}
		}

		public void MovePlatformTo(MeasureRecord record)
		{
			MovePlatformTo(record.XPos, record.YPos);
			SelectedRecordRow = record.Index;
		}

		public void MovePlatformTo(PointF location)
		{
			if (syjkPlatform != null)
			{
				SetCamera(toStart: true);
				syjkPlatform.MoveTo(location);
			}
		}

		public void MovePlatformTo(float x, float y)
		{
			MovePlatformTo(new PointF(x, y));
		}

		public void MovePlatform(float deltaX, float deltaY)
		{
			PointF currentLocation = syjkPlatform.Location;
			MovePlatformTo(currentLocation.X + deltaX, currentLocation.Y + deltaY);
		}

		private void LoadHardnessTester()
		{
			try
			{
				SerialPortInfo serialPortInfo = portsInfo.FirstOrDefault((SerialPortInfo x) => x.Identifier == "Main");
				hardnessTester = new HardnessTester(serialPortInfo, hardnessTesterInfo);
				hardnessTester.OnScaleChanged += hardnessTester_OnScaleChanged;
				hardnessTester.OnTurretChanged += hardnessTester_OnTurretChanged;
				hardnessTester.OnLoadTimeChanged += hardnessTester_OnLoadTimeChanged;
				hardnessTester.OnLightnessChanged += hardnessTester_OnLightnessChanged;
				hardnessTester.QueryMachineStatus();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to connect durometer！");
				bool enableSound = true;
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToConnectHardnessTester, null, null, enableSound);
				SystemStatus = ResourcesManager.Resources.R_Main_StatusFailedToLoadHardnessTester;
				hardnessTester = null;
				EnableHardnessTesterComponent = false;
			}
		}

		private void LoadPlatform()
		{
			try
			{
				SerialPortInfo serialPortInfo = portsInfo.FirstOrDefault((SerialPortInfo x) => x.Identifier == "X/Y");
				syjkPlatform = new SYJKPlatform(serialPortInfo, syjkPlatformInfo);
				syjkPlatform.OnLocationChanged += syjkPlatform_OnLocationChanged;
				syjkPlatform.UpdateCurrentLocation();
				imageBox.InitPhysicalLocation(syjkPlatform.Location);
				if (syjkPlatformInfo.CurrentRate == SpeedRate.Slow)
				{
					rbSlow.Checked = true;
				}
				else if (syjkPlatformInfo.CurrentRate == SpeedRate.Medium)
				{
					rbMedium.Checked = true;
				}
				else if (syjkPlatformInfo.CurrentRate == SpeedRate.Fast)
				{
					rbFast.Checked = true;
				}
				else if (syjkPlatformInfo.CurrentRate == SpeedRate.VeryFast)
				{
					rbVeryFast.Checked = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to initialize platform！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToConnectPlatform);
				SystemStatus = ResourcesManager.Resources.R_Main_StatusFailedToLoadPlatform;
				syjkPlatform = null;
				EnablePlatformComponent = false;
			}
		}

		private void LoadZXis()
		{
			try
			{
				SerialPortInfo serialPortInfo = portsInfo.FirstOrDefault((SerialPortInfo x) => x.Identifier == "Z");
				zAxis = new ZAxis(serialPortInfo, zAxisInfo);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to initialize Z axis！");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToConnectZAxis);
				SystemStatus = ResourcesManager.Resources.R_Main_StatusFailedToLoadZAxis;
				zAxis = null;
				EnableZAxisComponent = false;
			}
		}

		private void LoadMicrometer()
		{
			try
			{
				SerialPortInfo serialPortInfo = portsInfo.FirstOrDefault((SerialPortInfo x) => x.Identifier == "Micrometer");
				micrometerSerialPort = new Serial(serialPortInfo);
				micrometerSerialPort.DataReceived += micrometerSerialPort_DataReceived;
				micrometerSerialPort.Open();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to initialize micrometer！");
				bool enableSound = true;
				MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToConnectMicrometer, null, null, enableSound);
			}
		}

		private void SetHardnessTesterFunction(bool isHardnessTesterFunctionOn)
		{
			btnImpress.Visible = isHardnessTesterFunctionOn;
			lbLoadTime.Visible = isHardnessTesterFunctionOn;
			nudLoadTime.Visible = isHardnessTesterFunctionOn;
			lbLightness.Visible = isHardnessTesterFunctionOn;
			nudLightness.Visible = isHardnessTesterFunctionOn;
			tsmiSerialPortSetting.Visible = isHardnessTesterFunctionOn;
			if (isHardnessTesterFunctionOn)
			{
				LoadHardnessTester();
			}
		}

		private void SetPlatformFunction(bool isPlatformOn)
		{
			//tpXYZ.Parent = (isPlatformOn ? tcFuncArea : null);
			//tpMultiPoints.Parent = (isPlatformOn ? tcFuncArea : null);
			//tpPatternList.Parent = (isPlatformOn ? tcFuncArea : null);

            if (isPlatformOn == false)
            {
                tpXYZ.Parent = null;
                tpMultiPoints.Parent = null;
                tpPatternList.Parent = null;
            }


			dgvhRecordXPos.Visible = isPlatformOn;
			dgvhRecordYPos.Visible = isPlatformOn;
			tsmiXYPlatformSetting.Visible = isPlatformOn;
			btnplcforward2.Visible = isPlatformOn;
			btnplcleftward2.Visible = isPlatformOn;
			btnplcrightward2.Visible = isPlatformOn;
			btnplcbackward2.Visible = isPlatformOn;
			if (isPlatformOn)
			{
				patternHorizontal.Initialize(this);
				patternVertical.Initialize(this);
				patternSlash.Initialize(this);
				patternFree.Initialize(this);
				patternMatrix.Initialize(this);
				patternCircleExtension.Initialize(this);
				cbMultiPointsMode.Items.Clear();
				allPatternsControl = new IPatternControl[Enum.GetValues(typeof(PointsPattern)).Length];
				cbMultiPointsMode.Items.Add(ResourcesManager.Resources.R_Main_HorizontalMode);
				allPatternsControl[0] = patternHorizontal;
				cbMultiPointsMode.Items.Add(ResourcesManager.Resources.R_Main_VerticalMode);
				allPatternsControl[1] = patternVertical;
				cbMultiPointsMode.Items.Add(ResourcesManager.Resources.R_Main_SlashMode);
				allPatternsControl[2] = patternSlash;
				cbMultiPointsMode.Items.Add(ResourcesManager.Resources.R_Main_FreeMode);
				allPatternsControl[3] = patternFree;
				cbMultiPointsMode.Items.Add(ResourcesManager.Resources.R_Main_MatrixMode);
				allPatternsControl[4] = patternMatrix;
				cbMultiPointsMode.Items.Add(ResourcesManager.Resources.R_Main_CircleMode);
				allPatternsControl[5] = patternCircleExtension;
				cbMultiPointsMode.SelectedIndex = 0;
				multiPointRecordList = new BindingList<MeasureRecord>();
				patternList = new BindingList<PatternSet>();
				dgvPatterns.DataSource = patternList;
				LoadPlatform();
			}
		}

		private void SetZAxisFunction(bool isZAxisOn)
		{
			gbZAxis.Enabled = isZAxisOn;
			cbFocusAll.Visible = isZAxisOn;
			tsmiZAxisSetting.Visible = isZAxisOn;
			if (isZAxisOn)
			{
				LoadZXis();
			}
		}

		private void SetMicrometerFunction(bool isMicrometerOn)
		{
			lbMicrometer.Visible = isMicrometerOn;
			lbMicrometerValue.Visible = isMicrometerOn;
			if (isMicrometerOn)
			{
				LoadMicrometer();
			}
		}

		private void SetTurretInterface(bool visible)
		{
			lbTurret.Visible = visible;
			btnTurretLeft.Visible = visible;
			btnTurretCenter.Visible = visible;
			btnTurretRight.Visible = visible;
		}

		private void SetForceCorrectInterface(bool visible)
		{
			tsmiForceCorrect.Visible = visible;
			if (!visible)
			{
				hardnessTesterInfo.IsForceCoefficientOn = false;
			}
		}

		private void ResponseWhenTaskStart()
		{
			if (!taskManager.IsPaused)
			{
				btnPauseOrResumeMultiPointMode.Enabled = true;
				btnPauseOrResumeMultiPointMode.Text = ResourcesManager.Resources.R_Main_Pause;
				btnPauseOrResumeMultiPointMode.Visible = true;
				btnFinishMultiPointMode.Visible = true;
			}
			SetHardnessTesterComponent(available: false);
			SetPlatformComponent(available: false);
			SetZAxisComponent(available: false);
		}

		private void ResponseWhenTaskPause()
		{
			btnPauseOrResumeMultiPointMode.Enabled = true;
			btnPauseOrResumeMultiPointMode.Text = ResourcesManager.Resources.R_Main_Continue;
			SetHardnessTesterComponent(hardnessTester != null);
			SetPlatformComponent(syjkPlatform != null);
			SetZAxisComponent(zAxis != null);
		}

		private void ResponseWhenTaskAborted()
		{
			btnPauseOrResumeMultiPointMode.Visible = false;
			btnFinishMultiPointMode.Visible = false;
			SetHardnessTesterComponent(hardnessTester != null);
			SetPlatformComponent(syjkPlatform != null);
			SetZAxisComponent(zAxis != null);
		}

		private void ResponseWhenTaskFinish()
		{
			if (!taskManager.IsPaused)
			{
				btnPauseOrResumeMultiPointMode.Visible = false;
				btnFinishMultiPointMode.Visible = false;
			}
			SetHardnessTesterComponent(hardnessTester != null);
			SetPlatformComponent(syjkPlatform != null);
			SetZAxisComponent(zAxis != null);
		}

		private void SetDrawToolComponent(DrawToolType activeTool)
		{
			tsmiPointer.Enabled = activeTool != DrawToolType.Pointer;
			btnPointer.Enabled = activeTool != DrawToolType.Pointer;
			tsmiManualMeasure.Enabled = activeTool != DrawToolType.FourLine;
			btnManualMeasure.Enabled = activeTool != DrawToolType.FourLine;
			tsmiMeasureLength.Enabled = activeTool != DrawToolType.RangeFinder;
			btnMeasureLength.Enabled = activeTool != DrawToolType.RangeFinder;
			tsmiMeasureAngle.Enabled = activeTool != DrawToolType.Protractor;
			btnMeasureAngle.Enabled = activeTool != DrawToolType.Protractor;
			tsmiMagnifier.Enabled = activeTool != DrawToolType.Magnifier;
			btnMagnifier.Enabled = activeTool != DrawToolType.Magnifier;
		}

		public void SetHardnessTesterComponent(bool available)
		{
			if (hardnessTester != null)
			{
				cbForce.Enabled = available;
			}
			nudLoadTime.Enabled = available;
			nudLightness.Enabled = available;
			btnImpress.Enabled = available;
			btnTurretLeft.Enabled = available;
			btnTurretRight.Enabled = available;
			btnTurretCenter.Enabled = available;
		}

		public void SetPlatformComponent(bool available)
		{
			dgvRecords.Enabled = available || syjkPlatform == null;
			gbPlatform.Enabled = available;
			btnStart.Enabled = available;
			btnplcforward2.Enabled = available;
			btnplcbackward2.Enabled = available;
			btnplcleftward2.Enabled = available;
			btnplcrightward2.Enabled = available;
			measureRecordFlowLayout.EnabledGoButton(available);
		}

		public void SetZAxisComponent(bool available)
		{
			gbZAxis.Enabled = available;
		}

		private void FourLineChanged(object sender, MeasuringGraphicsEventArgs e)
		{
			double value1 = e.XPhysicalDistance;
			double value2 = e.YPhysicalDistance;
			string hardnessType = genericInfo.SoftwareSeries.ToString();
			string convertType = cbConvertType.Text;
			if (value1 <= 0.0 || (value2 <= 0.0 && (hardnessType == "HV" || hardnessType == "HBW")))
			{
				lbHardnessValue.ForeColor = Color.Red;
				lbHardnessValue.Text = "0";
				return;
			}
			if (!File.Exists(CommonData.CurrentOriginalImageFilepath))
			{
				imageBox.SaveOriginalImage(CommonData.CurrentOriginalImageFilepath);
			}
			double d1 = value1;
			double d2 = value2;
			double davg = (d1 + d2) / 2.0;
			float kgf = 0f;
			double hardness = 0.0;
			try
			{
				if (hardnessType == "HBW")
				{
					string forceAndDiameter = cbForce.Text.Substring(3);
					string[] tempArr = forceAndDiameter.Split('/');
					float diameter = float.Parse(tempArr[0]);
					kgf = float.Parse(tempArr[1]);
					hardness = HardnessFormula.CalculateHBWHardness(kgf, diameter, davg / 1000.0);
				}
				else
				{
					kgf = float.Parse(cbForce.Text.Replace("kgf", ""));
					if (hardnessType == "HV")
					{
						hardness = HardnessFormula.CalculateHVHardness(kgf, davg / 1000.0);
					}
					else if (hardnessType == "HK")
					{
						hardness = HardnessFormula.CalculateHKHardness(kgf, d1 / 1000.0);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error calculating hardness value！");
				lbHardnessValue.ForeColor = Color.Red;
				lbHardnessValue.Text = "0 ";
				return;
			}
			bool qualified = hardness <= sampleInfo.HardnessH && hardness >= sampleInfo.HardnessL;
			double convertValue = HardnessConverter.Convert(hardness, hardnessType, convertType);
			if (currentMeasureRecord == null)
			{
				currentMeasureRecord = new MeasureRecord();
			}
			currentMeasureRecord.OriginalImagePath = CommonData.CurrentOriginalImageFilepath;
			currentMeasureRecord.MeasuredImagePath = CommonData.CurrentMeasuredImageFilepath;
			currentMeasureRecord.MeasureTime = DateTime.Now;
			currentMeasureRecord.D1 = d1;
			currentMeasureRecord.D2 = d2;
			currentMeasureRecord.DAvg = davg;
			currentMeasureRecord.HardnessType = hardnessType;
			currentMeasureRecord.Hardness = hardness;
			currentMeasureRecord.Qualified = (qualified ? ResourcesManager.Resources.R_Main_QualifiedYes : ResourcesManager.Resources.R_Main_QualifiedNo);
			currentMeasureRecord.ConvertType = convertType;
			currentMeasureRecord.ConvertValue = convertValue;
			if ((genericInfo.SoftwareVersion == SoftwareVersion.SE || genericInfo.SoftwareVersion == SoftwareVersion.Basic_Sensor || genericInfo.SoftwareVersion == SoftwareVersion.Basic_Weight) && genericInfo.MicrometerOn)
			{
				currentMeasureRecord.Depth = MicrometerValue;
			}
			if (dgvRecords.DataSource is BindingList<MeasureRecord> recordList && recordList == singlePointRecordList && !recordList.Contains(currentMeasureRecord))
			{
				currentMeasureRecord.Index = recordList.Count + 1;
				recordList.Add(currentMeasureRecord);
			}
			if (sender is GraphicsFourLine graphics)
			{
				if (graphics.SelectedHandle < 0)
				{
					imageBox.SaveImage(CommonData.CurrentMeasuredImageFilepath);
					measureRecordFlowLayout.UpdateRecord(currentMeasureRecord);
				}
				else
				{
					File.Delete(CommonData.CurrentMeasuredImageFilepath);
				}
			}
			dgvRecords.Invalidate();
			lbHardnessType.Text = hardnessType;
			if (sampleInfo.HardnessH != 0.0 && (hardness < sampleInfo.HardnessL || hardness > sampleInfo.HardnessH))
			{
				lbHardnessValue.ForeColor = Color.Red;
			}
			else
			{
				lbHardnessValue.ForeColor = Color.Black;
			}
			if (hardness > 9999.0)
			{
				lbHardnessValue.Text = ">9999";
			}
			else
			{
				lbHardnessValue.Text = hardness.ToString("F1");
			}
			if (!string.IsNullOrEmpty(convertType))
			{
				lbConvertValue.Text = convertValue.ToString("F1");
			}
		}

		private void SetForce(string text)
		{
			if (!(cbForce.Text == text))
			{
				cbForce.Text = text;
			}
		}

		private string GetForce()
		{
			return cbForce.Text;
		}

		private void SetZoomTime(string text)
		{
			if (!(cbZoomTime.Text == text))
			{
				cbZoomTime.Text = text;
			}
		}

		private string GetZoomTime()
		{
			return cbZoomTime.Text;
		}

		private void SetMicrometer(float value)
		{
			lbMicrometerValue.Text = value.ToString();
		}

		private float GetMicrometer()
		{
			float.TryParse(lbMicrometerValue.Text, out var value);
			return value;
		}

		public void SetLoadTime(int loadTime)
		{
			if (decimal.ToInt32(nudLoadTime.Value) != loadTime)
			{
				nudLoadTime.Value = loadTime;
			}
		}

		private int GetLoadTime()
		{
			return decimal.ToInt32(nudLoadTime.Value);
		}

		public void SetLightness(int lightness)
		{
			if (decimal.ToInt32(nudLightness.Value) != lightness)
			{
				nudLightness.Value = lightness;
			}
		}

		private int GetLightness()
		{
			return decimal.ToInt32(nudLightness.Value);
		}

		private void SetSystemStatus(string value)
		{
			statusSystem.Text = ResourcesManager.Resources.R_Main_SystemStatus + ": " + value;
		}

		private void SetCameraStatus(string value)
		{
			statusCamera.Text = ResourcesManager.Resources.R_Main_CameraStatus + ": " + value;
		}

		private void SetSYJKXPos(string value)
		{
			lbSYJKXPos.Text = value;
		}

		private void SetSYJKYPos(string value)
		{
			lbSYJKYPos.Text = value;
		}

		private void SetSelectedRecordRow(int value)
		{
			dgvRecords.ClearSelection();
			if (value > 0 && value <= dgvRecords.RowCount)
			{
				dgvRecords.Rows[value - 1].Selected = true;
				dgvRecords.CurrentCell = dgvRecords.Rows[value - 1].Cells[0];
			}
			dgvRecords.Invalidate();
		}

		private int GetSelectedRecordRow()
		{
			try
			{
				return (int)dgvRecords.SelectedRows[0].Cells[0].Value;
			}
			catch
			{
				return -1;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusSystem = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusCamera = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCenterCrossLine = new Krypton.Toolkit.KryptonButton();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.btnAutoMeasure = new Krypton.Toolkit.KryptonButton();
            this.btnOpen = new Krypton.Toolkit.KryptonButton();
            this.btnResumeImage = new Krypton.Toolkit.KryptonButton();
            this.btnMagnifier = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnClearGraphics = new Krypton.Toolkit.KryptonButton();
            this.btnCameraStart = new Krypton.Toolkit.KryptonButton();
            this.btnMeasureAngle = new Krypton.Toolkit.KryptonButton();
            this.btnCameraPause = new Krypton.Toolkit.KryptonButton();
            this.btnMeasureLength = new Krypton.Toolkit.KryptonButton();
            this.btnPointer = new Krypton.Toolkit.KryptonButton();
            this.btnManualMeasure = new Krypton.Toolkit.KryptonButton();
            this.lbYPos = new Krypton.Toolkit.KryptonLabel();
            this.label6 = new Krypton.Toolkit.KryptonLabel();
            this.lbXPos = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnplcrightward2 = new Krypton.Toolkit.KryptonButton();
            this.btnplcbackward2 = new Krypton.Toolkit.KryptonButton();
            this.btnplcforward2 = new Krypton.Toolkit.KryptonButton();
            this.btnplcleftward2 = new Krypton.Toolkit.KryptonButton();
            this.imageBox = new Labtt.DrawArea.ImageBox();
            this.lbMicrometer = new Krypton.Toolkit.KryptonLabel();
            this.tcFuncArea = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.tpMachineControl = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.nudLoadTime = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.btnImpress = new Krypton.Toolkit.KryptonButton();
            this.nudLightness = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.lbLightness = new Krypton.Toolkit.KryptonLabel();
            this.cbForce = new Krypton.Toolkit.KryptonComboBox();
            this.btnTurretCenter = new Krypton.Toolkit.KryptonButton();
            this.lbForce = new Krypton.Toolkit.KryptonLabel();
            this.lbTurret = new Krypton.Toolkit.KryptonLabel();
            this.btnTurretRight = new Krypton.Toolkit.KryptonButton();
            this.lbLoadTime = new Krypton.Toolkit.KryptonLabel();
            this.cbHardnessLevel = new Krypton.Toolkit.KryptonComboBox();
            this.cbZoomTime = new Krypton.Toolkit.KryptonComboBox();
            this.btnTurretLeft = new Krypton.Toolkit.KryptonButton();
            this.lbObjective = new Krypton.Toolkit.KryptonLabel();
            this.lbHardnessLevel = new Krypton.Toolkit.KryptonLabel();
            this.tpXYZ = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.tableLayoutPanel2 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.gbZAxis = new Krypton.Toolkit.KryptonGroupBox();
            this.btnAutoFocus = new Krypton.Toolkit.KryptonButton();
            this.btnZAxisUnlock = new Krypton.Toolkit.KryptonButton();
            this.btnZAxisLock = new Krypton.Toolkit.KryptonButton();
            this.btnZAxisDownward = new Krypton.Toolkit.KryptonButton();
            this.btnZAxisUpward = new Krypton.Toolkit.KryptonButton();
            this.gbPlatform = new Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanel4 = new Krypton.Toolkit.KryptonTableLayoutPanel();
            this.lbSYJKYPos = new Krypton.Toolkit.KryptonLabel();
            this.label7 = new Krypton.Toolkit.KryptonLabel();
            this.btnLockMotor = new Krypton.Toolkit.KryptonButton();
            this.btnXYLocalCenter = new Krypton.Toolkit.KryptonButton();
            this.lbSYJKXPos = new Krypton.Toolkit.KryptonLabel();
            this.btnPLCLeftForward = new Krypton.Toolkit.KryptonButton();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.btnPLCRightward = new Krypton.Toolkit.KryptonButton();
            this.btnPLCCenter = new Krypton.Toolkit.KryptonButton();
            this.btnPLCRightBackward = new Krypton.Toolkit.KryptonButton();
            this.btnPLCLeftBackward = new Krypton.Toolkit.KryptonButton();
            this.btnUnlockMotor = new Krypton.Toolkit.KryptonButton();
            this.btnPLCBackward = new Krypton.Toolkit.KryptonButton();
            this.rbSlow = new Krypton.Toolkit.KryptonRadioButton();
            this.rbMedium = new Krypton.Toolkit.KryptonRadioButton();
            this.rbFast = new Krypton.Toolkit.KryptonRadioButton();
            this.btnPLCForward = new Krypton.Toolkit.KryptonButton();
            this.rbVeryFast = new Krypton.Toolkit.KryptonRadioButton();
            this.btnPLCRightForward = new Krypton.Toolkit.KryptonButton();
            this.btnPLCLeftward = new Krypton.Toolkit.KryptonButton();
            this.tpMultiPoints = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.patternCircleExtension = new AIO_Client.PatternCircleExtensionControl();
            this.patternMatrix = new AIO_Client.PatternMatrixControl();
            this.patternFree = new AIO_Client.PatternFreeControl();
            this.patternHorizontal = new AIO_Client.PatternHorizontalControl();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPauseOrResumeMultiPointMode = new Krypton.Toolkit.KryptonButton();
            this.rbImpressThenMeasure = new Krypton.Toolkit.KryptonRadioButton();
            this.rbImpressAndMeasure = new Krypton.Toolkit.KryptonRadioButton();
            this.btnStart = new Krypton.Toolkit.KryptonButton();
            this.btnAddPattern = new Krypton.Toolkit.KryptonButton();
            this.cbMultiLines = new Krypton.Toolkit.KryptonCheckBox();
            this.rbImpressOnly = new Krypton.Toolkit.KryptonRadioButton();
            this.cbFocusAll = new Krypton.Toolkit.KryptonCheckBox();
            this.btnResetPattern = new Krypton.Toolkit.KryptonButton();
            this.btnFinishMultiPointMode = new Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbMultiPointsMode = new Krypton.Toolkit.KryptonComboBox();
            this.lbPattern = new Krypton.Toolkit.KryptonLabel();
            this.patternSlash = new AIO_Client.PatternSlashControl();
            this.patternVertical = new AIO_Client.PatternVerticalControl();
            this.tpPatternList = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClearPatternSet = new Krypton.Toolkit.KryptonButton();
            this.btnDeletePatternSet = new Krypton.Toolkit.KryptonButton();
            this.dgvPatterns = new Krypton.Toolkit.KryptonDataGridView();
            this.dgvhPatternIndex = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.Identifier = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhPatternName = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhPatternPointsCount = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhPatternSelected = new Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.tpStatistics = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.lbCPValue = new Krypton.Toolkit.KryptonLabel();
            this.lbNumber = new Krypton.Toolkit.KryptonLabel();
            this.lbCP = new Krypton.Toolkit.KryptonLabel();
            this.lbMax = new Krypton.Toolkit.KryptonLabel();
            this.lbCPKValue = new Krypton.Toolkit.KryptonLabel();
            this.lbMin = new Krypton.Toolkit.KryptonLabel();
            this.lbCPK = new Krypton.Toolkit.KryptonLabel();
            this.lbAvg = new Krypton.Toolkit.KryptonLabel();
            this.lbVarianceValue = new Krypton.Toolkit.KryptonLabel();
            this.lbStdDev = new Krypton.Toolkit.KryptonLabel();
            this.lbVariance = new Krypton.Toolkit.KryptonLabel();
            this.lbNumberValue = new Krypton.Toolkit.KryptonLabel();
            this.lbMaxValue = new Krypton.Toolkit.KryptonLabel();
            this.lbStdDevValue = new Krypton.Toolkit.KryptonLabel();
            this.lbMinValue = new Krypton.Toolkit.KryptonLabel();
            this.lbAvgValue = new Krypton.Toolkit.KryptonLabel();
            this.tpAlbum = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.measureRecordFlowLayout = new AIO_Client.MeasureRecordFlowLayout();
            this.lbConvertValue = new Krypton.Toolkit.KryptonLabel();
            this.btnStatistics = new Krypton.Toolkit.KryptonButton();
            this.lbHardnessType = new Krypton.Toolkit.KryptonLabel();
            this.lbHardnessValue = new Krypton.Toolkit.KryptonLabel();
            this.lbMicrometerValue = new Krypton.Toolkit.KryptonLabel();
            this.dgvRecords = new Krypton.Toolkit.KryptonDataGridView();
            this.dgvhRecordIndex = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordXPos = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordYPos = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordMoveTo = new Krypton.Toolkit.KryptonDataGridViewButtonColumn();
            this.dgvhRecordHardness = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordHardnessType = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordQualified = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordD1 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordD2 = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordDavg = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordConvertType = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordConvertValue = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordDepth = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordMeasureTime = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhOriginalImagePath = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.dgvhRecordImagePath = new Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.btnExportReport = new Krypton.Toolkit.KryptonButton();
            this.btnClearRecord = new Krypton.Toolkit.KryptonButton();
            this.btnDeleteRecord = new Krypton.Toolkit.KryptonButton();
            this.btnEditRecord = new Krypton.Toolkit.KryptonButton();
            this.cbConvertType = new Krypton.Toolkit.KryptonComboBox();
            this.kribbon_Main = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.kOATBtn_Open = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_Save = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_Play = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_Pause = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_AutoMeasure = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_ManualMeasure = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_Cursor = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_Angle = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_Clear = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_ZoomIn = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_ZoomOut = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.kOATBtn_CrossLine = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.krtab_File = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.krgrp_File = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.krbntrip_File = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiOpenImage = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiSaveImage = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiSaveOriginalImage = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.krbntrip_Exit = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiExit = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.krtab_Device = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiCameraStart = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiCameraStop = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.krtab_Data = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiSampleInfo = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.krtab_Tools = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiAutoMeasure = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiManualMeasure = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupTriple4 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiPointer = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiMeasureLength = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiMeasureAngle = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupTriple5 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiMagnifier = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiResumeImage = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupTriple6 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiClearGraphics = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiTrimMeasure = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiCenterCrossLine = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.krtab_Config = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup5 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple8 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiCalibration = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiForceCorrect = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiAutoMeasureSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupTriple9 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiCameraSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiSerialPortSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiXYPlatformSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupTriple10 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.tsmiZAxisSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiGenericSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.tsmiOtherSetting = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonTab1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.kryptonRibbonGroup4 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple7 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.kryptonRibbonGroupButton6 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcFuncArea)).BeginInit();
            this.tcFuncArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpMachineControl)).BeginInit();
            this.tpMachineControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHardnessLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbZoomTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpXYZ)).BeginInit();
            this.tpXYZ.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis.Panel)).BeginInit();
            this.gbZAxis.Panel.SuspendLayout();
            this.gbZAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform.Panel)).BeginInit();
            this.gbPlatform.Panel.SuspendLayout();
            this.gbPlatform.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpMultiPoints)).BeginInit();
            this.tpMultiPoints.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbMultiPointsMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpPatternList)).BeginInit();
            this.tpPatternList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatterns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpStatistics)).BeginInit();
            this.tpStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpAlbum)).BeginInit();
            this.tpAlbum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbConvertType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kribbon_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusSystem,
            this.statusCamera});
            this.statusStrip1.Location = new System.Drawing.Point(0, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1339, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusSystem
            // 
            this.statusSystem.Name = "statusSystem";
            this.statusSystem.Size = new System.Drawing.Size(213, 24);
            this.statusSystem.Text = "System Status：Normal";
            // 
            // statusCamera
            // 
            this.statusCamera.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.statusCamera.Name = "statusCamera";
            this.statusCamera.Size = new System.Drawing.Size(214, 25);
            this.statusCamera.Text = "Camera Status：Paused";
            this.statusCamera.Visible = false;
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Silver;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiDevice,
            this.tsmiData,
            this.tsmiTools,
            this.tsmiConfiguration,
            this.tsmiHelp});
            this.menuStrip.Location = new System.Drawing.Point(62, 44);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip.Size = new System.Drawing.Size(351, 30);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.Visible = false;
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(46, 24);
            this.tsmiFile.Text = "File";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(71, 6);
            // 
            // tsmiDevice
            // 
            this.tsmiDevice.Name = "tsmiDevice";
            this.tsmiDevice.Size = new System.Drawing.Size(68, 24);
            this.tsmiDevice.Text = "Device";
            // 
            // tsmiData
            // 
            this.tsmiData.Name = "tsmiData";
            this.tsmiData.Size = new System.Drawing.Size(55, 24);
            this.tsmiData.Text = "Data";
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.toolStripSeparator2,
            this.toolStripSeparator6,
            this.toolStripSeparator3});
            this.tsmiTools.Name = "tsmiTools";
            this.tsmiTools.Size = new System.Drawing.Size(58, 24);
            this.tsmiTools.Text = "Tools";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(71, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(71, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(71, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(71, 6);
            // 
            // tsmiConfiguration
            // 
            this.tsmiConfiguration.Name = "tsmiConfiguration";
            this.tsmiConfiguration.Size = new System.Drawing.Size(114, 24);
            this.tsmiConfiguration.Text = "Configuration";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(55, 24);
            this.tsmiHelp.Text = "Help";
            this.tsmiHelp.Visible = false;
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(133, 26);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.btnCenterCrossLine);
            this.splitContainer1.Panel1.Controls.Add(this.btnAutoMeasure);
            this.splitContainer1.Panel1.Controls.Add(this.btnOpen);
            this.splitContainer1.Panel1.Controls.Add(this.btnResumeImage);
            this.splitContainer1.Panel1.Controls.Add(this.btnMagnifier);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            this.splitContainer1.Panel1.Controls.Add(this.btnClearGraphics);
            this.splitContainer1.Panel1.Controls.Add(this.btnCameraStart);
            this.splitContainer1.Panel1.Controls.Add(this.btnMeasureAngle);
            this.splitContainer1.Panel1.Controls.Add(this.btnCameraPause);
            this.splitContainer1.Panel1.Controls.Add(this.btnMeasureLength);
            this.splitContainer1.Panel1.Controls.Add(this.btnPointer);
            this.splitContainer1.Panel1.Controls.Add(this.btnManualMeasure);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip);
            this.splitContainer1.Panel1.Controls.Add(this.lbYPos);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.lbXPos);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.kryptonPanel1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.lbMicrometer);
            this.splitContainer1.Panel2.Controls.Add(this.tcFuncArea);
            this.splitContainer1.Panel2.Controls.Add(this.lbConvertValue);
            this.splitContainer1.Panel2.Controls.Add(this.btnStatistics);
            this.splitContainer1.Panel2.Controls.Add(this.lbHardnessType);
            this.splitContainer1.Panel2.Controls.Add(this.lbHardnessValue);
            this.splitContainer1.Panel2.Controls.Add(this.lbMicrometerValue);
            this.splitContainer1.Panel2.Controls.Add(this.dgvRecords);
            this.splitContainer1.Panel2.Controls.Add(this.btnExportReport);
            this.splitContainer1.Panel2.Controls.Add(this.btnClearRecord);
            this.splitContainer1.Panel2.Controls.Add(this.btnDeleteRecord);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditRecord);
            this.splitContainer1.Panel2.Controls.Add(this.cbConvertType);
            this.splitContainer1.Size = new System.Drawing.Size(1335, 630);
            this.splitContainer1.SplitterDistance = 782;
            this.splitContainer1.TabIndex = 230;
            // 
            // btnCenterCrossLine
            // 
            this.btnCenterCrossLine.Location = new System.Drawing.Point(617, 136);
            this.btnCenterCrossLine.Name = "btnCenterCrossLine";
            this.btnCenterCrossLine.Palette = this.kp1;
            this.btnCenterCrossLine.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCenterCrossLine.Size = new System.Drawing.Size(35, 35);
            this.btnCenterCrossLine.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.CrossLine;
            this.btnCenterCrossLine.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnCenterCrossLine.TabIndex = 233;
            this.btnCenterCrossLine.Values.Text = "";
            this.btnCenterCrossLine.Visible = false;
            this.btnCenterCrossLine.Click += new System.EventHandler(this.tsmiCenterCrossLine_Click);
            // 
            // btnAutoMeasure
            // 
            this.btnAutoMeasure.Location = new System.Drawing.Point(289, 136);
            this.btnAutoMeasure.Name = "btnAutoMeasure";
            this.btnAutoMeasure.Palette = this.kp1;
            this.btnAutoMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnAutoMeasure.Size = new System.Drawing.Size(35, 35);
            this.btnAutoMeasure.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Auto_Measure;
            this.btnAutoMeasure.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnAutoMeasure.TabIndex = 228;
            this.btnAutoMeasure.Values.Text = "";
            this.btnAutoMeasure.Visible = false;
            this.btnAutoMeasure.Click += new System.EventHandler(this.tsmiAutoMeasure_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(166, 101);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Palette = this.kp1;
            this.btnOpen.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnOpen.Size = new System.Drawing.Size(35, 35);
            this.btnOpen.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Open;
            this.btnOpen.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnOpen.TabIndex = 217;
            this.btnOpen.Values.Text = "";
            this.btnOpen.Visible = false;
            this.btnOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // btnResumeImage
            // 
            this.btnResumeImage.Location = new System.Drawing.Point(576, 136);
            this.btnResumeImage.Name = "btnResumeImage";
            this.btnResumeImage.Palette = this.kp1;
            this.btnResumeImage.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnResumeImage.Size = new System.Drawing.Size(35, 35);
            this.btnResumeImage.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.MagnifierZoomOut;
            this.btnResumeImage.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnResumeImage.TabIndex = 227;
            this.btnResumeImage.Values.Text = "";
            this.btnResumeImage.Visible = false;
            this.btnResumeImage.Click += new System.EventHandler(this.tsmiResumeImage_Click);
            // 
            // btnMagnifier
            // 
            this.btnMagnifier.Location = new System.Drawing.Point(535, 136);
            this.btnMagnifier.Name = "btnMagnifier";
            this.btnMagnifier.Palette = this.kp1;
            this.btnMagnifier.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnMagnifier.Size = new System.Drawing.Size(35, 35);
            this.btnMagnifier.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.MagnifierZoomIn;
            this.btnMagnifier.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnMagnifier.TabIndex = 226;
            this.btnMagnifier.Values.Text = "";
            this.btnMagnifier.Visible = false;
            this.btnMagnifier.Click += new System.EventHandler(this.tsmiMagnifier_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(166, 136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(35, 35);
            this.btnSave.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.SaveAs;
            this.btnSave.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnSave.TabIndex = 218;
            this.btnSave.Values.Text = "";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.tsmiSaveImage_Click);
            // 
            // btnClearGraphics
            // 
            this.btnClearGraphics.Location = new System.Drawing.Point(494, 136);
            this.btnClearGraphics.Name = "btnClearGraphics";
            this.btnClearGraphics.Palette = this.kp1;
            this.btnClearGraphics.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnClearGraphics.Size = new System.Drawing.Size(35, 35);
            this.btnClearGraphics.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Clear;
            this.btnClearGraphics.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnClearGraphics.TabIndex = 225;
            this.btnClearGraphics.Values.Text = "";
            this.btnClearGraphics.Visible = false;
            this.btnClearGraphics.Click += new System.EventHandler(this.tsmiClearGraphics_Click);
            // 
            // btnCameraStart
            // 
            this.btnCameraStart.Location = new System.Drawing.Point(207, 136);
            this.btnCameraStart.Name = "btnCameraStart";
            this.btnCameraStart.Palette = this.kp1;
            this.btnCameraStart.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCameraStart.Size = new System.Drawing.Size(35, 35);
            this.btnCameraStart.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Start;
            this.btnCameraStart.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnCameraStart.TabIndex = 219;
            this.btnCameraStart.Values.Text = "";
            this.btnCameraStart.Visible = false;
            this.btnCameraStart.Click += new System.EventHandler(this.tsmiCameraStart_Click);
            // 
            // btnMeasureAngle
            // 
            this.btnMeasureAngle.Location = new System.Drawing.Point(453, 136);
            this.btnMeasureAngle.Name = "btnMeasureAngle";
            this.btnMeasureAngle.Palette = this.kp1;
            this.btnMeasureAngle.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnMeasureAngle.Size = new System.Drawing.Size(35, 35);
            this.btnMeasureAngle.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Angle;
            this.btnMeasureAngle.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnMeasureAngle.TabIndex = 224;
            this.btnMeasureAngle.Values.Text = "";
            this.btnMeasureAngle.Visible = false;
            this.btnMeasureAngle.Click += new System.EventHandler(this.tsmiMeasureAngle_Click);
            // 
            // btnCameraPause
            // 
            this.btnCameraPause.Location = new System.Drawing.Point(248, 136);
            this.btnCameraPause.Name = "btnCameraPause";
            this.btnCameraPause.Palette = this.kp1;
            this.btnCameraPause.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCameraPause.Size = new System.Drawing.Size(35, 35);
            this.btnCameraPause.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Pause;
            this.btnCameraPause.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnCameraPause.TabIndex = 220;
            this.btnCameraPause.Values.Text = "";
            this.btnCameraPause.Visible = false;
            this.btnCameraPause.Click += new System.EventHandler(this.tsmiCameraStop_Click);
            // 
            // btnMeasureLength
            // 
            this.btnMeasureLength.Location = new System.Drawing.Point(122, 133);
            this.btnMeasureLength.Name = "btnMeasureLength";
            this.btnMeasureLength.Size = new System.Drawing.Size(35, 35);
            this.btnMeasureLength.TabIndex = 223;
            this.btnMeasureLength.Values.Text = "";
            this.btnMeasureLength.Visible = false;
            this.btnMeasureLength.Click += new System.EventHandler(this.tsmiMeasureLength_Click);
            // 
            // btnPointer
            // 
            this.btnPointer.Enabled = false;
            this.btnPointer.Location = new System.Drawing.Point(371, 136);
            this.btnPointer.Name = "btnPointer";
            this.btnPointer.Palette = this.kp1;
            this.btnPointer.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPointer.Size = new System.Drawing.Size(35, 35);
            this.btnPointer.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Pointer;
            this.btnPointer.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnPointer.TabIndex = 221;
            this.btnPointer.Values.Text = "";
            this.btnPointer.Visible = false;
            this.btnPointer.Click += new System.EventHandler(this.tsmiPointer_Click);
            // 
            // btnManualMeasure
            // 
            this.btnManualMeasure.Location = new System.Drawing.Point(330, 136);
            this.btnManualMeasure.Name = "btnManualMeasure";
            this.btnManualMeasure.Palette = this.kp1;
            this.btnManualMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnManualMeasure.Size = new System.Drawing.Size(35, 35);
            this.btnManualMeasure.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.Indentation;
            this.btnManualMeasure.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnManualMeasure.TabIndex = 222;
            this.btnManualMeasure.Values.Text = "";
            this.btnManualMeasure.Visible = false;
            this.btnManualMeasure.Click += new System.EventHandler(this.tsmiManualMeasure_Click);
            // 
            // lbYPos
            // 
            this.lbYPos.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbYPos.Location = new System.Drawing.Point(123, 601);
            this.lbYPos.Name = "lbYPos";
            this.lbYPos.Palette = this.kp1;
            this.lbYPos.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbYPos.Size = new System.Drawing.Size(45, 24);
            this.lbYPos.TabIndex = 202;
            this.lbYPos.Values.Text = "1024";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label6.Location = new System.Drawing.Point(97, 601);
            this.label6.Name = "label6";
            this.label6.Palette = this.kp1;
            this.label6.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label6.Size = new System.Drawing.Size(23, 24);
            this.label6.TabIndex = 201;
            this.label6.Values.Text = "Y:";
            // 
            // lbXPos
            // 
            this.lbXPos.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbXPos.Location = new System.Drawing.Point(50, 601);
            this.lbXPos.Name = "lbXPos";
            this.lbXPos.Palette = this.kp1;
            this.lbXPos.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbXPos.Size = new System.Drawing.Size(45, 24);
            this.lbXPos.TabIndex = 200;
            this.lbXPos.Values.Text = "1024";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.Location = new System.Drawing.Point(23, 601);
            this.label2.Name = "label2";
            this.label2.Palette = this.kp1;
            this.label2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label2.Size = new System.Drawing.Size(24, 24);
            this.label2.TabIndex = 199;
            this.label2.Values.Text = "X:";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnplcrightward2);
            this.kryptonPanel1.Controls.Add(this.btnplcbackward2);
            this.kryptonPanel1.Controls.Add(this.btnplcforward2);
            this.kryptonPanel1.Controls.Add(this.btnplcleftward2);
            this.kryptonPanel1.Controls.Add(this.imageBox);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(782, 630);
            this.kryptonPanel1.TabIndex = 235;
            // 
            // btnplcrightward2
            // 
            this.btnplcrightward2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnplcrightward2.BackgroundImage = global::AIO_Client_Properties_Resources.arrow_rightward;
            this.btnplcrightward2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnplcrightward2.Location = new System.Drawing.Point(763, 317);
            this.btnplcrightward2.Name = "btnplcrightward2";
            this.btnplcrightward2.Palette = this.kp1;
            this.btnplcrightward2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnplcrightward2.Size = new System.Drawing.Size(10, 50);
            this.btnplcrightward2.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_rightward;
            this.btnplcrightward2.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnplcrightward2.TabIndex = 237;
            this.btnplcrightward2.Values.Text = "";
            // 
            // btnplcbackward2
            // 
            this.btnplcbackward2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnplcbackward2.BackgroundImage = global::AIO_Client_Properties_Resources.arrow_backward;
            this.btnplcbackward2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnplcbackward2.Location = new System.Drawing.Point(366, 595);
            this.btnplcbackward2.Name = "btnplcbackward2";
            this.btnplcbackward2.Palette = this.kp1;
            this.btnplcbackward2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnplcbackward2.Size = new System.Drawing.Size(50, 16);
            this.btnplcbackward2.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_backward;
            this.btnplcbackward2.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnplcbackward2.TabIndex = 235;
            this.btnplcbackward2.Values.Text = "";
            // 
            // btnplcforward2
            // 
            this.btnplcforward2.BackgroundImage = global::AIO_Client_Properties_Resources.arrow_forward;
            this.btnplcforward2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnplcforward2.Location = new System.Drawing.Point(366, 19);
            this.btnplcforward2.Name = "btnplcforward2";
            this.btnplcforward2.Palette = this.kp1;
            this.btnplcforward2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnplcforward2.Size = new System.Drawing.Size(50, 10);
            this.btnplcforward2.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_forward;
            this.btnplcforward2.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnplcforward2.TabIndex = 234;
            this.btnplcforward2.Values.Text = "";
            // 
            // btnplcleftward2
            // 
            this.btnplcleftward2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnplcleftward2.BackgroundImage = global::AIO_Client_Properties_Resources.arrow_leftward;
            this.btnplcleftward2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnplcleftward2.Location = new System.Drawing.Point(9, 317);
            this.btnplcleftward2.Name = "btnplcleftward2";
            this.btnplcleftward2.Palette = this.kp1;
            this.btnplcleftward2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnplcleftward2.Size = new System.Drawing.Size(10, 50);
            this.btnplcleftward2.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_leftward;
            this.btnplcleftward2.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnplcleftward2.TabIndex = 236;
            this.btnplcleftward2.Values.Text = "";
            // 
            // imageBox
            // 
            this.imageBox.ActiveToolIdentifier = null;
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BackColor = System.Drawing.Color.Gainsboro;
            this.imageBox.Image = null;
            this.imageBox.ImageHeight = 0;
            this.imageBox.Location = new System.Drawing.Point(13, 35);
            this.imageBox.Name = "imageBox";
            this.imageBox.PhysicalLocation = ((System.Drawing.PointF)(resources.GetObject("imageBox.PhysicalLocation")));
            this.imageBox.Size = new System.Drawing.Size(744, 554);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 233;
            this.imageBox.TabStop = false;
            // 
            // lbMicrometer
            // 
            this.lbMicrometer.BackColor = System.Drawing.SystemColors.Control;
            this.lbMicrometer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMicrometer.Location = new System.Drawing.Point(361, 6);
            this.lbMicrometer.Name = "lbMicrometer";
            this.lbMicrometer.Palette = this.kp1;
            this.lbMicrometer.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMicrometer.Size = new System.Drawing.Size(91, 24);
            this.lbMicrometer.TabIndex = 216;
            this.lbMicrometer.Values.Text = "Micrometer";
            this.lbMicrometer.Visible = false;
            // 
            // tcFuncArea
            // 
            this.tcFuncArea.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.tcFuncArea.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.tcFuncArea.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.tcFuncArea.Location = new System.Drawing.Point(3, 321);
            this.tcFuncArea.Name = "tcFuncArea";
            this.tcFuncArea.PageBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.tcFuncArea.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.tpMachineControl,
            this.tpXYZ,
            this.tpMultiPoints,
            this.tpPatternList,
            this.tpStatistics,
            this.tpAlbum});
            this.tcFuncArea.SelectedIndex = 0;
            this.tcFuncArea.Size = new System.Drawing.Size(524, 297);
            this.tcFuncArea.TabIndex = 234;
            this.tcFuncArea.Text = "kryptonNavigator1";
            // 
            // tpMachineControl
            // 
            this.tpMachineControl.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpMachineControl.Controls.Add(this.nudLoadTime);
            this.tpMachineControl.Controls.Add(this.btnImpress);
            this.tpMachineControl.Controls.Add(this.nudLightness);
            this.tpMachineControl.Controls.Add(this.lbLightness);
            this.tpMachineControl.Controls.Add(this.cbForce);
            this.tpMachineControl.Controls.Add(this.btnTurretCenter);
            this.tpMachineControl.Controls.Add(this.lbForce);
            this.tpMachineControl.Controls.Add(this.lbTurret);
            this.tpMachineControl.Controls.Add(this.btnTurretRight);
            this.tpMachineControl.Controls.Add(this.lbLoadTime);
            this.tpMachineControl.Controls.Add(this.cbHardnessLevel);
            this.tpMachineControl.Controls.Add(this.cbZoomTime);
            this.tpMachineControl.Controls.Add(this.btnTurretLeft);
            this.tpMachineControl.Controls.Add(this.lbObjective);
            this.tpMachineControl.Controls.Add(this.lbHardnessLevel);
            this.tpMachineControl.Flags = 65534;
            this.tpMachineControl.LastVisibleSet = true;
            this.tpMachineControl.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpMachineControl.Name = "tpMachineControl";
            this.tpMachineControl.Size = new System.Drawing.Size(522, 266);
            this.tpMachineControl.Text = "Machine Control";
            this.tpMachineControl.ToolTipTitle = "Page ToolTip";
            this.tpMachineControl.UniqueName = "4071BB815CE540D4D487F752D743384F";
            // 
            // nudLoadTime
            // 
            this.nudLoadTime.Location = new System.Drawing.Point(369, 162);
            this.nudLoadTime.Name = "nudLoadTime";
            this.nudLoadTime.Size = new System.Drawing.Size(120, 26);
            this.nudLoadTime.TabIndex = 215;
            this.nudLoadTime.Visible = false;
            this.nudLoadTime.ValueChanged += new System.EventHandler(this.nudLoadTime_ValueChanged);
            // 
            // btnImpress
            // 
            this.btnImpress.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImpress.Location = new System.Drawing.Point(43, 43);
            this.btnImpress.Name = "btnImpress";
            this.btnImpress.Palette = this.kp1;
            this.btnImpress.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnImpress.Size = new System.Drawing.Size(130, 45);
            this.btnImpress.TabIndex = 184;
            this.btnImpress.Values.Text = "Force";
            this.btnImpress.Visible = false;
            this.btnImpress.Click += new System.EventHandler(this.btnImpress_Click);
            // 
            // nudLightness
            // 
            this.nudLightness.Location = new System.Drawing.Point(369, 128);
            this.nudLightness.Name = "nudLightness";
            this.nudLightness.Size = new System.Drawing.Size(120, 26);
            this.nudLightness.TabIndex = 214;
            this.nudLightness.Visible = false;
            this.nudLightness.ValueChanged += new System.EventHandler(this.nudLightness_ValueChanged);
            // 
            // lbLightness
            // 
            this.lbLightness.Location = new System.Drawing.Point(274, 128);
            this.lbLightness.Name = "lbLightness";
            this.lbLightness.Palette = this.kp1;
            this.lbLightness.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLightness.Size = new System.Drawing.Size(76, 24);
            this.lbLightness.TabIndex = 180;
            this.lbLightness.Values.Text = "Lightness";
            this.lbLightness.Visible = false;
            // 
            // cbForce
            // 
            this.cbForce.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForce.DropDownWidth = 111;
            this.cbForce.FormattingEnabled = true;
            this.cbForce.IntegralHeight = false;
            this.cbForce.Items.AddRange(new object[] {
            "10gf",
            "15gf",
            "20gf",
            "25gf",
            "50gf",
            "100gf",
            "200gf",
            "300gf",
            "500gf",
            "1000gf",
            "2000gf",
            "2500gf",
            "3000gf",
            "5000gf",
            "10000gf",
            "15000gf",
            "20000gf",
            "30000gf",
            "50000gf",
            "60000gf",
            "80000gf",
            "100000gf",
            "120000gf"});
            this.cbForce.Location = new System.Drawing.Point(142, 126);
            this.cbForce.Name = "cbForce";
            this.cbForce.Palette = this.kp1;
            this.cbForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbForce.Size = new System.Drawing.Size(111, 25);
            this.cbForce.TabIndex = 205;
            this.cbForce.SelectedIndexChanged += new System.EventHandler(this.cbForce_SelectedIndexChanged);
            // 
            // btnTurretCenter
            // 
            this.btnTurretCenter.Location = new System.Drawing.Point(383, 43);
            this.btnTurretCenter.Name = "btnTurretCenter";
            this.btnTurretCenter.Palette = this.kp1;
            this.btnTurretCenter.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnTurretCenter.Size = new System.Drawing.Size(60, 45);
            this.btnTurretCenter.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_down_blue;
            this.btnTurretCenter.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnTurretCenter.TabIndex = 206;
            this.btnTurretCenter.Values.Text = "";
            this.btnTurretCenter.Visible = false;
            this.btnTurretCenter.Click += new System.EventHandler(this.btnTurretCenter_Click);
            // 
            // lbForce
            // 
            this.lbForce.Location = new System.Drawing.Point(16, 128);
            this.lbForce.Name = "lbForce";
            this.lbForce.Palette = this.kp1;
            this.lbForce.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbForce.Size = new System.Drawing.Size(49, 24);
            this.lbForce.TabIndex = 204;
            this.lbForce.Values.Text = "Force";
            // 
            // lbTurret
            // 
            this.lbTurret.Location = new System.Drawing.Point(191, 54);
            this.lbTurret.Name = "lbTurret";
            this.lbTurret.Palette = this.kp1;
            this.lbTurret.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbTurret.Size = new System.Drawing.Size(52, 24);
            this.lbTurret.TabIndex = 210;
            this.lbTurret.Values.Text = "Turret";
            this.lbTurret.Visible = false;
            // 
            // btnTurretRight
            // 
            this.btnTurretRight.Location = new System.Drawing.Point(449, 43);
            this.btnTurretRight.Name = "btnTurretRight";
            this.btnTurretRight.Palette = this.kp1;
            this.btnTurretRight.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnTurretRight.Size = new System.Drawing.Size(60, 45);
            this.btnTurretRight.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_right_blue;
            this.btnTurretRight.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnTurretRight.TabIndex = 187;
            this.btnTurretRight.Values.Text = "";
            this.btnTurretRight.Visible = false;
            this.btnTurretRight.Click += new System.EventHandler(this.btnTurretRight_Click);
            // 
            // lbLoadTime
            // 
            this.lbLoadTime.Location = new System.Drawing.Point(274, 162);
            this.lbLoadTime.Name = "lbLoadTime";
            this.lbLoadTime.Palette = this.kp1;
            this.lbLoadTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLoadTime.Size = new System.Drawing.Size(98, 24);
            this.lbLoadTime.TabIndex = 178;
            this.lbLoadTime.Values.Text = "Load Time(s)";
            this.lbLoadTime.Visible = false;
            // 
            // cbHardnessLevel
            // 
            this.cbHardnessLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbHardnessLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHardnessLevel.DropDownWidth = 111;
            this.cbHardnessLevel.FormattingEnabled = true;
            this.cbHardnessLevel.IntegralHeight = false;
            this.cbHardnessLevel.Items.AddRange(new object[] {
            "Low",
            "Middle",
            "High"});
            this.cbHardnessLevel.Location = new System.Drawing.Point(142, 194);
            this.cbHardnessLevel.Name = "cbHardnessLevel";
            this.cbHardnessLevel.Palette = this.kp1;
            this.cbHardnessLevel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbHardnessLevel.Size = new System.Drawing.Size(111, 25);
            this.cbHardnessLevel.TabIndex = 213;
            this.cbHardnessLevel.SelectedIndexChanged += new System.EventHandler(this.cbHardnessLevel_SelectedIndexChanged);
            // 
            // cbZoomTime
            // 
            this.cbZoomTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbZoomTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZoomTime.DropDownWidth = 111;
            this.cbZoomTime.FormattingEnabled = true;
            this.cbZoomTime.IntegralHeight = false;
            this.cbZoomTime.Items.AddRange(new object[] {
            "5X",
            "10X",
            "20X",
            "40X",
            "50X"});
            this.cbZoomTime.Location = new System.Drawing.Point(142, 160);
            this.cbZoomTime.Name = "cbZoomTime";
            this.cbZoomTime.Palette = this.kp1;
            this.cbZoomTime.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbZoomTime.Size = new System.Drawing.Size(111, 25);
            this.cbZoomTime.TabIndex = 177;
            this.cbZoomTime.SelectedIndexChanged += new System.EventHandler(this.cbZoomTime_SelectedIndexChanged);
            // 
            // btnTurretLeft
            // 
            this.btnTurretLeft.Location = new System.Drawing.Point(317, 43);
            this.btnTurretLeft.Name = "btnTurretLeft";
            this.btnTurretLeft.Palette = this.kp1;
            this.btnTurretLeft.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnTurretLeft.Size = new System.Drawing.Size(60, 45);
            this.btnTurretLeft.StateCommon.Back.Image = global::AIO_Client_Properties_Resources.arrow_left_blue;
            this.btnTurretLeft.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            this.btnTurretLeft.TabIndex = 186;
            this.btnTurretLeft.Values.Text = "";
            this.btnTurretLeft.Visible = false;
            this.btnTurretLeft.Click += new System.EventHandler(this.btnTurretLeft_Click);
            // 
            // lbObjective
            // 
            this.lbObjective.Location = new System.Drawing.Point(16, 162);
            this.lbObjective.Name = "lbObjective";
            this.lbObjective.Palette = this.kp1;
            this.lbObjective.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbObjective.Size = new System.Drawing.Size(76, 24);
            this.lbObjective.TabIndex = 176;
            this.lbObjective.Values.Text = "Objective";
            // 
            // lbHardnessLevel
            // 
            this.lbHardnessLevel.Location = new System.Drawing.Point(16, 196);
            this.lbHardnessLevel.Name = "lbHardnessLevel";
            this.lbHardnessLevel.Palette = this.kp1;
            this.lbHardnessLevel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHardnessLevel.Size = new System.Drawing.Size(114, 24);
            this.lbHardnessLevel.TabIndex = 212;
            this.lbHardnessLevel.Values.Text = "Hardness Level";
            // 
            // tpXYZ
            // 
            this.tpXYZ.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpXYZ.Controls.Add(this.tableLayoutPanel2);
            this.tpXYZ.Flags = 65534;
            this.tpXYZ.LastVisibleSet = true;
            this.tpXYZ.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpXYZ.Name = "tpXYZ";
            this.tpXYZ.Size = new System.Drawing.Size(522, 270);
            this.tpXYZ.Text = "XYZ Platform Control";
            this.tpXYZ.ToolTipTitle = "Page ToolTip";
            this.tpXYZ.UniqueName = "9BC514354D944C314694FE3E32800DE7";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel2.BackgroundImage")));
            this.tableLayoutPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.Controls.Add(this.gbZAxis, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbPlatform, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Palette = this.kp1;
            this.tableLayoutPanel2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(522, 270);
            this.tableLayoutPanel2.TabIndex = 223;
            // 
            // gbZAxis
            // 
            this.gbZAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbZAxis.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbZAxis.Location = new System.Drawing.Point(342, 3);
            this.gbZAxis.Name = "gbZAxis";
            this.gbZAxis.Palette = this.kp1;
            this.gbZAxis.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbZAxis.Panel
            // 
            this.gbZAxis.Panel.Controls.Add(this.btnAutoFocus);
            this.gbZAxis.Panel.Controls.Add(this.btnZAxisUnlock);
            this.gbZAxis.Panel.Controls.Add(this.btnZAxisLock);
            this.gbZAxis.Panel.Controls.Add(this.btnZAxisDownward);
            this.gbZAxis.Panel.Controls.Add(this.btnZAxisUpward);
            this.gbZAxis.Size = new System.Drawing.Size(177, 264);
            this.gbZAxis.TabIndex = 222;
            this.gbZAxis.Values.Heading = "Z";
            // 
            // btnAutoFocus
            // 
            this.btnAutoFocus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAutoFocus.Location = new System.Drawing.Point(6, 124);
            this.btnAutoFocus.Name = "btnAutoFocus";
            this.btnAutoFocus.Size = new System.Drawing.Size(76, 94);
            this.btnAutoFocus.TabIndex = 212;
            this.btnAutoFocus.Values.Text = "Auto Focus";
            this.btnAutoFocus.Click += new System.EventHandler(this.btnAutoFocus_Click);
            // 
            // btnZAxisUnlock
            // 
            this.btnZAxisUnlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnZAxisUnlock.Location = new System.Drawing.Point(88, 74);
            this.btnZAxisUnlock.Name = "btnZAxisUnlock";
            this.btnZAxisUnlock.Size = new System.Drawing.Size(76, 44);
            this.btnZAxisUnlock.TabIndex = 207;
            this.btnZAxisUnlock.Values.Text = "Unlock";
            this.btnZAxisUnlock.Click += new System.EventHandler(this.btnZAxisUnlock_Click);
            // 
            // btnZAxisLock
            // 
            this.btnZAxisLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnZAxisLock.Location = new System.Drawing.Point(6, 74);
            this.btnZAxisLock.Name = "btnZAxisLock";
            this.btnZAxisLock.Size = new System.Drawing.Size(76, 44);
            this.btnZAxisLock.TabIndex = 206;
            this.btnZAxisLock.Values.Text = "Lock";
            this.btnZAxisLock.Click += new System.EventHandler(this.btnZAxisLock_Click);
            // 
            // btnZAxisDownward
            // 
            this.btnZAxisDownward.Location = new System.Drawing.Point(88, 174);
            this.btnZAxisDownward.Name = "btnZAxisDownward";
            this.btnZAxisDownward.Size = new System.Drawing.Size(76, 44);
            this.btnZAxisDownward.TabIndex = 198;
            this.btnZAxisDownward.Click += new System.EventHandler(this.btnZAxisDownward_Click);
            // 
            // btnZAxisUpward
            // 
            this.btnZAxisUpward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnZAxisUpward.Location = new System.Drawing.Point(88, 124);
            this.btnZAxisUpward.Name = "btnZAxisUpward";
            this.btnZAxisUpward.Size = new System.Drawing.Size(76, 44);
            this.btnZAxisUpward.TabIndex = 197;
            this.btnZAxisUpward.Click += new System.EventHandler(this.btnZAxisUpward_Click);
            // 
            // gbPlatform
            // 
            this.gbPlatform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPlatform.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbPlatform.Location = new System.Drawing.Point(3, 3);
            this.gbPlatform.Name = "gbPlatform";
            this.gbPlatform.Palette = this.kp1;
            this.gbPlatform.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbPlatform.Panel
            // 
            this.gbPlatform.Panel.Controls.Add(this.tableLayoutPanel4);
            this.gbPlatform.Size = new System.Drawing.Size(333, 264);
            this.gbPlatform.TabIndex = 221;
            this.gbPlatform.Values.Heading = "X/Y";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel4.BackgroundImage")));
            this.tableLayoutPanel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.lbSYJKYPos, 3, 4);
            this.tableLayoutPanel4.Controls.Add(this.label7, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.btnLockMotor, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnXYLocalCenter, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.lbSYJKXPos, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCLeftForward, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCRightward, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCCenter, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCRightBackward, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCLeftBackward, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.btnUnlockMotor, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCBackward, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.rbSlow, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbMedium, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbFast, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCForward, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.rbVeryFast, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCRightForward, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnPLCLeftward, 0, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Palette = this.kp1;
            this.tableLayoutPanel4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tableLayoutPanel4.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(329, 263);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // lbSYJKYPos
            // 
            this.lbSYJKYPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSYJKYPos.Location = new System.Drawing.Point(249, 211);
            this.lbSYJKYPos.Name = "lbSYJKYPos";
            this.lbSYJKYPos.Palette = this.kp1;
            this.lbSYJKYPos.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSYJKYPos.Size = new System.Drawing.Size(77, 49);
            this.lbSYJKYPos.TabIndex = 222;
            this.lbSYJKYPos.Values.Text = "0";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(167, 211);
            this.label7.Name = "label7";
            this.label7.Palette = this.kp1;
            this.label7.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label7.Size = new System.Drawing.Size(76, 49);
            this.label7.TabIndex = 221;
            this.label7.Values.Text = "Y:";
            // 
            // btnLockMotor
            // 
            this.btnLockMotor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLockMotor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLockMotor.Location = new System.Drawing.Point(249, 55);
            this.btnLockMotor.Name = "btnLockMotor";
            this.btnLockMotor.Palette = this.kp1;
            this.btnLockMotor.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnLockMotor.Size = new System.Drawing.Size(77, 46);
            this.btnLockMotor.TabIndex = 205;
            this.btnLockMotor.Values.Text = "Lock";
            this.btnLockMotor.Click += new System.EventHandler(this.btnLockMotor_Click);
            // 
            // btnXYLocalCenter
            // 
            this.btnXYLocalCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnXYLocalCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXYLocalCenter.Location = new System.Drawing.Point(249, 159);
            this.btnXYLocalCenter.Name = "btnXYLocalCenter";
            this.btnXYLocalCenter.Palette = this.kp1;
            this.btnXYLocalCenter.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnXYLocalCenter.Size = new System.Drawing.Size(77, 46);
            this.btnXYLocalCenter.TabIndex = 211;
            this.btnXYLocalCenter.Values.Text = "Relocation";
            this.btnXYLocalCenter.Click += new System.EventHandler(this.btnXYLocalCenter_Click);
            // 
            // lbSYJKXPos
            // 
            this.lbSYJKXPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSYJKXPos.Location = new System.Drawing.Point(85, 211);
            this.lbSYJKXPos.Name = "lbSYJKXPos";
            this.lbSYJKXPos.Palette = this.kp1;
            this.lbSYJKXPos.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSYJKXPos.Size = new System.Drawing.Size(76, 49);
            this.lbSYJKXPos.TabIndex = 216;
            this.lbSYJKXPos.Values.Text = "0";
            // 
            // btnPLCLeftForward
            // 
            this.btnPLCLeftForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPLCLeftForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCLeftForward.Location = new System.Drawing.Point(3, 55);
            this.btnPLCLeftForward.Name = "btnPLCLeftForward";
            this.btnPLCLeftForward.Palette = this.kp1;
            this.btnPLCLeftForward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCLeftForward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCLeftForward.TabIndex = 200;
            this.btnPLCLeftForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCLeftForward_MouseDown);
            this.btnPLCLeftForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 211);
            this.label4.Name = "label4";
            this.label4.Palette = this.kp1;
            this.label4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label4.Size = new System.Drawing.Size(76, 49);
            this.label4.TabIndex = 166;
            this.label4.Values.Text = "X:";
            // 
            // btnPLCRightward
            // 
            this.btnPLCRightward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCRightward.Location = new System.Drawing.Point(167, 107);
            this.btnPLCRightward.Name = "btnPLCRightward";
            this.btnPLCRightward.Palette = this.kp1;
            this.btnPLCRightward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCRightward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCRightward.TabIndex = 199;
            this.btnPLCRightward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCRightward_MouseDown);
            this.btnPLCRightward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // btnPLCCenter
            // 
            this.btnPLCCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCCenter.Location = new System.Drawing.Point(85, 107);
            this.btnPLCCenter.Name = "btnPLCCenter";
            this.btnPLCCenter.Palette = this.kp1;
            this.btnPLCCenter.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCCenter.Size = new System.Drawing.Size(76, 46);
            this.btnPLCCenter.TabIndex = 204;
            this.btnPLCCenter.Tag = "";
            this.btnPLCCenter.Click += new System.EventHandler(this.btnPLCCenter_Click);
            // 
            // btnPLCRightBackward
            // 
            this.btnPLCRightBackward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPLCRightBackward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCRightBackward.Location = new System.Drawing.Point(167, 159);
            this.btnPLCRightBackward.Name = "btnPLCRightBackward";
            this.btnPLCRightBackward.Palette = this.kp1;
            this.btnPLCRightBackward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCRightBackward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCRightBackward.TabIndex = 202;
            this.btnPLCRightBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCRightBackward_MouseDown);
            this.btnPLCRightBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // btnPLCLeftBackward
            // 
            this.btnPLCLeftBackward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPLCLeftBackward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCLeftBackward.Location = new System.Drawing.Point(3, 159);
            this.btnPLCLeftBackward.Name = "btnPLCLeftBackward";
            this.btnPLCLeftBackward.Palette = this.kp1;
            this.btnPLCLeftBackward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCLeftBackward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCLeftBackward.TabIndex = 203;
            this.btnPLCLeftBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCLeftBackward_MouseDown);
            this.btnPLCLeftBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // btnUnlockMotor
            // 
            this.btnUnlockMotor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUnlockMotor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUnlockMotor.Location = new System.Drawing.Point(249, 107);
            this.btnUnlockMotor.Name = "btnUnlockMotor";
            this.btnUnlockMotor.Palette = this.kp1;
            this.btnUnlockMotor.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnUnlockMotor.Size = new System.Drawing.Size(77, 46);
            this.btnUnlockMotor.TabIndex = 206;
            this.btnUnlockMotor.Values.Text = "Unlock";
            this.btnUnlockMotor.Click += new System.EventHandler(this.btnUnlockMotor_Click);
            // 
            // btnPLCBackward
            // 
            this.btnPLCBackward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCBackward.Location = new System.Drawing.Point(85, 159);
            this.btnPLCBackward.Name = "btnPLCBackward";
            this.btnPLCBackward.Palette = this.kp1;
            this.btnPLCBackward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCBackward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCBackward.TabIndex = 197;
            this.btnPLCBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCBackward_MouseDown);
            this.btnPLCBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // rbSlow
            // 
            this.rbSlow.Checked = true;
            this.rbSlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbSlow.Location = new System.Drawing.Point(3, 3);
            this.rbSlow.Name = "rbSlow";
            this.rbSlow.Palette = this.kp1;
            this.rbSlow.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbSlow.Size = new System.Drawing.Size(76, 46);
            this.rbSlow.TabIndex = 217;
            this.rbSlow.Values.Text = "Slow";
            this.rbSlow.CheckedChanged += new System.EventHandler(this.syjkSpeedRate_CheckedChanged);
            // 
            // rbMedium
            // 
            this.rbMedium.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbMedium.Location = new System.Drawing.Point(85, 3);
            this.rbMedium.Name = "rbMedium";
            this.rbMedium.Palette = this.kp1;
            this.rbMedium.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbMedium.Size = new System.Drawing.Size(76, 46);
            this.rbMedium.TabIndex = 218;
            this.rbMedium.Values.Text = "Mid";
            this.rbMedium.CheckedChanged += new System.EventHandler(this.syjkSpeedRate_CheckedChanged);
            // 
            // rbFast
            // 
            this.rbFast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbFast.Location = new System.Drawing.Point(167, 3);
            this.rbFast.Name = "rbFast";
            this.rbFast.Palette = this.kp1;
            this.rbFast.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbFast.Size = new System.Drawing.Size(76, 46);
            this.rbFast.TabIndex = 219;
            this.rbFast.Values.Text = "Fast";
            this.rbFast.CheckedChanged += new System.EventHandler(this.syjkSpeedRate_CheckedChanged);
            // 
            // btnPLCForward
            // 
            this.btnPLCForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPLCForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCForward.Location = new System.Drawing.Point(85, 55);
            this.btnPLCForward.Name = "btnPLCForward";
            this.btnPLCForward.Palette = this.kp1;
            this.btnPLCForward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCForward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCForward.TabIndex = 196;
            this.btnPLCForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCForward_MouseDown);
            this.btnPLCForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // rbVeryFast
            // 
            this.rbVeryFast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbVeryFast.Location = new System.Drawing.Point(249, 3);
            this.rbVeryFast.Name = "rbVeryFast";
            this.rbVeryFast.Palette = this.kp1;
            this.rbVeryFast.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbVeryFast.Size = new System.Drawing.Size(77, 46);
            this.rbVeryFast.TabIndex = 220;
            this.rbVeryFast.Values.Text = "Ultra";
            this.rbVeryFast.CheckedChanged += new System.EventHandler(this.syjkSpeedRate_CheckedChanged);
            // 
            // btnPLCRightForward
            // 
            this.btnPLCRightForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPLCRightForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCRightForward.Location = new System.Drawing.Point(167, 55);
            this.btnPLCRightForward.Name = "btnPLCRightForward";
            this.btnPLCRightForward.Palette = this.kp1;
            this.btnPLCRightForward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCRightForward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCRightForward.TabIndex = 201;
            this.btnPLCRightForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCRightForward_MouseDown);
            this.btnPLCRightForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // btnPLCLeftward
            // 
            this.btnPLCLeftward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLCLeftward.Location = new System.Drawing.Point(3, 107);
            this.btnPLCLeftward.Name = "btnPLCLeftward";
            this.btnPLCLeftward.Palette = this.kp1;
            this.btnPLCLeftward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPLCLeftward.Size = new System.Drawing.Size(76, 46);
            this.btnPLCLeftward.TabIndex = 198;
            this.btnPLCLeftward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPLCLeftward_MouseDown);
            this.btnPLCLeftward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PLC_MouseUp);
            // 
            // tpMultiPoints
            // 
            this.tpMultiPoints.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpMultiPoints.Controls.Add(this.patternCircleExtension);
            this.tpMultiPoints.Controls.Add(this.patternMatrix);
            this.tpMultiPoints.Controls.Add(this.patternFree);
            this.tpMultiPoints.Controls.Add(this.patternHorizontal);
            this.tpMultiPoints.Controls.Add(this.tableLayoutPanel5);
            this.tpMultiPoints.Controls.Add(this.tableLayoutPanel3);
            this.tpMultiPoints.Controls.Add(this.patternSlash);
            this.tpMultiPoints.Controls.Add(this.patternVertical);
            this.tpMultiPoints.Flags = 65534;
            this.tpMultiPoints.LastVisibleSet = true;
            this.tpMultiPoints.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpMultiPoints.Name = "tpMultiPoints";
            this.tpMultiPoints.Size = new System.Drawing.Size(522, 270);
            this.tpMultiPoints.Text = "Multipoint";
            this.tpMultiPoints.ToolTipTitle = "Page ToolTip";
            this.tpMultiPoints.UniqueName = "3157C598CE464F0B25B176D6C370914A";
            // 
            // patternCircleExtension
            // 
            this.patternCircleExtension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternCircleExtension.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F);
            this.patternCircleExtension.Location = new System.Drawing.Point(0, 30);
            this.patternCircleExtension.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.patternCircleExtension.Name = "patternCircleExtension";
            this.patternCircleExtension.Size = new System.Drawing.Size(522, 174);
            this.patternCircleExtension.TabIndex = 226;
            // 
            // patternMatrix
            // 
            this.patternMatrix.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.patternMatrix.Location = new System.Drawing.Point(0, 30);
            this.patternMatrix.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.patternMatrix.Name = "patternMatrix";
            this.patternMatrix.Size = new System.Drawing.Size(522, 174);
            this.patternMatrix.TabIndex = 222;
            // 
            // patternFree
            // 
            this.patternFree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternFree.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.patternFree.Location = new System.Drawing.Point(0, 30);
            this.patternFree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.patternFree.Name = "patternFree";
            this.patternFree.Size = new System.Drawing.Size(522, 174);
            this.patternFree.TabIndex = 224;
            // 
            // patternHorizontal
            // 
            this.patternHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternHorizontal.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.patternHorizontal.Location = new System.Drawing.Point(0, 30);
            this.patternHorizontal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.patternHorizontal.Name = "patternHorizontal";
            this.patternHorizontal.Size = new System.Drawing.Size(522, 174);
            this.patternHorizontal.TabIndex = 225;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.btnPauseOrResumeMultiPointMode, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.rbImpressThenMeasure, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.rbImpressAndMeasure, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnStart, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnAddPattern, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbMultiLines, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.rbImpressOnly, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.cbFocusAll, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnResetPattern, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.btnFinishMultiPointMode, 4, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 204);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(522, 66);
            this.tableLayoutPanel5.TabIndex = 218;
            // 
            // btnPauseOrResumeMultiPointMode
            // 
            this.btnPauseOrResumeMultiPointMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPauseOrResumeMultiPointMode.Location = new System.Drawing.Point(315, 3);
            this.btnPauseOrResumeMultiPointMode.Name = "btnPauseOrResumeMultiPointMode";
            this.btnPauseOrResumeMultiPointMode.Palette = this.kp1;
            this.btnPauseOrResumeMultiPointMode.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnPauseOrResumeMultiPointMode.Size = new System.Drawing.Size(98, 26);
            this.btnPauseOrResumeMultiPointMode.TabIndex = 9;
            this.btnPauseOrResumeMultiPointMode.Values.Text = "Resume";
            this.btnPauseOrResumeMultiPointMode.Visible = false;
            this.btnPauseOrResumeMultiPointMode.Click += new System.EventHandler(this.btnPauseOrResumeMultiPointMode_Click);
            // 
            // rbImpressThenMeasure
            // 
            this.rbImpressThenMeasure.Location = new System.Drawing.Point(211, 36);
            this.rbImpressThenMeasure.Name = "rbImpressThenMeasure";
            this.rbImpressThenMeasure.Palette = this.kp1;
            this.rbImpressThenMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbImpressThenMeasure.Size = new System.Drawing.Size(98, 24);
            this.rbImpressThenMeasure.TabIndex = 7;
            this.rbImpressThenMeasure.Values.Text = "After Impress";
            // 
            // rbImpressAndMeasure
            // 
            this.rbImpressAndMeasure.Location = new System.Drawing.Point(107, 36);
            this.rbImpressAndMeasure.Name = "rbImpressAndMeasure";
            this.rbImpressAndMeasure.Palette = this.kp1;
            this.rbImpressAndMeasure.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbImpressAndMeasure.Size = new System.Drawing.Size(98, 24);
            this.rbImpressAndMeasure.TabIndex = 6;
            this.rbImpressAndMeasure.Values.Text = "While Impress";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Palette = this.kp1;
            this.btnStart.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnStart.Size = new System.Drawing.Size(98, 26);
            this.btnStart.TabIndex = 0;
            this.btnStart.Values.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnAddPattern
            // 
            this.btnAddPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPattern.Location = new System.Drawing.Point(107, 3);
            this.btnAddPattern.Name = "btnAddPattern";
            this.btnAddPattern.Palette = this.kp1;
            this.btnAddPattern.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnAddPattern.Size = new System.Drawing.Size(98, 26);
            this.btnAddPattern.TabIndex = 1;
            this.btnAddPattern.Values.Text = "Add Pattern";
            this.btnAddPattern.Click += new System.EventHandler(this.btnAddPattern_Click);
            // 
            // cbMultiLines
            // 
            this.cbMultiLines.Location = new System.Drawing.Point(211, 3);
            this.cbMultiLines.Name = "cbMultiLines";
            this.cbMultiLines.Palette = this.kp1;
            this.cbMultiLines.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbMultiLines.Size = new System.Drawing.Size(94, 24);
            this.cbMultiLines.TabIndex = 4;
            this.cbMultiLines.Values.Text = "MultiLines";
            // 
            // rbImpressOnly
            // 
            this.rbImpressOnly.Checked = true;
            this.rbImpressOnly.Location = new System.Drawing.Point(3, 36);
            this.rbImpressOnly.Name = "rbImpressOnly";
            this.rbImpressOnly.Palette = this.kp1;
            this.rbImpressOnly.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.rbImpressOnly.Size = new System.Drawing.Size(77, 24);
            this.rbImpressOnly.TabIndex = 5;
            this.rbImpressOnly.Values.Text = "Impress";
            // 
            // cbFocusAll
            // 
            this.cbFocusAll.Location = new System.Drawing.Point(315, 36);
            this.cbFocusAll.Name = "cbFocusAll";
            this.cbFocusAll.Palette = this.kp1;
            this.cbFocusAll.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbFocusAll.Size = new System.Drawing.Size(85, 24);
            this.cbFocusAll.TabIndex = 8;
            this.cbFocusAll.Values.Text = "Focus All";
            // 
            // btnResetPattern
            // 
            this.btnResetPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetPattern.Location = new System.Drawing.Point(419, 36);
            this.btnResetPattern.Name = "btnResetPattern";
            this.btnResetPattern.Palette = this.kp1;
            this.btnResetPattern.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnResetPattern.Size = new System.Drawing.Size(100, 26);
            this.btnResetPattern.TabIndex = 2;
            this.btnResetPattern.Values.Text = "Reset";
            this.btnResetPattern.Click += new System.EventHandler(this.btnResetPattern_Click);
            // 
            // btnFinishMultiPointMode
            // 
            this.btnFinishMultiPointMode.Location = new System.Drawing.Point(419, 3);
            this.btnFinishMultiPointMode.Name = "btnFinishMultiPointMode";
            this.btnFinishMultiPointMode.Palette = this.kp1;
            this.btnFinishMultiPointMode.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnFinishMultiPointMode.Size = new System.Drawing.Size(96, 26);
            this.btnFinishMultiPointMode.TabIndex = 3;
            this.btnFinishMultiPointMode.Values.Text = "Finish";
            this.btnFinishMultiPointMode.Visible = false;
            this.btnFinishMultiPointMode.Click += new System.EventHandler(this.btnFinishMultiPointMode_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel3.Controls.Add(this.cbMultiPointsMode, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbPattern, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(522, 30);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // cbMultiPointsMode
            // 
            this.cbMultiPointsMode.BackColor = System.Drawing.SystemColors.Window;
            this.cbMultiPointsMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMultiPointsMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbMultiPointsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMultiPointsMode.DropDownWidth = 351;
            this.cbMultiPointsMode.FormattingEnabled = true;
            this.cbMultiPointsMode.IntegralHeight = false;
            this.cbMultiPointsMode.Location = new System.Drawing.Point(159, 3);
            this.cbMultiPointsMode.Name = "cbMultiPointsMode";
            this.cbMultiPointsMode.Palette = this.kp1;
            this.cbMultiPointsMode.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbMultiPointsMode.Size = new System.Drawing.Size(360, 25);
            this.cbMultiPointsMode.TabIndex = 15;
            this.cbMultiPointsMode.SelectedIndexChanged += new System.EventHandler(this.cbMultiPointsMode_SelectedIndexChanged);
            // 
            // lbPattern
            // 
            this.lbPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPattern.Location = new System.Drawing.Point(3, 3);
            this.lbPattern.Name = "lbPattern";
            this.lbPattern.Palette = this.kp1;
            this.lbPattern.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbPattern.Size = new System.Drawing.Size(150, 24);
            this.lbPattern.TabIndex = 16;
            this.lbPattern.Values.Text = "Pattern";
            // 
            // patternSlash
            // 
            this.patternSlash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSlash.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.patternSlash.Location = new System.Drawing.Point(0, 0);
            this.patternSlash.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.patternSlash.Name = "patternSlash";
            this.patternSlash.Size = new System.Drawing.Size(522, 270);
            this.patternSlash.TabIndex = 221;
            // 
            // patternVertical
            // 
            this.patternVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternVertical.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.patternVertical.Location = new System.Drawing.Point(0, 0);
            this.patternVertical.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.patternVertical.Name = "patternVertical";
            this.patternVertical.Size = new System.Drawing.Size(522, 270);
            this.patternVertical.TabIndex = 220;
            // 
            // tpPatternList
            // 
            this.tpPatternList.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpPatternList.Controls.Add(this.tableLayoutPanel1);
            this.tpPatternList.Controls.Add(this.dgvPatterns);
            this.tpPatternList.Flags = 65534;
            this.tpPatternList.LastVisibleSet = true;
            this.tpPatternList.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpPatternList.Name = "tpPatternList";
            this.tpPatternList.Size = new System.Drawing.Size(522, 270);
            this.tpPatternList.Text = "Patten List";
            this.tpPatternList.ToolTipTitle = "Page ToolTip";
            this.tpPatternList.UniqueName = "58BEFE94BE0648D230BDC830A9F48A72";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnClearPatternSet, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDeletePatternSet, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 238);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 32);
            this.tableLayoutPanel1.TabIndex = 205;
            // 
            // btnClearPatternSet
            // 
            this.btnClearPatternSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearPatternSet.Location = new System.Drawing.Point(107, 3);
            this.btnClearPatternSet.Name = "btnClearPatternSet";
            this.btnClearPatternSet.Size = new System.Drawing.Size(98, 26);
            this.btnClearPatternSet.TabIndex = 1;
            this.btnClearPatternSet.Values.Text = "Clear";
            // 
            // btnDeletePatternSet
            // 
            this.btnDeletePatternSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePatternSet.Location = new System.Drawing.Point(3, 3);
            this.btnDeletePatternSet.Name = "btnDeletePatternSet";
            this.btnDeletePatternSet.Size = new System.Drawing.Size(98, 26);
            this.btnDeletePatternSet.TabIndex = 0;
            this.btnDeletePatternSet.Values.Text = "Delete";
            // 
            // dgvPatterns
            // 
            this.dgvPatterns.AllowUserToAddRows = false;
            this.dgvPatterns.AllowUserToDeleteRows = false;
            this.dgvPatterns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatterns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvhPatternIndex,
            this.Identifier,
            this.dgvhPatternName,
            this.dgvhPatternPointsCount,
            this.dgvhPatternSelected});
            this.dgvPatterns.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPatterns.Location = new System.Drawing.Point(0, 0);
            this.dgvPatterns.Name = "dgvPatterns";
            this.dgvPatterns.Palette = this.kp1;
            this.dgvPatterns.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.dgvPatterns.RowHeadersWidth = 30;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPatterns.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPatterns.RowTemplate.Height = 23;
            this.dgvPatterns.Size = new System.Drawing.Size(522, 235);
            this.dgvPatterns.TabIndex = 204;
            this.dgvPatterns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatterns_CellContentClick);
            // 
            // dgvhPatternIndex
            // 
            this.dgvhPatternIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhPatternIndex.DataPropertyName = "Index";
            this.dgvhPatternIndex.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvhPatternIndex.HeaderText = "#";
            this.dgvhPatternIndex.MinimumWidth = 6;
            this.dgvhPatternIndex.Name = "dgvhPatternIndex";
            this.dgvhPatternIndex.ReadOnly = true;
            this.dgvhPatternIndex.Width = 51;
            // 
            // Identifier
            // 
            this.Identifier.DataPropertyName = "Identifier";
            this.Identifier.DefaultCellStyle = dataGridViewCellStyle2;
            this.Identifier.HeaderText = "Column1";
            this.Identifier.MinimumWidth = 6;
            this.Identifier.Name = "Identifier";
            this.Identifier.Visible = false;
            this.Identifier.Width = 125;
            // 
            // dgvhPatternName
            // 
            this.dgvhPatternName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvhPatternName.DataPropertyName = "PatternName";
            this.dgvhPatternName.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvhPatternName.HeaderText = "Pattern Name";
            this.dgvhPatternName.MinimumWidth = 6;
            this.dgvhPatternName.Name = "dgvhPatternName";
            this.dgvhPatternName.ReadOnly = true;
            this.dgvhPatternName.Width = 147;
            // 
            // dgvhPatternPointsCount
            // 
            this.dgvhPatternPointsCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvhPatternPointsCount.DataPropertyName = "PointCount";
            this.dgvhPatternPointsCount.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvhPatternPointsCount.HeaderText = "Point Count";
            this.dgvhPatternPointsCount.MinimumWidth = 6;
            this.dgvhPatternPointsCount.Name = "dgvhPatternPointsCount";
            this.dgvhPatternPointsCount.ReadOnly = true;
            this.dgvhPatternPointsCount.Width = 147;
            // 
            // dgvhPatternSelected
            // 
            this.dgvhPatternSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvhPatternSelected.DataPropertyName = "Checked";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.NullValue = false;
            this.dgvhPatternSelected.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvhPatternSelected.FalseValue = false;
            this.dgvhPatternSelected.HeaderText = "Checked";
            this.dgvhPatternSelected.IndeterminateValue = false;
            this.dgvhPatternSelected.MinimumWidth = 6;
            this.dgvhPatternSelected.Name = "dgvhPatternSelected";
            this.dgvhPatternSelected.TrueValue = true;
            // 
            // tpStatistics
            // 
            this.tpStatistics.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpStatistics.Controls.Add(this.lbCPValue);
            this.tpStatistics.Controls.Add(this.lbNumber);
            this.tpStatistics.Controls.Add(this.lbCP);
            this.tpStatistics.Controls.Add(this.lbMax);
            this.tpStatistics.Controls.Add(this.lbCPKValue);
            this.tpStatistics.Controls.Add(this.lbMin);
            this.tpStatistics.Controls.Add(this.lbCPK);
            this.tpStatistics.Controls.Add(this.lbAvg);
            this.tpStatistics.Controls.Add(this.lbVarianceValue);
            this.tpStatistics.Controls.Add(this.lbStdDev);
            this.tpStatistics.Controls.Add(this.lbVariance);
            this.tpStatistics.Controls.Add(this.lbNumberValue);
            this.tpStatistics.Controls.Add(this.lbMaxValue);
            this.tpStatistics.Controls.Add(this.lbStdDevValue);
            this.tpStatistics.Controls.Add(this.lbMinValue);
            this.tpStatistics.Controls.Add(this.lbAvgValue);
            this.tpStatistics.Flags = 65534;
            this.tpStatistics.LastVisibleSet = true;
            this.tpStatistics.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpStatistics.Name = "tpStatistics";
            this.tpStatistics.Size = new System.Drawing.Size(522, 270);
            this.tpStatistics.Text = "Statistics";
            this.tpStatistics.ToolTipTitle = "Page ToolTip";
            this.tpStatistics.UniqueName = "A8DF5AC9154F42C32B807251A97C7995";
            // 
            // lbCPValue
            // 
            this.lbCPValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbCPValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCPValue.Location = new System.Drawing.Point(285, 177);
            this.lbCPValue.Name = "lbCPValue";
            this.lbCPValue.Palette = this.kp1;
            this.lbCPValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCPValue.Size = new System.Drawing.Size(48, 24);
            this.lbCPValue.TabIndex = 165;
            // 
            // lbNumber
            // 
            this.lbNumber.BackColor = System.Drawing.SystemColors.Control;
            this.lbNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbNumber.Location = new System.Drawing.Point(43, 54);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Palette = this.kp1;
            this.lbNumber.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbNumber.Size = new System.Drawing.Size(68, 24);
            this.lbNumber.TabIndex = 150;
            this.lbNumber.Values.Text = "Number";
            // 
            // lbCP
            // 
            this.lbCP.BackColor = System.Drawing.SystemColors.Control;
            this.lbCP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCP.Location = new System.Drawing.Point(285, 136);
            this.lbCP.Name = "lbCP";
            this.lbCP.Palette = this.kp1;
            this.lbCP.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCP.Size = new System.Drawing.Size(30, 24);
            this.lbCP.TabIndex = 164;
            this.lbCP.Values.Text = "CP";
            // 
            // lbMax
            // 
            this.lbMax.BackColor = System.Drawing.SystemColors.Control;
            this.lbMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMax.Location = new System.Drawing.Point(285, 54);
            this.lbMax.Name = "lbMax";
            this.lbMax.Palette = this.kp1;
            this.lbMax.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMax.Size = new System.Drawing.Size(40, 24);
            this.lbMax.TabIndex = 151;
            this.lbMax.Values.Text = "Max";
            // 
            // lbCPKValue
            // 
            this.lbCPKValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbCPKValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCPKValue.Location = new System.Drawing.Point(406, 177);
            this.lbCPKValue.Name = "lbCPKValue";
            this.lbCPKValue.Palette = this.kp1;
            this.lbCPKValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCPKValue.Size = new System.Drawing.Size(48, 24);
            this.lbCPKValue.TabIndex = 163;
            // 
            // lbMin
            // 
            this.lbMin.BackColor = System.Drawing.SystemColors.Control;
            this.lbMin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMin.Location = new System.Drawing.Point(164, 54);
            this.lbMin.Name = "lbMin";
            this.lbMin.Palette = this.kp1;
            this.lbMin.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMin.Size = new System.Drawing.Size(38, 24);
            this.lbMin.TabIndex = 152;
            this.lbMin.Values.Text = "Min";
            // 
            // lbCPK
            // 
            this.lbCPK.BackColor = System.Drawing.SystemColors.Control;
            this.lbCPK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbCPK.Location = new System.Drawing.Point(406, 136);
            this.lbCPK.Name = "lbCPK";
            this.lbCPK.Palette = this.kp1;
            this.lbCPK.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCPK.Size = new System.Drawing.Size(39, 24);
            this.lbCPK.TabIndex = 162;
            this.lbCPK.Values.Text = "CPK";
            // 
            // lbAvg
            // 
            this.lbAvg.BackColor = System.Drawing.SystemColors.Control;
            this.lbAvg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbAvg.Location = new System.Drawing.Point(406, 54);
            this.lbAvg.Name = "lbAvg";
            this.lbAvg.Palette = this.kp1;
            this.lbAvg.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbAvg.Size = new System.Drawing.Size(41, 24);
            this.lbAvg.TabIndex = 153;
            this.lbAvg.Values.Text = "Avg.";
            // 
            // lbVarianceValue
            // 
            this.lbVarianceValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbVarianceValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbVarianceValue.Location = new System.Drawing.Point(43, 177);
            this.lbVarianceValue.Name = "lbVarianceValue";
            this.lbVarianceValue.Palette = this.kp1;
            this.lbVarianceValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbVarianceValue.Size = new System.Drawing.Size(48, 24);
            this.lbVarianceValue.TabIndex = 161;
            // 
            // lbStdDev
            // 
            this.lbStdDev.BackColor = System.Drawing.SystemColors.Control;
            this.lbStdDev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbStdDev.Location = new System.Drawing.Point(164, 136);
            this.lbStdDev.Name = "lbStdDev";
            this.lbStdDev.Palette = this.kp1;
            this.lbStdDev.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbStdDev.Size = new System.Drawing.Size(71, 24);
            this.lbStdDev.TabIndex = 154;
            this.lbStdDev.Values.Text = "Std. Dev.";
            // 
            // lbVariance
            // 
            this.lbVariance.BackColor = System.Drawing.SystemColors.Control;
            this.lbVariance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbVariance.Location = new System.Drawing.Point(43, 136);
            this.lbVariance.Name = "lbVariance";
            this.lbVariance.Palette = this.kp1;
            this.lbVariance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbVariance.Size = new System.Drawing.Size(70, 24);
            this.lbVariance.TabIndex = 160;
            this.lbVariance.Values.Text = "Variance";
            // 
            // lbNumberValue
            // 
            this.lbNumberValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbNumberValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbNumberValue.Location = new System.Drawing.Point(43, 95);
            this.lbNumberValue.Name = "lbNumberValue";
            this.lbNumberValue.Palette = this.kp1;
            this.lbNumberValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbNumberValue.Size = new System.Drawing.Size(48, 24);
            this.lbNumberValue.TabIndex = 155;
            // 
            // lbMaxValue
            // 
            this.lbMaxValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbMaxValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMaxValue.Location = new System.Drawing.Point(285, 95);
            this.lbMaxValue.Name = "lbMaxValue";
            this.lbMaxValue.Palette = this.kp1;
            this.lbMaxValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMaxValue.Size = new System.Drawing.Size(48, 24);
            this.lbMaxValue.TabIndex = 156;
            // 
            // lbStdDevValue
            // 
            this.lbStdDevValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbStdDevValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbStdDevValue.Location = new System.Drawing.Point(164, 177);
            this.lbStdDevValue.Name = "lbStdDevValue";
            this.lbStdDevValue.Palette = this.kp1;
            this.lbStdDevValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbStdDevValue.Size = new System.Drawing.Size(48, 24);
            this.lbStdDevValue.TabIndex = 159;
            // 
            // lbMinValue
            // 
            this.lbMinValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbMinValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbMinValue.Location = new System.Drawing.Point(164, 95);
            this.lbMinValue.Name = "lbMinValue";
            this.lbMinValue.Palette = this.kp1;
            this.lbMinValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMinValue.Size = new System.Drawing.Size(48, 24);
            this.lbMinValue.TabIndex = 157;
            // 
            // lbAvgValue
            // 
            this.lbAvgValue.BackColor = System.Drawing.SystemColors.Control;
            this.lbAvgValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbAvgValue.Location = new System.Drawing.Point(406, 95);
            this.lbAvgValue.Name = "lbAvgValue";
            this.lbAvgValue.Palette = this.kp1;
            this.lbAvgValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbAvgValue.Size = new System.Drawing.Size(48, 24);
            this.lbAvgValue.TabIndex = 158;
            // 
            // tpAlbum
            // 
            this.tpAlbum.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tpAlbum.Controls.Add(this.measureRecordFlowLayout);
            this.tpAlbum.Flags = 65534;
            this.tpAlbum.LastVisibleSet = true;
            this.tpAlbum.MinimumSize = new System.Drawing.Size(50, 50);
            this.tpAlbum.Name = "tpAlbum";
            this.tpAlbum.Size = new System.Drawing.Size(522, 270);
            this.tpAlbum.Text = "Album";
            this.tpAlbum.ToolTipTitle = "Page ToolTip";
            this.tpAlbum.UniqueName = "E28072931E8F42D2519DAC46F1415560";
            // 
            // measureRecordFlowLayout
            // 
            this.measureRecordFlowLayout.AutoScroll = true;
            this.measureRecordFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.measureRecordFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.measureRecordFlowLayout.Name = "measureRecordFlowLayout";
            this.measureRecordFlowLayout.Size = new System.Drawing.Size(522, 270);
            this.measureRecordFlowLayout.TabIndex = 0;
            this.measureRecordFlowLayout.WrapContents = false;
            this.measureRecordFlowLayout.OnRecordButtonClick += new System.EventHandler(this.measureRecordFlowLayout_OnRecordButtonClick);
            // 
            // lbConvertValue
            // 
            this.lbConvertValue.BackColor = System.Drawing.Color.Gainsboro;
            this.lbConvertValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbConvertValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbConvertValue.Location = new System.Drawing.Point(270, 6);
            this.lbConvertValue.Name = "lbConvertValue";
            this.lbConvertValue.Palette = this.kp1;
            this.lbConvertValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbConvertValue.Size = new System.Drawing.Size(48, 24);
            this.lbConvertValue.TabIndex = 164;
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(325, 288);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Palette = this.kp1;
            this.btnStatistics.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnStatistics.Size = new System.Drawing.Size(100, 27);
            this.btnStatistics.TabIndex = 211;
            this.btnStatistics.Values.Text = "Statistics";
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
            // 
            // lbHardnessType
            // 
            this.lbHardnessType.BackColor = System.Drawing.SystemColors.Control;
            this.lbHardnessType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbHardnessType.Location = new System.Drawing.Point(7, 6);
            this.lbHardnessType.Name = "lbHardnessType";
            this.lbHardnessType.Palette = this.kp1;
            this.lbHardnessType.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHardnessType.Size = new System.Drawing.Size(32, 24);
            this.lbHardnessType.TabIndex = 168;
            this.lbHardnessType.Values.Text = "HV";
            // 
            // lbHardnessValue
            // 
            this.lbHardnessValue.BackColor = System.Drawing.Color.LightGray;
            this.lbHardnessValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHardnessValue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbHardnessValue.Location = new System.Drawing.Point(93, 6);
            this.lbHardnessValue.Name = "lbHardnessValue";
            this.lbHardnessValue.Palette = this.kp1;
            this.lbHardnessValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbHardnessValue.Size = new System.Drawing.Size(48, 24);
            this.lbHardnessValue.TabIndex = 169;
            // 
            // lbMicrometerValue
            // 
            this.lbMicrometerValue.BackColor = System.Drawing.Color.White;
            this.lbMicrometerValue.Location = new System.Drawing.Point(457, 7);
            this.lbMicrometerValue.Name = "lbMicrometerValue";
            this.lbMicrometerValue.Palette = this.kp1;
            this.lbMicrometerValue.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMicrometerValue.Size = new System.Drawing.Size(20, 24);
            this.lbMicrometerValue.TabIndex = 209;
            this.lbMicrometerValue.Values.Text = "0";
            this.lbMicrometerValue.Visible = false;
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvhRecordIndex,
            this.dgvhRecordXPos,
            this.dgvhRecordYPos,
            this.dgvhRecordMoveTo,
            this.dgvhRecordHardness,
            this.dgvhRecordHardnessType,
            this.dgvhRecordQualified,
            this.dgvhRecordD1,
            this.dgvhRecordD2,
            this.dgvhRecordDavg,
            this.dgvhRecordConvertType,
            this.dgvhRecordConvertValue,
            this.dgvhRecordDepth,
            this.dgvhRecordMeasureTime,
            this.dgvhOriginalImagePath,
            this.dgvhRecordImagePath});
            this.dgvRecords.Location = new System.Drawing.Point(7, 44);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.Palette = this.kp1;
            this.dgvRecords.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.dgvRecords.RowHeadersWidth = 30;
            this.dgvRecords.Size = new System.Drawing.Size(524, 238);
            this.dgvRecords.TabIndex = 203;
            this.dgvRecords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllRecords_CellContentClick);
            this.dgvRecords.SelectionChanged += new System.EventHandler(this.dgvRecords_SelectionChanged);
            // 
            // dgvhRecordIndex
            // 
            this.dgvhRecordIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordIndex.DataPropertyName = "Index";
            this.dgvhRecordIndex.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvhRecordIndex.HeaderText = "#";
            this.dgvhRecordIndex.MinimumWidth = 6;
            this.dgvhRecordIndex.Name = "dgvhRecordIndex";
            this.dgvhRecordIndex.ReadOnly = true;
            this.dgvhRecordIndex.Width = 51;
            // 
            // dgvhRecordXPos
            // 
            this.dgvhRecordXPos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordXPos.DataPropertyName = "XPos";
            dataGridViewCellStyle8.Format = "N4";
            dataGridViewCellStyle8.NullValue = null;
            this.dgvhRecordXPos.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvhRecordXPos.HeaderText = "X(mm)";
            this.dgvhRecordXPos.MinimumWidth = 6;
            this.dgvhRecordXPos.Name = "dgvhRecordXPos";
            this.dgvhRecordXPos.ReadOnly = true;
            this.dgvhRecordXPos.Width = 87;
            // 
            // dgvhRecordYPos
            // 
            this.dgvhRecordYPos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordYPos.DataPropertyName = "YPos";
            dataGridViewCellStyle9.Format = "N4";
            dataGridViewCellStyle9.NullValue = null;
            this.dgvhRecordYPos.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvhRecordYPos.HeaderText = "Y(mm)";
            this.dgvhRecordYPos.MinimumWidth = 6;
            this.dgvhRecordYPos.Name = "dgvhRecordYPos";
            this.dgvhRecordYPos.ReadOnly = true;
            this.dgvhRecordYPos.Width = 86;
            // 
            // dgvhRecordMoveTo
            // 
            this.dgvhRecordMoveTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordMoveTo.DataPropertyName = "ButtonText";
            this.dgvhRecordMoveTo.HeaderText = "Move To";
            this.dgvhRecordMoveTo.MinimumWidth = 6;
            this.dgvhRecordMoveTo.Name = "dgvhRecordMoveTo";
            this.dgvhRecordMoveTo.ReadOnly = true;
            this.dgvhRecordMoveTo.Text = "";
            this.dgvhRecordMoveTo.Visible = false;
            this.dgvhRecordMoveTo.Width = 76;
            // 
            // dgvhRecordHardness
            // 
            this.dgvhRecordHardness.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordHardness.DataPropertyName = "Hardness";
            dataGridViewCellStyle10.Format = "N1";
            dataGridViewCellStyle10.NullValue = null;
            this.dgvhRecordHardness.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvhRecordHardness.HeaderText = "Hardness";
            this.dgvhRecordHardness.MinimumWidth = 6;
            this.dgvhRecordHardness.Name = "dgvhRecordHardness";
            this.dgvhRecordHardness.ReadOnly = true;
            this.dgvhRecordHardness.Width = 103;
            // 
            // dgvhRecordHardnessType
            // 
            this.dgvhRecordHardnessType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordHardnessType.DataPropertyName = "HardnessType";
            this.dgvhRecordHardnessType.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvhRecordHardnessType.HeaderText = "Hardness Type";
            this.dgvhRecordHardnessType.MinimumWidth = 6;
            this.dgvhRecordHardnessType.Name = "dgvhRecordHardnessType";
            this.dgvhRecordHardnessType.ReadOnly = true;
            this.dgvhRecordHardnessType.Width = 138;
            // 
            // dgvhRecordQualified
            // 
            this.dgvhRecordQualified.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordQualified.DataPropertyName = "Qualified";
            this.dgvhRecordQualified.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvhRecordQualified.HeaderText = "Qualified";
            this.dgvhRecordQualified.MinimumWidth = 6;
            this.dgvhRecordQualified.Name = "dgvhRecordQualified";
            this.dgvhRecordQualified.ReadOnly = true;
            this.dgvhRecordQualified.Width = 103;
            // 
            // dgvhRecordD1
            // 
            this.dgvhRecordD1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordD1.DataPropertyName = "D1";
            dataGridViewCellStyle13.Format = "N3";
            dataGridViewCellStyle13.NullValue = null;
            this.dgvhRecordD1.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvhRecordD1.HeaderText = "D1(um)";
            this.dgvhRecordD1.MinimumWidth = 6;
            this.dgvhRecordD1.Name = "dgvhRecordD1";
            this.dgvhRecordD1.ReadOnly = true;
            this.dgvhRecordD1.Width = 92;
            // 
            // dgvhRecordD2
            // 
            this.dgvhRecordD2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordD2.DataPropertyName = "D2";
            dataGridViewCellStyle14.Format = "N3";
            this.dgvhRecordD2.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvhRecordD2.HeaderText = "D2(um)";
            this.dgvhRecordD2.MinimumWidth = 6;
            this.dgvhRecordD2.Name = "dgvhRecordD2";
            this.dgvhRecordD2.ReadOnly = true;
            this.dgvhRecordD2.Width = 92;
            // 
            // dgvhRecordDavg
            // 
            this.dgvhRecordDavg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordDavg.DataPropertyName = "DAvg";
            dataGridViewCellStyle15.Format = "N3";
            this.dgvhRecordDavg.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvhRecordDavg.HeaderText = "Davg(um)";
            this.dgvhRecordDavg.MinimumWidth = 6;
            this.dgvhRecordDavg.Name = "dgvhRecordDavg";
            this.dgvhRecordDavg.ReadOnly = true;
            this.dgvhRecordDavg.Width = 108;
            // 
            // dgvhRecordConvertType
            // 
            this.dgvhRecordConvertType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordConvertType.DataPropertyName = "ConvertType";
            this.dgvhRecordConvertType.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvhRecordConvertType.HeaderText = "Convert Type";
            this.dgvhRecordConvertType.MinimumWidth = 6;
            this.dgvhRecordConvertType.Name = "dgvhRecordConvertType";
            this.dgvhRecordConvertType.ReadOnly = true;
            this.dgvhRecordConvertType.Width = 128;
            // 
            // dgvhRecordConvertValue
            // 
            this.dgvhRecordConvertValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordConvertValue.DataPropertyName = "ConvertValue";
            dataGridViewCellStyle17.Format = "N1";
            dataGridViewCellStyle17.NullValue = null;
            this.dgvhRecordConvertValue.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvhRecordConvertValue.HeaderText = "Convert Value";
            this.dgvhRecordConvertValue.MinimumWidth = 6;
            this.dgvhRecordConvertValue.Name = "dgvhRecordConvertValue";
            this.dgvhRecordConvertValue.ReadOnly = true;
            this.dgvhRecordConvertValue.Width = 133;
            // 
            // dgvhRecordDepth
            // 
            this.dgvhRecordDepth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordDepth.DataPropertyName = "Depth";
            dataGridViewCellStyle18.Format = "N3";
            dataGridViewCellStyle18.NullValue = null;
            this.dgvhRecordDepth.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvhRecordDepth.HeaderText = "Depth";
            this.dgvhRecordDepth.MinimumWidth = 6;
            this.dgvhRecordDepth.Name = "dgvhRecordDepth";
            this.dgvhRecordDepth.Width = 83;
            // 
            // dgvhRecordMeasureTime
            // 
            this.dgvhRecordMeasureTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvhRecordMeasureTime.DataPropertyName = "MeasureTime";
            dataGridViewCellStyle19.Format = "T";
            dataGridViewCellStyle19.NullValue = null;
            this.dgvhRecordMeasureTime.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvhRecordMeasureTime.HeaderText = "Measure Time";
            this.dgvhRecordMeasureTime.MinimumWidth = 6;
            this.dgvhRecordMeasureTime.Name = "dgvhRecordMeasureTime";
            this.dgvhRecordMeasureTime.ReadOnly = true;
            this.dgvhRecordMeasureTime.Width = 135;
            // 
            // dgvhOriginalImagePath
            // 
            this.dgvhOriginalImagePath.DataPropertyName = "OriginalImagePath";
            this.dgvhOriginalImagePath.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvhOriginalImagePath.HeaderText = "OriginalImagePath";
            this.dgvhOriginalImagePath.MinimumWidth = 6;
            this.dgvhOriginalImagePath.Name = "dgvhOriginalImagePath";
            this.dgvhOriginalImagePath.Visible = false;
            this.dgvhOriginalImagePath.Width = 125;
            // 
            // dgvhRecordImagePath
            // 
            this.dgvhRecordImagePath.DataPropertyName = "MeasuredImagePath";
            this.dgvhRecordImagePath.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgvhRecordImagePath.HeaderText = "MeasuredImagePath";
            this.dgvhRecordImagePath.MinimumWidth = 6;
            this.dgvhRecordImagePath.Name = "dgvhRecordImagePath";
            this.dgvhRecordImagePath.Visible = false;
            this.dgvhRecordImagePath.Width = 125;
            // 
            // btnExportReport
            // 
            this.btnExportReport.Location = new System.Drawing.Point(431, 288);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Palette = this.kp1;
            this.btnExportReport.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnExportReport.Size = new System.Drawing.Size(100, 27);
            this.btnExportReport.TabIndex = 198;
            this.btnExportReport.Values.Text = "Export";
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // btnClearRecord
            // 
            this.btnClearRecord.Location = new System.Drawing.Point(219, 288);
            this.btnClearRecord.Name = "btnClearRecord";
            this.btnClearRecord.Palette = this.kp1;
            this.btnClearRecord.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnClearRecord.Size = new System.Drawing.Size(100, 27);
            this.btnClearRecord.TabIndex = 197;
            this.btnClearRecord.Values.Text = "Clear";
            this.btnClearRecord.Click += new System.EventHandler(this.btnClearRecord_Click);
            // 
            // btnDeleteRecord
            // 
            this.btnDeleteRecord.Location = new System.Drawing.Point(113, 288);
            this.btnDeleteRecord.Name = "btnDeleteRecord";
            this.btnDeleteRecord.Palette = this.kp1;
            this.btnDeleteRecord.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnDeleteRecord.Size = new System.Drawing.Size(100, 27);
            this.btnDeleteRecord.TabIndex = 196;
            this.btnDeleteRecord.Values.Text = "Delte";
            this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            // 
            // btnEditRecord
            // 
            this.btnEditRecord.Location = new System.Drawing.Point(7, 288);
            this.btnEditRecord.Name = "btnEditRecord";
            this.btnEditRecord.Palette = this.kp1;
            this.btnEditRecord.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnEditRecord.Size = new System.Drawing.Size(100, 27);
            this.btnEditRecord.TabIndex = 195;
            this.btnEditRecord.Values.Text = "Edit";
            this.btnEditRecord.Click += new System.EventHandler(this.btnEditRecord_Click);
            // 
            // cbConvertType
            // 
            this.cbConvertType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbConvertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConvertType.DropDownWidth = 80;
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
            this.cbConvertType.Location = new System.Drawing.Point(184, 9);
            this.cbConvertType.Name = "cbConvertType";
            this.cbConvertType.Palette = this.kp1;
            this.cbConvertType.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbConvertType.Size = new System.Drawing.Size(80, 25);
            this.cbConvertType.TabIndex = 193;
            this.cbConvertType.SelectedIndexChanged += new System.EventHandler(this.cbConvertType_SelectedIndexChanged);
            // 
            // kribbon_Main
            // 
            this.kribbon_Main.AllowMinimizedChange = false;
            this.kribbon_Main.HideRibbonSize = new System.Drawing.Size(300, 100);
            this.kribbon_Main.InDesignHelperMode = false;
            this.kribbon_Main.MinimizedMode = true;
            this.kribbon_Main.Name = "kribbon_Main";
            this.kribbon_Main.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.kribbon_Main.QATButtons.AddRange(new System.ComponentModel.Component[] {
            this.kOATBtn_Open,
            this.kOATBtn_Save,
            this.kOATBtn_Play,
            this.kOATBtn_Pause,
            this.kOATBtn_AutoMeasure,
            this.kOATBtn_ManualMeasure,
            this.kOATBtn_Cursor,
            this.kOATBtn_Angle,
            this.kOATBtn_Clear,
            this.kOATBtn_ZoomIn,
            this.kOATBtn_ZoomOut,
            this.kOATBtn_CrossLine});
            this.kribbon_Main.QATUserChange = false;
            this.kribbon_Main.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.kribbon_Main.RibbonAppButton.AppButtonVisible = false;
            this.kribbon_Main.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.krtab_File,
            this.krtab_Device,
            this.krtab_Data,
            this.krtab_Tools,
            this.krtab_Config,
            this.kryptonRibbonTab1});
            this.kribbon_Main.SelectedContext = null;
            this.kribbon_Main.SelectedTab = this.krtab_Device;
            this.kribbon_Main.ShowMinimizeButton = false;
            this.kribbon_Main.Size = new System.Drawing.Size(1339, 165);
            this.kribbon_Main.TabIndex = 233;
            // 
            // kOATBtn_Open
            // 
            this.kOATBtn_Open.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Open.Image")));
            this.kOATBtn_Open.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Open.ToolTipTitle = "Open";
            this.kOATBtn_Open.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // kOATBtn_Save
            // 
            this.kOATBtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Save.Image")));
            this.kOATBtn_Save.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Save.ToolTipTitle = "Save";
            this.kOATBtn_Save.Click += new System.EventHandler(this.tsmiSaveImage_Click);
            // 
            // kOATBtn_Play
            // 
            this.kOATBtn_Play.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Play.Image")));
            this.kOATBtn_Play.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Play.ToolTipTitle = "Camera Start";
            this.kOATBtn_Play.Click += new System.EventHandler(this.tsmiCameraStart_Click);
            // 
            // kOATBtn_Pause
            // 
            this.kOATBtn_Pause.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Pause.Image")));
            this.kOATBtn_Pause.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Pause.ToolTipTitle = "Camera Pause";
            this.kOATBtn_Pause.Click += new System.EventHandler(this.tsmiCameraStop_Click);
            // 
            // kOATBtn_AutoMeasure
            // 
            this.kOATBtn_AutoMeasure.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_AutoMeasure.Image")));
            this.kOATBtn_AutoMeasure.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_AutoMeasure.ToolTipTitle = "Auto Measure";
            this.kOATBtn_AutoMeasure.Click += new System.EventHandler(this.tsmiAutoMeasure_Click);
            // 
            // kOATBtn_ManualMeasure
            // 
            this.kOATBtn_ManualMeasure.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_ManualMeasure.Image")));
            this.kOATBtn_ManualMeasure.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_ManualMeasure.ToolTipTitle = "Manual Measure";
            this.kOATBtn_ManualMeasure.Click += new System.EventHandler(this.tsmiManualMeasure_Click);
            // 
            // kOATBtn_Cursor
            // 
            this.kOATBtn_Cursor.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Cursor.Image")));
            this.kOATBtn_Cursor.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Cursor.ToolTipTitle = "Pointer";
            this.kOATBtn_Cursor.Click += new System.EventHandler(this.tsmiPointer_Click);
            // 
            // kOATBtn_Angle
            // 
            this.kOATBtn_Angle.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Angle.Image")));
            this.kOATBtn_Angle.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Angle.ToolTipTitle = "Angle";
            this.kOATBtn_Angle.Click += new System.EventHandler(this.tsmiMeasureAngle_Click);
            // 
            // kOATBtn_Clear
            // 
            this.kOATBtn_Clear.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_Clear.Image")));
            this.kOATBtn_Clear.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_Clear.ToolTipTitle = "Clear Graphics";
            this.kOATBtn_Clear.Click += new System.EventHandler(this.tsmiClearGraphics_Click);
            // 
            // kOATBtn_ZoomIn
            // 
            this.kOATBtn_ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_ZoomIn.Image")));
            this.kOATBtn_ZoomIn.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_ZoomIn.ToolTipTitle = "Zoom In";
            this.kOATBtn_ZoomIn.Click += new System.EventHandler(this.tsmiMagnifier_Click);
            // 
            // kOATBtn_ZoomOut
            // 
            this.kOATBtn_ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_ZoomOut.Image")));
            this.kOATBtn_ZoomOut.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_ZoomOut.ToolTipTitle = "Zoom Out";
            this.kOATBtn_ZoomOut.Click += new System.EventHandler(this.tsmiResumeImage_Click);
            // 
            // kOATBtn_CrossLine
            // 
            this.kOATBtn_CrossLine.Image = ((System.Drawing.Image)(resources.GetObject("kOATBtn_CrossLine.Image")));
            this.kOATBtn_CrossLine.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.kOATBtn_CrossLine.ToolTipTitle = "Cross Line";
            this.kOATBtn_CrossLine.Click += new System.EventHandler(this.tsmiCenterCrossLine_Click);
            // 
            // krtab_File
            // 
            this.krtab_File.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.krgrp_File});
            this.krtab_File.Text = "File";
            // 
            // krgrp_File
            // 
            this.krgrp_File.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.krbntrip_File,
            this.krbntrip_Exit});
            // 
            // krbntrip_File
            // 
            this.krbntrip_File.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiOpenImage,
            this.tsmiSaveImage,
            this.tsmiSaveOriginalImage});
            // 
            // tsmiOpenImage
            // 
            this.tsmiOpenImage.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiOpenImage.ImageLarge")));
            this.tsmiOpenImage.TextLine1 = "Open";
            this.tsmiOpenImage.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiSaveImage
            // 
            this.tsmiSaveImage.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiSaveImage.ImageLarge")));
            this.tsmiSaveImage.TextLine1 = "Save";
            this.tsmiSaveImage.Click += new System.EventHandler(this.tsmiSaveImage_Click);
            // 
            // tsmiSaveOriginalImage
            // 
            this.tsmiSaveOriginalImage.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiSaveOriginalImage.ImageLarge")));
            this.tsmiSaveOriginalImage.TextLine1 = "Save Original";
            this.tsmiSaveOriginalImage.Click += new System.EventHandler(this.tsmiSaveOriginalImage_Click);
            // 
            // krbntrip_Exit
            // 
            this.krbntrip_Exit.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiExit});
            // 
            // tsmiExit
            // 
            this.tsmiExit.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiExit.ImageLarge")));
            this.tsmiExit.TextLine1 = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // krtab_Device
            // 
            this.krtab_Device.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup1});
            this.krtab_Device.Text = "Device";
            // 
            // kryptonRibbonGroup1
            // 
            this.kryptonRibbonGroup1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple1});
            // 
            // kryptonRibbonGroupTriple1
            // 
            this.kryptonRibbonGroupTriple1.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiCameraStart,
            this.tsmiCameraStop});
            // 
            // tsmiCameraStart
            // 
            this.tsmiCameraStart.TextLine1 = "Open";
            this.tsmiCameraStart.TextLine2 = "Camera";
            this.tsmiCameraStart.Click += new System.EventHandler(this.tsmiCameraStart_Click);
            // 
            // tsmiCameraStop
            // 
            this.tsmiCameraStop.Enabled = false;
            this.tsmiCameraStop.TextLine1 = "Close";
            this.tsmiCameraStop.TextLine2 = "Camera";
            this.tsmiCameraStop.Click += new System.EventHandler(this.tsmiCameraStop_Click);
            // 
            // krtab_Data
            // 
            this.krtab_Data.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup2});
            this.krtab_Data.Text = "Data";
            // 
            // kryptonRibbonGroup2
            // 
            this.kryptonRibbonGroup2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple2});
            // 
            // kryptonRibbonGroupTriple2
            // 
            this.kryptonRibbonGroupTriple2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiSampleInfo});
            // 
            // tsmiSampleInfo
            // 
            this.tsmiSampleInfo.TextLine1 = "Sample";
            this.tsmiSampleInfo.TextLine2 = "Info";
            this.tsmiSampleInfo.Click += new System.EventHandler(this.tsmiSampleInfo_Click);
            // 
            // krtab_Tools
            // 
            this.krtab_Tools.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup3});
            this.krtab_Tools.Text = "Tools";
            // 
            // kryptonRibbonGroup3
            // 
            this.kryptonRibbonGroup3.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple3,
            this.kryptonRibbonGroupTriple4,
            this.kryptonRibbonGroupTriple5,
            this.kryptonRibbonGroupTriple6});
            // 
            // kryptonRibbonGroupTriple3
            // 
            this.kryptonRibbonGroupTriple3.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiAutoMeasure,
            this.tsmiManualMeasure});
            // 
            // tsmiAutoMeasure
            // 
            this.tsmiAutoMeasure.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiAutoMeasure.ImageLarge")));
            this.tsmiAutoMeasure.TextLine1 = "Auto";
            this.tsmiAutoMeasure.TextLine2 = "Measure";
            this.tsmiAutoMeasure.Click += new System.EventHandler(this.tsmiAutoMeasure_Click);
            // 
            // tsmiManualMeasure
            // 
            this.tsmiManualMeasure.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiManualMeasure.ImageLarge")));
            this.tsmiManualMeasure.TextLine1 = "Manual";
            this.tsmiManualMeasure.TextLine2 = "Measure";
            this.tsmiManualMeasure.Click += new System.EventHandler(this.tsmiManualMeasure_Click);
            // 
            // kryptonRibbonGroupTriple4
            // 
            this.kryptonRibbonGroupTriple4.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiPointer,
            this.tsmiMeasureLength,
            this.tsmiMeasureAngle});
            // 
            // tsmiPointer
            // 
            this.tsmiPointer.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiPointer.ImageLarge")));
            this.tsmiPointer.TextLine1 = "Pointer";
            this.tsmiPointer.Click += new System.EventHandler(this.tsmiPointer_Click);
            // 
            // tsmiMeasureLength
            // 
            this.tsmiMeasureLength.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiMeasureLength.ImageLarge")));
            this.tsmiMeasureLength.TextLine1 = "Measure";
            this.tsmiMeasureLength.TextLine2 = "Length";
            this.tsmiMeasureLength.Click += new System.EventHandler(this.tsmiMeasureLength_Click);
            // 
            // tsmiMeasureAngle
            // 
            this.tsmiMeasureAngle.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiMeasureAngle.ImageLarge")));
            this.tsmiMeasureAngle.TextLine1 = "Measure";
            this.tsmiMeasureAngle.TextLine2 = "Angle";
            this.tsmiMeasureAngle.Click += new System.EventHandler(this.tsmiMeasureAngle_Click);
            // 
            // kryptonRibbonGroupTriple5
            // 
            this.kryptonRibbonGroupTriple5.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiMagnifier,
            this.tsmiResumeImage});
            // 
            // tsmiMagnifier
            // 
            this.tsmiMagnifier.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiMagnifier.ImageLarge")));
            this.tsmiMagnifier.TextLine1 = "Magnifier";
            this.tsmiMagnifier.Click += new System.EventHandler(this.tsmiMagnifier_Click);
            // 
            // tsmiResumeImage
            // 
            this.tsmiResumeImage.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiResumeImage.ImageLarge")));
            this.tsmiResumeImage.TextLine1 = "Magnifier";
            this.tsmiResumeImage.Click += new System.EventHandler(this.tsmiResumeImage_Click);
            // 
            // kryptonRibbonGroupTriple6
            // 
            this.kryptonRibbonGroupTriple6.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiClearGraphics,
            this.tsmiTrimMeasure,
            this.tsmiCenterCrossLine});
            // 
            // tsmiClearGraphics
            // 
            this.tsmiClearGraphics.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiClearGraphics.ImageLarge")));
            this.tsmiClearGraphics.TextLine1 = "Clear";
            this.tsmiClearGraphics.TextLine2 = "Graphics";
            this.tsmiClearGraphics.Click += new System.EventHandler(this.tsmiClearGraphics_Click);
            // 
            // tsmiTrimMeasure
            // 
            this.tsmiTrimMeasure.TextLine1 = "Trim";
            this.tsmiTrimMeasure.TextLine2 = "Measure";
            this.tsmiTrimMeasure.Click += new System.EventHandler(this.tsmiTrimMeasure_Click);
            // 
            // tsmiCenterCrossLine
            // 
            this.tsmiCenterCrossLine.ImageLarge = ((System.Drawing.Image)(resources.GetObject("tsmiCenterCrossLine.ImageLarge")));
            this.tsmiCenterCrossLine.TextLine1 = "Center";
            this.tsmiCenterCrossLine.TextLine2 = "Cross Line";
            this.tsmiCenterCrossLine.Click += new System.EventHandler(this.tsmiCenterCrossLine_Click);
            // 
            // krtab_Config
            // 
            this.krtab_Config.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup5});
            this.krtab_Config.Text = "Configuration";
            // 
            // kryptonRibbonGroup5
            // 
            this.kryptonRibbonGroup5.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple8,
            this.kryptonRibbonGroupTriple9,
            this.kryptonRibbonGroupTriple10});
            // 
            // kryptonRibbonGroupTriple8
            // 
            this.kryptonRibbonGroupTriple8.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiCalibration,
            this.tsmiForceCorrect,
            this.tsmiAutoMeasureSetting});
            // 
            // tsmiCalibration
            // 
            this.tsmiCalibration.TextLine1 = "Calibration";
            this.tsmiCalibration.Click += new System.EventHandler(this.tsmiCalibration_Click);
            // 
            // tsmiForceCorrect
            // 
            this.tsmiForceCorrect.TextLine1 = "Force Correct";
            this.tsmiForceCorrect.Click += new System.EventHandler(this.tsmiForceCorrect_Click);
            // 
            // tsmiAutoMeasureSetting
            // 
            this.tsmiAutoMeasureSetting.TextLine1 = "Auto Measure";
            this.tsmiAutoMeasureSetting.TextLine2 = "Setting";
            this.tsmiAutoMeasureSetting.Click += new System.EventHandler(this.tsmiAutoMeasureSetting_Click);
            // 
            // kryptonRibbonGroupTriple9
            // 
            this.kryptonRibbonGroupTriple9.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiCameraSetting,
            this.tsmiSerialPortSetting,
            this.tsmiXYPlatformSetting});
            // 
            // tsmiCameraSetting
            // 
            this.tsmiCameraSetting.TextLine1 = "Camera";
            this.tsmiCameraSetting.TextLine2 = "Setting";
            this.tsmiCameraSetting.Click += new System.EventHandler(this.tsmiCameraSetting_Click);
            // 
            // tsmiSerialPortSetting
            // 
            this.tsmiSerialPortSetting.TextLine1 = "Serial Port";
            this.tsmiSerialPortSetting.TextLine2 = "Setting";
            this.tsmiSerialPortSetting.Click += new System.EventHandler(this.tsmiSerialPortSetting_Click);
            // 
            // tsmiXYPlatformSetting
            // 
            this.tsmiXYPlatformSetting.TextLine1 = "XY Platform";
            this.tsmiXYPlatformSetting.TextLine2 = "Setting";
            this.tsmiXYPlatformSetting.Click += new System.EventHandler(this.tsmiXYPlatformSetting_Click);
            // 
            // kryptonRibbonGroupTriple10
            // 
            this.kryptonRibbonGroupTriple10.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.tsmiZAxisSetting,
            this.tsmiGenericSetting,
            this.tsmiOtherSetting});
            // 
            // tsmiZAxisSetting
            // 
            this.tsmiZAxisSetting.TextLine1 = "Z Axis";
            this.tsmiZAxisSetting.TextLine2 = "Setting";
            this.tsmiZAxisSetting.Click += new System.EventHandler(this.tsmiZAxisSetting_Click);
            // 
            // tsmiGenericSetting
            // 
            this.tsmiGenericSetting.TextLine1 = "Generic";
            this.tsmiGenericSetting.TextLine2 = "Setting";
            this.tsmiGenericSetting.Click += new System.EventHandler(this.tsmiGenericSetting_Click);
            // 
            // tsmiOtherSetting
            // 
            this.tsmiOtherSetting.TextLine1 = "Other";
            this.tsmiOtherSetting.TextLine2 = "Setting";
            this.tsmiOtherSetting.Click += new System.EventHandler(this.tsmiOtherSetting_Click);
            // 
            // kryptonRibbonTab1
            // 
            this.kryptonRibbonTab1.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.kryptonRibbonGroup4});
            this.kryptonRibbonTab1.Text = "Help";
            // 
            // kryptonRibbonGroup4
            // 
            this.kryptonRibbonGroup4.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple7});
            // 
            // kryptonRibbonGroupTriple7
            // 
            this.kryptonRibbonGroupTriple7.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.kryptonRibbonGroupButton6});
            // 
            // kryptonRibbonGroupButton6
            // 
            this.kryptonRibbonGroupButton6.TextLine1 = "About";
            this.kryptonRibbonGroupButton6.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.CaptionVisible = false;
            this.kryptonGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 85);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            this.kryptonGroupBox1.Palette = this.kp1;
            this.kryptonGroupBox1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.splitContainer1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(1339, 634);
            this.kryptonGroupBox1.TabIndex = 235;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 749);
            this.Controls.Add(this.kribbon_Main);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hardness Tester";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcFuncArea)).EndInit();
            this.tcFuncArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tpMachineControl)).EndInit();
            this.tpMachineControl.ResumeLayout(false);
            this.tpMachineControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbHardnessLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbZoomTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpXYZ)).EndInit();
            this.tpXYZ.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis.Panel)).EndInit();
            this.gbZAxis.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbZAxis)).EndInit();
            this.gbZAxis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform.Panel)).EndInit();
            this.gbPlatform.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbPlatform)).EndInit();
            this.gbPlatform.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpMultiPoints)).EndInit();
            this.tpMultiPoints.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbMultiPointsMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpPatternList)).EndInit();
            this.tpPatternList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatterns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpStatistics)).EndInit();
            this.tpStatistics.ResumeLayout(false);
            this.tpStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpAlbum)).EndInit();
            this.tpAlbum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbConvertType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kribbon_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void btnplcrightward2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

     
    }
}
