using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace CMPL_Hardness
{
    public partial class frm_MainLatest : KryptonForm
    {
        public frm_MainLatest()
        {
            InitializeComponent();
        }


		private void tsmiOpen_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Static image|*.bmp;*.jpeg;*.jpg;*.png";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					//OpenImage(openFileDialog.FileName);
				}
			}
			catch (Exception ex)
			{
				//Logger.Error(ex, "Fail to open the file！");
				//MsgBox.ShowWarning(ResourcesManager.Resources.R_Main_Message_FailedToOpenImage);
			}
		}
	}
}
