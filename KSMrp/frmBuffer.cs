using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace KSMrp
{
    public partial class frmBuffer : Form
    {
        SysPara _sys;
        SqlData _SqlData;
        public frmBuffer()
        {
            InitializeComponent();
            _sys = new SysPara();
            _SqlData = new SqlData(_sys.WareHouse);
        }
        public void PanelStart()
        {
            initLV1();
            this.Top = 0;
            if (KXMSSysPara.Sys.WareHouse == 3)
            {

            }
            else
            {
                label1.Visible = false;
                LV2.Visible = false;
                Command1.Visible = false;
                panel1.Visible = false;
                panel2.Visible = false;
                splitContainer1.Panel2MinSize = 0;
                splitContainer1.SplitterDistance = 800;
                splitContainer1.IsSplitterFixed = true;
            }
        }
        private void frmBuffer_Load(object sender, EventArgs e)
        {
            PanelStart();
            gbLoad = true;
            //timer1.Enabled = true;
            backgroundWorker1.RunWorkerAsync();
        }

        public void SaveLVWidth()
        {
            //KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }
        private void initLV1()
        {
            LV1.Clear();
            LV2.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Left);
            LV2.Columns.Add(" ", 0, HorizontalAlignment.Left);
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                LV1.Columns.Add("料號", 120, HorizontalAlignment.Left);
                LV1.Columns.Add("location", 120, HorizontalAlignment.Left);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("批號", 80, HorizontalAlignment.Left);
                LV1.Columns.Add("層", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("X", 40, HorizontalAlignment.Right);
                LV1.Columns.Add("Y", 40, HorizontalAlignment.Right);
                LV1.Columns.Add("時間", 150, HorizontalAlignment.Left);

                LV2.Columns.Add("料號", 120, HorizontalAlignment.Left);
                LV2.Columns.Add("location", 120, HorizontalAlignment.Left);
                LV2.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV2.Columns.Add("批號", 80, HorizontalAlignment.Left);
                LV2.Columns.Add("位置", 150, HorizontalAlignment.Left);
                LV2.Columns.Add("時間", 150, HorizontalAlignment.Left);
            }
            else
            {
                LV1.Columns.Add("料號", 130, HorizontalAlignment.Left);
                LV1.Columns.Add("數量", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("層", 60, HorizontalAlignment.Right);
                LV1.Columns.Add("X", 40, HorizontalAlignment.Right);
                LV1.Columns.Add("Y", 40, HorizontalAlignment.Right);
                LV1.Columns.Add("時間", 150, HorizontalAlignment.Left);
            }
            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            //LV1.SetColWidth(vColStr);
        }

        private enum enLV1Column : int
        {
            料號 = 1,
            location = 2,
            數量 = 3,
            批號 = 4,
            層 = 5,
            X位置 = 6,
            Y位置 = 7,
            位置 = 8,
        }

        private void Command1_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(Sqlstr, Conn);

            if (MessageBox.Show("確定要清除嗎?" + "\r\n" + "建議平置倉作業完成後再清除", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            Sqlstr = _SqlData.GetData("異動", 12);
            try
            {
                Conn.Open();
                oleCmd.CommandText = Sqlstr;
                oleCmd.ExecuteNonQuery();
                Conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadBuffer();
            timer1.Start();
        }
        public void LoadBuffer()
        {
            LV1.Items.Clear();
            LV2.Items.Clear();
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();
            DataTable DT0 = new DataTable();

            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //取未完成異動單(自動倉)
                Sqlstr = _SqlData.GetData("異動", 10);
                Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.MachineNo + "");
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    foreach (DataRow DR in DT.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR["mno"].ToString());
                        //lvitem.Tag = DR["pid"].ToString();
                        lvitem.SubItems.Add(DR["Location"].ToString());
                        if (DR["TransType"].ToString() == "0")
                        {
                            lvitem.SubItems.Add(DR["TransQty"].ToString());
                        }
                        else if (DR["TransType"].ToString() == "1")
                        {
                            lvitem.SubItems.Add(int.Parse("-" + DR["TransQty"].ToString()) + "");
                        }
                        lvitem.SubItems.Add(DR["ov"].ToString());
                        lvitem.SubItems.Add(DR["Carry"].ToString());
                        lvitem.SubItems.Add(DR["Pos"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR["Depth"].ToString()) + 1 + "");
                        lvitem.SubItems.Add(DR["Transdate"].ToString());
                        LV1.Items.Add(lvitem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //取未完成異動單(平置倉)
                Sqlstr = _SqlData.GetData("異動", 11);
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT0);
                    Conn.Close();

                    foreach (DataRow DR0 in DT0.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR0["mno"].ToString());
                        //lvitem.Tag = DR["pid"].ToString();
                        lvitem.SubItems.Add(DR0["Location"].ToString());
                        if (DR0["TransType"].ToString() == "0")
                        {
                            lvitem.SubItems.Add(DR0["TransQty"].ToString());
                        }
                        else if (DR0["TransType"].ToString() == "1")
                        {
                            lvitem.SubItems.Add(int.Parse("-" + DR0["TransQty"].ToString()) + "");
                        }
                        lvitem.SubItems.Add(DR0["ov"].ToString());
                        lvitem.SubItems.Add(DR0["StoreTypeDesc"].ToString());
                        lvitem.SubItems.Add(DR0["Transdate"].ToString());
                        LV2.Items.Add(lvitem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //取未完成異動單
                Sqlstr = _SqlData.GetData("異動", 6);
                Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    foreach (DataRow DR in DT.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        if (KXMSSysPara.Sys.WareHouse == 1)
                        {
                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR["Mno"].ToString());
                        }
                        else if (KXMSSysPara.Sys.WareHouse == 2)
                        {
                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR["FinishNo"].ToString());
                        }
                        else if (KXMSSysPara.Sys.WareHouse == 3)
                        {
                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR["Mno"].ToString());

                        }


                        if (DR["TransType"].ToString() == "0")
                        {
                            lvitem.SubItems.Add(DR["TransQty"].ToString());
                        }
                        else if (DR["TransType"].ToString() == "1")
                        {
                            lvitem.SubItems.Add(int.Parse("-" + DR["TransQty"].ToString()) + "");
                        }
                        lvitem.SubItems.Add(DR["Carry"].ToString());
                        lvitem.SubItems.Add(DR["Pos"].ToString());
                        lvitem.SubItems.Add((int.Parse(DR["Depth"].ToString()) + 1) + "");
                        lvitem.SubItems.Add(DR["Transdate"].ToString());
                        LV1.Items.Add(lvitem);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        bool gbLoad = false;
        public bool gbCallQuit = false;
        private void btnTimer_Click(object sender, EventArgs e)
        {

            if (gbLoad)
            {
                gbLoad = false;
               // timer1.Enabled = false;
                btnTimer.ImageIndex = 0;
            }
            else
            {
                gbLoad = true;

                //timer1.Enabled = true;
                btnTimer.ImageIndex = 1;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBuffer();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            do
            {
                System.Threading.Thread.Sleep(10000);
                if (gbLoad) { LoadBufferBW(); }
            } while (!gbCallQuit);
        }

        public void LoadBufferBW()
        {

            LV1.Invoke(new Action(() => { LV1.Items.Clear(); }));
            LV2.Invoke(new Action(() => { LV2.Items.Clear(); }));

            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();
            DataTable DT0 = new DataTable();

            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                //取未完成異動單(自動倉)
                Sqlstr = _SqlData.GetData("異動", 10);
                Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.MachineNo + "");
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    foreach (DataRow DR in DT.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR["mno"].ToString());
                        //lvitem.Tag = DR["pid"].ToString();
                        lvitem.SubItems.Add(DR["Location"].ToString());
                        if (DR["TransType"].ToString() == "0")
                        {
                            lvitem.SubItems.Add(DR["TransQty"].ToString());
                        }
                        else if (DR["TransType"].ToString() == "1")
                        {
                            lvitem.SubItems.Add(int.Parse("-" + DR["TransQty"].ToString()) + "");
                        }
                        lvitem.SubItems.Add(DR["ov"].ToString());
                        lvitem.SubItems.Add(DR["Carry"].ToString());
                        lvitem.SubItems.Add(DR["Pos"].ToString());
                        lvitem.SubItems.Add(int.Parse(DR["Depth"].ToString()) + 1 + "");
                        lvitem.SubItems.Add(DR["Transdate"].ToString());
                       

                        LV1.Invoke(new Action(() => { LV1.Items.Add(lvitem); }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //取未完成異動單(平置倉)
                Sqlstr = _SqlData.GetData("異動", 11);
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT0);
                    Conn.Close();

                    foreach (DataRow DR0 in DT0.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        lvitem.Text = "";
                        lvitem.SubItems.Add(DR0["mno"].ToString());
                        //lvitem.Tag = DR["pid"].ToString();
                        lvitem.SubItems.Add(DR0["Location"].ToString());
                        if (DR0["TransType"].ToString() == "0")
                        {
                            lvitem.SubItems.Add(DR0["TransQty"].ToString());
                        }
                        else if (DR0["TransType"].ToString() == "1")
                        {
                            lvitem.SubItems.Add(int.Parse("-" + DR0["TransQty"].ToString()) + "");
                        }
                        lvitem.SubItems.Add(DR0["ov"].ToString());
                        lvitem.SubItems.Add(DR0["StoreTypeDesc"].ToString());
                        lvitem.SubItems.Add(DR0["Transdate"].ToString());
                        LV2.Invoke(new Action(() => { LV2.Items.Add(lvitem); }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //取未完成異動單
                Sqlstr = _SqlData.GetData("異動", 6);
                Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");
                try
                {
                    Conn.Open();
                    DA.SelectCommand.CommandText = Sqlstr;
                    DA.Fill(DT);
                    Conn.Close();

                    foreach (DataRow DR in DT.Rows)
                    {
                        ListViewItem lvitem = new ListViewItem();
                        if (KXMSSysPara.Sys.WareHouse == 1)
                        {
                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR["Mno"].ToString());
                        }
                        else if (KXMSSysPara.Sys.WareHouse == 2)
                        {
                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR["FinishNo"].ToString());
                        }
                        else if (KXMSSysPara.Sys.WareHouse == 3)
                        {
                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR["Mno"].ToString());

                        }


                        if (DR["TransType"].ToString() == "0")
                        {
                            lvitem.SubItems.Add(DR["TransQty"].ToString());
                        }
                        else if (DR["TransType"].ToString() == "1")
                        {
                            lvitem.SubItems.Add(int.Parse("-" + DR["TransQty"].ToString()) + "");
                        }
                        lvitem.SubItems.Add(DR["Carry"].ToString());
                        lvitem.SubItems.Add(DR["Pos"].ToString());
                        lvitem.SubItems.Add((int.Parse(DR["Depth"].ToString()) + 1) + "");
                        lvitem.SubItems.Add(DR["Transdate"].ToString());

                        LV1.Invoke(new Action(() => { LV1.Items.Add(lvitem); }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
