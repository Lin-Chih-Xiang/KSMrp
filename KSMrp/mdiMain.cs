using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using nsKXMSUC;


namespace KSMrp
{
    public partial class mdiMain : Form
    {
        SqlData _SqlData;
        clsUI _UI;
        public bool isReLogin = false;

        public mdiMain()
        {
            InitializeComponent();
            _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
            _UI = new clsUI();
        }

        private void mdiMain_Load(object sender, EventArgs e)
        {
            //檢查更新END
            foreach (Control ctl in this.Controls.OfType<MdiClient>())          //mdi顏色的修改
            {
                ctl.BackColor = Color.MidnightBlue;
            }
            timer1.Enabled = true;

            this.Visible = false;
            frmLogin _frmLogin = new frmLogin();
            _frmLogin.ShowDialog();

            if (_frmLogin.DialogResult == DialogResult.OK)
            {
                //跳出主視窗
                this.Visible = true;
                if (_frmBuffer == null) { _frmBuffer = new frmBufferVM(); }
                _frmBuffer.MdiParent = this;
                _frmBuffer.Show();
                _frmBuffer.Width = 0;
                setFrmBufferSize();

                CheckMenuPower();
            }
            else
            {
                this.Close();
            }

            if (KXMSSysPara.Sys.UID == "Hewtech")
            {
                toolStripStatusLabel4.Text = KXMSSysPara.Sys.UID;
            }
            else
            {
                toolStripStatusLabel4.Text = KXMSSysPara.Sys.UID;
            }
            //開啟HewCtrl

            switch (KXMSSysPara.Sys.WareHouse)
            {
                case 1:
                case 2:
                    if (KXMSSysPara.Sys.MachineNo > 0)
                    {
                        //開啟HewCtrl
                        System.Diagnostics.Process[] vHewCtrlProcess = System.Diagnostics.Process.GetProcessesByName("HewCtrl");
                        if (vHewCtrlProcess.Length == 0)
                        {
                            System.Diagnostics.Process.Start(Application.StartupPath + @"\HewCtrl.exe");
                        }
                    }
                    break;
                case 3:
                    if (KXMSSysPara.Sys.MachineNo == 1)
                    {
                        //開啟HewCtrl
                        System.Diagnostics.Process[] vHewCtrlProcess = System.Diagnostics.Process.GetProcessesByName("HewCtrl");
                        if (vHewCtrlProcess.Length == 0)
                        {
                            System.Diagnostics.Process.Start(Application.StartupPath + @"\HewCtrl.exe");
                        }
                    }
                    else if (KXMSSysPara.Sys.MachineNo == 4 || KXMSSysPara.Sys.MachineNo == 5)
                    {
                        //開啟HewCtrlC3
                        System.Diagnostics.Process[] vHewCtrlProcess = System.Diagnostics.Process.GetProcessesByName("HewCtrl_v3");
                        if (vHewCtrlProcess.Length == 0)
                        {
                            System.Diagnostics.Process.Start(Application.StartupPath + @"\HewCtrl_v3.exe");
                        }
                    }
                    break;
            }
            
            //CheckMenuPower();
        }

        private void mdiMain_Activated(object sender, EventArgs e)
        {
            if (KXMSSysPara.Sys.UID.Length > 0)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            toolStripStatusLabel3.Text = dt.ToString();
        }

        #region 權限(0->關 1->開)
        private void CheckMenuPower()
        {
            string S = "";
            //[入庫]"TbIn"
            //[批次入庫]"TbIns"
            //[領料]"TbOut"
            //[批次領料]"TbOuts"
            //[物料中心]新增比對資料"TbUPCdata"
            //[物料中心]盤點調整"TbAdj"
            //[物料中心]物料查詢(by料號)"TbQMno"
            //[物料中心]成品倉庫資料比對 (X-不使用了)
            //[報表中心]"TbReport"
            //[系統設定]使用者設定"TbUser"
            //[系統設定]儲位管理"TbStore"
            //[系統設定]新增包裝"TbPackage"
            //[系統設定]新增庫位"TbStoreHouse"
            //[系統設定]錯誤資料維護"TbError"

            //[物料中心]平置倉儲位設定"TbStorePos"
            //[物料中心]平置倉儲位查詢"TbQPos"
            S = KXMSSysPara.Sys.Power;
            string vVerNo = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            switch (KXMSSysPara.Sys.IsTest)
            {
                case 0:
                    this.Text = "自動倉儲管理系統 v" + vVerNo + " - 正常模式";
                    TbIn.Visible = int.Parse(S.Substring(0, 1)) > 0;
                    TbOut.Visible = int.Parse(S.Substring(1, 1)) > 0;
                    break;
                case 2:
                    this.Text = "自動倉儲管理系統 v" + vVerNo + " - 測試模式";
                    TbIn.Visible = int.Parse(S.Substring(0, 1)) > 0;
                    TbOut.Visible = int.Parse(S.Substring(1, 1)) > 0;
                    break;
                case 1:
                    this.Text = "自動倉儲管理系統 v" + vVerNo + " - 查詢模式";
                    TbIn.Visible = false;
                    TbOut.Visible = false;
                    break;
            }

            TbAdj.Visible = int.Parse(S.Substring(3, 1)) > 0;
            TbQMno.Visible = int.Parse(S.Substring(4, 1)) > 0;
            if (KXMSSysPara.Sys.WareHouse == 2)
            {
                TbUPCdata.Visible = int.Parse(S.Substring(2, 1)) > 0;
                TbStorePos.Visible = true;
                TbQPos.Visible = true;
            }
            else
            {
                TbUPCdata.Visible = false;
                TbStorePos.Visible = false;
                TbQPos.Visible = false;
            }

            TbReport.Visible = int.Parse(S.Substring(6, 1)) > 0;
            TbUser.Visible = int.Parse(S.Substring(7, 1)) > 0;
            TbStore.Visible = int.Parse(S.Substring(8, 1)) > 0;
            TbPackage.Visible = int.Parse(S.Substring(9, 1)) > 0;
            TbStoreHouse.Visible = int.Parse(S.Substring(10, 1)) > 0;
            if (S.Length < 12)
            { TbError.Visible = false; }
            else { TbError.Visible = int.Parse(S.Substring(11, 1)) > 0; }
            if (KXMSSysPara.Sys.WareHouse == 3)
            {
                if (S.Length == 13) { TbIns.Visible = int.Parse(S.Substring(12, 1)) > 0; }
                else { TbIns.Visible = false; }
                if (S.Length == 14) { TbShelf.Visible = int.Parse(S.Substring(13, 1)) > 0; }
                else { TbShelf.Visible = false; }
            }
            else { TbIns.Visible = false; TbShelf.Visible = false; }

            //TbOuts.Visible = int.Parse(S.Substring(13, 1)) > 0;
        }
        #endregion

        #region ToolStrip Button設定
        frmBufferVM _frmBuffer = new frmBufferVM();
        frmInM _frmInM;
        frmOutM _frmOutM;
        MsgBox _msgBox;
        frmAdjM _frmAdjM;
        frmQMno _frmQMno;
        frmAbout _frmAbout;
        frm_ReportLV _frmReport;
        frmUser _frmUser;
        frmStore _frmStore;
        frmPassword _frmPassword;
        frmPackage _frmPackage;
        frmStoreHouse _frmStoreHouse;
        frmUPCdata _frmUPCdata;
        frmStorePos _frmStorePos;
        frmQPos _frmQPos;
        frm_ExcelImport _frm_ExcelImport;
        frm_StoreMultiIn _frm_StoreMultiIn;
        frmShelfLife _frmShelfLife;
        public void newMDIChild(string i)
        {
            switch (i)
            {
                case "入庫作業":
                    //如果是空的，會建立一個新視窗
                    if (_frmInM == null) { _frmInM = new frmInM(); }
                    //判斷如果form被處置後，建立新的from
                    if (_frmInM.IsDisposed == true) { _frmInM = new frmInM(); }
                    // Set the Parent Form of the Child window.
                    _frmInM.MdiParent = this;
                    // Display the new form.
                    _frmInM.Show();
                    //點選到視窗會跳到最前面
                    _frmInM.Activate();
                    break;
                case "批次入庫":
                    if (_frm_StoreMultiIn == null) { _frm_StoreMultiIn = new frm_StoreMultiIn(); }
                    if (_frm_StoreMultiIn.IsDisposed == true) { _frm_StoreMultiIn = new frm_StoreMultiIn(); }
                    _frm_StoreMultiIn.MdiParent = this;
                    _frm_StoreMultiIn.Show();
                    _frm_StoreMultiIn.Activate();
                    break;
                case "領料作業":
                    if (_frmOutM == null) { _frmOutM = new frmOutM(); }
                    if (_frmOutM.IsDisposed == true) { _frmOutM = new frmOutM(); }
                    _frmOutM.MdiParent = this;
                    _frmOutM.Show();
                    _frmOutM.Activate();
                    break;
                case "盤點調整":
                    if (_frmAdjM == null) { _frmAdjM = new frmAdjM(); }
                    if (_frmAdjM.IsDisposed == true) { _frmAdjM = new frmAdjM(); }
                    _frmAdjM.MdiParent = this;
                    _frmAdjM.Show();
                    _frmAdjM.Activate();
                    break;
                case "物料查詢":
                    if (_frmQMno == null) { _frmQMno = new frmQMno(); }
                    if (_frmQMno.IsDisposed == true) { _frmQMno = new frmQMno(); }
                    _frmQMno.MdiParent = this;
                    _frmQMno.Show();
                    _frmQMno.Activate();
                    break;
                case "報表中心":
                    if (_frmReport == null) { _frmReport = new frm_ReportLV(KXMSSysPara.Sys.WareHouse); }
                    if (_frmReport.IsDisposed == true) { _frmReport = new frm_ReportLV(KXMSSysPara.Sys.WareHouse); }
                    _frmReport.MdiParent = this;
                    _frmReport.StartPosition = FormStartPosition.Manual;
                    _frmReport.Top = 30;
                    _frmReport.Left = 30;

                    if (this.Width - 200 > 1024)
                    {
                        _frmReport.Width = this.Width - 200;
                    }
                    else
                    {
                        _frmReport.Width = 1024;
                    }
                    if (this.Height - 150 > 700)
                    {
                        _frmReport.Height = this.Height - 150;
                    }
                    else
                    {
                        _frmReport.Height = 700;
                    }
                    _frmReport.Show();
                    _frmReport.Activate();
                    break;
                case "使用者設定":
                    if (_frmUser == null) { _frmUser = new frmUser(); }
                    if (_frmUser.IsDisposed == true) { _frmUser = new frmUser(); }
                    _frmUser.MdiParent = this;
                    _frmUser.Show();
                    _frmUser.Activate();
                    break;
                case "儲位管理":
                    if (_frmStore == null) { _frmStore = new frmStore(); }
                    if (_frmStore.IsDisposed == true) { _frmStore = new frmStore(); }
                    _frmStore.MdiParent = this;
                    _frmStore.Show();
                    _frmStore.Activate();
                    break;

                case "修改密碼":
                    if (_frmPassword == null) { _frmPassword = new frmPassword(); }
                    if (_frmPassword.IsDisposed == true) { _frmPassword = new frmPassword(); }
                    _frmPassword.MdiParent = this;
                    _frmPassword.Show();
                    _frmPassword.Activate();
                    break;
                case "新增包裝":
                    if (_frmPackage == null) { _frmPackage = new frmPackage(); }
                    if (_frmPackage.IsDisposed == true) { _frmPackage = new frmPackage(); }
                    _frmPackage.MdiParent = this;
                    _frmPackage.Show();
                    _frmPackage.Activate();
                    break;
                case "新增庫位":
                    if (_frmStoreHouse == null) { _frmStoreHouse = new frmStoreHouse(); }
                    if (_frmStoreHouse.IsDisposed == true) { _frmStoreHouse = new frmStoreHouse(); }
                    _frmStoreHouse.MdiParent = this;
                    _frmStoreHouse.Show();
                    _frmStoreHouse.Activate();
                    break;
                case "比對資料作業":
                    if (_frmUPCdata == null) { _frmUPCdata = new frmUPCdata(); }
                    if (_frmUPCdata.IsDisposed == true) { _frmUPCdata = new frmUPCdata(); }
                    _frmUPCdata.MdiParent = this;
                    _frmUPCdata.Show();
                    _frmUPCdata.Activate();
                    break;
                case "平置倉儲位設定":
                    if (_frmStorePos == null) { _frmStorePos = new frmStorePos(); }
                    if (_frmStorePos.IsDisposed == true) { _frmStorePos = new frmStorePos(); }
                    _frmStorePos.MdiParent = this;
                    _frmStorePos.Show();
                    _frmStorePos.Activate();
                    break;
                case "平置倉儲位查詢":
                    if (_frmQPos == null) { _frmQPos = new frmQPos(); }
                    if (_frmQPos.IsDisposed == true) { _frmQPos = new frmQPos(); }
                    _frmQPos.MdiParent = this;
                    _frmQPos.Show();
                    _frmQPos.Activate();
                    break;
                case "離開系統":
                    KXMSSysPara.LVColWidth.SaveLVColumnSetting();
                    if (_msgBox == null) { _msgBox = new MsgBox(); }
                    _msgBox.ShowDialog();

                    break;
                case "登出":
                    break;
                case "操作手冊":
                    break;
                case "關於自動倉儲管理系統":
                    if (_frmAbout == null) { _frmAbout = new frmAbout(); }
                    _frmAbout.MdiParent = this;
                    _frmAbout.Show();
                    _frmAbout.Activate();
                    break;
                case "ShelfLife":
                    if (_frmShelfLife == null) { _frmShelfLife = new frmShelfLife(); }
                    if (_frmShelfLife.IsDisposed == true) { _frmShelfLife = new frmShelfLife(); }
                    _frmShelfLife.MdiParent = this;
                    _frmShelfLife.Show();
                    _frmShelfLife.Activate();
                    break;
            }
        }
        #endregion

        #region Button 事件
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newMDIChild("入庫作業");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            newMDIChild("領料作業");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            newMDIChild("離開系統");
        }

        private void 盤點調整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("盤點調整");
        }

        private void 物料查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("物料查詢");
        }

        private void 關於自動倉儲管理系統ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("關於自動倉儲管理系統");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            newMDIChild("報表中心");
        }

        private void 使用者設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("使用者設定");
        }

        private void 儲位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("儲位管理");
        }

        private void 修改密碼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("修改密碼");
        }

        private void 新增包裝作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("新增包裝");
        }

        private void 新增庫位作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("新增庫位");
        }

        private void 比對資料作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("比對資料作業");
        }

        private void 平置倉儲位設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("平置倉儲位設定");
        }

        private void 平置倉儲位查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newMDIChild("平置倉儲位查詢");
        }

        private void TbIns_Click(object sender, EventArgs e)
        {
            newMDIChild("批次入庫");
        }

        private void TbShelf_Click(object sender, EventArgs e)
        {
            newMDIChild("ShelfLife");
        }

        #endregion

        #region 記憶體狀況


        private void Btnbuf_Click(object sender, EventArgs e)
        {
            if (_frmBuffer.Visible == false) { return; }
            setFrmBufferSize();

        }
        private void setFrmBufferSize()
        {
            if (_frmBuffer.Width == 0)
            {
                if (KXMSSysPara.Sys.WareHouse == 3)
                {
                    _frmBuffer.Width = 700;
                    //_frmBuffer.Left = 1200;
                    _frmBuffer.Left = this.Width - 700 - 36;
                }
                else
                {
                    _frmBuffer.Width = 500;
                    _frmBuffer.Left = this.Width - 500 - 36;
                }

                _frmBuffer.Top = 0;
                _frmBuffer.Height = this.Height - 116;


                Btnbuf.Text = ">";
            }
            else
            {
                _frmBuffer.Width = 0;
                Btnbuf.Text = "<";
            }
        }
        private void mdiMain_SizeChanged(object sender, EventArgs e)
        {
            setFrmBufferSize();
        }
        #endregion

        bool _backup = false;
        private void timerBackup_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 17)
            {   //17點整備份一次
                if (_backup == false)
                {
                    BackupToExcel();
                    _backup = true;
                }
            }
            else
            {
                _backup = false;
            }
        }

        private void 錯誤資料維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Sqlstr = "";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
            OleDbCommand Cmd = new OleDbCommand(Sqlstr, Conn);

            if (KXMSMsgBox.Show("請確定目前已全部完成入庫、領料的動作，才可執行" + "\r\n" + "錯誤資料維修，請小心使用", "", "", enMessageType.Question, enMessageButton.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            Sqlstr = _SqlData.GetData("異動", 8);
            try
            {
                Conn.Open();
                Cmd.CommandText = Sqlstr;
                Cmd.ExecuteNonQuery();
                Conn.Close();
                KXMSMsgBox.Show("修正完成", "", "", enMessageType.Success);
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show(ex.Message, "", "", enMessageType.Warning);
            }
        }

        private void 重新產生報表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupToExcel(false);
        }

        #region "產生備份報表"

        private void BackupToExcel(bool Background = true)
        {
            string vHeaderStr;
            string vColStr;
            string sqlstr;
            string vfileName = "";
            DataTable DT = null;
            try
            {
                //讀取當前庫存
                switch (KXMSSysPara.Sys.WareHouse)
                {
                    case 1://半成品倉
                        vfileName = @"report\Backup01_T.R";
                        break;
                    case 2: //成品倉
                        vfileName = @"report\Backup02_T.R";
                        break;
                    case 3: //IC倉
                        vfileName = @"report\Backup03_T.R";
                        break;
                }

                if (readReportFile(vfileName, out vHeaderStr, out vColStr, out sqlstr))
                {
                    //讀取資料
                    DT = LoadDT(sqlstr);
                }

                //匯出Excel
                string vErrMsg;
                ExportExcel(DT, out vErrMsg);
                if (String.IsNullOrEmpty(vErrMsg) == false) { throw new Exception(vErrMsg); }
                if (Background == false)
                {
                    KXMSMsgBox.Show("報表匯出成功！");
                }
            }
            catch (Exception ex)
            {

                if (Background == false)
                {
                    KXMSMsgBox.Show("報表匯出失敗！\r\n" + ex.Message, "", "", enMessageType.Warning);
                }
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
        private DataTable LoadDT(string sqlstr)
        {
            DataTable DT = new DataTable();
            try
            {
                OleDbConnection oleConn = new OleDbConnection(KXMSSysPara.Sys.DBConnStr);
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

        private void ExportExcel(DataTable DT, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                string vErrMsg = "";
                //詢問儲存路徑
                string vFileName = KXMSSysPara.Sys.BackupDir + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";
                string vSheetName = DateTime.Now.ToString("yyyyMMdd");

                //建立內容
                ExportService exService = new ExportService();
                if (exService.ExportExcel(vSheetName, vFileName, DT, out vErrMsg) == false)
                {
                    throw new Exception(vErrMsg);
                };

            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
        }
        #endregion

        private void mdiMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            KXMSSysPara.LVColWidth.SaveLVColumnSetting();
            _frmBuffer.gbCallQuit = true;
        }
    }
}
