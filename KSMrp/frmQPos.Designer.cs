namespace KSMrp
{
    partial class frmQPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQPos));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btndemand = new System.Windows.Forms.Button();
            this.btnreorganize = new System.Windows.Forms.Button();
            this.LV1 = new nsKXMSUC.KXMSLV();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "料號";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "儲位";
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNo.Location = new System.Drawing.Point(55, 16);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(168, 27);
            this.txtNo.TabIndex = 1;
            this.txtNo.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtNo.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox1.Location = new System.Drawing.Point(55, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 27);
            this.textBox1.TabIndex = 2;
            this.textBox1.Enter += new System.EventHandler(this.TextBox_Enter);
            this.textBox1.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // btndemand
            // 
            this.btndemand.Font = new System.Drawing.Font("新細明體", 12F);
            this.btndemand.Location = new System.Drawing.Point(239, 21);
            this.btndemand.Name = "btndemand";
            this.btndemand.Size = new System.Drawing.Size(92, 45);
            this.btndemand.TabIndex = 3;
            this.btndemand.Text = "查詢";
            this.btndemand.UseVisualStyleBackColor = true;
            this.btndemand.Click += new System.EventHandler(this.btndemand_Click);
            // 
            // btnreorganize
            // 
            this.btnreorganize.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnreorganize.Location = new System.Drawing.Point(337, 21);
            this.btnreorganize.Name = "btnreorganize";
            this.btnreorganize.Size = new System.Drawing.Size(92, 44);
            this.btnreorganize.TabIndex = 4;
            this.btnreorganize.Text = "重新整理";
            this.btnreorganize.UseVisualStyleBackColor = true;
            this.btnreorganize.Click += new System.EventHandler(this.btnreorganize_Click);
            // 
            // LV1
            // 
            this.LV1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.LV1.Font = new System.Drawing.Font("新細明體", 12F);
            this.LV1.FullRowSelect = true;
            this.LV1.GridLines = true;
            this.LV1.HideSelection = false;
            this.LV1.Location = new System.Drawing.Point(12, 82);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(488, 258);
            this.LV1.TabIndex = 6;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            this.LV1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LV1_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "庫位";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "料號";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "儲位";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "庫存量";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 80;
            // 
            // frmQPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 352);
            this.Controls.Add(this.LV1);
            this.Controls.Add(this.btnreorganize);
            this.Controls.Add(this.btndemand);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQPos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "平置倉查詢";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQPos_FormClosing);
            this.Load += new System.EventHandler(this.frmQPos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btndemand;
        private System.Windows.Forms.Button btnreorganize;
        private nsKXMSUC.KXMSLV LV1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}