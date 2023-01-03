using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Labtt.Data;
using MessageBoxExApp;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class LoginForm : KryptonForm
    {
		[Serializable]
		private class LoginInfo
		{
			public string Username { get; set; }

			public string Password { get; set; }
		}

		private LoginInfo loginInfo = null;

		private IContainer components = null;

		private KryptonLabel label1;

		private KryptonLabel label2;

		private KryptonTextBox tbUsername;

		private KryptonTextBox tbPassword;

		private KryptonButton btnLogin;

		private KryptonButton btnCancel;

		private KryptonCheckBox cbRemenberUsername;
        private KryptonPalette kp1;
        private KryptonCheckBox cbRemenberPassword;

		public LoginForm(MainForm owner)
		{
			InitializeComponent();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;

        }

		private void LoginForm_Shown(object sender, EventArgs e)
		{
			loginInfo = (LoginInfo)FileOperator.DeserializeFromBinaryFile(CommonData.LoginInfoFilepath);
			if (loginInfo == null)
			{
				loginInfo = new LoginInfo();
			}
			if (!string.IsNullOrEmpty(loginInfo.Password))
			{
				tbPassword.Text = loginInfo.Password;
				cbRemenberPassword.Checked = true;
				cbRemenberUsername.Enabled = false;
			}
			if (!string.IsNullOrEmpty(loginInfo.Username))
			{
				tbUsername.Text = loginInfo.Username;
				cbRemenberUsername.Checked = true;
			}
		}

		private void cbRemenberPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (cbRemenberPassword.Checked)
			{
				cbRemenberUsername.Checked = true;
				cbRemenberUsername.Enabled = false;
			}
			else
			{
				cbRemenberUsername.Enabled = true;
			}
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			string username = tbUsername.Text;
			string password = tbPassword.Text;
			if (string.IsNullOrEmpty(username))
			{
				MsgBox.ShowInfo("Please enter user name");
				return;
			}
			if (string.IsNullOrEmpty(password))
			{
				MsgBox.ShowInfo("Please enter password");
				return;
			}
			loginInfo.Username = (cbRemenberUsername.Checked ? tbUsername.Text : "");
			loginInfo.Password = (cbRemenberPassword.Checked ? tbPassword.Text : "");
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		public new void Dispose()
		{
			base.Dispose();
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
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.tbUsername = new Krypton.Toolkit.KryptonTextBox();
            this.tbPassword = new Krypton.Toolkit.KryptonTextBox();
            this.btnLogin = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.cbRemenberUsername = new Krypton.Toolkit.KryptonCheckBox();
            this.cbRemenberPassword = new Krypton.Toolkit.KryptonCheckBox();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Palette = this.kp1;
            this.label1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "Username：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Palette = this.kp1;
            this.label2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 1;
            this.label2.Values.Text = "Password：";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(118, 33);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Palette = this.kp1;
            this.tbUsername.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbUsername.Size = new System.Drawing.Size(168, 23);
            this.tbUsername.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(118, 75);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Palette = this.kp1;
            this.tbPassword.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(168, 23);
            this.tbPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(79, 138);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Palette = this.kp1;
            this.btnLogin.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnLogin.Size = new System.Drawing.Size(100, 35);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Values.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(212, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Palette = this.kp1;
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbRemenberUsername
            // 
            this.cbRemenberUsername.Location = new System.Drawing.Point(292, 37);
            this.cbRemenberUsername.Name = "cbRemenberUsername";
            this.cbRemenberUsername.Palette = this.kp1;
            this.cbRemenberUsername.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbRemenberUsername.Size = new System.Drawing.Size(141, 20);
            this.cbRemenberUsername.TabIndex = 6;
            this.cbRemenberUsername.Values.Text = "Remember Username";
            // 
            // cbRemenberPassword
            // 
            this.cbRemenberPassword.Location = new System.Drawing.Point(292, 79);
            this.cbRemenberPassword.Name = "cbRemenberPassword";
            this.cbRemenberPassword.Palette = this.kp1;
            this.cbRemenberPassword.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cbRemenberPassword.Size = new System.Drawing.Size(138, 20);
            this.cbRemenberPassword.TabIndex = 7;
            this.cbRemenberPassword.Values.Text = "Remember Password";
            this.cbRemenberPassword.CheckedChanged += new System.EventHandler(this.cbRemenberPassword_CheckedChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(505, 188);
            this.Controls.Add(this.cbRemenberPassword);
            this.Controls.Add(this.cbRemenberUsername);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
