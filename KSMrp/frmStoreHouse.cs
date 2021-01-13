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
    public partial class frmStoreHouse : Form
    {
        SqlData _SqlData;
        public frmStoreHouse()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        private void frmStoreHouse_Load(object sender, EventArgs e)
        {
            //窗體起始位置
            int x = (1300 - this.Size.Width) / 2;
            int y = (800 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            initLV1();
            LoadData();
        }

        private void frmStoreHouse_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
            //this.Hide();
            //e.Cancel = true;
        }

        private void SetTxtState(int ShowMode)
        {
            if (ShowMode == 1)
            {
                //編輯模式
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                LV1.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                LV1.Enabled = true;
            }
        }

        #region LsitView 設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }
        private void initLV1()
        {
            LV1.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LV1.Columns.Add("編號", 60, HorizontalAlignment.Right);
            LV1.Columns.Add("庫位名稱", 172, HorizontalAlignment.Center);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column : int
        {
            編號 = 1,
            庫位名稱 = 2,
        }
        #endregion

        public void LoadData()
        {
            string Sqlstr = "";
            LV1.Items.Clear();
            Sqlstr = _SqlData.GetData("其他", 4);
            //Sqlstr = Sqlstr.Replace("?1", _Sys.WareHouse + "");

            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
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
                    lvitem.SubItems.Add(DR["id"].ToString());
                    lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                    LV1.Items.Add(lvitem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SetTxtState(0);
        }

        private void LV1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            textBox2.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.編號].Text;
            textBox1.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.庫位名稱].Text;

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        #region Button事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

            SetTxtState(1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LV1.Items.Clear();
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();                            //表示記憶體中資料表
            
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("請選擇刪除的庫位!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (textBox1.Text == "T3")
                {
                    MessageBox.Show("[T3]此庫位不能刪除!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    return;
                }
                //檢查庫位庫存
                int SHStoreCount = 0;
                if (textBox1.Text.Trim(' ') == "")
                {
                    //空白庫位不檢查
                }
                else
                {
                    Sqlstr = _SqlData.GetData("其他", 5);
                    Sqlstr = Sqlstr.Replace("?1", textBox1.Text);

                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    SHStoreCount = int.Parse(DT.Rows[0][0].ToString());

                    if (SHStoreCount > 0)
                    {
                        MessageBox.Show("["+textBox1.Text+"]庫位尚有庫存，不能刪除!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    LoadData();
                }

                if (MessageBox.Show("確定刪除[" + textBox1.Text + "]庫位?", "刪除庫位", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Sqlstr = _SqlData.GetData("其他",3);
                    Sqlstr = Sqlstr.Replace("?1", textBox2.Text);
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();
                    LoadData();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();                            //表示記憶體中資料表

            if (textBox1.Text == "T3")
            {
                MessageBox.Show("[T3]此庫位不能修改!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                return;
            }
            int SHStoreCount;
            if (textBox1.Text.Trim(' ') == "")
            {
                //空白庫位不檢查
            }
            else
            {
                Sqlstr = _SqlData.GetData("其他", 5);
                Sqlstr = Sqlstr.Replace("?1", textBox1.Text);

                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    SHStoreCount = int.Parse(DT.Rows[0][0].ToString());
                    if (SHStoreCount > 0)
                    {
                        MessageBox.Show("[" + textBox1.Text + "]庫位尚有庫存，不能修改!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                SetTxtState(1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand("", Conn);
          
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("請輸入正確的庫位名稱!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (textBox2.Text.Length ==0)
            {
                //新增
                Sqlstr = _SqlData.GetData("其他",1);
                Sqlstr = Sqlstr.Replace("?1",textBox1.Text);
            }
            else
            {
                //修改
                Sqlstr = _SqlData.GetData("其他", 2);
                Sqlstr = Sqlstr.Replace("?1", textBox1.Text);
                Sqlstr = Sqlstr.Replace("?2", textBox2.Text);
            }

            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();
            LoadData();
        }
        #endregion

        #region textbox顏色
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.PaleGreen;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }
        #endregion

    }
}
