using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KSMrp
{
    public static class WordTextBox
    {
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        
        private static extern Int32 SendMessage
         (IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        
        public static void SetWatermark(this TextBox textBox, string watermark)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermark);
        }
       
        public static void ClearWatermark(this TextBox textBox)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, string.Empty);
        }
    }
}
