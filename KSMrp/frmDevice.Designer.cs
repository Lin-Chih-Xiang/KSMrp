namespace KSMrp
{
    partial class frmDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevice));
            this.btndelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMachineNo = new nsKXMSUC.DigitTextBox();
            this.txtMachineDesc = new nsKXMSUC.CustomTextBox();
            this.txtCarry = new nsKXMSUC.DigitTextBox();
            this.txtMaxX = new nsKXMSUC.DigitTextBox();
            this.txtMaxY = new nsKXMSUC.DigitTextBox();
            this.txtWareHouse = new nsKXMSUC.DigitTextBox();
            this.LV1 = new nsKXMSUC.KXMSLV();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeviceNum = new nsKXMSUC.DigitTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEdit2 = new System.Windows.Forms.Button();
            this.btnAdd2 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btndelete.Font = new System.Drawing.Font("新細明體", 12F);
            this.btndelete.Location = new System.Drawing.Point(630, 9);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(67, 32);
            this.btndelete.TabIndex = 3;
            this.btndelete.Text = "刪除";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightGreen;
            this.btnSave.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnSave.Location = new System.Drawing.Point(101, 237);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 33);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.Font = new System.Drawing.Font("新細明體", 12F);
            this.btnCancel.Location = new System.Drawing.Point(188, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 33);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(4, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "設備代號";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(36, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "說明";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(4, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "最大層數";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(25, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Max X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F);
            this.label6.Location = new System.Drawing.Point(156, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Max Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F);
            this.label7.Location = new System.Drawing.Point(4, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "WareHouse";
            // 
            // txtMachineNo
            // 
            this.txtMachineNo.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtMachineNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMachineNo.Location = new System.Drawing.Point(82, 41);
            this.txtMachineNo.Name = "txtMachineNo";
            this.txtMachineNo.PlaceHolder = "";
            this.txtMachineNo.ShortcutsEnabled = false;
            this.txtMachineNo.Size = new System.Drawing.Size(53, 27);
            this.txtMachineNo.TabIndex = 15;
            this.txtMachineNo.Text = "1";
            // 
            // txtMachineDesc
            // 
            this.txtMachineDesc.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtMachineDesc.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMachineDesc.Location = new System.Drawing.Point(82, 75);
            this.txtMachineDesc.Name = "txtMachineDesc";
            this.txtMachineDesc.PlaceHolder = "";
            this.txtMachineDesc.Size = new System.Drawing.Size(188, 27);
            this.txtMachineDesc.TabIndex = 17;
            // 
            // txtCarry
            // 
            this.txtCarry.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtCarry.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCarry.Location = new System.Drawing.Point(82, 109);
            this.txtCarry.Name = "txtCarry";
            this.txtCarry.PlaceHolder = "";
            this.txtCarry.ShortcutsEnabled = false;
            this.txtCarry.Size = new System.Drawing.Size(53, 27);
            this.txtCarry.TabIndex = 18;
            this.txtCarry.Text = "1";
            // 
            // txtMaxX
            // 
            this.txtMaxX.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtMaxX.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMaxX.Location = new System.Drawing.Point(82, 143);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.PlaceHolder = "";
            this.txtMaxX.ShortcutsEnabled = false;
            this.txtMaxX.Size = new System.Drawing.Size(53, 27);
            this.txtMaxX.TabIndex = 19;
            this.txtMaxX.Text = "1";
            // 
            // txtMaxY
            // 
            this.txtMaxY.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtMaxY.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMaxY.Location = new System.Drawing.Point(212, 144);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.PlaceHolder = "";
            this.txtMaxY.ShortcutsEnabled = false;
            this.txtMaxY.Size = new System.Drawing.Size(53, 27);
            this.txtMaxY.TabIndex = 21;
            this.txtMaxY.Text = "1";
            // 
            // txtWareHouse
            // 
            this.txtWareHouse.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtWareHouse.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtWareHouse.Location = new System.Drawing.Point(82, 8);
            this.txtWareHouse.Name = "txtWareHouse";
            this.txtWareHouse.PlaceHolder = "";
            this.txtWareHouse.ShortcutsEnabled = false;
            this.txtWareHouse.Size = new System.Drawing.Size(53, 27);
            this.txtWareHouse.TabIndex = 22;
            this.txtWareHouse.Text = "1";
            // 
            // LV1
            // 
            this.LV1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LV1.FullRowSelect = true;
            this.LV1.GridLines = true;
            this.LV1.HideSelection = false;
            this.LV1.Location = new System.Drawing.Point(12, 9);
            this.LV1.Name = "LV1";
            this.LV1.Size = new System.Drawing.Size(398, 310);
            this.LV1.TabIndex = 23;
            this.LV1.UseCompatibleStateImageBehavior = false;
            this.LV1.View = System.Windows.Forms.View.Details;
            this.LV1.SelectedIndexChanged += new System.EventHandler(this.LV1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F);
            this.label5.Location = new System.Drawing.Point(5, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "DeviceNo";
            // 
            // txtDeviceNum
            // 
            this.txtDeviceNum.Font = new System.Drawing.Font("新細明體", 12F);
            this.txtDeviceNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtDeviceNum.Location = new System.Drawing.Point(82, 177);
            this.txtDeviceNum.Name = "txtDeviceNum";
            this.txtDeviceNum.PlaceHolder = "";
            this.txtDeviceNum.ShortcutsEnabled = false;
            this.txtDeviceNum.Size = new System.Drawing.Size(53, 27);
            this.txtDeviceNum.TabIndex = 20;
            this.txtDeviceNum.Text = "1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtWareHouse);
            this.panel2.Controls.Add(this.txtMaxY);
            this.panel2.Controls.Add(this.txtDeviceNum);
            this.panel2.Controls.Add(this.txtMaxX);
            this.panel2.Controls.Add(this.txtCarry);
            this.panel2.Controls.Add(this.txtMachineDesc);
            this.panel2.Controls.Add(this.txtMachineNo);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(425, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 278);
            this.panel2.TabIndex = 24;
            // 
            // btnEdit2
            // 
            this.btnEdit2.BackColor = System.Drawing.Color.PaleGreen;
            this.btnEdit2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEdit2.Location = new System.Drawing.Point(493, 9);
            this.btnEdit2.Name = "btnEdit2";
            this.btnEdit2.Size = new System.Drawing.Size(67, 32);
            this.btnEdit2.TabIndex = 26;
            this.btnEdit2.Text = "修改";
            this.btnEdit2.UseVisualStyleBackColor = false;
            this.btnEdit2.Click += new System.EventHandler(this.btnEdit2_Click);
            // 
            // btnAdd2
            // 
            this.btnAdd2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnAdd2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAdd2.Location = new System.Drawing.Point(420, 9);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(67, 32);
            this.btnAdd2.TabIndex = 25;
            this.btnAdd2.Text = "新增";
            this.btnAdd2.UseVisualStyleBackColor = false;
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
            // 
            // frmDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 331);
            this.Controls.Add(this.btnEdit2);
            this.Controls.Add(this.btnAdd2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LV1);
            this.Controls.Add(this.btndelete);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "新增設備";
            this.Load += new System.EventHandler(this.frmDevice_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private nsKXMSUC.DigitTextBox txtMachineNo;
        private nsKXMSUC.CustomTextBox txtMachineDesc;
        private  nsKXMSUC.DigitTextBox txtCarry;
        private nsKXMSUC.DigitTextBox txtMaxX;
        private nsKXMSUC.DigitTextBox txtMaxY;
        private nsKXMSUC.DigitTextBox txtWareHouse;
        private nsKXMSUC.KXMSLV LV1;
        private System.Windows.Forms.Label label5;
        private nsKXMSUC.DigitTextBox txtDeviceNum;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEdit2;
        private System.Windows.Forms.Button btnAdd2;
    }
}