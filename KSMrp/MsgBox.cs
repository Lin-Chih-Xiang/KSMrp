using System;
using System.Windows.Forms;

namespace KSMrp
{
    public partial class MsgBox : Form
    {
        public MsgBox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MsgBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
