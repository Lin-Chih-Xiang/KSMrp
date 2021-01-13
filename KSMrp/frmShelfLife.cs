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
using System.Text.RegularExpressions;

namespace KSMrp
{
    public partial class frmShelfLife : Form
    {
        SqlData _SqlData;

        public frmShelfLife()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        private void frmShelfLife_Load(object sender, EventArgs e)
        {
            initLV1();
            LoadData();
            //浮水印提字
            txtMno.SetWatermark("請輸入料號的前三碼");
            txtWeek.SetWatermark("請輸入存放天數");
            
        }
        
        private void frmShelfLife_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
        }

        #region ListView設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }

        private void initLV1()
        {
            
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                LV1.Clear();
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV1.Columns.Add("料號", 110, HorizontalAlignment.Left);
                LV1.Columns.Add("存放天數/月", 100, HorizontalAlignment.Center);
            }

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column : int
        {
            料號 = 1,
            存放天數 = 2,
        }
        #endregion

        #region 一般查詢連線SQL、All SQL
        private DataTable ConnectQuery(string Sqlstr)
        {
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
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

        #region Button Enable Setting
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
                txtMno.Enabled = true;
                txtWeek.Enabled = true;
                txtPK.Enabled = true;
                LV1.Enabled = false;
            }
            else 
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                txtMno.Enabled = false;
                txtWeek.Enabled = false;
                txtPK.Enabled = false;
                LV1.Enabled = true;
            }
        }
        #endregion

        private void LoadData()
        {
            string Sqlstr = "";
            LV1.Items.Clear();
            Sqlstr = _SqlData.GetData("ShelfLife", 1);
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["KTCPartNo"].ToString());
                lvitem.SubItems.Add(DR["ShelfLife"].ToString());
                lvitem.SubItems.Add(DR["PK"].ToString());
                LV1.Items.Add(lvitem);
            }

            SetTxtState(0);
        }

        private void LV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMno.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.料號].Text;
            txtWeek.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.存放天數].Text;
            txtPK.Text = LV1.FocusedItem.SubItems[3].Text;    // PK id

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        #region Button 事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtMno.Text = "";
            txtWeek.Text = "";
            txtPK.Text = "";

            SetTxtState(1);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            int SHStoreCount;
            if (txtMno.Text.Trim(' ') == "")
            {
                //空白庫位不檢查
            }
            else
            {
                Sqlstr = _SqlData.GetData("ShelfLife", 2);
                Sqlstr = Sqlstr.Replace("?1", txtMno.Text);
                DataTable DT = ConnectQuery(Sqlstr);

                SHStoreCount = int.Parse(DT.Rows[0][0].ToString());
                if (SHStoreCount > 0)
                {
                    if (MessageBox.Show("[" + txtMno.Text + "]此料號尚有庫存，確定要修改!!", "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    { return; }
                }
            }

            SetTxtState(1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LV1.Items.Clear();
            string Sqlstr = "";

            if (txtMno.Text.Length == 0)
            {
                MessageBox.Show("請選擇刪除的ShelfLife設定!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //檢查庫位庫存
                int SHStoreCount = 0;
                if (txtMno.Text.Trim(' ') == "")
                {
                    //空白庫位不檢查
                }
                else
                {
                    Sqlstr = _SqlData.GetData("ShelfLife", 2);
                    Sqlstr = Sqlstr.Replace("?1", txtMno.Text);
                    DataTable DT = ConnectQuery(Sqlstr);

                    SHStoreCount = int.Parse(DT.Rows[0][0].ToString());
                    if (SHStoreCount > 0)
                    {
                        MessageBox.Show("[" + txtMno.Text + "]此料號尚有庫存，不能刪除!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        LoadData();
                        return;
                    }
                }

                if (MessageBox.Show("確定刪除[" + txtMno.Text + "]設定?", "刪除ShelfLife設定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Sqlstr = _SqlData.GetData("ShelfLife", 3);
                    Sqlstr = Sqlstr.Replace("?1", txtPK.Text);
                    Connect(Sqlstr);
                    LoadData();

                    txtMno.Text = "";
                    txtWeek.Text = "";
                    txtWeek.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";

            if (txtMno.Text.Length == 0)
            {
                MessageBox.Show("請輸入正確的料號關鍵字!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string KTCPartNo = "";
            string PK = "";
            Sqlstr = "SELECT * FROM MainShelfLife WHERE KTCPartNo = "+ txtMno.Text + "ORDER BY ShelfLife";
            DataTable DT = ConnectQuery(Sqlstr);
            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["KTCPartNo"].ToString());
                lvitem.SubItems.Add(DR["PK"].ToString());
                KTCPartNo = lvitem.SubItems[1].Text;
                PK = lvitem.SubItems[2].Text;
            }

            if (txtPK.Text.Length == 0)
            {
                if (txtMno.Text == KTCPartNo)
                {
                    MessageBox.Show("此[" + txtMno.Text + "]料號已輸入過!! \r\n" + "請重新輸入!!","KSMRP",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    //新增
                    Sqlstr = _SqlData.GetData("ShelfLife", 4);
                    Sqlstr = Sqlstr.Replace("?1", txtMno.Text);
                    Sqlstr = Sqlstr.Replace("?2", txtWeek.Text);
                }
            }
            else
            {
                if (txtMno.Text == KTCPartNo && txtPK.Text != PK)
                {
                    MessageBox.Show("此[" + txtMno.Text + "]料號已輸入過!! \r\n" + "請重新輸入!!", "KSMRP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    //修改
                    Sqlstr = _SqlData.GetData("ShelfLife", 5);
                    Sqlstr = Sqlstr.Replace("?1", txtMno.Text);
                    Sqlstr = Sqlstr.Replace("?2", txtWeek.Text);
                    Sqlstr = Sqlstr.Replace("?3", txtPK.Text);
                }
            }
            Connect(Sqlstr);
            LoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadData();

            txtWeek.Text = "";
            txtMno.Text = "";
            txtPK.Text = "";
        }
        #endregion
        
    }
}
