namespace nsKXMSUC
{
    partial class KXMSTextMsgBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KXMSTextMsgBox));
            this.imageList48 = new System.Windows.Forms.ImageList(this.components);
            this.txtMsg1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // imageList48
            // 
            this.imageList48.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList48.ImageStream")));
            this.imageList48.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList48.Images.SetKeyName(0, "btnClose.jpg");
            this.imageList48.Images.SetKeyName(1, "btnOK.jpg");
            // 
            // txtMsg1
            // 
            this.txtMsg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMsg1.Location = new System.Drawing.Point(0, 0);
            this.txtMsg1.Multiline = true;
            this.txtMsg1.Name = "txtMsg1";
            this.txtMsg1.ReadOnly = true;
            this.txtMsg1.Size = new System.Drawing.Size(429, 284);
            this.txtMsg1.TabIndex = 0;
            // 
            // KXMSTextMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(429, 284);
            this.Controls.Add(this.txtMsg1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KXMSTextMsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KXMS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList48;
        private System.Windows.Forms.TextBox txtMsg1;
    }
}