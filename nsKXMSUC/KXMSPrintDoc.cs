using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zen.Barcode;


namespace nsKXMSUC
{
    public partial class KXMSPrintDoc : System.Drawing.Printing.PrintDocument
    {
        string gbPrintText1 = "";
        string gbPrintText2 = "";
        string gbPrintBarcodeText = "";
        int gbPrintCount = 0;
        int gbTargetPrintCount = 1;
        int gbLabelWidth = 244;
        int gbLabelHeight = 102;

        public KXMSPrintDoc(int PaperWidth, int PaperHeight, string PrinterName, string DocName = "Label")
        {
            InitializeComponent();

            //定義紙張大小 長6.2 cm = 2.44 inch 寬 2.6 = 1.02 inch  ( 1 cm = 0.393 inch)
            this.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("label", PaperWidth, PaperHeight);
            this.PrinterSettings.PrinterName = PrinterName;
            this.DocumentName = DocName;
            gbLabelWidth = PaperWidth;
            gbLabelHeight = PaperHeight;



        }
        public bool SetPreviewPrintValue(string Text1, string BarcodeText, int PrintCount)
        {
            gbPrintText1 = Text1;
            gbPrintBarcodeText = BarcodeText;
            gbPrintCount = 0;
            gbTargetPrintCount = PrintCount;

            return true;
        }
        public bool PrintLabel(string Text1,string Text2, string BarcodeText, int PrintCount, out string ErrMsg)
        {
            bool vResult = true;
            ErrMsg = "";
            try
            {
                gbPrintText1 = Text1;
                gbPrintText2 = Text2;
                gbPrintBarcodeText = BarcodeText;
                gbPrintCount = 0;
                gbTargetPrintCount = PrintCount;

                this.Print();
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                vResult = false;
            }
            return vResult;
        }

        public string PrinterName
        {
            get { return this.PrinterSettings.PrinterName; }
            set { this.PrinterSettings.PrinterName = value; }
        }
        public string DocName
        {
            get { return this.DocumentName; }
            set { this.DocumentName = value; }
        }
        public int PrintCount
        {
            get { return gbTargetPrintCount; }
            set { gbTargetPrintCount = value; }
        }

        public string PrintBarcodeText
        {
            get { return gbPrintBarcodeText; }
            set { gbPrintBarcodeText = value; }
        }
        public string PrintTxt1
        {
            get { return gbPrintText1; }
            set { gbPrintText1 = value; }
        }
        public string PrintTxt2
        {
            get { return gbPrintText2; }
            set { gbPrintText2 = value; }
        }
        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //主要列印樣式
            Graphics g = e.Graphics;
            if (gbPrintText1.Length <= 30)
            {
                g.DrawString(gbPrintText1, new Font("微軟正黑體", 8, FontStyle.Bold), Brushes.Black, new RectangleF(20, 10, gbLabelWidth - 30, 50), new StringFormat());
            }
            else 
            {
                g.DrawString(gbPrintText1, new Font("微軟正黑體", 8, FontStyle.Bold), Brushes.Black, new RectangleF(20, 10, gbLabelWidth - 30, 50), new StringFormat());
            }


            if (gbPrintText2.Length <= 40)
            {
                g.DrawString(gbPrintText2, new Font("微軟正黑體", 11, FontStyle.Bold), Brushes.Black, new RectangleF(20, 20, gbLabelWidth - 30, 50), new StringFormat());
            }
            else
            {
                //只印前20個字
                g.DrawString(gbPrintText2.Substring(0, 40), new Font("微軟正黑體", 11, FontStyle.Bold), Brushes.Black, new RectangleF(20, 20, gbLabelWidth - 30, 50), new StringFormat());
            }
            
            Code128BarcodeDraw BD = new Code128BarcodeDraw(Code128Checksum.Instance);
            var vBarcodeImg = BD.Draw(gbPrintText1, 30, 1);
            int vImageWidth;
            if (vBarcodeImg.Width > (gbLabelWidth - 30))
            {
                vImageWidth = gbLabelWidth - 30;
            }
            else
            {
                vImageWidth = vBarcodeImg.Width;
            }
            g.DrawImage(vBarcodeImg, 20, 65, vImageWidth, vBarcodeImg.Height);
            gbPrintCount += 1;

            //HasMorePages true 表示還有下一頁，會再次呼叫PrintPage
            if (gbTargetPrintCount > gbPrintCount)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
    }
}
