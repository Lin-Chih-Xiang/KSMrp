using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nsKXMSUC
{
    
       
    public partial class KXMSBtnOSK : Button
    {
        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int RegisterWindowMessage(string lpString);
        public KXMSBtnOSK()
        {
            InitializeComponent();
            this.Text = "";
        }
        private int  gbStatus = 0;
        public static int ShowInputPanel()
        {
            try
            {
                string file = @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return -1;
                Process.Start(file);

                return 1;
            }
            catch (Exception)
            {
                return 255;
            }
        }

        public static void HideInputPanel()
        {
            IntPtr TouchhWnd = new IntPtr(0);
            TouchhWnd = FindWindow("IPTip_Main_Window", null);
            if (TouchhWnd == IntPtr.Zero)
                return;
            PostMessage(TouchhWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
        }
        private void btnKB_Click(object sender, EventArgs e)
        {
            //string path64 = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_10.0.14393.0_none_d8313ee770a155e8\osk.exe";
            //string path32 = @"C:\windows\system32\osk.exe";
            //string path = "";//= (Environment.Is64BitOperatingSystem) ? path64 : path32;
            //switch (Environment.OSVersion.Version.Build)
            //{
            //    case 9200:
            //        path = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_10.0.14393.0_none_d8313ee770a155e8\osk.exe";
            //        //path = @"C:\Program Files\Common Files\microsoft  shared\ink\TabTip.exe";
            //        break;
            //    default :
            //        path = @"osk.exe";
            //        break;
            ////}
            //System.Diagnostics.Process.Start(path);
            ShowInputPanel();
        }
    }
}
