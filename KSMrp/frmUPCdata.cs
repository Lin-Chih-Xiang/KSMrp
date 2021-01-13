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
    public partial class frmUPCdata : Form
    {
        public string Status;
        SqlData _SqlData;
        public frmUPCdata()
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
            OleDbCommand oleCmd = new OleDbCommand("",Conn);
            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();
        }
        #endregion

        private void frmUPCdata_Load(object sender, EventArgs e)
        {
            Status = "Add";
            btnAddSave.Text = "新增";
        }

        private void btnAddSave_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            if (txtUPC.Text.Length != 12)
            {
                MessageBox.Show("請輸入正確的UPC料號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUPC.Focus();
                return;
            }
            if (txtNub.Text.Length == 0)
            {
                MessageBox.Show("請輸入正確的成品料號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNub.Focus();
                return;
            }

            if (Status == "Add")
            {
                Sqlstr = _SqlData.GetData("其他", 21);
                Sqlstr = Sqlstr.Replace("?1", txtUPC.Text);
                Sqlstr = Sqlstr.Replace("?2", txtNub.Text);
                Connect(Sqlstr);
            }
            else if (Status == "Update")
            {
                Sqlstr = _SqlData.GetData("其他", 22);
                Sqlstr = Sqlstr.Replace("?1", txtNub.Text);
                Sqlstr = Sqlstr.Replace("?2", txtUPC.Text);
                Connect(Sqlstr);
            }

            txtUPC.Text = "";
            txtNub.Text = "";
            Status = "Add";
            btnAddSave.Text = "新增存檔";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string S = "";
            string Sqlstr = "";
            char[] charsToTrim = { '*', ' ', '\'' };

            if (txtUPC.Text.Length > 0)
            {
                S = txtUPC.Text.Trim(charsToTrim);
                txtUPC.Text = S;
                if (txtUPC.Text.Length == 12)
                {
                    //檢查是否有比對資料
                    Sqlstr = _SqlData.GetData("其他", 24);
                    Sqlstr = Sqlstr.Replace("?1", S);
                    DataTable DT = ConnectQuery(Sqlstr);

                    if (DT.Rows.Count == 0)
                    {
                        MessageBox.Show("資料庫無對應此UPC code的成品料號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNub.Text = "";
                        btnAddSave.Text = "新增存檔";
                        Status = "Add";
                    }
                    else
                    {
                        txtNub.Text = DT.Rows[0][0].ToString();
                        btnAddSave.Text = "修改存檔";
                        Status = "Update";
                    }
                }
                else
                {
                    MessageBox.Show("UPC code有錯誤!!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUPC.Focus();
                }
            }
            else
            {
                S = txtNub.Text.Trim(charsToTrim);
                txtNub.Text = S;
                if (S.Length > 0)
                {
                    //檢查是否有比對資料
                    Sqlstr = _SqlData.GetData("其他", 25);
                    Sqlstr = Sqlstr.Replace("?1", S);
                    DataTable DT = ConnectQuery(Sqlstr);

                    if (DT.Rows.Count == 0)
                    {
                        MessageBox.Show("資料庫無對應此成品料號的UPC code!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnAddSave.Text = "新增存檔";
                        Status = "Add";
                    }
                    else
                    {
                        txtUPC.Text = DT.Rows[0][0].ToString();
                        btnAddSave.Text = "修改存檔";
                        Status = "Update";
                    }
                }
            }
        }
    }
}
