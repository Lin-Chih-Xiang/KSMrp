namespace KSMrp
{
    partial class frmBuffer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuffer));
            this.label1 = new System.Windows.Forms.Label();
            this.Command1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnTimer = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelM = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LV1 = new System.Windows.Forms.ListView();
            this.LV2 = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelM.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "平置倉";
            // 
            // Command1
            // 
            this.Command1.Font = new System.Drawing.Font("新細明體", 12F);
            this.Command1.Location = new System.Drawing.Point(3, 3);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(75, 37);
            this.Command1.TabIndex = 3;
            this.Command1.Text = "清除";
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LV1);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LV2);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(513, 721);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SkyBlue;
            this.panel3.Controls.Add(this.btnTimer);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(513, 33);
            this.panel3.TabIndex = 1;
            // 
            // btnTimer
            // 
            this.btnTimer.ImageIndex = 1;
            this.btnTimer.ImageList = this.imageList1;
            this.btnTimer.Location = new System.Drawing.Point(190, 0);
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(32, 32);
            this.btnTimer.TabIndex = 4;
            this.btnTimer.UseVisualStyleBackColor = true;
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "if_player_play_1825.png");
            this.imageList1.Images.SetKeyName(1, "if_player_stop_1829.png");
            this.imageList1.Images.SetKeyName(2, "if_reload_all_tabs_1839.png");
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 2;
            this.btnRefresh.ImageList = this.imageList1;
            this.btnRefresh.Location = new System.Drawing.Point(152, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 32);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(19, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "設備記憶體狀況";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.Command1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 345);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 40);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 39);
            this.panel1.TabIndex = 0;
            // 
            // panelM
            // 
            this.panelM.Controls.Add(this.splitContainer1);
            this.panelM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelM.Location = new System.Drawing.Point(0, 0);
            this.panelM.Name = "panelM";
            this.panelM.Size = new System.Drawing.Size(513, 721);
            this.panelM.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // LV1
            // 
            this.LV1.BackColor = System.Drawing.Color.PapayaWhip;
            this.LV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV1.Location = new System.Drawing.Point(0, 33);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(513, 299);
            this.LV1.TabIndex = 2;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            this.LV1.VirtualMode = true;
            // 
            // LV2
            // 
            this.LV2.BackColor = System.Drawing.Color.PapayaWhip;
            this.LV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV2.Location = new System.Drawing.Point(0, 39);
            this.LV2.Name = "LV2";
            this.LV2.Size = new System.Drawing.Size(513, 306);
            this.LV2.TabIndex = 3;
            this.LV2.UseCompatibleStateImageBehavior = false;
            this.LV2.View = System.Windows.Forms.View.Details;
            this.LV2.VirtualMode = true;
            // 
            // frmBuffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 721);
            this.ControlBox = false;
            this.Controls.Add(this.panelM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(824, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuffer";
            this.Load += new System.EventHandler(this.frmBuffer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelM.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Command1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panelM;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTimer;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView LV1;
        private System.Windows.Forms.ListView LV2;
    }
}