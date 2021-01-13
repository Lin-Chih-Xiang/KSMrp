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
    public partial class frmOutM : Form
    {
        SqlData _SqlData;
        modCtrl _modCtrl;
        string Mno1 = "";
        bool IsAuto;                    //是否電腦選
        //private int SelLVicItemIndex;
        public frmOutM()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
            _modCtrl = new modCtrl();
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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

        #region 辨別是否為數字及英文字母
        public  bool IsNumeric(string Value)
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

        public  bool IsEnglish(string Value)
        {
            try
            {
                Regex reg1 = new Regex(@"^[A-Za-z]+$");
                return reg1.IsMatch(Value);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        private void frmOutM_Load(object sender, EventArgs e)
        {
            //窗體起始位置
            int x = (2600 - this.Size.Width) / 2;
            int y = (1000 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            //WareHouse 1、2 Listview 設定
            if (KXMSSysPara.Sys.WareHouse == 1 || KXMSSysPara.Sys.WareHouse ==2)
            { initLV1(); }
            else { initLVic(); }
            
            this.Width = 535;
            this.Height = 279;
            string Sqlstr ="";
            DtpOut.Value = DateTime.Now;
            IsAuto = true;

            //改變視窗排列================================================
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                this.Width = 535;
                this.Height = 279;
                txtNubP1.Select();
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                //GroupBox(1);
                //groupBox5.Location = new System.Drawing.Point(518, 31);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                this.Width = 535;
                this.Height = 279;
                groupBox1.Visible = false;
                groupBox3.Visible = false;
                //GroupBox(2);
                groupBox2.Location = new System.Drawing.Point(27, 39);
                txtNbP2.Select();
                //groupBox5.Location = new System.Drawing.Point(518, 31);
            }
            else 
            {
                this.Width = 1050;
                this.Height = 675;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                //GroupBox(3);
                groupBox3.Location = new System.Drawing.Point(27, 39);
                LV1.Visible = false;
                chkPcs.Visible = false;
                cmdOut.Visible = false;
                cmdOutInsert.Visible = false;
                txtItemNbP3.Select();
                //groupBox5.Location = new System.Drawing.Point(890, 31);
            }

            //帶出基本資料===============================================
            //找出庫位
            Sqlstr = _SqlData.GetData("其他",4);          //找出全部庫位
            DataTable DT = ConnectQuery(Sqlstr);
            cbDataP0.Items.Clear();
            foreach (DataRow DR in DT.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR["StoreHouse"].ToString();
                vitem0.Value = DR["id"].ToString();
                cbDataP0.Items.Add(vitem0);

                if (cbDataP0.Items.Count == 0)
                {
                    return;
                }
                else
                {
                    cbDataP0.SelectedIndex = 0;
                }
            }
            //找出包裝
            Sqlstr = _SqlData.GetData("其他",14);                         //查詢包裝全部
            Sqlstr = Sqlstr.Replace("?1",KXMSSysPara.Sys.WareHouse +"");
            DataTable DT0 = ConnectQuery(Sqlstr);

            //設定初始值
            DtpOut.Value = DateTime.Now;
            foreach (DataRow DR0 in DT0.Rows)
            {
                ComboboxItem lvitem0 = new ComboboxItem();
                //半成品倉
                if (KXMSSysPara.Sys.WareHouse == 1)
                {
                    lvitem0.Text = DR0["PackageDesc"].ToString();
                    lvitem0.Value = DR0["id"].ToString();
                    cbDataP1.Items.Add(lvitem0);
                    cbDataP1.Text = KXMSSysPara.Sys.DefaultPackage;
                    cbDataP0.Text = "T3";
                }
                //成品倉
                else if (KXMSSysPara.Sys.WareHouse == 2)
                {
                    lvitem0.Text = DR0["PackageDesc"].ToString();
                    lvitem0.Value = DR0["id"].ToString();
                    cbDataP2.Items.Add(lvitem0);
                    cbDataP2.Text = KXMSSysPara.Sys.DefaultPackage;
                    cbDataP0.Text = "T3";
                    txtID.Text = DR0["id"].ToString();
                }
                //IC倉
                else if (KXMSSysPara.Sys.WareHouse == 3)
                {
                    cbDataP0.Text = "T3";
                }
            }

            KXMSSysPara.Sys.CheckEmptyStore();
        }

        private void frmOutM_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
            //this.Hide();
            //e.Cancel = true;
        }

        #region button切換鈕
        private void GroupBox(int i)
        {

            switch (i)
            {
                case 1:
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    this.Width = 535;
                    this.Height = 259;
                    break;
                case 2:
                    groupBox1.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = false;
                    this.Width = 535;
                    this.Height = 259;
                    break;
                case 3:
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    this.Width = 925;
                    this.Height = 580;
                    break;
            }

        }
        private void button7_Click(object sender, EventArgs e)
        {
            GroupBox(1);
            //groupBox5.Location = new System.Drawing.Point(518, 31);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GroupBox(2);
            groupBox2.Location = new System.Drawing.Point(27,39);
            //groupBox5.Location = new System.Drawing.Point(518, 31);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GroupBox(3);
            groupBox3.Location = new System.Drawing.Point(27, 39);
            //groupBox5.Location = new System.Drawing.Point(890, 31);
        }
        #endregion

        #region ListView設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LVic.Name, LVic.GetColWidth());
        }
        private void initLV1()
        {
            LV1.Clear();
            LV1.View = View.Details;
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
                LV1.Columns.Add("料號", 150, HorizontalAlignment.Center);
                LV1.Columns.Add("備註", 150, HorizontalAlignment.Center);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Center);
                LV1.Columns.Add("機器", 60, HorizontalAlignment.Center);
                LV1.Columns.Add("層", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("燈號", 60, HorizontalAlignment.Center);
                //LV1.Columns.Add("id", 80, HorizontalAlignment.Right);
                //LV1.Columns.Add("storeno", 80, HorizontalAlignment.Right);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
                LV1.Columns.Add("料號", 150, HorizontalAlignment.Center);
                LV1.Columns.Add("Location", 150, HorizontalAlignment.Center);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Center);
                LV1.Columns.Add("機器", 60, HorizontalAlignment.Center);
                LV1.Columns.Add("層", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("燈號", 60, HorizontalAlignment.Center);
                //LV1.Columns.Add("id", 80, HorizontalAlignment.Right);
                //LV1.Columns.Add("storeno", 80, HorizontalAlignment.Right);
            }
         

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column : int
        {
            料號 = 1,
            備註 = 2,
            數量 = 3,
            機器 = 4,
            層 = 5,
            燈號 = 6,
            id = 7,
            storeno = 8,
            領出量 = 9,
            Location = 10,
            批號 = 11,
            備註2 = 12,
            位置 = 13,
            MMid = 14,
            storetype = 15,
        }
        private void initLVic()
        {
            LVic.Clear();
            LVic.View = View.Details;
            
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                LVic.Columns.Add(" ", 0, HorizontalAlignment.Right);
                LVic.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LVic.Columns.Add("領出量", 80, HorizontalAlignment.Center);
                LVic.Columns.Add("Location", 100, HorizontalAlignment.Center);
                LVic.Columns.Add("Date Code", 80, HorizontalAlignment.Center);
                LVic.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LVic.Columns.Add("備註2", 100, HorizontalAlignment.Center);
                LVic.Columns.Add("位置", 80, HorizontalAlignment.Center);
                LVic.Columns.Add("storeno", 80, HorizontalAlignment.Center);
                LVic.Columns.Add("MMid", 80, HorizontalAlignment.Center);
                LVic.Columns.Add("storetype", 80, HorizontalAlignment.Right);
                LVic.Columns.Add("machineno", 0, HorizontalAlignment.Right);
                LVic.Columns.Add("廠務編號", 65, HorizontalAlignment.Center);
                LVic.Columns.Add("Lot Code", 65, HorizontalAlignment.Center);
                LVic.Columns.Add("Overdue", 65, HorizontalAlignment.Center);
                LVic.Columns.Add("剩餘週數", 65, HorizontalAlignment.Center);
            }
            

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVic.Name);
            LVic.SetColWidth(vColStr);

        }
        private enum enLVicColumn : int
        {
            數量 = 1,
            領出量 = 2,
            Location = 3,
            DateCode = 4,
            備註 = 5,
            備註2 = 6,
            位置 = 7,
            storeno = 8,
            MMid = 9,
            storetype = 10,
            機器=11,
            廠務編號 = 12,
            LotCode = 13,
            Overdue = 14,
            剩餘週數 = 15,
        }
        
        #endregion

        #region button 領料、插單設定(1、2 及 3)
        private void cmdOut_Click(object sender, EventArgs e)
        {
            int InType;
            //檢查
            if (!DataCheck()) { return; }
            // 正常1 插單2
            InType = 1;
            ObjOutput(InType);
        }

        private void cmdOutInsert_Click(object sender, EventArgs e)
        {
            int InType ;
            //檢查
            if (!DataCheck()) { return; }
            //正常1 插單2
            InType = 2;
            ObjOutput(InType);
        }

        private void cmdOutP3_Click(object sender, EventArgs e)
        {
            int InType;
            //檢查
            if (!DataCheck()) { return; }
            // 正常1 插單2
            InType = 1;
            ObjOutput(InType);
        }

        private void cmdOutInsertP3_Click(object sender, EventArgs e)
        {
            int InType;
            //檢查
            if (!DataCheck()) { return; }
            //正常1 插單2
            InType = 2;
            ObjOutput(InType);
        }
        #endregion

        //1-自已選擇，2-自動
        private bool DataCheck()
        {
            string FinishNo ="";
            string Sqlstr ="";
            bool _DataCheck;
            char[] charsToTrim = { '*', ' ', '\'' };
            _DataCheck = true;

            switch (KXMSSysPara.Sys.WareHouse)
            {
                //半成品倉==========================================
                case 1:
                    //檢查工單是否正確 2012/08/29由原先11碼修改為12碼
                    if (txtNubP1.Text.Trim(charsToTrim).Length <= 6 || txtNubP1.Text.Trim(charsToTrim).Length >= 12)
                    {
                        MessageBox.Show("輸入的工單單號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNubP1.Focus();
                        txtNubP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    else
                    {
                        //正確
                    }
                    //檢查料號是否正確(1234567-123.A12)
                    Mno1 = txtItemNbP1.Text.Trim(charsToTrim);

                    if (Mno1.Length !=15)
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsNumeric(Mno1.Substring(0,7)))
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (Mno1.Substring(7,1) != "-")
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsNumeric(Mno1.Substring(8,3)))
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (Mno1.Substring(11,1) != ".")
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsEnglish(Mno1.Substring(12,1).ToUpper()))
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsNumeric(Mno1.Substring(13,2)))
                    {
                        MessageBox.Show("輸入的99料號不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    //檢查數量是否正確
                    if (IsAuto)
                    {
                        if (!IsNumeric(txtAntP1.Text) || double.Parse(txtAntP1.Text) < 1)
                        {
                            MessageBox.Show("輸入數量不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtAntP1.Focus();
                            txtAntP1.SelectAll();
                            _DataCheck = false;
                            return _DataCheck;
                        }
                    }
                    break;
                //成品倉============================================
                case 2:
                    if (txtNbP2.Text.Trim(charsToTrim).ToUpper() == "")
                    {
                        MessageBox.Show("請輸入料號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNbP2.Focus();
                        _DataCheck = false;
                        return _DataCheck;
                    }

                    FinishNo = txtNbP2.Text.Trim(charsToTrim).ToUpper();
                    //檢查數量是否正確
                    if (IsAuto)
                    {
                        if (!IsNumeric(txtAntP2.Text) || double.Parse(txtAntP2.Text) < 1)
                        {
                            MessageBox.Show("數量輸入不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtAntP2.Focus();
                            txtAntP2.SelectAll();
                            _DataCheck = false;
                            return _DataCheck;
                        }
                    }

                    //比對資料
                    Sqlstr = _SqlData.GetData("其他",25);
                    Sqlstr = Sqlstr.Replace("?1", FinishNo);
                    DataTable DT = ConnectQuery(Sqlstr);
                    if (DT.Rows.Count == 0)
                    {
                        MessageBox.Show("資料庫無此UPC code的資料，請再輸入一次!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNbP2.Focus();
                        txtNbP2.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    else
                    {
                        Mno1 = DT.Rows[0][0].ToString();
                    }
                    break;
                //IC倉==============================================
                case 3:
                    char[] charsToTrim3 = { '.', ' ', '\'' };
                    //檢查機台/盤號/物料編號
                    if (txtMachineNoP3.Text == "*")
                    { }
                    else
                    {
                        if (IsNumeric(txtTrayP3.Text) == false)
                        {
                            MessageBox.Show("請輸入正確的盤號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtTrayP3.Focus();
                            txtTrayP3.SelectAll();
                            _DataCheck = false;
                            return _DataCheck;
                        }
                    }

                    Mno1 = txtItemNbP3.Text.Trim(charsToTrim3).ToUpper();
                    //沒輸入盤號則檢查料號輸入
                    if (txtTrayP3.Text == "*")
                    {
                        //檢查料號
                        if (Mno1.Length == 0)
                        {
                            MessageBox.Show("請輸入物料編號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtItemNbP3.Focus();
                            txtItemNbP3.SelectAll();
                            _DataCheck = false;
                            return _DataCheck;
                        }
                    }

                    if (!IsNumeric(txtAntP3.Text) || double.Parse(txtAntP3.Text) < 1)
                    {
                        MessageBox.Show("數量輸入不正確!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAntP3.Focus();
                        txtAntP3.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (txtRemarkP3.Text.Trim(charsToTrim3).Length == 0)
                    {
                        MessageBox.Show("請輸入領料原因!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtRemarkP3.Focus();
                        txtRemarkP3.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    break;
            }

            return _DataCheck;
        }

        private int LVicShow()
        {
            string Sqlstr;
            string Mno;
            string Ov;
            string Location;
            string sWhere ="";
            string SelMachineNo;
            string SelCarry;
            string StoreHouse;              //庫位
            int i =0;
            int _LVicShow;
            long Qty = 0;

            //2020/5/12 新增廠務編號、Lotcode
            string VanderName;
            string Lotcode;

            LVic.Items.Clear();
            Mno = txtItemNbP3.Text;
            Location = txtLocationP3.Text;
            Ov = txtOv.Text;
            StoreHouse = cbDataP0.Text;
            SelMachineNo = txtMachineNoP3.Text;
            SelCarry = txtTrayP3.Text;
            VanderName = txtVN.Text;
            Lotcode = txtLotcode.Text;

            if (Location == "*")
            { }
            else
            {
                sWhere = " AND (MMain.Location='" + Location + "') ";
            }
            if (Ov == "*")
            { }
            else
            {
                sWhere = sWhere + " AND (MMain.OV='" + Ov + "') ";
            }
            if (Mno == "*" || Mno == "")
            { }
            else
            {
                sWhere = sWhere + " AND (MMain.Mno ='" + Mno + "') ";
            }
            if (VanderName == "*")
            { }
            else
            {
                sWhere = sWhere + " AND (MMain.VanderName ='" + VanderName + "') ";
            }
            if (Lotcode == "*")
            { }
            else
            {
                sWhere = sWhere + " AND (MMain.LotCode ='" + Lotcode + "') ";
            }
            if (SelMachineNo == "*" || SelMachineNo == "")
            { }
            else
            {
                sWhere = sWhere + " AND (Store.MachineNo =" + SelMachineNo + ") ";
            }
            if (SelCarry == "*" || SelCarry == "")
            { }
            else
            {
                sWhere = sWhere + " AND (Store.Carry = " + SelCarry + ") ";
            }

            DateTime _DateTime = new DateTime(DateTime.Now.Year, 1, 1);
            int NowDateY;
            int NowDateW;
            NowDateY = DateTime.Now.Year % 100;
            NowDateW = int.Parse((DateDiff.Simulate.DateDiff(DateDiff.Simulate.DateInterval.Weekday, _DateTime, DateTime.Now)).ToString());       //算週數
            int WeekDiff;
            string Pcode;
            string PcodeY;
            string PcodeW = "";
            string KTCPartNo;
            string ShelfLife;
            int DateW;              //存放週數

            Sqlstr = _SqlData.GetData("儲位", 82);
            Sqlstr = Sqlstr.Replace("?2", StoreHouse);
            Sqlstr = Sqlstr.Replace("?3", sWhere);
            //暫不使用儲位限制
            DataTable DT = ConnectQuery(Sqlstr);

            int MachineNo;
            foreach (DataRow DR in DT.Rows)
            {
                i = i + 1;
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add("0");
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["MDesc"].ToString());
                lvitem.SubItems.Add(DR["MDesc2"].ToString());
            
                MachineNo = int.Parse(DR["MachineNo"].ToString());
                if (int.Parse(DR["StoreType"].ToString()) == 0)
                {
                    lvitem.SubItems.Add(MachineNo + "：" + DR["Carry"].ToString() + "-" + DR["Pos"].ToString() + "-" + (int.Parse(DR["Depth"].ToString()) + 1));
                }
                else
                {
                    lvitem.SubItems.Add(DR["StoreTypeDesc"].ToString());
                }

                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                lvitem.SubItems.Add(DR["MID"].ToString());
                lvitem.SubItems.Add(DR["StoreType"].ToString());
                lvitem.SubItems.Add(DR["MachineNo"].ToString());
                lvitem.SubItems.Add(DR["VanderName"].ToString());
                lvitem.SubItems.Add(DR["LotCode"].ToString());
                lvitem.SubItems.Add(DR["Overdue"].ToString());
                lvitem.SubItems.Add(DR["remaining"].ToString());
                LVic.Items.Add(lvitem);

                //lvitem.UseItemStyleForSubItems = false;
                if (KXMSSysPara.Sys.MachineNo == 0)
                {
                    lvitem.ForeColor = Color.Black;
                }
                else
                {
                    if (MachineNo == KXMSSysPara.Sys.MachineNo || MachineNo == 0)           //0是平置倉
                    {
                        lvitem.ForeColor = Color.Black;
                        //lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Regular);
                    }
                    else                                                                    // 4 or 5
                    {
                        lvitem.BackColor = Color.LightGray;
                        lvitem.ForeColor = Color.Black;
                        //lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Regular);
                    }
                }

                Qty += int.Parse(DR["MQty"].ToString());
                Pcode = DR["OV"].ToString();
                if (Pcode.Length >= 4)
                {
                    //取前四碼 ex:1912
                    PcodeY = Pcode.Substring(0, 2);      //ex:2019年
                    PcodeW = Pcode.Substring(2, 2);      //ex: 12週
                    if ((IsNumeric(PcodeY) && IsNumeric(PcodeW)) == false)
                    {
                        //非數字格式
                        WeekDiff = 0;
                    }
                    else
                    {
                        //計算週差異 以52週為1年
                        WeekDiff = (NowDateY - int.Parse(PcodeY)) * 52 + (NowDateW - int.Parse(PcodeW));
                    }
                }
                else
                {
                    WeekDiff = 0;
                }
                //比較年限
                string PartNoType;
                Sqlstr = _SqlData.GetData("ShelfLife", 6);
                DataTable DT0 = ConnectQuery(Sqlstr);
                foreach (DataRow DR0 in DT0.Rows)
                {
                    ListViewItem lvitem0 = new ListViewItem();
                    lvitem0.Text = "";
                    lvitem0.SubItems.Add(DR0["KTCPartNo"].ToString());
                    lvitem0.SubItems.Add(DR0["ShelfLife"].ToString());
                    lvitem0.SubItems.Add(DR0["DateW"].ToString());

                    KTCPartNo = lvitem0.SubItems[1].Text;
                    ShelfLife = lvitem0.SubItems[2].Text;
                    DateW = int.Parse(lvitem0.SubItems[3].Text);

                    if (Mno.Length >= 3)
                    {
                        PartNoType = Mno;
                        if (PartNoType.Contains(KTCPartNo))
                        {
                            if (WeekDiff > DateW - 4)
                            {
                                if (WeekDiff > DateW)
                                {
                                    //過期轉紅色
                                    lvitem.ForeColor = Color.Red;

                                    if (WeekDiff < DateW - 4)
                                    {
                                        //超過一個月 0~4週
                                        lvitem.SubItems[14].Text = "1";                       //Overdue 
                                    }
                                    else if (WeekDiff < DateW - 13)
                                    {
                                        //超過三個月 5~13週
                                        lvitem.SubItems[14].Text = "3";                       //Overdue 
                                    }
                                    else
                                    {
                                        //超過六個月(含以上) 14~26週
                                        lvitem.SubItems[14].Text = "6";                       //Overdue 
                                    }

                                    lvitem.SubItems[15].Text = (DateW - WeekDiff + "");    //remaining Weeks
                                }
                                else
                                {
                                    //剩下1個月轉成紅色
                                    lvitem.ForeColor = Color.Red;
                                    lvitem.SubItems[15].Text = DateW - WeekDiff + "";    //remaining Weeks
                                }
                            }
                            else if (WeekDiff > DateW - 13)
                            {
                                //剩下3個月轉成橘色
                                lvitem.ForeColor = Color.DarkOrange;
                                lvitem.SubItems[15].Text = DateW - WeekDiff + "";    //remaining Weeks
                            }
                            else if (WeekDiff > DateW - 26)
                            {
                                //剩下6個月轉成藍色
                                lvitem.ForeColor = Color.Blue;
                                lvitem.SubItems[15].Text = DateW - WeekDiff + "";    //remaining Weeks
                            }
                            else
                            {
                                //存放仍在期限內
                                lvitem.SubItems[15].Text = DateW - WeekDiff + "";    //remaining Weeks
                            }

                            if (PcodeW == "" || int.Parse(PcodeW) > 52)
                            {
                                lvitem.SubItems[15].Text = "";
                                lvitem.SubItems[15].Text += "格式有誤";
                            }

                            lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                        }

                        ////116、118存放3年(52*3)
                        //if (PartNoType == "116" || PartNoType == "118")
                        //{
                        //    if (WeekDiff > ((52 * 3) - 4))
                        //    {
                        //        lvitem.SubItems[(int)enLVicColumn.批號].ForeColor = Color.Red;
                        //        //剩下1個月轉成紅色
                        //        lvitem.ForeColor = Color.Red;
                        //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                        //        //lvitem.SubItems[3].Font.Bold = true;
                        //    }
                        //    else if (WeekDiff > ((52 * 3) - 13))
                        //    {
                        //        //剩下3個月轉成橘色
                        //        lvitem.ForeColor = Color.DarkOrange;

                        //        lvitem.SubItems[(int)enLVicColumn.批號].ForeColor = Color.DarkOrange;
                        //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                        //        //lvitem.SubItems[3].Font.Bold = true;
                        //    }
                        //}
                        //else  //116、118以外存放1年(52)
                        //{
                        //    //剩下1個月轉成紅色
                        //    if (WeekDiff > (52 - 4))
                        //    {
                        //        lvitem.ForeColor = Color.Red;
                        //        lvitem.SubItems[(int)enLVicColumn.批號].ForeColor = Color.Red;
                        //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                        //    }
                        //    //剩下3個月轉成橘色
                        //    else if (WeekDiff > (52 - 13))
                        //    {
                        //        lvitem.ForeColor = Color.DarkOrange;
                        //        lvitem.SubItems[(int)enLVicColumn.批號].ForeColor = Color.DarkOrange;
                        //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                        //    }
                        //}
                    }
                }
            }

            _LVicShow = i;
            return _LVicShow;

        }

        private void LvDataShow()
        {
            string Sqlstr = "";
            int PackageNo;              //包裝
            string WorkOrderNo ="";     //工單單號
            string StoreHouse;          //庫位
            string Location ="";
            char[] charsToTrim = { '/', ' ', '\'' };
            char[] charsToTrims = { '.', ' ', '\'' };

            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                WorkOrderNo = txtNubP1.Text.Trim(charsToTrim);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                PackageNo = int.Parse(txtID.Text);
                Location = txtLocationP2.Text.Trim(charsToTrims);
            }
            StoreHouse = cbDataP0.Text;

            if (!DataCheck())
            {
                this.Height = 279;
                chkPcs.Checked = false;
                return;
            }

            LV1.Items.Clear();
            //半成品倉
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                Sqlstr = _SqlData.GetData("儲位", 38);
                Sqlstr = Sqlstr.Replace("?1", Mno1);
                Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                //SqlStr = SqlStr.Replace("?3", PackageNo);
                Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
            }
            //成品倉
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                if (Location.Trim(charsToTrim) == "*")
                {
                    //by Mno(全部)
                    Sqlstr = _SqlData.GetData("儲位", 36);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                    //SqlStr = Replace(SqlStr, "?3", PackageNo);
                }
                else
                {
                    //by Mno、Location
                    Sqlstr = _SqlData.GetData("儲位", 35);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", Location);
                    Sqlstr = Sqlstr.Replace("?3", StoreHouse);
                    //SqlStr = Replace(SqlStr, "?4", PackageNo);
                }
            }

            DataTable DT = ConnectQuery(Sqlstr);
            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["Mno"].ToString());
                if (KXMSSysPara.Sys.WareHouse == 1)
                {
                    lvitem.SubItems.Add(DR["MDesc"].ToString());
                }
                else if (KXMSSysPara.Sys.WareHouse == 2)
                {
                    lvitem.SubItems.Add(DR["Location"].ToString());
                }

                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["MachineNo"].ToString());
                lvitem.SubItems.Add(DR["Carry"].ToString());
                lvitem.SubItems.Add(DR["Pos"].ToString());
                lvitem.SubItems.Add(DR["id"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                LV1.Items.Add(lvitem);
            }
        }

        private void ObjOutput(int InType)
        {
            string FinishNo ="";
            string Location ="";
            int Qty =0;
            int NowTotalMnoQty =0;        //目前庫存量
            long StoreNo;

            int TQ;
            string ActionStore ="";         //SSSSQQQQ
            string ActionQty;
            string[] S;
            string[] S2;
            int i;
            long PID;
            string Sqlstr;
            bool IsSameStore;
            int AllQty;
            int nowStore;
            int CheckQty;
            string SubStoreID;
            long AllSubQty;
            int PackageNo;                 //包裝
            string WorkOrderNo ="";        //工單單號
            string StoreHouse ="";         //庫位
            string TransMemo = "";
            string SelMachineNo;
            string SelCarry;
            long MMID;
            char[] charsToTrim = { '*', ' ', '\'' };
            char[] charsToTrims = { '.', ' ', '\'' };

            //先檢查是否有足夠的數量========================================================
            //半成品倉
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                if (!IsNumeric(txtAntP1.Text) || double.Parse(txtAntP1.Text) < 1)
                {
                    Qty = 0;
                }
                else
                { Qty = int.Parse(txtAntP1.Text); }

                StoreHouse = cbDataP0.Text;
                WorkOrderNo = txtNubP1.Text.Trim(charsToTrim);

                Sqlstr = _SqlData.GetData("儲位",37);
                Sqlstr = Sqlstr.Replace("?1", Mno1);
                Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                DataTable DT = ConnectQuery(Sqlstr);

                if (DT.Rows[0][0].ToString() == "")
                {
                    NowTotalMnoQty = 0;
                }
                else
                {
                    NowTotalMnoQty = int.Parse(DT.Rows[0][0].ToString());
                }

                if (Qty > NowTotalMnoQty)
                {
                    if (MessageBox.Show("此物料庫存量不足 \r\n 目前尚餘 =>" + NowTotalMnoQty + "個 \r\n 如果確定要領料，請按確定。", "自動倉儲管理系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        Qty = NowTotalMnoQty;
                        if (Qty < 1)
                        {
                            ClearData();
                            return;
                        }
                    }
                    else
                    {
                        ClearData();
                        return;
                    }
                }
            }
            //成品倉
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                FinishNo = txtNbP2.Text;
                if (!IsNumeric(txtAntP2.Text) || double.Parse(txtAntP2.Text) < 1)
                {
                    Qty = 0;
                }
                else
                { Qty = int.Parse(txtAntP2.Text); }
                
                Location = txtLocationP2.Text;
                PackageNo = int.Parse(txtID.Text);
                StoreHouse = cbDataP0.Text;
                if (Location.Trim(charsToTrims) == "*")
                {
                    //by Mno全部
                    Sqlstr = _SqlData.GetData("儲位", 34);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                    Sqlstr = Sqlstr.Replace("?3", PackageNo + "");
                }
                else
                {
                    //by Mno、Location
                    Sqlstr = _SqlData.GetData("儲位", 33);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", Location);
                    Sqlstr = Sqlstr.Replace("?3", StoreHouse);
                    Sqlstr = Sqlstr.Replace("?4", PackageNo +"");
                }

                DataTable DT0 = ConnectQuery(Sqlstr);
                if (DT0.Rows[0][0].ToString() == "")
                {
                    NowTotalMnoQty = 0;
                }
                else
                {
                    NowTotalMnoQty = int.Parse(DT0.Rows[0][0].ToString());
                }

                if (Qty > NowTotalMnoQty)
                {
                    if (MessageBox.Show("此物料庫存量不足 \r\n 目前尚餘 =>" + NowTotalMnoQty + "個 \r\n 如果確定要領料，請按確定。", "自動倉儲管理系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        Qty = NowTotalMnoQty;
                        if (Qty < 1)
                        {
                            ClearData();
                            return;
                        }
                    }
                    else
                    {
                        ClearData();
                        return;
                    }
                }
            }
            // 2020/5/12 新增廠務編號、Lotcode
            else if (KXMSSysPara.Sys.WareHouse == 3 && ICcheck.Checked == false)
            {
                string Ov;
                string VanderName;
                string Lotcode;
                string sWhere = "";
                Location = txtLocationP3.Text;
                Ov = txtOv.Text;
                Qty = int.Parse(txtAntP3.Text);
                StoreHouse = cbDataP0.Text;
                TransMemo = txtRemarkP3.Text;
                SelMachineNo = txtMachineNoP3.Text;
                SelCarry = txtTrayP3.Text;
                VanderName = txtVN.Text;
                Lotcode = txtLotcode.Text;

                if (Location == "*")
                { }
                else
                {
                    sWhere = " AND (MMain.Location='" + Location + "') ";
                }
                if (Ov == "*")
                { }
                else
                {
                    sWhere = sWhere + " AND (MMain.OV='" + Ov + "') ";
                }
                if (SelMachineNo == "*" || SelMachineNo == "")
                { }
                else
                {
                    sWhere = sWhere + " AND (Store.MachineNo='" + SelMachineNo + "') ";
                }
                if (SelCarry == "*" || SelCarry == "")
                { }
                else
                {
                    sWhere = sWhere + " AND (Store.Carry='" + SelCarry + "') ";
                }
                if (VanderName == "*")
                { }
                else
                {
                    sWhere = sWhere + " AND (MMain.VanderName='" + VanderName + "') ";
                }
                if (Lotcode == "*")
                { }
                else
                {
                    sWhere = sWhere + " AND (MMain.LotCode='" + Lotcode + "') ";
                }

                if (Mno1.Length >0)
                {
                    //有輸入料號再檢查
                    Sqlstr = _SqlData.GetData("儲位", 74);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                    Sqlstr = Sqlstr.Replace("?3", sWhere);
                    DataTable DT1 = ConnectQuery(Sqlstr);

                    if (DT1.Rows.Count >0)
                    {
                        int.TryParse(DT1.Rows[0][0].ToString(), out NowTotalMnoQty);
                    }
                }

                if (Qty > NowTotalMnoQty)
                {
                    if (MessageBox.Show("此物料庫存量不足 \r\n 目前尚餘 =>" + NowTotalMnoQty + "個 \r\n 如果確定要領料，請按確定。", "自動倉儲管理系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        Qty = NowTotalMnoQty;
                        if (Qty < 1)
                        {
                            ClearData();
                            return;
                        }
                    }
                    else
                    {
                        ClearData();
                        return;
                    }
                }
            }

            //方法二、電腦選====================================================
            //找出目前庫存(排序==>小->大)
            //半成品倉
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                Sqlstr = _SqlData.GetData("儲位", 38);
                Sqlstr = Sqlstr.Replace("?1", Mno1);
                Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                //SqlStr = Sqlstr.Replace("?3", PackageNo);
                Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                DataTable DT = ConnectQuery(Sqlstr);
                if (DT.Rows.Count ==0)
                {
                    MessageBox.Show("目前無庫存!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (DataRow DR in DT.Rows)
                {
                    if (Qty - int.Parse(DR["MQty"].ToString()) >= 0)
                    {
                        ActionStore = ActionStore + "￡" + int.Parse(DR["StoreNo"].ToString()).ToString("00000") + int.Parse(DR["MQty"].ToString()).ToString("0000") + DR["MID"].ToString();
                    }
                    else
                    {
                        ActionStore = ActionStore + "￡" + int.Parse(DR["StoreNo"].ToString()).ToString("00000") + Qty.ToString("0000") + DR["MID"].ToString();
                    }

                    Qty = Qty - int.Parse(DR["MQty"].ToString());
                    if (Qty <= 0 || DT.Rows.Count ==0)
                    {
                        goto EndLoop1;
                    }
                }
            EndLoop1:
                ;
            }
            //成品倉
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                if (Location.Trim(charsToTrims) == "*")
                {
                    //by Mno(全部)
                    Sqlstr = _SqlData.GetData("儲位", 36);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", StoreHouse);
                    //SqlStr = Replace(SqlStr, "?3", PackageNo)
                }
                else
                {
                    //by Mno、Location
                    Sqlstr = _SqlData.GetData("儲位", 35);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    Sqlstr = Sqlstr.Replace("?2", Location);
                    Sqlstr = Sqlstr.Replace("?3", StoreHouse);
                    //SqlStr = Replace(SqlStr, "?4", PackageNo);
                }

                DataTable DT0 = ConnectQuery(Sqlstr);
                if (DT0.Rows.Count == 0)
                {
                    MessageBox.Show("目前無庫存!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (DataRow DR0 in DT0.Rows)
                {
                    if (Qty - int.Parse(DR0["MQty"].ToString()) >= 0)
                    {
                        ActionStore = ActionStore + "￡" + int.Parse(DR0["StoreNo"].ToString()).ToString("00000") + int.Parse(DR0["MQty"].ToString()).ToString("0000") + DR0["MID"].ToString();
                    }
                    else
                    {
                        ActionStore = ActionStore + "￡" + int.Parse(DR0["StoreNo"].ToString()).ToString("00000") + Qty.ToString("0000") + DR0["MID"].ToString();
                    }

                    Qty = Qty - int.Parse(DR0["MQty"].ToString());
                    if (Qty <= 0 || DT0.Rows.Count == 0)
                    {
                        goto EndLoop2;
                    }
                }
            EndLoop2:
                ;
            }

            //IC倉
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (ICcheck.Checked == false)
                {
                    i = LVicShow();
                    ICcheck.Checked = true;
                    return;
                }
                long i5;
                foreach (ListViewItem lvitem in LVic.Items)
                {
                    if (lvitem.Checked == true)
                    {
                        i5 = int.Parse(lvitem.SubItems[(int)enLVicColumn.數量].Text) - int.Parse(lvitem.SubItems[(int)enLVicColumn.領出量].Text);
                        ActionStore = ActionStore + "￡" + lvitem.SubItems[(int)enLVicColumn.storeno].Text + "@" + lvitem.SubItems[(int)enLVicColumn.領出量].Text + "@" + lvitem.SubItems[(int)enLVicColumn.storetype].Text + "@" + lvitem.SubItems[(int)enLVicColumn.MMid].Text + "@" + i5 + "@" + lvitem.SubItems[(int)enLVicColumn.Location].Text + "*" + lvitem.SubItems[(int)enLVicColumn.DateCode].Text + "@" + lvitem.SubItems[(int)enLVicColumn.備註2].Text + "@" + lvitem.SubItems[(int)enLVicColumn.備註].Text + "@" + lvitem.SubItems[(int)enLVicColumn.廠務編號].Text + "@" + lvitem.SubItems[(int)enLVicColumn.LotCode].Text;
                    }
                }

                int StoreType;
                string NotAutoStoreDesc = "";
                string Location00;
                string LocationOV;
                long RemainQty;
                string MDesc;
                string MDesc2;
                string VanderName;
                string Lotcode;

                //send command 及 過帳================================================================================
                if (ActionStore.Length <= 0)
                { }
                else
                {
                    ActionStore = ActionStore.Substring(1);
                }
                TransMemo = txtRemarkP3.Text;
                if (ActionStore.Length > 0)
                {
                    S = ActionStore.Split('￡');
                    for (i = 0; i <= S.GetUpperBound(0); i++)
                    {
                        //取得PID
                        Sqlstr = _SqlData.GetData("異動", 3);
                        DataTable DT = ConnectQuery(Sqlstr);
                        PID = int.Parse(DT.Rows[0][0].ToString());
                        PID += 1;
                        //拆解儲位的資訊
                        S2 = S[i].Split('@');
                        if (S2.GetUpperBound(0) == 9)
                        {
                            StoreNo = int.Parse(S2[0]);
                            Qty = int.Parse(S2[1]);
                            StoreType = int.Parse(S2[2]);
                            MMID = int.Parse(S2[3]);
                            RemainQty = int.Parse(S2[4]);
                            MDesc2 = S2[6].ToString();
                            MDesc = S2[7].ToString();
                            VanderName = S2[8].ToString();
                            Lotcode = S2[9].ToString();
                        }
                        else
                        {
                            goto Next_i;
                        }

                        Location00 = Mno1.ToUpper() + "*" + S2[5] + "!!" + RemainQty;
                        if (Location00.Length > 38)
                        {
                            LocationOV = Location00.Substring(37);
                        }
                        else
                        {
                            LocationOV = Location00;
                        }

                        Sqlstr = _SqlData.GetData("儲位", 5);
                        Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                        DataTable DT0 = ConnectQuery(Sqlstr);

                        long ShowQty = 0;
                        if (Qty > 999999)
                        {
                            ShowQty = 999999;
                        }
                        else
                        {
                            ShowQty = Qty;
                        }

                        //取儲位位置
                        foreach (DataRow DR0 in DT0.Rows)
                        {
                            //IC倉出自動倉
                            if (StoreType == 0)
                            {
                                if (KXMSSysPara.Sys.IsTest == 0)
                                {
                                    int vMachineNo;
                                    vMachineNo = int.Parse(DR0["MachineNo"].ToString());

                                    if (vMachineNo == 1)
                                    {
                                        //正常
                                        if (InType == 1)
                                        {
                                            _modCtrl.SendCommand(Convert.ToInt16(vMachineNo), "E1", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16((int.Parse(DR0["Depth"].ToString()) + 1)), Convert.ToInt32(ShowQty), "-" + LocationOV, "", PID, 6);
                                        }
                                        //插單
                                        else if (InType == 2)
                                        {
                                            _modCtrl.SendCommand(Convert.ToInt16(vMachineNo), "E2", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16((int.Parse(DR0["Depth"].ToString()) + 1)), Convert.ToInt32(ShowQty), "-" + LocationOV, "", PID, 6);
                                        }
                                    }
                                    else if (vMachineNo == 4 || vMachineNo == 5)
                                    {
                                        if (S2[5].Length > 20)
                                        {
                                            LocationOV = S2[5].Substring(19);
                                        }
                                        else
                                        {
                                            LocationOV = S2[5];
                                        }

                                        //正常
                                        if (InType == 1)
                                        {
                                            _modCtrl.SendC3Command(Convert.ToInt16(vMachineNo), 1, "E1", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16((int.Parse(DR0["Depth"].ToString()) + 1)), Qty, "-" + Mno1.ToUpper(), LocationOV, "", "!!" + RemainQty, PID);
                                        }
                                        //插單
                                        else if (InType == 2)
                                        {
                                            _modCtrl.SendC3Command(Convert.ToInt16(vMachineNo), 1, "E2", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16((int.Parse(DR0["Depth"].ToString()) + 1)), Qty, "-" + Mno1.ToUpper(), LocationOV, "", "!!" + RemainQty, PID);
                                        }
                                    }
                                }
                            }
                            //出平置倉
                            else
                            {
                                NotAutoStoreDesc = NotAutoStoreDesc + DR0["storetypedesc"].ToString() + "---" + Mno1 + "---數量：" + Qty + "\r\n";
                            }

                            //查詢是新增或是修改==========================================
                            Sqlstr = _SqlData.GetData("儲位", 17);
                            Sqlstr = Sqlstr.Replace("?1", Qty + "");
                            Sqlstr = Sqlstr.Replace("?2", StoreNo + "");
                            Sqlstr = Sqlstr.Replace("?3", MMID + "");
                            ConnectQuery(Sqlstr);
                            //============================================================
                            //新增異動記錄
                            if (StoreType == 0)
                            {
                                Sqlstr = _SqlData.GetData("異動", 4);
                            }
                            else
                            {
                                Sqlstr = _SqlData.GetData("異動", 9);
                            }

                            string[] SS;
                            SS = Location00.Split('*');
                            Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);            //工單單號
                            Sqlstr = Sqlstr.Replace("?2", Mno1);
                            Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                            DtpOut.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?4", DtpOut.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                            Sqlstr = Sqlstr.Replace("?6", 1 + "");
                            Sqlstr = Sqlstr.Replace("?7", Qty + "");
                            Sqlstr = Sqlstr.Replace("?8", TransMemo);
                            Sqlstr = Sqlstr.Replace("?9", DR0["MachineNo"].ToString());
                            Sqlstr = Sqlstr.Replace("?01", PID + "");
                            Sqlstr = Sqlstr.Replace("?02", SS[1]);                 //Location
                            Sqlstr = Sqlstr.Replace("?03", StoreHouse);            //庫位
                            Sqlstr = Sqlstr.Replace("?04", SS[2]);
                            Sqlstr = Sqlstr.Replace("?05", MDesc2);
                            Sqlstr = Sqlstr.Replace("?06", MDesc);
                            Sqlstr = Sqlstr.Replace("?07", VanderName);
                            Sqlstr = Sqlstr.Replace("?08", Lotcode);
                            ConnectQuery(Sqlstr);
                        }

                    Next_i:
                        continue;
                    }
                }
                else
                {
                    MessageBox.Show("領料失敗!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //IC倉以外
            else
            {
                //Send Command 及 過帳====================================================================================
                ActionStore = ActionStore.Substring(1);
                if (ActionStore.Length > 0)
                {
                    S = ActionStore.Split('￡');
                    for (i = 0; i <= S.GetUpperBound(0); i++)
                    {
                        //取得PID
                        Sqlstr = _SqlData.GetData("異動", 3);
                        DataTable DT = ConnectQuery(Sqlstr);
                        PID = int.Parse(DT.Rows[0][0].ToString()) + 1;
                        //PID = PID + 1;
                        StoreNo = int.Parse(S[i].Substring(0, 5));         //儲位
                        Qty = int.Parse(S[i].Substring(5, 4));             //數量
                        MMID = int.Parse(S[i].Substring(9));

                        Sqlstr = _SqlData.GetData("儲位", 5);
                        Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                        DataTable DT0 = ConnectQuery(Sqlstr);
                        //取儲位位置
                        foreach (DataRow DR0 in DT0.Rows)
                        {
                            if (KXMSSysPara.Sys.WareHouse == 1)
                            {
                                //正常
                                if (InType == 1)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(DR0["MachineNo"].ToString()), "E1", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16(int.Parse(DR0["Depth"].ToString()) + 1), Qty, "- (" + WorkOrderNo + ")" + Mno1.ToUpper(), "", PID, 4);
                                }
                                //插單
                                else if (InType == 2)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(DR0["MachineNo"].ToString()), "E2", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16(int.Parse(DR0["Depth"].ToString()) + 1), Qty, "- (" + WorkOrderNo + ")" + Mno1.ToUpper(), "", PID, 4);
                                }
                            }
                            else if (KXMSSysPara.Sys.WareHouse == 2)
                            {
                                //正常
                                if (InType == 1)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(DR0["MachineNo"].ToString()), "E1", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16(int.Parse(DR0["Depth"].ToString()) + 1), Qty, "-" + FinishNo.ToUpper(), "", PID, 4);
                                }
                                else if (InType == 2)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(DR0["MachineNo"].ToString()), "E2", Convert.ToInt16(DR0["Carry"].ToString()), Convert.ToInt16(DR0["Pos"].ToString()), Convert.ToInt16(int.Parse(DR0["Depth"].ToString()) + 1), Qty, "-" + FinishNo.ToUpper(), "", PID, 4);
                                }
                            }

                            //查詢是新增或是修改==========================================
                            Sqlstr = _SqlData.GetData("儲位", 17);
                            Sqlstr = Sqlstr.Replace("?1", Qty + "");
                            Sqlstr = Sqlstr.Replace("?2", StoreNo + "");
                            Sqlstr = Sqlstr.Replace("?3", MMID + "");
                            ConnectQuery(Sqlstr);
                            //============================================================
                            //新增異動紀錄
                            Sqlstr = _SqlData.GetData("異動", 4);
                            Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);     //工單單號
                            Sqlstr = Sqlstr.Replace("?2", Mno1);
                            Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                            DtpOut.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?4", DtpOut.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                            Sqlstr = Sqlstr.Replace("?6", 1 + "");
                            Sqlstr = Sqlstr.Replace("?7", Qty + "");
                            Sqlstr = Sqlstr.Replace("?8", TransMemo);
                            Sqlstr = Sqlstr.Replace("?9", DR0["MachineNo"].ToString());
                            Sqlstr = Sqlstr.Replace("?01", PID + "");
                            Sqlstr = Sqlstr.Replace("?02", Location);
                            Sqlstr = Sqlstr.Replace("?03", StoreHouse);     //庫位
                            Sqlstr = Sqlstr.Replace("?04", "");
                            Sqlstr = Sqlstr.Replace("?05", "");
                            Sqlstr = Sqlstr.Replace("?06", "");
                            ConnectQuery(Sqlstr);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("領料失敗!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            KXMSSysPara.Sys.CheckEmptyStore();
            ClearData();
        }

        private void ClearData()
        {
            DtpOut.Value = DateTime.Now;
            chkPcs.Checked = false;

            //半成品倉
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                txtNubP1.Text = "";
                txtItemNbP1.Text = "";
                txtAntP1.Text = "";
                txtNubP1.Focus();
                cbDataP1.Text = KXMSSysPara.Sys.DefaultPackage;
                cbDataP0.Text = "T3";
            }
            //成品倉
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                txtNbP2.Text = "";
                txtAntP2.Text = "";
                txtLocationP2.Text = "";
                txtNbP2.Focus();
                cbDataP2.Text = KXMSSysPara.Sys.DefaultPackage;
                cbDataP0.Text = "T3";
            }
            //IC倉
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                txtItemNbP3.Text = "";
                txtLocationP3.Text = "*";
                txtAntP3.Text = "";
                txtOv.Text = "*";
                txtItemNbP3.Focus();
                //cbDataP0.Text = "T3";
                LVic.Items.Clear();
                //清除勾選反藍
                //LVic.CheckBoxes = false;
                txtOv2.Text = "";
                txtMMID.Text = "";
                //SelLVicItemIndex = -1;
                txtVN.Text = "*";
                txtLotcode.Text = "*";
                ICcheck.Checked = false;
            }
        }

        private void chkPcs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPcs.Checked == true)
            {
                IsAuto = false;
                this.Height = 600;
                LvDataShow();
            }
            else
            {
                IsAuto = true;
                this.Height = 279;
                LV1.Items.Clear();
            }
        }

        #region Button事件
        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            string Sqlstr ="";
            if (txtMMID.Text.Length == 0)
            {
                MessageBox.Show("請在下面重新點選資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Sqlstr = _SqlData.GetData("儲位", 27);
            Sqlstr = Sqlstr.Replace("?1", txtOv2.Text);
            Sqlstr = Sqlstr.Replace("?2", txtMMID.Text);
            Connect(Sqlstr);

            LVic.FocusedItem.SubItems[(int)enLVicColumn.DateCode].Text = txtOv2.Text;
        }
        #endregion

        #region LV事件
        private void LVic_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOv2.Text = LVic.FocusedItem.SubItems[(int)enLVicColumn.DateCode ].Text;
            txtMMID.Text = LVic.FocusedItem.SubItems[(int)enLVicColumn.MMid].Text;
        }

        private void LVic_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //int i;
            long Qty = 0;       //需領出量
            long Qty2 = 0;      //加總總領出量
            
            //不同機台(MachineNo)不可以選取
            //foreach (ListViewItem lvitem0 in LVic.Items)
            //{
            //    if (lvitem0.SubItems[11].Text == KXMSSysPara.Sys.MachineNo + "" || lvitem0.SubItems[11].Text == 0 + "")
            //    { }
            //    else
            //    { lvitem0.Checked = false; }
            //}

            //if (e.Item.Checked == false) { return; }
            foreach (ListViewItem lvitem0 in LVic.Items)
            {
                Qty2 = Qty2 + int.Parse(lvitem0.SubItems[(int)enLVicColumn.領出量].Text);
            }

            if (e.Item.Checked)
            {
                if (IsNumeric(txtAntP3.Text))
                {
                    Qty = int.Parse(txtAntP3.Text);
                }
                else
                {
                    Qty = 0;
                }

                if (Qty2 >= Qty)
                {
                    MessageBox.Show("領出來的數量已經夠了!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Item.Checked = false;
                    return; 
                }

                if (Qty - Qty2 > int.Parse(e.Item.SubItems[(int)enLVicColumn.數量].Text))
                {
                    e.Item.SubItems[(int)enLVicColumn.領出量].Text = e.Item.SubItems[(int)enLVicColumn.數量].Text;
                }
                else
                {
                    e.Item.SubItems[(int)enLVicColumn.領出量].Text = (Qty - Qty2) + "";
                }
            }
            else
            {
                e.Item.SubItems[(int)enLVicColumn.領出量].Text = "0";
            }      
            
            
            Qty2 = 0;
            foreach (ListViewItem lvitem0 in LVic.Items)
            {
                Qty2 = Qty2 + int.Parse(lvitem0.SubItems[(int)enLVicColumn.領出量].Text);
            }
            lblQty.Text = "領出數量：" + Qty2;
        }
        #endregion

        #region Textbox按Enter觸發事件
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            
            if (e.KeyCode == Keys.Enter || e.KeyCode== Keys.Tab)
            {
                switch (Tb.TabIndex)
                {
                    case 1:
                        txtItemNbP1.Focus();
                        break;
                    case 2:
                        txtAntP1.Focus();
                        break;
                    case 3:
                        cbDataP1.Focus();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 8:
                        txtAntP2.Focus();
                        break;
                    case 9:
                        txtLocationP2.Focus();
                        break;
                    case 10:
                        if (MessageBox.Show("確認領出資料是否正確!!", "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            cmdOut.PerformClick();
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 27:
                        txtTrayP3.Focus();
                        break;
                    case 28:
                        txtItemNbP3.Focus();
                        break;
                    case 29:
                        if (txtItemNbP3.Text.Length == 0)
                        { }
                        else if (txtItemNbP3.Text.Substring(0, 1).ToUpper() == "P")
                        {
                            txtItemNbP3.Text = txtItemNbP3.Text.Substring(1);
                        }
                        txtLocationP3.Focus();
                        break;
                    case 30:
                        txtAntP3.Focus();
                        break;
                    case 31:
                        txtOv.Focus();
                        break;
                    case 32:
                        txtVN.Select();
                        break;
                    case 33:
                        txtLotcode.Focus();
                        break;
                    case 34:
                        txtRemarkP3.Focus();
                        break;
                    case 35:
                        //輸入完領料原因 不自動按領料
                        //cmdOutP3.PerformClick();
                        break;
                }
            }
        }
        #endregion

        private void LVic_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (LVic.Items[e.Index].SubItems[(int)enLVicColumn.機器].Text == KXMSSysPara.Sys.MachineNo + "" || LVic.Items[e.Index].SubItems[(int)enLVicColumn.機器].Text == 0 + "")
                { }
                else
                { e.NewValue = CheckState.Unchecked; }
            }
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }
    }
}
