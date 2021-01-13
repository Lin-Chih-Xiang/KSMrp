namespace KSMrp
{
    partial class frm_Sub_StoreAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Sub_StoreAdd));
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMachineNo = new nsKXMSUC.DigitTextBox();
            this.txtCarryF = new nsKXMSUC.DigitTextBox();
            this.txtCarryT = new nsKXMSUC.DigitTextBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(254, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 61);
            this.button2.TabIndex = 5;
            this.button2.Text = "新增";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 29;
            this.label4.Text = "機台：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(31, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "盤：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(152, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 24);
            this.label6.TabIndex = 31;
            this.label6.Text = "～";
            // 
            // txtMachineNo
            // 
            this.txtMachineNo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtMachineNo.Location = new System.Drawing.Point(85, 4);
            this.txtMachineNo.MaxLength = 3;
            this.txtMachineNo.Name = "txtMachineNo";
            this.txtMachineNo.PlaceHolder = "";
            this.txtMachineNo.ReadOnly = true;
            this.txtMachineNo.ShortcutsEnabled = false;
            this.txtMachineNo.Size = new System.Drawing.Size(61, 29);
            this.txtMachineNo.TabIndex = 32;
            this.txtMachineNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMachineNo.TextChanged += new System.EventHandler(this.txtMachineNo_TextChanged);
            // 
            // txtCarryF
            // 
            this.txtCarryF.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCarryF.Location = new System.Drawing.Point(85, 42);
            this.txtCarryF.MaxLength = 3;
            this.txtCarryF.Name = "txtCarryF";
            this.txtCarryF.PlaceHolder = "";
            this.txtCarryF.ShortcutsEnabled = false;
            this.txtCarryF.Size = new System.Drawing.Size(61, 29);
            this.txtCarryF.TabIndex = 33;
            this.txtCarryF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCarryT
            // 
            this.txtCarryT.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCarryT.Location = new System.Drawing.Point(187, 42);
            this.txtCarryT.MaxLength = 3;
            this.txtCarryT.Name = "txtCarryT";
            this.txtCarryT.PlaceHolder = "";
            this.txtCarryT.ShortcutsEnabled = false;
            this.txtCarryT.Size = new System.Drawing.Size(61, 29);
            this.txtCarryT.TabIndex = 34;
            this.txtCarryT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frm_Sub_StoreAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 81);
            this.Controls.Add(this.txtCarryT);
            this.Controls.Add(this.txtCarryF);
            this.Controls.Add(this.txtMachineNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Sub_StoreAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "新增盤號";
            this.Load += new System.EventHandler(this.frmStorePos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private nsKXMSUC.DigitTextBox txtMachineNo;
        private nsKXMSUC.DigitTextBox txtCarryF;
        private nsKXMSUC.DigitTextBox txtCarryT;
    }
}