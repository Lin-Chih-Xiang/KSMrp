namespace KSMrp
{
    partial class frm_StartProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LB = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LB
            // 
            this.LB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LB.FormattingEnabled = true;
            this.LB.ItemHeight = 20;
            this.LB.Location = new System.Drawing.Point(0, 0);
            this.LB.Name = "LB";
            this.LB.Size = new System.Drawing.Size(530, 125);
            this.LB.TabIndex = 0;
            // 
            // frmStartProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 125);
            this.Controls.Add(this.LB);
            this.Name = "frmStartProcess";
            this.Text = "frmStartProcess";
            this.Load += new System.EventHandler(this.frmStartProcess_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LB;
    }
}