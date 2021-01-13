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
    public partial class frmPassword : Form
    {
        SqlData _SqlData;
        public frmPassword()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        private void Connect(string Sqlstr)
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
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";

            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("請輸入原始密碼!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return;
            }
            else if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("請輸入新的密碼!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Focus();
                return;
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("新的密碼與驗證密碼不相符!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                textBox2.Text = "";
                textBox3.Text = "";
                return;
            }

            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                if (textBox1.Text != KXMSSysPara.Sys.Pass)
                {
                    Sqlstr = _SqlData.GetData("USER", 6);
                    Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.UID);
                    Sqlstr = Sqlstr.Replace("?2", textBox2.Text);
                    Connect(Sqlstr);
                    MessageBox.Show("原始密碼輸入錯誤!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else if (textBox1.Text == KXMSSysPara.Sys.Pass)
                {
                    Sqlstr = _SqlData.GetData("USER", 7);
                    Sqlstr = Sqlstr.Replace("?1", textBox2.Text);
                    Sqlstr = Sqlstr.Replace("?2", KXMSSysPara.Sys.AutoID + "");
                    Connect(Sqlstr);
                    MessageBox.Show("修改完成!!", "KSMrp");
                    KXMSSysPara.Sys.Pass = textBox2.Text;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox1.Focus();
                    this.Close();
                }
            }
        }

        #region textBox 顏色
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

        #endregion

        #region textbox按下enter會觸發
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
        #endregion

    }  
}
