namespace KSMrp
{
    partial class frm_StoreMultiIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_StoreMultiIn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTop = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtAnt = new System.Windows.Forms.TextBox();
            this.txtOV = new System.Windows.Forms.TextBox();
            this.btnLVInListClear = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LVInList = new nsKXMSUC.KXMSLV();
            this.lbrbOp = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbOp2 = new System.Windows.Forms.RadioButton();
            this.rbOp1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStoreNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDataP3 = new System.Windows.Forms.ComboBox();
            this.btnQueue = new System.Windows.Forms.Button();
            this.lblStoreHouse = new System.Windows.Forms.Label();
            this.cbStoreHouse = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1066, 50);
            this.panel1.TabIndex = 0;
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.Color.Navy;
            this.btnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTop.Font = new System.Drawing.Font("微軟正黑體", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Location = new System.Drawing.Point(0, 0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(1066, 50);
            this.btnTop.TabIndex = 0;
            this.btnTop.Text = "批次入庫";
            this.btnTop.UseVisualStyleBackColor = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Excels.ico");
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Wheat;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtAnt);
            this.panel3.Controls.Add(this.txtOV);
            this.panel3.Controls.Add(this.btnLVInListClear);
            this.panel3.Controls.Add(this.btnImport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1066, 52);
            this.panel3.TabIndex = 33;
            // 
            // txtAnt
            // 
            this.txtAnt.Location = new System.Drawing.Point(930, 28);
            this.txtAnt.Name = "txtAnt";
            this.txtAnt.Size = new System.Drawing.Size(73, 22);
            this.txtAnt.TabIndex = 87;
            this.txtAnt.Visible = false;
            // 
            // txtOV
            // 
            this.txtOV.Location = new System.Drawing.Point(857, 5);
            this.txtOV.Name = "txtOV";
            this.txtOV.Size = new System.Drawing.Size(203, 22);
            this.txtOV.TabIndex = 86;
            this.txtOV.Visible = false;
            // 
            // btnLVInListClear
            // 
            this.btnLVInListClear.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.btnLVInListClear.Image = ((System.Drawing.Image)(resources.GetObject("btnLVInListClear.Image")));
            this.btnLVInListClear.Location = new System.Drawing.Point(112, 7);
            this.btnLVInListClear.Name = "btnLVInListClear";
            this.btnLVInListClear.Size = new System.Drawing.Size(87, 36);
            this.btnLVInListClear.TabIndex = 34;
            this.btnLVInListClear.Text = "清除";
            this.btnLVInListClear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLVInListClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLVInListClear.UseVisualStyleBackColor = true;
            this.btnLVInListClear.Click += new System.EventHandler(this.btnLVInListClear_Click);
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Location = new System.Drawing.Point(16, 7);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(87, 36);
            this.btnImport.TabIndex = 33;
            this.btnImport.Text = "匯入";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1066, 492);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LVInList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbrbOp);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.rbOp2);
            this.splitContainer1.Panel2.Controls.Add(this.rbOp1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.txtStoreNo);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.cbDataP3);
            this.splitContainer1.Panel2.Controls.Add(this.btnQueue);
            this.splitContainer1.Panel2.Controls.Add(this.lblStoreHouse);
            this.splitContainer1.Panel2.Controls.Add(this.cbStoreHouse);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(1066, 440);
            this.splitContainer1.SplitterDistance = 851;
            this.splitContainer1.TabIndex = 59;
            // 
            // LVInList
            // 
            this.LVInList.CheckBoxes = true;
            this.LVInList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LVInList.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LVInList.FullRowSelect = true;
            this.LVInList.GridLines = true;
            this.LVInList.HideSelection = false;
            this.LVInList.Location = new System.Drawing.Point(0, 0);
            this.LVInList.Name = "LVInList";
            this.LVInList.Size = new System.Drawing.Size(851, 440);
            this.LVInList.TabIndex = 50;
            this.LVInList.UseCompatibleStateImageBehavior = false;
            this.LVInList.View = System.Windows.Forms.View.Details;
            // 
            // lbrbOp
            // 
            this.lbrbOp.BackColor = System.Drawing.Color.White;
            this.lbrbOp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbrbOp.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbrbOp.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbrbOp.ForeColor = System.Drawing.Color.Red;
            this.lbrbOp.Location = new System.Drawing.Point(114, 299);
            this.lbrbOp.Name = "lbrbOp";
            this.lbrbOp.Size = new System.Drawing.Size(83, 26);
            this.lbrbOp.TabIndex = 85;
            this.lbrbOp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbrbOp.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(72, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 21);
            this.label3.TabIndex = 84;
            this.label3.Text = "空位";
            this.label3.Visible = false;
            // 
            // rbOp2
            // 
            this.rbOp2.AutoSize = true;
            this.rbOp2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbOp2.ForeColor = System.Drawing.Color.Black;
            this.rbOp2.Location = new System.Drawing.Point(115, 262);
            this.rbOp2.Name = "rbOp2";
            this.rbOp2.Size = new System.Drawing.Size(91, 25);
            this.rbOp2.TabIndex = 82;
            this.rbOp2.Text = "Option2";
            this.rbOp2.UseVisualStyleBackColor = true;
            this.rbOp2.CheckedChanged += new System.EventHandler(this.rbOp2_CheckedChanged);
            // 
            // rbOp1
            // 
            this.rbOp1.AutoSize = true;
            this.rbOp1.Checked = true;
            this.rbOp1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rbOp1.ForeColor = System.Drawing.Color.Black;
            this.rbOp1.Location = new System.Drawing.Point(22, 262);
            this.rbOp1.Name = "rbOp1";
            this.rbOp1.Size = new System.Drawing.Size(91, 25);
            this.rbOp1.TabIndex = 81;
            this.rbOp1.TabStop = true;
            this.rbOp1.Text = "Option1";
            this.rbOp1.UseVisualStyleBackColor = true;
            this.rbOp1.CheckedChanged += new System.EventHandler(this.rbOp1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(20, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 22);
            this.label1.TabIndex = 80;
            this.label1.Text = "包裝：";
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(157, 176);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.Size = new System.Drawing.Size(40, 22);
            this.txtStoreNo.TabIndex = 79;
            this.txtStoreNo.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(20, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 22);
            this.label2.TabIndex = 77;
            this.label2.Text = "平置倉：";
            // 
            // cbDataP3
            // 
            this.cbDataP3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbDataP3.FormattingEnabled = true;
            this.cbDataP3.Location = new System.Drawing.Point(20, 176);
            this.cbDataP3.Name = "cbDataP3";
            this.cbDataP3.Size = new System.Drawing.Size(127, 24);
            this.cbDataP3.Sorted = true;
            this.cbDataP3.TabIndex = 76;
            this.cbDataP3.SelectedIndexChanged += new System.EventHandler(this.cbDataP3_SelectedIndexChanged);
            // 
            // btnQueue
            // 
            this.btnQueue.BackColor = System.Drawing.Color.PaleGreen;
            this.btnQueue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueue.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnQueue.Location = new System.Drawing.Point(72, 370);
            this.btnQueue.Name = "btnQueue";
            this.btnQueue.Size = new System.Drawing.Size(125, 31);
            this.btnQueue.TabIndex = 73;
            this.btnQueue.Text = "批次入庫";
            this.btnQueue.UseVisualStyleBackColor = false;
            this.btnQueue.Click += new System.EventHandler(this.btnQueue_Click);
            // 
            // lblStoreHouse
            // 
            this.lblStoreHouse.AutoSize = true;
            this.lblStoreHouse.Font = new System.Drawing.Font("微軟正黑體", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblStoreHouse.Location = new System.Drawing.Point(20, 58);
            this.lblStoreHouse.Name = "lblStoreHouse";
            this.lblStoreHouse.Size = new System.Drawing.Size(61, 22);
            this.lblStoreHouse.TabIndex = 71;
            this.lblStoreHouse.Text = "庫位：";
            // 
            // cbStoreHouse
            // 
            this.cbStoreHouse.Font = new System.Drawing.Font("新細明體", 12F);
            this.cbStoreHouse.FormattingEnabled = true;
            this.cbStoreHouse.Location = new System.Drawing.Point(20, 92);
            this.cbStoreHouse.Name = "cbStoreHouse";
            this.cbStoreHouse.Size = new System.Drawing.Size(107, 24);
            this.cbStoreHouse.TabIndex = 70;
            // 
            // frm_StoreMultiIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 542);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_StoreMultiIn";
            this.Text = "批次入庫";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_StoreMultiIn_FormClosing);
            this.Load += new System.EventHandler(this.frm_StoreMultiIn_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLVInListClear;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private nsKXMSUC.KXMSLV LVInList;
        private System.Windows.Forms.Button btnQueue;
        private System.Windows.Forms.Label lblStoreHouse;
        private System.Windows.Forms.ComboBox cbStoreHouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDataP3;
        private System.Windows.Forms.TextBox txtStoreNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbOp2;
        private System.Windows.Forms.RadioButton rbOp1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbrbOp;
        private System.Windows.Forms.TextBox txtOV;
        private System.Windows.Forms.TextBox txtAnt;
    }
}