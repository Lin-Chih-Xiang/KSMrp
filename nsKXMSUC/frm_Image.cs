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
    public partial class frm_Image : Form
    {
        public frm_Image(string ImageURL)
        {
            InitializeComponent();
            picImg.ImageLocation = ImageURL;
        }


        private void picImg_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_Image_Load(object sender, EventArgs e)
        {

        }

    }
}
