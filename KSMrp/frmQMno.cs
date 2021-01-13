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
using Microsoft.VisualBasic;
using nsKXMSUC;

namespace KSMrp
{
    public partial class frmQMno : Form
    {
        SqlData _SqlData;
        private ExportService exService;
        public frmQMno()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
            exService = new ExportService();
        }

        #region 判斷是否為數字
        public static bool IsNumeric(string Value)
        {
            try
            {
                int i = Convert.ToInt32(Value);
                return true ;
            }
            catch
            {
                return false ;
            }
        }
        #endregion

        #region 一般SQL連線
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
            OleDbCommand olCmd = new OleDbCommand("",Conn);
            Conn.Open();
            olCmd.CommandText = Sqlstr;
            olCmd.ExecuteNonQuery();
            Conn.Close();
        }
        #endregion

        #region WareHouse顯示方式
        private void visible(int i)
        {
            switch (i)
            {
                case 1:
                    label1.Text = "料號";
                    label1.Location = new System.Drawing.Point(52, 13);
                    label2.Text = "工單單號";
                   
                    chIC.Visible = false;
                    //將修改txt改放至chIC的位置
                    txtmodify.Visible = true;
                    //txtmodify.Location = new System.Drawing.Point(402, 8);
                    txtmodify.Left = chIC.Left;
                    txtmodify.Top = 8;
                    txtmodifyNumNo.Visible = true;
                    //txtmodifyNumNo.Location = new System.Drawing.Point(402, 45);
                    txtmodifyNumNo.Left = chIC.Left;
                    txtmodifyNumNo.Top = 45;

                    btnRemark.Visible = true;
                    //btnRemark.Location = new System.Drawing.Point(558, 7);
                    btnRemark.Left = txtmodifyNumNo.Left + txtmodify.Width + 30;
                    btnRemark.Top = 7;

                    btnNub.Visible = true;
                    //btnNub.Location = new System.Drawing.Point(558, 44);
                    btnNub.Left = txtmodifyNumNo.Left + txtmodify.Width + 30;
                    btnNub.Top = 44;
                    labelData.Left = btnRemark.Left + btnRemark.Width + 30;
                    labelData.Top = 12;
                    //labelData.Location = new System.Drawing.Point(674, 12);

                    //LV1.Visible = true;
                    //LV2.Visible = false;
                    break;
                case 2:
                    label1.Text = "UPC";
                    label1.Location = new System.Drawing.Point(52, 13);
                    label2.Text = "成品料號";
                    //labelData.Location = new System.Drawing.Point(491, 15);
                    
                    labelData.Left = chIC.Left + chIC.Width +30; // = new System.Drawing.Point(491, 15);
                    labelData.Top = 15;
                    txtmodify.Visible = false;
                    txtmodifyNumNo.Visible = true;
                    //txtmodifyNumNo.Location = new System.Drawing.Point(487, 45);

                    txtmodifyNumNo.Left = chIC.Left + chIC.Width + 30;
                    txtmodifyNumNo.Top = 45;

                    btnRemark.Visible = false;
                    btnNub.Visible = true;
                    //btnNub.Location = new System.Drawing.Point(643, 44);

                    btnNub.Left = txtmodifyNumNo.Left + txtmodifyNumNo.Width + 30;
                    btnNub.Top = 44;
                    //LV1.Visible = true;
                    //LV2.Visible = false;
                    break;
                case 3:
                    label1.Text = "物料編號";
                    label1.Location = new System.Drawing.Point(12, 13);
                    label2.Text = "Location";
                    labelData.Left = chIC.Left + chIC.Width +30; // = new System.Drawing.Point(491, 15);
                    labelData.Top = 15;
                    lblOverdue.Visible = true;
                    lblOverdue.Left = chIC.Left + chIC.Width + 30;
                    lblOverdue.Top = 60;
                    btnExport1.Visible = true;
                    btnExport1.Left = chIC.Left + chIC.Width + 350;
                    btnExport1.Top = 1;
                    txtmodify.Visible = false;
                    txtmodifyNumNo.Visible = false;
                    btnRemark.Visible = false;
                    btnNub.Visible = false;
                    chIC.Visible = true;
                    //LV1.Visible = false;
                    //LV2.Visible = true;
                    break;
            }
        }
        #endregion

        private void frmQMno_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
        }

        private void frmQMno_Load(object sender, EventArgs e)
        {
            //窗體起始位置
            int x = (1500 - this.Size.Width) / 2;
            int y = (800 - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;          //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(x, y);                  //窗體的起始位置為(x,y)

            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                visible(1);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                visible(2);
            }
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                visible(3);
            }
            initLV1();
        }

        #region ListView設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }

        private void initLV1()
        {
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                LV1.Clear();
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV1.Columns.Add("料號", 120, HorizontalAlignment.Left);
                LV1.Columns.Add("機器", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("層數", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("燈號 x", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("燈號 y", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("庫位", 70, HorizontalAlignment.Center);
                LV1.Columns.Add("工單單號", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("Location", 88, HorizontalAlignment.Center);
                LV1.Columns.Add("包裝", 110, HorizontalAlignment.Center);
                LV1.Columns.Add("備註", 120, HorizontalAlignment.Center);
                LV1.Columns.Add("MMainID", 80, HorizontalAlignment.Center);
            }
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                LV1.Clear();
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV1.Columns.Add("料號", 120, HorizontalAlignment.Left);
                LV1.Columns.Add("機器", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("層數", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("燈號 x", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("燈號 y", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("庫位", 70, HorizontalAlignment.Center);
                LV1.Columns.Add("工單單號", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("Location", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("包裝", 110, HorizontalAlignment.Center);
                LV1.Columns.Add("備註", 110, HorizontalAlignment.Center);
                LV1.Columns.Add("MMainID", 80, HorizontalAlignment.Center);
            }
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                LV1.Clear();
                LV1.Columns.Add(" ", 0, HorizontalAlignment.Left);
                LV1.Columns.Add("料號", 120, HorizontalAlignment.Left);
                LV1.Columns.Add("Location", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("Date Code", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("備註", 90, HorizontalAlignment.Center);
                LV1.Columns.Add("備註2", 90, HorizontalAlignment.Center);
                LV1.Columns.Add("機器", 50, HorizontalAlignment.Right);
                LV1.Columns.Add("層數", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("燈號 x", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("燈號 y", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("庫位", 60, HorizontalAlignment.Center);
                LV1.Columns.Add("棧板位置", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("MMainID", 80, HorizontalAlignment.Center);
                // 2020/05/13 新增廠編、Lot code、Overdue、remaining
                LV1.Columns.Add("廠務編號", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("Lot Code", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("Overdue", 80, HorizontalAlignment.Center);
                LV1.Columns.Add("剩餘週數", 80, HorizontalAlignment.Center);
            }

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column : int
        {
            料號 = 1,
            機器 = 2,
            層數 = 3,
            燈號x = 4,
            燈號y = 5,
            數量 = 6,
            庫位 = 7,
            工單單號 = 8,
            Location = 9,
            包裝 = 10,
            備註 = 11,
            MMainID = 12,
        }
        private enum enLV1Column2 : int
        {
            料號 = 1,
            Location = 2,
            數量 = 3,
            DateCode = 4,
            備註 = 5,
            備註2 = 6,
            機器 = 7,
            層數 = 8,
            燈號x = 9,
            燈號y = 10,
            庫位 = 11,
            棧板位置 = 12,
            MMainID = 13,
            //  2020/05/13 新增廠編、Lot code、Overdue
            廠務編號 = 14,
            LotCode = 15,
            Overdue = 16,
            剩餘週數 = 17,
        }
        #endregion

        private void LoadLV()
        {
            string sqlstr = "";
            long Qty = 0;

            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(sqlstr, Conn);
            if (txtNo.Text.Length == 0 && txtNumNo.Text.Length == 0)
            {
                MessageBox.Show("請輸入正確的查詢資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNo.Focus();
                return;
            }

            //半成品倉
            if (KXMSSysPara.Sys.WareHouse == 1)
            {
                //查詢(by工單單號)
                if (txtNo.Text.Length == 0 && txtNumNo.Text.Length != 0)
                {
                    sqlstr = _SqlData.GetData("儲位", 53);
                    sqlstr = sqlstr.Replace("?1", txtNumNo.Text);
                }
                //查詢(料號)
                else if (txtNo.Text.Length != 0 && txtNumNo.Text.Length == 0)
                {
                    sqlstr = _SqlData.GetData("儲位", 51);
                    sqlstr = sqlstr.Replace("?1", txtNo.Text);
                }
                //查詢(by料號、工單單號)
                else if (txtNo.Text.Length != 0 && txtNumNo.Text.Length != 0)
                {
                    sqlstr = _SqlData.GetData("儲位", 54);
                    sqlstr = sqlstr.Replace("?1", txtNo.Text);
                    sqlstr = sqlstr.Replace("?2", txtNumNo.Text);
                }
            }
            //成品倉
            else if (KXMSSysPara.Sys.WareHouse == 2)
            {
                DataTable DT0 = new DataTable();
                //成品料號
                if (txtNumNo.Text.Length != 0)
                {
                    sqlstr = _SqlData.GetData("其他", 25);
                    sqlstr = sqlstr.Replace("?1", txtNumNo.Text);
                    Conn.Open();
                    DA.SelectCommand.CommandText = sqlstr;
                    DT0.Clear();
                    DA.Fill(DT0);
                    Conn.Close();
                    if (DT0.Rows.Count == 0)
                    {
                        MessageBox.Show("無此成品料號!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        txtNo.Text = DT0.Rows[0][0].ToString();
                    }
                }
                //UDP code
                else if (txtNo.Text.Length != 0)
                {
                    sqlstr = _SqlData.GetData("其他", 24);
                    sqlstr = sqlstr.Replace("?1", txtNo.Text);
                    Conn.Open();
                    DA.SelectCommand.CommandText = sqlstr;
                    DT0.Clear();
                    DA.Fill(DT0);
                    Conn.Close();
                    if (DT0.Rows.Count == 0)
                    {
                        MessageBox.Show("無此UPC!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        txtNumNo.Text = DT0.Rows[0][0].ToString();
                    }
                }

                sqlstr = _SqlData.GetData("儲位", 51);
                sqlstr = sqlstr.Replace("?1", txtNo.Text);
            }
            //IC倉
            else if (KXMSSysPara.Sys.WareHouse == 3)
            {
                string s5 = "";
                sqlstr = _SqlData.GetData("儲位", 56);

                if (chIC.Checked)
                {
                    //查詢(by location)
                    if (txtNo.Text.Length == 0 && txtNumNo.Text.Length != 0)
                    {
                        s5 = "MMain.Location like '" + txtNumNo.Text + "%'";
                    }
                    //查詢(by 料號)
                    else if (txtNo.Text.Length != 0 && txtNumNo.Text.Length == 0)
                    {
                        s5 = "MMain.Mno like '" + txtNo.Text + "%'";
                    }
                    //查詢(by location、料號)
                    else if (txtNo.Text.Length != 0 && txtNumNo.Text.Length != 0)
                    {
                        s5 = "MMain.Mno like '" + txtNo.Text + "%'";
                        s5 += " and MMain.Location like '" + txtNumNo.Text + "%'";
                        
                    }
                }
                else
                {
                    //查詢(by location)
                    if (txtNo.Text.Length == 0 && txtNumNo.Text.Length != 0)
                    {
                        s5 = "MMain.Location = '" + txtNumNo.Text + "'";
                    }
                    //查詢(by 料號)
                    else if (txtNo.Text.Length != 0 && txtNumNo.Text.Length == 0)
                    {
                        s5 = "MMain.Mno = '" + txtNo.Text + "'";
                    }
                    //查詢(by location、料號)
                    else if (txtNo.Text.Length != 0 && txtNumNo.Text.Length != 0)
                    {
                        s5 = "MMain.Location = '" + txtNumNo.Text + "'";
                        s5 += " and MMain.Mno = '" + txtNo.Text + "'";
                    }
                }
                 sqlstr = sqlstr.Replace("?1", s5);
            }

            LV1.Items.Clear();
            DataTable DT = new DataTable();                            //表示記憶體中資料表
            try
            {
                Conn.Open();
                DA.SelectCommand.CommandText = sqlstr;
                DA.Fill(DT);
                Conn.Close();
                //IC倉
                if (KXMSSysPara.Sys.WareHouse == 3)
                {
                    DateTime _DateTime = new DateTime(DateTime.Now.Year,1,1);
                    int NowDateY;
                    int NowDateW;
                    NowDateY = DateTime.Now.Year % 100;
                    NowDateW = int.Parse((DateDiff.Simulate.DateDiff(DateDiff.Simulate.DateInterval.Weekday,_DateTime,DateTime.Now)).ToString());       //算週數
                    int WeekDiff;
                    string Pcode;
                    string PcodeY;
                    string PcodeW = "";
                    string Mno;
                    string KTCPartNo;        
                    string ShelfLife;      
                    int DateW;              //存放週數
                    int Overdue = 0;

                    foreach (DataRow DR in DT.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();     //使用預設值，初始化 ListViewItem 類別的新執行個體
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR["Mno"].ToString());
                        lvitem.SubItems.Add(DR["Location"].ToString());
                        lvitem.SubItems.Add(DR["MQty"].ToString());
                        lvitem.SubItems.Add(DR["OV"].ToString());
                        lvitem.SubItems.Add(DR["Mdesc"].ToString());
                        lvitem.SubItems.Add(DR["Mdesc2"].ToString());
                        lvitem.SubItems.Add(DR["MachineNo"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR["Carry"].ToString()).ToString("00"));
                        lvitem.SubItems.Add(int.Parse(DR["Pos"].ToString()).ToString("00"));
                        lvitem.SubItems.Add((int.Parse(DR["Depth"].ToString()) + 1) + "");
                        lvitem.SubItems.Add(DR["StoreHouse"].ToString());
                        lvitem.SubItems.Add(DR["StoreTypeDesc"].ToString());
                        lvitem.SubItems.Add(DR["MID"].ToString());
                        lvitem.SubItems.Add(DR["VanderName"].ToString());
                        lvitem.SubItems.Add(DR["LotCode"].ToString());
                        lvitem.SubItems.Add(DR["Overdue"].ToString());
                        lvitem.SubItems.Add(DR["remaining"].ToString());
                        Qty += int.Parse(DR["MQty"].ToString());
                        LV1.Items.Add(lvitem);

                        Mno = lvitem.SubItems[1].Text.ToString();
                        Pcode = lvitem.SubItems[4].Text.ToString();
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
                        sqlstr = _SqlData.GetData("ShelfLife", 6);
                        DataTable DT0 = ConnectQuery(sqlstr);
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
                                                lvitem.SubItems[16].Text = "1";                       //Overdue 
                                            }
                                            else if (WeekDiff < DateW - 13)
                                            {
                                                //超過三個月 5~13週
                                                lvitem.SubItems[16].Text = "3";                       //Overdue 
                                            }
                                            else
                                            {
                                                //超過六個月(含以上) 14~26週
                                                lvitem.SubItems[16].Text = "6";                       //Overdue 
                                            }

                                            lvitem.SubItems[17].Text = (DateW - WeekDiff + "");    //remaining Weeks

                                            if (lvitem.SubItems[16].Text == "1" || lvitem.SubItems[16].Text == "3" || lvitem.SubItems[16].Text == "6")
                                            {
                                                Overdue += 1;
                                                lblOverdue.Text = "逾期件數：" + Overdue + "件";
                                            }
                                        }
                                        else
                                        {
                                            //剩下1個月轉成紅色
                                            lvitem.ForeColor = Color.Red;
                                            lvitem.SubItems[17].Text = DateW - WeekDiff + "";    //remaining Weeks
                                        }
                                    }
                                    else if (WeekDiff > DateW - 13)
                                    {
                                        //剩下3個月轉成橘色
                                        lvitem.ForeColor = Color.DarkOrange;
                                        lvitem.SubItems[17].Text = DateW - WeekDiff + "";    //remaining Weeks
                                    }
                                    else if (WeekDiff > DateW - 26)
                                    {
                                        //剩下6個月轉成藍色
                                        lvitem.ForeColor = Color.Blue;
                                        lvitem.SubItems[17].Text = DateW - WeekDiff + "";    //remaining Weeks
                                    }
                                    else
                                    {
                                        //存放仍在期限內
                                        lvitem.SubItems[17].Text = DateW - WeekDiff + "";    //remaining Weeks
                                    }

                                    if (PcodeW == "" || int.Parse(PcodeW) > 52)
                                    {
                                        lvitem.SubItems[17].Text = "";
                                        lvitem.SubItems[17].Text += "格式有誤";
                                    }

                                    lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                                }

                                ////116、118存放3年(52*3)
                                //if (PartNoType == "116" || PartNoType == "118")
                                //{
                                //    if (WeekDiff > ((52 * 3) - 4))
                                //    {
                                //        //剩下1個月轉成紅色
                                //        lvitem.ForeColor = Color.Red;
                                //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                                //        //lvitem.SubItems[3].Font.Bold = true;
                                //    }
                                //    else if (WeekDiff > ((52 * 3) - 13))
                                //    {
                                //        //剩下3個月轉成橘色
                                //        lvitem.ForeColor = Color.DarkOrange;
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
                                //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                                //    }
                                //    //剩下3個月轉成橘色
                                //    else if (WeekDiff > (52 - 13))
                                //    {
                                //        lvitem.ForeColor = Color.DarkOrange;
                                //        lvitem.Font = new System.Drawing.Font("新細明體", 12F, FontStyle.Bold);
                                //    }
                                //}
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow DR in DT.Rows)
                    {
                        ListViewItem lvitem1 = new ListViewItem();     //使用預設值，初始化 ListViewItem 類別的新執行個體
                        lvitem1.Text = "";
                        lvitem1.SubItems.Add(DR["Mno"].ToString());
                        lvitem1.SubItems.Add(DR["MachineNo"].ToString());
                        lvitem1.SubItems.Add(int.Parse(DR["Carry"].ToString()).ToString("00"));
                        lvitem1.SubItems.Add(int.Parse(DR["Pos"].ToString()).ToString("00"));
                        lvitem1.SubItems.Add((int.Parse(DR["Depth"].ToString()) + 1) + "");
                        lvitem1.SubItems.Add(DR["MQty"].ToString());
                        lvitem1.SubItems.Add(DR["StoreHouse"].ToString());
                        lvitem1.SubItems.Add(DR["WorkOrderNo"].ToString());
                        lvitem1.SubItems.Add(DR["Location"].ToString());
                        lvitem1.SubItems.Add(DR["PackageDesc"].ToString());
                        lvitem1.SubItems.Add(DR["Mdesc"].ToString());
                        lvitem1.SubItems.Add(DR["MMainID"].ToString());
                        Qty += int.Parse(DR["MQty"].ToString());
                        LV1.Items.Add(lvitem1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            labelSum.Text = Qty.ToString();
        }
        
        private void LV1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                labelData.Text = "第" + LV1.FocusedItem.SubItems[(int)enLV1Column2.機器].Text + "台" + LV1.FocusedItem.SubItems[(int)enLV1Column2.層數].Text + "層-(x)" + LV1.FocusedItem.SubItems[(int)enLV1Column2.燈號x].Text + "-(y)" + LV1.FocusedItem.SubItems[(int)enLV1Column2.燈號y].Text + "-料號:" + LV1.FocusedItem.SubItems[(int)enLV1Column2.料號].Text;
            }
            else 
            {
                labelData.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.層數].Text + "層-(x)" + LV1.FocusedItem.SubItems[(int)enLV1Column.燈號x].Text + "-(y)" + LV1.FocusedItem.SubItems[(int)enLV1Column.燈號y].Text + "-料號:" + LV1.FocusedItem.SubItems[(int)enLV1Column.料號].Text;
                txtMID.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.MMainID].Text;
                txtmodify.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.備註].Text;
                txtmodifyNumNo.Text = LV1.FocusedItem.SubItems[(int)enLV1Column.工單單號].Text;
            }
        }

        #region ExportLV1設定
        private void ExportLV1()
        {
            string ReportName = "物料資訊";
            ExportExcel(ReportName);
        }

        private void ExportExcel(string ReportName)
        {
            try
            {
                string vErrMsg = "";
                if (LV1.Items.Count == 0) { throw new KXMSException("無可匯出的資料！", "", enMessageType.Info); }
                //詢問儲存路徑
                string vFileName = KXMSSysPara.Sys.ReportFilePath + @"\Export.xlsx";
                string vSheetName = ReportName;
                SFDlg1.FileName = vSheetName + "-" + DateTime.Now.ToString("yyyyMMdd");
                SFDlg1.Filter = "Excel File (*.xlsx)|*.xlsx";
                SFDlg1.InitialDirectory = KXMSSysPara.Sys.ReportFilePath;
                //SFDlg1.RestoreDirectory = true;
                SFDlg1.CheckFileExists = false;
                if (SFDlg1.ShowDialog() == DialogResult.OK)
                {
                    vFileName = SFDlg1.FileName;
                }
                else
                {
                    throw new KXMSException("取消匯出", "", enMessageType.Info);
                }
                DataTable DT = new DataTable();
                //建立標頭
                for (int i = 1; i < LV1.Columns.Count; i++)
                {
                    //跳過第一欄
                    if (LV1.Columns[i].TextAlign == HorizontalAlignment.Right)
                    {
                        DT.Columns.Add(LV1.Columns[i].Text, typeof(Decimal));
                    }
                    else
                    {
                        DT.Columns.Add(LV1.Columns[i].Text, typeof(String));
                    }
                }
                //建立內容
                foreach (ListViewItem vItem in LV1.Items)
                {
                    DataRow DR = DT.NewRow();
                    for (int i = 1; i < LV1.Columns.Count; i++)
                    {
                        //跳過第一欄
                        DR[i - 1] = vItem.SubItems[i].Text;
                    }
                    DT.Rows.Add(DR);
                }

                if (exService.ExportExcel(vSheetName, vFileName, DT, out vErrMsg) == false)
                {
                    throw new Exception(vErrMsg);
                };
                KXMSMsgBox.Show("Excel匯出成功", "報表匯出", "", enMessageType.Success, enMessageButton.OK);
            }
            catch (KXMSException ex)
            {
                if (String.IsNullOrEmpty(ex.Message) == false)
                {
                    KXMSMsgBox.Show(ex.Message, "報表匯出", ex.MessageDetail, ex.MessageType, enMessageButton.OK);
                }
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show(ex.Message, "報表匯出", ex.Message, enMessageType.Warning, enMessageButton.OK);

            }
        }
        #endregion

        #region Button事件
        private void btndemand_Click(object sender, EventArgs e)
        {
            LoadLV();
        }

        private void btnRemark_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            if (txtMID.Text.Length == 0)
            {
                MessageBox.Show("資料有誤，請在下面重新點選資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Sqlstr = _SqlData.GetData("儲位", 22);
            Sqlstr = Sqlstr.Replace("?1", txtmodify.Text);
            Sqlstr = Sqlstr.Replace("?2", txtMID.Text);
            Connect(Sqlstr);

            labelData.Text = "";
            txtMID.Text = "";
            txtmodify.Text = "";
            txtmodifyNumNo.Text = "";
            LoadLV();
        }

        //(一領一出，改工單號碼，原位置不動)--------20110318加入(半成品倉)
        //修改工單
        private void btnNub_Click(object sender, EventArgs e)
        {
            string Sqlstr ="";
            //string S;
            long StoreNo = 0;
            long PID = 0;

            if (txtMID.Text.Length == 0 || txtmodifyNumNo.Text.Length ==0)
            {
                MessageBox.Show("資料有誤，請在下面重新點選資料!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("確定修改工單" + txtNumNo.Text + "->" + txtmodifyNumNo.Text, "KSMrp", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            { return; }

            string value = "請輸入密碼";
            if (InputBox.Inputbox("KSMrp", "使用者為：" + KXMSSysPara.Sys.UID + "\r\n" + "請輸入登入密碼確認身分", ref value) == DialogResult.OK)
            {
                if (value != KXMSSysPara.Sys.Pass)
                {
                    MessageBox.Show("密碼輸入錯誤!!","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
            else { return; }

            if(txtMID.Text.Length == 0 || txtmodifyNumNo.Text.Length == 0)
            {
                MessageBox.Show("資料有誤，請在下面重新點選資料!!!", "KSMrp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //領料出來
            Sqlstr = _SqlData.GetData("異動", 3);        //取異動最大代號PID
            DataTable DT = ConnectQuery(Sqlstr);
            PID = int.Parse(DT.Rows[0][0].ToString());
            PID += 1;

            Sqlstr = _SqlData.GetData("儲位",81);        //取STORENO儲位代號
            Sqlstr = Sqlstr.Replace("?1", LV1.FocusedItem.SubItems[(int)enLV1Column.料號].Text);
            Sqlstr = Sqlstr.Replace("?2", LV1.FocusedItem.SubItems[(int)enLV1Column.工單單號].Text);
            DataTable DT0 = ConnectQuery(Sqlstr);
            StoreNo = int.Parse(DT0.Rows[0][0].ToString());

            Sqlstr = _SqlData.GetData("異動",4);
            Sqlstr = Sqlstr.Replace("?1", LV1.FocusedItem.SubItems[(int)enLV1Column.工單單號].Text);     //工單單號
            Sqlstr = Sqlstr.Replace("?2", LV1.FocusedItem.SubItems[(int)enLV1Column.料號].Text);
            Sqlstr = Sqlstr.Replace("?3", StoreNo +"");
            DateTimePicker DtpIn = new DateTimePicker();
            DtpIn.Value = DateTime.Now;
            Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID +"");
            Sqlstr = Sqlstr.Replace("?6", 1 +"");
            Sqlstr = Sqlstr.Replace("?7", LV1.FocusedItem.SubItems[(int)enLV1Column.數量].Text);
            Sqlstr = Sqlstr.Replace("?8", "");
            Sqlstr = Sqlstr.Replace("?9", LV1.FocusedItem.SubItems[(int)enLV1Column.機器].Text);
            Sqlstr = Sqlstr.Replace("?01", PID +"");
            Sqlstr = Sqlstr.Replace("?02", LV1.FocusedItem.SubItems[(int)enLV1Column.Location].Text);
            Sqlstr = Sqlstr.Replace("?03", LV1.FocusedItem.SubItems[(int)enLV1Column.庫位].Text);
            Sqlstr = Sqlstr.Replace("?04", "");
            Sqlstr = Sqlstr.Replace("?05", "");
            Sqlstr = Sqlstr.Replace("?06", "");
            Connect(Sqlstr);

            //入庫回去
            Sqlstr = _SqlData.GetData("異動", 4);
            Sqlstr = Sqlstr.Replace("?1", LV1.FocusedItem.SubItems[(int)enLV1Column.工單單號].Text);     //工單單號
            Sqlstr = Sqlstr.Replace("?2", LV1.SelectedItems + "");
            Sqlstr = Sqlstr.Replace("?3", StoreNo + "");
            DtpIn.Value = DateTime.Now;
            Sqlstr = Sqlstr.Replace("?4", DtpIn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            Sqlstr = Sqlstr.Replace("?5", KXMSSysPara.Sys.AutoID + "");
            Sqlstr = Sqlstr.Replace("?6", 0 + "");
            Sqlstr = Sqlstr.Replace("?7", LV1.FocusedItem.SubItems[(int)enLV1Column.數量].Text);
            Sqlstr = Sqlstr.Replace("?8", "");
            Sqlstr = Sqlstr.Replace("?9", LV1.FocusedItem.SubItems[(int)enLV1Column.機器].Text);
            Sqlstr = Sqlstr.Replace("?01", PID + "");
            Sqlstr = Sqlstr.Replace("?02", LV1.FocusedItem.SubItems[(int)enLV1Column.Location].Text);
            Sqlstr = Sqlstr.Replace("?03", LV1.FocusedItem.SubItems[(int)enLV1Column.庫位].Text);
            Sqlstr = Sqlstr.Replace("?04", "");
            Sqlstr = Sqlstr.Replace("?05", "");
            Sqlstr = Sqlstr.Replace("?06", "");

            Sqlstr = _SqlData.GetData("儲位",80);
            Sqlstr = Sqlstr.Replace("?1", txtmodifyNumNo.Text);
            Sqlstr = Sqlstr.Replace("?2", txtMID.Text);
            Connect(Sqlstr);

            labelData.Text = "";
            txtMID.Text = "";
            txtmodify.Text = "";
            txtmodifyNumNo.Text = "";
            LoadLV();
        }

        private void btnExport1_Click(object sender, EventArgs e)
        {
            ExportLV1();
        }

        #endregion

        #region TextBox顏色 及 按下enter觸發事件
        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            switch (Tb.TabIndex)
            {
                case 1:
                    txtNo.BackColor = Color.PaleGreen;
                    break;
                case 2:
                    txtNumNo.BackColor = Color.PaleGreen;
                    break;
                case 5:
                    txtmodify.BackColor = Color.PaleGreen;
                    break;
                case 7:
                    txtmodifyNumNo.BackColor = Color.PaleGreen;
                    break;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox Tb = (TextBox)sender;
            switch (Tb.TabIndex)
            {
                case 1:
                    txtNo.BackColor = Color.White;
                    break;
                case 2:
                    txtNumNo.BackColor = Color.White;
                    break;
                case 5:
                    txtmodify.BackColor = Color.White;
                    break;
                case 7:
                    txtmodifyNumNo.BackColor = Color.White;
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
                        if (txtNo.Text.Length == 0)
                        { }
                        else if (txtNo.Text.Substring(0, 1).ToUpper() == "P")
                        {
                            txtNo.Text = txtNo.Text.Substring(1);
                        }
                        btndemand.PerformClick();
                        break;
                    case 2:
                        if (KXMSSysPara.Sys.WareHouse == 3)
                        {
                            if (txtNumNo.Text.Length == 0)
                            { }
                            else if (txtNumNo.Text.Substring(0, 1).ToUpper() == "V")
                            {
                                txtNumNo.Text = txtNumNo.Text.Substring(1);
                            }
                        }
                        btndemand.PerformClick();
                        break;
                    case 5:
                        btnRemark.PerformClick();
                        break;
                    case 7:
                        btnNub.PerformClick();
                        break;
                }
            }
        }
        #endregion

       
    }
}
