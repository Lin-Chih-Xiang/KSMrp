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
    public partial class KXMSTextMsgBox : Form
    {
        public KXMSTextMsgBox()
        {
            InitializeComponent();
        }
        static KXMSTextMsgBox MsgBox; 
        static DialogResult result = DialogResult.Cancel;

        public static DialogResult Show(string MsgBoxMessage, string MsgBoxHeader = "")
        {
            MsgBox = new KXMSTextMsgBox();
            MsgBox.txtMsg1.Text = MsgBoxMessage;
            if (String.IsNullOrEmpty(MsgBoxHeader) == false)
            {
                MsgBox.Text = MsgBoxHeader;
            }
            MsgBox.ShowDialog();
            return result;
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
        }
    }
}
