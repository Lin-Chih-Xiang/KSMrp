namespace KSMrp
{
    partial class frmQMno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQMno));
            this.LV1 = new nsKXMSUC.KXMSLV();
            this.chNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMachineNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTray = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDepth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chposition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chorder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chlocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chpackage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chremark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMMID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.txtNumNo = new System.Windows.Forms.TextBox();
            this.btndemand = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSum = new System.Windows.Forms.Label();
            this.chIC = new System.Windows.Forms.CheckBox();
            this.txtmodify = new System.Windows.Forms.TextBox();
            this.txtmodifyNumNo = new System.Windows.Forms.TextBox();
            this.btnRemark = new System.Windows.Forms.Button();
            this.btnNub = new System.Windows.Forms.Button();
            this.labelData = new System.Windows.Forms.Label();
            this.txtMID = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOverdue = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExport1 = new System.Windows.Forms.Button();
            this.imgList48 = new System.Windows.Forms.ImageList(this.components);
            this.SFDlg1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LV1
            // 
            this.LV1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNo,
            this.chMachineNo,
            this.chTray,
            this.chPos,
            this.chDepth,
            this.chQty,
            this.chposition,
            this.chorder,
            this.chlocation,
            this.chpackage,
            this.chremark,
            this.chMMID});
            this.LV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV1.Font = new System.Drawing.Font("新細明體", 12F);
            this.LV1.FullRowSelect = true;
            this.LV1.GridLines = true;
            this.LV1.HideSelection = false;
            this.LV1.Location = new System.Drawing.Point(0, 0);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(1390, 504);
            this.LV1.TabIndex = 8;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            this.LV1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LV1_ItemSelectionChanged);
            // 
            // chNo
            // 
            this.chNo.Text = "料號";
            this.chNo.Width = 130;
            // 
            // chMachineNo
            // 
            this.chMachineNo.Text = "機器";
            this.chMachineNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chTray
            // 
            this.chTray.Text = "層數";
            this.chTray.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chPos
            // 
            this.chPos.Text = "燈號 x";
            this.chPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chDepth
            // 
            this.chDepth.Text = "燈號 y";
            this.chDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chQty
            // 
            this.chQty.Text = "數量";
            this.chQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chQty.Width = 50;
            // 
            // chposition
            // 
            this.chposition.Text = "庫位";
            this.chposition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chorder
            // 
            this.chorder.Text = "工單單號";
            this.chorder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chorder.Width = 80;
            // 
            // chlocation
            // 
            this.chlocation.Text = "Location";
            this.chlocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chlocation.Width = 80;
            // 
            // chpackage
            // 
            this.chpackage.Text = "包裝";
            this.chpackage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chpackage.Width = 110;
            // 
            // chremark
            // 
            this.chremark.Text = "備註";
            this.chremark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chremark.Width = 100;
            // 
            // chMMID
            // 
            this.chMMID.Text = "MMainID";
            this.chMMID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chMMID.Width = 80;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(50, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "料號";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "工單單號";
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNo.Location = new System.Drawing.Point(100, 8);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(229, 30);
            this.txtNo.TabIndex = 1;
            this.txtNo.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.txtNo.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtNumNo
            // 
            this.txtNumNo.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtNumNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtNumNo.Location = new System.Drawing.Point(100, 45);
            this.txtNumNo.Name = "txtNumNo";
            this.txtNumNo.Size = new System.Drawing.Size(229, 30);
            this.txtNumNo.TabIndex = 2;
            this.txtNumNo.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtNumNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.txtNumNo.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // btndemand
            // 
            this.btndemand.Font = new System.Drawing.Font("標楷體", 13F);
            this.btndemand.Location = new System.Drawing.Point(335, 44);
            this.btndemand.Name = "btndemand";
            this.btndemand.Size = new System.Drawing.Size(100, 32);
            this.btndemand.TabIndex = 4;
            this.btndemand.Text = "查詢";
            this.btndemand.UseVisualStyleBackColor = true;
            this.btndemand.Click += new System.EventHandler(this.btndemand_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(335, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "總量=>";
            // 
            // labelSum
            // 
            this.labelSum.AutoSize = true;
            this.labelSum.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSum.ForeColor = System.Drawing.Color.Blue;
            this.labelSum.Location = new System.Drawing.Point(387, 14);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(16, 16);
            this.labelSum.TabIndex = 6;
            this.labelSum.Text = "0";
            // 
            // chIC
            // 
            this.chIC.AutoSize = true;
            this.chIC.Font = new System.Drawing.Font("標楷體", 13F);
            this.chIC.Location = new System.Drawing.Point(495, 14);
            this.chIC.Name = "chIC";
            this.chIC.Size = new System.Drawing.Size(99, 22);
            this.chIC.TabIndex = 3;
            this.chIC.Text = "相似查詢";
            this.chIC.UseVisualStyleBackColor = true;
            this.chIC.Visible = false;
            // 
            // txtmodify
            // 
            this.txtmodify.BackColor = System.Drawing.SystemColors.Window;
            this.txtmodify.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtmodify.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtmodify.Location = new System.Drawing.Point(596, 10);
            this.txtmodify.Name = "txtmodify";
            this.txtmodify.Size = new System.Drawing.Size(150, 30);
            this.txtmodify.TabIndex = 5;
            this.txtmodify.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtmodify.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.txtmodify.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtmodifyNumNo
            // 
            this.txtmodifyNumNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtmodifyNumNo.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtmodifyNumNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtmodifyNumNo.Location = new System.Drawing.Point(596, 47);
            this.txtmodifyNumNo.Name = "txtmodifyNumNo";
            this.txtmodifyNumNo.Size = new System.Drawing.Size(150, 30);
            this.txtmodifyNumNo.TabIndex = 7;
            this.txtmodifyNumNo.Enter += new System.EventHandler(this.TextBox_Enter);
            this.txtmodifyNumNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.txtmodifyNumNo.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // btnRemark
            // 
            this.btnRemark.Font = new System.Drawing.Font("標楷體", 13F);
            this.btnRemark.Location = new System.Drawing.Point(752, 9);
            this.btnRemark.Name = "btnRemark";
            this.btnRemark.Size = new System.Drawing.Size(100, 32);
            this.btnRemark.TabIndex = 6;
            this.btnRemark.Text = "修改備註";
            this.btnRemark.UseVisualStyleBackColor = true;
            this.btnRemark.Click += new System.EventHandler(this.btnRemark_Click);
            // 
            // btnNub
            // 
            this.btnNub.Font = new System.Drawing.Font("標楷體", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnNub.Location = new System.Drawing.Point(752, 46);
            this.btnNub.Name = "btnNub";
            this.btnNub.Size = new System.Drawing.Size(100, 32);
            this.btnNub.TabIndex = 8;
            this.btnNub.Text = "修改工單";
            this.btnNub.UseVisualStyleBackColor = true;
            this.btnNub.Click += new System.EventHandler(this.btnNub_Click);
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelData.ForeColor = System.Drawing.Color.Blue;
            this.labelData.Location = new System.Drawing.Point(858, 18);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(200, 16);
            this.labelData.TabIndex = 13;
            this.labelData.Text = "                        ";
            // 
            // txtMID
            // 
            this.txtMID.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMID.Location = new System.Drawing.Point(950, 44);
            this.txtMID.Name = "txtMID";
            this.txtMID.Size = new System.Drawing.Size(46, 30);
            this.txtMID.TabIndex = 14;
            this.txtMID.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport1);
            this.panel1.Controls.Add(this.lblOverdue);
            this.panel1.Controls.Add(this.txtMID);
            this.panel1.Controls.Add(this.btnNub);
            this.panel1.Controls.Add(this.labelData);
            this.panel1.Controls.Add(this.btnRemark);
            this.panel1.Controls.Add(this.txtmodifyNumNo);
            this.panel1.Controls.Add(this.txtmodify);
            this.panel1.Controls.Add(this.chIC);
            this.panel1.Controls.Add(this.labelSum);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btndemand);
            this.panel1.Controls.Add(this.txtNumNo);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1390, 82);
            this.panel1.TabIndex = 15;
            // 
            // lblOverdue
            // 
            this.lblOverdue.AutoSize = true;
            this.lblOverdue.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOverdue.ForeColor = System.Drawing.Color.Red;
            this.lblOverdue.Location = new System.Drawing.Point(1188, 59);
            this.lblOverdue.Name = "lblOverdue";
            this.lblOverdue.Size = new System.Drawing.Size(149, 19);
            this.lblOverdue.TabIndex = 15;
            this.lblOverdue.Text = "逾期件數：0 件";
            this.lblOverdue.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LV1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1390, 504);
            this.panel2.TabIndex = 16;
            // 
            // btnExport1
            // 
            this.btnExport1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExport1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExport1.ImageIndex = 0;
            this.btnExport1.ImageList = this.imgList48;
            this.btnExport1.Location = new System.Drawing.Point(1100, 1);
            this.btnExport1.Name = "btnExport1";
            this.btnExport1.Size = new System.Drawing.Size(84, 80);
            this.btnExport1.TabIndex = 16;
            this.btnExport1.Text = "Excel匯出";
            this.btnExport1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExport1.UseVisualStyleBackColor = true;
            this.btnExport1.Visible = false;
            this.btnExport1.Click += new System.EventHandler(this.btnExport1_Click);
            // 
            // imgList48
            // 
            this.imgList48.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList48.ImageStream")));
            this.imgList48.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList48.Images.SetKeyName(0, "if_logo_brand_brands_logos_excel_2993694.png");
            this.imgList48.Images.SetKeyName(1, "if_search_48_10348.png");
            // 
            // frmQMno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 586);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQMno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "物料查詢";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQMno_FormClosing);
            this.Load += new System.EventHandler(this.frmQMno_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private nsKXMSUC.KXMSLV LV1;
        private System.Windows.Forms.ColumnHeader chNo;
        private System.Windows.Forms.ColumnHeader chMachineNo;
        private System.Windows.Forms.ColumnHeader chTray;
        private System.Windows.Forms.ColumnHeader chPos;
        private System.Windows.Forms.ColumnHeader chDepth;
        private System.Windows.Forms.ColumnHeader chQty;
        private System.Windows.Forms.ColumnHeader chposition;
        private System.Windows.Forms.ColumnHeader chorder;
        private System.Windows.Forms.ColumnHeader chlocation;
        private System.Windows.Forms.ColumnHeader chpackage;
        private System.Windows.Forms.ColumnHeader chremark;
        private System.Windows.Forms.ColumnHeader chMMID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.TextBox txtNumNo;
        private System.Windows.Forms.Button btndemand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.CheckBox chIC;
        private System.Windows.Forms.TextBox txtmodify;
        private System.Windows.Forms.TextBox txtmodifyNumNo;
        private System.Windows.Forms.Button btnRemark;
        private System.Windows.Forms.Button btnNub;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.TextBox txtMID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblOverdue;
        private System.Windows.Forms.Button btnExport1;
        private System.Windows.Forms.ImageList imgList48;
        private System.Windows.Forms.SaveFileDialog SFDlg1;
    }
}