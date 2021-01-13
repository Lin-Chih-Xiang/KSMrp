namespace KSMrp
{
    partial class frmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "入庫作業"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), new System.Drawing.Font("新細明體", 12F));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "領料作業"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("新細明體", 12F));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("物料中心-新增比對資料");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("物料中心-盤點調整");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("物料中心-物料查詢");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("物料中心-成品倉庫存資料比對");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("報表中心");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("系統設定-使用者設定");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("系統設定-儲位管理");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("系統設定-新增包裝作業");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("系統設定-新增庫位作業");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("系統設定-錯誤資料維護");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("批次入庫");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("Shelf Life設定");
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TpbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.TpbtnEdit = new System.Windows.Forms.ToolStripButton();
            this.TpbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.TpbtnSave = new System.Windows.Forms.ToolStripButton();
            this.TpbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.cbName = new System.Windows.Forms.ComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.LV1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TpbtnAdd,
            this.TpbtnEdit,
            this.TpbtnDelete,
            this.TpbtnSave,
            this.TpbtnCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(460, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TpbtnAdd
            // 
            this.TpbtnAdd.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TpbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("TpbtnAdd.Image")));
            this.TpbtnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TpbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TpbtnAdd.Name = "TpbtnAdd";
            this.TpbtnAdd.Size = new System.Drawing.Size(60, 28);
            this.TpbtnAdd.Text = "新增";
            this.TpbtnAdd.Click += new System.EventHandler(this.TpbtnAdd_Click);
            // 
            // TpbtnEdit
            // 
            this.TpbtnEdit.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.TpbtnEdit.Image = global::KSMrp.Properties.Resources.modify;
            this.TpbtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TpbtnEdit.Name = "TpbtnEdit";
            this.TpbtnEdit.Size = new System.Drawing.Size(56, 28);
            this.TpbtnEdit.Text = "修改";
            this.TpbtnEdit.Click += new System.EventHandler(this.TpbtnEdit_Click);
            // 
            // TpbtnDelete
            // 
            this.TpbtnDelete.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.TpbtnDelete.Image = global::KSMrp.Properties.Resources.delete1;
            this.TpbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TpbtnDelete.Name = "TpbtnDelete";
            this.TpbtnDelete.Size = new System.Drawing.Size(56, 28);
            this.TpbtnDelete.Text = "刪除";
            this.TpbtnDelete.Click += new System.EventHandler(this.TpbtnDelete_Click);
            // 
            // TpbtnSave
            // 
            this.TpbtnSave.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.TpbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("TpbtnSave.Image")));
            this.TpbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TpbtnSave.Name = "TpbtnSave";
            this.TpbtnSave.Size = new System.Drawing.Size(56, 28);
            this.TpbtnSave.Text = "儲存";
            this.TpbtnSave.Click += new System.EventHandler(this.TpbtnSave_Click);
            // 
            // TpbtnCancel
            // 
            this.TpbtnCancel.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F);
            this.TpbtnCancel.Image = global::KSMrp.Properties.Resources.cancel;
            this.TpbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TpbtnCancel.Name = "TpbtnCancel";
            this.TpbtnCancel.Size = new System.Drawing.Size(56, 28);
            this.TpbtnCancel.Text = "取消";
            this.TpbtnCancel.Click += new System.EventHandler(this.TpbtnCancel_Click);
            // 
            // cbName
            // 
            this.cbName.Font = new System.Drawing.Font("新細明體", 12F);
            this.cbName.FormattingEnabled = true;
            this.cbName.Location = new System.Drawing.Point(12, 34);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(155, 24);
            this.cbName.TabIndex = 1;
            this.cbName.SelectedIndexChanged += new System.EventHandler(this.cbName_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(9, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "中文姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(9, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "登入帳號";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(9, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "登入密碼";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(9, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "確認密碼";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox1.Location = new System.Drawing.Point(81, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 27);
            this.textBox1.TabIndex = 1;
            this.textBox1.Enter += new System.EventHandler(this.TextBox_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox2.Location = new System.Drawing.Point(81, 114);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(86, 27);
            this.textBox2.TabIndex = 2;
            this.textBox2.Enter += new System.EventHandler(this.TextBox_Enter);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.textBox2.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox3.Location = new System.Drawing.Point(81, 157);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(86, 27);
            this.textBox3.TabIndex = 3;
            this.textBox3.Enter += new System.EventHandler(this.TextBox_Enter);
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.textBox3.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox4.Location = new System.Drawing.Point(81, 200);
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '*';
            this.textBox4.Size = new System.Drawing.Size(86, 27);
            this.textBox4.TabIndex = 4;
            this.textBox4.Enter += new System.EventHandler(this.TextBox_Enter);
            this.textBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            this.textBox4.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // LV1
            // 
            this.LV1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LV1.CheckBoxes = true;
            this.LV1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LV1.Font = new System.Drawing.Font("新細明體", 12F);
            this.LV1.FullRowSelect = true;
            this.LV1.GridLines = true;
            this.LV1.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.StateImageIndex = 0;
            listViewItem13.StateImageIndex = 0;
            listViewItem14.StateImageIndex = 0;
            this.LV1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14});
            this.LV1.Location = new System.Drawing.Point(182, 33);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(262, 194);
            this.LV1.TabIndex = 10;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "選擇開啟項目 ☑  ";
            this.columnHeader1.Width = 245;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox5.Location = new System.Drawing.Point(400, 0);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(44, 27);
            this.textBox5.TabIndex = 11;
            this.textBox5.Visible = false;
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 242);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.LV1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUser";
            this.Text = "使用者設定";
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TpbtnAdd;
        private System.Windows.Forms.ToolStripButton TpbtnEdit;
        private System.Windows.Forms.ToolStripButton TpbtnDelete;
        private System.Windows.Forms.ToolStripButton TpbtnSave;
        private System.Windows.Forms.ToolStripButton TpbtnCancel;
        private System.Windows.Forms.ComboBox cbName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ListView LV1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox textBox5;
    }
}