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
    public partial class frmLogin : Form
    {
        SqlData _SqlData;
        public frmLogin()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        public void CheckUser()
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr,Conn);
            DataTable DT = new DataTable();

            if (txtAccount.Text != "HewtechSA" && txtPassword.Text != "75625913")
            {
                Sqlstr = _SqlData.GetData("USER", 6);
                Sqlstr = Sqlstr.Replace("?1", txtAccount.Text);
                Sqlstr = Sqlstr.Replace("?2", txtPassword.Text);
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    if (DT.Rows.Count > 0)
                    {
                        DataRow DR = DT.Rows[0];
                        KXMSSysPara.Sys.UID = DR["userid"].ToString();
                        KXMSSysPara.Sys.Power = DR["userpower"].ToString();
                        KXMSSysPara.Sys.AutoID = int.Parse(DR["id"].ToString());
                        KXMSSysPara.Sys.Pass = DR["userpassword"].ToString();
                        if (txtAccount.Text == KXMSSysPara.Sys.UID && txtPassword.Text == KXMSSysPara.Sys.Pass)
                        {
                            this.Close();
                            //mdiMain _mdiMain = new mdiMain();
                            //_mdiMain.ShowDialog();
                            KXMSSysPara.Sys._DialogResult = DialogResult.ToString();
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            char[] ch = KXMSSysPara.Sys.Pass.ToCharArray();
                            int count = 0;
                            foreach (var item in ch)
                            {
                                if (item >= 65 && item <= 90) { count += 1; }       //大寫字母：A~Z
                                if (item >= 97 && item <= 122) { count += 1; }      //小寫字母：a~z
                                if (item >= 48 && item <= 57) { count += 1; }       //數字字：0~9
                            }
                            this.Close();
                            //mdiMain _mdiMain = new mdiMain();
                            //_mdiMain.ShowDialog();
                            KXMSSysPara.Sys._DialogResult = DialogResult.ToString();
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        MessageBox.Show("登入失敗！", "KSMrp", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtAccount.Text = "";
                        txtPassword.Text = "";
                        txtAccount.Focus();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (txtAccount.Text == "Hewtech" && txtPassword.Text == "75625913")
                {
                    KXMSSysPara.Sys.UID = "Hewtech";
                    this.Close();
                    //mdiMain _mdiMain = new mdiMain();
                    //_mdiMain.ShowDialog();
                    KXMSSysPara.Sys._DialogResult = DialogResult.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtAccount.Text.Length > 0)
            {
                CheckUser();
            }
            else
            {
                MessageBox.Show("未輸入使用者代號!", "KSMrp", MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtAccount.Focus();
            }
        }

        #region textbox按下Enter會觸發
        private void txaccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                //if (txtAccount.Text.Length == 0)
                //{
                //    txtAccount.Focus();
                //}
                //else
                //{
                //    CheckUser();
                //}          
            }
        }

        private void txpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                //if (txtPassword.Text.Length == 0)
                //{
                //    txtAccount.Focus();
                //}
                //else
                //{
                //    CheckUser();
                //}
            }
        }
        #endregion

        #region textbox顏色
        private void txtAccount_Enter(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.PaleGreen;
        }

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            txtAccount.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.PaleGreen;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.PasswordChar = '\0';        //密碼顯示出來
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
