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
    public partial class frmInM : Form
    {
        SqlData _SqlData;
        modCtrl _modCtrl;
        string Mno1 = "";
        string FinishNo = "";

        public frmInM()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
            _modCtrl = new modCtrl();
        }

        #region 視窗設定
        private void GroupBox(int i)
        {

            switch (i)
            {
                case 1: //半成品倉
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    label22.Visible = false;
                    label24.Visible = false;
                    lbrbOp.Visible = false;
                    txt2D.Visible = false;
                    this.Width = 576;
                    this.Height = 344;
                    break;
                case 2: //成品倉
                    groupBox1.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = false;
                    label22.Visible = false;
                    label24.Visible = false;
                    lbrbOp.Visible = false;
                    txt2D.Visible = false;
                    this.Width = 576;
                    this.Height = 344;
                    break;
                case 3: //IC倉
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    this.Width = 576;
                    this.Height = 590;
                    break;
            }

        }

        private void frmInM_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Hide();
            //e.Cancel = true;
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

        private void frmInM_Load(object sender, EventArgs e)
        {
            int x = (1140 - this.Size.Width) / 2;
            int y = (1000 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            if (rbOp1.Checked)
            {
                lbrbOp.Text = FindEmptyStore(23) + "";
            }
            else
            {
                lbrbOp.Text = FindEmptyStore(22) + "";
            }
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            //改變視窗排列===============================================================
            this.Width = 576;
            this.Height = 344;
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                GroupBox(1);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                GroupBox(2);
                groupBox2.Location = new System.Drawing.Point(26, 55);
            }
            else
            {
                GroupBox(3);
                groupBox3.Location = new System.Drawing.Point(26, 55);
            }
            //帶出基本資料===============================================================
            //找出庫位
            DataTable DT = new DataTable();
            Sqlstr = _SqlData.GetData("其他", 4);
            try
            {
                Conn.Open();
                DA.SelectCommand.CommandText = Sqlstr;
                DA.Fill(DT);
                Conn.Close();
                cbDataP0.Items.Clear();
                foreach (DataRow DR in DT.Rows)
                {
                    ComboboxItem vitem0 = new ComboboxItem();
                    vitem0.Text = DR["StoreHouse"].ToString();
                    vitem0.Value = DR["id"].ToString();
                    cbDataP0.Items.Add(vitem0);
                }
                if (cbDataP0.Items.Count == 0)
                {
                    return;
                }
                else
                {
                    cbDataP0.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            //找出包裝
            Sqlstr = _SqlData.GetData("其他", 14);
            Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");

            //設定初始值===============================================================
            this.DtpIn.Value = DateTime.Now;
            cbDataP1.Items.Clear();
            cbDataP2.Items.Clear();
            DataTable DT0 = new DataTable();
            try
            {
                Conn.Open();
                DA.SelectCommand.CommandText = Sqlstr;
                DA.Fill(DT0);
                Conn.Close();
                foreach (DataRow DR in DT0.Rows)
                {
                    ComboboxItem lvitem0 = new ComboboxItem();

                    //半成品倉
                    if (KXMSSysPara.Sys.WareHouse == 1)
                    {
                        lvitem0.Text = DR["PackageDesc"].ToString();
                        lvitem0.Value = DR["id"].ToString();
                        cbDataP1.Items.Add(lvitem0);

                        cbDataP1.Text = KXMSSysPara.Sys.DefaultPackage;
                        cbDataP0.Text = "T3";
                    }
                    //成品倉
                    else if (KXMSSysPara.Sys.WareHouse == 2)
                    {
                        lvitem0.Text = DR["PackageDesc"].ToString();
                        lvitem0.Value = DR["id"].ToString();
                        cbDataP2.Items.Add(lvitem0);

                        cbDataP2.Text = KXMSSysPara.Sys.DefaultPackage;
                        cbDataP0.Text = "T3";
                    }
                    //IC倉
                    else if (KXMSSysPara.Sys.WareHouse == 3)
                    {
                        string S = DR["id"].ToString();
                        if (S == "23")
                        {
                            rbOp1.Text = DR["PackageDesc"].ToString();
                        }
                        else if (S == "22")
                        {
                            rbOp2.Text = DR["PackageDesc"].ToString();
                        }

                        cbDataP3.Items.Clear();
                        DataTable DT1 = new DataTable();
                        Sqlstr = _SqlData.GetData("儲位", 10);
                        Conn.Open();
                        DA.SelectCommand.CommandText = Sqlstr;
                        DA.Fill(DT1);
                        Conn.Close();
                        foreach (DataRow DR1 in DT1.Rows)
                        {
                            ComboboxItem lvitem1 = new ComboboxItem();
                            lvitem1.Text = DR1["StoreTypeDesc"].ToString();
                            lvitem1.Value = DR1["StoreNo"].ToString();
                            cbDataP3.Items.Add(lvitem1);

                            cbDataP0.Text = "T3";
                        }
                        //if (cbDataP3.Items.Count == 0)
                        //{
                        //    return;
                        //}
                        //else
                        //{
                        //    cbDataP3.SelectedIndex = 0;
                        //}
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            KXMSSysPara.Sys.CheckEmptyStore();
        }

        #region 包裝捲和盤計算與顯示

        private void rbOp1_CheckedChanged(object sender, EventArgs e)
        {
            txtVN.Focus();
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                lbrbOp.Text = FindEmptyStore(23) + "";
            }
        }

        private void rbOp2_CheckedChanged(object sender, EventArgs e)
        {
            txtVN.Focus();
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                lbrbOp.Text = FindEmptyStore(22) + "";
            }
        }

        //計算 MTypeID = 22 或 MTypeID = 23 之空儲位當WareHouse = 3 時
        public int FindEmptyStore(int MType)
        {
            string Sqlstr = "";
            int _FindEmptyStore = 0;
            int vMachineNo = KXMSSysPara.Sys.MachineNo;
            if (vMachineNo <= 0) { vMachineNo = 1; }
            switch (MType)
            {
                //盤
                case 22:
                    Sqlstr = _SqlData.GetData("庫位", 7);
                    Sqlstr = Sqlstr.Replace("?2", vMachineNo + "");
                    break;
                //捲
                case 23:
                    Sqlstr = _SqlData.GetData("庫位", 8);
                    Sqlstr = Sqlstr.Replace("?2", vMachineNo + "");
                    break;
            }
            DataTable DT = ConnectQuery(Sqlstr);
            if (DT.Rows.Count == 0)
            {
                _FindEmptyStore = 0;
            }
            else
            {
                _FindEmptyStore = int.Parse(DT.Rows[0][0].ToString());
            }

            return _FindEmptyStore;
        }
        #endregion

        public bool DataCheck()
        {
            string Sqlstr = "";
            bool _DataCheck;
            _DataCheck = true;
            char[] charsToTrim = { '*', ' ', '\'' };

            switch (KXMSSysPara.Sys.WareHouse)
            {
                //半成品倉======================================================
                case 1:
                    Mno1 = txtItemNbP1.Text.Trim(charsToTrim).ToUpper();
                    //檢查料號是否正確(1234567-123.A12)
                    if (Mno1.Length != 15)
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsNumeric(Mno1.Substring(0, 7)))
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (Mno1.Substring(7, 1) != "-")
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsNumeric(Mno1.Substring(8, 3)))
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (Mno1.Substring(11, 1) != ".")
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsEnglish(Mno1.Substring(12, 1).ToUpper()))
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    if (!IsNumeric(Mno1.Substring(13, 2)))
                    {
                        MessageBox.Show("輸入的99料號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtItemNbP1.Focus();
                        txtItemNbP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    //檢查工單是否正確 2012/8/29由原11碼修改為12碼
                    if (txtNubP1.Text.Trim(charsToTrim).Length >= 6 && txtNubP1.Text.Trim(charsToTrim).Length <= 12)
                    {

                    }
                    else
                    {
                        MessageBox.Show("輸入的工單單號不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNubP1.Focus();
                        txtNubP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    //檢查數量是否正確
                    if (!IsNumeric(txtAntP1.Text) || double.Parse(txtAntP1.Text) < 1)
                    {
                        MessageBox.Show("數量輸入不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAntP1.Focus();
                        txtAntP1.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }

                    //比對資料庫
                    Sqlstr = _SqlData.GetData("其他", 34);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    DataTable DT = ConnectQuery(Sqlstr);
                    foreach (DataRow DR in DT.Rows)
                    {
                        string S = DR["code99"].ToString();
                        if (txtItemNbP1.Text != S)
                        {
                            if (MessageBox.Show("這料號是第一次輸入，請再確認一下!!", "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                            {
                                txtNubP1.Focus();
                                txtNubP1.SelectAll();
                                _DataCheck = false;
                                return _DataCheck;
                            }
                            else
                            {
                                Sqlstr = _SqlData.GetData("其他", 31);
                                Sqlstr = Sqlstr.Replace("?1", Mno1);
                                Connect(Sqlstr);
                            }
                        }
                    }
                    break;
                //成品倉========================================================
                case 2:
                    Mno1 = txtUPCP2.Text;
                    FinishNo = txtNubP2.Text.Trim(charsToTrim).ToUpper();
                    //檢查UPC code 是否正確
                    if (Mno1.Length != 12)
                    {
                        MessageBox.Show("UPC code輸入不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUPCP2.Focus();
                        txtUPCP2.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    //檢查數量是否正確
                    if (!IsNumeric(txtAntP2.Text) || double.Parse(txtAntP2.Text) < 1)
                    {
                        MessageBox.Show("數量輸入不正確", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAntP2.Focus();
                        txtAntP2.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }

                    //比對資料庫
                    Sqlstr = _SqlData.GetData("其他", 24);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    DataTable DT0 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR0 in DT0.Rows)
                    {
                        if (DT0.Rows.Count == 0)
                        {
                            MessageBox.Show("資料庫無此UPC code的資料，請再輸入一次!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtUPCP2.Focus();
                            txtUPCP2.SelectAll();
                            _DataCheck = false;
                            return _DataCheck;
                        }
                        else
                        {
                            string S = DT0.Rows[0][0].ToString();
                            if (S.ToUpper() != FinishNo)
                            {
                                MessageBox.Show("成品料號和資料庫的不符，請再輸入一次!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtNubP2.Focus();
                                txtNubP2.SelectAll();
                                _DataCheck = false;
                                return _DataCheck;
                            }
                        }
                    }
                    break;
                //IC倉==========================================================
                case 3:
                    Mno1 = txtItemNbP3.Text.Trim(charsToTrim).ToUpper();        //字串去除符號+小寫轉大寫

                    if (Mno1.Length == 0)
                    {
                        MessageBox.Show("請輸入物料編號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemNbP3.Focus();
                        txtItemNbP3.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }

                    //檢查數量是否正確
                    if (txtAntP3.Text.Length == 0)
                    {
                        MessageBox.Show("請輸入數量!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtAntP3.Focus();
                        txtAntP3.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    else if (int.Parse(txtAntP3.Text) < 1)
                    {
                        MessageBox.Show("輸入數量錯誤!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAntP3.Focus();
                        txtAntP3.SelectAll();
                        _DataCheck = false;
                        return _DataCheck;
                    }

                    //比對資料庫
                    Sqlstr = _SqlData.GetData("其他", 34);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        string S = "";
                        S = DR1["code99"].ToString();
                        if (txtItemNbP3.Text != S)
                        {
                            if (MessageBox.Show("這料號是第一次輸入，請在確認一次!!", "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                            {
                                txtItemNbP3.Focus();
                                txtItemNbP3.SelectAll();
                                _DataCheck = false;
                            }
                            else
                            {
                                Sqlstr = _SqlData.GetData("其他", 31);
                                Sqlstr = Sqlstr.Replace("?1", Mno1);
                                Connect(Sqlstr);
                            }
                        }
                    }
                    break;
            }

            return _DataCheck;
        }

        #region 判斷是否為數字及英文字母
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
        public bool IsEnglish(string Value)
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

        #region 計算週數
        private int PCodeDiffWeek(string Pcode)
        {
            int Result;
            int ResultTotal;
            string PcodeY;
            string PcodeW;
            int NowDateY;
            int NowDateW;
            DateTime _DateTime = new DateTime(DateTime.Now.Year, 1, 1);         //今年2019/1/1
            NowDateY = DateTime.Now.Year % 100;
            NowDateW = int.Parse((DateDiff.Simulate.DateDiff(DateDiff.Simulate.DateInterval.Weekday, _DateTime, DateTime.Now)).ToString());       //算週數
            int WeekDiff;

            if (Pcode.Length >= 4)
            {
                //取前4碼
                PcodeY = Pcode.Substring(0, 2);      //ex:2019年
                PcodeW = Pcode.Substring(2, 2);      //ex: 12週
                if (IsNumeric(PcodeY) && (IsNumeric(PcodeW)) == false)
                {
                    //非數字格式
                    Result = -99;
                }
                else
                {
                    //比較年限   
                    //計算週差異 以52週為1年
                    WeekDiff = (NowDateY - int.Parse(PcodeY)) * 52 + (NowDateW - int.Parse(PcodeW));
                    Result = WeekDiff;
                }

            }
            else
            {
                Result = -99;
            }

            ResultTotal = Result;
            return ResultTotal;
        }
        #endregion

        //入庫的Function(包裝的id、儲位比x、儲位比y、儲位比最大量、庫位、料號、Location(or備註)、數量、工單單號、包裝編號、是否插單(1正常2插單)、先進先出否(1是先進先出,2不是)
        public string ObjInput(long PkID, int Mx, int My, long Maxqty, string StoreHouse, string Mno, string Location, long ActionQty, string WorkOrderNo, int PackageNo, int InType, int IsFirstIn, string Ov = "", string Remark = "", string MDesc2 = "")
        {
            string _ObjInput = "";
            int TQ;
            string ActionStore = "";  //SSSSQQQQ
            string[] S;
            string[] S2;
            int i;
            int j;
            int k;
            long PID = 0;
            bool FindEmpty = false;
            bool NoEmpty = true;
            string Sqlstr;
            int Qty = 0;
            long SurplusQty;    //賸下的空間

            long StoreNo = 0;
            int MachineNo = 0;
            int Carry = 0;
            int Pos = 0;
            int Depth = 0;
            long MMID;          //MMain的id
            int x;

            bool FirstIn;       //是否第一次入庫
            int StoreCounts;    //計算要放幾個儲位
            int GoodMachineNo;  //找出合適的
            int GoodCarry;      //找出合適的
            int Package;

        Redo:
            FirstIn = false;
            //(((半成品倉)))##################################################################
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                //計算要放幾個儲位，如果兩個以上，就盡量放同一層
                StoreCounts = (int)(ActionQty / Maxqty);
                if (StoreCounts >= 2)
                {
                    //找空儲位
                    Sqlstr = _SqlData.GetData("儲位", 43);
                    Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                    Sqlstr = Sqlstr.Replace("?2", StoreCounts + "");
                    DataTable DT0 = ConnectQuery(Sqlstr);
                    DataRow DR0 = DT0.Rows[0];
                    //有合適的同一層的儲位
                    if (DT0.Rows.Count > 0)
                    {
                        GoodMachineNo = Convert.ToInt32(DR0["MachineNo"].ToString());
                        GoodCarry = Convert.ToInt32(DR0["Carry"].ToString());
                        Sqlstr = _SqlData.GetData("儲位", 15);
                        Sqlstr = Sqlstr.Replace("?1", GoodMachineNo + "");
                        Sqlstr = Sqlstr.Replace("?2", GoodCarry + "");
                        DataTable DT1 = ConnectQuery(Sqlstr);
                        if ((ActionQty / Maxqty) > 0)
                        {
                            DataRow DR1 = DT1.Rows[0];
                            ActionStore = ActionStore + "," + int.Parse(DR1["MachineNo"].ToString()).ToString("00") + int.Parse(DR1["Carry"].ToString()).ToString("000") + int.Parse(DR1["Pos"].ToString()).ToString("00") + int.Parse(DR1["Depth"].ToString()).ToString("0") + int.Parse(DR1["MaxQty"].ToString()).ToString("0000") + int.Parse(DR1["StoreNo"].ToString()).ToString("00000");
                            ActionQty = ActionQty - Maxqty;

                            //暫時指定儲位、填入庫日期
                            string _StoreNo = DR1["StoreNo"].ToString();
                            Sqlstr = _SqlData.GetData("儲位", 9);
                            DtpIn.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?2", 2 + "");
                            Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                            ConnectQuery(Sqlstr);
                        }
                    }
                    //沒有合適的同一層的儲位
                    else
                    {
                        Sqlstr = _SqlData.GetData("儲位", 44);
                        Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                        DataTable DT2 = ConnectQuery(Sqlstr);
                        if ((ActionQty / Maxqty) > 0)
                        {
                            DataRow DR2 = DT2.Rows[0];
                            ActionStore = ActionStore + "," + int.Parse(DR2["MachineNo"].ToString()).ToString("00") + int.Parse(DR2["Carry"].ToString()).ToString("000") + int.Parse(DR2["Pos"].ToString()).ToString("00") + int.Parse(DR2["Depth"].ToString()).ToString("0") + int.Parse(DR2["MaxQty"].ToString()).ToString("0000") + int.Parse(DR2["StoreNo"].ToString()).ToString("00000");
                            ActionQty = ActionQty - Maxqty;

                            //暫時指定儲位、填入庫日期
                            string _StoreNo = DR2["StoreNo"].ToString();
                            Sqlstr = _SqlData.GetData("儲位", 9);
                            DtpIn.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?2", 2 + "");
                            Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                            ConnectQuery(Sqlstr);
                        }
                    }
                }
                //(第一步驟) 找已存在的儲位==================================================
                Sqlstr = _SqlData.GetData("儲位", 40);
                Sqlstr = Sqlstr.Replace("?1", Mno);
                Sqlstr = Sqlstr.Replace("?2", WorkOrderNo);
                Sqlstr = Sqlstr.Replace("?3", StoreHouse);
                Sqlstr = Sqlstr.Replace("?4", PkID + "");
                DataTable DT = ConnectQuery(Sqlstr);
                if (DT.Rows.Count > 0)
                {
                    //有現有儲位
                    SurplusQty = 0;
                    foreach (DataRow DR in DT.Rows)
                    {
                        //儲位空間 = width的倍數*height的倍數*最大量
                        //賸下空間 = 儲位空間-已存放量
                        SurplusQty = SurplusQty + (Convert.ToInt32(DR["Width"].ToString()) / Mx) * (Convert.ToInt32(DR["Height"].ToString()) / My) * Maxqty - Convert.ToInt32(DR["MQty"].ToString());
                        if (SurplusQty >= ActionQty)
                        {
                            if (ActionQty >= 0)
                            {
                                //空間夠
                                SurplusQty = (Convert.ToInt32(DR["Width"].ToString()) / Mx) * (Convert.ToInt32(DR["Height"].ToString()) / My) * Maxqty - Convert.ToInt32(DR["MQty"].ToString());
                                if (SurplusQty > 0)
                                {
                                    ActionQty = ActionQty - SurplusQty;
                                    if (ActionQty > 0)
                                    {
                                        ActionStore = ActionStore + "," + Convert.ToInt32(DR["MachineNo"].ToString()).ToString("00") + Convert.ToInt32(DR["Carry"].ToString()).ToString("000") + Convert.ToInt32(DR["Pos"].ToString()).ToString("00") + Convert.ToInt32(DR["Depth"].ToString()).ToString("0") + Convert.ToInt32(SurplusQty).ToString("0000") + Convert.ToInt32(DR["StoreNo"].ToString()).ToString("00000");
                                    }
                                    else
                                    {
                                        ActionStore = ActionStore + "," + Convert.ToInt32(DR["MachineNo"].ToString()).ToString("00") + Convert.ToInt32(DR["Carry"].ToString()).ToString("000") + Convert.ToInt32(DR["Pos"].ToString()).ToString("00") + Convert.ToInt32(DR["Depth"].ToString()).ToString("0") + Convert.ToInt32(SurplusQty + ActionQty).ToString("0000") + Convert.ToInt32(DR["StoreNo"].ToString()).ToString("00000");
                                    }
                                }
                            }
                        }
                        else
                        {
                            //空間不夠
                            FindEmpty = true;
                        }
                    }
                }
                else
                {
                    //沒有現有儲位
                    FindEmpty = true;
                }
                //(第二步驟)找空的儲位 ============================================================
                if (ActionQty > 0 && FindEmpty)
                {
                    DataTable DT0 = ConnectQuery(Sqlstr);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    //檢查是否有剛好的空儲位(一次放)排序由小到大*****************************
                    x = (int)(ActionQty / Maxqty) + (int)((ActionQty % Maxqty) * 10);
                    Sqlstr = _SqlData.GetData("儲位", 41);
                    Sqlstr = Sqlstr.Replace("?1", x + "");
                    Sqlstr = Sqlstr.Replace("?2", My + "");
                    Sqlstr = Sqlstr.Replace("?3", KXMSSysPara.Sys.WareHouse + "");
                    DT0 = ConnectQuery(Sqlstr);
                    //檢查是否其他的小儲位(分批放)排序由大到小********************************
                    if (DT0.Rows.Count == 0)
                    {
                        Sqlstr = _SqlData.GetData("儲位", 39);
                        Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                        DT1 = ConnectQuery(Sqlstr);
                    }
                    if (DT0.Rows.Count > 0)
                    {
                        DataRow DR0 = DT0.Rows[0];
                        NoEmpty = false;
                        //計算儲位容量
                        SurplusQty = Convert.ToInt32((double.Parse(DR0["Width"].ToString()) / Mx) * (double.Parse(DR0["Height"].ToString()) / My) * Maxqty);
                        ActionQty = ActionQty - SurplusQty;
                        if (ActionQty > 0)
                        {
                            ActionStore = ActionStore + "," + Convert.ToInt32(DR0["MachineNo"].ToString()).ToString("00") + Convert.ToInt32(DR0["Carry"].ToString()).ToString("000") + Convert.ToInt32(DR0["Pos"].ToString()).ToString("00") + Convert.ToInt32(DR0["Depth"].ToString()).ToString("0") + Convert.ToInt32(SurplusQty).ToString("0000") + Convert.ToInt32(DR0["StoreNo"].ToString()).ToString("00000");
                        }
                        else
                        {
                            ActionStore = ActionStore + "," + Convert.ToInt32(DR0["MachineNo"].ToString()).ToString("00") + Convert.ToInt32(DR0["Carry"].ToString()).ToString("000") + Convert.ToInt32(DR0["Pos"].ToString()).ToString("00") + Convert.ToInt32(DR0["Depth"].ToString()).ToString("0") + Convert.ToInt32(SurplusQty + ActionQty).ToString("0000") + Convert.ToInt32(DR0["StoreNo"].ToString()).ToString("00000");
                        }
                        //暫時指定儲位、填入庫日期
                        string _StoreNo = DR0["StoreNo"].ToString();
                        Sqlstr = _SqlData.GetData("儲位", 9);
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?2", 2 + "");
                        Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                        ConnectQuery(Sqlstr);
                    }
                    else if (DT1.Rows.Count > 0)
                    {
                        DataRow DR1 = DT1.Rows[0];
                        NoEmpty = false;
                        //計算儲位容量
                        SurplusQty = Convert.ToInt32((double.Parse(DR1["Width"].ToString()) / Mx) * (double.Parse(DR1["Height"].ToString()) / My) * Maxqty);
                        ActionQty = ActionQty - SurplusQty;
                        if (ActionQty > 0)
                        {
                            ActionStore = ActionStore + "," + Convert.ToInt32(DR1["MachineNo"].ToString()).ToString("00") + Convert.ToInt32(DR1["Carry"].ToString()).ToString("000") + Convert.ToInt32(DR1["Pos"].ToString()).ToString("00") + Convert.ToInt32(DR1["Depth"].ToString()).ToString("0") + Convert.ToInt32(SurplusQty).ToString("0000") + Convert.ToInt32(DR1["StoreNo"].ToString()).ToString("00000");
                        }
                        else
                        {
                            ActionStore = ActionStore + "," + Convert.ToInt32(DR1["MachineNo"].ToString()).ToString("00") + Convert.ToInt32(DR1["Carry"].ToString()).ToString("000") + Convert.ToInt32(DR1["Pos"].ToString()).ToString("00") + Convert.ToInt32(DR1["Depth"].ToString()).ToString("0") + Convert.ToInt32(SurplusQty + ActionQty).ToString("0000") + Convert.ToInt32(DR1["StoreNo"].ToString()).ToString("00000");
                        }
                        //暫時指定儲位、填入庫日期
                        string _StoreNo = DR1["StoreNo"].ToString();
                        Sqlstr = _SqlData.GetData("儲位", 9);
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?2", 2 + "");
                        Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                        ConnectQuery(Sqlstr);
                    }
                    else
                    {
                        NoEmpty = true;
                    }
                    //如果空儲位空間不足
                    if (NoEmpty)
                    {
                        _ObjInput = _ObjInput + "儲位空間不足，無法放入自動倉儲!!!!" + "\r\n";
                        KXMSSysPara.Sys.CancelTempStore();
                        return _ObjInput;
                    }
                    if (ActionQty > 0) { goto Redo; }
                }
                ActionStore = ActionStore.Substring(1);
                //Send command 及 過帳=======================================================
                if (ActionStore.Length > 0)
                {
                    S = ActionStore.Split(',');
                    for (i = 0; i <= S.GetUpperBound(0); i++)
                    {
                        //取得PID
                        Sqlstr = _SqlData.GetData("異動", 3);
                        DataTable DT0 = ConnectQuery(Sqlstr);
                        PID = int.Parse(DT0.Rows[0][0].ToString());
                        PID += 1;
                        //拆解儲位資訊
                        MachineNo = int.Parse(S[i].Substring(0, 2));
                        Carry = int.Parse(S[i].Substring(2, 3));
                        Pos = int.Parse(S[i].Substring(5, 2));
                        Depth = int.Parse(S[i].Substring(7, 1));
                        Qty = int.Parse(S[i].Substring(8, 4));
                        StoreNo = int.Parse(S[i].Substring(12, 5));

                        //檢查是否插單=======================================================
                        if (KXMSSysPara.Sys.IsTest == 0)
                        {
                            //正常
                            if (InType == 1)
                            {
                                _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E1", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+ (" + WorkOrderNo + ")" + Mno, "", PID, 4);
                            }
                            //插單
                            else if (InType == 2)
                            {
                                _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E2", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+ (" + WorkOrderNo + ")" + Mno, "", PID, 4);
                            }
                        }

                        //查詢是新增或是修改
                        Sqlstr = _SqlData.GetData("儲位", 15);
                        Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                        DataTable DT1 = ConnectQuery(Sqlstr);
                        //修改StoreM
                        if (DT1.Rows.Count > 0)
                        {
                            Sqlstr = _SqlData.GetData("儲位", 12);
                            Sqlstr = Sqlstr.Replace("?1", Qty + int.Parse(DT1.Rows[0]["MQty"].ToString()) + "");
                            Sqlstr = Sqlstr.Replace("?2", StoreNo + "");
                            Sqlstr = Sqlstr.Replace("?3", DT1.Rows[0]["MID"].ToString());
                            Connect(Sqlstr);
                        }
                        //新增
                        else
                        {
                            //新增MMID
                            Sqlstr = _SqlData.GetData("儲位", 21);
                            Sqlstr = Sqlstr.Replace("?1", StoreHouse);
                            Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                            Sqlstr = Sqlstr.Replace("?3", "");
                            Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                            Sqlstr = Sqlstr.Replace("?5", PackageNo + "");
                            Sqlstr = Sqlstr.Replace("?6", Location);
                            Sqlstr = Sqlstr.Replace("?7", "");
                            Sqlstr = Sqlstr.Replace("?8", "");
                            Connect(Sqlstr);

                            Sqlstr = _SqlData.GetData("儲位", 26);
                            DataTable DT2 = ConnectQuery(Sqlstr);
                            MMID = Convert.ToInt32(DT2.Rows[0][0].ToString());
                            //新增StoreM
                            Sqlstr = _SqlData.GetData("儲位", 11);
                            Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                            Sqlstr = Sqlstr.Replace("?2", MMID + "");
                            Sqlstr = Sqlstr.Replace("?3", Qty + "");
                            Connect(Sqlstr);
                        }
                        //===============================================================
                        //新增異動紀錄
                        Sqlstr = _SqlData.GetData("異動", 4);
                        Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);     //工單單號
                        Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                        Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                        Sqlstr = Sqlstr.Replace("?6", 0 + "");
                        Sqlstr = Sqlstr.Replace("?7", Qty + "");
                        Sqlstr = Sqlstr.Replace("?8", "");
                        Sqlstr = Sqlstr.Replace("?9", MachineNo + "");
                        Sqlstr = Sqlstr.Replace("?01", PID + "");
                        Sqlstr = Sqlstr.Replace("?02", Location);
                        Sqlstr = Sqlstr.Replace("?03", StoreHouse);    //庫位
                        Sqlstr = Sqlstr.Replace("?04", "");
                        Sqlstr = Sqlstr.Replace("?05", "");
                        Sqlstr = Sqlstr.Replace("?06", "");
                        Connect(Sqlstr);

                        continue;
                    }
                }
                else
                {
                    _ObjInput = _ObjInput + "儲位空間不足, 無法放入自動倉儲!!!" + "\r\n";
                }
            }
            //(((成品倉)))###################################################################
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                //(第一步驟)找已存在儲位=====================================================
                Sqlstr = _SqlData.GetData("儲位", 31);
                Sqlstr = Sqlstr.Replace("?1", Mno);
                Sqlstr = Sqlstr.Replace("?2", Location);
                Sqlstr = Sqlstr.Replace("?3", StoreHouse);
                Sqlstr = Sqlstr.Replace("?4", PkID + "");
                DataTable DT = ConnectQuery(Sqlstr);
                if (DT.Rows.Count > 0)
                {
                    //現有儲位
                    SurplusQty = 0;
                    foreach (DataRow DR in DT.Rows)
                    {
                        //儲位空間 = width的倍數*height的倍數*最大量
                        //賸下空間 = 儲位空間-已存放量
                        SurplusQty = Convert.ToInt32((double.Parse(DR["Width"].ToString()) / Mx) * (double.Parse(DR["Height"].ToString()) / My) * Maxqty * KXMSSysPara.Sys.StoreHeight(Convert.ToInt32(DR["Carry"].ToString()))) - Convert.ToInt32(DR["MQty"].ToString());
                        if (SurplusQty > 0)
                        {
                            //@如果空間可以一次放進去
                            if (SurplusQty >= ActionQty)
                            {
                                ActionStore = ActionStore + "," + int.Parse(DR["MachineNo"].ToString()).ToString("00") + int.Parse(DR["Carry"].ToString()).ToString("000") + int.Parse(DR["Pos"].ToString()).ToString("00") + int.Parse(DR["Depth"].ToString()).ToString("0") + int.Parse(ActionQty.ToString()).ToString("0000") + int.Parse(DR["StoreNo"].ToString()).ToString("00000");
                                ActionQty = 0;
                            }
                            //@有儲位，但空間小於25，且入庫的東西大於25，則不入，另放新的儲位
                            else if (SurplusQty < ActionQty && SurplusQty < 25)
                            {
                                FindEmpty = true;
                            }
                            //@
                            else
                            {
                                if (MessageBox.Show("現有空間不足，無法一次放入" + "\r\n" + "是 => 拆分批放" + "\r\n" + "否 => 找新的儲位", "KSMrp", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                                {
                                    FindEmpty = true;
                                    return "";
                                }
                                else
                                {
                                    ActionQty = ActionQty - (SurplusQty / 25) * 25;
                                    ActionStore = ActionStore + "," + int.Parse(DR["MachineNo"].ToString()).ToString("00") + int.Parse(DR["Carry"].ToString()).ToString("000") + int.Parse(DR["Pos"].ToString()).ToString("00") + int.Parse(DR["Depth"].ToString()).ToString("0") + int.Parse(((SurplusQty / 25) * 25).ToString()).ToString("0000") + int.Parse(DR["StoreNo"].ToString()).ToString("00000");
                                    FindEmpty = true;
                                }
                            }
                        }
                        else
                        {
                            //空間不夠(已經放滿了)
                            FindEmpty = true;
                        }
                    }
                }
                else
                {
                    //沒有現有儲位
                    FirstIn = true;
                    FindEmpty = true;
                }

                //(第二步驟)找空的儲位========================================================
                if (ActionQty > 0 && FindEmpty)
                {
                    DataTable DT0 = ConnectQuery(Sqlstr);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    DataTable DT2 = ConnectQuery(Sqlstr);
                    //成品倉專用的入庫方式(A,B二種包裝如果<20)
                    if (ActionQty < KXMSSysPara.Sys.MinNumForAB && (FirstIn = true) && (PkID == 11 || PkID == 12))
                    {
                        Sqlstr = _SqlData.GetData("儲位", 61);
                        Sqlstr = Sqlstr.Replace("?1", 2 + "");
                        DT0 = ConnectQuery(Sqlstr);
                        if (DT0.Rows.Count == 0)
                        {
                            FirstIn = false;
                        }
                    }
                    else
                    {
                        FirstIn = false;
                    }

                    if (FirstIn == false)
                    {
                        //檢查是否有剛好的空儲位(一次放)排序由小到大*************************
                        x = (int)(ActionQty / Maxqty) + (int)((ActionQty % Maxqty) * 10);
                        Sqlstr = _SqlData.GetData("儲位", 32);
                        Sqlstr = Sqlstr.Replace("?1", x + "");
                        Sqlstr = Sqlstr.Replace("?2", My + "");
                        Sqlstr = Sqlstr.Replace("?3", KXMSSysPara.Sys.WareHouse + "");
                        DT1 = ConnectQuery(Sqlstr);
                        //檢查是否其他的小儲位(分批放)排序由大到小****************************
                        if (DT1.Rows.Count == 0)
                        {
                            Sqlstr = _SqlData.GetData("儲位", 42);
                            Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                            DT2 = ConnectQuery(Sqlstr);
                        }
                    }

                    if (DT0.Rows.Count > 0)
                    {
                        DataRow DR0 = DT0.Rows[0];
                        NoEmpty = false;
                        //計算儲位容量
                        SurplusQty = Convert.ToInt32((double.Parse(DR0["Width"].ToString()) / Mx) * (double.Parse(DR0["Height"].ToString()) / My) * Maxqty) * KXMSSysPara.Sys.StoreHeight(Convert.ToInt32(DR0["Carry"].ToString()));
                        ActionQty = ActionQty - SurplusQty;
                        if (ActionQty > 0)
                        {
                            ActionStore = ActionStore + "," + int.Parse(DR0["MachineNo"].ToString()).ToString("00") + int.Parse(DR0["Carry"].ToString()).ToString("000") + int.Parse(DR0["Pos"].ToString()).ToString("00") + int.Parse(DR0["Depth"].ToString()).ToString("0") + int.Parse((SurplusQty).ToString()).ToString("0000") + int.Parse(DR0["StoreNo"].ToString()).ToString("00000");
                        }
                        else
                        {
                            ActionStore = ActionStore + "," + int.Parse(DR0["MachineNo"].ToString()).ToString("00") + int.Parse(DR0["Carry"].ToString()).ToString("000") + int.Parse(DR0["Pos"].ToString()).ToString("00") + int.Parse(DR0["Depth"].ToString()).ToString("0") + int.Parse((SurplusQty + ActionQty).ToString()).ToString("0000") + int.Parse(DR0["StoreNo"].ToString()).ToString("00000");
                        }
                        //暫時指定儲位、填入庫日期
                        string _StoreNo = DR0["StoreNo"].ToString();
                        Sqlstr = _SqlData.GetData("儲位", 9);
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?2", 2 + "");
                        Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                        ConnectQuery(Sqlstr);
                    }
                    else if (DT1.Rows.Count > 0)
                    {
                        DataRow DR1 = DT1.Rows[0];
                        NoEmpty = false;
                        //計算儲位容量
                        SurplusQty = Convert.ToInt32((double.Parse(DR1["Width"].ToString()) / Mx) * (double.Parse(DR1["Height"].ToString()) / My) * Maxqty) * KXMSSysPara.Sys.StoreHeight(Convert.ToInt32(DR1["Carry"].ToString()));
                        ActionQty = ActionQty - SurplusQty;
                        if (ActionQty > 0)
                        {
                            ActionStore = ActionStore + "," + int.Parse(DR1["MachineNo"].ToString()).ToString("00") + int.Parse(DR1["Carry"].ToString()).ToString("000") + int.Parse(DR1["Pos"].ToString()).ToString("00") + int.Parse(DR1["Depth"].ToString()).ToString("0") + int.Parse((SurplusQty).ToString()).ToString("0000") + int.Parse(DR1["StoreNo"].ToString()).ToString("00000");
                        }
                        else
                        {
                            ActionStore = ActionStore + "," + int.Parse(DR1["MachineNo"].ToString()).ToString("00") + int.Parse(DR1["Carry"].ToString()).ToString("000") + int.Parse(DR1["Pos"].ToString()).ToString("00") + int.Parse(DR1["Depth"].ToString()).ToString("0") + int.Parse((SurplusQty + ActionQty).ToString()).ToString("0000") + int.Parse(DR1["StoreNo"].ToString()).ToString("00000");
                        }
                        //暫時指定儲位、填入庫日期
                        string _StoreNo = DR1["StoreNo"].ToString();
                        Sqlstr = _SqlData.GetData("儲位", 9);
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?2", 2 + "");
                        Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                        ConnectQuery(Sqlstr);
                    }
                    else if (DT2.Rows.Count > 0)
                    {
                        DataRow DR2 = DT2.Rows[0];
                        NoEmpty = false;
                        //計算儲位容量
                        SurplusQty = Convert.ToInt32((double.Parse(DR2["Width"].ToString()) / Mx) * (double.Parse(DR2["Height"].ToString()) / My) * Maxqty) * KXMSSysPara.Sys.StoreHeight(Convert.ToInt32(DR2["Carry"].ToString()));
                        ActionQty = ActionQty - SurplusQty;
                        if (ActionQty > 0)
                        {
                            ActionStore = ActionStore + "," + int.Parse(DR2["MachineNo"].ToString()).ToString("00") + int.Parse(DR2["Carry"].ToString()).ToString("000") + int.Parse(DR2["Pos"].ToString()).ToString("00") + int.Parse(DR2["Depth"].ToString()).ToString("0") + int.Parse((SurplusQty).ToString()).ToString("0000") + int.Parse(DR2["StoreNo"].ToString()).ToString("00000");
                        }
                        else
                        {
                            ActionStore = ActionStore + "," + int.Parse(DR2["MachineNo"].ToString()).ToString("00") + int.Parse(DR2["Carry"].ToString()).ToString("000") + int.Parse(DR2["Pos"].ToString()).ToString("00") + int.Parse(DR2["Depth"].ToString()).ToString("0") + int.Parse((SurplusQty + ActionQty).ToString()).ToString("0000") + int.Parse(DR2["StoreNo"].ToString()).ToString("00000");
                        }
                        //暫時指定儲位、填入庫日期
                        string _StoreNo = DR2["StoreNo"].ToString();
                        Sqlstr = _SqlData.GetData("儲位", 9);
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?2", 2 + "");
                        Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                        ConnectQuery(Sqlstr);
                    }
                    else
                    {
                        NoEmpty = true;
                    }

                    //如果儲位空間不足
                    if (NoEmpty)
                    {
                        _ObjInput = _ObjInput + "儲位空間不足, 無法放入自動倉儲!!!" + "\r\n";
                        KXMSSysPara.Sys.CancelTempStore();
                        return _ObjInput;
                    }
                    if (ActionQty > 0) { goto Redo; }
                }

                ActionStore = ActionStore.Substring(1);
                //Send Command 及 過帳=========================================================
                if (ActionStore.Length > 0)
                {
                    S = ActionStore.Split(',');
                    for (i = 0; i <= S.GetUpperBound(0); i++)
                    {
                        //取得PID
                        Sqlstr = _SqlData.GetData("異動", 3);
                        DataTable DT0 = ConnectQuery(Sqlstr);
                        PID = int.Parse(DT0.Rows[0][0].ToString());
                        PID += 1;
                        //拆解儲位的資訊
                        MachineNo = int.Parse(S[i].Substring(0, 2));
                        Carry = int.Parse(S[i].Substring(2, 3));
                        Pos = int.Parse(S[i].Substring(5, 2));
                        Depth = int.Parse(S[i].Substring(7, 1));
                        Qty = int.Parse(S[i].Substring(8, 4));
                        StoreNo = int.Parse(S[i].Substring(12, 5));

                        //正常
                        if (InType == 1)
                        {
                            _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E1", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+ " + FinishNo, "", PID, 4);
                        }
                        //插單
                        else if (InType == 2)
                        {
                            _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E2", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+ " + FinishNo, "", PID, 4);
                        }

                        //查詢是新增或是修改=================================================
                        Sqlstr = _SqlData.GetData("儲位", 15);
                        Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                        DataTable DT1 = ConnectQuery(Sqlstr);
                        //修改StoreM
                        if (DT1.Rows.Count > 0)
                        {
                            DataRow DR1 = DT1.Rows[0];
                            Sqlstr = _SqlData.GetData("儲位", 12);
                            Sqlstr = Sqlstr.Replace("?1", Qty + DR1["MQty"].ToString());
                            Sqlstr = Sqlstr.Replace("?2", StoreNo + "");
                            Sqlstr = Sqlstr.Replace("?3", DR1["MID"].ToString());
                            Connect(Sqlstr);
                        }
                        //新增
                        else
                        {
                            //新增MMain
                            Sqlstr = _SqlData.GetData("儲位", 21);
                            Sqlstr = Sqlstr.Replace("?1", StoreHouse);
                            Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                            Sqlstr = Sqlstr.Replace("?3", Location);
                            Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                            Sqlstr = Sqlstr.Replace("?5", PackageNo + "");
                            Sqlstr = Sqlstr.Replace("?6", "");
                            Sqlstr = Sqlstr.Replace("?7", "");
                            Sqlstr = Sqlstr.Replace("?8", "");
                            Connect(Sqlstr);

                            Sqlstr = _SqlData.GetData("儲位", 26);
                            DataTable DT2 = ConnectQuery(Sqlstr);
                            MMID = Convert.ToInt32(DT2.Rows[0][0].ToString());
                            //新增StoreM
                            Sqlstr = _SqlData.GetData("儲位", 11);
                            Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                            Sqlstr = Sqlstr.Replace("?2", MMID + "");
                            Sqlstr = Sqlstr.Replace("?3", Qty + "");
                            Connect(Sqlstr);
                        }
                        //===============================================================
                        //新增異動紀錄
                        Sqlstr = _SqlData.GetData("異動", 4);
                        Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);     //工單單號
                        Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                        Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                        Sqlstr = Sqlstr.Replace("?6", 0 + "");
                        Sqlstr = Sqlstr.Replace("?7", Qty + "");
                        Sqlstr = Sqlstr.Replace("?8", "");
                        Sqlstr = Sqlstr.Replace("?9", MachineNo + "");
                        Sqlstr = Sqlstr.Replace("?01", PID + "");
                        Sqlstr = Sqlstr.Replace("?02", Location);
                        Sqlstr = Sqlstr.Replace("?03", StoreHouse);    //庫位
                        Sqlstr = Sqlstr.Replace("?04", "");
                        Sqlstr = Sqlstr.Replace("?05", "");
                        Sqlstr = Sqlstr.Replace("?06", "");
                        Connect(Sqlstr);

                        continue;
                    }
                }
                else
                {
                    _ObjInput = _ObjInput + "儲位空間不足, 無法放入自動倉儲!!!" + "\r\n";
                }
            }
            //(((IC倉)))######################################################################
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //(第二步驟) 找空的儲位======================================================
                if (rbOp3.Checked == true)
                {
                    if (ActionQty > 0)
                    {
                        if (rbOp1.Checked == true)
                        {
                            Package = 23;
                        }
                        else
                        {
                            Package = 22;
                        }

                        Sqlstr = _SqlData.GetData("儲位", 72);
                        Sqlstr = Sqlstr.Replace("?1", Package + "");
                        Sqlstr = Sqlstr.Replace("?2", KXMSSysPara.Sys.MachineNo + "");
                        DataTable DT = ConnectQuery(Sqlstr);

                        if (DT.Rows.Count > 0)
                        {
                            DataRow DR = DT.Rows[0];            //抓出第一項
                            NoEmpty = false;

                            //計算儲位容量
                            ActionStore = ActionStore + "," + DR["MachineNo"].ToString() + "@" + DR["Carry"].ToString() + "@" + DR["Pos"].ToString() + "@" + DR["Depth"].ToString() + "@" + ActionQty + "@" + DR["StoreNo"].ToString();
                            ActionQty = 0;
                            //暫時指定儲位、填入庫日期
                            string _StoreNo = DR["StoreNo"].ToString();
                            Sqlstr = _SqlData.GetData("儲位", 9);
                            DtpIn.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?2", 2 + "");
                            Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                            Connect(Sqlstr);
                        }
                        //如果空儲位空間不足
                        if (NoEmpty)
                        {
                            _ObjInput = _ObjInput + "儲位空間不足，無法放入自動倉儲!!!!" + "\r\n";
                            KXMSSysPara.Sys.CancelTempStore();
                            return _ObjInput;
                        }
                        if (ActionQty > 0) { goto Redo; }
                    }
                    ActionStore = ActionStore.Substring(1);
                    //Send command 及過帳=====================================================================
                    if (ActionStore.Length > 0)
                    {
                        S = ActionStore.Split(',');
                        for (i = 0; i <= S.GetUpperBound(0); i++)
                        {
                            //取得PID
                            Sqlstr = _SqlData.GetData("異動", 3);
                            DataTable DT = ConnectQuery(Sqlstr);
                            PID = int.Parse(DT.Rows[0][0].ToString());
                            PID = PID + 1;

                            S2 = S[i].Split('@');
                            if (S2.GetUpperBound(0) == 5)
                            {
                                MachineNo = int.Parse(S2[0]);
                                Carry = int.Parse(S2[1]);
                                Pos = int.Parse(S2[2]);
                                Depth = int.Parse(S2[3]);
                                Qty = int.Parse(S2[4]);
                                StoreNo = int.Parse(S2[5]);
                            }
                            else
                            {
                                goto Next_i;
                            }

                            //檢查是否插單=======================================================
                            string ShowStr = "";
                            int ShowQty = 0;
                            string ShowLocation = "";
                            string ShowOv = "";

                            //Location txtLocationP3  Ov  txtBthNbP3
                            ShowStr = Mno + "*" + Location + "*" + Ov;
                            if (ShowStr.Length > 38)
                            {
                                ShowStr = ShowStr.Substring(0, 38);
                            }
                            //正常
                            if (Qty > 999999)
                            {
                                ShowQty = 999999;
                            }
                            else
                            {
                                ShowQty = Qty;
                            }

                            if (MachineNo == 1)
                            {
                                if (InType == 1)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E1", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), ShowQty, "+" + ShowStr, "", PID, 6);
                                }
                                //插單
                                else if (InType == 2)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E2", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), ShowQty, "+" + ShowStr, "", PID, 6);
                                }
                            }
                            else if (MachineNo == 4 || MachineNo == 5)
                            {
                                if (Location.Length > 20)
                                {
                                    ShowLocation = Location.Substring(0, 20);
                                }
                                else
                                {
                                    ShowLocation = Location;
                                }
                                if (Ov.Length > 20)
                                {
                                    ShowOv = Ov.Substring(0, 20);
                                }
                                else
                                {
                                    ShowOv = Ov;
                                }
                                //正常
                                if (InType == 1)
                                {
                                    _modCtrl.SendC3Command(Convert.ToInt16(MachineNo), 1, "E1", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+" + Mno, ShowLocation, ShowOv, "", PID);
                                }
                                //插單
                                else if (InType == 2)
                                {
                                    _modCtrl.SendC3Command(Convert.ToInt16(MachineNo), 1, "E2", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+" + Mno, ShowLocation, ShowOv, "", PID);
                                }
                            }

                            //查詢是新增或是修改=======================================================================
                            Sqlstr = _SqlData.GetData("儲位", 15);
                            Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                            DataTable DT0 = ConnectQuery(Sqlstr);
                            if (DT0.Rows.Count > 0)
                            {
                                //修改StoreM
                                Sqlstr = _SqlData.GetData("儲位", 12);
                                Sqlstr = Sqlstr.Replace("?1", Qty + int.Parse(DT0.Rows[0]["MQty"].ToString()) + "");
                                Sqlstr = Sqlstr.Replace("?2", StoreNo + "");
                                Sqlstr = Sqlstr.Replace("?3", DT0.Rows[0]["MID"].ToString());
                                Connect(Sqlstr);
                            }
                            else
                            {
                                //新增MMID
                                Sqlstr = _SqlData.GetData("儲位", 21);
                                Sqlstr = Sqlstr.Replace("?1", StoreHouse);
                                Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                                Sqlstr = Sqlstr.Replace("?3", Location);
                                Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                                Sqlstr = Sqlstr.Replace("?5", PackageNo + "");
                                Sqlstr = Sqlstr.Replace("?6", Remark);
                                Sqlstr = Sqlstr.Replace("?7", Ov);
                                Sqlstr = Sqlstr.Replace("?8", MDesc2);
                                Connect(Sqlstr);

                                Sqlstr = _SqlData.GetData("儲位", 26);
                                DataTable DT1 = ConnectQuery(Sqlstr);
                                MMID = Convert.ToInt32(DT1.Rows[0][0].ToString());
                                //新增StoreM
                                Sqlstr = _SqlData.GetData("儲位", 11);
                                Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                                Sqlstr = Sqlstr.Replace("?2", MMID + "");
                                Sqlstr = Sqlstr.Replace("?3", Qty + "");
                                Connect(Sqlstr);
                            }
                            //====================================================================
                            //新增異動紀錄
                            Sqlstr = _SqlData.GetData("異動", 4);
                            Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);  //工單單號
                            Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                            Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                            DtpIn.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                            Sqlstr = Sqlstr.Replace("?6", 0 + "");
                            Sqlstr = Sqlstr.Replace("?7", Qty + "");
                            Sqlstr = Sqlstr.Replace("?8", "");
                            Sqlstr = Sqlstr.Replace("?9", MachineNo + "");
                            Sqlstr = Sqlstr.Replace("?01", PID + "");
                            Sqlstr = Sqlstr.Replace("?02", Location);
                            Sqlstr = Sqlstr.Replace("?03", StoreHouse);  //庫位
                            Sqlstr = Sqlstr.Replace("?04", Ov);
                            Sqlstr = Sqlstr.Replace("?05", MDesc2);
                            Sqlstr = Sqlstr.Replace("?06", Remark);
                            Connect(Sqlstr);

                        Next_i:
                            continue;
                        }
                    }
                    else
                    {
                        _ObjInput = _ObjInput + "儲存空間不足，無法放入自動倉儲!!!" + "\r\n";
                    }

                }
                //入平置倉
                else
                {
                    //新增MMID
                    MachineNo = 0;
                    Qty = Convert.ToInt32(ActionQty);
                    if (cbDataP3.SelectedIndex >= 0)
                    {
                        StoreNo = Convert.ToInt32(txtStoreNo.Text);
                        Sqlstr = _SqlData.GetData("儲位", 21);
                        Sqlstr = Sqlstr.Replace("?1", StoreHouse);
                        Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                        Sqlstr = Sqlstr.Replace("?3", Location);
                        Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                        Sqlstr = Sqlstr.Replace("?5", PackageNo + "");
                        Sqlstr = Sqlstr.Replace("?6", Remark);
                        Sqlstr = Sqlstr.Replace("?7", Ov);
                        Sqlstr = Sqlstr.Replace("?8", MDesc2);
                        Connect(Sqlstr);

                        Sqlstr = _SqlData.GetData("儲位", 26);
                        DataTable DT = ConnectQuery(Sqlstr);
                        MMID = Convert.ToInt32(DT.Rows[0][0].ToString());
                        //新增StoreM
                        Sqlstr = _SqlData.GetData("儲位", 11);
                        Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                        Sqlstr = Sqlstr.Replace("?2", MMID + "");
                        Sqlstr = Sqlstr.Replace("?3", Qty + "");
                        Connect(Sqlstr);

                        //新增異動紀錄=====================================================
                        Sqlstr = _SqlData.GetData("異動", 9);
                        Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);        //工單單號
                        Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                        Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                        Sqlstr = Sqlstr.Replace("?6", 0 + "");
                        Sqlstr = Sqlstr.Replace("?7", Qty + "");
                        Sqlstr = Sqlstr.Replace("?8", "");
                        Sqlstr = Sqlstr.Replace("?9", MachineNo + "");
                        Sqlstr = Sqlstr.Replace("?01", PID + "");
                        Sqlstr = Sqlstr.Replace("?02", Location + "");
                        Sqlstr = Sqlstr.Replace("?03", StoreHouse);       //庫位
                        Sqlstr = Sqlstr.Replace("?04", Ov);
                        Sqlstr = Sqlstr.Replace("?05", MDesc2);
                        Sqlstr = Sqlstr.Replace("?06", Remark);
                        Connect(Sqlstr);
                    }
                    else
                    {
                        _ObjInput = _ObjInput + "請選擇平置倉" + "\r\n";
                    }
                }
            }
            KXMSSysPara.Sys.CancelTempStore();
            return _ObjInput;
        }

        public string ObjInput3(long PkID, int Mx, int My, long Maxqty, string StoreHouse, string Mno, string Location, long ActionQty, string WorkOrderNo, int PackageNo, int InType, int IsFirstIn, string Ov = "", string VanderName = "", string Lotcode = "", string Remark = "", string MDesc2 = "")
        {
            string _ObjInput = "";
            int TQ;
            string ActionStore = "";  //SSSSQQQQ
            string[] S;
            string[] S2;
            int i;
            int j;
            int k;
            long PID = 0;
            bool FindEmpty = false;
            bool NoEmpty = true;
            string Sqlstr;
            int Qty = 0;
            long SurplusQty;    //賸下的空間

            long StoreNo = 0;
            int MachineNo = 0;
            int Carry = 0;
            int Pos = 0;
            int Depth = 0;
            long MMID;          //MMain的id
            int x;

            bool FirstIn;       //是否第一次入庫
            int StoreCounts;    //計算要放幾個儲位
            int GoodMachineNo;  //找出合適的
            int GoodCarry;      //找出合適的
            int Package;

        Redo:
            FirstIn = false;
            //(((IC倉)))######################################################################
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //(第二步驟) 找空的儲位======================================================
                if (rbOp3.Checked == true)
                {
                    if (ActionQty > 0)
                    {
                        if (rbOp1.Checked == true)
                        {
                            Package = 23;
                        }
                        else
                        {
                            Package = 22;
                        }

                        Sqlstr = _SqlData.GetData("儲位", 72);
                        Sqlstr = Sqlstr.Replace("?1", Package + "");
                        Sqlstr = Sqlstr.Replace("?2", KXMSSysPara.Sys.MachineNo + "");
                        DataTable DT = ConnectQuery(Sqlstr);

                        if (DT.Rows.Count > 0)
                        {
                            DataRow DR = DT.Rows[0];            //抓出第一項
                            NoEmpty = false;

                            //計算儲位容量
                            ActionStore = ActionStore + "," + DR["MachineNo"].ToString() + "@" + DR["Carry"].ToString() + "@" + DR["Pos"].ToString() + "@" + DR["Depth"].ToString() + "@" + ActionQty + "@" + DR["StoreNo"].ToString();
                            ActionQty = 0;
                            //暫時指定儲位、填入庫日期
                            string _StoreNo = DR["StoreNo"].ToString();
                            Sqlstr = _SqlData.GetData("儲位", 9);
                            DtpIn.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?1", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?2", 2 + "");
                            Sqlstr = Sqlstr.Replace("?3", _StoreNo);
                            Connect(Sqlstr);
                        }
                        //如果空儲位空間不足
                        if (NoEmpty)
                        {
                            _ObjInput = _ObjInput + "儲位空間不足，無法放入自動倉儲!!!!" + "\r\n";
                            KXMSSysPara.Sys.CancelTempStore();
                            return _ObjInput;
                        }
                        if (ActionQty > 0) { goto Redo; }
                    }
                    ActionStore = ActionStore.Substring(1);
                    //Send command 及過帳=====================================================================
                    if (ActionStore.Length > 0)
                    {
                        S = ActionStore.Split(',');
                        for (i = 0; i <= S.GetUpperBound(0); i++)
                        {
                            //取得PID
                            Sqlstr = _SqlData.GetData("異動", 3);
                            DataTable DT = ConnectQuery(Sqlstr);
                            PID = int.Parse(DT.Rows[0][0].ToString());
                            PID = PID + 1;

                            S2 = S[i].Split('@');
                            if (S2.GetUpperBound(0) == 5)
                            {
                                MachineNo = int.Parse(S2[0]);
                                Carry = int.Parse(S2[1]);
                                Pos = int.Parse(S2[2]);
                                Depth = int.Parse(S2[3]);
                                Qty = int.Parse(S2[4]);
                                StoreNo = int.Parse(S2[5]);
                            }
                            else
                            {
                                goto Next_i;
                            }

                            //檢查是否插單=======================================================
                            string ShowStr = "";
                            int ShowQty = 0;
                            string ShowLocation = "";
                            string ShowOv = "";

                            //Location txtLocationP3  Ov  txtBthNbP3
                            ShowStr = Mno + "*" + Location + "*" + Ov;
                            if (ShowStr.Length > 38)
                            {
                                ShowStr = ShowStr.Substring(0, 38);
                            }
                            //正常
                            if (Qty > 999999)
                            {
                                ShowQty = 999999;
                            }
                            else
                            {
                                ShowQty = Qty;
                            }

                            if (MachineNo == 1)
                            {
                                if (InType == 1)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E1", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), ShowQty, "+" + ShowStr, "", PID, 6);
                                }
                                //插單
                                else if (InType == 2)
                                {
                                    _modCtrl.SendCommand(Convert.ToInt16(MachineNo), "E2", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), ShowQty, "+" + ShowStr, "", PID, 6);
                                }
                            }
                            else if (MachineNo == 4 || MachineNo == 5)
                            {
                                if (Location.Length > 20)
                                {
                                    ShowLocation = Location.Substring(0, 20);
                                }
                                else
                                {
                                    ShowLocation = Location;
                                }
                                if (Ov.Length > 20)
                                {
                                    ShowOv = Ov.Substring(0, 20);
                                }
                                else
                                {
                                    ShowOv = Ov;
                                }
                                //正常
                                if (InType == 1)
                                {
                                    _modCtrl.SendC3Command(Convert.ToInt16(MachineNo), 1, "E1", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+" + Mno, ShowLocation, ShowOv, "", PID);
                                }
                                //插單
                                else if (InType == 2)
                                {
                                    _modCtrl.SendC3Command(Convert.ToInt16(MachineNo), 1, "E2", Convert.ToInt16(Carry), Convert.ToInt16(Pos), Convert.ToInt16(Depth + 1), Qty, "+" + Mno, ShowLocation, ShowOv, "", PID);
                                }
                            }

                            //查詢是新增或是修改=======================================================================
                            Sqlstr = _SqlData.GetData("儲位", 15);
                            Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                            DataTable DT0 = ConnectQuery(Sqlstr);
                            if (DT0.Rows.Count > 0)
                            {
                                //修改StoreM
                                Sqlstr = _SqlData.GetData("儲位", 12);
                                Sqlstr = Sqlstr.Replace("?1", Qty + int.Parse(DT0.Rows[0]["MQty"].ToString()) + "");
                                Sqlstr = Sqlstr.Replace("?2", StoreNo + "");
                                Sqlstr = Sqlstr.Replace("?3", DT0.Rows[0]["MID"].ToString());
                                Connect(Sqlstr);
                            }
                            else
                            {
                                //新增MMID
                                Sqlstr = _SqlData.GetData("儲位", 87);
                                Sqlstr = Sqlstr.Replace("?1", StoreHouse);
                                Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                                Sqlstr = Sqlstr.Replace("?3", Location);
                                Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                                Sqlstr = Sqlstr.Replace("?5", PackageNo + "");
                                Sqlstr = Sqlstr.Replace("?6", Remark);
                                Sqlstr = Sqlstr.Replace("?7", Ov);
                                Sqlstr = Sqlstr.Replace("?8", MDesc2);
                                Sqlstr = Sqlstr.Replace("?9", VanderName);
                                Sqlstr = Sqlstr.Replace("?01", Lotcode);
                                Connect(Sqlstr);

                                Sqlstr = _SqlData.GetData("儲位", 26);
                                DataTable DT1 = ConnectQuery(Sqlstr);
                                MMID = Convert.ToInt32(DT1.Rows[0][0].ToString());
                                //新增StoreM
                                Sqlstr = _SqlData.GetData("儲位", 11);
                                Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                                Sqlstr = Sqlstr.Replace("?2", MMID + "");
                                Sqlstr = Sqlstr.Replace("?3", Qty + "");
                                Connect(Sqlstr);
                            }
                            //====================================================================
                            //新增異動紀錄
                            Sqlstr = _SqlData.GetData("異動", 13);
                            Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);  //工單單號
                            Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                            Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                            DtpIn.Value = DateTime.Now;
                            Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                            Sqlstr = Sqlstr.Replace("?6", 0 + "");
                            Sqlstr = Sqlstr.Replace("?7", Qty + "");
                            Sqlstr = Sqlstr.Replace("?8", "");
                            Sqlstr = Sqlstr.Replace("?9", MachineNo + "");
                            Sqlstr = Sqlstr.Replace("?01", PID + "");
                            Sqlstr = Sqlstr.Replace("?02", Location);
                            Sqlstr = Sqlstr.Replace("?03", StoreHouse);  //庫位
                            Sqlstr = Sqlstr.Replace("?04", Ov);
                            Sqlstr = Sqlstr.Replace("?05", MDesc2);
                            Sqlstr = Sqlstr.Replace("?06", Remark);
                            Sqlstr = Sqlstr.Replace("?07", VanderName);
                            Sqlstr = Sqlstr.Replace("?08", Lotcode);
                            Connect(Sqlstr);

                        Next_i:
                            continue;
                        }
                    }
                    else
                    {
                        _ObjInput = _ObjInput + "儲存空間不足，無法放入自動倉儲!!!" + "\r\n";
                    }

                }
                //入平置倉
                else
                {
                    //新增MMID
                    MachineNo = 0;
                    Qty = Convert.ToInt32(ActionQty);
                    if (cbDataP3.SelectedIndex >= 0)
                    {
                        StoreNo = Convert.ToInt32(txtStoreNo.Text);
                        Sqlstr = _SqlData.GetData("儲位", 87);
                        Sqlstr = Sqlstr.Replace("?1", StoreHouse);
                        Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                        Sqlstr = Sqlstr.Replace("?3", Location);
                        Sqlstr = Sqlstr.Replace("?4", WorkOrderNo);
                        Sqlstr = Sqlstr.Replace("?5", PackageNo + "");
                        Sqlstr = Sqlstr.Replace("?6", Remark);
                        Sqlstr = Sqlstr.Replace("?7", Ov);
                        Sqlstr = Sqlstr.Replace("?8", MDesc2);
                        Sqlstr = Sqlstr.Replace("?9", VanderName);
                        Sqlstr = Sqlstr.Replace("?01", Lotcode);
                        Connect(Sqlstr);

                        Sqlstr = _SqlData.GetData("儲位", 26);
                        DataTable DT = ConnectQuery(Sqlstr);
                        MMID = Convert.ToInt32(DT.Rows[0][0].ToString());
                        //新增StoreM
                        Sqlstr = _SqlData.GetData("儲位", 11);
                        Sqlstr = Sqlstr.Replace("?1", StoreNo + "");
                        Sqlstr = Sqlstr.Replace("?2", MMID + "");
                        Sqlstr = Sqlstr.Replace("?3", Qty + "");
                        Connect(Sqlstr);

                        //新增異動紀錄=====================================================
                        Sqlstr = _SqlData.GetData("異動", 14);
                        Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);        //工單單號
                        Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                        Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
                        DtpIn.Value = DateTime.Now;
                        Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                        Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                        Sqlstr = Sqlstr.Replace("?6", 0 + "");
                        Sqlstr = Sqlstr.Replace("?7", Qty + "");
                        Sqlstr = Sqlstr.Replace("?8", "");
                        Sqlstr = Sqlstr.Replace("?9", MachineNo + "");
                        Sqlstr = Sqlstr.Replace("?01", PID + "");
                        Sqlstr = Sqlstr.Replace("?02", Location + "");
                        Sqlstr = Sqlstr.Replace("?03", StoreHouse);       //庫位
                        Sqlstr = Sqlstr.Replace("?04", Ov);
                        Sqlstr = Sqlstr.Replace("?05", MDesc2);
                        Sqlstr = Sqlstr.Replace("?06", Remark);
                        Sqlstr = Sqlstr.Replace("?07", VanderName);
                        Sqlstr = Sqlstr.Replace("?08", Lotcode);
                        Connect(Sqlstr);
                    }
                    else
                    {
                        _ObjInput = _ObjInput + "請選擇平置倉" + "\r\n";
                    }
                }
            }
            KXMSSysPara.Sys.CancelTempStore();
            return _ObjInput;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string ErrMsg = "";
            int InType = 2;
            string Sqlstr = "";
            int PackageNo = 0;
            string Pcode = "";   //批號
            int i = 0;

            //On Error Resume Next
            if (!DataCheck()) { return; }
            ////正常
            //if (btnIn.Tag.ToString() == "0")
            //{
            //    InType = 1;
            //}
            ////插單
            //else if (btnInsert.Tag.ToString() == "1")
            //{
            //    InType = 2;
            //}

            //=============================================================
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                PackageNo = Convert.ToInt32(txtStoreNo.Text);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                PackageNo = Convert.ToInt32(txtStoreNo.Text);
                FinishNo = txtNubP2.Text.ToUpper();
            }
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                //IC倉 批號檢查
                Pcode = txtOvP3.Text.Trim(charsToTrim).Replace(",", ".");        //批號','轉成'.'

                int WeekDiff;
                WeekDiff = PCodeDiffWeek(Pcode);
                if (WeekDiff == -99)
                {
                    //非數字格式或長度不足
                    if (MessageBox.Show("批號格式不符(年週)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        txtOvP3.Focus();
                        txtOvP3.SelectAll();
                        return;
                    }
                }
                else
                {
                    //比較年限
                    string PartNoType;
                    PartNoType = Mno1.Substring(0, 3);
                    if (PartNoType == "116" || PartNoType == "118")
                    {
                        if (WeekDiff > (52 * 3))
                        {
                            if (MessageBox.Show("此物料已超出年限(3年)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            {
                                txtOvP3.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (WeekDiff > 52)
                        {
                            if (MessageBox.Show("此物料已超出年限(1年)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            {
                                txtOvP3.Focus();
                                return;
                            }
                        }
                    }
                }
            }

            if (KXMSSysPara.Sys.WareHouse != 3)
            {
                //檢查包裝
                Sqlstr = _SqlData.GetData("其他", 15);
                Sqlstr = Sqlstr.Replace("?1", PackageNo + "");
                DataTable DT = ConnectQuery(Sqlstr);
                foreach (DataRow DR in DT.Rows)
                {
                    string S = DR["id"].ToString();
                    if (PackageNo != int.Parse(S))
                    {
                        MessageBox.Show("無此包裝", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            //入庫的Function(包裝的id、儲位比x、儲位比y、儲位比最大量、庫位、料號、Location(or備註)、數量、工單單號、包裝編號、是否插單(1正常2插單)、先進先出否(1是先進先出,2不是)
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                Sqlstr = _SqlData.GetData("其他", 15);
                Sqlstr = Sqlstr.Replace("?1", PackageNo + "");
                DataTable DT = ConnectQuery(Sqlstr);
                DataRow DR = DT.Rows[0];
                //依次數做入庫
                ErrMsg = ObjInput(int.Parse(DR["id"].ToString()), int.Parse(DR["Width"].ToString()), int.Parse(DR["Height"].ToString()), int.Parse(DR["MaxQty"].ToString()), cbDataP0.Text, Mno1, txtRemarkP1.Text.Trim(charsToTrim), int.Parse(txtAntP1.Text), txtNubP1.Text.Trim(charsToTrim), PackageNo, InType, 2);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                Sqlstr = _SqlData.GetData("其他", 15);
                Sqlstr = Sqlstr.Replace("?1", PackageNo + "");
                DataTable DT = ConnectQuery(Sqlstr);
                DataRow DR = DT.Rows[0];
                //依次數做入庫
                ErrMsg = ObjInput(int.Parse(DR["id"].ToString()), int.Parse(DR["Width"].ToString()), int.Parse(DR["Height"].ToString()), int.Parse(DR["MaxQty"].ToString()), cbDataP0.Text, Mno1, txtLocationP2.Text.Trim(charsToTrim), int.Parse(txtAntP2.Text), txtBox8.Text.Trim(charsToTrim), PackageNo, InType, 2);
            }
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                if (!IsNumeric(txtAntsP3.Text))
                {
                    txtAntsP3.Text = "1";
                }

                for (i = 1; i <= int.Parse(txtAntsP3.Text); i++)
                {
                    //依次數做入庫
                    ErrMsg = ObjInput3(0, 0, 0, 0, cbDataP0.Text, Mno1, txtLocationP3.Text.Trim(charsToTrim), Convert.ToInt32(txtAntP3.Text), txtBox8.Text.Trim(charsToTrim), 0, InType, 2, Pcode, txtVN.Text.Trim(), txtLotcode.Text.Trim(),txtRemarkP3.Text.Trim(charsToTrim), txtRemarksP3.Text.Trim(charsToTrim));
                }
            }

            if (ErrMsg.Length == 0)
            {
                //入庫完成
            }
            else
            {
                MessageBox.Show("入庫過程中，有部分資料有問題" + "\r\n" + ErrMsg, "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            ClearData();
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (rbOp2.Checked == true)
                {
                    lbrbOp.Text = FindEmptyStore(22) + "";
                }
                else if (rbOp1.Checked == true)
                {
                    lbrbOp.Text = FindEmptyStore(23) + "";
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string ErrMsg = "";
            int InType = 1;
            string Sqlstr = "";
            int PackageNo = 0;
            string Pcode = "";   //批號
            int i = 0;

            //On Error Resume Next
            if (!DataCheck())
            { return; }
            //正常
            //if (btnIn.Tag.ToString() == "0")
            //{
            //    InType = 1;
            //}
            //插單
            //else if (btnInsert.Tag.ToString() == "1")
            //{
            //    InType = 2;
            //}

            //=============================================================
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                PackageNo = Convert.ToInt32(txtStoreNo.Text);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                PackageNo = Convert.ToInt32(txtStoreNo.Text);
                FinishNo = txtNubP2.Text.ToUpper();
            }
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                //IC倉 批號檢查
                Pcode = txtOvP3.Text.Trim(charsToTrim).Replace(",", ".");        //批號','轉成'.'

                int WeekDiff;
                WeekDiff = PCodeDiffWeek(Pcode);
                if (WeekDiff == -99)
                {
                    //非數字格式或長度不足
                    if (MessageBox.Show("批號格式不符(年週)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        txtOvP3.Focus();
                        txtOvP3.SelectAll();
                        return;
                    }
                }
                else
                {
                    //比較年限
                    string PartNoType;
                    PartNoType = Mno1.Substring(0, 3);
                    if (PartNoType == "116" || PartNoType == "118")
                    {
                        if (WeekDiff > (52 * 3))
                        {
                            if (MessageBox.Show("此物料已超出年限(3年)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            {
                                txtOvP3.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (WeekDiff > 52)
                        {
                            if (MessageBox.Show("此物料已超出年限(1年)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            {
                                txtOvP3.Focus();
                                return;
                            }
                        }
                    }
                }
            }

            if (KXMSSysPara.Sys.WareHouse != 3)
            {
                //檢查包裝
                Sqlstr = _SqlData.GetData("其他", 15);
                Sqlstr = Sqlstr.Replace("?1", PackageNo + "");
                DataTable DT = ConnectQuery(Sqlstr);
                foreach (DataRow DR in DT.Rows)
                {
                    string S = DR["id"].ToString();
                    if (PackageNo != int.Parse(S))
                    {
                        MessageBox.Show("無此包裝", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            //入庫的Function(包裝的id、儲位比x、儲位比y、儲位比最大量、庫位、料號、Location(or備註)、數量、工單單號、包裝編號、是否插單(1正常2插單)、先進先出否(1是先進先出,2不是)
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                Sqlstr = _SqlData.GetData("其他", 15);
                Sqlstr = Sqlstr.Replace("?1", PackageNo + "");
                DataTable DT = ConnectQuery(Sqlstr);
                DataRow DR = DT.Rows[0];
                //依次數做入庫
                ErrMsg = ObjInput(int.Parse(DR["id"].ToString()), int.Parse(DR["Width"].ToString()), int.Parse(DR["Height"].ToString()), int.Parse(DR["MaxQty"].ToString()), cbDataP0.Text, Mno1, txtRemarkP1.Text.Trim(charsToTrim), int.Parse(txtAntP1.Text), txtNubP1.Text.Trim(charsToTrim), PackageNo, InType, 2);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                Sqlstr = _SqlData.GetData("其他", 15);
                Sqlstr = Sqlstr.Replace("?1", PackageNo + "");
                DataTable DT = ConnectQuery(Sqlstr);
                DataRow DR = DT.Rows[0];
                //依次數做入庫
                ErrMsg = ObjInput(int.Parse(DR["id"].ToString()), int.Parse(DR["Width"].ToString()), int.Parse(DR["Height"].ToString()), int.Parse(DR["MaxQty"].ToString()), cbDataP0.Text, Mno1, txtLocationP2.Text.Trim(charsToTrim), int.Parse(txtAntP2.Text), txtBox8.Text.Trim(charsToTrim), PackageNo, InType, 2);
            }
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                char[] charsToTrim = { '*', ' ', '/' };
                if (!IsNumeric(txtAntsP3.Text))
                {
                    txtAntsP3.Text = "1";
                }

                for (i = 1; i <= int.Parse(txtAntsP3.Text); i++)
                {
                    //依次數做入庫
                    ErrMsg = ObjInput3(0, 0, 0, 0, cbDataP0.Text, Mno1, txtLocationP3.Text.Trim(charsToTrim), Convert.ToInt32(txtAntP3.Text), txtBox8.Text.Trim(charsToTrim), 0, InType, 2, Pcode, txtVN.Text.Trim(), txtLotcode.Text.Trim() ,txtRemarkP3.Text.Trim(charsToTrim), txtRemarksP3.Text.Trim(charsToTrim));
                }
            }

            if (ErrMsg.Length == 0)
            {
                //入庫完成
            }
            else
            {
                MessageBox.Show("入庫過程中，有部分資料有問題" + "\r\n" + ErrMsg, "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            ClearData();
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (rbOp2.Checked == true)
                {
                    lbrbOp.Text = FindEmptyStore(22) + "";
                }
                else if (rbOp1.Checked == true)
                {
                    lbrbOp.Text = FindEmptyStore(23) + "";
                }
            }
        }

        public void ClearData()
        {
            //半成品倉
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                txtNubP1.Text = "";
                txtItemNbP1.Text = "";
                txtAntP1.Text = "";
                txtRemarkP1.Text = "";
                txtNubP1.Focus();
                cbDataP1.Text = KXMSSysPara.Sys.DefaultPackage;
                cbDataP0.Text = "T3";
            }
            //成品倉
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                txtUPCP2.Text = "";
                txtNubP2.Text = "";
                txtAntP2.Text = "";
                txtLocationP2.Text = "";
                txtUPCP2.Focus();
                cbDataP2.Text = KXMSSysPara.Sys.DefaultPackage;
                cbDataP0.Text = "T3";
            }
            //IC倉
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                txtItemNbP3.Text = "";
                txtLocationP3.Text = "";
                txtAntP3.Text = "";
                txtAntsP3.Text = "1";
                txtOvP3.Text = "";
                txtVN.Text = "";
                txtLotcode.Text = "";
                txtRemarkP3.Text = "";
                txtRemarksP3.Text = "";
                txtItemNbP3.Focus();
                //cbDataP0.Text = "T3";
            }
        }

        #region ComBox (id)
        //半成品倉
        private void cbDataP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id;
            if (cbDataP1.SelectedItem == null)
            {
                id = 0;
            }
            else
            {
                int.TryParse((cbDataP1.SelectedItem as ComboboxItem).Value, out id);
            }
            txtStoreNo.Text = id + "";
        }
        //成品倉
        private void cbDataP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id;
            if (cbDataP2.SelectedItem == null)
            {
                id = 0;
            }
            else
            {
                int.TryParse((cbDataP2.SelectedItem as ComboboxItem).Value, out id);
            }
            txtStoreNo.Text = id + "";
        }
        //IC倉
        private void cbDataP3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int StoreNo;
            if (cbDataP3.SelectedItem == null)
            {
                StoreNo = 0;
            }
            else
            {
                int.TryParse((cbDataP3.SelectedItem as ComboboxItem).Value, out StoreNo);
            }

            txtStoreNo.Text = StoreNo + "";
        }
        #endregion

        #region Textbox 按下Enter觸發事件
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
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
                        break;
                    case 4:
                        txtRemarkP1.Focus();
                        break;
                    case 5:
                        if (MessageBox.Show("確認入庫資料是否正確!!", "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            btnIn.PerformClick();
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 6:
                        txtNubP2.Focus();
                        break;
                    case 7:
                        txtAntP2.Focus();
                        break;
                    case 8:
                        break;
                    case 9:
                        txtLocationP2.Focus();
                        break;
                    case 10:
                        if (MessageBox.Show("確認入庫資料是否正確!!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            btnIn.PerformClick();
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case 11:
                        if (txtItemNbP3.Text.Length == 0)
                        { }
                        else if (txtItemNbP3.Text.Substring(0, 1).ToUpper() == "P")
                        {
                            txtItemNbP3.Text = txtItemNbP3.Text.Substring(1);
                        }
                        txtLocationP3.Focus();
                        break;
                    case 12:
                        if (txtLocationP3.Text.Length == 0)
                        { }
                        else if (txtLocationP3.Text.Substring(0, 1).ToUpper() == "V")
                        {
                            txtLocationP3.Text = txtLocationP3.Text.Substring(1);
                        }
                        txtAntP3.Focus();
                        break;
                    case 13:
                        //數量Enter改跳批號(Data Code)
                        txtOvP3.Focus();
                        break;
                    case 14:
                        txtOvP3.Focus();
                        break;
                    case 15:
                        txtVN.Focus();
                        break;
                    case 16:
                        txtVN.Focus();
                        break;
                    case 17:
                        break;
                    case 18:
                        txtLotcode.Focus();
                        break;
                    case 19:
                        txtRemarkP3.Focus();
                        break;
                    case 20:
                        if (txtRemarkP3.Text.Length < 2)
                        { }
                        else if (txtRemarkP3.Text.Substring(0, 2).ToUpper() == "1T")
                        {
                            txtRemarkP3.Text = txtRemarkP3.Text.Substring(2);
                        }
                        txtRemarksP3.Focus();
                        break;
                    case 21:
                        if (txtRemarksP3.Text.Length < 2)
                        { }
                        else if (txtRemarksP3.Text.Substring(0, 2).ToUpper() == "1P")
                        {
                            txtRemarksP3.Text = txtRemarksP3.Text.Substring(2);
                        }
                        cbDataP3.Focus();
                        break;
                    case 22:
                        break;
                    case 23:
                        cbDataP3.Focus();
                        break;
                    case 24:
                        break;
                }
            }
        }
        #endregion

        private void txtUPCP2_Leave(object sender, EventArgs e)
        {
            string Sqlstr = "";

            if (KXMSSysPara.Sys.WareHouse == 2)
            {
                if (txtUPCP2.Tag.ToString() == "1")
                {
                    Mno1 = txtUPCP2.Text;                   //UPC code
                    Sqlstr = _SqlData.GetData("其他", 24);
                    Sqlstr = Sqlstr.Replace("?1", Mno1);
                    DataTable DT = ConnectQuery(Sqlstr);

                    if (DT.Rows.Count == 0)
                    { }
                    else
                    {
                        txtNubP2.Text = (DT.Rows[0][0].ToString()).ToUpper();
                    }
                }
            }
        }

        #region 讓groupBox邊框去除
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.groupBox4.BackColor);
        }
        #endregion

        private void txt2D_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //檢查長度是否符合2D輸入的長度
                string vInput2D = txt2D.Text.Trim();
                if (vInput2D.Length > 62)
                {
                    string vPartNo = vInput2D.Substring(0, 26).Trim();
                    if (vInput2D.Substring(0, 1) == "P")
                    {
                        txtItemNbP3.Text = vPartNo.Substring(1);
                    }
                    else
                    {
                        txtItemNbP3.Text = vPartNo;
                    }
                    txtLocationP3.Text = vInput2D.Substring(26, 20).Trim();
                    int vQty = 0;
                    vQty = Convert.ToInt32(Convert.ToDecimal(vInput2D.Substring(46, 8).Trim()));
                    txtAntP3.Text = vQty+"";
                    txtOvP3.Text = vInput2D.Substring(54, 8).Trim();

                }
                txtRemarkP3.Focus();
                txt2D.Text = "";
            }
        }
        
    }
}
