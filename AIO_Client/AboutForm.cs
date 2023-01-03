using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;
namespace AIO_Client
{

	public class AboutForm : KryptonForm
	{
		private IContainer components = null;

		private KryptonLabel lbCopyright;

		private KryptonLabel lbWebsite;

		private KryptonLabel lbCompanyName;
        private KryptonPalette kp1;
        private KryptonLabel lbAppName;

		public AboutForm()
		{
			InitializeComponent();
			//LoadLanguageResources();

            kp1.ResetToDefaults(true);
            kp1.BasePaletteMode = Program.palete_mode;
  	}

		private void LoadLanguageResources()
		{
			Text = ResourcesManager.Resources.R_About_Title;
			lbAppName.Text = ResourcesManager.Resources.R_About_AppName;
			lbCompanyName.Text = ResourcesManager.Resources.R_About_CompanyName;
			lbWebsite.Text = ResourcesManager.Resources.R_About_Website;
			lbCopyright.Text = ResourcesManager.Resources.R_About_Copyright;
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
            this.lbCopyright = new Krypton.Toolkit.KryptonLabel();
            this.lbWebsite = new Krypton.Toolkit.KryptonLabel();
            this.lbCompanyName = new Krypton.Toolkit.KryptonLabel();
            this.lbAppName = new Krypton.Toolkit.KryptonLabel();
            this.kp1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.SuspendLayout();
            // 
            // lbCopyright
            // 
            this.lbCopyright.Location = new System.Drawing.Point(23, 153);
            this.lbCopyright.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Palette = this.kp1;
            this.lbCopyright.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCopyright.Size = new System.Drawing.Size(254, 20);
            this.lbCopyright.TabIndex = 7;
            this.lbCopyright.Values.Text = "All Rights Reserved：©  2022 ChennaiMetco";
            // 
            // lbWebsite
            // 
            this.lbWebsite.Location = new System.Drawing.Point(23, 110);
            this.lbWebsite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbWebsite.Name = "lbWebsite";
            this.lbWebsite.Palette = this.kp1;
            this.lbWebsite.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbWebsite.Size = new System.Drawing.Size(203, 20);
            this.lbWebsite.TabIndex = 6;
            this.lbWebsite.Values.Text = "Website： www.chennaimetco.com";
            // 
            // lbCompanyName
            // 
            this.lbCompanyName.Location = new System.Drawing.Point(23, 65);
            this.lbCompanyName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCompanyName.Name = "lbCompanyName";
            this.lbCompanyName.Palette = this.kp1;
            this.lbCompanyName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbCompanyName.Size = new System.Drawing.Size(134, 20);
            this.lbCompanyName.TabIndex = 5;
            this.lbCompanyName.Values.Text = "Chennai Metco Pvt Ltd";
            // 
            // lbAppName
            // 
            this.lbAppName.Location = new System.Drawing.Point(23, 24);
            this.lbAppName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbAppName.Name = "lbAppName";
            this.lbAppName.Palette = this.kp1;
            this.lbAppName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.lbAppName.Size = new System.Drawing.Size(138, 20);
            this.lbAppName.TabIndex = 4;
            this.lbAppName.Values.Text = "Hardness Testing V1.00";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 201);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.lbWebsite);
            this.Controls.Add(this.lbCompanyName);
            this.Controls.Add(this.lbAppName);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Palette = this.kp1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Us";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void AboutForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
