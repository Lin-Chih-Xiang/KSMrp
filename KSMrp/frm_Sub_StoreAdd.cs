using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using nsKXMSUC;

namespace KSMrp
{
    public partial class frm_Sub_StoreAdd : Form
    {
        int gbMachineNo = 0;
        public frm_Sub_StoreAdd(int MachineNo)
        {
            InitializeComponent();
            txtMachineNo.Text = MachineNo+"";
            gbMachineNo = MachineNo;
        }

        private void frmStorePos_Load(object sender, EventArgs e)
        {
        }
       
        private void initCbCarry(int MachineNo)
        {
           
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
            int.TryParse(txtMachineNo.Text, out vMachineNo);
            int.TryParse(txtCarryF.Text, out vCarryF);
            int.TryParse(txtCarryT.Text, out vCarryT);

            if (vCarryF > vCarryT)
            {
                vCarryF = vCarryT;
                int.TryParse(txtCarryF.Text, out vCarryT);

            }
            //檢查新增的盤號是否已存在
            string sqlstr = "";
            sqlstr = "Select * from Store where ";
            sqlstr += "  WareHouse=" + KXMSSysPara.Sys.WareHouse + " and MachineNo = " + vMachineNo + " and (Carry between " + vCarryF + " and " + vCarryT + ") ";
            DataTable DT = LoadDT(sqlstr);
            if (DT.Rows.Count > 0)
            {
                KXMSMsgBox.Show("輸入的盤號區間已有盤號存在！\n請重新輸入！");
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
           
            OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand oleCmd = new OleDbCommand(sqlstr, oleConn);
            try
            {
                oleConn.Open();

                for (int i = vCarryF; i <= vCarryT; i++)
                {
                    sqlstr = "INSERT INTO Store(StoreNo ,StoreType ,MachineNo ,Carry ,Pos ,Depth ,Width ,Height ,MinDate ,MTypeID ,Enabled ,WareHouse ,StoreTypeDesc)";
                    sqlstr += " values( " + vStoreNo + ", 0," + vMachineNo + "," + i + ",1 ,1 ,1 ,1 ,GETDATE() ,0 ,1 ,"+KXMSSysPara.Sys.WareHouse +" ,'')";
                    oleCmd.CommandText = sqlstr;
                    oleCmd.ExecuteNonQuery();

                    vStoreNo += 1;
                }
                    oleConn.Close();

                    KXMSMsgBox.Show("新增成功！", "", "", enMessageType.Success);
                    this.Close();
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("操作失敗！", "", ex.Message, enMessageType.Warning);
            }
        }

        private void txtMachineNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
