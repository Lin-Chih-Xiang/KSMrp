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
    public partial class frmPackage : Form
    {
        SqlData _SqlData;

        public frmPackage()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        #region 判斷是否為數字
        public static bool IsNumeric(string Value)
        {
            try
            {
                int i = Convert.ToInt32(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        private void frmPackage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
            //this.Hide();
            //e.Cancel = true;
        }

        private void frmPackage_Load(object sender, EventArgs e)
        {
            //窗體起始位置
            int x = (1400 - this.Size.Width) / 2;
            int y = (1000 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            initLV1();
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                textBox2.Text = 3 + "";
            }
            else
            {
                textBox2.Text = 2 + "";
            }

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;

            LoadData();
        }
        private void SetTxtState(int ShowMode)
        {
            if (ShowMode == 1)
            {
                //編輯模式
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btndelete.Enabled = false;
                btnsave.Enabled = true;
                btncancel.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                LV1.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btndelete.Enabled = false;
                btnsave.Enabled = false;
                btncancel.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                LV1.Enabled = true;
            }
        }
        public void LoadData()
        {
            string Sqlstr = "";
            LV1.Items.Clear();
            Sqlstr = _SqlData.GetData("其他", 14);
            Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse+"");

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
                    lvitem.SubItems.Add(DR["PackageDesc"].ToString());
                    lvitem.SubItems.Add(DR["Width"].ToString());
                    lvitem.SubItems.Add(DR["Height"].ToString());
                    lvitem.SubItems.Add(DR["MaxQty"].ToString());
                    LV1.Items.Add(lvitem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SetTxtState(0);
        }

        private void LV_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            textBox5.Text = LV1.FocusedItem.SubItems[1].Text;
            textBox1.Text = LV1.FocusedItem.SubItems[2].Text;
            textBox2.Text = LV1.FocusedItem.SubItems[3].Text;
            textBox3.Text = LV1.FocusedItem.SubItems[4].Text;
            textBox4.Text = LV1.FocusedItem.SubItems[5].Text;

            btnEdit.Enabled = true;
            btndelete.Enabled = true;
        }

        #region Button事件
        private void btncancel_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            LV1.Items.Clear();
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("請選擇刪除的包裝名稱!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {  
                DataTable DT = new DataTable();
                
                if (MessageBox.Show("確認刪除[" + textBox1.Text + "]包裝?","刪除包裝", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Sqlstr = _SqlData.GetData("其他",13);
                    Sqlstr = Sqlstr.Replace("?1", textBox5.Text);
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
            SetTxtState(1);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand("", Conn);

            if (textBox1.Text.Length ==0)
            {
                MessageBox.Show("請輸入正確的包裝名稱!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            if (!IsNumeric(textBox2.Text))
            {
                MessageBox.Show("請輸入正確的資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Focus();
            }
            if (!IsNumeric(textBox3.Text))
            {
                MessageBox.Show("請輸入正確的資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox3.Focus();
            }
            if (!IsNumeric(textBox4.Text))
            {
                MessageBox.Show("請輸入正確的資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox4.Focus();
            }
            //新增
            if (textBox5.Text.Length == 0)
            {
                Sqlstr = _SqlData.GetData("其他", 11);
                Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                Sqlstr = Sqlstr.Replace("?2", textBox1.Text);
                Sqlstr = Sqlstr.Replace("?3", textBox2.Text);
                Sqlstr = Sqlstr.Replace("?4", textBox3.Text);
                Sqlstr = Sqlstr.Replace("?5", textBox4.Text);
            }
            //修改
            else
            {
                Sqlstr = _SqlData.GetData("其他", 12);
                Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                Sqlstr = Sqlstr.Replace("?2", textBox1.Text);
                Sqlstr = Sqlstr.Replace("?3", textBox2.Text);
                Sqlstr = Sqlstr.Replace("?4", textBox3.Text);
                Sqlstr = Sqlstr.Replace("?5", textBox4.Text);
                Sqlstr = Sqlstr.Replace("?6", textBox5.Text);
            }

            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();
            LoadData();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "1";
            textBox3.Text = "1";
            textBox4.Text = "";
            textBox5.Text = "";

            SetTxtState(1);
        }
        #endregion

        #region textBox顏色
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.PaleGreen;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.PaleGreen;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.PaleGreen;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.BackColor = Color.White;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.PaleGreen;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
        }
        #endregion

        #region ListView設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }
        private void initLV1()
        {
            LV1.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LV1.Columns.Add("編號", 50);
            LV1.Columns.Add("包裝說明", 118, HorizontalAlignment.Center);
            LV1.Columns.Add("寬度(燈號)", 90, HorizontalAlignment.Right);
            LV1.Columns.Add("深度(燈號)", 90, HorizontalAlignment.Right);
            LV1.Columns.Add("最大數量", 80, HorizontalAlignment.Right);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column : int
        {
            編號 = 1,
            包裝說明 = 2,
            寬度 = 3,
            深度 = 4,
            最大數量 = 5,
        }
        #endregion
    }
}
