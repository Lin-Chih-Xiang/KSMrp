using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace nsKXMSUC
{
    public partial class frmShowProcess : Form
    {
        public frmShowProcess()
        {
            InitializeComponent();
        }

        private void frmStartProcess_Load(object sender, EventArgs e)
        {

        }
        public void ShowStr(string Msg, string Title = "", enMessageType StateColor = enMessageType.Default)
        {
            lblTitle.Text = Title;
            lblMsg.Text = Msg;
            switch (StateColor)
            {
                case enMessageType.Default:
                    lblTitle.BackColor = Color.ForestGreen;
                    lblTitle.ForeColor = Color.White;
                    break;
                case enMessageType.Primary:
                    lblTitle.BackColor = Color.DodgerBlue;
                    lblTitle.ForeColor = Color.White;
                    break;

                case enMessageType.Info:
                    lblTitle.BackColor = Color.SkyBlue;
                    lblTitle.ForeColor = Color.Black;
                    break;

                case enMessageType.Success:
                    lblTitle.BackColor = Color.ForestGreen;
                    lblTitle.ForeColor = Color.White;
                    break;

                case enMessageType.Warning:
                    lblTitle.BackColor = Color.Orange ;
                    lblTitle.ForeColor = Color.Black;
                    break;

                case enMessageType.Danger:
                    lblTitle.BackColor = Color.Red;
                    lblTitle.ForeColor = Color.White;
                    break;

            }
         }
         public void ProcessHide()
         {
             this.Hide();
         }
   
    }
  
}
