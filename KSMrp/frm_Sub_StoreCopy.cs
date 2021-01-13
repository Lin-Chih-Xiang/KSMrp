using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using nsKXMSUC;

namespace KSMrp
{
    public partial class frm_Sub_StoreCopy : Form
    {
        int gbMachineNo = 0;
        int gbCarry = 0;
        public frm_Sub_StoreCopy(int MachineNo, int Carry)
        {
            InitializeComponent();
            label3.Text = "機台：" + MachineNo + "；盤：" + Carry + "";
            gbMachineNo = MachineNo;
            gbCarry = Carry;
        }

        private void frmStorePos_Load(object sender, EventArgs e)
        {
            initCbMachineNo();
        }
        private void initCbMachineNo()
        {
            cbMachineNo.Items.Clear();
            string sqlstr = "SELECT distinct MachineNo from store where warehouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo >0 Order By MachineNo";
            DataTable DT = LoadDT(sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                cbMachineNo.Items.Add(DR["MachineNo"].ToString());
            }
            if (cbMachineNo.Items.Count > 0)
            {
                cbMachineNo.Text = gbMachineNo+"";
                initCbCarry(int.Parse(cbMachineNo.Text));
            }

        }
        private void initCbCarry(int MachineNo)
        {
            cbCarryF.Items.Clear();
            cbCarryT.Items.Clear();
            string sqlstr = "SELECT distinct Carry from store where warehouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo=" + MachineNo + " Order By Carry";
            DataTable DT = LoadDT(sqlstr);

            foreach (DataRow DR in DT.Rows)
            {
                cbCarryF.Items.Add(DR["Carry"].ToString());
                cbCarryT.Items.Add(DR["Carry"].ToString());
            }
            if (cbCarryF.Items.Count > 0)
            {
                cbCarryF.SelectedIndex = 0;
                cbCarryT.SelectedIndex = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            int vMachineNo = 0;
            int vCarryF = 0;
            int vCarryT = 0;
            int.TryParse(cbMachineNo.Text, out vMachineNo);
            int.TryParse(cbCarryF.Text, out vCarryF);
            int.TryParse(cbCarryT.Text, out vCarryT);

            if (vCarryF > vCarryT)
            {
                vCarryF = vCarryT;
                int.TryParse(cbCarryF.Text, out vCarryT);

            }
            //檢查複製的儲位是否有物料
            //檢查儲位是否物料
            string sqlstr = "";
            sqlstr = "Select * from StoreM where StoreNo in (Select storeno from store ";
            sqlstr += " where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + vMachineNo + " and (Carry between " + vCarryF + " and " + vCarryT + ")) and MQty>0 ";
            DataTable DT = LoadDT(sqlstr);
            if (DT.Rows.Count > 0)
            {
                KXMSMsgBox.Show("規劃的儲位仍有物料，請移除後再規劃！");
                return;
            }
            //讀取StoreNo
            int vStoreNo = 0;
            sqlstr = "SELECT Max(StoreNo) as MaxStoreNo from store"; 
            DataTable DT2 = LoadDT(sqlstr);

            if (DT2.Rows.Count > 0)
            {
                int.TryParse(DT2.Rows[0][0].ToString(), out vStoreNo);
                vStoreNo += 1;
            }
            int vFirstStoreNo = 0;
            int vTotalStoreNo = 0;
            sqlstr = "SELECT Min(StoreNo) as MinStoreNo,Count(StoreNo) as TotalStoreNo  from store where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + gbMachineNo + " and Carry=" + gbCarry;
            DataTable DT3= LoadDT(sqlstr);

            if (DT3.Rows.Count > 0)
            {
                int.TryParse(DT3.Rows[0][0].ToString(), out vFirstStoreNo);
                int.TryParse(DT3.Rows[0][1].ToString(), out vTotalStoreNo);
                
            }
            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(sqlstr, oleConn);
            try
            {
                oleConn.Open();
                //刪除物料
                sqlstr = "DELETE StoreM Where StoreNo in (Select storeno from store ";
                if (gbMachineNo == vMachineNo)
                {
                    sqlstr += " where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + vMachineNo + " and (Carry between " + vCarryF + " and " + vCarryT + ") and Carry != " + gbCarry + ") ";
                }
                else
                {
                    sqlstr += " where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + vMachineNo + " and (Carry between " + vCarryF + " and " + vCarryT + ")) ";
                }
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();
                
                //刪除原配置 (排除選擇的儲位)
                sqlstr = "DELETE Store Where StoreNo in (Select storeno from store ";
                if (gbMachineNo == vMachineNo)
                {
                    sqlstr += " where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + vMachineNo + " and (Carry between " + vCarryF + " and " + vCarryT + ") and Carry != "+ gbCarry +") ";
                }
                else
                {
                    sqlstr += " where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + vMachineNo + " and (Carry between " + vCarryF + " and " + vCarryT + ")) ";
                }
                oleCmd.CommandText = sqlstr;
                oleCmd.ExecuteNonQuery();


                for (int i = vCarryF; i <= vCarryT; i++)
                {
                    if (gbMachineNo == vMachineNo && gbCarry == i) { continue; }
                    sqlstr = "INSERT INTO Store(StoreNo ,StoreType ,MachineNo ,Carry ,Pos ,Depth ,Width ,Height ,MinDate ,MTypeID ,Enabled ,WareHouse ,StoreTypeDesc)";
                    sqlstr += " Select " + vStoreNo + " + (StoreNo-" + vFirstStoreNo + ") ,StoreType," + vMachineNo + "," + i + ",Pos ,Depth ,Width ,Height ,MinDate ,MTypeID ,Enabled ,WareHouse ,StoreTypeDesc";
                    sqlstr += " from Store where WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + gbMachineNo + " and Carry=" + gbCarry;
                    oleCmd.CommandText = sqlstr;
                    oleCmd.ExecuteNonQuery();

                    vStoreNo += vTotalStoreNo;
                }
                    oleConn.Close();

                    KXMSMsgBox.Show("複製規劃成功！", "", "", enMessageType.Success);
                    this.Close();
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }
        }
    }
}
