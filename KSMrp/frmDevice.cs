using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using nsKXMSUC;

namespace KSMrp
{
    public partial class frmDevice : Form
    {
        SqlData _SqlData;
        public frmDevice()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string sqlstr = "";
            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(sqlstr, oleConn);

            int vMachineNo = 0;
            int vWareHouse = 0;
            int vMaxX = 0;
            int vMaxY = 0;
            int vCarry = 0;
            string vMachineDesc = txtMachineDesc.Text.Trim();
            int vDeviceNo = 0;
            int.TryParse(txtMachineNo.Text, out vMachineNo);
            int.TryParse(txtWareHouse.Text, out vWareHouse);
            int.TryParse(txtMaxX.Text, out vMaxX);
            int.TryParse(txtMaxY.Text, out vMaxY);
            int.TryParse(txtCarry.Text, out vCarry);
            int.TryParse(txtDeviceNum.Text, out vDeviceNo);

            try
            {
                oleConn.Open();
                if (gbID == 0)
                {
                    //新增
                    int vMaxID = 0;

                    DataTable DT = LoadDT(_SqlData.GetData("machineinfor", 7));

                    if (DT.Rows.Count > 0)
                    {
                        int.TryParse(DT.Rows[0][0].ToString(), out vMaxID);
                        vMaxID += 1;
                    }


                    sqlstr = _SqlData.GetData("machineinfor", 3);
                    //id,machineno,machinedesc,maxcarry,devicenum,machinetype,MaxX,MaxY,WareHouse
                    sqlstr = sqlstr.Replace("?1", vMaxID + "");
                    sqlstr = sqlstr.Replace("?2",  vMachineNo + "");
                    sqlstr = sqlstr.Replace("?3", vMachineDesc+ "");
                    sqlstr = sqlstr.Replace("?4", vCarry + "");
                    sqlstr = sqlstr.Replace("?5", vDeviceNo + "");
                    sqlstr = sqlstr.Replace("?6",  "0");
                    sqlstr = sqlstr.Replace("?7", vMaxX+ "");
                    sqlstr = sqlstr.Replace("?8", vMaxY + "");
                    sqlstr = sqlstr.Replace("?9", vWareHouse + "");
                }
                else
                {
                    //修改
                    sqlstr = _SqlData.GetData("machineinfor", 4);
                    sqlstr = sqlstr.Replace("?1", gbID + "");
                    sqlstr = sqlstr.Replace("?2", vMachineNo + "");
                    sqlstr = sqlstr.Replace("?3", vMachineDesc + "");
                    sqlstr = sqlstr.Replace("?4", vCarry + "");
                    sqlstr = sqlstr.Replace("?5", vDeviceNo + "");
                    sqlstr = sqlstr.Replace("?6", "0");
                    sqlstr = sqlstr.Replace("?7", vMaxX + "");
                    sqlstr = sqlstr.Replace("?8", vMaxY + "");
                    sqlstr = sqlstr.Replace("?9", vWareHouse + "");
                }
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }

            LoadLV1();
        }

        private void frmDevice_Load(object sender, EventArgs e)
        {
            initLV1();
            LoadLV1();
        }
        private void initLV1()
        {
            LV1.View = View.Details;
            LV1.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Right);
            LV1.Columns.Add("ID", 0, HorizontalAlignment.Right);
            LV1.Columns.Add("機台",60, HorizontalAlignment.Right);
            LV1.Columns.Add("說明", 60, HorizontalAlignment.Left);
            LV1.Columns.Add("最大層數", 60, HorizontalAlignment.Right);
            LV1.Columns.Add("DeviceNo", 60, HorizontalAlignment.Right);
            LV1.Columns.Add("MaxX", 60, HorizontalAlignment.Right);
            LV1.Columns.Add("MaxY", 60, HorizontalAlignment.Right);
            LV1.Columns.Add("WareHouse", 60, HorizontalAlignment.Right);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);

        }
        private enum enLV1Column
        {
            ID=1,
            機台=2,
            說明 = 3,
            最大層數 = 4,
            DeviceNo=5,
            MaxX = 6,
            MaxY = 7,
            WareHouse = 8,
        }
        private void LoadLV1()
        {
            string Sqlstr = "";
            LV1.Items.Clear();

            Sqlstr = _SqlData.GetData("machineinfor", 6);

            DataTable DT = LoadDT(Sqlstr);
            try
            {
                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();

                    lvitem.Text = "";
                    lvitem.SubItems.Add(DR["ID"].ToString());
                    lvitem.SubItems.Add(DR["MachineNo"].ToString());
                    lvitem.SubItems.Add(DR["machinedesc"].ToString());
                    lvitem.SubItems.Add(DR["MaxCarry"].ToString());
                    lvitem.SubItems.Add(DR["DeviceNum"].ToString());
                    lvitem.SubItems.Add(DR["MaxX"].ToString());
                    lvitem.SubItems.Add(DR["MaxY"].ToString());
                    lvitem.SubItems.Add(DR["WareHouse"].ToString());
                    LV1.Items.Add(lvitem);
                }
                panel2.Enabled = false;
                panel2.BackColor = System.Drawing.Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
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
        int gbID = 0;
        private void LV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel2.BackColor = System.Drawing.Color.Transparent;
            if (LV1.SelectedItems.Count > 0)
            {
                ListViewItem vItem = LV1.SelectedItems[0];
                int.TryParse(vItem.SubItems[(int)enLV1Column.ID].Text, out gbID);
                txtWareHouse.Text = vItem.SubItems[(int)enLV1Column.WareHouse].Text;
                txtMachineNo.Text = vItem.SubItems[(int)enLV1Column.機台].Text;
                txtMachineDesc.Text = vItem.SubItems[(int)enLV1Column.說明].Text;
                txtCarry.Text = vItem.SubItems[(int)enLV1Column.最大層數].Text;
                txtMaxX.Text = vItem.SubItems[(int)enLV1Column.MaxX].Text;
                txtMaxY.Text = vItem.SubItems[(int)enLV1Column.MaxY].Text;
                txtDeviceNum.Text = vItem.SubItems[(int)enLV1Column.DeviceNo].Text;
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            gbID = 0;
            panel2.Enabled = true;
            panel2.BackColor = btnAdd2.BackColor;
        }

        private void btnEdit2_Click(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            panel2.BackColor = btnEdit2.BackColor;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            string sqlstr = "";
            string vMachineNo = txtMachineNo.Text;
            string vWareHouse = txtWareHouse.Text;
            if (gbID == 0) { return; }
            if (KXMSMsgBox.Show("確定刪除 WareHouse："+ vWareHouse +"；機台：" + vMachineNo + " ", "刪除自動倉", "", enMessageType.Question, enMessageButton.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            //檢查倉儲是否物料
            sqlstr = "Select * from StoreM where StoreNo in (Select StoreNo from Store where StoreType=0 and WareHouse=" + vWareHouse + " and MachineNo=" + vMachineNo + ") and MQty>0 ";
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

                //刪除物料
                sqlstr = "DELETE StoreM where StoreNo in (Select StoreNo from Store where StoreType=0 and WareHouse=" + vWareHouse + " and MachineNo=" + vMachineNo + ")";
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();

                //刪除儲位
                sqlstr = "DELETE Store  where StoreType=0 and WareHouse=" + vWareHouse + " and MachineNo=" + vMachineNo + "";
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                    //刪除
                sqlstr = "DELETE Machine Where ID=" + gbID;
                
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                oleConn.Close();
                gbID = 0;
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }
            LoadLV1();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel2.BackColor = System.Drawing.Color.Transparent;
        }
    }
}
