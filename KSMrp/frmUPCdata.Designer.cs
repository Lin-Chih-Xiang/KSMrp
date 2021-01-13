namespace KSMrp
{
    partial class frmUPCdata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUPCdata));
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnAddSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNub = new nsKXMSUC.CustomTextBox();
            this.txtUPC = new nsKXMSUC.DigitTextBox();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnQuery.Location = new System.Drawing.Point(298, 16);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(85, 28);
            this.btnQuery.TabIndex = 12;
            this.btnQuery.Text = "查詢";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnAddSave
            // 
            this.btnAddSave.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnAddSave.Location = new System.Drawing.Point(298, 51);
            this.btnAddSave.Name = "btnAddSave";
            this.btnAddSave.Size = new System.Drawing.Size(85, 28);
            this.btnAddSave.TabIndex = 11;
            this.btnAddSave.Text = "新增存檔";
            this.btnAddSave.UseVisualStyleBackColor = true;
            this.btnAddSave.Click += new System.EventHandler(this.btnAddSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 14F);
            this.label2.Location = new System.Drawing.Point(10, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "成品料號";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14F);
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "UPC料號";
            // 
            // txtNub
            // 
            this.txtNub.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtNub.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNub.Location = new System.Drawing.Point(94, 52);
            this.txtNub.Name = "txtNub";
            this.txtNub.PlaceHolder = "";
            this.txtNub.Size = new System.Drawing.Size(198, 27);
            this.txtNub.TabIndex = 14;
            // 
            // txtUPC
            // 
            this.txtUPC.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtUPC.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtUPC.Location = new System.Drawing.Point(94, 16);
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.PlaceHolder = "";
            this.txtUPC.ShortcutsEnabled = false;
            this.txtUPC.Size = new System.Drawing.Size(198, 27);
            this.txtUPC.TabIndex = 15;
            // 
            // frmUPCdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 94);
            this.Controls.Add(this.txtUPC);
            this.Controls.Add(this.txtNub);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnAddSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUPCdata";
            this.Text = "比對資料作業";
            this.Load += new System.EventHandler(this.frmUPCdata_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnAddSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private nsKXMSUC.CustomTextBox txtNub;
        private nsKXMSUC.DigitTextBox txtUPC;
    }
}