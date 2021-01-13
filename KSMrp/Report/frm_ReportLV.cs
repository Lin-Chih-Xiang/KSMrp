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
using nsKXMSUC;
namespace KSMrp
{
    //enum enLVType
    //{
    //    半成品倉_總庫存_料號 = 101,
    //    總庫存清冊_批號 = 102,
    //    庫存明細 = 103,
    //    低庫存清冊 = 104,
    //    交易查詢 = 201,
    //    交易統計查詢 = 202,
    //    單據查詢 = 301,
    //    儲位使用率 = 401,
    //    儲位屬性清冊 = 402,
    //}
    public partial class frm_ReportLV : Form
    {
        int gbWareHouse;
        string gbSelReportType = "";
        string gbStoreStateFilePath = Application.StartupPath + @"/storestate.htm";
        string _ConnStr = KXMSSysPara.Sys.DBConnStr;
        private ExportService exService;
        SqlData _SqlData;
        public frm_ReportLV(int warehouse)
        {
            InitializeComponent();
            gbWareHouse = warehouse;
            exService = new ExportService();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        }

        private void frm_Report_Load(object sender, EventArgs e)
        {

            //調整顯示項目
            panel1.Height = 130;
            int vStartTop = 3;
            int vStartLeft = 3;
            switch (gbWareHouse)
            {
                case 1:
                    gbx11.Visible = true;
                    gbx11.Top = vStartTop;
                    gbx11.Left = vStartLeft;
                    gbx12.Visible = false;
                    gbx13.Visible = false;

                    gbx21.Visible = true;
                    gbx21.Top = vStartTop;
                    gbx21.Left = vStartLeft;
                    gbx22.Visible = false;
                    gbx23.Visible = false;

                    gbx31.Visible = true;
                    gbx31.Top = vStartTop;
                    gbx31.Left = vStartLeft;
                    gbx32.Visible = false;
                    gbx33.Visible = false;
                    break;
                case 2:

                    gbx11.Visible = false;
                    gbx12.Visible = true;
                    gbx12.Top = vStartTop;
                    gbx12.Left = vStartLeft;
                    gbx13.Visible = false;

                    gbx21.Visible = false;
                    gbx22.Visible = true;
                    gbx22.Top = vStartTop;
                    gbx22.Left = vStartLeft;
                    gbx23.Visible = false;

                    gbx31.Visible = false;
                    gbx32.Visible = true;
                    gbx32.Top = vStartTop;
                    gbx32.Left = vStartLeft;
                    gbx33.Visible = false;
                    break;
                case 3:

                    gbx11.Visible = false;
                    gbx12.Visible = false;
                    gbx13.Visible = true;
                    gbx13.Top = vStartTop;
                    gbx13.Left = vStartLeft;

                    gbx21.Visible = false;
                    gbx22.Visible = false;
                    gbx23.Visible = true;
                    gbx23.Top = vStartTop;
                    gbx23.Left = vStartLeft;

                    gbx31.Visible = false;
                    gbx32.Visible = false;
                    gbx33.Visible = true;
                    gbx33.Top = vStartTop;
                    gbx33.Left = vStartLeft;

                    break;

            }
        }


        //讀檔
        private bool readReportFile(string filename, out string headerStr, out string colStr, out string sqlstr)
        {
            headerStr = "";
            colStr = "";
            sqlstr = "";
            bool vCheck = false;
            //讀取檔案
            try
            {
                string fileData;
                headerStr = "";
                colStr = "";
                sqlstr = "";
                System.IO.StreamReader FSR = new System.IO.StreamReader(filename, System.Text.Encoding.Default);
                fileData = FSR.ReadToEnd();
                fileData = fileData.Replace("\r", "");
                FSR.Close();
                string[] vTFile = fileData.Split('\n');

                switch (vTFile.Length)
                {
                    case 1:
                    case 2:
                        throw new Exception(filename + " 檔案格式不符！\r\n應為：\r\n欄位1,欄位2,欄位3 \r\n col1,col2,col3 \r\n Select col1,col2,col3 from xxx (SQL語法)");

                    case 3:
                        headerStr = vTFile[0];
                        colStr = vTFile[1];
                        sqlstr = vTFile[2];
                        break;
                    default:
                        headerStr = vTFile[0];
                        colStr = vTFile[1];
                        sqlstr = vTFile[2];

                        break;
                }

                //最後檢查欄位長度是否相符
                string[] vHS = headerStr.Split(',');
                string[] vCS = colStr.Split(',');

                if (vHS.Length != vCS.Length)
                {
                    throw new Exception("欄位與col數不一致，請調整");
                }
                vCheck = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                vCheck = false;
            }
            return vCheck;
        }

        private void initLV1(string HeaderStr, string ReportType)
        {
            btnExport3.Enabled = true;
            web1.Visible = false;
            //儲存LV寬度
            if (String.IsNullOrEmpty(gbSelReportType) == false)
            {
                KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, gbSelReportType, LV1.GetColWidth());
            }

            //如果前次查詢與現在查詢一致，保持原樣
            if (ReportType == gbSelReportType) { return; }
            LV1.Clear();
            string[] vHeaderArr = HeaderStr.Split(',');
            HorizontalAlignment vAlign = 0;
            string vHeader = "";

            LV1.Columns.Add("　", 0);
            for (int i = 0; i < vHeaderArr.Length; i++)
            {
                vHeader = vHeaderArr[i].Trim();
                vAlign = HorizontalAlignment.Left;
                if (vHeader.Length > 0)
                {
                    vAlign = (vHeader.Substring(0, 1) == "N") ? HorizontalAlignment.Right : vAlign;
                    vAlign = (vHeader.Substring(0, 1) == "C") ? HorizontalAlignment.Center : vAlign;

                    if (vAlign == HorizontalAlignment.Left) { }
                    else { vHeader = vHeader.Substring(1); }
                }
                LV1.Columns.Add(vHeader, 100, vAlign);
            }
            gbSelReportType = ReportType;
            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, ReportType);
            LV1.SetColWidth(vColStr);
        }

        private void LoadLV1(DataTable DT, string ColStr)
        {
            //重置排序
            LV1.ListViewItemSorter = null;
            LV1.Tag = -1;

            LV1.Items.Clear();
            
            string[] vColArr = ColStr.Split(',');
            foreach (DataRow DR in DT.Rows)
            {
                ListViewItem vItem = new ListViewItem();
                vItem.Text = "";
                for (int i = 0; i < vColArr.Length; i++)
                {
                    vItem.SubItems.Add(DR[vColArr[i].Trim()].ToString());
                }
                LV1.Items.Add(vItem);
            }
            lblLVCount.Text = "共" + LV1.Items.Count + "筆";
        }
        private DataTable LoadDT(string sqlstr)
        {
            DataTable DT = new DataTable();
            try
            {
                OleDbConnection oleConn = new OleDbConnection(_ConnStr);
                OleDbDataAdapter DA = new OleDbDataAdapter(sqlstr, oleConn);
                oleConn.Open();
                DA.Fill(DT);
                oleConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return DT;
        }
        private void btnQ1_Click(object sender, EventArgs e)
        {
            string vHeaderStr;
            string vColStr;
            string sqlstr;
            string vfileName = "";
            string vReportType = "";
            DataTable DT = null;
            switch (gbWareHouse)
            {
                case 1://半成品倉
                    if (rb111.Checked) { vfileName = @"report\AllMno01_T.R"; vReportType = "R111"; }
                    else if (rb112.Checked) { vfileName = @"report\All01_T.R"; vReportType = "R112"; }
                    else if (rb113.Checked) { vfileName = @"report\List01_T.R"; vReportType = "R113"; }
                    else if (rb114.Checked) { vfileName = @"report\ListByCarry01_T.R"; vReportType = "R114"; }
                    else if (rb115.Checked) { vfileName = @"report\ListMno01_T.R"; vReportType = "R115"; };

                    if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                    {

                        if (rb111.Checked) { sqlstr = sqlstr.Replace("?1", 1 + ""); };
                        if (rb112.Checked) { sqlstr = sqlstr.Replace("?1", 1 + ""); };
                        if (rb113.Checked) { sqlstr = sqlstr.Replace("?1", 1 + ""); };
                        if (rb114.Checked) { sqlstr = sqlstr.Replace("?1", 1 + ""); };
                        if (rb115.Checked)
                        {
                            string vPartNo = txtPartNo11.Text.Trim();
                            string vWO = txtWO11.Text.Trim();
                            string vSWhere = "";
                            if (vPartNo.Length > 0)
                            {
                                vSWhere = "and mno='" + vPartNo + "' ";
                            }
                            if (vWO.Length > 0)
                            {
                                vSWhere = "and workorderno='" + vWO + "' ";
                            }
                            sqlstr = sqlstr.Replace("?2", "MachineNo=1 " + vSWhere);
                        }
                        //重置LV1標頭
                        initLV1(vHeaderStr, vReportType);
                        //讀取資料
                        DT = LoadDT(sqlstr);
                        //寫入Table
                        LoadLV1(DT, vColStr);
                    }

                    break;
                case 2: //成品倉
                    if (rb121.Checked) { vfileName = @"report\AllMno02_T.R"; vReportType = "R121"; }
                    else if (rb122.Checked) { vfileName = @"report\All02_T.R"; vReportType = "R122"; }
                    else if (rb123.Checked) { vfileName = @"report\List02_T.R"; vReportType = "R123"; }
                    else if (rb124.Checked) { vfileName = @"report\ListByCarry02_T.R"; vReportType = "R124"; }
                    else if (rb125.Checked) { vfileName = @"report\ListMno02_T.R"; vReportType = "R125"; };

                    if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                    {

                        if (rb121.Checked) { sqlstr = sqlstr.Replace("?1", "2"); };
                        if (rb122.Checked) { sqlstr = sqlstr.Replace("?1", "2"); };
                        if (rb123.Checked) { sqlstr = sqlstr.Replace("?1", "2"); };
                        if (rb124.Checked) { sqlstr = sqlstr.Replace("?1", "2"); };
                        if (rb125.Checked)
                        {
                            string vPartNo = txtPartNo12.Text.Trim();
                            sqlstr = sqlstr.Replace("?2", vPartNo);
                        }
                        //重置LV1標頭
                        initLV1(vHeaderStr, vReportType);
                        //讀取資料
                        DT = LoadDT(sqlstr);
                        //寫入Table
                        LoadLV1(DT, vColStr);
                    }
                    break;
                case 3: //IC倉

                    if (rb131.Checked) { vfileName = @"report\All03_T.R"; vReportType = "R131"; }
                    else if (rb132.Checked) { vfileName = @"report\ListMF03_T.R"; vReportType = "R132"; }
                    if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                    {
                        string vSWhere = "";
                        string vPartNo = txtPartNo13.Text.Trim();
                        string vLotNoF = txtLotNo131.Text.Trim();
                        string vLotNoT = txtLotNo132.Text.Trim();
                        string vMachineStr = "";
                        if (ckbK1.Checked) { vMachineStr += ",1"; }
                        if (ckbK4.Checked) { vMachineStr += ",4"; }
                        if (ckbK5.Checked) { vMachineStr += ",5"; }
                        if (ckbP.Checked) { vMachineStr += ",0"; }
                        if (vMachineStr.Length > 0) { vMachineStr = vMachineStr.Substring(1); vSWhere += " and MachineNo in (" + vMachineStr + ")"; }

                        if (vPartNo.Length > 0)
                        {
                            vSWhere += " and mno like '%" + vPartNo + "%' ";
                        }
                        if (String.IsNullOrEmpty(vLotNoT) && String.IsNullOrEmpty(vLotNoF) == false) { vSWhere += " and OV ='" + vLotNoF + "'"; }
                        else if (String.IsNullOrEmpty(vLotNoF) && String.IsNullOrEmpty(vLotNoT) == false) { vSWhere += " and OV  between '' and '" + vLotNoT + "'"; }
                        else if (String.IsNullOrEmpty(vLotNoT) && String.IsNullOrEmpty(vLotNoF)) { }
                        else { vSWhere += " and OV between '" + vLotNoF + "' and '" + vLotNoT + "'"; }

                        sqlstr = sqlstr.Replace("?3", vSWhere);
                        //重置LV1標頭
                        initLV1(vHeaderStr, vReportType);
                        //讀取資料
                        DT = LoadDT(sqlstr);
                        //寫入Table
                        LoadLV1(DT, vColStr);
                    }
                    break;
            }
        }

        private void btnQ2_Click(object sender, EventArgs e)
        {
            string vHeaderStr;
            string vColStr;
            string sqlstr;
            string vfileName = "";
            string vReportType = "";
            DataTable DT = null;
            switch (gbWareHouse)
            {
                case 1://半成品倉
                    vfileName = @"report\DayChangeD01_T.R";
                    vReportType = "R211";

                    if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                    {

                        sqlstr = sqlstr.Replace("?1", "1"); //TransDevice;
                        sqlstr = sqlstr.Replace("?2", "'" + dtpS21.Value.ToString("yyyy-MM-dd") + " 00:00:00'");
                        sqlstr = sqlstr.Replace("?3", "'" + dtpE21.Value.ToString("yyyy-MM-dd") + " 23:59:59'");

                        string vPartNo = txtPartNo21.Text.Trim();
                        string vSWhere = "";
                        if (vPartNo.Length > 0)
                        {
                            vSWhere = " and mno='" + vPartNo + "' ";
                        }
                        sqlstr = sqlstr.Replace("?4", vSWhere);

                        //重置LV1標頭
                        initLV1(vHeaderStr, vReportType);
                        //讀取資料
                        DT = LoadDT(sqlstr);
                        //寫入Table
                        LoadLV1(DT, vColStr);
                    }

                    break;
                case 2: //成品倉
                    vfileName = @"report\DayChangeD02_T.R";
                    vReportType = "R221";
                    if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                    {

                        sqlstr = sqlstr.Replace("?1", "2"); //TransDevice;
                        sqlstr = sqlstr.Replace("?2", "'" + dtpS22.Value.ToString("yyyy-MM-dd") + " 00:00:00'");
                        sqlstr = sqlstr.Replace("?3", "'" + dtpE22.Value.ToString("yyyy-MM-dd") + " 23:59:59'");

                        string vPartNo = txtPartNo22.Text.Trim();
                        string vSWhere = "";
                        if (vPartNo.Length > 0)
                        {
                            vSWhere = " and finishno='" + vPartNo + "' ";
                        }
                        sqlstr = sqlstr.Replace("?4", vSWhere);
                        //重置LV1標頭
                        initLV1(vHeaderStr, vReportType);
                        //讀取資料
                        DT = LoadDT(sqlstr);
                        //寫入Table
                        LoadLV1(DT, vColStr);
                    }
                    break;
                case 3: //IC倉


                    vfileName = @"report\DayChangeD03_T.R";
                    vReportType = "R231";
                    if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                    {
                        sqlstr = sqlstr.Replace("?1", "1"); //TransDevice;
                        sqlstr = sqlstr.Replace("?2", "'" + dtpS23.Value.ToString("yyyy-MM-dd") + " 00:00:00'");
                        sqlstr = sqlstr.Replace("?3", "'" + dtpE23.Value.ToString("yyyy-MM-dd") + " 23:59:59'");

                        string vPartNo = txtPartNo23.Text.Trim();
                        string vLocation = txtLocation23.Text.Trim();
                        string vSWhere = "";
                        if (vPartNo.Length > 0)
                        {
                            vSWhere = " and mno='" + vPartNo + "' ";
                        }
                        if (vLocation.Length > 0)
                        {
                            if (vLocation != "*")
                            {
                                vSWhere += " and Location='" + vLocation + "' ";
                            }
                        }
                        sqlstr = sqlstr.Replace("?4", vSWhere);

                        //重置LV1標頭
                        initLV1(vHeaderStr, vReportType);
                        //讀取資料
                        DT = LoadDT(sqlstr);
                        //寫入Table
                        LoadLV1(DT, vColStr);
                    }
                    break;
            }
        }

        private void btnQ3_Click(object sender, EventArgs e)
        {
            string vHeaderStr;
            string vColStr;
            string sqlstr;
            string vfileName = "";
            string vReportType = "";
            DataTable DT = null;
            switch (gbWareHouse)
            {
                case 1://半成品倉
                    ShowStoreLayoutState(gbWareHouse, 1);
                    web1.Visible = true;
                    web1.Url = new Uri("file:///" + gbStoreStateFilePath);
                    break;
                case 2: //成品倉
                    if (rb321.Checked)
                    {
                        ShowStoreLayoutState(gbWareHouse, 2);
                        web1.Visible = true;
                        web1.Url = new Uri("file:///" + gbStoreStateFilePath);
                    }
                    else
                    {

                        if (rb322.Checked) { vfileName = @"report\USA_ROC02_T.R"; vReportType = "R322"; }
                        else if (rb323.Checked) { vfileName = @"report\Ground_T.R"; vReportType = "R323"; }

                        if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                        {
                            //重置LV1標頭
                            initLV1(vHeaderStr, vReportType);
                            //讀取資料
                            DT = LoadDT(sqlstr);
                            //寫入Table
                            LoadLV1(DT, vColStr);
                        }
                    }
                    

                    break;
                case 3: //IC倉
                    if (rb331.Checked)
                    {
                        ShowStoreLayoutState(gbWareHouse, 1);
                        web1.Visible = true;
                        web1.Url = new Uri("file:///" + gbStoreStateFilePath);
                    }
                    else if (rb332.Checked)
                    {
                        ShowStoreLayoutState(gbWareHouse, 4);
                        web1.Visible = true;
                        web1.Url = new Uri("file:///" + gbStoreStateFilePath);
                    }
                    else if (rb333.Checked)
                    {
                        ShowStoreLayoutState(gbWareHouse, 5);
                        web1.Visible = true;
                        web1.Url = new Uri("file:///" + gbStoreStateFilePath);
                    }

                    break;
            }
        }

        public int FindEmptyStore(int MType,int vMachineNo)
        {
            string Sqlstr = "";
            int _FindEmptyStore = 0;
            //int vMachineNo = KXMSSysPara.Sys.MachineNo;
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
                case 0:
                    Sqlstr = _SqlData.GetData("庫位",9);
                    Sqlstr = Sqlstr.Replace("?2", vMachineNo + "");
                    break;
            }
            DataTable DT = LoadDT(Sqlstr);
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

        private void ShowStoreLayoutState(int WareHouse, int MachineNo)
        {
            //隱藏匯出按鈕
            btnExport3.Enabled = false;
            string vStr = "";
            string sqlstr = "";

            int vNowCarry = 0;
            int vNowDepth = 0;
            int vNowPos = 0;
            int vCarry = 0;
            int vPos = 0;
            int vDepth = 0;

            int vMqty = 0;
            int vMaxPos = 0;
            int vCheckDepth = 0;
            string vHtmlStr = "";
            string vColor = "";



            const string Green = "#00FF00";
            const string Blue = "#00FFFF";
            const string Yellow = "#FFFF00";

            DataTable DT;
            if (WareHouse == 1 || WareHouse == 2)
            {
                //成品與半成品倉
                vMaxPos = 25;
                sqlstr = "SELECT Store.*, ISNULL(StoreM.MQty,0) as MQty, MMain.Mno, UPCdata.FinishNo, MMain.Location, MMain.WorkOrderNo, MMain.PackageNo, MMain.Mdesc, MMain.StoreHouse, ISNULL(Package.Width,0) AS P_Width, ISNULL(Package.Height,0) AS P_Height, ISNULL(Package.MaxQty,0) AS P_MaxQty ";
                sqlstr += " FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID INNER JOIN Package ON MMain.PackageNo = Package.id RIGHT OUTER JOIN Store ON StoreM.StoreNo = Store.StoreNo LEFT OUTER JOIN UPCdata ON MMain.Mno = UPCdata.UPC ";
                sqlstr += "WHERE (Store.MachineNo=" + MachineNo + ") AND (Store.WareHouse=" + WareHouse + ") ORDER BY Store.MachineNo, Store.Carry, Store.Depth, Store.Pos";
                DT = LoadDT(sqlstr);

                vHtmlStr = "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>";
                vHtmlStr += "<BODY leftmargin=0 topmargin=0 bottommargin=0 rightmargin=0>";
                vHtmlStr += "<TABLE cellSpacing=10 cellPadding=1 border=0>";
                vHtmlStr += "<TR>";
                vHtmlStr += "<TD width=10></TD><TD bgcolor=#ffff00 width=22></TD><TD width=103>=&gt;空儲位</TD><TD bgcolor=#00ff00 width=25></TD>";
                vHtmlStr += "<TD width=122>=&gt;已使用儲位</TD><TD bgcolor=#00ffff width=26></TD><TD width=111>=&gt;未分割儲位</TD></TR></TABLE>";

                vHtmlStr += "<table border=1 width=100%><tr>";
                vHtmlStr += "<td>層</td>";
                for (int i = 1; i <= vMaxPos; i++)
                {
                    vHtmlStr += "<td><FONT size=2>" + (i / 10) + "\r\n" + (i % 10) + "</FONT></td>";
                }

                foreach (DataRow DR in DT.Rows)
                {
                    int.TryParse(DR["Carry"].ToString(), out vNowCarry);
                    int.TryParse(DR["Depth"].ToString(), out vNowDepth);
                    int.TryParse(DR["Pos"].ToString(), out vNowPos);


                    if (vCarry != vNowCarry || vDepth != vNowDepth)
                    {
                        //空後面
                        if (vCarry > 0 && vPos < vMaxPos)
                        {
                            for (int i = vPos; i <= vMaxPos; i++)
                            {
                                vHtmlStr += "<td bgcolor=" + Blue + "></td>";
                            }
                        }
                            if (vCarry != vNowCarry)
                            {
                                vCheckDepth = 1;
                            }
                            else
                            {
                                vCheckDepth = vCheckDepth + 1;
                            }
                            vCarry = vNowCarry;
                            vDepth = vNowDepth;
                            vPos = 1;
                            vHtmlStr += "</tr><tr>";
                            vHtmlStr += "<td align=center><FONT size=2>" + vCarry + "-" + vCheckDepth + "</FONT></td>";

                    }
                    if (vPos != vNowPos)
                    {
                        for (int i = vPos; i < vNowPos; i++)
                        {
                            vHtmlStr += "<td bgcolor=" + Blue + "></td>";
                        }
                        vPos = vNowPos;
                    }
                    vStr = "<td colspan=" + DR["width"].ToString();

                    int.TryParse(DR["MQty"].ToString(), out vMqty);
                    vColor = (vMqty > 0) ? Green : Yellow;
                    vStr += " bgcolor=" + vColor + ">";


                    int vWidth = int.Parse(DR["width"].ToString());
                    int vHeight = int.Parse(DR["height"].ToString());
                    int vP_width = int.Parse(DR["P_width"].ToString());
                    int vP_height = int.Parse(DR["P_height"].ToString());
                    int vP_maxqty = int.Parse(DR["P_maxqty"].ToString());


                    if (vMqty > 0)
                    {
                        if (WareHouse == 1)
                        {
                            vStr += "<FONT size=2>工單:" + DR["WorkOrderNo"].ToString() + "<br>";
                            vStr += DR["Mno"].ToString() + "<br>";
                            vStr += "數量:" + vMqty + "<br>";
                            vStr += "賸餘:" + ((vWidth / vP_width) * (vHeight / vP_height) * vP_maxqty - vMqty) + "<br></FONT>";
                        }
                        else
                        {
                            vStr += "<FONT size=1>" + DR["FinishNo"].ToString() + "<br>";
                            vStr += "數量:" + vMqty + "<br>";
                            vStr += "賸餘:" + ((vWidth / vP_width) * (vHeight / vP_height) * vP_maxqty * KXMSSysPara.Sys.StoreHeight(vNowCarry) - vMqty) + "<br></FONT>";
                        }
                    }
                    else
                    {
                        vStr += "　";
                    }

                    vStr += "</td>";
                    vHtmlStr += vStr;
                    vPos += vWidth;


                }//End foreach DR
                vHtmlStr += "</tr></table></BODY>";
            }
            else
            {
                //IC倉
                if (MachineNo == 1) { vMaxPos = 48; }
                else if (MachineNo == 4) { vMaxPos = 19; }
                else if (MachineNo == 5) { vMaxPos = 17; }
                sqlstr = "SELECT Store.*, StoreM.MQty, MMain.Mno, MMain.Location, MMain.ov, MMain.PackageNo, MMain.Mdesc, MMain.StoreHouse, StoreM.InDate AS Expr1 ";
                sqlstr += " FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID RIGHT OUTER JOIN Store ON StoreM.StoreNo = Store.StoreNo ";
                sqlstr += "WHERE (Store.MachineNo = " + MachineNo + ") AND (Store.WareHouse = 3) ORDER BY Store.MachineNo, Store.Carry, Store.Depth, Store.Pos";

                DT = LoadDT(sqlstr);

                vHtmlStr = "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>";
                vHtmlStr += "<BODY leftmargin=0 topmargin=0 bottommargin=0 rightmargin=0>";
                vHtmlStr += "<TABLE cellSpacing=10 cellPadding=1 border=0>";
                vHtmlStr += "<TR>";
                vHtmlStr += "<TD width=10></TD><TD bgcolor=#ffff00 width=22></TD><TD width=103>=&gt;空儲位</TD><TD bgcolor=#00ff00 width=25></TD>";
                vHtmlStr += "<TD width=122>=&gt;已使用儲位</TD><TD bgcolor=#00ffff width=26></TD><TD width=111>=&gt;未分割儲位</TD>";
                vHtmlStr += "<TD width=234>剩餘捲=&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color= #FF0000>" + FindEmptyStore(23, MachineNo) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;捲</font></TD>";
                vHtmlStr += "<TD width=234>剩餘盤=&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color= #FF0000>" + FindEmptyStore(22,MachineNo) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;盤</font></TD>";
                vHtmlStr += "<TD width=234>總儲位=&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color= #FF0000>" + FindEmptyStore(0, MachineNo) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;個</font></TD></TR></TABLE>";

                vHtmlStr += "<table border=1 width=100%><tr>";
                vHtmlStr += "<td>層</td>";
                for (int i = 1; i <= vMaxPos; i++)
                {
                    vHtmlStr += "<td><FONT size=2>" + (i / 10) + "\r\n" + (i % 10) + "</FONT></td>";
                }
                foreach (DataRow DR in DT.Rows)
                {
                    int.TryParse(DR["Carry"].ToString(), out vNowCarry);
                    int.TryParse(DR["Depth"].ToString(), out vNowDepth);
                    int.TryParse(DR["Pos"].ToString(), out vNowPos);


                    if (vCarry != vNowCarry || vDepth != vNowDepth)
                    {
                        //空後面
                        if (vCarry > 0 && vPos < vMaxPos)
                        {
                            for (int i = vPos; i <= vMaxPos; i++)
                            {
                                vHtmlStr += "<td bgcolor=" + Blue + "></td>";
                            }
                        }
                            if (vCarry != vNowCarry)
                            {
                                vCheckDepth = 1;
                            }
                            else
                            {
                                vCheckDepth = vCheckDepth + 1;
                            }
                            vCarry = vNowCarry;
                            vDepth = vNowDepth;
                            vPos = 1;
                            vHtmlStr += "</tr><tr>";
                            vHtmlStr += "<td align=center><FONT size=2>" + vCarry + "-" + vCheckDepth + "</FONT></td>";

                    }
                    if (vPos != vNowPos)
                    {
                        for (int i = vPos; i < vNowPos; i++)
                        {
                            vHtmlStr += "<td bgcolor=" + Blue + "></td>";
                        }
                        vPos = vNowPos;
                    }
                    vStr = "<td colspan=" + DR["width"].ToString();

                    int.TryParse(DR["MQty"].ToString(), out vMqty);
                    vColor = (vMqty > 0) ? Green : Yellow;
                    vStr += " bgcolor=" + vColor + ">";


                    int vWidth = int.Parse(DR["width"].ToString());
                    int vHeight = int.Parse(DR["height"].ToString());


                    if (vMqty > 0)
                    {

                        vStr += DR["Mno"].ToString() + "<br>";
                        vStr += "L:" + DR["location"].ToString() + "<br>";
                        vStr += "數量:" + vMqty + "<br>";
                        vStr += "O:" + DR["ov"].ToString() + "<br>";

                    }
                    else
                    {
                        vStr += "　";
                    }

                    vStr += "</td>";
                    vHtmlStr += vStr;
                    vPos +=  vWidth;


                }//End foreach DR
                vHtmlStr += "</tr></table></BODY>";
            }

            //存檔

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(gbStoreStateFilePath, false))
            {
                file.WriteLine(vHtmlStr);
            }

        }

        //private int StoreHeight(int Carry)
        //{
        //    if (Carry < 4)
        //    {
        //        return 11;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        private void btnExport1_Click(object sender, EventArgs e)
        {
            ExportLV1();
        }
        private void btnExport2_Click(object sender, EventArgs e)
        {
            ExportLV1();
        }

        private void btnExport3_Click(object sender, EventArgs e)
        {
            ExportLV1();
        }
        private void ExportLV1()
        {
            string ReportName = "";
            switch (gbSelReportType)
            {
                case "R111":
                    ReportName = "自動倉儲庫存表(料號)";
                    break;
                case "R112":
                    ReportName = "自動倉儲庫存表(料號、工單)";
                    break;
                case "R113":
                    ReportName = "自動倉儲庫存狀況表";
                    break;
                case "R114":
                    ReportName = "自動倉儲盤點表";
                    break;
                case "R115":
                    ReportName = "自動倉儲庫存表(料號)";
                    break;
                case "R121":
                    ReportName = "自動倉儲庫存表(料號、Location)";
                    break;
                case "R122":
                    ReportName = "自動倉儲庫存表";
                    break;
                case "R123":
                    ReportName = "自動倉儲庫存狀況表";
                    break;
                case "R124":
                    ReportName = "自動倉儲盤點表";
                    break;
                case "R125":
                    ReportName = "自動倉儲庫存表(料號)";
                    break;
                case "R131":
                    ReportName = "IC倉庫存清冊(合併)";
                    break;
                case "R132":
                    ReportName = "IC倉庫存清冊(明細)";
                    break;
                case "R211":
                    ReportName = "異動清冊";
                    break;
                case "R221":
                    ReportName = "異動清冊";
                    break;
                case "R231":
                    ReportName = "異動清冊";
                    break;
                case "R322":
                    ReportName = "與美國資料比對";
                    break;
                case "R323":
                    ReportName = "平置倉儲位";
                    break;
            }
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
        
    }




}
