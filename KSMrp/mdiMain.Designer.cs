﻿namespace KSMrp
{
    partial class mdiMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiMain));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerQuit = new System.Windows.Forms.Timer(this.components);
            this.timerBuffer = new System.Windows.Forms.Timer(this.components);
            this.timerBackup = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TbIn = new System.Windows.Forms.ToolStripButton();
            this.TbIns = new System.Windows.Forms.ToolStripButton();
            this.TbOut = new System.Windows.Forms.ToolStripButton();
            this.TbOuts = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.TbUPCdata = new System.Windows.Forms.ToolStripMenuItem();
            this.TbAdj = new System.Windows.Forms.ToolStripMenuItem();
            this.TbQMno = new System.Windows.Forms.ToolStripMenuItem();
            this.TbStorePos = new System.Windows.Forms.ToolStripMenuItem();
            this.TbQPos = new System.Windows.Forms.ToolStripMenuItem();
            this.TbReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.TbUser = new System.Windows.Forms.ToolStripMenuItem();
            this.TbStore = new System.Windows.Forms.ToolStripMenuItem();
            this.TbPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.TbPackage = new System.Windows.Forms.ToolStripMenuItem();
            this.TbStoreHouse = new System.Windows.Forms.ToolStripMenuItem();
            this.TbError = new System.Windows.Forms.ToolStripMenuItem();
            this.重新產生報表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TbShelf = new System.Windows.Forms.ToolStripMenuItem();
            this.TbQuit = new System.Windows.Forms.ToolStripButton();
            this.TbLogOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.操作說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關於自動倉儲管理系統ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBuf = new System.Windows.Forms.Panel();
            this.Btnbuf = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerQuit
            // 
            this.timerQuit.Interval = 200;
            // 
            // timerBuffer
            // 
            this.timerBuffer.Enabled = true;
            this.timerBuffer.Interval = 5000;
            // 
            // timerBackup
            // 
            this.timerBackup.Interval = 60000;
            this.timerBackup.Tick += new System.EventHandler(this.timerBackup_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(943, 36);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 31);
            this.toolStripStatusLabel1.Text = "使用者";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.BackColor = System.Drawing.Color.Gainsboro;
            this.toolStripStatusLabel4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("新細明體", 12F);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(95, 31);
            this.toolStripStatusLabel4.Text = "使用者帳號";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(701, 31);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "自動倉儲管理系統";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(76, 31);
            this.toolStripStatusLabel3.Text = "現在時間";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TbIn,
            this.TbIns,
            this.TbOut,
            this.TbOuts,
            this.TbReport,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.TbQuit,
            this.TbLogOut,
            this.toolStripDropDownButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(943, 37);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TbIn
            // 
            this.TbIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbIn.Image = global::KSMrp.Properties.Resources.入庫;
            this.TbIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbIn.Name = "TbIn";
            this.TbIn.Size = new System.Drawing.Size(102, 34);
            this.TbIn.Text = "入庫作業";
            this.TbIn.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // TbIns
            // 
            this.TbIns.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbIns.Image = global::KSMrp.Properties.Resources.批次入庫;
            this.TbIns.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbIns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbIns.Name = "TbIns";
            this.TbIns.Size = new System.Drawing.Size(102, 34);
            this.TbIns.Text = "批次入庫";
            this.TbIns.Click += new System.EventHandler(this.TbIns_Click);
            // 
            // TbOut
            // 
            this.TbOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbOut.Image = global::KSMrp.Properties.Resources.領料;
            this.TbOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbOut.Name = "TbOut";
            this.TbOut.Size = new System.Drawing.Size(102, 34);
            this.TbOut.Text = "領料作業";
            this.TbOut.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // TbOuts
            // 
            this.TbOuts.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbOuts.Image = global::KSMrp.Properties.Resources.批次領料;
            this.TbOuts.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbOuts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbOuts.Name = "TbOuts";
            this.TbOuts.Size = new System.Drawing.Size(102, 34);
            this.TbOuts.Text = "批次領料";
            this.TbOuts.Visible = false;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TbUPCdata,
            this.TbAdj,
            this.TbQMno,
            this.TbStorePos,
            this.TbQPos});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripDropDownButton1.Image = global::KSMrp.Properties.Resources.物料中心;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(111, 34);
            this.toolStripDropDownButton1.Text = "物料中心";
            // 
            // TbUPCdata
            // 
            this.TbUPCdata.Name = "TbUPCdata";
            this.TbUPCdata.Size = new System.Drawing.Size(181, 22);
            this.TbUPCdata.Text = "比對資料作業";
            this.TbUPCdata.Click += new System.EventHandler(this.比對資料作業ToolStripMenuItem_Click);
            // 
            // TbAdj
            // 
            this.TbAdj.MergeIndex = 1;
            this.TbAdj.Name = "TbAdj";
            this.TbAdj.Size = new System.Drawing.Size(181, 22);
            this.TbAdj.Text = "盤點調整";
            this.TbAdj.Click += new System.EventHandler(this.盤點調整ToolStripMenuItem_Click);
            // 
            // TbQMno
            // 
            this.TbQMno.MergeIndex = 2;
            this.TbQMno.Name = "TbQMno";
            this.TbQMno.Size = new System.Drawing.Size(181, 22);
            this.TbQMno.Text = "物料查詢";
            this.TbQMno.Click += new System.EventHandler(this.物料查詢ToolStripMenuItem_Click);
            // 
            // TbStorePos
            // 
            this.TbStorePos.Name = "TbStorePos";
            this.TbStorePos.Size = new System.Drawing.Size(181, 22);
            this.TbStorePos.Text = "平置倉儲位設定";
            this.TbStorePos.Click += new System.EventHandler(this.平置倉儲位設定ToolStripMenuItem_Click);
            // 
            // TbQPos
            // 
            this.TbQPos.Name = "TbQPos";
            this.TbQPos.Size = new System.Drawing.Size(181, 22);
            this.TbQPos.Text = "平置倉儲位查詢";
            this.TbQPos.Click += new System.EventHandler(this.平置倉儲位查詢ToolStripMenuItem_Click);
            // 
            // TbReport
            // 
            this.TbReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbReport.Image = global::KSMrp.Properties.Resources.報表;
            this.TbReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbReport.Name = "TbReport";
            this.TbReport.Size = new System.Drawing.Size(102, 34);
            this.TbReport.Text = "報表中心";
            this.TbReport.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TbUser,
            this.TbStore,
            this.TbPassword,
            this.TbPackage,
            this.TbStoreHouse,
            this.TbError,
            this.重新產生報表ToolStripMenuItem,
            this.TbShelf});
            this.toolStripDropDownButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripDropDownButton2.Image = global::KSMrp.Properties.Resources.設定;
            this.toolStripDropDownButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(111, 34);
            this.toolStripDropDownButton2.Text = "系統設定";
            // 
            // TbUser
            // 
            this.TbUser.Name = "TbUser";
            this.TbUser.Size = new System.Drawing.Size(170, 22);
            this.TbUser.Text = "使用者設定";
            this.TbUser.Click += new System.EventHandler(this.使用者設定ToolStripMenuItem_Click);
            // 
            // TbStore
            // 
            this.TbStore.Name = "TbStore";
            this.TbStore.Size = new System.Drawing.Size(170, 22);
            this.TbStore.Tag = "";
            this.TbStore.Text = "儲位管理";
            this.TbStore.Click += new System.EventHandler(this.儲位管理ToolStripMenuItem_Click);
            // 
            // TbPassword
            // 
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.Size = new System.Drawing.Size(170, 22);
            this.TbPassword.Text = "修改密碼";
            this.TbPassword.Click += new System.EventHandler(this.修改密碼ToolStripMenuItem_Click);
            // 
            // TbPackage
            // 
            this.TbPackage.Name = "TbPackage";
            this.TbPackage.Size = new System.Drawing.Size(170, 22);
            this.TbPackage.Text = "新增包裝作業";
            this.TbPackage.Click += new System.EventHandler(this.新增包裝作業ToolStripMenuItem_Click);
            // 
            // TbStoreHouse
            // 
            this.TbStoreHouse.Name = "TbStoreHouse";
            this.TbStoreHouse.Size = new System.Drawing.Size(170, 22);
            this.TbStoreHouse.Text = "新增庫位作業";
            this.TbStoreHouse.Click += new System.EventHandler(this.新增庫位作業ToolStripMenuItem_Click);
            // 
            // TbError
            // 
            this.TbError.Name = "TbError";
            this.TbError.Size = new System.Drawing.Size(170, 22);
            this.TbError.Text = "錯誤資料維護";
            this.TbError.Click += new System.EventHandler(this.錯誤資料維護ToolStripMenuItem_Click);
            // 
            // 重新產生報表ToolStripMenuItem
            // 
            this.重新產生報表ToolStripMenuItem.Name = "重新產生報表ToolStripMenuItem";
            this.重新產生報表ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.重新產生報表ToolStripMenuItem.Text = "重新產生報表";
            this.重新產生報表ToolStripMenuItem.Click += new System.EventHandler(this.重新產生報表ToolStripMenuItem_Click);
            // 
            // TbShelf
            // 
            this.TbShelf.Name = "TbShelf";
            this.TbShelf.Size = new System.Drawing.Size(170, 22);
            this.TbShelf.Text = "Shelf Life 設定";
            this.TbShelf.Click += new System.EventHandler(this.TbShelf_Click);
            // 
            // TbQuit
            // 
            this.TbQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbQuit.Image = global::KSMrp.Properties.Resources.btnaway;
            this.TbQuit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbQuit.Name = "TbQuit";
            this.TbQuit.Size = new System.Drawing.Size(102, 34);
            this.TbQuit.Text = "離開系統";
            this.TbQuit.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // TbLogOut
            // 
            this.TbLogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbLogOut.Image = global::KSMrp.Properties.Resources.btnsignout;
            this.TbLogOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TbLogOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbLogOut.Name = "TbLogOut";
            this.TbLogOut.Size = new System.Drawing.Size(72, 34);
            this.TbLogOut.Text = "登出";
            this.TbLogOut.Visible = false;
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作說明ToolStripMenuItem,
            this.關於自動倉儲管理系統ToolStripMenuItem});
            this.toolStripDropDownButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripDropDownButton3.Image = global::KSMrp.Properties.Resources.btncaption;
            this.toolStripDropDownButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(81, 34);
            this.toolStripDropDownButton3.Text = "說明";
            this.toolStripDropDownButton3.ToolTipText = "關於";
            // 
            // 操作說明ToolStripMenuItem
            // 
            this.操作說明ToolStripMenuItem.Name = "操作說明ToolStripMenuItem";
            this.操作說明ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.操作說明ToolStripMenuItem.Text = "操作手冊";
            this.操作說明ToolStripMenuItem.Visible = false;
            // 
            // 關於自動倉儲管理系統ToolStripMenuItem
            // 
            this.關於自動倉儲管理系統ToolStripMenuItem.Name = "關於自動倉儲管理系統ToolStripMenuItem";
            this.關於自動倉儲管理系統ToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.關於自動倉儲管理系統ToolStripMenuItem.Text = "關於自動倉儲管理系統";
            this.關於自動倉儲管理系統ToolStripMenuItem.Click += new System.EventHandler(this.關於自動倉儲管理系統ToolStripMenuItem_Click);
            // 
            // panelBuf
            // 
            this.panelBuf.BackColor = System.Drawing.Color.White;
            this.panelBuf.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelBuf.Location = new System.Drawing.Point(943, 37);
            this.panelBuf.Name = "panelBuf";
            this.panelBuf.Size = new System.Drawing.Size(0, 402);
            this.panelBuf.TabIndex = 10;
            // 
            // Btnbuf
            // 
            this.Btnbuf.Cursor = System.Windows.Forms.Cursors.Default;
            this.Btnbuf.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btnbuf.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Btnbuf.Location = new System.Drawing.Point(927, 37);
            this.Btnbuf.Name = "Btnbuf";
            this.Btnbuf.Size = new System.Drawing.Size(16, 402);
            this.Btnbuf.TabIndex = 12;
            this.Btnbuf.Text = "<";
            this.Btnbuf.UseVisualStyleBackColor = true;
            this.Btnbuf.Click += new System.EventHandler(this.Btnbuf_Click);
            // 
            // mdiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(943, 475);
            this.Controls.Add(this.Btnbuf);
            this.Controls.Add(this.panelBuf);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "mdiMain";
            this.Text = "自動倉儲管理系統";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.mdiMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mdiMain_FormClosing);
            this.Load += new System.EventHandler(this.mdiMain_Load);
            this.SizeChanged += new System.EventHandler(this.mdiMain_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerQuit;
        private System.Windows.Forms.Timer timerBuffer;
        private System.Windows.Forms.Timer timerBackup;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TbIn;
        private System.Windows.Forms.ToolStripButton TbOut;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem TbAdj;
        private System.Windows.Forms.ToolStripMenuItem TbQMno;
        private System.Windows.Forms.ToolStripButton TbReport;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem TbUser;
        private System.Windows.Forms.ToolStripMenuItem TbStore;
        private System.Windows.Forms.ToolStripMenuItem TbPassword;
        private System.Windows.Forms.ToolStripMenuItem TbPackage;
        private System.Windows.Forms.ToolStripMenuItem TbStoreHouse;
        private System.Windows.Forms.ToolStripMenuItem TbError;
        private System.Windows.Forms.ToolStripMenuItem 重新產生報表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton TbQuit;
        private System.Windows.Forms.ToolStripButton TbLogOut;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem 操作說明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關於自動倉儲管理系統ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TbUPCdata;
        private System.Windows.Forms.ToolStripMenuItem TbStorePos;
        private System.Windows.Forms.ToolStripMenuItem TbQPos;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Panel panelBuf;
        private System.Windows.Forms.Button Btnbuf;
        private System.Windows.Forms.ToolStripButton TbIns;
        private System.Windows.Forms.ToolStripButton TbOuts;
        private System.Windows.Forms.ToolStripMenuItem TbShelf;
    }
}

