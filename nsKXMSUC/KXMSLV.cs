using System;
using System.Drawing;
using System.Windows.Forms;

namespace nsKXMSUC
{
    public partial class KXMSLV : ListView
    {
        public KXMSLV()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //  this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.View = System.Windows.Forms.View.Details;
            this.FullRowSelect = true;
            this.GridLines = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
        }
        protected override void OnNotifyMessage(Message m)
        {

            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
        public string GetColWidth()
        {
            //儲存LV寬度
            string vColStr = "";

            for (int i = 0; i < this.Columns.Count; i++)
            {
                vColStr += "," + this.Columns[i].Width;
            }
            //去除開頭的逗號
            if (vColStr.Length > 0)
            {
                vColStr = vColStr.Substring(1);
            }
            return vColStr;
        }
        /// <summary>
        /// 設定欄位寬度
        /// </summary>
        /// <param name="ColWidthStr">欄位寬度字串(ex: 100,0,120,80,60)</param>
        /// <returns></returns>
        public void SetColWidth(string ColWidthStr)
        {
            if (ColWidthStr.Length == 0)
            {
                return;
            }
            //去除開頭逗號
            if (ColWidthStr.Substring(0, 1) == ",")
            {
                ColWidthStr = ColWidthStr.Substring(1);
            }
            //分析欄寬
            string[] vCW = ColWidthStr.Split(',');

            int vColLen = 0;
            if (vCW.Length < this.Columns.Count)
            {
                //兩者取最小 (ColWidthStr資料 欄位不足，剩餘的保持原程式設定)
                vColLen = vCW.Length;
            }
            else
            {
                vColLen = this.Columns.Count;
            }
            //設定LV 寬度
            for (int i = 0; i < vColLen; i++)
            {
                this.Columns[i].Width = int.Parse(vCW[i]);
            }

        }
        protected void KXMSLV_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                //勾選變色
                e.Item.BackColor = Color.FromArgb(192, 255, 192);
                //勾選標記
                e.Item.Text = " ";
            }
            else
            {
                //取消勾選變色
                e.Item.BackColor = Color.FromArgb(255, 255, 255);
                e.Item.Text = "";
            }
        }
        protected void KXMSLV_Click(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count > 1)
            {
                foreach (ListViewItem vItem in this.SelectedItems)
                {
                    if (vItem.Text == "")
                    {
                        vItem.Checked = false;
                    }
                    else
                    {
                        vItem.Checked = true;
                    }
                }
            }
        }
        private void KXMSLV_ItemSelectionChange(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if (e.Item.Checked == false)
                {
                    e.Item.Text = " ";
                }
            }
            else
            {
                e.Item.Text = "";
            }
        }
        private void KXMSLV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int vLastSelLVCol;
            ListView LV = (ListView)sender;
            if (LV.Tag == null) { vLastSelLVCol = -1; }
            else
            {
                int.TryParse(((ListView)sender).Tag.ToString(), out vLastSelLVCol);
            }
            if (e.Column == 0)
            {
                if (LV.Items.Count > 0)
                {
                    bool vCheck = LV.Items[0].Checked;
                    foreach (ListViewItem vItem in LV.Items)
                    {
                        vItem.Checked = !vCheck;
                    }
                }
            }
            else
            {
                if (e.Column == vLastSelLVCol)
                {
                    LV.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Descending, (int)LV.Columns[e.Column].TextAlign);
                    LV.Sort();
                    LV.Tag = -1;
                }
                else
                {
                    LV.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending, (int)LV.Columns[e.Column].TextAlign);
                    LV.Sort();
                    LV.Tag = e.Column;
                }
            }
        }
    }

    public class ListViewItemComparer : System.Collections.IComparer
    {
        int gbColumn;
        SortOrder gbSortType;
        int gbDataType; //1 數字 2 文字
        public ListViewItemComparer()
        {
            gbColumn = 0;
            gbSortType = SortOrder.Ascending;
            gbDataType = 0;
        }

        public ListViewItemComparer(int column)
        {
            gbColumn = column;
            gbSortType = SortOrder.Ascending;
            gbDataType = 0;
        }
        public ListViewItemComparer(int column, SortOrder SortType, int DataType)
        {
            gbColumn = column;
            gbSortType = SortType;
            gbDataType = DataType;
        }
        public int Compare(object x, object y)
        {
            if (gbSortType == SortOrder.Ascending)
            {
                if (gbDataType == 1)
                {
                    return NumberCompare(decimal.Parse(((ListViewItem)x).SubItems[gbColumn].Text), decimal.Parse(((ListViewItem)y).SubItems[gbColumn].Text));
                }
                else
                {
                    return String.Compare(((ListViewItem)x).SubItems[gbColumn].Text, ((ListViewItem)y).SubItems[gbColumn].Text);
                }
            }
            else
            {
                if (gbDataType == 1)
                {
                    return NumberCompare(decimal.Parse(((ListViewItem)y).SubItems[gbColumn].Text), decimal.Parse(((ListViewItem)x).SubItems[gbColumn].Text));
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[gbColumn].Text, ((ListViewItem)x).SubItems[gbColumn].Text);
                }
            }
        }

        private int NumberCompare(decimal x, decimal y)
        {
            if (x > y) { return 1; }
            else if (x == y) { return 0; }
            else { return -1; }
        }
    }


}
