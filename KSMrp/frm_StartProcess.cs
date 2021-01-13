using System;
using System.Windows.Forms;

namespace KSMrp
{
    public partial class frm_StartProcess : Form
    {
        public frm_StartProcess()
        {
            InitializeComponent();
        }

        private void frmStartProcess_Load(object sender, EventArgs e)
        {

        }
         public void ShowStr(string Str){
             LB.Items.Add(Str);
         }
   
    }
}
