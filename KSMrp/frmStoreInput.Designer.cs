namespace KSMrp
{
    partial class frmStoreInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStoreInput));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnonly = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Ud = new System.Windows.Forms.DomainUpDown();
            this.Udland = new System.Windows.Forms.DomainUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnsure = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnonly);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(8, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnonly
            // 
            this.btnonly.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnonly.Location = new System.Drawing.Point(146, 11);
            this.btnonly.Name = "btnonly";
            this.btnonly.Size = new System.Drawing.Size(136, 66);
            this.btnonly.TabIndex = 1;
            this.btnonly.Text = "單一儲位";
            this.btnonly.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1.BackColor = System.Drawing.SystemColors.Window;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton1.Font = new System.Drawing.Font("新細明體", 12F);
            this.radioButton1.Location = new System.Drawing.Point(5, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(135, 66);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "整層";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Ud);
            this.groupBox2.Controls.Add(this.Udland);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnsure);
            this.groupBox2.Location = new System.Drawing.Point(8, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // Ud
            // 
            this.Ud.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Bold);
            this.Ud.Location = new System.Drawing.Point(80, 58);
            this.Ud.Name = "Ud";
            this.Ud.Size = new System.Drawing.Size(60, 30);
            this.Ud.TabIndex = 5;
            this.Ud.Text = "1";
            this.Ud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Udland
            // 
            this.Udland.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Bold);
            this.Udland.Location = new System.Drawing.Point(80, 22);
            this.Udland.Name = "Udland";
            this.Udland.Size = new System.Drawing.Size(60, 30);
            this.Udland.TabIndex = 4;
            this.Udland.Text = "5";
            this.Udland.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(5, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "縱向燈數";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(5, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "橫向燈數";
            // 
            // btnsure
            // 
            this.btnsure.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnsure.Location = new System.Drawing.Point(146, 19);
            this.btnsure.Name = "btnsure";
            this.btnsure.Size = new System.Drawing.Size(136, 66);
            this.btnsure.TabIndex = 1;
            this.btnsure.Text = "確定";
            this.btnsure.UseVisualStyleBackColor = true;
            // 
            // frmStoreInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 186);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStoreInput";
            this.Text = "請輸入儲位大小";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnonly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DomainUpDown Ud;
        private System.Windows.Forms.DomainUpDown Udland;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnsure;
    }
}