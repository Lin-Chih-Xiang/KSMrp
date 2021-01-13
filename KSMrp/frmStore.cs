using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using nsKXMSUC;

namespace KSMrp
{
    public partial class frmStore : Form
    {
        SqlData _SqlData;
        public frmStore()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }




        private void frmStore_Load(object sender, EventArgs e)
        {
            initLV1();
            initLV2();
            initCbMachineNo();
            LoadLV();
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                txtMTypeID.Visible = true;
                label6.Visible = label7.Visible = true;
            }
            else
            {
                txtMTypeID.Visible = false;
                label6.Visible = label7.Visible = false;

            }
        }

        private void frmStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
            this.Hide();
            e.Cancel = true;
        }

        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }
        private void initLV1()
        {
            LV1.View = View.Details;
            LV1.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LV1.Columns.Add("平置倉名稱", 230, HorizontalAlignment.Center);
            LV1.Columns.Add("StoreNo", 0, HorizontalAlignment.Right);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private void initLV2()
        {
            LV2.View = View.Details;
            LV2.Clear();
            LV2.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LV2.Columns.Add("ID", 0, HorizontalAlignment.Right);
            LV2.Columns.Add("StoreNo", 0, HorizontalAlignment.Right);
            LV2.Columns.Add("機台", 0, HorizontalAlignment.Right);
            LV2.Columns.Add("盤號", 0, HorizontalAlignment.Right);
            LV2.Columns.Add("橫向", 60, HorizontalAlignment.Right);
            LV2.Columns.Add("縱向", 60, HorizontalAlignment.Right);
            LV2.Columns.Add("寬", 60, HorizontalAlignment.Right);
            LV2.Columns.Add("深", 60, HorizontalAlignment.Right);
            LV2.Columns.Add("MTypeID", 60, HorizontalAlignment.Right);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV2.Name);
            LV2.SetColWidth(vColStr);

        }

        private enum enLV1Column : int
        {
            庫位說明 = 1,
            StoreNo = 2,
        }
        private enum enLV2Column : int
        {
            ID = 1,
            StoreNo = 2,
            機台 = 3,
            盤號 = 4,
            橫向 = 5,
            縱向 = 6,
            寬 = 7,
            深 = 8,
            MTypeID = 9,
        }
        private void initCbMachineNo()
        {
            cbMachineNo.Items.Clear();
            string sqlstr = "SELECT distinct MachineNo from Machine where warehouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo >0 Order By MachineNo";
            DataTable DT = LoadDT(sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                cbMachineNo.Items.Add(DR["MachineNo"].ToString());
            }
            if (cbMachineNo.Items.Count > 0)
            {
                cbMachineNo.SelectedIndex = 0;
                initCbCarry(int.Parse(cbMachineNo.Text));
            }

        }
        private void initCbCarry(int MachineNo)
        {
            cbCarry.Items.Clear();
            string sqlstr = "SELECT distinct Carry from store where warehouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo=" + MachineNo + " Order By Carry";
            DataTable DT = LoadDT(sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                cbCarry.Items.Add(DR["Carry"].ToString());
            }
            if (cbCarry.Items.Count > 0)
            {
                cbCarry.SelectedIndex = 0;
            }
        }

        #region "平置倉"
        private void LoadLV()
        {
            string Sqlstr = "";
            LV1.Items.Clear();

            Sqlstr = _SqlData.GetData("SQLofStore", 9);
            Sqlstr = Sqlstr.Replace("?1", KXMSSysPara.Sys.WareHouse + "");

            DataTable DT = LoadDT(Sqlstr);
            try
            {
                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();

                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR["StoreTypeDesc"].ToString());
                    lvitem.SubItems.Add(DR["StoreNo"].ToString());
                    LV1.Items.Add(lvitem);
                }
                txtStoreTypeDesc.Text = "";
                txtStoreNo.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbDataAdapter DA = new OleDbDataAdapter(Sqlstr, Conn);
            DataTable DT = new DataTable();
            Sqlstr = _SqlData.GetData("SQLofStore", 1);
            Conn.Open();
            DA.SelectCommand.CommandText = Sqlstr;
            DA.Fill(DT);
            Conn.Close();
            int S = int.Parse(DT.Rows[0][0].ToString());
            S += 1;

            OleDbCommand oleCmd = new OleDbCommand("", Conn);
            Sqlstr = _SqlData.GetData("SQLofStore", 7);
            Sqlstr = Sqlstr.Replace("?1", S + "");
            Sqlstr = Sqlstr.Replace("?2", KXMSSysPara.Sys.WareHouse + "");
            Sqlstr = Sqlstr.Replace("?3", txtStoreTypeDesc.Text);
            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();

            txtStoreTypeDesc.Text = "";
            txtStoreNo.Text = "0";
            LoadLV();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);

            OleDbCommand oleCmd = new OleDbCommand("", Conn);
            Sqlstr = _SqlData.GetData("SQLofStore", 10);
            Sqlstr = Sqlstr.Replace("?1", txtStoreNo.Text + "");
            Sqlstr = Sqlstr.Replace("?3", txtStoreTypeDesc.Text);
            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();

            txtStoreTypeDesc.Text = "";
            txtStoreNo.Text = "0";
            LoadLV();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sqlstr = "";

            //檢查儲位是否物料
            sqlstr = "Select * from StoreM where StoreNo=" + gbSelStoreNo + " and MQty>0 ";
            DataTable DT = LoadDT(sqlstr);
            if (DT.Rows.Count > 0)
            {
                KXMSMsgBox.Show("儲位仍有物料，不能刪除！");
                return;
            }

            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand("", Conn);
            int vID = 0;
            int.TryParse(txtStoreNo.Text, out vID);
            if (vID <= 0) { return; }
            if (MessageBox.Show("確定刪除[" + txtStoreTypeDesc.Text + "]？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (txtStoreNo.Text.Length < 1) { return; }
                sqlstr = _SqlData.GetData("SQLofStore", 8);
                sqlstr = sqlstr.Replace("?1", txtStoreNo.Text);
                Conn.Open();
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                Conn.Close();
            }

            txtStoreTypeDesc.Text = "";
            txtStoreNo.Text = "0";
            LoadLV();
        }
        private void LV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV1.SelectedItems.Count > 0)
            {
                txtStoreTypeDesc.Text = LV1.SelectedItems[0].SubItems[(int)enLV1Column.庫位說明].Text;
                txtStoreNo.Text = LV1.SelectedItems[0].SubItems[(int)enLV1Column.StoreNo].Text;
            }
            else
            {
                txtStoreTypeDesc.Text = "";
                txtStoreNo.Text = "0";
            }
        }

        #endregion
        private DataTable LoadDT(string sqlstr)
        {
            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            DataTable DT = new DataTable();
            OleDbDataAdapter DA = new OleDbDataAdapter(sqlstr, oleConn);
            try
            {
                oleConn.Open();
                DA.Fill(DT);
                oleConn.Close();
                DA.Dispose();
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("讀取失敗\n" + ex.Message, "", sqlstr, enMessageType.Warning);
            }
            return DT;
        }
        #region "自動倉"


        private void btndelete2_Click(object sender, EventArgs e)
        {
            string sqlstr = "";

            if (gbSelStoreNo == 0) { return; }
            //檢查儲位是否物料
            sqlstr = "Select * from StoreM where StoreNo=" + gbSelStoreNo + " and MQty>0 ";
            DataTable DT = LoadDT(sqlstr);
            if (DT.Rows.Count > 0)
            {
                KXMSMsgBox.Show("儲位仍有物料，不能刪除！");
                return;
            }



            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(sqlstr, oleConn);
            try
            {
                oleConn.Open();
                if (gbSelStoreNo > 0)
                {
                    //刪除
                    sqlstr = "DELETE Store Where StoreNo=" + gbSelStoreNo;
                }
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                sqlstr = "DELETE StoreM Where StoreNo=" + gbSelStoreNo;
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                oleConn.Close();
                gbSelStoreNo = 0;
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }
            LoadLV2();
        }

        private void tslAddStore_Click(object sender, EventArgs e)
        {
            frmDevice vfrm = new frmDevice();
            vfrm.ShowDialog();
            initCbMachineNo();
        }


        
        private void cbMachineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vMachineNo = 0;
            int.TryParse(cbMachineNo.Text, out vMachineNo);
            initCbCarry(vMachineNo);
        }

        private void cbCarry_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadLV2();
        }
        private void LoadLV2()
        {
            int vMachineNo = 0;
            int vCarry = 0;
            int.TryParse(cbMachineNo.Text, out vMachineNo);
            int.TryParse(cbCarry.Text, out vCarry);
            LV2.Items.Clear();
            string sqlstr = "select ID,storeNo,MachineNo,Carry,Pos,Depth,Width,Height,ISNULL(MtypeID,0) as MTypeID from store where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo=" + vMachineNo + " and Carry =" + vCarry;
            sqlstr += " Order By Pos,Depth";
            DataTable DT = LoadDT(sqlstr);
            try
            {
                //ID = 1,
                //StoreNo = 2,
                //機台 = 3,
                //盤號 = 4,
                //橫向 = 5,
                //縱向 = 6,
                //寬 = 7,
                //深 = 8,
                //MTypeID = 9,


                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();

                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR["ID"].ToString());
                    lvitem.SubItems.Add(DR["StoreNo"].ToString());
                    lvitem.SubItems.Add(DR["MachineNo"].ToString());
                    lvitem.SubItems.Add(DR["Carry"].ToString());
                    lvitem.SubItems.Add(DR["Pos"].ToString());
                    lvitem.SubItems.Add(DR["Depth"].ToString());
                    lvitem.SubItems.Add(DR["Width"].ToString());
                    lvitem.SubItems.Add(DR["Height"].ToString());
                    lvitem.SubItems.Add(DR["MtypeID"].ToString());
                    LV2.Items.Add(lvitem);
                }
                panel2.Enabled = false;
                panel2.BackColor = System.Drawing.Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LV2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel2.BackColor = System.Drawing.Color.Transparent;
            if (LV2.SelectedItems.Count > 0)
            {
                ListViewItem vItem = LV2.SelectedItems[0];
                int.TryParse(vItem.SubItems[(int)enLV2Column.StoreNo].Text, out gbSelStoreNo);
                txtPos.Text = vItem.SubItems[(int)enLV2Column.橫向].Text;
                txtDepth.Text = vItem.SubItems[(int)enLV2Column.縱向].Text;
                txtWidth.Text = vItem.SubItems[(int)enLV2Column.寬].Text;
                txtHeight.Text = vItem.SubItems[(int)enLV2Column.深].Text;
                txtMTypeID.Text = vItem.SubItems[(int)enLV2Column.MTypeID].Text;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            string sqlstr = "";


            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(sqlstr, oleConn);
            try
            {
                oleConn.Open();
                if (gbSelStoreNo == 0)
                {
                    //新增
                    int vStoreNo = 0;

                    DataTable DT = LoadDT(_SqlData.GetData("SQLofStore", 1));

                    if (DT.Rows.Count > 0)
                    {
                        int.TryParse(DT.Rows[0][0].ToString(), out vStoreNo);
                        vStoreNo += 1;
                    }


                    sqlstr = "INSERT INTO Store(StoreNo ,StoreType ,MachineNo ,Carry ,Pos ,Depth ,Width ,Height ,MinDate ,MTypeID ,Enabled ,WareHouse ,StoreTypeDesc)";
                    sqlstr = sqlstr + " Values (?2,0,?3,?4,?5,?6,?7,?8,GETDATE(),?9,1," + KXMSSysPara.Sys.WareHouse + ",'')";
                    sqlstr = sqlstr.Replace("?2", vStoreNo + "");
                    sqlstr = sqlstr.Replace("?3", cbMachineNo.Text + "");
                    sqlstr = sqlstr.Replace("?4", cbCarry.Text + "");
                    sqlstr = sqlstr.Replace("?5", txtPos.Text + "");
                    sqlstr = sqlstr.Replace("?6", txtDepth.Text + "");
                    sqlstr = sqlstr.Replace("?7", txtWidth.Text + "");
                    sqlstr = sqlstr.Replace("?8", txtHeight.Text + "");
                    sqlstr = sqlstr.Replace("?9", txtMTypeID.Text + "");
                }
                else
                {
                    //修改
                    sqlstr = "UPDATE Store SET Pos=?5,Depth=?6,Width=?7,Height=?8,MTypeID=?9";
                    sqlstr = sqlstr + " Where StoreNo=?1";
                    sqlstr = sqlstr.Replace("?1", gbSelStoreNo + "");
                    sqlstr = sqlstr.Replace("?5", txtPos.Text + "");
                    sqlstr = sqlstr.Replace("?6", txtDepth.Text + "");
                    sqlstr = sqlstr.Replace("?7", txtWidth.Text + "");
                    sqlstr = sqlstr.Replace("?8", txtHeight.Text + "");
                    sqlstr = sqlstr.Replace("?9", txtMTypeID.Text + "");
                }
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }

            LoadLV2();
        }

        int gbSelStoreNo;
        private void btnAdd2_Click(object sender, EventArgs e)
        {
            gbSelStoreNo = 0;
            panel2.Enabled = true;
            panel2.BackColor = btnAdd2.BackColor;

        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel2.BackColor = btnEdit2.BackColor;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel2.BackColor = System.Drawing.Color.Transparent;
        }

        private void btnDelCarry_Click(object sender, EventArgs e)
        {
            string sqlstr = "";

            int vMachineNo = 0;
            int vCarry = 0;
            int.TryParse(cbMachineNo.Text,out vMachineNo);
            int.TryParse(cbCarry.Text,out vCarry);

            if (vMachineNo==0|| vCarry==0){ KXMSMsgBox.Show("請選擇要刪除的盤號"); return;}
            if(KXMSMsgBox.Show("確定刪除第"+vMachineNo +"台 第"+vCarry+"盤？","刪除料盤","", enMessageType.Question, enMessageButton.OKCancel)== System.Windows.Forms.DialogResult.Cancel){
                return;
            }

            //檢查儲位是否物料
            sqlstr = "Select * from StoreM where StoreNo in (Select StoreNo from Store where WareHouse="+ KXMSSysPara.Sys.WareHouse + " and MachineNo=" +vMachineNo + " and Carry="+vCarry +") and MQty>0 ";
            DataTable DT = LoadDT(sqlstr);
            if (DT.Rows.Count > 0)
            {
                KXMSMsgBox.Show("此盤仍有物料，不能刪除！");
                return;
            }

            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(sqlstr, oleConn);
            try
            {
                oleConn.Open();
                
                    //刪除
                    sqlstr = "DELETE Store Where StoreNo in  (Select StoreNo from Store where WareHouse="+ KXMSSysPara.Sys.WareHouse + " and MachineNo=" +vMachineNo + " and Carry="+vCarry +")";
             
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                sqlstr = "DELETE StoreM Where StoreNo in  (Select StoreNo from Store where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo=" + vMachineNo + " and Carry=" + vCarry + ")";
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                oleConn.Close();
                gbSelStoreNo = 0;
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }
            initCbCarry(vMachineNo);
        }

        private void btnCopyCarry_Click(object sender, EventArgs e)
        {
            int vMachineNo = 0;
            int vCarry = 0;
            int.TryParse(cbMachineNo.Text,out vMachineNo);
            int.TryParse(cbCarry.Text,out vCarry);
            if (vMachineNo==0|| vCarry==0){ KXMSMsgBox.Show("請選擇要複製的盤號"); return;}
            frm_Sub_StoreCopy vfrm = new frm_Sub_StoreCopy(vMachineNo, vCarry);
            vfrm.ShowDialog();
        }

        private void btnAddCarry_Click(object sender, EventArgs e)
        {
            int vMachineNo = 0;
            int.TryParse(cbMachineNo.Text, out vMachineNo);
            if (vMachineNo == 0 ) { KXMSMsgBox.Show("請選擇機台"); return; }
            frm_Sub_StoreAdd vfrm = new frm_Sub_StoreAdd(vMachineNo);
            vfrm.ShowDialog();
            initCbCarry(vMachineNo);
        }


        #endregion
    }
}
