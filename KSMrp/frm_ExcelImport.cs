using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nsKXMSUC;

namespace KSMrp
{
    public partial class frm_ExcelImport : Form
    {
        string gbFuncName = "Excel匯入";
        Form gbParentForm = null;
        public enExcelImportType gbImportType;
        ExportService exService = new ExportService();
        public enum enExcelImportType : int
        {
            批次入庫 = 11,
            批次出庫 = 12,
        }

        public frm_ExcelImport(enExcelImportType ImportType, Form parentform)
        {
            InitializeComponent();
            gbImportType = ImportType;
            gbParentForm = parentform;
        }

        private void frm_ExcelImport_Load(object sender, EventArgs e)
        {
            //this.Text = gbFileName;
            initLV1();
            lblHeader.Text = "批次入庫物料";
        }

        private void frm_ExcelImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLVWidth();
        }

        #region Listview設定
        public void SaveLVWidth()
        {
            KXMSSysPara.LVColWidth.SetLVColWidth(this.Name, LV1.Name, LV1.GetColWidth());
        }

        private void initLV1()
        {
            LV1.Clear();
            LV1.Columns.Add(" ", 0, HorizontalAlignment.Center);
            LV1.Columns.Add("物料編號", 120, HorizontalAlignment.Center);
            LV1.Columns.Add("批號", 120, HorizontalAlignment.Center);
            LV1.Columns.Add("備註", 120, HorizontalAlignment.Center);
            LV1.Columns.Add("數量", 120, HorizontalAlignment.Right);
            LV1.Columns.Add("Location", 120, HorizontalAlignment.Center);
            //LV1.Columns.Add("工單號", 60, HorizontalAlignment.Center);
            //LV1.Columns.Add("數量", 70, HorizontalAlignment.Center);
            //LV1.Columns.Add("JDE儲位", 80, HorizontalAlignment.Center);
            //LV1.Columns.Add("供應商", 88, HorizontalAlignment.Center);
            //LV1.Columns.Add("儲位", 110, HorizontalAlignment.Center);
            //LV1.Columns.Add("PO", 120, HorizontalAlignment.Center);
            //LV1.Columns.Add("備註", 80, HorizontalAlignment.Center);
            //LV1.Columns.Add("出庫時間", 80, HorizontalAlignment.Center);

            //設定LV 寬度
            string vColStr = KXMSSysPara.LVColWidth.GetLVColWidth(this.Name, LV1.Name);
            LV1.SetColWidth(vColStr);
        }
        private enum enLV1Column : int
        {
            物料編號 = 0,
            批號 = 3,
            備註 = 4,
            數量 = 6,
            Location = 7,
            //工單號 = 6,
            //數量 = 7,
            //JDE儲位 = 8,
            //供應商 = 9,
            //儲位 = 10,
            //PO = 11,
            //出庫時間 = 12,
        }
        #endregion

        private string gbFileName = "";
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpfDlg1.Filter = "XLSX|*.xlsx";
            OpfDlg1.FileName = "";
            if (OpfDlg1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gbFileName = OpfDlg1.FileName;
                txtFileName.Text = gbFileName;
            }
            else
            {
                return;
            }
            LoadExcelSheet();
        }

        private void LoadExcelSheet()
        {
            string vErrMsg = "";
            try
            {
                //取得副檔名
                gbFileName = txtFileName.Text;
                string vSubName = System.IO.Path.GetExtension(gbFileName);
                switch (vSubName)
                {
                    case ".xlsx":
                        break;
                    default:
                        throw new KXMSException("載入的檔案非Excel(.xlsx)檔，請重新選擇！");
                }

                List<string> vList = exService.LoadExcelSheet(gbFileName, out vErrMsg);
                if (String.IsNullOrEmpty(vErrMsg) == false) { throw new KXMSException("Excel讀取失敗！\n請見詳細資料", vErrMsg); }

                cbSheet.Items.Clear();
                foreach (string lvitemStr in vList)
                {
                    cbSheet.Items.Add(lvitemStr);
                }
                if (cbSheet.Items.Count > 0)
                {
                    cbSheet.SelectedIndex = 0;
                }
                panel3.Enabled = true;
            }
            catch (KXMSException ex)
            {
                gbFileName = "";
                KXMSMsgBox.Show(ex.Message, gbFuncName, ex.MessageDetail, ex.MessageType, enMessageButton.OK);
            }
            catch (Exception ex)
            {
                gbFileName = "";
                KXMSMsgBox.Show("Excel讀取失敗！\n請見詳細資料", gbFuncName, ex.Message, enMessageType.Warning, enMessageButton.OK);
            }
        }

        private DataTable LoadExcelDT(string FileName, string SheetName, out string vErrMsg)
        {
            vErrMsg = "";
            DataTable DT = exService.ImportExcel(SheetName, FileName, out vErrMsg);
            return DT;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string vErrMsg = "";
            try
            {
                DataTable DT = LoadExcelDT(gbFileName, cbSheet.Text, out vErrMsg);
                if (String.IsNullOrEmpty(vErrMsg) == false) { throw new KXMSException("Excel讀取失敗！\n請見詳細資料", vErrMsg); }
                LV1.Items.Clear();
                int MaxCol = DT.Columns.Count;

                foreach (DataRow DR in DT.Rows)
                {
                    ListViewItem lvitem = new ListViewItem();
                    switch (gbImportType)
                    {
                        case enExcelImportType.批次入庫:

                            lvitem.Text = "";
                            lvitem.SubItems.Add(DR[0].ToString());       // 物料編號 = 0,
                            lvitem.SubItems.Add(DR[3].ToString());       // 批號 = 3,
                            lvitem.SubItems.Add(DR[4].ToString());       // 備註 = 4,
                            lvitem.SubItems.Add(DR[6].ToString());       // 數量 = 6,
                            lvitem.SubItems.Add(DR[7].ToString());       // Location = 7,
                            //lvitem.SubItems.Add(DR[10].ToString());      // 數量 = 10,
                            //lvitem.SubItems.Add(DR[11].ToString());      // 物料備註 = 11,
                            LV1.Items.Add(lvitem);
                            break;
                        case enExcelImportType.批次出庫:

                            //lvitem.Text = DR[0].ToString();
                            //lvitem.SubItems.Add(DR[1].ToString());       // 料號 = 1,
                            //lvitem.SubItems.Add(DR[2].ToString());       // 批號 = 2,
                            //lvitem.SubItems.Add(DR[3].ToString());       // 儲位 = 3,
                            //lvitem.SubItems.Add(DR[10].ToString());      // 數量 = 10,
                            //lvitem.SubItems.Add(DR[11].ToString());      // 物料備註 = 11,
                            //LV1.Items.Add(lvitem);
                            break;
                    }

                }

                lblLVCount.Text = "共" + DT.Rows.Count + "筆";
            }
            catch (KXMSException ex)
            {
                KXMSMsgBox.Show(ex.Message, gbFuncName, ex.MessageDetail, ex.MessageType, enMessageButton.OK);
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("Excel讀取失敗！\n請見詳細資料", gbFuncName, ex.Message, enMessageType.Warning, enMessageButton.OK);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            switch (gbImportType)
            {
                //case enExcelImportType.物料資訊:
                //    UpdateMMainDescData();
                //    break;
                //case enExcelImportType.供應商:
                //    UpdateSupplierData();
                //    break;
                case enExcelImportType.批次入庫:
                    SendDataToMultiIn();
                    break;
                //case enExcelImportType.批次出庫:
                //    SendDataToMultiOut();
                //    break;
            }
        }

        private void SendDataToMultiIn()
        {
            string vErrMsg = "";
            try
            {
                DataTable DT = LoadExcelDT(gbFileName, cbSheet.Text, out vErrMsg);
                ((frm_StoreMultiIn)gbParentForm).insertLVInList(DT, true);
                Close();
            }
            catch (KXMSException ex)
            {
                KXMSMsgBox.Show(ex.Message, gbFuncName, ex.MessageDetail, ex.MessageType, enMessageButton.OK);
            }
            catch (Exception ex)
            {
                KXMSMsgBox.Show("Excel讀取失敗！\n請見詳細資料", gbFuncName, ex.Message, enMessageType.Warning, enMessageButton.OK);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LoadExcelSheet();
        }
    }
}
