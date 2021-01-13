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
    public partial class frmStorePos : Form
    {
        public string Status;
        SqlData _SqlData;
        public frmStorePos()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        #region 一般SQL連線、All SQL
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

        private void frmStorePos_Load(object sender, EventArgs e)
        {
            Status = "Add";
            btnAddSave.Text = "新增";
        }
        
        private void btnAddSave_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";

            if (txtNub.Text.Length == 0)
            {
                MessageBox.Show("請輸入正確的料號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNub.Focus();
                return;
            }
            if (txtStoreHouse.Text.Length == 0)
            {
                MessageBox.Show("請輸入正確的平置倉儲位!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStoreHouse.Focus();
                return;
            }

            if (Status == "Add")
            {
                Sqlstr = _SqlData.GetData("其他", 41);
                Sqlstr = Sqlstr.Replace("?1", txtNub.Text);
                Sqlstr = Sqlstr.Replace("?2", txtStoreHouse.Text);
                Connect(Sqlstr);
            }
            else if (Status == "Update")
            {
                Sqlstr = _SqlData.GetData("其他", 42);
                Sqlstr = Sqlstr.Replace("?1", txtStoreHouse.Text);
                Sqlstr = Sqlstr.Replace("?2", txtNub.Text);
                Connect(Sqlstr);
            }

            txtNub.Text = "";
            txtStoreHouse.Text = "";
            txtNub.Focus();
            Status = "Add";
            btnAddSave.Text = "新增存檔";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            string S = "";
            char[] charsTrim = { '*', ' ', '\'' };

            S = txtNub.Text.Trim(charsTrim);
            txtNub.Text = S;
            if (S.Length != 0)
            {
                //檢查是否有比對資料
                Sqlstr = _SqlData.GetData("其他", 44);
                Sqlstr = Sqlstr.Replace("?1", S);
                DataTable DT = ConnectQuery(Sqlstr);

                if (DT.Rows.Count == 0)
                {
                    MessageBox.Show("資料庫無對應此料號的平置倉庫位!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtStoreHouse.Text = "";
                    btnAddSave.Text = "新增存檔";
                    Status = "Add";
                }
                else
                {
                    txtStoreHouse.Text = DT.Rows[0][0].ToString();
                    btnAddSave.Text = "修改存檔";
                    Status = "Update";
                }
            }
            else
            {
                MessageBox.Show("請輸入料號!!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNub.Focus();
            }
        }
    }
}
