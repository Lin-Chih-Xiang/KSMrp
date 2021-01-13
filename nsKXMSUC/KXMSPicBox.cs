using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsKXMSUC
{
    public partial class KXMSPicBox : UserControl
    {
        public KXMSPicBox()
        {
            InitializeComponent();
           
        }

        private void picImg_Click(object sender, EventArgs e)
        {
            if (picImg.ImageLocation != "")
            {
                frm_Image vfrmImage = new frm_Image(picImg.ImageLocation);
                vfrmImage.ShowDialog();
            }
        }
        public void SetImage(Image Img)
        {
            picImg.Image = Img;
        }

        public void SetImgLocation(string ImageURL)
        {
            picImg.ImageLocation = ImageURL;
        }
        public string GetImgLocation()
        {
            return picImg.ImageLocation ;
        }
        private void KXMSPicBox_Load(object sender, EventArgs e)
        {
            
        }
    }
}
