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
    public partial class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            InitializeComponent();
            ShortcutsEnabled = true;
        }
        private string gbPlaceHolder = "";
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        public string PlaceHolder
        {
            get { return gbPlaceHolder; }
            set { gbPlaceHolder = value; showPlaceHolder(); }
        }

        private void CustomTextBox_Leave(object sender, EventArgs e)
        {
            if (this.Enabled && !this.ReadOnly) { this.BackColor = Color.White; }
            showPlaceHolder();
        }
        private void showPlaceHolder()
        {
            if (this.Text == "" && gbPlaceHolder != "" )
            {
                this.Text = gbPlaceHolder;
                if (this.Text == gbPlaceHolder)
                {
                    this.ForeColor = Color.Silver;
                }
                else
                {
                    this.ForeColor = Color.Black;
                }
            }
        }
        private void CustomTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Text != "" &&  this.Text == gbPlaceHolder)
            {
                this.Text = "";
            }
            this.ForeColor = Color.Black;
        }

        private void CustomTextBox_Enter(object sender, EventArgs e)
        {
            if (this.Enabled && !this.ReadOnly ) { this.BackColor = Color.PaleGreen; }
          
                if (this.Text == gbPlaceHolder)
                {
                    this.Text = "";
                }
               
                    this.ForeColor = Color.Black;
                
          
        }
       


       
    }
}
