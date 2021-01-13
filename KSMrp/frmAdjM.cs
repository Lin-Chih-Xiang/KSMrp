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
    public partial class frmAdjM : Form
    {
        SqlData _SqlData;
        modCtrl _modCtrl;
        string StoreHouse;
        string WorkOrderNo;
        long Qty;
        string Location;
        long StoreNo;
        string Mno;
        long Mid;
        string Ov;
        public frmAdjM()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
            _modCtrl = new modCtrl();
        }

        #region WareHouse顯示設定
        private void visable(int i)
        {
            if (i < 3)
            {
                this.Width = 956;
                this.Height = 538;
            }
            else
            {
                this.Width = 956;
                this.Height = 558;
            }

            switch (i)
            {
                case 1:
                    tc1.Visible = false;
                    tc2.Location = new System.Drawing.Point(12, 12);
                    label37.Visible = false;
                    label38.Visible = false;
                    txtLocationP2.Visible = false;
                    cbPackageNo.Visible = false;
                    label26.Visible = false;
                    label28.Visible = false;
                    cbStoreHouseP31.Visible = false;
                    cbStoreHouse.Visible = false;
                    break;
                case 2:
                    tc1.Visible = false;
                    tc2.Location = new System.Drawing.Point(12, 12);
                    label26.Visible = false;
                    label28.Visible = false;
                    cbStoreHouseP31.Visible = false;
                    cbStoreHouse.Visible = false;
                    LoadPackage();
                    break;
                case 3:
                    tc2.Visible = false;

                    tc1.Size = new System.Drawing.Size(915, 496);
                    break;
            }
        }
        
        private void tc1_Click(object sender, EventArgs e)
        {
            if (tc1.SelectedTab == tpAuto)
            {
                this.Width = 956;
                this.Height = 558;
                tc1.Size = new System.Drawing.Size(915, 496);
            }
            else if (tc1.SelectedTab == tpparity)
            {
                this.Width = 956;
                this.Height = 568;
                tc1.Size = new System.Drawing.Size(915, 504);
            }
            else if (tc1.SelectedTab == tprollover)
            {
                this.Width = 1063;
                this.Height = 703;
                tc1.Size = new System.Drawing.Size(1022, 640);
            }
        }
        #endregion

        private void LoadLV()
        {
            string Sqlstr = "";
            cbDataP31.Items.Clear();
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //自動倉===================================================
                //找有幾台機器
                Sqlstr = _SqlData.GetData("儲位", 83);
                DataTable DT = ConnectQuery(Sqlstr);
                foreach (DataRow DR in DT.Rows)
                {
                    ComboboxItem vitem0 = new ComboboxItem();
                    vitem0.Text = DR["MachineNo"].ToString();
                    cbDataP30.Items.Add(vitem0);
                    cbMachine1.Items.Add(vitem0);
                    cbMachine2.Items.Add(vitem0);
                }
                if (cbDataP30.Items.Count == 0 || cbMachine1.Items.Count == 0 || cbMachine2.Items.Count == 0)
                {
                    return;
                }
                else
                {
                    cbDataP30.SelectedIndex = 0;
                    cbMachine1.SelectedIndex = 0;
                    cbMachine2.SelectedIndex = 0;
                }

                //平置倉=====================================================
                Sqlstr = _SqlData.GetData("儲位", 10);
                DataTable DTs = ConnectQuery(Sqlstr);
                foreach (DataRow DRs in DTs.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DRs["StoreTypeDesc"].ToString());
                    lvitem.SubItems.Add(DRs["StoreNo"].ToString());
                    LVP3.Items.Add(lvitem);
                    txtStoreNo.Text = DRs["StoreNo"].ToString();

                    ComboboxItem vitem0 = new ComboboxItem();
                    vitem0.Text = DRs["StoreTypeDesc"].ToString();
                    vitem0.Value = DRs["StoreNo"].ToString();
                    cbTurn1.Items.Add(vitem0);
                    cbTurn2.Items.Add(vitem0);
                    txtStoreNo1.Text = DRs["StoreNo"].ToString();
                    txtStoreNo4.Text = DRs["StoreNo"].ToString();
                }

                if (cbTurn1.Items.Count == 0 || cbTurn2.Items.Count == 0)
                {
                    return;
                }
                else
                {
                    cbTurn1.SelectedIndex = 0;
                    cbTurn2.SelectedIndex = 0;
                }

                Sqlstr = _SqlData.GetData("其他",4);
                DataTable DT0 = ConnectQuery(Sqlstr);
                foreach (DataRow DR0 in DT0.Rows)
                {
                    ComboboxItem vitem0 = new ComboboxItem();
                    vitem0.Text = DR0["StoreHouse"].ToString();
                    vitem0.Value = DR0["id"].ToString();
                    cbStoreHouse.Items.Add(vitem0);
                    cbStoreHouseP31.Items.Add(vitem0);
                }
            }
            else
            {
                //自動倉===================================================
                //找有幾台機器
                Sqlstr = _SqlData.GetData("儲位", 83);
                DataTable DT = ConnectQuery(Sqlstr);
                foreach (DataRow DR in DT.Rows)
                {
                    ComboboxItem vitem0 = new ComboboxItem();
                    vitem0.Text = DR["MachineNo"].ToString();
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

        }

        private void frmAdjM_Load(object sender, EventArgs e)
        {
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                visable(1);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                visable(2);
            }
            else
            {
                visable(3);
                label33.Text = "轉" + "\r\n" + "倉";
                label35.Text = "轉" + "\r\n" + "倉";
            }

            initLV();
            //this.Size = new System.Drawing.Size(956, 537);
            LoadLV();
        }

        private void frmAdjM_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
            //this.Hide();          //隱藏視窗
            //e.Cancel = true;      //取消關閉作業
        }

        #region 一般SQL連線 及 All SQL
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
            {
                MessageBox.Show(ex.Message);
            }
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

        #region 辨別是否為數字及英文字母
        public bool IsNumeric(string Value)
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

        #region ListView 設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LVP1.Name, LVP1.GetColWidth());
        }
        private void initLV()
        {
            LVP1.Items.Clear();
            LVP2.Items.Clear();
            LVP3.Items.Clear();
            LVP4.Items.Clear();
            LV1.Items.Clear();
            LV2.Items.Clear();
            LV3.Items.Clear();
            LV4.Items.Clear();
            LV5.Items.Clear();
            LV6.Items.Clear();
            LVP5.Items.Clear();
            LVP6.Items.Clear();
           
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //自動倉
                LVP1.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP1.Columns.Add("層", 55, HorizontalAlignment.Center);
                LVP1.Columns.Add("X燈號", 60, HorizontalAlignment.Center);
                LVP1.Columns.Add("Y燈號", 60, HorizontalAlignment.Center);

                LVP2.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP2.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LVP2.Columns.Add("料號", 100, HorizontalAlignment.Left);
                LVP2.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LVP2.Columns.Add("Location", 100, HorizontalAlignment.Center);
                LVP2.Columns.Add("批號", 100, HorizontalAlignment.Center);
                LVP2.Columns.Add("廠務編號", 80, HorizontalAlignment.Center);
                LVP2.Columns.Add("Lot Code", 80, HorizontalAlignment.Center);
                LVP2.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LVP2.Columns.Add("備註2", 100, HorizontalAlignment.Center);

                //平置倉
                LVP3.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP3.Columns.Add("棧板說明", 195, HorizontalAlignment.Center);

                LVP4.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP4.Columns.Add("料號", 100, HorizontalAlignment.Left);
                LVP4.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LVP4.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LVP4.Columns.Add("Location", 80, HorizontalAlignment.Center);
                LVP4.Columns.Add("批號", 80, HorizontalAlignment.Center);
                LVP4.Columns.Add("廠務編號", 80, HorizontalAlignment.Center);
                LVP4.Columns.Add("Lot Code", 80, HorizontalAlignment.Center);
                LVP4.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LVP4.Columns.Add("備註2", 100, HorizontalAlignment.Center);
                LVP4.Columns.Add("StoreNo", 60, HorizontalAlignment.Center);
                LVP4.Columns.Add("WorkOrderNo", 100, HorizontalAlignment.Center);

                //轉倉作業==============================================================
                //自動倉
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV1.Columns.Add("層", 55, HorizontalAlignment.Center);
                LV1.Columns.Add("X燈號", 60, HorizontalAlignment.Center);
                LV1.Columns.Add("Y燈號", 60, HorizontalAlignment.Center);

                LV2.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV2.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LV2.Columns.Add("料號", 100, HorizontalAlignment.Left);
                LV2.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV2.Columns.Add("Location", 100, HorizontalAlignment.Center);
                LV2.Columns.Add("批號", 100, HorizontalAlignment.Center);
                LV2.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LV2.Columns.Add("備註2", 100, HorizontalAlignment.Center);

                LV3.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV3.Columns.Add("層", 55, HorizontalAlignment.Center);
                LV3.Columns.Add("X燈號", 60, HorizontalAlignment.Center);
                LV3.Columns.Add("Y燈號", 60, HorizontalAlignment.Center);

                LV4.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV4.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LV4.Columns.Add("料號", 100, HorizontalAlignment.Left);
                LV4.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV4.Columns.Add("Location", 100, HorizontalAlignment.Center);
                LV4.Columns.Add("批號", 100, HorizontalAlignment.Center);
                LV4.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LV4.Columns.Add("備註2", 100, HorizontalAlignment.Center);

                //平置倉
                LV5.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV5.Columns.Add("料號", 100, HorizontalAlignment.Left);
                LV5.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LV5.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV5.Columns.Add("Location", 80, HorizontalAlignment.Center);
                LV5.Columns.Add("批號", 80, HorizontalAlignment.Center);
                LV5.Columns.Add("廠務編號", 80, HorizontalAlignment.Center);
                LV5.Columns.Add("LotCode", 80, HorizontalAlignment.Center);
                LV5.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LV5.Columns.Add("備註2", 100, HorizontalAlignment.Center);
                LV5.Columns.Add("StoreNo", 60, HorizontalAlignment.Center);
                LV5.Columns.Add("WorkOrderNo", 100, HorizontalAlignment.Center);

                LV6.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV6.Columns.Add("料號", 100, HorizontalAlignment.Left);
                LV6.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LV6.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV6.Columns.Add("Location", 80, HorizontalAlignment.Center);
                LV6.Columns.Add("批號", 80, HorizontalAlignment.Center);
                LV6.Columns.Add("廠務編號", 80, HorizontalAlignment.Center);
                LV6.Columns.Add("LotCode", 80, HorizontalAlignment.Center);
                LV6.Columns.Add("備註", 100, HorizontalAlignment.Center);
                LV6.Columns.Add("備註2", 100, HorizontalAlignment.Center);
                LV6.Columns.Add("StoreNo", 60, HorizontalAlignment.Center);
                LV6.Columns.Add("WorkOrderNo", 100, HorizontalAlignment.Center);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                //自動倉
                LVP5.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP5.Columns.Add("層", 55, HorizontalAlignment.Center);
                LVP5.Columns.Add("X燈號", 60, HorizontalAlignment.Center);
                LVP5.Columns.Add("Y燈號", 60, HorizontalAlignment.Center);

                LVP6.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP6.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LVP6.Columns.Add("UPC", 140, HorizontalAlignment.Left);
                LVP6.Columns.Add("成品料號", 140, HorizontalAlignment.Left);
                LVP6.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LVP6.Columns.Add("包裝", 80, HorizontalAlignment.Center);
                LVP6.Columns.Add("Location", 80, HorizontalAlignment.Center);
            }
            else
            {
                //自動倉
                LVP5.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP5.Columns.Add("層", 55, HorizontalAlignment.Center);
                LVP5.Columns.Add("X燈號", 60, HorizontalAlignment.Center);
                LVP5.Columns.Add("Y燈號", 60, HorizontalAlignment.Center);

                LVP6.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LVP6.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LVP6.Columns.Add("工單單號", 140, HorizontalAlignment.Center);
                LVP6.Columns.Add("料號", 140, HorizontalAlignment.Left);
                LVP6.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LVP6.Columns.Add("包裝", 140, HorizontalAlignment.Center);
                LVP6.Columns.Add("備註", 120, HorizontalAlignment.Center);
            }
            //設定LV 寬度
            string vColStr1 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVP1.Name);
            LVP1.SetColWidth(vColStr1);
            string vColStr2 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVP2.Name);
            LVP2.SetColWidth(vColStr2);
            string vColStr3 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVP3.Name);
            LVP3.SetColWidth(vColStr3);
            string vColStr4 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVP4.Name);
            LVP4.SetColWidth(vColStr4);
            string vColStr5 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVP5.Name);
            LVP5.SetColWidth(vColStr5);
            string vColStr6 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LVP6.Name);
            LVP6.SetColWidth(vColStr6);
            string vColStr7 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr7);
            string vColStr8 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV2.Name);
            LV2.SetColWidth(vColStr8);
            string vColStr9 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV3.Name);
            LV3.SetColWidth(vColStr9);
            string vColStr10 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV4.Name);
            LV4.SetColWidth(vColStr10);
            string vColStr11 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV5.Name);
            LV5.SetColWidth(vColStr11);
            string vColStr12 = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV6.Name);
            LV6.SetColWidth(vColStr12);
        }
        private enum enLV1ColumnLVP1 : int
        {
            層 = 1,
            X燈號 = 2,
            Y燈號 = 3,
        }
        private enum enLV1ColumnLVP2 : int
        {
            庫位 = 1,
            料號 = 2,
            數量 = 3,
            Location = 4,
            批號 = 5,
            廠務編號 = 6,
            LotCode = 7, 
            備註 = 8,
            備註2 = 9,
        }

        private enum enLV1ColumnLVP3 : int
        {
            棧板說明 = 1,
        }
        private enum enLV1ColumnLVP4 : int
        {
            料號 = 1,
            庫位 = 2,
            數量 = 3,
            Location = 4,
            批號 = 5,
            廠務編號 = 6,
            LotCode = 7,
            備註 = 8,
            備註2 = 9,
            StoreNo = 10,
            WorkOrderNo = 11,
        }

        private enum enLV1ColumnLV1 : int
        {
            層 = 1,
            X燈號 = 2,
            Y燈號 = 3,
        }
        private enum enLV1ColumnLV2 : int
        {
            庫位 = 1,
            料號 = 2,
            數量 = 3,
            Location = 4,
            批號 = 5,
            備註 = 6,
            備註2 = 7,
        }

        private enum enLV1ColumnLV3 : int
        {
            層 = 1,
            X燈號 = 2,
            Y燈號 = 3,
        }
        private enum enLV1ColumnLV4 : int
        {
            庫位 = 1,
            料號 = 2,
            數量 = 3,
            Location = 4,
            批號 = 5,
            備註 = 6,
            備註2 = 7,
        }

        private enum enLV1ColumnLV5 : int
        {
            料號 = 1,
            庫位 = 2,
            數量 = 3,
            Location = 4,
            批號 = 5,
            廠務編號 = 6,
            Lotcode = 7,
            備註 = 8,
            備註2 = 9,
            StoreNo = 10,
            WorkOrderNo = 11,
        }
        private enum enLV1ColumnLV6 : int
        {
            料號 = 1,
            庫位 = 2,
            數量 = 3,
            Location = 4,
            批號 = 5,
            廠務編號 = 6,
            Lotcode = 7,
            備註 = 8,
            備註2 = 9,
            StoreNo = 10,
            WorkOrderNo = 11,
        }

        private enum enLV1ColumnLVP5 : int
        {
            層 = 1,
            X燈號 = 2,
            Y燈號 = 3,
        }
        private enum enLV1ColumnLVP6 : int
        {
            層 = 1,
            UPC = 2,
            成品料號 = 3,
            數量 = 4,
            包裝 = 5,
            Location = 6,
        }
        private enum enLV1ColumnLVP51 : int
        {
            層 = 1,
            X燈號 = 2,
            Y燈號 = 3,
        }
        private enum enLV1ColumnLVP61 : int
        {
            庫位 = 1,
            工單單號 = 2,
            料號 = 3,
            數量 = 4,
            包裝 = 5,
            備註 = 6,
        }

        #endregion

        #region LVP 12、34、56 & LV 12、34、56 內容設定
        private void LVP12()
        {
            LVP2.Items.Clear();
            string Sqlstr = "";
            int Machine =0;
            int Carry =0;
            int x =0; 
            int y =0;

            Machine = int.Parse(cbDataP30.Text);
            if (cbDataP31.Text == "")
            {
                Carry = int.Parse(txtTray.Text);
            }
            else
            {
                Carry = int.Parse(cbDataP31.Text);
            }
            x = int.Parse(txtPos.Text);
            y = int.Parse(txtDepth.Text);

            if (Machine == 0 || Carry == 0 || x == 0 || y == 0) { return; }

            Sqlstr = _SqlData.GetData("儲位", 58);
            Sqlstr = Sqlstr.Replace("?1", Machine + "");
            Sqlstr = Sqlstr.Replace("?2", Carry + "");
            Sqlstr = Sqlstr.Replace("?3", x + "");
            Sqlstr = Sqlstr.Replace("?4", (y-1) + "");
            //SqlStr = Replace(SqlStr, "?5", SysPara.WareHouse)
            DataTable DT = ConnectQuery(Sqlstr);
            if (DT.Rows.Count == 0)
            {
                txtAntP31.Text = "";
                txtLocationP31.Text = "";
                txtOvP31.Text = "";
                txtRemarkP31.Text = "";
                txtRemarksP31.Text = "";
            }
            else
            {
                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                    lvitem.SubItems.Add(DR["Mno"].ToString());
                    lvitem.SubItems.Add(DR["MQty"].ToString());
                    lvitem.SubItems.Add(DR["Location"].ToString());
                    lvitem.SubItems.Add(DR["OV"].ToString());
                    lvitem.SubItems.Add(DR["VanderName"].ToString());
                    lvitem.SubItems.Add(DR["LotCode"].ToString());
                    lvitem.SubItems.Add(DR["Mdesc"].ToString());
                    lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                    LVP2.Items.Add(lvitem);

                    StoreHouse = DR["StoreHouse"].ToString();
                    WorkOrderNo = DR["WorkOrderNo"].ToString();
                    Mno = DR["Mno"].ToString();
                    Ov = DR["OV"].ToString();
                    Qty = int.Parse(DR["MQty"].ToString());
                    Location = DR["Location"].ToString();
                    StoreNo = int.Parse(DR["StoreNo"].ToString());
                    Mid = int.Parse(DR["Mid"].ToString());
                } 
            }
        }

        private void LVP34()
        {
            LVP4.Items.Clear();
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("儲位", 71);
            Sqlstr = Sqlstr.Replace("?1", txtStoreNo.Text);
            Sqlstr = Sqlstr + " Order By Mno ";
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["VanderName"].ToString());
                lvitem.SubItems.Add(DR["LotCode"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                lvitem.SubItems.Add(DR["WorkOrderNo"].ToString());
                lvitem.SubItems.Add(DR["Mid"].ToString());
                LVP4.Items.Add(lvitem);            
            }
        }

        private void LVP56()
        {
            LVP6.Items.Clear();
            string Sqlstr = "";
            int Machine = 0;
            int Carry = 0;
            int x = 0;
            int y = 0;

            Machine = int.Parse(cbDataP0.Text);
            if (cbDataP1.Text == "")
            {
                Carry = int.Parse(txtTray1.Text);
            }
            else
            {
                Carry = int.Parse(cbDataP1.Text);
            }
            x = int.Parse(txtPos1.Text);
            y = int.Parse(txtDepth1.Text);

            if (Machine == 0 || Carry == 0 || x == 0 || y == 0) { return; }

            Sqlstr = _SqlData.GetData("儲位", 52);
            Sqlstr = Sqlstr.Replace("?1", Machine + "");
            Sqlstr = Sqlstr.Replace("?2", Carry + "");
            Sqlstr = Sqlstr.Replace("?3", x + "");
            Sqlstr = Sqlstr.Replace("?4", (y-1) + "");
            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.WareHouse + "");
            DataTable DT = ConnectQuery(Sqlstr);
            if (DT.Rows.Count == 0)
            {
                txtAntP1.Text = "";
                if (KXMSSysPara.Sys.WareHouse == 2)
                {
                    txtLocationP2.Text = "";
                    cbPackageNo.Text = KXMSSysPara.Sys.DefaultPackage;
                }
            }
            else
            {
                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    if (KXMSSysPara.Sys.WareHouse == 1)
                    {
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                        lvitem.SubItems.Add(DR["WorkOrderNo"].ToString());
                        lvitem.SubItems.Add(DR["Mno"].ToString());
                        lvitem.SubItems.Add(DR["MQty"].ToString());
                        lvitem.SubItems.Add(DR["PackageDesc"].ToString());
                        lvitem.SubItems.Add(DR["Mdesc"].ToString());
                    }
                    else if (KXMSSysPara.Sys.WareHouse == 2)
                    {
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                        lvitem.SubItems.Add(DR["Mno"].ToString());
                        lvitem.SubItems.Add(DR["FinishNo"].ToString());
                        lvitem.SubItems.Add(DR["MQty"].ToString());
                        lvitem.SubItems.Add(DR["PackageDesc"].ToString());
                        lvitem.SubItems.Add(DR["Location"].ToString());
                        lvitem.SubItems.Add(DR["id"].ToString());

                        txtPackageId2.Text = DR["id"].ToString();
                    }
                    LVP6.Items.Add(lvitem);

                    Mno = DR["Mno"].ToString();
                    StoreHouse = DR["StoreHouse"].ToString();
                    WorkOrderNo = DR["WorkOrderNo"].ToString();
                    Qty = int.Parse(DR["MQty"].ToString());
                    Location = DR["Location"].ToString();
                    StoreNo = int.Parse(DR["StoreNo"].ToString());
                    Mid = int.Parse(DR["Mid"].ToString());
                }
            }
        }
        private void LV12()
        {
            LV2.Items.Clear();
            string Sqlstr = "";
            int Machine = 0;
            int Carry = 0;
            int x = 0;
            int y = 0;

            Machine = int.Parse(cbMachine1.Text);
            if (cbTray1.Text == "")
            {
                Carry = int.Parse(txtTray2.Text);
            }
            else
            {
                Carry = int.Parse(cbTray1.Text);
            }
            x = int.Parse(txtPos2.Text);
            y = int.Parse(txtDepth2.Text);
            
            if (Machine == 0 || Carry == 0 || x == 0 || y == 0) { return; }

            Sqlstr = _SqlData.GetData("儲位", 57);
            Sqlstr = Sqlstr.Replace("?1", Machine + "");
            Sqlstr = Sqlstr.Replace("?2", Carry + "");
            Sqlstr = Sqlstr.Replace("?3", x + "");
            Sqlstr = Sqlstr.Replace("?4", (y - 1) + "");
            //SqlStr = Replace(SqlStr, "?5", SysPara.WareHouse)
            DataTable DT = ConnectQuery(Sqlstr);
            
            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["storemid"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                LV2.Items.Add(lvitem);
            }
        }

        private void LV34()
        {
            LV4.Items.Clear();
            string Sqlstr = "";
            int Machine = 0;
            int Carry = 0;
            int x = 0;
            int y = 0;

            Machine = int.Parse(cbMachine2.Text);
            if (cbTray2.Text == "")
            {
                Carry = int.Parse(txtTray3.Text);
            }
            else
            {
                Carry = int.Parse(cbTray2.Text);
            }
            x = int.Parse(txtPos3.Text);
            y = int.Parse(txtDepth3.Text);

            if (Machine == 0 || Carry == 0 || x == 0 || y == 0) { return; }

            Sqlstr = _SqlData.GetData("儲位", 57);
            Sqlstr = Sqlstr.Replace("?1", Machine + "");
            Sqlstr = Sqlstr.Replace("?2", Carry + "");
            Sqlstr = Sqlstr.Replace("?3", x + "");
            Sqlstr = Sqlstr.Replace("?4", (y - 1) + "");
            //SqlStr = Replace(SqlStr, "?5", SysPara.WareHouse)
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["storemid"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                LV4.Items.Add(lvitem);

                txtMno.Text = DR["Mno"].ToString();
            }
        }

        private void LV56()
        {
            int StoreNo;
            if (cbTurn1.SelectedItem == null)
            {
                StoreNo = 0;
            }
            else
            {
                int.TryParse((cbTurn1.SelectedItem as ComboboxItem).Value, out StoreNo);
            }
            txtStoreNo1.Text = StoreNo + "";

            LV5.Items.Clear();
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("儲位", 71);
            Sqlstr = Sqlstr.Replace("?1", txtStoreNo1.Text);
            Sqlstr = Sqlstr + " Order By Mno ";
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["VanderName"].ToString());
                lvitem.SubItems.Add(DR["LotCode"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                lvitem.SubItems.Add(DR["WorkOrderNo"].ToString());
                lvitem.SubItems.Add(DR["Mid"].ToString());
                lvitem.SubItems.Add(DR["storemid"].ToString());
                LV5.Items.Add(lvitem);
            }
        }

        private void LV561()
        {
            int StoreNo;
            if (cbTurn2.SelectedItem == null)
            {
                StoreNo = 0;
            }
            else
            {
                int.TryParse((cbTurn2.SelectedItem as ComboboxItem).Value, out StoreNo);
            }
            txtStoreNo4.Text = StoreNo + "";

            LV6.Items.Clear();
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("儲位", 71);
            Sqlstr = Sqlstr.Replace("?1", txtStoreNo4.Text);
            Sqlstr = Sqlstr + " Order By Mno ";
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["VanderName"].ToString());
                lvitem.SubItems.Add(DR["LotCode"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                lvitem.SubItems.Add(DR["WorkOrderNo"].ToString());
                lvitem.SubItems.Add(DR["Mid"].ToString());
                lvitem.SubItems.Add(DR["storemid"].ToString());
                LV6.Items.Add(lvitem);
            }
        }
        #endregion

        #region 查詢包裝(全部)
        private void LoadPackage()
        {
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("其他", 14);
            Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse +"");
            DataTable DT  =ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR["PackageDesc"].ToString();
                vitem0.Value = DR["id"].ToString();
                cbPackageNo.Items.Add(vitem0);
            }
            cbPackageNo.Text = KXMSSysPara.Sys.DefaultPackage;
        }
        #endregion

        #region (修改)Button
        private void btnModify_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            if (MessageBox.Show("確定要修改資訊?", "KSMrp", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.Cancel) { return; }
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (tc1.SelectedTab == tpAuto)
                {
                    if (!IsNumeric(txtAntP31.Text))
                    {
                        MessageBox.Show("請輸入正確修改數量!!", "KSMrp", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else if (tc1.SelectedTab == tpparity)
                {
                    if (!IsNumeric(txtAntP32.Text))
                    {
                        MessageBox.Show("請輸入正確修改數量!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                if (!IsNumeric(txtAntP1.Text))
                {
                    MessageBox.Show("請輸入正確修改數量!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                if (!IsNumeric(txtAntP1.Text) || Qty == int.Parse(txtAntP1.Text))
                {
                    MessageBox.Show("請輸入正確修改數量!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Sqlstr = _SqlData.GetData("儲位", 12);
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (tc1.SelectedTab == tpAuto)
                {
                    Sqlstr = Sqlstr.Replace("?1", txtAntP31.Text);
                }
                else if (tc1.SelectedTab == tpparity)
                {
                    Sqlstr = Sqlstr.Replace("?1", txtAntP32.Text);
                }
            }
            else
            {
                Sqlstr = Sqlstr.Replace("?1", txtAntP1.Text);
            }
            Sqlstr = Sqlstr.Replace("?2", StoreNo +"");
            Sqlstr = Sqlstr.Replace("?3", Mid +"");
            Connect(Sqlstr);

            //2004/11/29修改，為了給ic倉可以改location，ov，備註 2018/11/18 增加備註2====================
            //2020/3/20 修改，給IC倉 增加庫位============================================================
            //2020/5/12 修改，IC倉新增廠務編號、LotCode
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (tc1.SelectedTab == tpAuto)
                {
                    Sqlstr = _SqlData.GetData("儲位", 19);
                    Sqlstr = Sqlstr.Replace("?1", txtLocationP31.Text);
                    Sqlstr = Sqlstr.Replace("?2", txtOvP31.Text);
                    Sqlstr = Sqlstr.Replace("?3", txtRemarkP31.Text);
                    Sqlstr = Sqlstr.Replace("?4", txtRemarksP31.Text);
                    Sqlstr = Sqlstr.Replace("?5", Mid + "");
                    Sqlstr = Sqlstr.Replace("?6", cbStoreHouseP31.Text);
                    Sqlstr = Sqlstr.Replace("?7", txtVNP31.Text);
                    Sqlstr = Sqlstr.Replace("?8", txtLotCodeP31.Text);
                }
                else if (tc1.SelectedTab == tpparity)
                {
                    Sqlstr = _SqlData.GetData("儲位", 19);
                    Sqlstr = Sqlstr.Replace("?1", txtLocationP32.Text);
                    Sqlstr = Sqlstr.Replace("?2", txtOvP32.Text);
                    Sqlstr = Sqlstr.Replace("?3", txtRemarkP32.Text);
                    Sqlstr = Sqlstr.Replace("?4", txtRemarksP32.Text);
                    Sqlstr = Sqlstr.Replace("?5", Mid + "");
                    Sqlstr = Sqlstr.Replace("?6", cbStoreHouse.Text);
                    Sqlstr = Sqlstr.Replace("?7", txtVNP32.Text);
                    Sqlstr = Sqlstr.Replace("?8", txtLotCodeP32.Text);
                }
                Connect(Sqlstr);
            }
            //2019/3/7 修改，給成品倉改location、包裝====================================================
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                int PackageNo;
                Sqlstr = _SqlData.GetData("儲位", 20);
                Sqlstr = Sqlstr.Replace("?1", txtLocationP2.Text);     //Location
                Sqlstr = Sqlstr.Replace("?5", Mid +"");
                if (txtPackageId2.Text == txtPackageId1.Text)
                {
                    //維持原本的值
                    PackageNo = int.Parse(txtPackageId1.Text);
                    Sqlstr = Sqlstr.Replace("?2", PackageNo +"");
                }
                else
                {
                    PackageNo = int.Parse(txtPackageId1.Text);
                    Sqlstr = Sqlstr.Replace("?2", PackageNo + "");
                }
                Connect(Sqlstr);
            }

            //===========================================================================================
            //2004/11/29修改，如果沒修改數量就不寫入異動檔
            if (KXMSSysPara.Sys.WareHouse != 3)
            {
                txtAntP31.Text = Qty +"";
                txtAntP32.Text = Qty +"";
            }
            else if (KXMSSysPara.Sys.WareHouse != 1 || KXMSSysPara.Sys.WareHouse != 2)
            {
                txtAntP1.Text = Qty +"";
                if (tc1.SelectedTab == tpAuto)
                {
                    txtAntP32.Text = Qty + "";
                }
                else if (tc1.SelectedTab == tpparity)
                {
                    txtAntP31.Text = Qty + "";
                }
            }
            if (int.Parse(txtAntP1.Text) - Qty != 0 || int.Parse(txtAntP31.Text) - Qty != 0  || int.Parse(txtAntP32.Text) - Qty != 0)
            {
                //TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID
                Sqlstr = _SqlData.GetData("異動", 7);
                Sqlstr = Sqlstr.Replace("?1", WorkOrderNo);             //工單單號
                Sqlstr = Sqlstr.Replace("?2", Mno.ToUpper());
                Sqlstr = Sqlstr.Replace("?3", StoreNo +"");
                DateTime dpth = DateTime.Now;
                Sqlstr = Sqlstr.Replace("?4", dpth.ToString("yyyy/MM/dd HH:mm:ss"));
                Sqlstr = Sqlstr.Replace("?5",KXMSSysPara.Sys.AutoID +"");
                Sqlstr = Sqlstr.Replace("?6", 2 +"");
                if (KXMSSysPara.Sys.WareHouse == 3)
                {
                    if (tc1.SelectedTab == tpAuto)
                    {
                        Sqlstr = Sqlstr.Replace("?7", int.Parse(txtAntP31.Text) - Qty + "");
                    }
                    else if (tc1.SelectedTab == tpparity)
                    {
                        Sqlstr = Sqlstr.Replace("?7", int.Parse(txtAntP32.Text) - Qty + "");
                    }
                }
                else
                {
                    Sqlstr = Sqlstr.Replace("?7", int.Parse(txtAntP1.Text) - Qty + "");
                }
                Sqlstr = Sqlstr.Replace("?8", "");
                if (KXMSSysPara.Sys.WareHouse == 3)
                {
                    if (tc1.SelectedTab == tpAuto)
                    {
                        Sqlstr = Sqlstr.Replace("?9", cbDataP30.Text);
                    }
                    else 
                    {
                        Sqlstr = Sqlstr.Replace("?9", 0 + "");
                    }
                }
                else
                {
                    Sqlstr = Sqlstr.Replace("?9", cbDataP0.Text);
                }
                
                Sqlstr = Sqlstr.Replace("?01", 0 +"");
                Sqlstr = Sqlstr.Replace("?02", Location);
                Sqlstr = Sqlstr.Replace("?03", StoreHouse);             //庫位
                Sqlstr = Sqlstr.Replace("?04", Ov);
                if (tc1.SelectedTab == tpAuto)
                {
                    Sqlstr = Sqlstr.Replace("?05", txtRemarkP31.Text);
                    Sqlstr = Sqlstr.Replace("?06", txtRemarksP31.Text);
                    Sqlstr = Sqlstr.Replace("?07", txtVNP31.Text);
                    Sqlstr = Sqlstr.Replace("?08", txtLotCodeP31.Text);
                }
                else if (tc1.SelectedTab == tpparity)
                {
                    Sqlstr = Sqlstr.Replace("?05", txtRemarkP32.Text);
                    Sqlstr = Sqlstr.Replace("?06", txtRemarksP32.Text);
                    Sqlstr = Sqlstr.Replace("?07", txtVNP32.Text);
                    Sqlstr = Sqlstr.Replace("?08", txtLotCodeP32.Text);
                }
                Connect(Sqlstr);
            }

            KXMSSysPara.Sys.CheckEmptyStore();

            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (tc1.SelectedTab == tpAuto)
                {
                    LVP12();
                    txtAntP31.Text = "";
                    txtLocationP31.Text = "";
                    txtOvP31.Text = "";
                    txtRemarkP31.Text = "";
                    txtRemarksP31.Text = "";
                    cbStoreHouseP31.Text = "";
                    txtLotCodeP31.Text = "";
                    txtVNP31.Text = "";
                }
                else if (tc1.SelectedTab == tpparity)
                {
                    LVP34();
                    txtAntP32.Text = "";
                    txtLocationP32.Text = "";
                    txtOvP32.Text = "";
                    txtRemarkP32.Text = "";
                    txtRemarksP32.Text = "";
                    cbStoreHouse.Text = "";
                    txtLotCodeP32.Text = "";
                    txtVNP32.Text = "";
                }
            }
            else
            {
                LVP56();
                txtAntP1.Text = "";
                if (KXMSSysPara.Sys.WareHouse == 2)
                {
                    txtLocationP2.Text = "";
                    cbPackageNo.Text = KXMSSysPara.Sys.DefaultPackage;
                }
            }
        }
        #endregion

        //搜尋平置倉儲位
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            Sqlstr = "SELECT StoreNo, StoreTypeDesc FROM Store WHERE(WareHouse = " + KXMSSysPara.Sys.WareHouse + ") AND(StoreType = 1) AND (StoreTypeDesc like '?1%') ORDER BY StoreTypeDesc";
            Sqlstr = Sqlstr.Replace("?1", txtSearch.Text);
            DataTable DT = ConnectQuery(Sqlstr);
            LVP3.Items.Clear();
            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["StoreTypeDesc"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                LVP3.Items.Add(lvitem);
            }
        }

        #region LV 事件
        private void LVP1_Click(object sender, EventArgs e)
        {
            LVP12();
        }

        private void LVP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPos.Text = LVP1.FocusedItem.SubItems[(int)enLV1ColumnLVP1.X燈號].Text;
            txtDepth.Text = LVP1.FocusedItem.SubItems[(int)enLV1ColumnLVP1.Y燈號].Text;
            txtTray.Text = LVP1.FocusedItem.SubItems[(int)enLV1ColumnLVP1.層].Text; ;
        }

        private void LVP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAntP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.數量].Text;
            txtLocationP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.Location].Text;
            txtOvP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.批號].Text;
            txtRemarkP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.備註].Text;
            txtRemarksP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.備註2].Text;
            cbStoreHouseP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.庫位].Text;
            txtVNP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.廠務編號].Text;
            txtLotCodeP31.Text = LVP2.FocusedItem.SubItems[(int)enLV1ColumnLVP2.LotCode].Text;
        }

        private void LVP3_Click(object sender, EventArgs e)
        {
            LVP34();
            txtAntP32.Text = "";
            txtLocationP32.Text = "";
            txtOvP32.Text = "";
            txtRemarkP32.Text = "";
            txtRemarksP32.Text = "";
            txtVNP32.Text = "";
            txtLotCodeP32.Text = "";
        }

        private void LVP3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtStoreNo.Text = LVP3.FocusedItem.SubItems[2].Text;
            StoreNo = int.Parse(txtStoreNo.Text);
        }

        private void LVP4_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAntP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.數量].Text;
            txtLocationP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.Location].Text;
            txtOvP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.批號].Text;
            txtRemarkP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.備註].Text;
            txtRemarksP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.備註2].Text;
            cbStoreHouse.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.庫位].Text;
            txtVNP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.廠務編號].Text;
            txtLotCodeP32.Text = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.LotCode].Text;

            //StoreHouse = LVP4.FocusedItem.SubItems[2].Text;
            WorkOrderNo = LVP4.FocusedItem.SubItems[11].Text;
            Qty = int.Parse(LVP4.FocusedItem.SubItems[3].Text);
            Mno = LVP4.FocusedItem.SubItems[(int)enLV1ColumnLVP4.料號].Text;
            Location = LVP4.FocusedItem.SubItems[4].Text;
            Mid = int.Parse(LVP4.FocusedItem.SubItems[12].Text);
        }

        private void LVP5_Click(object sender, EventArgs e)
        {
            LVP56();
        }
        
        private void LVP5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KXMSSysPara.Sys.WareHouse == 2)
            {
                txtPos1.Text = LVP5.FocusedItem.SubItems[(int)enLV1ColumnLVP5.X燈號].Text;
                txtDepth1.Text = LVP5.FocusedItem.SubItems[(int)enLV1ColumnLVP5.Y燈號].Text;
                txtTray1.Text = LVP5.FocusedItem.SubItems[(int)enLV1ColumnLVP5.層].Text; 
            }
            else if (KXMSSysPara.Sys.WareHouse == 1)
            {
                txtPos1.Text = LVP5.FocusedItem.SubItems[(int)enLV1ColumnLVP51.X燈號].Text;
                txtDepth1.Text = LVP5.FocusedItem.SubItems[(int)enLV1ColumnLVP51.Y燈號].Text;
                txtTray1.Text = LVP5.FocusedItem.SubItems[(int)enLV1ColumnLVP51.層].Text; ;
            }
        }

        private void LVP6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KXMSSysPara.Sys.WareHouse == 2)
            {
                txtAntP1.Text = LVP6.FocusedItem.SubItems[(int)enLV1ColumnLVP6.數量].Text;
                txtLocationP2.Text = LVP6.FocusedItem.SubItems[(int)enLV1ColumnLVP6.Location].Text;
                cbPackageNo.Text = LVP6.FocusedItem.SubItems[(int)enLV1ColumnLVP6.包裝].Text;
            }
            else if (KXMSSysPara.Sys.WareHouse == 1)
            {
                txtAntP1.Text = LVP6.FocusedItem.SubItems[(int)enLV1ColumnLVP61.數量].Text;
            }
        }

        private void LV1_Click(object sender, EventArgs e)
        {
            LV12();
        }

        private void LV3_Click(object sender, EventArgs e)
        {
            LV34();
        }

        private void LV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPos2.Text = LV1.FocusedItem.SubItems[(int)enLV1ColumnLV1.X燈號].Text;
            txtDepth2.Text = LV1.FocusedItem.SubItems[(int)enLV1ColumnLV1.Y燈號].Text;
            txtTray2.Text = LV1.FocusedItem.SubItems[(int)enLV1ColumnLV1.層].Text; ;
            txtStoreNo2.Text = LV1.FocusedItem.SubItems[4].Text;
        }

        private void LV2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmid1.Text = LV2.FocusedItem.SubItems[8].Text;
            txtStoreNo2.Text = LV2.FocusedItem.SubItems[9].Text;
        }

        private void LV3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            txtPos3.Text = LV3.FocusedItem.SubItems[(int)enLV1ColumnLV3.X燈號].Text;
            txtDepth3.Text = LV3.FocusedItem.SubItems[(int)enLV1ColumnLV3.Y燈號].Text;
            txtTray3.Text = LV3.FocusedItem.SubItems[(int)enLV1ColumnLV3.層].Text; ;
            txtStoreNo3.Text = LV3.FocusedItem.SubItems[4].Text;
        }

        private void LV4_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmid2.Text = LV4.FocusedItem.SubItems[8].Text;
            txtStoreNo3.Text = LV4.FocusedItem.SubItems[9].Text;
        }

        private void LV5_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmid1.Text = LV5.FocusedItem.SubItems[13].Text;
        }
        #endregion

        #region Combobox設定
        private void cbDataP30_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LVP1.Items.Clear();

            if (cbDataP30.SelectedItem == null)
            {
                return;
            }
            else
            {
                if (cbDataP31.SelectedItem == null)
                {
                    Sqlstr = _SqlData.GetData("儲位", 85);
                    Sqlstr = Sqlstr.Replace("?1", cbDataP30.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        LVP1.Items.Add(lvitem);

                        txtPos.Text = DR1["Pos"].ToString();
                        txtDepth.Text = DR1["Depth"].ToString();
                    }
                }
                else
                {
                    Sqlstr = _SqlData.GetData("儲位", 86);
                    Sqlstr = Sqlstr.Replace("?1", cbDataP30.Text);
                    Sqlstr = Sqlstr.Replace("?2", cbDataP31.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        LVP1.Items.Add(lvitem);

                        txtPos.Text = DR1["Pos"].ToString();
                        txtDepth.Text = DR1["Depth"].ToString();
                    }
                }

                cbDataP31.Text = "";
                LVP1.Items.Clear();
                Sqlstr = _SqlData.GetData("儲位", 85);
                Sqlstr = Sqlstr.Replace("?1", cbDataP30.Text);
                DataTable DT2 = ConnectQuery(Sqlstr);
                foreach (DataRow DR2 in DT2.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR2["Carry"].ToString());
                    lvitem.SubItems.Add(int.Parse(DR2["Pos"].ToString()).ToString("00"));
                    lvitem.SubItems.Add((int.Parse(DR2["Depth"].ToString()) + 1).ToString("00"));
                    LVP1.Items.Add(lvitem);

                    txtPos.Text = DR2["Pos"].ToString();
                    txtDepth.Text = DR2["Depth"].ToString();
                }
            }

            cbDataP31.Items.Clear();
            //查詢第幾台所有層數(only Carry)
            Sqlstr = _SqlData.GetData("儲位", 84);
            Sqlstr = Sqlstr.Replace("?1", cbDataP30.Text);
            DataTable DT0 = ConnectQuery(Sqlstr);
            foreach (DataRow DR0 in DT0.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR0["Carry"].ToString();
                cbDataP31.Items.Add(vitem0);
            }

            LVP2.Items.Clear();
            txtAntP31.Text = "";
            txtLocationP31.Text = "";
            txtOvP31.Text = "";
            txtRemarkP31.Text = "";
            txtRemarksP31.Text = "";
        }

        private void cbDataP31_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LVP1.Items.Clear();
            Sqlstr = _SqlData.GetData("儲位", 86);
            Sqlstr = Sqlstr.Replace("?1", cbDataP30.Text);
            Sqlstr = Sqlstr.Replace("?2", cbDataP31.Text);
            DataTable DT1 = ConnectQuery(Sqlstr);
            foreach (DataRow DR1 in DT1.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR1["Carry"].ToString());
                lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                LVP1.Items.Add(lvitem);

                txtPos.Text = DR1["Pos"].ToString();
                txtDepth.Text = DR1["Depth"].ToString();
            }
        }

        private void cbDataP0_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LVP5.Items.Clear();

            if (cbDataP0.SelectedItem == null)
            {
                return;
            }
            else
            {
                if (cbDataP1.SelectedItem == null)
                {
                    Sqlstr = _SqlData.GetData("儲位", 85);
                    Sqlstr = Sqlstr.Replace("?1", cbDataP0.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        LVP5.Items.Add(lvitem);

                        txtPos1.Text = DR1["Pos"].ToString();
                        txtDepth1.Text = DR1["Depth"].ToString();
                    }
                }
                else
                {
                    Sqlstr = _SqlData.GetData("儲位", 86);
                    Sqlstr = Sqlstr.Replace("?1", cbDataP0.Text);
                    Sqlstr = Sqlstr.Replace("?2", cbDataP1.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        LVP5.Items.Add(lvitem);

                        txtPos1.Text = DR1["Pos"].ToString();
                        txtDepth1.Text = DR1["Depth"].ToString();
                    }
                }

                cbDataP1.Text = "";
                LVP5.Items.Clear();
                Sqlstr = _SqlData.GetData("儲位", 85);
                Sqlstr = Sqlstr.Replace("?1", cbDataP0.Text);
                DataTable DT2 = ConnectQuery(Sqlstr);
                foreach (DataRow DR2 in DT2.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR2["Carry"].ToString());
                    lvitem.SubItems.Add(int.Parse(DR2["Pos"].ToString()).ToString("00"));
                    lvitem.SubItems.Add((int.Parse(DR2["Depth"].ToString()) + 1).ToString("00"));
                    LVP5.Items.Add(lvitem);

                    txtPos1.Text = DR2["Pos"].ToString();
                    txtDepth1.Text = DR2["Depth"].ToString();
                }
            }

            cbDataP1.Items.Clear();
            //查詢第幾台所有層數(only Carry)
            Sqlstr = _SqlData.GetData("儲位", 84);
            Sqlstr = Sqlstr.Replace("?1", cbDataP0.Text);
            DataTable DT0 = ConnectQuery(Sqlstr);
            foreach (DataRow DR0 in DT0.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR0["Carry"].ToString();
                cbDataP1.Items.Add(vitem0);
            }

            LVP6.Items.Clear();
            txtAntP1.Text = "";
            txtLocationP2.Text = "";
            cbPackageNo.Text = KXMSSysPara.Sys.DefaultPackage;
        }

        private void cbDataP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LVP5.Items.Clear();
            Sqlstr = _SqlData.GetData("儲位", 86);
            Sqlstr = Sqlstr.Replace("?1", cbDataP0.Text);
            Sqlstr = Sqlstr.Replace("?2", cbDataP1.Text);
            DataTable DT1 = ConnectQuery(Sqlstr);
            foreach (DataRow DR1 in DT1.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR1["Carry"].ToString());
                lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                LVP5.Items.Add(lvitem);

                txtPos1.Text = DR1["Pos"].ToString();
                txtDepth1.Text = DR1["Depth"].ToString();
            }

            LVP6.Items.Clear();
            txtAntP1.Text = "";
            txtLocationP2.Text = "";
            cbPackageNo.Text = KXMSSysPara.Sys.DefaultPackage;
        }

        private void cbPackageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id;
            if (cbPackageNo.SelectedItem == null)
            {
                id = 0;
            }
            else
            {
                int.TryParse((cbPackageNo.SelectedItem as ComboboxItem).Value, out id);
            }
            txtPackageId1.Text = id +"";
        }


        //轉倉作業==================================================================================
        private void cbTurn1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StoreNo;
            if (cbTurn1.SelectedItem == null)
            {
                StoreNo = 0;
            }
            else
            {
                int.TryParse((cbTurn1.SelectedItem as ComboboxItem).Value, out StoreNo);
            }
            txtStoreNo1.Text = StoreNo +"";

            LV5.Items.Clear();
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("儲位", 71);
            Sqlstr = Sqlstr.Replace("?1", txtStoreNo1.Text);
            Sqlstr = Sqlstr + " Order By Mno ";
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["VanderName"].ToString());
                lvitem.SubItems.Add(DR["LotCode"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                lvitem.SubItems.Add(DR["WorkOrderNo"].ToString());
                lvitem.SubItems.Add(DR["Mid"].ToString());
                lvitem.SubItems.Add(DR["storemid"].ToString());
                LV5.Items.Add(lvitem);
            }
        }

        private void cbTurn2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StoreNo;
            if (cbTurn2.SelectedItem == null)
            {
                StoreNo = 0;
            }
            else
            {
                int.TryParse((cbTurn2.SelectedItem as ComboboxItem).Value, out StoreNo);
            }
            txtStoreNo4.Text = StoreNo + "";

            LV6.Items.Clear();
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("儲位", 71);
            Sqlstr = Sqlstr.Replace("?1", txtStoreNo4.Text);
            Sqlstr = Sqlstr + " Order By Mno ";
            DataTable DT = ConnectQuery(Sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR["Mno"].ToString());
                lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                lvitem.SubItems.Add(DR["MQty"].ToString());
                lvitem.SubItems.Add(DR["Location"].ToString());
                lvitem.SubItems.Add(DR["OV"].ToString());
                lvitem.SubItems.Add(DR["VanderName"].ToString());
                lvitem.SubItems.Add(DR["LotCode"].ToString());
                lvitem.SubItems.Add(DR["Mdesc"].ToString());
                lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                lvitem.SubItems.Add(DR["StoreNo"].ToString());
                lvitem.SubItems.Add(DR["WorkOrderNo"].ToString());
                lvitem.SubItems.Add(DR["Mid"].ToString());
                LV6.Items.Add(lvitem);
            }
        }
        
        private void cbMachine1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LV1.Items.Clear();

            if (cbMachine1.SelectedItem == null)
            {
                return;
            }
            else
            {
                if (cbTray1.SelectedItem == null)
                {
                    Sqlstr = _SqlData.GetData("儲位", 85);
                    Sqlstr = Sqlstr.Replace("?1", cbMachine1.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        lvitem.SubItems.Add(DR1["StoreNo"].ToString());
                        LV1.Items.Add(lvitem);

                        txtPos2.Text = DR1["Pos"].ToString();
                        txtDepth2.Text = DR1["Depth"].ToString();
                        txtStoreNo2.Text = DR1["StoreNo"].ToString();
                    }
                }
                else
                {
                    Sqlstr = _SqlData.GetData("儲位", 86);
                    Sqlstr = Sqlstr.Replace("?1", cbMachine1.Text);
                    Sqlstr = Sqlstr.Replace("?2", cbTray1.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        lvitem.SubItems.Add(DR1["StoreNo"].ToString());
                        LV1.Items.Add(lvitem);

                        txtPos2.Text = DR1["Pos"].ToString();
                        txtDepth2.Text = DR1["Depth"].ToString();
                        txtStoreNo2.Text = DR1["StoreNo"].ToString();
                    }
                }

                cbTray2.Text = "";
                LV1.Items.Clear();
                Sqlstr = _SqlData.GetData("儲位", 85);
                Sqlstr = Sqlstr.Replace("?1", cbMachine1.Text);
                DataTable DT2 = ConnectQuery(Sqlstr);
                foreach (DataRow DR2 in DT2.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR2["Carry"].ToString());
                    lvitem.SubItems.Add(int.Parse(DR2["Pos"].ToString()).ToString("00"));
                    lvitem.SubItems.Add((int.Parse(DR2["Depth"].ToString()) + 1).ToString("00"));
                    lvitem.SubItems.Add(DR2["StoreNo"].ToString());
                    LV1.Items.Add(lvitem);

                    txtPos2.Text = DR2["Pos"].ToString();
                    txtDepth2.Text = DR2["Depth"].ToString();
                    txtStoreNo2.Text = DR2["StoreNo"].ToString();
                }
            }

            cbTray1.Items.Clear();
            //查詢第幾台所有層數(only Carry)
            Sqlstr = _SqlData.GetData("儲位", 84);
            Sqlstr = Sqlstr.Replace("?1", cbMachine1.Text);
            DataTable DT0 = ConnectQuery(Sqlstr);
            foreach (DataRow DR0 in DT0.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR0["Carry"].ToString();
                cbTray1.Items.Add(vitem0);
            }

            LV2.Items.Clear();
        }

        private void cbTray1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LV1.Items.Clear();
            Sqlstr = _SqlData.GetData("儲位", 86);
            Sqlstr = Sqlstr.Replace("?1", cbMachine1.Text);
            Sqlstr = Sqlstr.Replace("?2", cbTray1.Text);
            DataTable DT1 = ConnectQuery(Sqlstr);
            foreach (DataRow DR1 in DT1.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR1["Carry"].ToString());
                lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                lvitem.SubItems.Add(DR1["StoreNo"].ToString());
                LV1.Items.Add(lvitem);

                txtPos2.Text = DR1["Pos"].ToString();
                txtDepth2.Text = DR1["Depth"].ToString();
                txtStoreNo2.Text = DR1["StoreNo"].ToString();
            }
        }

        private void cbMachine2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LV3.Items.Clear();

            if (cbMachine2.SelectedItem == null)
            {
                return;
            }
            else
            {
                if (cbTray2.SelectedItem == null)
                {
                    Sqlstr = _SqlData.GetData("儲位", 85);
                    Sqlstr = Sqlstr.Replace("?1", cbMachine2.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        lvitem.SubItems.Add(DR1["StoreNo"].ToString());
                        LV3.Items.Add(lvitem);

                        txtPos3.Text = DR1["Pos"].ToString();
                        txtDepth3.Text = DR1["Depth"].ToString();
                        txtStoreNo3.Text = DR1["StoreNo"].ToString();
                    }
                }
                else
                {
                    Sqlstr = _SqlData.GetData("儲位", 86);
                    Sqlstr = Sqlstr.Replace("?1", cbMachine2.Text);
                    Sqlstr = Sqlstr.Replace("?2", cbTray2.Text);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    foreach (DataRow DR1 in DT1.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR1["Carry"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                        lvitem.SubItems.Add(DR1["StoreNo"].ToString());
                        LV3.Items.Add(lvitem);

                        txtPos3.Text = DR1["Pos"].ToString();
                        txtDepth3.Text = DR1["Depth"].ToString();
                        txtStoreNo3.Text = DR1["StoreNo"].ToString();
                    }
                }

                cbTray2.Text = "";
                LV3.Items.Clear();
                Sqlstr = _SqlData.GetData("儲位", 85);
                Sqlstr = Sqlstr.Replace("?1", cbMachine2.Text);
                DataTable DT2 = ConnectQuery(Sqlstr);
                foreach (DataRow DR2 in DT2.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR2["Carry"].ToString());
                    lvitem.SubItems.Add(int.Parse(DR2["Pos"].ToString()).ToString("00"));
                    lvitem.SubItems.Add((int.Parse(DR2["Depth"].ToString()) + 1).ToString("00"));
                    lvitem.SubItems.Add(DR2["StoreNo"].ToString());
                    LV3.Items.Add(lvitem);

                    txtPos3.Text = DR2["Pos"].ToString();
                    txtDepth3.Text = DR2["Depth"].ToString();
                    txtStoreNo3.Text = DR2["StoreNo"].ToString();
                }
            }

            cbTray2.Items.Clear();
            //查詢第幾台所有層數(only Carry)
            Sqlstr = _SqlData.GetData("儲位", 84);
            Sqlstr = Sqlstr.Replace("?1", cbMachine2.Text);
            DataTable DT0 = ConnectQuery(Sqlstr);
            foreach (DataRow DR0 in DT0.Rows)
            {
                ComboboxItem vitem0 = new ComboboxItem();
                vitem0.Text = DR0["Carry"].ToString();
                cbTray2.Items.Add(vitem0);
            }

            LV4.Items.Clear();
        }

        private void cbTray2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Sqlstr = "";
            LV3.Items.Clear();
            Sqlstr = _SqlData.GetData("儲位", 86);
            Sqlstr = Sqlstr.Replace("?1", cbMachine2.Text);
            Sqlstr = Sqlstr.Replace("?2", cbTray2.Text);
            DataTable DT1 = ConnectQuery(Sqlstr);
            foreach (DataRow DR1 in DT1.Rows)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = "";
                lvitem.SubItems.Add(DR1["Carry"].ToString());
                lvitem.SubItems.Add(int.Parse(DR1["Pos"].ToString()).ToString("00"));
                lvitem.SubItems.Add((int.Parse(DR1["Depth"].ToString()) + 1).ToString("00"));
                lvitem.SubItems.Add(DR1["StoreNo"].ToString());
                LV3.Items.Add(lvitem);

                txtPos3.Text = DR1["Pos"].ToString();
                txtDepth3.Text = DR1["Depth"].ToString();
                txtStoreNo3.Text = DR1["StoreNo"].ToString();
            }
        }

        private void cbStoreHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            StoreHouse = cbStoreHouse.Text;
        }
        #endregion

        #region Button (轉倉)
        private void btnTurn_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            long PID = 0;
            int ActionQty = 0;
            int ShowQty = 0;

            Button Btn = (Button)sender;
            switch (Btn.Tag.ToString())
            {
                //自動倉轉平置倉===========================================================================================
                case "1":
                    if (LV2.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("請選取需要轉倉的物料!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    txtStoreHouse.Text = LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.庫位].Text;
                    
                    if (LV2.FocusedItem.SubItems[8].Text == "")
                    {
                        MessageBox.Show("此儲位有問題，不能轉倉!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        Sqlstr = _SqlData.GetData("儲位", 18);
                        Sqlstr = Sqlstr.Replace("?1", txtStoreNo1.Text);
                        Sqlstr = Sqlstr.Replace("?2", LV2.FocusedItem.SubItems[8].Text);
                        Connect(Sqlstr);
                    }

                    //取得PID
                    Sqlstr = _SqlData.GetData("異動", 3);
                    DataTable DT = ConnectQuery(Sqlstr);
                    PID = int.Parse(DT.Rows[0][0].ToString()) + 1;
                    ActionQty = int.Parse(LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.數量].Text);

                    if (KXMSSysPara.Sys.WareHouse == 3)
                    {
                        if (ActionQty > 999999)
                        {
                            ShowQty = 999999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine1.Text), "E1", Convert.ToInt16(txtTray2.Text), Convert.ToInt16(txtPos2.Text), Convert.ToInt16(txtDepth2.Text), ShowQty, "- " + txtStoreHouse.Text, "", PID, 6);
                    }
                    else
                    {
                        if (ActionQty > 9999)
                        {
                            ShowQty = 9999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine1.Text), "E1", Convert.ToInt16(txtTray2.Text), Convert.ToInt16(txtPos2.Text), Convert.ToInt16(txtDepth2.Text), ShowQty, "- " + txtStoreHouse.Text, "", PID, 4);
                    }

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3",txtStoreNo1.Text);                                               //storeno
                    DateTime Dtp = DateTime.Now; 
                    Sqlstr = Sqlstr.Replace("?4", Dtp.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID +"");
                    Sqlstr = Sqlstr.Replace("?6", 1 +"");                                                         //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", "");                                                            //備註
                    Sqlstr = Sqlstr.Replace("?9", cbMachine1.Text);                                               //機器
                    Sqlstr = Sqlstr.Replace("?01", PID +"");
                    Sqlstr = Sqlstr.Replace("?02", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註2].Text);     //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註].Text);      //備註
                    Connect(Sqlstr);

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", txtStoreNo1.Text);                                              //storeno (cbTrun1)
                    DateTime Dtp1 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp1.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 0 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註].Text);       //備註
                    Sqlstr = Sqlstr.Replace("?9", 0 +"");                                                         //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註2].Text);     //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註].Text);      //備註
                    Connect(Sqlstr);

                    LV12();
                    LV56();
                    break;

                //平置倉轉自動倉=========================================================================================
                case "2":
                    if (LV1.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("請選取轉倉的位置!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (LV2.SelectedItems.Count == 0 || txtStoreNo2.Text != "")
                    {
                        Sqlstr = _SqlData.GetData("儲位", 18);
                        Sqlstr = Sqlstr.Replace("?1", txtStoreNo2.Text);
                        Sqlstr = Sqlstr.Replace("?2", txtmid1.Text);
                        Connect(Sqlstr);
                    }
                    else
                    {
                        MessageBox.Show("此儲位有問題或已存放其他物料，不能轉倉!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //取得PID
                    Sqlstr = _SqlData.GetData("異動", 3);
                    DataTable DT1 = ConnectQuery(Sqlstr);
                    PID = int.Parse(DT1.Rows[0][0].ToString()) + 1;
                    ActionQty = int.Parse(LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.數量].Text);
                    txtStoreHouse.Text = LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.庫位].Text;
                    if (KXMSSysPara.Sys.WareHouse == 3)
                    {
                        if (ActionQty > 999999)
                        {
                            ShowQty = 999999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine1.Text), "E1", Convert.ToInt16(txtTray2.Text), Convert.ToInt16(txtPos2.Text), Convert.ToInt16(txtDepth2.Text), ShowQty, "+ " + txtStoreHouse.Text, "", PID, 6);
                    }
                    else
                    {
                        if (ActionQty > 9999)
                        {
                            ShowQty = 9999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine1.Text), "E1", Convert.ToInt16(txtTray2.Text), Convert.ToInt16(txtPos2.Text), Convert.ToInt16(txtDepth2.Text), ShowQty, "+ " + txtStoreHouse.Text, "", PID, 4);
                    }

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.StoreNo].Text);    //storeno
                    DateTime Dtp2 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp2.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 1 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", "");                                                            //備註
                    Sqlstr = Sqlstr.Replace("?9", 0 +"");                                                         //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註].Text);      //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註2].Text);     //備註
                    Sqlstr = Sqlstr.Replace("?07", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.廠務編號].Text);  //廠務編號
                    Sqlstr = Sqlstr.Replace("?08", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Lotcode].Text);   //Lot Code
                    Connect(Sqlstr);

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", txtStoreNo2.Text);                                              //storeno
                    DateTime Dtp3 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp3.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 0 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註].Text);       //備註
                    Sqlstr = Sqlstr.Replace("?9", cbMachine1.Text);                                               //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註].Text);      //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註2].Text);     //備註
                    Sqlstr = Sqlstr.Replace("?07", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.廠務編號].Text);  //廠務編號
                    Sqlstr = Sqlstr.Replace("?08", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Lotcode].Text);   //Lot Code
                    Connect(Sqlstr);

                    LV56();
                    LV12();
                    break;

                //自動倉轉自動倉================================================================================================
                case "3":
                    if (LV2.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("請選取需要轉倉的物料!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (txtmid1.Text != "" && txtStoreNo3.Text != "" && txtMno.Text == "")
                    {
                        if (txtStoreNo2.Text == txtStoreNo3.Text)
                        {
                            MessageBox.Show("儲位相同!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        Sqlstr = _SqlData.GetData("儲位", 18);
                        Sqlstr = Sqlstr.Replace("?1", txtStoreNo3.Text);
                        Sqlstr = Sqlstr.Replace("?2", txtmid1.Text);
                        Connect(Sqlstr);
                    }
                    else
                    {
                        MessageBox.Show("此儲位有問題或已存放其他物料，不能轉倉!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //取得PID
                    Sqlstr = _SqlData.GetData("異動", 3);
                    DataTable DT2 = ConnectQuery(Sqlstr);
                    PID = int.Parse(DT2.Rows[0][0].ToString()) + 1;
                    ActionQty = int.Parse(LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.數量].Text);
                    txtStoreHouse.Text = LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.庫位].Text;
                    if (KXMSSysPara.Sys.WareHouse == 3)
                    {
                        if (ActionQty > 999999)
                        {
                            ShowQty = 999999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine1.Text), "E1", Convert.ToInt16(txtTray2.Text), Convert.ToInt16(txtPos2.Text), Convert.ToInt16(txtDepth2.Text), ShowQty, "- " + txtStoreHouse.Text, "", PID, 6);
                    }
                    else
                    {
                        if (ActionQty > 9999)
                        {
                            ShowQty = 9999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine1.Text), "E1", Convert.ToInt16(txtTray2.Text), Convert.ToInt16(txtPos2.Text), Convert.ToInt16(txtDepth2.Text), ShowQty, "- " + txtStoreHouse.Text, "", PID, 4);
                    }

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", txtStoreNo2.Text);    //storeno
                    DateTime Dtp4 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp4.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 1 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", "");                                                            //備註
                    Sqlstr = Sqlstr.Replace("?9", cbMachine1.Text);                                               //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註].Text);      //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註2].Text);     //備註
                    Connect(Sqlstr);


                    //取得PID
                    Sqlstr = _SqlData.GetData("異動", 3);
                    DataTable DT3 = ConnectQuery(Sqlstr);
                    PID = int.Parse(DT2.Rows[0][0].ToString()) + 1;
                    if (KXMSSysPara.Sys.WareHouse == 3)
                    {
                        if (ActionQty > 999999)
                        {
                            ShowQty = 999999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine2.Text), "E1", Convert.ToInt16(txtTray3.Text), Convert.ToInt16(txtPos3.Text), Convert.ToInt16(txtDepth3.Text), ShowQty, "+ " + txtStoreHouse.Text, "", PID, 6);
                    }
                    else
                    {
                        if (ActionQty > 9999)
                        {
                            ShowQty = 9999;
                        }
                        else
                        {
                            ShowQty = ActionQty;
                        }
                        _modCtrl.SendCommand(Convert.ToInt16(cbMachine2.Text), "E1", Convert.ToInt16(txtTray3.Text), Convert.ToInt16(txtPos3.Text), Convert.ToInt16(txtDepth3.Text), ShowQty, "+ " + txtStoreHouse.Text, "", PID, 4);
                    }

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", txtStoreNo3.Text);                              //storeno
                    DateTime Dtp5 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp5.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 0 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", "");                                                            //備註
                    Sqlstr = Sqlstr.Replace("?9", cbMachine1.Text);                                               //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註].Text);      //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV2.FocusedItem.SubItems[(int)enLV1ColumnLV2.備註2].Text);     //備註
                    Connect(Sqlstr);

                    LV12();
                    LV34();
                    break;

                //平置倉轉平置倉=============================================================================================
                case "4":
                    if (LV5.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("請選取需要轉倉的物料!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    Sqlstr = _SqlData.GetData("儲位", 18);
                    Sqlstr = Sqlstr.Replace("?1", txtStoreNo4.Text);
                    Sqlstr = Sqlstr.Replace("?2", txtmid1.Text);
                    Connect(Sqlstr);

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", txtStoreNo1.Text);    //storeno
                    DateTime Dtp6 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp6.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 1 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", "");                                                            //備註
                    Sqlstr = Sqlstr.Replace("?9", 0 +"");                                                         //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註].Text);      //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註2].Text);     //備註
                    Sqlstr = Sqlstr.Replace("?07", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.廠務編號].Text);  //廠務編號
                    Sqlstr = Sqlstr.Replace("?08", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Lotcode].Text);   //Lot Code
                    Connect(Sqlstr);

                    Sqlstr = _SqlData.GetData("異動", 4);
                    Sqlstr = Sqlstr.Replace("?1", "");                                                            //工單單號
                    Sqlstr = Sqlstr.Replace("?2", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.料號].Text);       //mno
                    Sqlstr = Sqlstr.Replace("?3", txtStoreNo4.Text);    //storeno
                    DateTime Dtp7 = DateTime.Now;
                    Sqlstr = Sqlstr.Replace("?4", Dtp7.ToString("yyyy/MM/dd HH:mm:ss"));
                    Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
                    Sqlstr = Sqlstr.Replace("?6", 0 + "");                                                        //0入庫1領料
                    Sqlstr = Sqlstr.Replace("?7", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.數量].Text);       //數量
                    Sqlstr = Sqlstr.Replace("?8", "");                                                            //備註
                    Sqlstr = Sqlstr.Replace("?9", 0 +"");                                                         //機器
                    Sqlstr = Sqlstr.Replace("?01", PID + "");
                    Sqlstr = Sqlstr.Replace("?02", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Location].Text);  //location
                    Sqlstr = Sqlstr.Replace("?03", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.庫位].Text);      //庫位
                    Sqlstr = Sqlstr.Replace("?04", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.批號].Text);      //批號
                    Sqlstr = Sqlstr.Replace("?05", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註].Text);      //備註2
                    Sqlstr = Sqlstr.Replace("?06", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.備註2].Text);     //備註
                    Sqlstr = Sqlstr.Replace("?07", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.廠務編號].Text);  //廠務編號
                    Sqlstr = Sqlstr.Replace("?08", LV5.FocusedItem.SubItems[(int)enLV1ColumnLV5.Lotcode].Text);   //Lot Code
                    Connect(Sqlstr);

                    LV56();
                    LV561();
                    break;
                default:
                    MessageBox.Show("轉倉成功!!!", "KSMrp");
                    break;
            }

        }
        #endregion
    }

}
