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
    public partial class MinusDoubleTextBox : TextBox
    {
        public MinusDoubleTextBox()
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
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar == (Char)46 || e.KeyChar == (Char)45)
            {
                
                if (e.KeyChar == (Char)46 && this.Text.IndexOf('.') >= 0)
                { //已有一個小數點
                    e.Handled = true;
                }
                else if (e.KeyChar == (Char)46 && this.Text.Length == 0)
                { //開頭是小數點
                    e.Handled = true;
                }
                else if (e.KeyChar == (Char)45 && this.Text.Length > 0)
                { //負號不在開頭
                    e.Handled = true;
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
