using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace KSMrp
{
    class modCtrl
    {
        public static string CommandLevel ="";
        public static string FeedBack;
        //Const CommandStr = "000000000001"
        public static CommandStatus QueryST;
        //Dim MsComm1 As AxMSCommLib.AxMSComm
        //Dim FeedBack As String
        //Dim CommandLevel As String
        SqlData _SqlData = new SqlData(KXMSSysPara.Sys.WareHouse);
        public enum CommandStatus
        {

            // A
            TransferE1Data_A = 65,
            // B
            TransferE1Data_B = 66,
            // C
            ClearAlphaDisplay_C = 67,
            // D
            ClearE1RecallBuffer_D = 68,
            // E
            ClearE2RecallBuffer_E = 69,
            // F
            ClearE1Buffer_F = 70,
            // G
            ClearE2Buffer_G = 71,
            // H
            ClearAllBuffer_H = 72,
            // I
            RequestE1RecallBuffer_I = 73,
            // J
            RequestE2RecallBuffer_J = 74,
            // K
            RemoteStartE1_K = 75,
            // L
            RemoteStartE2_L = 76,
            // M
            ResetSafetyInterruption_M = 77,
            // N
            RequestE1E2BufferStatus_N = 78,
            // O
            RequestDeviceStatus_O = 79,
            // o
            RequestDeviceStatusExtend_o = 111,
            // P
            TransferAlphaDisplayData_P = 80,
            // Q
            ResetE1E2Command_Q = 81,
            // R
            TransferAutoStartE1Buffer_R = 82,
            // S
            RequestAutoStartRecallBuffer_S = 83,
            // T
            DirectStartWithoutBuffer_T = 84,
            // U
            RequestDeviceStatusCombined_U = 85,
            // V
            SpecialFunctionForShuttle_V = 86,
            // W
            RequestDeviceStatusCombinedExtend_W = 87,
        }

        private void Connect(string Sqlstr)
        {
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.CtrlConnStr);
            OleDbCommand oleCmd = new OleDbCommand("",Conn);
            DataTable DT = new DataTable();
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

        private void LongCommand(string Action, string ActionCommand, int PID)
        {
            string S = "";
            S = Action + ActionCommand;
            SendStr(S,PID);  
        }

        private void SendStr(string S, int PID)
        {
            string Sqlstr = "";
            S = KXMSSysPara.ComStr.STX + S + KXMSSysPara.ComStr.ETX;
            switch (CommandLevel)
            {
                case "":
                    //新增命令 S,PID
                    Sqlstr = _SqlData.GetData("命令",1);
                    Sqlstr = Sqlstr.Replace("?1",S);
                    Sqlstr = Sqlstr.Replace("?2",PID +"");
                    Connect(Sqlstr);
                    break;
                case "TOP":
                    //新增測試命令 S
                    Sqlstr = _SqlData.GetData("命令",2);
                    Sqlstr = Sqlstr.Replace("?1",S);
                    Connect(Sqlstr);
                    break;
            }
        }

        public string TransStr(int Device, int Carrier, int Pos, int Depth, int Qty, int QtyDigit)
        {
            //用來組合傳遞字串
            string S = "";
            // int i;
            // System.Text.StringBuilder QD = new System.Text.StringBuilder();  //資料型別，是一種不變的字串
            // QD.Append(Convert.ToChar("0"), QtyDigit);                        //將資訊附加至目前 StringBuilder 的結尾
            // Get Command
            // Get Device
            S = S + Device.ToString("#00");         //string.Format("00", Device));
            // Get Carrier
            S = S + Carrier.ToString("#000");      //string.Format("000", Carrier));
            // Get POS
            S = S + Pos.ToString("#00");            //string.Format("00", Pos));
            // Get Depth
            S = S + Depth.ToString("#0");           //string.Format("0", Depth));
            // Get Qty & QtyDigit
            if (QtyDigit == 6)                       //數量位元如果等於6，則#000000
            {
                S = S + Qty.ToString("#000000");
            }
            else
            {
                S = S + Qty.ToString("#0000");
            }
            //string.Format("", QD, Qty));
            // Calc CS
            S = CalcCS(S);
            // S = S & CS1 & CS2(檢查碼的產生)

            return S;
        }

        private string CalcCS(string S)
        {
            int ASCSUM = 0;
            int Carry;
            short CS1;
            short CS2;
            for (int i = 1; (i <= S.Length); i++)
            {
                ASCSUM += (Convert.ToChar(S.Substring((i - 1), 1)) - 48);
            }

            CS2 = (Convert.ToInt16(ASCSUM % 16));
            Carry = CS2 / 10;
            CS2 = (Convert.ToInt16(CS2 % 10));
            CS1 = Convert.ToInt16(ASCSUM / 16 + Carry);

            return (S + (CS1.ToString() + CS2.ToString()));
        }

        public string SendCommand(short Device, string Memory, short Carrier, short Pos, short Depth, int Qty, string Message, string Level = "", long PID = 0, short QtyDigit =0)
        {
            string S = "";
            string ActionCommand;
            CommandLevel = "";
            if (Level !="") { if (Level == "TOP") { CommandLevel = "TOP"; } }
            FeedBack = "";
            modCtrl T = new modCtrl();

            ActionCommand = (T.TransStr(Device, Carrier, Pos, Depth, Qty, QtyDigit) + Message);
            switch (Memory)
            {
                case "E1":
                    modCtrl.QueryST = modCtrl.CommandStatus.TransferE1Data_A;
                    S = "A";
                    break;
                case "E2":
                    modCtrl.QueryST = modCtrl.CommandStatus.TransferE1Data_B;
                    S = "B";
                    break;
                case "DISPLAY":
                    modCtrl.QueryST = modCtrl.CommandStatus.TransferAlphaDisplayData_P;
                    S = "P";
                    break;
                case "AUTOSTART":
                    modCtrl.QueryST = modCtrl.CommandStatus.TransferAutoStartE1Buffer_R;
                    S = "R";
                    break;
                case "DIRECTSTART":
                    modCtrl.QueryST = modCtrl.CommandStatus.DirectStartWithoutBuffer_T;
                    S = "T";
                    break;
                case "SHUTTLE":
                    modCtrl.QueryST = modCtrl.CommandStatus.SpecialFunctionForShuttle_V;
                    S = "V";
                    break;
            }

            LongCommand(S,ActionCommand,Convert.ToInt32(PID));
            return FeedBack;
        }

        public string SendC3Command(short Device, short HostID, string Memory, short Carrier, short Pos, short Depth, int Qty, string Msg1, string Msg2, string Msg3, string Msg4, long PID = 0)
        {
            string Sqlstr ="";
            OleDbConnection Conn = new OleDbConnection(KXMSSysPara.Sys.CtrlConnStr);
            OleDbCommand oleCmd = new OleDbCommand("", Conn);
            switch (Memory)
            {
                case "E1":
                    Sqlstr = _SqlData.GetData("命令",11);
                    break;
                case "E2":
                    Sqlstr = _SqlData.GetData("命令", 12);
                    break;
                default:
                    Sqlstr = _SqlData.GetData("命令", 11);
                    break;
            }

            Sqlstr = Sqlstr.Replace("?01",HostID +"");
            Sqlstr = Sqlstr.Replace("?02",Carrier +"");
            Sqlstr = Sqlstr.Replace("?03",Pos +"");
            Sqlstr = Sqlstr.Replace("?04",Depth +"");
            Sqlstr = Sqlstr.Replace("?05",Qty +"");
            Sqlstr = Sqlstr.Replace("?06",Msg1);
            Sqlstr = Sqlstr.Replace("?07",Msg2);
            Sqlstr = Sqlstr.Replace("?08",Msg3);
            Sqlstr = Sqlstr.Replace("?09",Msg4);
            Sqlstr = Sqlstr.Replace("?10",PID +"");
            Sqlstr = Sqlstr.Replace("?11",Device +"");
            Conn.Open();
            oleCmd.CommandText = Sqlstr;
            oleCmd.ExecuteNonQuery();
            Conn.Close();

            return "";
        }

        public static string RequestCommand(short Device, string Action, short QtyDigit)
        {
            string S = "";
            string ActionCommand;
            modCtrl T = new modCtrl();
            ActionCommand = T.TransStr(Device, 0, 0, 0, 0, QtyDigit);
            switch (Action)
            {
                case "E1":
                    QueryST = CommandStatus.RequestE1RecallBuffer_I;
                    S = "I";
                    break;
                case "E2":
                    QueryST = CommandStatus.RequestE2RecallBuffer_J;
                    S = "J";
                    break;
                case "AUTOSTART":
                    QueryST = CommandStatus.RequestAutoStartRecallBuffer_S;
                    S = "S";
                    break;
                case "MEMORY":
                    QueryST = CommandStatus.RequestE1E2BufferStatus_N;
                    S = "N";
                    break;
                case "DEVICE":
                    break;
                case "EXTDEVICE":
                    break;
                case "COMBINED":
                    QueryST = CommandStatus.RequestDeviceStatusCombined_U;
                    S = "U";
                    break;
                case "SHUTTLE":
                    QueryST = CommandStatus.RequestDeviceStatusCombinedExtend_W;
                    S = "W";
                    break;
            }
            return (S + ActionCommand);
        }

        public string RemoteCommand(short Device, string Action, short QtyDigit)
        {
            string S = "";
            string ActionCommand;
            ActionCommand = TransStr(Device, 0, 0, 0, 0, QtyDigit);
            switch (Action)
            {
                case "CONFRIM":
                    QueryST = CommandStatus.ResetE1E2Command_Q;
                    S = "Q";
                    break;

                case "RESET":
                    QueryST = CommandStatus.ResetSafetyInterruption_M;
                    S = "M";
                    break;

                case "STARTE1":
                    QueryST = CommandStatus.RemoteStartE1_K;
                    S = "K";
                    break;

                case "STARTE2":
                    QueryST = CommandStatus.RemoteStartE2_L;
                    S = "L";
                    break;
            }
            return S + ActionCommand;
        }

        public string ClearCommand(short Device, string Memory, short QtyDigit)
        {
            string S = "";
            string ActionCommand;
            ActionCommand = TransStr(Device, 0, 0, 0, 0, QtyDigit);
            //If Not (Level) Then
            //  If Level = "TOP" Then CommandLevel = "TOP"
            //End If
            //FeedBack = ""
            switch (Memory)
            {
                case "DISPLAY":
                    QueryST = CommandStatus.ClearAlphaDisplay_C;
                    S = "C";
                    break;
                case "E1":
                    QueryST = CommandStatus.ClearE1Buffer_F;
                    S = "F";
                    break;
                case "E2":
                    QueryST = CommandStatus.ClearE2Buffer_G;
                    S = "G";
                    break;
                case "E1RECALL":
                    QueryST = CommandStatus.ClearE1RecallBuffer_D;
                    S = "D";
                    break;
                case "E2RECALL":
                    QueryST = CommandStatus.ClearE2RecallBuffer_E;
                    S = "E";
                    break;
                case "ALL":
                    QueryST = CommandStatus.ClearAllBuffer_H;
                    S = "H";
                    break;
            }
            return (S + ActionCommand);
            //ShortCommand(S, Val(Device), PID, IIf(IsMissing(QtyDigit), 4, QtyDigit))
            //ClearCommand = FeedBack
        }

        public string ErrDesc(string N)
        {
            string S;

            switch (N)
            {
                case "0":
                    S = "檢測出物品放置不平衡狀態";
                    break;
                case "01":
                    S = "物料保護電眼動作";
                    break;
                case "10":
                    S = "任何安全裝置動作";
                    break;
                case "09":
                    S = "目前貨架未停止於定位";
                    break;
                case "02":
                    S = "操作門未開至定位";
                    break;
                case "03":
                    S = "維護門未關好";
                    break;
                case "04":
                    S = "手動扳手護蓋未蓋好";
                    break;
                case "05":
                    S = "緊急停止裝置動作";
                    break;
                case "06":
                    S = "馬達溫度過熱不再運轉";
                    break;
                case "07":
                    S = "選購第二組緊急停止裝置動作";
                    break;
                case "08":
                    S = "其他";
                    break;
                case "73":
                    S = "請先按下S鍵，再開始操作";
                    break;
                default:
                    S = "不明代號" + N;
                    break;
            }
            return S;
        }
    }
}
