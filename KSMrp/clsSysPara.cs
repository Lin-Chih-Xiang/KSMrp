﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data;
namespace KSMrp
{
    class KXMSSysPara
    {
        public static SysPara Sys;
        public static CommStr ComStr;
        public static clsLVColumnWidth LVColWidth;
        public KXMSSysPara()
        {
            Sys = new SysPara();
            ComStr = new CommStr();
            LVColWidth = new clsLVColumnWidth();
        }
    }

    class SysPara
    {
        public string ReportFilePath = @"C:\Users\MIS\Desktop\金士頓\KSMrp\KSMrp\bin";      //預設的儲存路徑       
        public string BackupDir = @"C:\KXMS\Backup";           //備份路徑                                                                                  // public string BackupDir = @"C:\Users\MIS\Desktop\金士頓\KSMrp\KSMrp\bin";           //備份路徑
        public int AutoID = 0;
        public string UID = "";
        public string Pass = "";
        public string Power = "";
        public int MaxDevice = 0;
        public string _DialogResult = "";

        //登錄檔要用的欄位如下========================================================
        public string DefaultPackage = "預設包裝(150)";     //預設的包裝
        public int IsTest = 0;                              //0:正常模式,1:查詢模式,2:測試模式
        public int MachineNo = 4;                      //有哪幾台機器 (逗號分隔)
        public int MinHeightForAB = 11;                     //預設的123Carry的ab料號的最小量
        public int MinNumForAB = 11;
        public int WareHouse = 1;                           //1.半成品倉 2.成品倉 3.IC倉
        public int AutoUpdate = 1;                          //自動更新
        public string UpdateFilePath = "";                  //自動更新的來源路徑
        public string DBConnStr = "Provider=SQLOLEDB.1;Password=Hewtech;Persist Security Info=True;User ID=Hewtech;Initial Catalog=KSMRP_DBDATA;Data Source=192.168.100.120\\SQLEXPRESS,1433";                       //SQL資料庫
        public string CtrlConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=hewCtrl_v3.mdb;Persist Security Info=False";                       //SQL資料庫
        //============================================================================
        public string SaPassWord = "Hewtech";

        SqlData _SqlData;
        public SysPara()
        {
            LoadHewCtrlSetting();
            _SqlData = new SqlData(WareHouse);
        }
        
        public int StoreHeight(int F)
        {
            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
            int b;
            for (b = 1; b <= 3; b++)
            {
                a[F] = MinHeightForAB;
            }
            for (b = 4; b <= 26; b++)
            {
                a[F] = 1;
            }

            return a[F];
        }

        //private string gbErrMsg { get; set; }  //初始過程有錯誤
        public void LoadHewCtrlSetting()                         //資料讀取
        {
            int i;

            try
            {
                //讀取AS_Stting
                string fileData;
                StreamReader FSR = new StreamReader("KSMRP_Setting", System.Text.Encoding.Default);  //建立讀取FSR(string(檔名),encoding(字元碼))
                fileData = FSR.ReadToEnd();                     //將FSR資料全部讀取於fileData裡
                fileData = fileData.Replace("\r", "");
                string[] CPstr;
                CPstr = fileData.Split('\n');                   // 資料行換行

                for (i = 0; i < CPstr.GetUpperBound(0); i++)    // CPStr指定維度上限                 
                {
                    string[] c;
                    //以 | 做區隔
                    c = CPstr[i].Split('|');
                    switch (c[0])
                    {
                        case "DefaultPackage":
                            DefaultPackage = c[1];
                            break;
                        case "IsTest":
                            IsTest = int.Parse(c[1]);
                            break;
                        case "MachineNo":
                            MachineNo = int.Parse(c[1]);
                            break;
                        case "MinHeightForAB":
                            MinHeightForAB = int.Parse(c[1]);
                            break;
                        case "MinNumForAB":
                            MinNumForAB = int.Parse(c[1]);
                            break;
                        case "WareHouse":
                            WareHouse = int.Parse(c[1]);
                            break;
                        case "DBConnStr":
                            DBConnStr = c[1];
                            break;
                        case "CtrlConnStr":
                            CtrlConnStr = c[1];
                            break;
                        case "ReportFilePath":
                            ReportFilePath = c[1];
                            break;
                        case "BackupDir":
                            BackupDir = c[1];
                            break;
                        case "AutoUpdate":
                            AutoUpdate = int.Parse(c[1]);
                            break;
                        case "UpdateFilePath":
                            UpdateFilePath = c[1];
                            break;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //直接跳出迴圈 
            }
        }
        public string SaveHewCtrlSetting()                                  //資料存取
        {
            string Str = "";
            //處理路徑部分
            if (ReportFilePath.Substring(ReportFilePath.Length - 1, 1) != @"\")
            {
                ReportFilePath += @"\";
            }

            //=======================================================
            Str += "DefaultPackage" + "|" + DefaultPackage + "\r\n";
            Str += "IsTest" + "|" + IsTest + "\r\n";
            Str += "MachineNo" + "|" + MachineNo + "\r\n";
            Str += "MinHeightForAB" + "|" + MinHeightForAB + "\r\n";
            Str += "MinNumForAB" + "|" + MinNumForAB + "\r\n";
            Str += "WareHouse" + "|" + WareHouse + "\r\n";
            Str += "DBConnStr" + "|" + DBConnStr + "\r\n";
            Str += "CtrlConnStr" + "|" + CtrlConnStr + "\r\n";
            Str += "ReportFilePath" + "|" + ReportFilePath + "\r\n";
            Str += "BackupDir" + "|" + BackupDir + "\r\n";
            Str += "AutoUpdate" + "|" + AutoUpdate + "\r\n";
            Str += "UpdateFilePath" + "|" + UpdateFilePath + "\r\n";
            //=======================================================

            try
            {
                StreamWriter FSW = new StreamWriter(ReportFilePath);
                FSW.Write(Str);
                FSW.WriteLine();
                FSW.Close();
                return "OK";
            }
            catch (FileNotFoundException)
            {
                FileStream fs = File.Create(ReportFilePath);
                StreamWriter FSW = new StreamWriter(ReportFilePath);
                FSW.Write(Str);
                FSW.WriteLine();
                FSW.Close();
                return "OK";
            }
            catch (IOException ex)
            {
                return "檔案正被使用中，請關閉使用中的程式，再進行存檔" + "\r\n" + ex.Message;
            }
            catch (Exception ex)
            {
                return "參數儲存失敗" + "\r\n" + ex.Message;
            }

        }

        public void ConnExec(string SQL, OleDbDataReader oleDR)
        {
            OleDbConnection oleConn = new OleDbConnection(DBConnStr);
            oleConn.Open();
            OleDbCommand oleComm = new OleDbCommand(SQL, oleConn);
            oleDR = oleComm.ExecuteReader();                                        //快速讀取資料庫查詢資料(oleDbDataReader)
        }

        public void ConnExec(string SQL)
        {
            OleDbConnection oleConn = new OleDbConnection(DBConnStr);
            oleConn.Open();
            OleDbCommand oleComm = new OleDbCommand(SQL, oleConn);
            oleComm.ExecuteNonQuery();                                              //建立資料表
        }

        //紀錄上次視窗的位置
        public void SavePOS(Form frm)
        {
            string S;

            if (frm.Top < 0) { frm.Top = 0; }
            S = frm.Left + "," + frm.Top;
        }

        //讀取上次視窗的位置
        public void LoadPos(Form frm)
        {
            string S = "";
            string[] A;

            if (S.Length == 0)
            {
                mdiMain W = new mdiMain();
                S = (W.Width - frm.Width) / 2 + "," + (W.Height - frm.Height) / 2;
                A = S.Split(',');
                if (int.Parse(A[0]) < 0)
                {
                    A[0] = "0";
                }
                frm.Top = int.Parse(A[0]);
                frm.Left = int.Parse(A[1]);
            }
        }

        //16進制轉2進制
        public string chHexToBin(string S)
        {
            int intNum = Convert.ToInt32(S, 16);
            string strBinary = Convert.ToString(intNum, 2);
            return strBinary;
        }

        public void Connect(string Sqlstr)
        {
            OleDbConnection Conn = new OleDbConnection(DBConnStr);
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
        }

        //DE.刪除無庫存儲位為空儲位
        public void CheckEmptyStore()
        {
            _SqlData = new SqlData(WareHouse);
            string Sqlstr = "";
            Sqlstr = _SqlData.GetData("儲位", 16);
            Connect(Sqlstr);
            Sqlstr = _SqlData.GetData("儲位", 23);
            Connect(Sqlstr);
        }

        //暫時指定儲位取消(Enabled 2->1)
        public void CancelTempStore()
        {
            string Sqlstr;
            Sqlstr = _SqlData.GetData("儲位", 6);
            Connect(Sqlstr);
        }

        //料號驗證
        public string MnoCheck(string Mno)
        {
            double i;
            Mno = Mno.Trim();

            switch (WareHouse)
            {
                //半成品倉
                case 1:
                    if (Mno.Length != 15) { return Mno; }
                    //1234567-123.A12
                    else if (!(double.TryParse(Mno.Substring(0, 7), out i))) { return Mno; }
                    else if (Mno.Substring(7, 1) != "-") { return Mno; }
                    else if (!(double.TryParse(Mno.Substring(8, 3), out i))) { return Mno; }
                    else if (Mno.Substring(11, 1) != ".") { return Mno; }
                    else if (Mno.Substring(12, 1).ToUpper() != "[A-Z]") { return Mno; }
                    else if(!(double.TryParse(Mno.Substring(3, 2), out i))) { return Mno; }
                    break;
                //成品倉
                case 2:
                    break;
            }

            return Mno;
        }

        public class RadioButtonItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public RadioButtonItem()
            {
            }
            public RadioButtonItem(string text, string value)
            {
                Text = text;
                Value = value;
            }
            public override string ToString()
            {
                return Text;
            }
        }
    }

    public class clsLVColumnWidth
    {

        //KXMS_LV_Setting 儲存之參數
        private string gbErrMsg { get; set; }  //初始過程有錯誤
        DataTable DT_LVCol = null;
        public clsLVColumnWidth()
        {
            LoadLVColumnSetting();
        }

        public string GetLVColWidth(string FormName, string ListViewName)
        {
            string vColStr = "";
            DataRow[] DR = DT_LVCol.Select("LV_NAME='" + FormName + "_" + ListViewName + "'");
            if (DR.Length == 0)
            {
                vColStr = "";
            }
            else
            {
                vColStr = DR[0][1].ToString();
            }
            return vColStr;
        }
        public bool SetLVColWidth(string FormName, string ListViewName, string ColStr)
        {
            bool vResult = true;
            try
            {
                DataRow[] DR = DT_LVCol.Select("LV_NAME='" + FormName + "_" + ListViewName + "'");
                if (DR.Length == 0)
                {
                    DataRow DR0 = DT_LVCol.NewRow();
                    DR0[0] = FormName + "_" + ListViewName;
                    DR0[1] = ColStr;
                    DT_LVCol.Rows.Add(DR0);
                }
                else
                {
                    DR[0][1] = ColStr;
                }
                vResult = true;
            }
            catch
            {
                vResult = false;
            }
            return vResult;
        }
        public void LoadLVColumnSetting()
        {
            int i;
            if (DT_LVCol == null)
            {
                DT_LVCol = new DataTable();
                DT_LVCol.Columns.Add("LV_NAME", typeof(String));
                DT_LVCol.Columns.Add("COLS_WIDTH", typeof(String));
            }
            else
            {
                DT_LVCol.Clear();
            }
            try
            {
                //讀取KXMS_LV_Setting
                string fileData;
                System.IO.StreamReader FSR = new System.IO.StreamReader("KXMS_LV_Setting", System.Text.Encoding.Default);
                fileData = FSR.ReadToEnd();
                string[] CPstr;
                CPstr = fileData.Split('\n');

                for (i = 0; i < CPstr.GetUpperBound(0); i++)
                {
                    if (String.IsNullOrEmpty(CPstr[i])) { continue; }
                    string[] c;
                    // 以 | 區隔
                    c = CPstr[i].Split('|');
                    if (c.Length == 2)
                    {
                        DataRow DR = DT_LVCol.NewRow();
                        DR[0] = c[0];
                        DR[1] = c[1];
                        DT_LVCol.Rows.Add(DR);
                    }
                }
                FSR.Close();
            }
            catch (Exception ex)
            {
                gbErrMsg = ex.Message;
            }

        }

        public void SaveLVColumnSetting()
        {
            try
            {
                string vSettingStr = "";
                foreach (DataRow DR in DT_LVCol.Rows)
                {
                    vSettingStr += DR[0] + "|" + DR[1] + "\n";
                }
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("KXMS_LV_Setting", false))
                {
                    file.WriteLine(vSettingStr);
                }
            }
            catch (Exception ex)
            {
                gbErrMsg = ex.Message;
            }

        }


    }

    public class CommStr
    {
        public char STX = Convert.ToChar(2);
        public char ETX = Convert.ToChar(3);
        public char ACK = Convert.ToChar(6);
        public char ETB = Convert.ToChar(23);
        public char NAK = Convert.ToChar(21);
        public char SO = Convert.ToChar(14);
        public char SI = Convert.ToChar(15);
        public char EOT = Convert.ToChar(4);
    }
}
