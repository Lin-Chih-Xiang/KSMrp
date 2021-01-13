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
    public partial class DoubleTextBox : CustomTextBox
    {
        public DoubleTextBox()
        {
            InitializeComponent();
            this.ShortcutsEnabled = false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
      
        private void DoubleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar == (Char)46)
            {
                if (e.KeyChar == (Char)46 && this.Text.IndexOf('.') >= 0)
                {
                    //小數點的判斷
                    e.Handled = true;
                }
                else if (e.KeyChar == (Char)46 && this.Text.Length == 0)
                {
                    //開頭不能是小數點
                    e.Handled = true;
                }
                else if (Char.IsDigit(e.KeyChar) && this.Text.IndexOf('.') >=0 && (this.Text.Length > this.Text.IndexOf('.') + 3))
                {
                    //小數點後最多三碼數字 
                    if (this.SelectionLength == this.Text.Length) { 
                        //假如全選，則可輸入
                        e.Handled = false; 
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }


       
    }
}
