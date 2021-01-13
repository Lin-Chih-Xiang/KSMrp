using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using nsKXMSUC;
namespace KSMrp
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            KXMSSysPara _Sys = new KXMSSysPara(); ;
            //檢查更新
            if (KXMSSysPara.Sys.AutoUpdate == 1)
            {
                frmUpdater vfrmUpdate = new frmUpdater(KXMSSysPara.Sys.UpdateFilePath, Application.StartupPath, "KSMRP.exe");
                if (vfrmUpdate.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    KXMSMsgBox.Show("更新完成！請重啟程式", "", "", enMessageType.Success, enMessageButton.OK);
                    Application.Exit();
                }
                else
                {
                    Application.Run(new mdiMain());
                }
            }
            else
            {
                Application.Run(new mdiMain());
            }


        }
    }
}
