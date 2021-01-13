namespace KSMrp
{
    partial class frmStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStore));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslAddStore = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtStoreNo = new nsKXMSUC.CustomTextBox();
            this.txtStoreTypeDesc = new nsKXMSUC.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LV1 = new nsKXMSUC.KXMSLV();
            this.chcaption = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chblank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMTypeID = new nsKXMSUC.DigitTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHeight = new nsKXMSUC.DigitTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWidth = new nsKXMSUC.DigitTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDepth = new nsKXMSUC.DigitTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPos = new nsKXMSUC.DigitTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCarry = new System.Windows.Forms.ComboBox();
            this.cbMachineNo = new System.Windows.Forms.ComboBox();
            this.LV2 = new nsKXMSUC.KXMSLV();
            this.btnDelCarry = new System.Windows.Forms.Button();
            this.btnAddCarry = new System.Windows.Forms.Button();
            this.btnCopyCarry = new System.Windows.Forms.Button();
            this.btnDelete2 = new System.Windows.Forms.Button();
            this.btnEdit2 = new System.Windows.Forms.Button();
            this.btnAdd2 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslAddStore,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(644, 37);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslAddStore
            // 
            this.tslAddStore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslAddStore.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslAddStore.Image = ((System.Drawing.Image)(resources.GetObject("tslAddStore.Image")));
            this.tslAddStore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tslAddStore.Name = "tslAddStore";
            this.tslAddStore.Size = new System.Drawing.Size(125, 34);
            this.tslAddStore.Text = "新增自動倉設備";
            this.tslAddStore.Click += new System.EventHandler(this.tslAddStore_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.Location = new System.Drawing.Point(0, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 353);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnEdit);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.txtStoreNo);
            this.tabPage1.Controls.Add(this.txtStoreTypeDesc);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.LV1);
            this.tabPage1.Font = new System.Drawing.Font("新細明體", 12F);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "平置倉";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDelete.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDelete.Location = new System.Drawing.Point(560, 122);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 32);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "刪除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEdit.Location = new System.Drawing.Point(428, 122);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(67, 32);
            this.btnEdit.TabIndex = 20;
            this.btnEdit.Text = "修改";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAdd.Location = new System.Drawing.Point(355, 122);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 32);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtStoreNo
            // 
            this.txtStoreNo.Location = new System.Drawing.Point(488, 243);
            this.txtStoreNo.Name = "txtStoreNo";
            this.txtStoreNo.PlaceHolder = "";
            this.txtStoreNo.Size = new System.Drawing.Size(58, 27);
            this.txtStoreNo.TabIndex = 5;
            this.txtStoreNo.Visible = false;
            // 
            // txtStoreTypeDesc
            // 
            this.txtStoreTypeDesc.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtStoreTypeDesc.Location = new System.Drawing.Point(355, 89);
            this.txtStoreTypeDesc.Name = "txtStoreTypeDesc";
            this.txtStoreTypeDesc.PlaceHolder = "";
            this.txtStoreTypeDesc.Size = new System.Drawing.Size(200, 29);
            this.txtStoreTypeDesc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(261, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "平置倉名稱";
            // 
            // LV1
            // 
            this.LV1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chcaption,
            this.chblank});
            this.LV1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LV1.FullRowSelect = true;
            this.LV1.GridLines = true;
            this.LV1.HideSelection = false;
            this.LV1.Location = new System.Drawing.Point(7, 6);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(234, 309);
            this.LV1.TabIndex = 0;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            this.LV1.SelectedIndexChanged += new System.EventHandler(this.LV1_SelectedIndexChanged);
            // 
            // chcaption
            // 
            this.chcaption.Text = "樣板說明";
            this.chcaption.Width = 100;
            // 
            // chblank
            // 
            this.chblank.Text = "";
            this.chblank.Width = 100;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "自動倉";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.cbCarry);
            this.groupBox1.Controls.Add(this.cbMachineNo);
            this.groupBox1.Controls.Add(this.LV2);
            this.groupBox1.Controls.Add(this.btnDelCarry);
            this.groupBox1.Controls.Add(this.btnAddCarry);
            this.groupBox1.Controls.Add(this.btnCopyCarry);
            this.groupBox1.Controls.Add(this.btnDelete2);
            this.groupBox1.Controls.Add(this.btnEdit2);
            this.groupBox1.Controls.Add(this.btnAdd2);
            this.groupBox1.Location = new System.Drawing.Point(7, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 320);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.txtMTypeID);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtHeight);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtWidth);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtDepth);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtPos);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Enabled = false;
            this.panel2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.panel2.Location = new System.Drawing.Point(325, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(294, 224);
            this.panel2.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(162, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "22-Tray 23-Reel";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCancel.Location = new System.Drawing.Point(221, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 43);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGreen;
            this.btnSave.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSave.Location = new System.Drawing.Point(144, 175);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 43);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMTypeID
            // 
            this.txtMTypeID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMTypeID.Location = new System.Drawing.Point(82, 147);
            this.txtMTypeID.Name = "txtMTypeID";
            this.txtMTypeID.PlaceHolder = "";
            this.txtMTypeID.ShortcutsEnabled = false;
            this.txtMTypeID.Size = new System.Drawing.Size(67, 29);
            this.txtMTypeID.TabIndex = 15;
            this.txtMTypeID.Text = "22";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(6, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "MTypeID";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtHeight.Location = new System.Drawing.Point(82, 112);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.PlaceHolder = "";
            this.txtHeight.ShortcutsEnabled = false;
            this.txtHeight.Size = new System.Drawing.Size(67, 29);
            this.txtHeight.TabIndex = 13;
            this.txtHeight.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(51, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "深";
            // 
            // txtWidth
            // 
            this.txtWidth.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtWidth.Location = new System.Drawing.Point(82, 77);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.PlaceHolder = "";
            this.txtWidth.ShortcutsEnabled = false;
            this.txtWidth.Size = new System.Drawing.Size(67, 29);
            this.txtWidth.TabIndex = 11;
            this.txtWidth.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(51, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "寬";
            // 
            // txtDepth
            // 
            this.txtDepth.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDepth.Location = new System.Drawing.Point(82, 40);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.PlaceHolder = "";
            this.txtDepth.ShortcutsEnabled = false;
            this.txtDepth.Size = new System.Drawing.Size(67, 29);
            this.txtDepth.TabIndex = 9;
            this.txtDepth.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "縱向燈號";
            // 
            // txtPos
            // 
            this.txtPos.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPos.Location = new System.Drawing.Point(82, 5);
            this.txtPos.Name = "txtPos";
            this.txtPos.PlaceHolder = "";
            this.txtPos.ShortcutsEnabled = false;
            this.txtPos.Size = new System.Drawing.Size(67, 29);
            this.txtPos.TabIndex = 7;
            this.txtPos.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "橫向燈號";
            // 
            // cbCarry
            // 
            this.cbCarry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCarry.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbCarry.FormattingEnabled = true;
            this.cbCarry.Location = new System.Drawing.Point(168, 18);
            this.cbCarry.Name = "cbCarry";
            this.cbCarry.Size = new System.Drawing.Size(96, 28);
            this.cbCarry.TabIndex = 25;
            this.cbCarry.SelectedIndexChanged += new System.EventHandler(this.cbCarry_SelectedIndexChanged);
            // 
            // cbMachineNo
            // 
            this.cbMachineNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMachineNo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbMachineNo.FormattingEnabled = true;
            this.cbMachineNo.Location = new System.Drawing.Point(35, 18);
            this.cbMachineNo.Name = "cbMachineNo";
            this.cbMachineNo.Size = new System.Drawing.Size(96, 28);
            this.cbMachineNo.TabIndex = 24;
            this.cbMachineNo.SelectedIndexChanged += new System.EventHandler(this.cbMachineNo_SelectedIndexChanged);
            // 
            // LV2
            // 
            this.LV2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LV2.FullRowSelect = true;
            this.LV2.GridLines = true;
            this.LV2.HideSelection = false;
            this.LV2.Location = new System.Drawing.Point(6, 55);
            this.LV2.Name = "LV2";
            this.LV2.Size = new System.Drawing.Size(316, 259);
            this.LV2.TabIndex = 23;
            this.LV2.UseCompatibleStateImageBehavior = false;
            this.LV2.View = System.Windows.Forms.View.Details;
            this.LV2.SelectedIndexChanged += new System.EventHandler(this.LV2_SelectedIndexChanged);
            // 
            // btnDelCarry
            // 
            this.btnDelCarry.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDelCarry.Location = new System.Drawing.Point(418, 16);
            this.btnDelCarry.Name = "btnDelCarry";
            this.btnDelCarry.Size = new System.Drawing.Size(84, 32);
            this.btnDelCarry.TabIndex = 22;
            this.btnDelCarry.Text = "刪除盤號";
            this.btnDelCarry.UseVisualStyleBackColor = true;
            this.btnDelCarry.Click += new System.EventHandler(this.btnDelCarry_Click);
            // 
            // btnAddCarry
            // 
            this.btnAddCarry.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAddCarry.Location = new System.Drawing.Point(328, 16);
            this.btnAddCarry.Name = "btnAddCarry";
            this.btnAddCarry.Size = new System.Drawing.Size(84, 32);
            this.btnAddCarry.TabIndex = 21;
            this.btnAddCarry.Text = "新增盤號";
            this.btnAddCarry.UseVisualStyleBackColor = true;
            this.btnAddCarry.Click += new System.EventHandler(this.btnAddCarry_Click);
            // 
            // btnCopyCarry
            // 
            this.btnCopyCarry.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCopyCarry.Location = new System.Drawing.Point(530, 16);
            this.btnCopyCarry.Name = "btnCopyCarry";
            this.btnCopyCarry.Size = new System.Drawing.Size(84, 32);
            this.btnCopyCarry.TabIndex = 20;
            this.btnCopyCarry.Text = "複製規劃";
            this.btnCopyCarry.UseVisualStyleBackColor = true;
            this.btnCopyCarry.Click += new System.EventHandler(this.btnCopyCarry_Click);
            // 
            // btnDelete2
            // 
            this.btnDelete2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDelete2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDelete2.Location = new System.Drawing.Point(546, 57);
            this.btnDelete2.Name = "btnDelete2";
            this.btnDelete2.Size = new System.Drawing.Size(67, 32);
            this.btnDelete2.TabIndex = 18;
            this.btnDelete2.Text = "刪除";
            this.btnDelete2.UseVisualStyleBackColor = false;
            this.btnDelete2.Click += new System.EventHandler(this.btndelete2_Click);
            // 
            // btnEdit2
            // 
            this.btnEdit2.BackColor = System.Drawing.Color.PaleGreen;
            this.btnEdit2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEdit2.Location = new System.Drawing.Point(401, 57);
            this.btnEdit2.Name = "btnEdit2";
            this.btnEdit2.Size = new System.Drawing.Size(67, 32);
            this.btnEdit2.TabIndex = 5;
            this.btnEdit2.Text = "修改";
            this.btnEdit2.UseVisualStyleBackColor = false;
            this.btnEdit2.Click += new System.EventHandler(this.btnEdit2_Click);
            // 
            // btnAdd2
            // 
            this.btnAdd2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnAdd2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAdd2.Location = new System.Drawing.Point(328, 57);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(67, 32);
            this.btnAdd2.TabIndex = 4;
            this.btnAdd2.Text = "新增";
            this.btnAdd2.UseVisualStyleBackColor = false;
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
            // 
            // frmStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 388);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "儲位管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStore_FormClosing);
            this.Load += new System.EventHandler(this.frmStore_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton tslAddStore;
        private nsKXMSUC.CustomTextBox txtStoreTypeDesc;
        private System.Windows.Forms.Label label1;
        private nsKXMSUC.KXMSLV LV1;
        private System.Windows.Forms.ColumnHeader chcaption;
        private System.Windows.Forms.ColumnHeader chblank;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDelete2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private nsKXMSUC.DigitTextBox txtMTypeID;
        private System.Windows.Forms.Label label6;
        private nsKXMSUC.DigitTextBox txtHeight;
        private System.Windows.Forms.Label label5;
        private nsKXMSUC.DigitTextBox txtWidth;
        private System.Windows.Forms.Label label4;
        private nsKXMSUC.DigitTextBox txtDepth;
        private System.Windows.Forms.Label label3;
        private nsKXMSUC.DigitTextBox txtPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEdit2;
        private System.Windows.Forms.Button btnAdd2;
        private System.Windows.Forms.Button btnCopyCarry;
        private System.Windows.Forms.Button btnDelCarry;
        private System.Windows.Forms.Button btnAddCarry;
        private nsKXMSUC.CustomTextBox txtStoreNo;
        private nsKXMSUC.KXMSLV LV2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbCarry;
        private System.Windows.Forms.ComboBox cbMachineNo;
        private System.Windows.Forms.Panel panel2;
    }
}