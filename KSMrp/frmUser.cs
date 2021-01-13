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
    public partial class frmUser : Form
    {
        SqlData _SqlData;
        public string SaveStatus = "";
        public frmUser()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            //窗體起始位置
            int x = (1200 - this.Size.Width) / 2;
            int y = (700 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            ButtonStatus("clear");
        }

        #region ButtonStatus
        private void ButtonStatus(string Status)
        {
            switch (Status)
            {
                case "add":
                    SaveStatus = "add";
                    TpbtnAdd.Enabled = false;
                    TpbtnEdit.Enabled = false;
                    TpbtnDelete.Enabled = false;
                    TpbtnSave.Enabled = true;

                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    LV1.Enabled = true;
                    cbName.Enabled = false;
                    TextboxBackColor("White");
                    break;
                case "edit":
                    SaveStatus = "edit";
                    TpbtnAdd.Enabled = false;
                    TpbtnEdit.Enabled = false;
                    TpbtnDelete.Enabled = false;
                    TpbtnSave.Enabled = true;

                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    LV1.Enabled = true;
                    cbName.Enabled = false;
                    TextboxBackColor("White");
                    break;
                case "save":
                    SaveStatus = "";
                    ShowcbName();
                    TpbtnAdd.Enabled = true;
                    TpbtnEdit.Enabled = true;
                    TpbtnDelete.Enabled = true;
                    TpbtnSave.Enabled = false;
                    ShowData(0);

                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    LV1.Enabled = false;
                    cbName.Enabled = true;
                    TextboxBackColor("Control");
                    break;
                case "clear":
                    SaveStatus = "";
                    ShowcbName();
                    TpbtnAdd.Enabled = true;
                    TpbtnEdit.Enabled = true;
                    TpbtnDelete.Enabled = true;
                    TpbtnSave.Enabled = false;
                    ShowData(0);

                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    LV1.Enabled = false;
                    cbName.Enabled = true;
                    TextboxBackColor("Control");
                    break;
            }
        }
        #endregion

        private string DataCheck()
        {
            string UserPower = KXMSSysPara.Sys.Power;
             UserPower = "";
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("請輸入姓名", "KSMrp", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return textBox1.Text;
            }
            else if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("請輸入登入帳號", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Focus();
                return textBox2.Text;
            }
            else if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("請輸入密碼", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox3.Focus();
                return textBox3.Text;
            }
            else
            {
                if (textBox3.Text != textBox4.Text)
                {
                    MessageBox.Show("登入密碼與驗證密碼不相符", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox3.Focus();
                    textBox3.Text = "";
                    textBox4.Text = "";
                    return textBox3.Text;
                }
                
            }
            foreach (ListViewItem item in LV1.Items)
            {
                if (item.Checked)
                {
                    UserPower = UserPower + "1";
                }
                else
                {
                    UserPower = UserPower + "0";
                }
            }
            return UserPower;
        }
        
        private void ShowData(int id)
        {
            string Sqlstr = "";
            string S1;
            int i = 0;

            Sqlstr = _SqlData.GetData("USER", 5);
            Sqlstr = Sqlstr.Replace("?1", id +"");

            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();
            try
            {
                Conn.Open();
                DA.Fill(DT);
                Conn.Close();

                foreach (DataRow DR in DT.Rows)
                {
                    textBox1.Text = DR["username"].ToString();
                    textBox2.Text = DR["userid"].ToString();
                    textBox3.Text = DR["userpassword"].ToString();
                    textBox4.Text = DR["userpassword"].ToString();
                    textBox5.Text = DR["id"].ToString();

                    S1 = DR["userpower"].ToString();
                    if (S1.Length != 0)
                    {
                        foreach (ListViewItem item in LV1.Items)
                        {
                            if (i >= S1.Length) { item.Checked = false;  continue; }
                            i = i + 1;
                            if (S1.Substring((i - 1), 1) == "1")
                            {
                                item.Checked = true;
                            }
                            else
                            {
                                item.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        #region Button事件
        private void TpbtnAdd_Click(object sender, EventArgs e)
        {
            int i;
            TextBox[] textbox = { textBox1, textBox2, textBox3, textBox4 };
            for (i = 0; i <= 3; i++)
            {
                textbox[i].Text = "";
            }

            foreach (ListViewItem item in LV1.Items)
            {
                item.Checked = false;
            }

            textbox[0].Focus();
            ButtonStatus("add");
        }

        private void TpbtnEdit_Click(object sender, EventArgs e)
        {
            ButtonStatus("edit");
        }

        private void TpbtnDelete_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand("",Conn);
            if (MessageBox.Show("確定要刪除嗎?", "KSMrp", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    Sqlstr = _SqlData.GetData("USER", 3);
                    Sqlstr = Sqlstr.Replace("?1", textBox5.Text);
                    Conn.Open();
                    oleCmd.CommandText = Sqlstr;
                    oleCmd.ExecuteNonQuery();
                   // Conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("使用者已有交易紀錄，不能刪除", "刪除使用者", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    if (Conn.State == ConnectionState.Open) { Conn.Close(); }
                }
                
                ButtonStatus("clear");
                
            }
        }

        private void TpbtnSave_Click(object sender, EventArgs e)
        {
            string UserPower = "";
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand("",Conn);
            UserPower = DataCheck();
            if (UserPower.Length == 0)
            {
                return;
            }

            switch (SaveStatus)
            {
                case "add":
                    Sqlstr = _SqlData.GetData("USER", 1);
                    Sqlstr = Sqlstr.Replace("?1", textBox2.Text);
                    Sqlstr = Sqlstr.Replace("?2", textBox1.Text);
                    Sqlstr = Sqlstr.Replace("?3", textBox3.Text);
                    Sqlstr = Sqlstr.Replace("?4", UserPower);
                    Conn.Open();
                    oleCmd.CommandText = Sqlstr;
                    oleCmd.ExecuteNonQuery();
                    Conn.Close();
                    break;
                case "edit":
                    Sqlstr = _SqlData.GetData("USER", 2);
                    Sqlstr = Sqlstr.Replace("?1", textBox2.Text);
                    Sqlstr = Sqlstr.Replace("?2", textBox1.Text);
                    Sqlstr = Sqlstr.Replace("?3", textBox3.Text);
                    Sqlstr = Sqlstr.Replace("?4", UserPower);
                    Sqlstr = Sqlstr.Replace("?5", textBox5.Text);
                    Conn.Open();
                    oleCmd.CommandText = Sqlstr;
                    oleCmd.ExecuteNonQuery();
                    Conn.Close();
                    break;
            }

            ButtonStatus("save");
        }

        private void TpbtnCancel_Click(object sender, EventArgs e)
        {
            ButtonStatus("clear");
        }
        #endregion

        private void ShowcbName()
        {
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("USER", 4);
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();
            try
            {
                Conn.Open();
                DA.Fill(DT);
                Conn.Close();
                cbName.Items.Clear();
                foreach (DataRow DR in DT.Rows)
                {
                    ComboboxItem vitem0 = new ComboboxItem();
                    vitem0.Text = DR["username"].ToString();
                    vitem0.Value = DR["id"].ToString();
                    cbName.Items.Add(vitem0);

                    //textBox5.Text = DR["id"].ToString();
                    //textBox1.Text = DR["username"].ToString();
                    //textBox2.Text = DR["userid"].ToString();
                    //textBox3.Text = DR["userpassword"].ToString();
                    //textBox4.Text = DR["userpassword"].ToString();
                    if (cbName.Items.Count == 0)
                    {
                        return;
                    }
                    else
                    {
                        cbName.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id;
            if (cbName.SelectedItem == null)
            {
                id = 0;
            }
            else
            {
                int.TryParse((cbName.SelectedItem as ComboboxItem).Value, out id);
            }
            ShowData(id);
        }

        #region Textbox顏色 及 按下Enter觸發事件
        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            switch (Tb.TabIndex)
            {
                case 1:
                    textBox1.BackColor = Color.PaleGreen;
                    break;
                case 2:
                    textBox2.BackColor = Color.PaleGreen;
                    break;
                case 3:
                    textBox3.BackColor = Color.PaleGreen;
                    break;
                case 4:
                    textBox4.BackColor = Color.PaleGreen;
                    break;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            switch (Tb.TabIndex)
            {
                case 1:
                    textBox1.BackColor = Color.White;
                    break;
                case 2:
                    textBox2.BackColor = Color.White;
                    break;
                case 3:
                    textBox3.BackColor = Color.White;
                    break;
                case 4:
                    textBox4.BackColor = Color.White;
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
                        textBox2.Focus();
                        break;
                    case 2:
                        textBox3.Focus();
                        break;
                    case 3:
                        textBox4.Focus();
                        break;
                    case 4:
                        TpbtnSave.PerformClick();
                        break;
                }
            }
        }

        private void TextboxBackColor(string S)
        {
            switch (S)
            {
                case "Control":
                    textBox1.BackColor = SystemColors.Control;
                    textBox2.BackColor = SystemColors.Control;
                    textBox3.BackColor = SystemColors.Control;
                    textBox4.BackColor = SystemColors.Control;
                    break;
                case "White":
                    textBox1.BackColor = Color.White;
                    textBox2.BackColor = Color.White;
                    textBox3.BackColor = Color.White;
                    textBox4.BackColor = Color.White;
                    break;
            }
        }
        #endregion

    }
}
