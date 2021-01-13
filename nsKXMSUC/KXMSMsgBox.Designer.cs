namespace nsKXMSUC
{
    partial class KXMSMsgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KXMSMsgBox));
            this.imageList48 = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDetail = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblIcon = new System.Windows.Forms.Label();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList48
            // 
            this.imageList48.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList48.ImageStream")));
            this.imageList48.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList48.Images.SetKeyName(0, "btnClose.jpg");
            this.imageList48.Images.SetKeyName(1, "btnOK.jpg");
            this.imageList48.Images.SetKeyName(2, "btnSearch.jpg");
            this.imageList48.Images.SetKeyName(3, "btnDetail.jpg");
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ImageIndex = 0;
            this.btnCancel.ImageList = this.imageList48;
            this.btnCancel.Location = new System.Drawing.Point(250, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(48, 48);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOK.ImageIndex = 1;
            this.btnOK.ImageList = this.imageList48;
            this.btnOK.Location = new System.Drawing.Point(128, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(48, 48);
            this.btnOK.TabIndex = 9;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDetail);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 57);
            this.panel1.TabIndex = 11;
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.BackColor = System.Drawing.SystemColors.Control;
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.ImageIndex = 3;
            this.btnDetail.ImageList = this.imageList48;
            this.btnDetail.Location = new System.Drawing.Point(376, 4);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(48, 48);
            this.btnDetail.TabIndex = 11;
            this.btnDetail.UseVisualStyleBackColor = false;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMessage.Location = new System.Drawing.Point(82, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(347, 129);
            this.lblMessage.TabIndex = 12;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "if_messagebox_warning_1995.png");
            this.imageList1.Images.SetKeyName(1, "if_button_cancel_1939.png");
            this.imageList1.Images.SetKeyName(2, "if_faq_46801.png");
            this.imageList1.Images.SetKeyName(3, "if_info_46816.png");
            this.imageList1.Images.SetKeyName(4, "if_button_ok_3207.png");
            // 
            // lblIcon
            // 
            this.lblIcon.ImageIndex = 4;
            this.lblIcon.ImageList = this.imageList1;
            this.lblIcon.Location = new System.Drawing.Point(12, 31);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(64, 64);
            this.lblIcon.TabIndex = 13;
            // 
            // txtDetail
            // 
            this.txtDetail.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDetail.Location = new System.Drawing.Point(82, 0);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ReadOnly = true;
            this.txtDetail.Size = new System.Drawing.Size(347, 129);
            this.txtDetail.TabIndex = 14;
            this.txtDetail.Visible = false;
            // 
            // KXMSMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(429, 185);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KXMSMsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "訊息";
            this.Load += new System.EventHandler(this.KXMSMsgBox_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList48;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.TextBox txtDetail;
    }
}