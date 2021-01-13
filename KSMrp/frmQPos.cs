using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace KSMrp
{
    public partial class frmQPos : Form
    {
        SqlData _SqlData;
        public frmQPos()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        #region 一般SQL連線、All SQL
        private DataTable ConnectQuery(string Sqlstr)
        {
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr,Conn);
            DataTable DT = new DataTable();

            try
            {
                Conn.Open();
                DA.SelectCommand.CommandText = Sqlstr;
                DA.Fill(DT);
                Conn.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            return DT;
        }

        private void Connect(string Sqlstr)
        {
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand("", Conn);
            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();
        }
        #endregion

        #region ListView 設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }
        private void initLV1()
        {
            LV1.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LV1.Columns.Add("庫位", 60, HorizontalAlignment.Center);
            LV1.Columns.Add("料號", 200, HorizontalAlignment.Center);
            LV1.Columns.Add("儲位", 128, HorizontalAlignment.Center);
            LV1.Columns.Add("庫存量", 80, HorizontalAlignment.Right);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column : int
        {
            庫位 = 1,
            料號 = 2,
            儲位 = 3,
            庫存量 = 4,
        }
        #endregion
        
        private void frmQPos_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
        }

        private void frmQPos_Load(object sender, EventArgs e)
        {
            //窗體起始位置
            int x = (1200 - this.Size.Width) / 2;
            int y = (700 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)
            initLV1();
        }

        private void LoadLV()
        {
            LV1.Items.Clear();
            string sqlstr;
            string sWhere = "";

            //查詢(by料號)
            if (txtNo.Text.Length == 0 && textBox1.Text.Length != 0)
            {
                sWhere = @"WHERE pStorePos.StorePos='" + textBox1.Text + "'";       //WHERE ='XXXX' <= SQL指令
            }
            //查詢(by儲位)
            else if (txtNo.Text.Length != 0 && textBox1.Text.Length == 0)
            {
                sWhere = @"WHERE TempUSA.Mno='" + txtNo.Text + "'";
            }
            //查詢(by料號、儲位)
            else if (txtNo.Text.Length != 0 && textBox1.Text.Length != 0)
            {
                sWhere = @"WHERE pStorePos.StorePos='" + textBox1.Text + "'";
                sWhere += @"WERE TempUSA.Mno='" + txtNo.Text + "'";
            }
            sqlstr = _SqlData.GetData("其他", 38);
            sqlstr = sqlstr.Replace("?1", sWhere);                      //把SQL "?1" 取代為sWhere條件式
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(sqlstr, Conn);
            DataTable DT = new DataTable();                            //表示記憶體中資料表
            try
            {
                Conn.Open();
                DA.Fill(DT);                                       //使用 DataSet 名稱，加入或重新整理 DataTable 中指定範圍內的資料列，以符合那些在資料來源中的資料列。
                Conn.Close();
                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();     //使用預設值，初始化 ListViewItem 類別的新執行個體
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                    lvitem.SubItems.Add(DR["Mno"].ToString());
                    lvitem.SubItems.Add(DR["StorePos"].ToString());
                    lvitem.SubItems.Add(DR["Qty3"].ToString());
                    LV1.Items.Add(lvitem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LV1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            txtNo.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.料號].Text;           //FousedItem選取值,SubItem第幾個欄位(料號)
            textBox1.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.儲位].Text;        //儲位
        }

        private void btndemand_Click(object sender, EventArgs e)
        {
            LoadLV();
        }

        private void btnreorganize_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";

            //刪除temp檔
            Sqlstr = _SqlData.GetData("其他", 23);
            Connect(Sqlstr);
            //將比對資料轉入temp檔
            Sqlstr = _SqlData.GetData("其他", 40);
            Connect(Sqlstr);
            Sqlstr = _SqlData.GetData("其他", 29);
            Connect(Sqlstr);
            Sqlstr = _SqlData.GetData("其他", 30);
            Connect(Sqlstr);
            Sqlstr = _SqlData.GetData("其他", 39);
            Connect(Sqlstr);

            MessageBox.Show("資料重整完成!!", "KSMrp");
        }

        #region TextBox顏色 及 按下Enter觸發事件
        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            switch (Tb.TabIndex)
            {
                case 1:
                    txtNo.BackColor = Color.PaleGreen;
                    break;
                case 2:
                    textBox1.BackColor = Color.PaleGreen;
                    break;

            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            switch (Tb.TabIndex)
            {
                case 1:
                    txtNo.BackColor = Color.White;
                    break;
                case 2:
                    textBox1.BackColor = Color.White;
                    break;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox Tb = (TextBox)sender;

            if (e.KeyCode == Keys.Enter)
            {
                switch (Tb.TabIndex)
                {
                    case 1:
                        btndemand.PerformClick();
                        break;
                    case 2:
                        btndemand.PerformClick();
                        break;
                }
            }
        }
        #endregion

    }
}
