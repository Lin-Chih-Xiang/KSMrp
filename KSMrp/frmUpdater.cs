using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace KSMrp
{
    public partial class frmUpdater : Form
    {
      
        private string gbUpdatePath = "";
        private string gbTargetPath = "";
        private string gbMainVerFile = "KSMRP.exe";
        private bool vSuccess = false;
        private bool vOK = false;
        private bool vCancel = false;
        public frmUpdater()
        {
            InitializeComponent();
        }
        public frmUpdater(string UpdatePath,string TargetPath,string MainVerFile)
        {
            InitializeComponent();
            gbUpdatePath = UpdatePath;
            gbTargetPath = TargetPath;
            gbMainVerFile = MainVerFile;
        }
       
        private void StartUpdate()
        {
            lblMsg2.Text = "檢查更新中...";
            //取得更新版號
            string vUpdateVersion = GetMainVersion(gbUpdatePath);
            //取得當前版號
            string vNowVersion = GetMainVersion(gbTargetPath);
            if (vUpdateVersion == "")
            {
                lblMsg.Text = "找不到更新伺服器";
            }
            else
            {
                lblMsg.Text = "當前版本：" + vNowVersion + " 遠端版本：" + vUpdateVersion;
                backgroundWorker1.RunWorkerAsync();
            }
        }
        #region "更新程式 路徑模式"
        private void Form1_Load(object sender, EventArgs e)
        {
            //gbUpdatePath = txtUpdatePath.Text;
            //gbTargetPath = txtPath.Text;
           
            DTFileF = new DataTable();
            DTFileF.Columns.Add("FileName");
            DTFileF.Columns.Add("LastUpdateTime");
            DTFileF.Columns.Add("NeedUpdate");

            DTFileT = new DataTable();
            DTFileT.Columns.Add("FileName");
            DTFileT.Columns.Add("LastUpdateTime");
            DTFileT.Columns.Add("NeedUpdate");

            DTEx = new DataTable();
            DTEx.Columns.Add("FileName");


            //開始檢查更新
            StartUpdate();
        }
        DataTable DTFileF;
        DataTable DTFileT;
        DataTable DTEx;
        private void DirSearch(string OriPath, string vPath, bool DeleteTemp, DataTable DT)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(vPath))
                {
                    LoadFileList(OriPath, d, DeleteTemp, DT);
                    DirSearch(OriPath, d, DeleteTemp, DT);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadFileList(string OriPath, string vPath,bool DeleteTemp, DataTable DT)
        {
            try
            {
                string vFileName = "";
                
                foreach (string f in Directory.GetFiles(vPath))
                {
                    DataRow DR = DT.NewRow();
                    vFileName = f.Replace(OriPath, "");
                    if (vFileName.Substring(0, 1) == @"\") { vFileName = vFileName.Substring(1); }
                    DR[0] = vFileName;
                    DR[1] = File.GetLastWriteTime(f).ToString("yyyy-MM-dd HH:mm:ss");
                    DR[2] = "";
                    //將Temp檔刪除
                    if (DeleteTemp == true)
                    {
                        if (Path.GetExtension(f) == "temp")
                        {
                            File.Delete(f);
                        };
                    }
                    DT.Rows.Add(DR);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string GetMainVersion(string Path)
        {
            string vVersion = "";
            if (File.Exists(Path + @"\" + gbMainVerFile))
            {
                vVersion = FileVersionInfo.GetVersionInfo(Path + @"\" + gbMainVerFile).FileVersion.ToString();
            }
            return vVersion;
        }
        private void GetExupdateList(string vFilePath, out string ErrMsg)
        {
            ErrMsg = "";
            //開啟檔案
            try
            {
                //取得排除檔案列表
                if (File.Exists(vFilePath + @"unupdate.txt") == false) { return; }
                string fileData;
                System.IO.StreamReader FSR = new System.IO.StreamReader(vFilePath + @"unupdate.txt", System.Text.Encoding.Default);
                fileData = FSR.ReadToEnd();
                FSR.Close();
                string[] CPstr;
                CPstr = fileData.Split('\n');
                DTEx.Rows.Clear();
                foreach ( string c in CPstr)
                {
                    if (String.IsNullOrEmpty(c)) { continue; }
                    
                    string vF = c.Replace("\r", "");
                    DataRow DR = DTEx.NewRow();
                    DR[0] = vF;
                    DTEx.Rows.Add(DR);
                }
                
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
        }


        private void GetUpdatefileList(DataTable DTF,DataTable DTT,DataTable DTExx){
            
            //將來源的例外排除
            foreach (DataRow DR in DTF.Rows)
            {
                foreach (DataRow DRE in DTExx.Rows)
                {   //檔名比較 相同的排除
                    if (DR[0].ToString().ToUpper() == DRE[0].ToString().ToUpper())
                    {
                        DR[2]="N";
                        break;
                    }
                }
            }
            //以來源檔案比較目前檔案 修改日期相同的排除
            foreach (DataRow DR in DTF.Rows)
            {
                foreach (DataRow DRT in DTT.Rows)
                {   //檔名比較 相同的
                    if (DR[0].ToString().ToUpper() == DRT[0].ToString().ToUpper())
                    {   //修改日期相同的排除 
                        if (DR[1].ToString() == DRT[1].ToString())
                        {
                            DR[2] = "N";
                            break;
                        }
                    }
                }
            }
        }
        private void UpdateProcessByFilePath(DataTable DTF,string UpdatePath,string TargetPath, out string ErrMsg)
        {
            ErrMsg = "";
            try
            {
                DataRow[] DRF = DTFileF.Select("NeedUpdate<>'N'");
                
                
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
        }

        #endregion

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PB2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string vErrMsg = "";
            //取得來源清單
            DTFileF.Rows.Clear();
            lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "取得更新項目..."; }));
            LoadFileList(gbUpdatePath, gbUpdatePath,false, DTFileF);
            DirSearch(gbUpdatePath, gbUpdatePath, false, DTFileF);
             System.Threading.Thread.Sleep(100);
            backgroundWorker1.ReportProgress(10);
            if (vCancel == true) { goto Exit; }
            //取得例外清單
            GetExupdateList(gbUpdatePath, out vErrMsg);

            //取得目前檔案清單

            lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "比對現有資料..."; }));
            DTFileT.Rows.Clear();
            LoadFileList(gbTargetPath, gbTargetPath, true, DTFileT);
            if (vCancel == true) { goto Exit; }
            DirSearch(gbTargetPath, gbTargetPath, true, DTFileT);
            System.Threading.Thread.Sleep(100);
            backgroundWorker1.ReportProgress(20);
            if (vCancel == true) { goto Exit; }
            //取得應更新清單
            GetUpdatefileList(DTFileF, DTFileT, DTEx);
            DataRow[] DRF = DTFileF.Select("NeedUpdate<>'N'");
            int vFC = DRF.Length;
            if (vFC == 0)
            {
                lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "當前版本已為最新版！"; }));
                vOK = true;
                backgroundWorker1.ReportProgress(100);
            }
            else
            {
                //更新處理
                lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "更新...(0/" + DRF.Length + ")"; }));
                UpdateProcessByFilePath(DTFileF, gbUpdatePath, gbTargetPath, out vErrMsg);
                int i = 0;
                foreach (DataRow DR in DRF)
                {
                    string vDLFileName = DR[0].ToString();
                    string vTempFileName = vDLFileName + ".temp";
                    
                    try
                    {
                        i = i + 1;
                        //將現有檔案更名
                        if (File.Exists(gbTargetPath + @"\" + vTempFileName)) { File.Delete(gbTargetPath + @"\" + vTempFileName); }
                        if (File.Exists(gbTargetPath + @"\" + vDLFileName)) { File.Move(gbTargetPath + @"\" + vDLFileName, gbTargetPath + @"\" + vTempFileName); }
                        else { 
                            //檢查目錄是否存在，否則新增目錄
                            if (Directory.Exists(Path.GetDirectoryName(gbTargetPath + @"\" + vDLFileName)) == false) 
                            { Directory.CreateDirectory(Path.GetDirectoryName(gbTargetPath + @"\" + vDLFileName)); } 
                        }
                        File.Copy(gbUpdatePath + @"\" + vDLFileName, gbTargetPath + @"\" + vDLFileName);
                        System.Threading.Thread.Sleep(50);
                        lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "更新...(" + i + "/" + vFC + ")"; }));
                        backgroundWorker1.ReportProgress(20 + (i * 80 / vFC));
                        if (vCancel == true) { goto Exit; }
                    }
                    catch (Exception ex)
                    {
                        vErrMsg += vDLFileName + "下載失敗 (" + ex.Message + ")\n";
                    }
                }

             
                if (String.IsNullOrEmpty(vErrMsg) == false)
                {
                    lblMsg2.Invoke(new Action(() => { lblMsg2.Text = vErrMsg; }));
                }
                else
                {
                    lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "更新完成！共更新" + DRF.Length + "個檔案"; }));
                    vSuccess = true;
                }
                backgroundWorker1.ReportProgress(100);
            }
        //更新結束
        Exit:
            if (vCancel == true)
            {
                lblMsg2.Invoke(new Action(() => { lblMsg2.Text = "取消更新！"; }));
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (vSuccess == true)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }else if(vOK== true){
                  this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            vCancel = true;
        }
    }
}
