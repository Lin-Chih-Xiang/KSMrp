namespace KSMrp
{
    partial class frm_ExcelImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ExcelImport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblLVCount = new System.Windows.Forms.Label();
            this.LV1 = new nsKXMSUC.KXMSLV();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cbSheet = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileName = new nsKXMSUC.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.OpfDlg1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 681);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Moccasin;
            this.panel3.Controls.Add(this.lblLVCount);
            this.panel3.Controls.Add(this.LV1);
            this.panel3.Controls.Add(this.btnImport);
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.cbSheet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(0, 329);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(778, 352);
            this.panel3.TabIndex = 1;
            // 
            // lblLVCount
            // 
            this.lblLVCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLVCount.Location = new System.Drawing.Point(21, 318);
            this.lblLVCount.Name = "lblLVCount";
            this.lblLVCount.Size = new System.Drawing.Size(240, 24);
            this.lblLVCount.TabIndex = 40;
            this.lblLVCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LV1
            // 
            this.LV1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LV1.FullRowSelect = true;
            this.LV1.GridLines = true;
            this.LV1.HideSelection = false;
            this.LV1.Location = new System.Drawing.Point(21, 96);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(742, 216);
            this.LV1.TabIndex = 39;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnImport.ForeColor = System.Drawing.Color.Black;
            this.btnImport.Location = new System.Drawing.Point(146, 58);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(98, 31);
            this.btnImport.TabIndex = 38;
            this.btnImport.Text = "匯入";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Location = new System.Drawing.Point(21, 58);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(98, 31);
            this.btnLoad.TabIndex = 37;
            this.btnLoad.Text = "預覽";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbSheet
            // 
            this.cbSheet.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbSheet.FormattingEnabled = true;
            this.cbSheet.Location = new System.Drawing.Point(21, 21);
            this.cbSheet.Name = "cbSheet";
            this.cbSheet.Size = new System.Drawing.Size(223, 28);
            this.cbSheet.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnOpenFile);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtFileName);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblHeader);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(778, 323);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(103, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 24);
            this.label2.TabIndex = 41;
            this.label2.Text = "※ 匯入前請先關閉匯入的Excel";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtFileName.ForeColor = System.Drawing.Color.Black;
            this.txtFileName.Location = new System.Drawing.Point(98, 48);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.PlaceHolder = "";
            this.txtFileName.Size = new System.Drawing.Size(580, 29);
            this.txtFileName.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 38;
            this.label1.Text = "匯入來源";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Honeydew;
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(21, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(163, 30);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "資料來源";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpfDlg1
            // 
            this.OpfDlg1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KSMrp.Properties.Resources.Excel_format3;
            this.pictureBox1.Location = new System.Drawing.Point(25, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(738, 203);
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnOK.ForeColor = System.Drawing.Color.LightCyan;
            this.btnOK.Location = new System.Drawing.Point(724, 41);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(39, 38);
            this.btnOK.TabIndex = 43;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.BackgroundImage")));
            this.btnOpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnOpenFile.ForeColor = System.Drawing.Color.LightCyan;
            this.btnOpenFile.Location = new System.Drawing.Point(681, 40);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(42, 39);
            this.btnOpenFile.TabIndex = 42;
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // frm_ExcelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 681);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ExcelImport";
            this.Text = "Excel 匯入";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_ExcelImport_FormClosing);
            this.Load += new System.EventHandler(this.frm_ExcelImport_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label label2;
        private nsKXMSUC.CustomTextBox txtFileName;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cbSheet;
        private nsKXMSUC.KXMSLV LV1;
        private System.Windows.Forms.OpenFileDialog OpfDlg1;
        internal System.Windows.Forms.Label lblLVCount;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}