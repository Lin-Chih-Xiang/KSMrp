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
    public partial class frm_StoreMultiIn : Form
    {
        string vPartNo;         //物料編號
        string vOV;             //批號
        string vLot;            //備註
        int vAnt;               //數量
        string vLocation;       //Location
        string textBox8;        //
        string vLots;           //備註2

        SqlData _SqlData;
        public frm_StoreMultiIn()
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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

        private void frm_StoreMultiIn_Load(object sender, EventArgs e)
        {
            int x = (1200 - this.Size.Width) / 2;
            int y = (650 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            initLVInList();

            if (rbOp1.Checked)
            {
                lbrbOp.Text = FindEmptyStore(23) + "";
            }
            else
            {
                lbrbOp.Text = FindEmptyStore(22) + "";
            }

            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("其他",4);
            DataTable DT = ConnectQuery(Sqlstr);
            cbStoreHouse.Items.Clear();
            foreach (DataRow DR in DT.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR["StoreHouse"].ToString();
                vitem0.Value = DR["id"].ToString();
                cbStoreHouse.Items.Add(vitem0);

                if (DT.Rows.Count == 0)
                { return; }
                else
                {
                    //cbStoreHouse.SelectedIndex = 0;
                    cbStoreHouse.Text = "T3";
                }
            }

            //找出包裝
            Sqlstr = _SqlData.GetData("其他", 14);
            Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
            DataTable DT0 = ConnectQuery(Sqlstr);
            foreach (DataRow DR0 in DT0.Rows)
            {
                string S = DR0["id"].ToString();
                if (S == "23")
                {
                    rbOp1.Text = DR0["PackageDesc"].ToString();
                }
                else if (S == "22")
                {
                    rbOp2.Text = DR0["PackageDesc"].ToString();
                }
            }

            //平置倉所有儲位
            cbDataP3.Items.Clear();
            Sqlstr = _SqlData.GetData("儲位", 10);
            DataTable DT1 = ConnectQuery(Sqlstr);
            foreach (DataRow DR1 in DT1.Rows)
            {
                ComboboxItem lvitem1 = new ComboboxItem();
                lvitem1.Text = DR1["StoreTypeDesc"].ToString();
                lvitem1.Value = DR1["StoreNo"].ToString();
                cbDataP3.Items.Add(lvitem1);
            }
            if (DT1.Rows.Count == 0)
            { return; }
            else
            {
                cbDataP3.SelectedIndex = 0;
            }
        }

        private void frm_StoreMultiIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
        }

        #region Listview設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LVInList.Name, LVInList.GetColWidth());
        }

        private void initLVInList()
        {
            LVInList.Clear();
            LVInList.View = View.Details;
            LVInList.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LVInList.Columns.Add("物料編號", 120, HorizontalAlignment.Center);
            LVInList.Columns.Add("批號", 120, HorizontalAlignment.Center);
            LVInList.Columns.Add("備註", 150, HorizontalAlignment.Center);
            LVInList.Columns.Add("數量", 100, HorizontalAlignment.Right);
            LVInList.Columns.Add("Location", 150, HorizontalAlignment.Center);
            //LVInList.Columns.Add("工單號", 60, HorizontalAlignment.Center);
            //LVInList.Columns.Add("數量", 70, HorizontalAlignment.Center);
            //LVInList.Columns.Add("JDE儲位", 80, HorizontalAlignment.Center);
            //LVInList.Columns.Add("供應商", 88, HorizontalAlignment.Center);
            //LVInList.Columns.Add("儲位", 110, HorizontalAlignment.Center);
            //LVInList.Columns.Add("PO", 120, HorizontalAlignment.Center);
            //LVInList.Columns.Add("備註", 80, HorizontalAlignment.Center);
            //LVInList.Columns.Add("出庫時間", 80, HorizontalAlignment.Center);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVInList.Name);
            LVInList.SetColWidth(vColStr);
        }
        private enum enLVInListColumn : int
        {
            物料編號 = 1,
            批號 = 2,
            備註 = 3,
            數量 = 4,
            Location = 5,
            //工單號 = 6,
            //數量 = 7,
            //JDE儲位 = 8,
            //供應商 = 9,
            //儲位 = 10,
            //PO = 11,
            //出庫時間 = 12,
        }
        #endregion

        #region 包裝捲和盤計算與顯示

        private void rbOp1_CheckedChanged(object sender, EventArgs e)
        {
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
            //    lbrbOp.Text = FindEmptyStore(23) + "";
            }
        }

        private void rbOp2_CheckedChanged(object sender, EventArgs e)
        {
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
             //   lbrbOp.Text = FindEmptyStore(22) + "";
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

        #region 匯入Excel
        private void btnImport_Click(object sender, EventArgs e)
        {
            frm_ExcelImport vfrm = new frm_ExcelImport(frm_ExcelImport.enExcelImportType.批次入庫, this);
            vfrm.ShowDialog();
        }

        public void insertLVInList(DataTable DT, bool clearLV = true)
        {
            if (clearLV) { LVInList.Items.Clear(); }

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();

                int MaxCol = DT.Columns.Count;
                lvitem.SubItems.Add(DR[0].ToString());       // 物料編號 = 0,
                lvitem.SubItems.Add(DR[3].ToString());       // 批號 = 3,
                lvitem.SubItems.Add(DR[4].ToString());       // 備註 = 4,
                lvitem.SubItems.Add(DR[6].ToString());       // 數量 = 6,
                lvitem.SubItems.Add(DR[7].ToString());       // Location = 7,
                lvitem.Checked = true;
                LVInList.Items.Add(lvitem);
            }
            splitContainer1.Panel2.Enabled = true;
        }
        #endregion

        private void btnLVInListClear_Click(object sender, EventArgs e)
        {
            LVInList.Items.Clear();
            splitContainer1.Panel2.Enabled = false;
        }

        private void btnQueue_Click(object sender, EventArgs e)
        {
            string ErrMsg = "";
            int InType = 1;
            string Sqlstr = "";
            int PackageNo = 0;
            string Pcode = "";   //批號
            int i = 0;

            try
            {
                foreach (ListViewItem vitem in LVInList.Items)
                {
                    if (vitem.Checked == false)
                    { }
                    else
                    {
                        vPartNo = vitem.SubItems[(int)enLVInListColumn.物料編號].Text;
                        vOV = vitem.SubItems[(int)enLVInListColumn.批號].Text;
                        vLot = vitem.SubItems[(int)enLVInListColumn.備註].Text;
                        txtAnt.Text = vitem.SubItems[(int)enLVInListColumn.數量].Text; 
                        vLocation = vitem.SubItems[(int)enLVInListColumn.Location].Text;
                        textBox8 = "";
                        vLots = "";

                        if (vOV == "")
                        { txtOV.Text += "?0  " + vOV + ","; }
                        else
                        { txtOV.Text += "?0" + vOV + ","; } 

                        //On Error Resume Next
                        if (!DataCheck())
                        { return; }

                        //判斷批號
                        string[] S;
                        S = txtOV.Text.Split(',');
                        
                        for (i = 0; i <= S.GetUpperBound(0); i++)
                        {
                            if (S[i].Substring(0) == "?0  ")
                            {
                                txtOV.Text = txtOV.Text.Replace("?0", "1");
                                goto Goin;
                            }
                            if (S[i].Substring(2) == vOV)
                            {
                                txtOV.Text = txtOV.Text.Replace("?0", "1");
                                goto Goin;
                            }
                            else if (S[i].Substring(1) != vOV)
                            { continue; }
                            else if (S[i].Substring(1) == vOV)
                            { goto Next; }
                            else if (S[i].Substring(2) != vOV)
                            { continue; }
                        }

                    Goin:
                        //=============================================================
                        if (KXMSSysPara.Sys.WareHouse == 3)
                        {
                            char[] charsToTrim = { '*', ' ', '\'' };
                            //IC倉 批號檢查
                            Pcode = vOV.Trim(charsToTrim).Replace(",", ".");        //批號','轉成'.'

                            int WeekDiff;
                            WeekDiff = PCodeDiffWeek(Pcode);
                            if (WeekDiff == -99)
                            {
                                //非數字格式或長度不足
                                if (MessageBox.Show("批號格式不符(年週)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                //比較年限
                                string PartNoType;
                                PartNoType = vPartNo.Substring(0, 3);
                                if (PartNoType == "116" || PartNoType == "118")
                                {
                                    if (WeekDiff > (52 * 3))
                                    {
                                        if (MessageBox.Show("此物料已超出年限(3年)" + "\r\n" + "確定入庫?", "入庫作業", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                        {
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
                                            throw new Exception();
                                        }
                                    }
                                }
                            }
                        }

                    Next:
                        //入庫的Function(包裝的id、儲位比x、儲位比y、儲位比最大量、庫位、料號、Location(or備註)、數量、工單單號、包裝編號、是否插單(1正常2插單)、先進先出否(1是先進先出,2不是)
                        if (KXMSSysPara.Sys.WareHouse == 3)
                        {
                            char[] charsToTrim = { '*', ' ', '/' };
                            //if (!IsNumeric(txtAntsP3.Text))
                            //{
                            //    txtAntsP3.Text = "1";
                            //}

                            int vAnts = 1;    //入庫次數
                            for (i = 1; i <= vAnts; i++)
                            {
                                //依次數做入庫
                                ErrMsg = ObjInput(0, 0, 0, 0, cbStoreHouse.Text, vPartNo, vLocation.Trim(charsToTrim), int.Parse(txtAnt.Text), textBox8.Trim(charsToTrim), 0, InType, 2, Pcode, vLot.Trim(charsToTrim), vLots.Trim(charsToTrim));
                            }
                        }
                        if (ErrMsg.Length == 0)
                        {
                            //入庫完成
                            LVInList.Items.Remove(vitem);
                        }
                        else
                        {
                            MessageBox.Show("入庫過程中，有部分資料有問題" + "\r\n" + ErrMsg, "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        //ClearData();
                        if (KXMSSysPara.Sys.WareHouse == 3)
                        {
                            //if (rbOp2.Checked == true)
                            //{
                            //    lbrbOp.Text = FindEmptyStore(22) + "";
                            //}
                            //else if (rbOp1.Checked == true)
                            //{
                            //    lbrbOp.Text = FindEmptyStore(23) + "";
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        
        public void ClearData()
        {
            cbStoreHouse.SelectedIndex = 0;
            rbOp1.Checked = true;
            cbDataP3.SelectedIndex = 0;
        }

        public bool DataCheck()
        {
            string Sqlstr = "";
            bool _DataCheck;
            _DataCheck = true;
            char[] charsToTrim = { '*', ' ', '\'' };

            vPartNo.Trim(charsToTrim).ToUpper();        //字串去除符號+小寫轉大寫

            if (vPartNo.Length == 0)
            {
                MessageBox.Show("此無填入物料編號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _DataCheck = false;
                return _DataCheck;
            }

            //檢查數量是否正確
            if (txtAnt.Text.Length == 0)
            {
                MessageBox.Show("此無填入數量!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _DataCheck = false;
                return _DataCheck;
            }
            if (int.Parse(txtAnt.Text) < 1)
            {
                MessageBox.Show("數量填入錯誤!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _DataCheck = false;
                return _DataCheck;
            }

            //比對資料庫
            Sqlstr = _SqlData.GetData("其他", 34);
            Sqlstr = Sqlstr.Replace("?1", vPartNo);
            DataTable DT1 = ConnectQuery(Sqlstr);
            foreach (DataRow DR1 in DT1.Rows)
            {
                string S = "";
                S = DR1["code99"].ToString();
                if (vPartNo != S)
                {
                    if (MessageBox.Show("這料號是第一次輸入，請在確認一次!!", "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    {
                        _DataCheck = false;
                        return _DataCheck;
                    }
                    else
                    {
                        Sqlstr = _SqlData.GetData("其他", 31);
                        Sqlstr = Sqlstr.Replace("?1", vPartNo);
                        Connect(Sqlstr);
                    }
                }
            }

            return _DataCheck;
        }

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

            //(((IC倉)))######################################################################
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //入平置倉
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
                    DateTime DtpIn = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", DtpIn.ToString("yyyy/MM/dd HH:mm:ss"));
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
                    _ObjInput = _ObjInput + "請選擇平置倉儲位!!" + "\r\n";
                }
            }

            KXMSSysPara.Sys.CancelTempStore();
            return _ObjInput;
        }

        private void cbDataP3_SelectedIndexChanged(object sender, EventArgs e)
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

    }
}
