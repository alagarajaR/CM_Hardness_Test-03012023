using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Labtt.Communication.SYJKPlatform;
using Labtt.Data;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class PlatformSettingForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonTextBox tbForward;

		private KryptonLabel lbForward;

		private KryptonCheckBox cbReverseHorizontalAxis;

		private KryptonCheckBox cbHasEmptyTrip;

		private KryptonCheckBox cbReverseVerticalAxis;

		private KryptonTextBox tbBackward;

		private KryptonLabel lbBackward;

		private KryptonTextBox tbRightward;

		private KryptonLabel lbRightward;

		private KryptonTextBox tbLeftward;

		private KryptonLabel lbLeftward;

		private KryptonTextBox tbSlowFinalSpeed;

		private KryptonLabel lbSlowFinalSpeed;

		private KryptonTextBox tbSlowAcceleration;

		private KryptonLabel lbSlowAcceleration;

		private KryptonTextBox tbSlowBeginSpeed;

		private KryptonLabel lbSlowBeginSpeed;

		private KryptonTextBox tbSlowStepDistance;

		private KryptonLabel lbSlowStepDistance;

		private KryptonGroupBox gbSlow;

		private KryptonGroupBox gbEmptyTrip;

		private KryptonGroupBox gbMedium;

		private KryptonLabel lbMediumStepDistance;

		private KryptonTextBox tbMediumFinalSpeed;

		private KryptonTextBox tbMediumStepDistance;

		private KryptonLabel lbMediumFinalSpeed;

		private KryptonLabel lbMediumBeginSpeed;

		private KryptonTextBox tbMediumAcceleration;

		private KryptonTextBox tbMediumBeginSpeed;

		private KryptonLabel lbMediumAcceleration;

		private KryptonGroupBox gbFast;

		private KryptonLabel lbFastStepDistance;

		private KryptonTextBox tbFastFinalSpeed;

		private KryptonTextBox tbFastStepDistance;

		private KryptonLabel lbFastFinalSpeed;

		private KryptonLabel lbFastBeginSpeed;

		private KryptonTextBox tbFastAcceleration;

		private KryptonTextBox tbFastBeginSpeed;

		private KryptonLabel lbFastAcceleration;

		private KryptonGroupBox gbVeryFast;

		private KryptonLabel lbVeryFastStepDistance;

		private KryptonTextBox tbVeryFastFinalSpeed;

		private KryptonTextBox tbVeryFastStepDistance;

		private KryptonLabel lbVeryFastFinalSpeed;

		private KryptonLabel lbVeryFastBeginSpeed;

		private KryptonTextBox tbVeryFastAcceleration;

		private KryptonTextBox tbVeryFastBeginSpeed;

		private KryptonLabel lbVeryFastAcceleration;

		private KryptonLabel lbPulsePerMM;

		private KryptonTextBox tbPulsePerMM;

		private KryptonButton btnSave;

		private KryptonButton btnCancel;
        private KryptonPalette kp1;
        private SYJKPlatformInfo platformInfo;

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
            this.tbForward = new Krypton.Toolkit.KryptonTextBox();
            this.lbForward = new Krypton.Toolkit.KryptonLabel();
            this.cbReverseHorizontalAxis = new Krypton.Toolkit.KryptonCheckBox();
            this.cbHasEmptyTrip = new Krypton.Toolkit.KryptonCheckBox();
            this.cbReverseVerticalAxis = new Krypton.Toolkit.KryptonCheckBox();
            this.tbBackward = new Krypton.Toolkit.KryptonTextBox();
            this.lbBackward = new Krypton.Toolkit.KryptonLabel();
            this.tbRightward = new Krypton.Toolkit.KryptonTextBox();
            this.lbRightward = new Krypton.Toolkit.KryptonLabel();
            this.tbLeftward = new Krypton.Toolkit.KryptonTextBox();
            this.lbLeftward = new Krypton.Toolkit.KryptonLabel();
            this.tbSlowFinalSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.lbSlowFinalSpeed = new Krypton.Toolkit.KryptonLabel();
            this.tbSlowAcceleration = new Krypton.Toolkit.KryptonTextBox();
            this.lbSlowAcceleration = new Krypton.Toolkit.KryptonLabel();
            this.tbSlowBeginSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.lbSlowBeginSpeed = new Krypton.Toolkit.KryptonLabel();
            this.tbSlowStepDistance = new Krypton.Toolkit.KryptonTextBox();
            this.lbSlowStepDistance = new Krypton.Toolkit.KryptonLabel();
            this.gbSlow = new Krypton.Toolkit.KryptonGroupBox();
            this.gbEmptyTrip = new Krypton.Toolkit.KryptonGroupBox();
            this.gbMedium = new Krypton.Toolkit.KryptonGroupBox();
            this.lbMediumStepDistance = new Krypton.Toolkit.KryptonLabel();
            this.tbMediumFinalSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.tbMediumStepDistance = new Krypton.Toolkit.KryptonTextBox();
            this.lbMediumFinalSpeed = new Krypton.Toolkit.KryptonLabel();
            this.lbMediumBeginSpeed = new Krypton.Toolkit.KryptonLabel();
            this.tbMediumAcceleration = new Krypton.Toolkit.KryptonTextBox();
            this.tbMediumBeginSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.lbMediumAcceleration = new Krypton.Toolkit.KryptonLabel();
            this.gbFast = new Krypton.Toolkit.KryptonGroupBox();
            this.lbFastStepDistance = new Krypton.Toolkit.KryptonLabel();
            this.tbFastFinalSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.tbFastStepDistance = new Krypton.Toolkit.KryptonTextBox();
            this.lbFastFinalSpeed = new Krypton.Toolkit.KryptonLabel();
            this.lbFastBeginSpeed = new Krypton.Toolkit.KryptonLabel();
            this.tbFastAcceleration = new Krypton.Toolkit.KryptonTextBox();
            this.tbFastBeginSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.lbFastAcceleration = new Krypton.Toolkit.KryptonLabel();
            this.gbVeryFast = new Krypton.Toolkit.KryptonGroupBox();
            this.lbVeryFastStepDistance = new Krypton.Toolkit.KryptonLabel();
            this.tbVeryFastFinalSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.tbVeryFastStepDistance = new Krypton.Toolkit.KryptonTextBox();
            this.lbVeryFastFinalSpeed = new Krypton.Toolkit.KryptonLabel();
            this.lbVeryFastBeginSpeed = new Krypton.Toolkit.KryptonLabel();
            this.tbVeryFastAcceleration = new Krypton.Toolkit.KryptonTextBox();
            this.tbVeryFastBeginSpeed = new Krypton.Toolkit.KryptonTextBox();
            this.lbVeryFastAcceleration = new Krypton.Toolkit.KryptonLabel();
            this.lbPulsePerMM = new Krypton.Toolkit.KryptonLabel();
            this.tbPulsePerMM = new Krypton.Toolkit.KryptonTextBox();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gbSlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbSlow.Panel)).BeginInit();
            this.gbSlow.Panel.SuspendLayout();
            this.gbSlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbEmptyTrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbEmptyTrip.Panel)).BeginInit();
            this.gbEmptyTrip.Panel.SuspendLayout();
            this.gbEmptyTrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbMedium)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMedium.Panel)).BeginInit();
            this.gbMedium.Panel.SuspendLayout();
            this.gbMedium.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbFast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbFast.Panel)).BeginInit();
            this.gbFast.Panel.SuspendLayout();
            this.gbFast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbVeryFast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbVeryFast.Panel)).BeginInit();
            this.gbVeryFast.Panel.SuspendLayout();
            this.gbVeryFast.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbForward
            // 
            this.tbForward.Location = new System.Drawing.Point(200, 6);
            this.tbForward.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbForward.Name = "tbForward";
            this.tbForward.Palette = this.kp1;
            this.tbForward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbForward.Size = new System.Drawing.Size(72, 23);
            this.tbForward.TabIndex = 15;
            // 
            // lbForward
            // 
            this.lbForward.Location = new System.Drawing.Point(4, 9);
            this.lbForward.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbForward.Name = "lbForward";
            this.lbForward.Palette = this.kp1;
            this.lbForward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbForward.Size = new System.Drawing.Size(55, 20);
            this.lbForward.TabIndex = 14;
            this.lbForward.Values.Text = "Forward";
            // 
            // cbReverseHorizontalAxis
            // 
            this.cbReverseHorizontalAxis.Location = new System.Drawing.Point(40, 71);
            this.cbReverseHorizontalAxis.Margin = new System.Windows.Forms.Padding(2);
            this.cbReverseHorizontalAxis.Name = "cbReverseHorizontalAxis";
            this.cbReverseHorizontalAxis.Palette = this.kp1;
            this.cbReverseHorizontalAxis.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbReverseHorizontalAxis.Size = new System.Drawing.Size(98, 20);
            this.cbReverseHorizontalAxis.TabIndex = 18;
            this.cbReverseHorizontalAxis.Values.Text = "Reverse XAxis";
            // 
            // cbHasEmptyTrip
            // 
            this.cbHasEmptyTrip.Location = new System.Drawing.Point(40, 39);
            this.cbHasEmptyTrip.Margin = new System.Windows.Forms.Padding(2);
            this.cbHasEmptyTrip.Name = "cbHasEmptyTrip";
            this.cbHasEmptyTrip.Palette = this.kp1;
            this.cbHasEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbHasEmptyTrip.Size = new System.Drawing.Size(108, 20);
            this.cbHasEmptyTrip.TabIndex = 20;
            this.cbHasEmptyTrip.Values.Text = "EmptyTrip(mm)";
            this.cbHasEmptyTrip.CheckedChanged += new System.EventHandler(this.cbHasEmptyTrip_CheckedChanged);
            // 
            // cbReverseVerticalAxis
            // 
            this.cbReverseVerticalAxis.Location = new System.Drawing.Point(40, 103);
            this.cbReverseVerticalAxis.Margin = new System.Windows.Forms.Padding(2);
            this.cbReverseVerticalAxis.Name = "cbReverseVerticalAxis";
            this.cbReverseVerticalAxis.Palette = this.kp1;
            this.cbReverseVerticalAxis.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbReverseVerticalAxis.Size = new System.Drawing.Size(98, 20);
            this.cbReverseVerticalAxis.TabIndex = 21;
            this.cbReverseVerticalAxis.Values.Text = "Reverse YAxis";
            // 
            // tbBackward
            // 
            this.tbBackward.Location = new System.Drawing.Point(200, 38);
            this.tbBackward.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbBackward.Name = "tbBackward";
            this.tbBackward.Palette = this.kp1;
            this.tbBackward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbBackward.Size = new System.Drawing.Size(72, 23);
            this.tbBackward.TabIndex = 23;
            // 
            // lbBackward
            // 
            this.lbBackward.Location = new System.Drawing.Point(4, 41);
            this.lbBackward.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbBackward.Name = "lbBackward";
            this.lbBackward.Palette = this.kp1;
            this.lbBackward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbBackward.Size = new System.Drawing.Size(63, 20);
            this.lbBackward.TabIndex = 22;
            this.lbBackward.Values.Text = "Backward";
            // 
            // tbRightward
            // 
            this.tbRightward.Location = new System.Drawing.Point(200, 102);
            this.tbRightward.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbRightward.Name = "tbRightward";
            this.tbRightward.Palette = this.kp1;
            this.tbRightward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbRightward.Size = new System.Drawing.Size(72, 23);
            this.tbRightward.TabIndex = 27;
            // 
            // lbRightward
            // 
            this.lbRightward.Location = new System.Drawing.Point(4, 105);
            this.lbRightward.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRightward.Name = "lbRightward";
            this.lbRightward.Palette = this.kp1;
            this.lbRightward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbRightward.Size = new System.Drawing.Size(66, 20);
            this.lbRightward.TabIndex = 26;
            this.lbRightward.Values.Text = "Rightward";
            // 
            // tbLeftward
            // 
            this.tbLeftward.Location = new System.Drawing.Point(200, 70);
            this.tbLeftward.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbLeftward.Name = "tbLeftward";
            this.tbLeftward.Palette = this.kp1;
            this.tbLeftward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbLeftward.Size = new System.Drawing.Size(72, 23);
            this.tbLeftward.TabIndex = 25;
            // 
            // lbLeftward
            // 
            this.lbLeftward.Location = new System.Drawing.Point(4, 73);
            this.lbLeftward.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLeftward.Name = "lbLeftward";
            this.lbLeftward.Palette = this.kp1;
            this.lbLeftward.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbLeftward.Size = new System.Drawing.Size(58, 20);
            this.lbLeftward.TabIndex = 24;
            this.lbLeftward.Values.Text = "Leftward";
            // 
            // tbSlowFinalSpeed
            // 
            this.tbSlowFinalSpeed.Location = new System.Drawing.Point(200, 103);
            this.tbSlowFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSlowFinalSpeed.Name = "tbSlowFinalSpeed";
            this.tbSlowFinalSpeed.Palette = this.kp1;
            this.tbSlowFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbSlowFinalSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbSlowFinalSpeed.TabIndex = 35;
            // 
            // lbSlowFinalSpeed
            // 
            this.lbSlowFinalSpeed.Location = new System.Drawing.Point(4, 106);
            this.lbSlowFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlowFinalSpeed.Name = "lbSlowFinalSpeed";
            this.lbSlowFinalSpeed.Palette = this.kp1;
            this.lbSlowFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSlowFinalSpeed.Size = new System.Drawing.Size(117, 20);
            this.lbSlowFinalSpeed.TabIndex = 34;
            this.lbSlowFinalSpeed.Values.Text = "Acceleration(mm/s)";
            // 
            // tbSlowAcceleration
            // 
            this.tbSlowAcceleration.Location = new System.Drawing.Point(200, 71);
            this.tbSlowAcceleration.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSlowAcceleration.Name = "tbSlowAcceleration";
            this.tbSlowAcceleration.Palette = this.kp1;
            this.tbSlowAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbSlowAcceleration.Size = new System.Drawing.Size(72, 23);
            this.tbSlowAcceleration.TabIndex = 33;
            // 
            // lbSlowAcceleration
            // 
            this.lbSlowAcceleration.Location = new System.Drawing.Point(4, 74);
            this.lbSlowAcceleration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlowAcceleration.Name = "lbSlowAcceleration";
            this.lbSlowAcceleration.Palette = this.kp1;
            this.lbSlowAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSlowAcceleration.Size = new System.Drawing.Size(93, 20);
            this.lbSlowAcceleration.TabIndex = 32;
            this.lbSlowAcceleration.Values.Text = "加速度(mm/s²)";
            // 
            // tbSlowBeginSpeed
            // 
            this.tbSlowBeginSpeed.Location = new System.Drawing.Point(200, 39);
            this.tbSlowBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSlowBeginSpeed.Name = "tbSlowBeginSpeed";
            this.tbSlowBeginSpeed.Palette = this.kp1;
            this.tbSlowBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbSlowBeginSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbSlowBeginSpeed.TabIndex = 31;
            // 
            // lbSlowBeginSpeed
            // 
            this.lbSlowBeginSpeed.Location = new System.Drawing.Point(4, 42);
            this.lbSlowBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlowBeginSpeed.Name = "lbSlowBeginSpeed";
            this.lbSlowBeginSpeed.Palette = this.kp1;
            this.lbSlowBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSlowBeginSpeed.Size = new System.Drawing.Size(118, 20);
            this.lbSlowBeginSpeed.TabIndex = 30;
            this.lbSlowBeginSpeed.Values.Text = "Begin Speed(mm/s)";
            // 
            // tbSlowStepDistance
            // 
            this.tbSlowStepDistance.Location = new System.Drawing.Point(200, 7);
            this.tbSlowStepDistance.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbSlowStepDistance.Name = "tbSlowStepDistance";
            this.tbSlowStepDistance.Palette = this.kp1;
            this.tbSlowStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbSlowStepDistance.Size = new System.Drawing.Size(72, 23);
            this.tbSlowStepDistance.TabIndex = 29;
            // 
            // lbSlowStepDistance
            // 
            this.lbSlowStepDistance.Location = new System.Drawing.Point(4, 10);
            this.lbSlowStepDistance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlowStepDistance.Name = "lbSlowStepDistance";
            this.lbSlowStepDistance.Palette = this.kp1;
            this.lbSlowStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbSlowStepDistance.Size = new System.Drawing.Size(114, 20);
            this.lbSlowStepDistance.TabIndex = 28;
            this.lbSlowStepDistance.Values.Text = "Step Distance(mm)";
            // 
            // gbSlow
            // 
            this.gbSlow.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbSlow.Location = new System.Drawing.Point(20, 172);
            this.gbSlow.Margin = new System.Windows.Forms.Padding(2);
            this.gbSlow.Name = "gbSlow";
            this.gbSlow.Palette = this.kp1;
            this.gbSlow.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbSlow.Panel
            // 
            this.gbSlow.Panel.Controls.Add(this.lbSlowStepDistance);
            this.gbSlow.Panel.Controls.Add(this.tbSlowFinalSpeed);
            this.gbSlow.Panel.Controls.Add(this.tbSlowStepDistance);
            this.gbSlow.Panel.Controls.Add(this.lbSlowFinalSpeed);
            this.gbSlow.Panel.Controls.Add(this.lbSlowBeginSpeed);
            this.gbSlow.Panel.Controls.Add(this.tbSlowAcceleration);
            this.gbSlow.Panel.Controls.Add(this.tbSlowBeginSpeed);
            this.gbSlow.Panel.Controls.Add(this.lbSlowAcceleration);
            this.gbSlow.Size = new System.Drawing.Size(292, 157);
            this.gbSlow.TabIndex = 36;
            this.gbSlow.Values.Heading = "Slow";
            // 
            // gbEmptyTrip
            // 
            this.gbEmptyTrip.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbEmptyTrip.Location = new System.Drawing.Point(316, 11);
            this.gbEmptyTrip.Margin = new System.Windows.Forms.Padding(2);
            this.gbEmptyTrip.Name = "gbEmptyTrip";
            this.gbEmptyTrip.Palette = this.kp1;
            this.gbEmptyTrip.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbEmptyTrip.Panel
            // 
            this.gbEmptyTrip.Panel.Controls.Add(this.lbForward);
            this.gbEmptyTrip.Panel.Controls.Add(this.tbForward);
            this.gbEmptyTrip.Panel.Controls.Add(this.tbRightward);
            this.gbEmptyTrip.Panel.Controls.Add(this.lbBackward);
            this.gbEmptyTrip.Panel.Controls.Add(this.lbRightward);
            this.gbEmptyTrip.Panel.Controls.Add(this.tbBackward);
            this.gbEmptyTrip.Panel.Controls.Add(this.tbLeftward);
            this.gbEmptyTrip.Panel.Controls.Add(this.lbLeftward);
            this.gbEmptyTrip.Size = new System.Drawing.Size(292, 157);
            this.gbEmptyTrip.TabIndex = 37;
            this.gbEmptyTrip.Values.Heading = "EmptyTrip(mm)";
            // 
            // gbMedium
            // 
            this.gbMedium.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbMedium.Location = new System.Drawing.Point(316, 172);
            this.gbMedium.Margin = new System.Windows.Forms.Padding(2);
            this.gbMedium.Name = "gbMedium";
            this.gbMedium.Palette = this.kp1;
            this.gbMedium.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbMedium.Panel
            // 
            this.gbMedium.Panel.Controls.Add(this.lbMediumStepDistance);
            this.gbMedium.Panel.Controls.Add(this.tbMediumFinalSpeed);
            this.gbMedium.Panel.Controls.Add(this.tbMediumStepDistance);
            this.gbMedium.Panel.Controls.Add(this.lbMediumFinalSpeed);
            this.gbMedium.Panel.Controls.Add(this.lbMediumBeginSpeed);
            this.gbMedium.Panel.Controls.Add(this.tbMediumAcceleration);
            this.gbMedium.Panel.Controls.Add(this.tbMediumBeginSpeed);
            this.gbMedium.Panel.Controls.Add(this.lbMediumAcceleration);
            this.gbMedium.Size = new System.Drawing.Size(292, 157);
            this.gbMedium.TabIndex = 37;
            this.gbMedium.Values.Heading = "Medium";
            // 
            // lbMediumStepDistance
            // 
            this.lbMediumStepDistance.Location = new System.Drawing.Point(4, 8);
            this.lbMediumStepDistance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMediumStepDistance.Name = "lbMediumStepDistance";
            this.lbMediumStepDistance.Palette = this.kp1;
            this.lbMediumStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMediumStepDistance.Size = new System.Drawing.Size(64, 20);
            this.lbMediumStepDistance.TabIndex = 28;
            this.lbMediumStepDistance.Values.Text = "Step(mm)";
            // 
            // tbMediumFinalSpeed
            // 
            this.tbMediumFinalSpeed.Location = new System.Drawing.Point(200, 101);
            this.tbMediumFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbMediumFinalSpeed.Name = "tbMediumFinalSpeed";
            this.tbMediumFinalSpeed.Palette = this.kp1;
            this.tbMediumFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbMediumFinalSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbMediumFinalSpeed.TabIndex = 35;
            // 
            // tbMediumStepDistance
            // 
            this.tbMediumStepDistance.Location = new System.Drawing.Point(200, 5);
            this.tbMediumStepDistance.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbMediumStepDistance.Name = "tbMediumStepDistance";
            this.tbMediumStepDistance.Palette = this.kp1;
            this.tbMediumStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbMediumStepDistance.Size = new System.Drawing.Size(72, 23);
            this.tbMediumStepDistance.TabIndex = 29;
            // 
            // lbMediumFinalSpeed
            // 
            this.lbMediumFinalSpeed.Location = new System.Drawing.Point(4, 104);
            this.lbMediumFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMediumFinalSpeed.Name = "lbMediumFinalSpeed";
            this.lbMediumFinalSpeed.Palette = this.kp1;
            this.lbMediumFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMediumFinalSpeed.Size = new System.Drawing.Size(112, 20);
            this.lbMediumFinalSpeed.TabIndex = 34;
            this.lbMediumFinalSpeed.Values.Text = "Final Speed(mm/s)";
            // 
            // lbMediumBeginSpeed
            // 
            this.lbMediumBeginSpeed.Location = new System.Drawing.Point(4, 40);
            this.lbMediumBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMediumBeginSpeed.Name = "lbMediumBeginSpeed";
            this.lbMediumBeginSpeed.Palette = this.kp1;
            this.lbMediumBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMediumBeginSpeed.Size = new System.Drawing.Size(118, 20);
            this.lbMediumBeginSpeed.TabIndex = 30;
            this.lbMediumBeginSpeed.Values.Text = "Begin Speed(mm/s)";
            // 
            // tbMediumAcceleration
            // 
            this.tbMediumAcceleration.Location = new System.Drawing.Point(200, 69);
            this.tbMediumAcceleration.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbMediumAcceleration.Name = "tbMediumAcceleration";
            this.tbMediumAcceleration.Palette = this.kp1;
            this.tbMediumAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbMediumAcceleration.Size = new System.Drawing.Size(72, 23);
            this.tbMediumAcceleration.TabIndex = 33;
            // 
            // tbMediumBeginSpeed
            // 
            this.tbMediumBeginSpeed.Location = new System.Drawing.Point(200, 37);
            this.tbMediumBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbMediumBeginSpeed.Name = "tbMediumBeginSpeed";
            this.tbMediumBeginSpeed.Palette = this.kp1;
            this.tbMediumBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbMediumBeginSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbMediumBeginSpeed.TabIndex = 31;
            // 
            // lbMediumAcceleration
            // 
            this.lbMediumAcceleration.Location = new System.Drawing.Point(4, 72);
            this.lbMediumAcceleration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMediumAcceleration.Name = "lbMediumAcceleration";
            this.lbMediumAcceleration.Palette = this.kp1;
            this.lbMediumAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbMediumAcceleration.Size = new System.Drawing.Size(121, 20);
            this.lbMediumAcceleration.TabIndex = 32;
            this.lbMediumAcceleration.Values.Text = "Acceleration(mm/s²)";
            // 
            // gbFast
            // 
            this.gbFast.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbFast.Location = new System.Drawing.Point(20, 333);
            this.gbFast.Margin = new System.Windows.Forms.Padding(2);
            this.gbFast.Name = "gbFast";
            this.gbFast.Palette = this.kp1;
            this.gbFast.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbFast.Panel
            // 
            this.gbFast.Panel.Controls.Add(this.lbFastStepDistance);
            this.gbFast.Panel.Controls.Add(this.tbFastFinalSpeed);
            this.gbFast.Panel.Controls.Add(this.tbFastStepDistance);
            this.gbFast.Panel.Controls.Add(this.lbFastFinalSpeed);
            this.gbFast.Panel.Controls.Add(this.lbFastBeginSpeed);
            this.gbFast.Panel.Controls.Add(this.tbFastAcceleration);
            this.gbFast.Panel.Controls.Add(this.tbFastBeginSpeed);
            this.gbFast.Panel.Controls.Add(this.lbFastAcceleration);
            this.gbFast.Size = new System.Drawing.Size(292, 157);
            this.gbFast.TabIndex = 38;
            this.gbFast.Values.Heading = "Fast";
            // 
            // lbFastStepDistance
            // 
            this.lbFastStepDistance.Location = new System.Drawing.Point(4, 11);
            this.lbFastStepDistance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFastStepDistance.Name = "lbFastStepDistance";
            this.lbFastStepDistance.Palette = this.kp1;
            this.lbFastStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbFastStepDistance.Size = new System.Drawing.Size(64, 20);
            this.lbFastStepDistance.TabIndex = 28;
            this.lbFastStepDistance.Values.Text = "Step(mm)";
            // 
            // tbFastFinalSpeed
            // 
            this.tbFastFinalSpeed.Location = new System.Drawing.Point(200, 104);
            this.tbFastFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbFastFinalSpeed.Name = "tbFastFinalSpeed";
            this.tbFastFinalSpeed.Palette = this.kp1;
            this.tbFastFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbFastFinalSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbFastFinalSpeed.TabIndex = 35;
            // 
            // tbFastStepDistance
            // 
            this.tbFastStepDistance.Location = new System.Drawing.Point(200, 8);
            this.tbFastStepDistance.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbFastStepDistance.Name = "tbFastStepDistance";
            this.tbFastStepDistance.Palette = this.kp1;
            this.tbFastStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbFastStepDistance.Size = new System.Drawing.Size(72, 23);
            this.tbFastStepDistance.TabIndex = 29;
            // 
            // lbFastFinalSpeed
            // 
            this.lbFastFinalSpeed.Location = new System.Drawing.Point(4, 107);
            this.lbFastFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFastFinalSpeed.Name = "lbFastFinalSpeed";
            this.lbFastFinalSpeed.Palette = this.kp1;
            this.lbFastFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbFastFinalSpeed.Size = new System.Drawing.Size(112, 20);
            this.lbFastFinalSpeed.TabIndex = 34;
            this.lbFastFinalSpeed.Values.Text = "Final Speed(mm/s)";
            // 
            // lbFastBeginSpeed
            // 
            this.lbFastBeginSpeed.Location = new System.Drawing.Point(4, 43);
            this.lbFastBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFastBeginSpeed.Name = "lbFastBeginSpeed";
            this.lbFastBeginSpeed.Palette = this.kp1;
            this.lbFastBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbFastBeginSpeed.Size = new System.Drawing.Size(118, 20);
            this.lbFastBeginSpeed.TabIndex = 30;
            this.lbFastBeginSpeed.Values.Text = "Begin Speed(mm/s)";
            // 
            // tbFastAcceleration
            // 
            this.tbFastAcceleration.Location = new System.Drawing.Point(200, 72);
            this.tbFastAcceleration.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbFastAcceleration.Name = "tbFastAcceleration";
            this.tbFastAcceleration.Palette = this.kp1;
            this.tbFastAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbFastAcceleration.Size = new System.Drawing.Size(72, 23);
            this.tbFastAcceleration.TabIndex = 33;
            // 
            // tbFastBeginSpeed
            // 
            this.tbFastBeginSpeed.Location = new System.Drawing.Point(200, 40);
            this.tbFastBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbFastBeginSpeed.Name = "tbFastBeginSpeed";
            this.tbFastBeginSpeed.Palette = this.kp1;
            this.tbFastBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbFastBeginSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbFastBeginSpeed.TabIndex = 31;
            // 
            // lbFastAcceleration
            // 
            this.lbFastAcceleration.Location = new System.Drawing.Point(4, 75);
            this.lbFastAcceleration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFastAcceleration.Name = "lbFastAcceleration";
            this.lbFastAcceleration.Palette = this.kp1;
            this.lbFastAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbFastAcceleration.Size = new System.Drawing.Size(121, 20);
            this.lbFastAcceleration.TabIndex = 32;
            this.lbFastAcceleration.Values.Text = "Acceleration(mm/s²)";
            // 
            // gbVeryFast
            // 
            this.gbVeryFast.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonGallery;
            this.gbVeryFast.Location = new System.Drawing.Point(316, 333);
            this.gbVeryFast.Margin = new System.Windows.Forms.Padding(2);
            this.gbVeryFast.Name = "gbVeryFast";
            this.gbVeryFast.Palette = this.kp1;
            this.gbVeryFast.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            // 
            // gbVeryFast.Panel
            // 
            this.gbVeryFast.Panel.Controls.Add(this.lbVeryFastStepDistance);
            this.gbVeryFast.Panel.Controls.Add(this.tbVeryFastFinalSpeed);
            this.gbVeryFast.Panel.Controls.Add(this.tbVeryFastStepDistance);
            this.gbVeryFast.Panel.Controls.Add(this.lbVeryFastFinalSpeed);
            this.gbVeryFast.Panel.Controls.Add(this.lbVeryFastBeginSpeed);
            this.gbVeryFast.Panel.Controls.Add(this.tbVeryFastAcceleration);
            this.gbVeryFast.Panel.Controls.Add(this.tbVeryFastBeginSpeed);
            this.gbVeryFast.Panel.Controls.Add(this.lbVeryFastAcceleration);
            this.gbVeryFast.Size = new System.Drawing.Size(292, 157);
            this.gbVeryFast.TabIndex = 38;
            this.gbVeryFast.Values.Heading = "Ultra";
            // 
            // lbVeryFastStepDistance
            // 
            this.lbVeryFastStepDistance.Location = new System.Drawing.Point(4, 10);
            this.lbVeryFastStepDistance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVeryFastStepDistance.Name = "lbVeryFastStepDistance";
            this.lbVeryFastStepDistance.Palette = this.kp1;
            this.lbVeryFastStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbVeryFastStepDistance.Size = new System.Drawing.Size(64, 20);
            this.lbVeryFastStepDistance.TabIndex = 28;
            this.lbVeryFastStepDistance.Values.Text = "Step(mm)";
            // 
            // tbVeryFastFinalSpeed
            // 
            this.tbVeryFastFinalSpeed.Location = new System.Drawing.Point(200, 103);
            this.tbVeryFastFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbVeryFastFinalSpeed.Name = "tbVeryFastFinalSpeed";
            this.tbVeryFastFinalSpeed.Palette = this.kp1;
            this.tbVeryFastFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbVeryFastFinalSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbVeryFastFinalSpeed.TabIndex = 35;
            // 
            // tbVeryFastStepDistance
            // 
            this.tbVeryFastStepDistance.Location = new System.Drawing.Point(200, 7);
            this.tbVeryFastStepDistance.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbVeryFastStepDistance.Name = "tbVeryFastStepDistance";
            this.tbVeryFastStepDistance.Palette = this.kp1;
            this.tbVeryFastStepDistance.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbVeryFastStepDistance.Size = new System.Drawing.Size(72, 23);
            this.tbVeryFastStepDistance.TabIndex = 29;
            // 
            // lbVeryFastFinalSpeed
            // 
            this.lbVeryFastFinalSpeed.Location = new System.Drawing.Point(4, 106);
            this.lbVeryFastFinalSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVeryFastFinalSpeed.Name = "lbVeryFastFinalSpeed";
            this.lbVeryFastFinalSpeed.Palette = this.kp1;
            this.lbVeryFastFinalSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbVeryFastFinalSpeed.Size = new System.Drawing.Size(112, 20);
            this.lbVeryFastFinalSpeed.TabIndex = 34;
            this.lbVeryFastFinalSpeed.Values.Text = "Final Speed(mm/s)";
            // 
            // lbVeryFastBeginSpeed
            // 
            this.lbVeryFastBeginSpeed.Location = new System.Drawing.Point(4, 42);
            this.lbVeryFastBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVeryFastBeginSpeed.Name = "lbVeryFastBeginSpeed";
            this.lbVeryFastBeginSpeed.Palette = this.kp1;
            this.lbVeryFastBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbVeryFastBeginSpeed.Size = new System.Drawing.Size(118, 20);
            this.lbVeryFastBeginSpeed.TabIndex = 30;
            this.lbVeryFastBeginSpeed.Values.Text = "Begin Speed(mm/s)";
            // 
            // tbVeryFastAcceleration
            // 
            this.tbVeryFastAcceleration.Location = new System.Drawing.Point(200, 71);
            this.tbVeryFastAcceleration.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbVeryFastAcceleration.Name = "tbVeryFastAcceleration";
            this.tbVeryFastAcceleration.Palette = this.kp1;
            this.tbVeryFastAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbVeryFastAcceleration.Size = new System.Drawing.Size(72, 23);
            this.tbVeryFastAcceleration.TabIndex = 33;
            // 
            // tbVeryFastBeginSpeed
            // 
            this.tbVeryFastBeginSpeed.Location = new System.Drawing.Point(200, 39);
            this.tbVeryFastBeginSpeed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbVeryFastBeginSpeed.Name = "tbVeryFastBeginSpeed";
            this.tbVeryFastBeginSpeed.Palette = this.kp1;
            this.tbVeryFastBeginSpeed.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbVeryFastBeginSpeed.Size = new System.Drawing.Size(72, 23);
            this.tbVeryFastBeginSpeed.TabIndex = 31;
            // 
            // lbVeryFastAcceleration
            // 
            this.lbVeryFastAcceleration.Location = new System.Drawing.Point(4, 74);
            this.lbVeryFastAcceleration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbVeryFastAcceleration.Name = "lbVeryFastAcceleration";
            this.lbVeryFastAcceleration.Palette = this.kp1;
            this.lbVeryFastAcceleration.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbVeryFastAcceleration.Size = new System.Drawing.Size(121, 20);
            this.lbVeryFastAcceleration.TabIndex = 32;
            this.lbVeryFastAcceleration.Values.Text = "Acceleration(mm/s²)";
            // 
            // lbPulsePerMM
            // 
            this.lbPulsePerMM.Location = new System.Drawing.Point(24, 136);
            this.lbPulsePerMM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPulsePerMM.Name = "lbPulsePerMM";
            this.lbPulsePerMM.Palette = this.kp1;
            this.lbPulsePerMM.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbPulsePerMM.Size = new System.Drawing.Size(148, 20);
            this.lbPulsePerMM.TabIndex = 36;
            this.lbPulsePerMM.Values.Text = "Pulse Per MM(pulse/mm)";
            // 
            // tbPulsePerMM
            // 
            this.tbPulsePerMM.Location = new System.Drawing.Point(220, 133);
            this.tbPulsePerMM.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tbPulsePerMM.Name = "tbPulsePerMM";
            this.tbPulsePerMM.Palette = this.kp1;
            this.tbPulsePerMM.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbPulsePerMM.Size = new System.Drawing.Size(72, 23);
            this.tbPulsePerMM.TabIndex = 37;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(76, 498);
            this.btnSave.Name = "btnSave";
            this.btnSave.Palette = this.kp1;
            this.btnSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 39;
            this.btnSave.Values.Text = "Confrim";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(400, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PlatformSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 555);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbPulsePerMM);
            this.Controls.Add(this.gbVeryFast);
            this.Controls.Add(this.tbPulsePerMM);
            this.Controls.Add(this.gbFast);
            this.Controls.Add(this.gbMedium);
            this.Controls.Add(this.gbEmptyTrip);
            this.Controls.Add(this.gbSlow);
            this.Controls.Add(this.cbReverseVerticalAxis);
            this.Controls.Add(this.cbHasEmptyTrip);
            this.Controls.Add(this.cbReverseHorizontalAxis);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlatformSettingForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Platform Setting";
            this.Load += new System.EventHandler(this.PlatformSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbSlow.Panel)).EndInit();
            this.gbSlow.Panel.ResumeLayout(false);
            this.gbSlow.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbSlow)).EndInit();
            this.gbSlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbEmptyTrip.Panel)).EndInit();
            this.gbEmptyTrip.Panel.ResumeLayout(false);
            this.gbEmptyTrip.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbEmptyTrip)).EndInit();
            this.gbEmptyTrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbMedium.Panel)).EndInit();
            this.gbMedium.Panel.ResumeLayout(false);
            this.gbMedium.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbMedium)).EndInit();
            this.gbMedium.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbFast.Panel)).EndInit();
            this.gbFast.Panel.ResumeLayout(false);
            this.gbFast.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbFast)).EndInit();
            this.gbFast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbVeryFast.Panel)).EndInit();
            this.gbVeryFast.Panel.ResumeLayout(false);
            this.gbVeryFast.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbVeryFast)).EndInit();
            this.gbVeryFast.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		public PlatformSettingForm(SYJKPlatformInfo platformInfo)
		{
			InitializeComponent();
			LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;


            this.platformInfo = platformInfo;
			if (this.platformInfo == null)
			{
				throw new NullReferenceException("载物平台信息为空！");
			}
		}

		private void PlatformSettingForm_Load(object sender, EventArgs e)
		{
			cbHasEmptyTrip.Checked = platformInfo.HasEmptyTrip;
			gbEmptyTrip.Enabled = platformInfo.HasEmptyTrip;
			cbReverseHorizontalAxis.Checked = platformInfo.IsReverseHorizontalAxis;
			cbReverseVerticalAxis.Checked = platformInfo.IsReverseVertivalAxis;
			tbPulsePerMM.Text = platformInfo.PulsePerMM.ToString();
			tbForward.Text = platformInfo.ForwardEmptyTrip.ToString();
			tbBackward.Text = platformInfo.BackwardEmptyTrip.ToString();
			tbLeftward.Text = platformInfo.LeftwardEmptyTrip.ToString();
			tbRightward.Text = platformInfo.RightwardEmptyTrip.ToString();
			SpeedRateInfo slowInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.Slow);
			if (slowInfo == null)
			{
				slowInfo = new SpeedRateInfo();
				platformInfo.RateList.Add(slowInfo);
			}
			tbSlowStepDistance.Text = slowInfo.StepDistance.ToString();
			tbSlowBeginSpeed.Text = slowInfo.BeginSpeed.ToString();
			tbSlowAcceleration.Text = slowInfo.Acceleration.ToString();
			tbSlowFinalSpeed.Text = slowInfo.FinalSpeed.ToString();
			SpeedRateInfo mediumInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.Medium);
			if (mediumInfo == null)
			{
				mediumInfo = new SpeedRateInfo();
				platformInfo.RateList.Add(mediumInfo);
			}
			tbMediumStepDistance.Text = mediumInfo.StepDistance.ToString();
			tbMediumBeginSpeed.Text = mediumInfo.BeginSpeed.ToString();
			tbMediumAcceleration.Text = mediumInfo.Acceleration.ToString();
			tbMediumFinalSpeed.Text = mediumInfo.FinalSpeed.ToString();
			SpeedRateInfo fastInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.Fast);
			if (fastInfo == null)
			{
				fastInfo = new SpeedRateInfo();
				platformInfo.RateList.Add(fastInfo);
			}
			tbFastStepDistance.Text = fastInfo.StepDistance.ToString();
			tbFastBeginSpeed.Text = fastInfo.BeginSpeed.ToString();
			tbFastAcceleration.Text = fastInfo.Acceleration.ToString();
			tbFastFinalSpeed.Text = fastInfo.FinalSpeed.ToString();
			SpeedRateInfo veryFastInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.VeryFast);
			if (veryFastInfo == null)
			{
				veryFastInfo = new SpeedRateInfo();
				platformInfo.RateList.Add(veryFastInfo);
			}
			tbVeryFastStepDistance.Text = veryFastInfo.StepDistance.ToString();
			tbVeryFastBeginSpeed.Text = veryFastInfo.BeginSpeed.ToString();
			tbVeryFastAcceleration.Text = veryFastInfo.Acceleration.ToString();
			tbVeryFastFinalSpeed.Text = veryFastInfo.FinalSpeed.ToString();
		}

		private void cbHasEmptyTrip_CheckedChanged(object sender, EventArgs e)
		{
			gbEmptyTrip.Enabled = cbHasEmptyTrip.Checked;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				int pulsePerMM = int.Parse(tbPulsePerMM.Text);
				float forward = float.Parse(tbForward.Text);
				float backward = float.Parse(tbBackward.Text);
				float leftward = float.Parse(tbLeftward.Text);
				float rightward = float.Parse(tbRightward.Text);
				float slowStepDistance = float.Parse(tbSlowStepDistance.Text);
				float slowBeginSpeed = float.Parse(tbSlowBeginSpeed.Text);
				float slowAcceleration = float.Parse(tbSlowAcceleration.Text);
				float slowFinalSpeed = float.Parse(tbSlowFinalSpeed.Text);
				float mediumStepDistance = float.Parse(tbMediumStepDistance.Text);
				float mediumBeginSpeed = float.Parse(tbMediumBeginSpeed.Text);
				float mediumAcceleration = float.Parse(tbMediumAcceleration.Text);
				float mediumFinalSpeed = float.Parse(tbMediumFinalSpeed.Text);
				float fastStepDistance = float.Parse(tbFastStepDistance.Text);
				float fastBeginSpeed = float.Parse(tbFastBeginSpeed.Text);
				float fastAcceleration = float.Parse(tbFastAcceleration.Text);
				float fastFinalSpeed = float.Parse(tbFastFinalSpeed.Text);
				float veryFastStepDistance = float.Parse(tbVeryFastStepDistance.Text);
				float veryFastBeginSpeed = float.Parse(tbVeryFastBeginSpeed.Text);
				float veryFastAcceleration = float.Parse(tbVeryFastAcceleration.Text);
				float veryFastFinalSpeed = float.Parse(tbVeryFastFinalSpeed.Text);
				platformInfo.HasEmptyTrip = cbHasEmptyTrip.Checked;
				platformInfo.IsReverseHorizontalAxis = cbReverseHorizontalAxis.Checked;
				platformInfo.IsReverseVertivalAxis = cbReverseVerticalAxis.Checked;
				platformInfo.PulsePerMM = pulsePerMM;
				platformInfo.ForwardEmptyTrip = forward;
				platformInfo.BackwardEmptyTrip = backward;
				platformInfo.LeftwardEmptyTrip = leftward;
				platformInfo.RightwardEmptyTrip = rightward;
				SpeedRateInfo slowInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.Slow);
				slowInfo.StepDistance = slowStepDistance;
				slowInfo.BeginSpeed = slowBeginSpeed;
				slowInfo.Acceleration = slowAcceleration;
				slowInfo.FinalSpeed = slowFinalSpeed;
				SpeedRateInfo mediumInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.Medium);
				mediumInfo.StepDistance = mediumStepDistance;
				mediumInfo.BeginSpeed = mediumBeginSpeed;
				mediumInfo.Acceleration = mediumAcceleration;
				mediumInfo.FinalSpeed = mediumFinalSpeed;
				SpeedRateInfo fastInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.Fast);
				fastInfo.StepDistance = fastStepDistance;
				fastInfo.BeginSpeed = fastBeginSpeed;
				fastInfo.Acceleration = fastAcceleration;
				fastInfo.FinalSpeed = fastFinalSpeed;
				SpeedRateInfo veryFastInfo = platformInfo.RateList.FirstOrDefault((SpeedRateInfo x) => x.RateName == SpeedRate.VeryFast);
				veryFastInfo.StepDistance = veryFastStepDistance;
				veryFastInfo.BeginSpeed = veryFastBeginSpeed;
				veryFastInfo.Acceleration = veryFastAcceleration;
				veryFastInfo.FinalSpeed = veryFastFinalSpeed;
				ConfigFileManager.SaveSYJKPlatformConfigFile(platformInfo);
				Dispose();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed to save platform data!");
				MsgBox.ShowWarning(ResourcesManager.Resources.R_PlatformSetting_Message_InputValueInvalid);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_PlatformSetting_Title;
			cbHasEmptyTrip.Text = ResourcesManager.Resources.R_PlatformSetting_HasEmptyTrip;
			cbReverseHorizontalAxis.Text = ResourcesManager.Resources.R_PlatformSetting_ReverseHorizontalAxis;
			cbReverseVerticalAxis.Text = ResourcesManager.Resources.R_PlatformSetting_ReverseVerticalAxis;
			lbPulsePerMM.Text = ResourcesManager.Resources.R_PlatformSetting_PulsePerMM;
			gbEmptyTrip.Text = ResourcesManager.Resources.R_PlatformSetting_EmptyTrip;
			lbForward.Text = ResourcesManager.Resources.R_PlatformSetting_Forward;
			lbBackward.Text = ResourcesManager.Resources.R_PlatformSetting_Backward;
			lbLeftward.Text = ResourcesManager.Resources.R_PlatformSetting_Leftward;
			lbRightward.Text = ResourcesManager.Resources.R_PlatformSetting_Rightward;
			gbSlow.Text = ResourcesManager.Resources.R_PlatformSetting_Slow;
			lbSlowStepDistance.Text = ResourcesManager.Resources.R_PlatformSetting_StepDistance;
			lbSlowBeginSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_BeginSpeed;
			lbSlowAcceleration.Text = ResourcesManager.Resources.R_PlatfoemSetting_Acceleration;
			lbSlowFinalSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_FinalSpeed;
			gbMedium.Text = ResourcesManager.Resources.R_PlatformSetting_Medium;
			lbMediumStepDistance.Text = ResourcesManager.Resources.R_PlatformSetting_StepDistance;
			lbMediumBeginSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_BeginSpeed;
			lbMediumAcceleration.Text = ResourcesManager.Resources.R_PlatfoemSetting_Acceleration;
			lbMediumFinalSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_FinalSpeed;
			gbFast.Text = ResourcesManager.Resources.R_PlatformSetting_Fast;
			lbFastStepDistance.Text = ResourcesManager.Resources.R_PlatformSetting_StepDistance;
			lbFastBeginSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_BeginSpeed;
			lbFastAcceleration.Text = ResourcesManager.Resources.R_PlatfoemSetting_Acceleration;
			lbFastFinalSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_FinalSpeed;
			gbVeryFast.Text = ResourcesManager.Resources.R_PlatformSetting_VeryFast;
			lbVeryFastStepDistance.Text = ResourcesManager.Resources.R_PlatformSetting_StepDistance;
			lbVeryFastBeginSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_BeginSpeed;
			lbVeryFastAcceleration.Text = ResourcesManager.Resources.R_PlatfoemSetting_Acceleration;
			lbVeryFastFinalSpeed.Text = ResourcesManager.Resources.R_PlatformSetting_FinalSpeed;
			btnSave.Text = ResourcesManager.Resources.R_PlatformSetting_Save;
			btnCancel.Text = ResourcesManager.Resources.R_PlatformSetting_Cancel;
		}
	}
}