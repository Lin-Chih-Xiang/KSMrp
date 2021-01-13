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

    public partial class KXMSMsgBox : Form
    {
        public KXMSMsgBox()
        {
            InitializeComponent();
        }
        static KXMSMsgBox thisMsgBox;
        static DialogResult result = DialogResult.Cancel;

        public static DialogResult Show(string MsgBoxMessage, string MsgBoxHeader = "", string MsgDetail = "", enMessageType MsgType = enMessageType.Info, enMessageButton MsgBtn = enMessageButton.OK)
        {
            thisMsgBox = new KXMSMsgBox();
            thisMsgBox.lblMessage.Text = MsgBoxMessage;
            if (String.IsNullOrEmpty(MsgBoxHeader) == false)
            {
                thisMsgBox.Text = MsgBoxHeader;
            }
            if (String.IsNullOrEmpty(MsgDetail))
            {
                thisMsgBox.btnDetail.Visible = false;
            }
            else
            {
                thisMsgBox.btnDetail.Visible = true;
                thisMsgBox.txtDetail.Text = MsgDetail;
            }
            switch (MsgType)
            {
                case enMessageType.Warning:
                    thisMsgBox.lblIcon.ImageIndex = 0;
                    break;
                case enMessageType.Danger:
                    thisMsgBox.lblIcon.ImageIndex = 1;
                    break;
                case enMessageType.Question:
                    thisMsgBox.lblIcon.ImageIndex = 2;
                    break;
                case enMessageType.Info:
                    thisMsgBox.lblIcon.ImageIndex = 3;
                    break;

                case enMessageType.Success :
                    thisMsgBox.lblIcon.ImageIndex = 4;
                    break;
                default:  
                    //將圖示隱藏 訊息欄位拉到與訊息欄同寬
                    thisMsgBox.lblIcon.Visible = false;
                    thisMsgBox.lblMessage.Left = 0;
                    thisMsgBox.lblMessage.Width = thisMsgBox.Width;
                    thisMsgBox.txtDetail.Left = 0;
                    thisMsgBox.txtDetail.Height = thisMsgBox.lblMessage.Height;
                    thisMsgBox.txtDetail.Width = thisMsgBox.Width;
                    break;
            }
            switch (MsgBtn)
            {
                case enMessageButton.OKCancel:
                    thisMsgBox.btnOK.Visible=true;
                    thisMsgBox.btnCancel.Visible=true;
                    break;
                case enMessageButton.OK:
                    thisMsgBox.btnOK.Left = thisMsgBox.Width/2 -32;
                    thisMsgBox.btnOK.Visible = true;
                    thisMsgBox.btnCancel.Visible = false;
                    break;
                default:
                    thisMsgBox.btnOK.Visible = true;
                    thisMsgBox.btnCancel.Visible = true;
                    break;
            }

            thisMsgBox.ShowDialog();
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

        private void KXMSMsgBox_Load(object sender, EventArgs e)
        {
            btnCancel.Focus();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            txtDetail.Visible = !txtDetail.Visible;
        }
        
    }
}
