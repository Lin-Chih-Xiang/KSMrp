using ClosedXML.Excel;
using System;
using System.Data;
using System.Collections.Generic;

namespace KSMrp
{
    class ExportService
    {
        public bool ExportExcel(string SheetName, string FileName, DataTable ExportData, out string ErrMsg)
        {
            ErrMsg = "";
            bool vResult = false;
            try
            {
                XLWorkbook workbook = new XLWorkbook();
                if (ExportData != null)
                {
                    //匯出資料
                    workbook.Worksheets.Add(ExportData, SheetName);
                    workbook.SaveAs(FileName);
                }
                workbook.Dispose();
                vResult = true;
            }
            catch (Exception ex)
            {
                vResult = false;
                ErrMsg = ex.Message;
            }
            return vResult;
        }

        public List<string> LoadExcelSheet(string FileName, out string ErrMsg)
        {
            ErrMsg = "";
            List<string> vList = new List<string>();
            try
            {
                XLWorkbook workbook = new XLWorkbook(FileName);
                int i = 0;
                for (i = 1; i <= workbook.Worksheets.Count; i++)
                {
                    string vSheetName = workbook.Worksheet(i).Name;
                    vList.Add(vSheetName);
                }

                workbook.Dispose();
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
            return vList;
        }
        public DataTable ImportExcel(string SheetName, string FileName, out string ErrMsg)
        {
            ErrMsg = "";
            DataTable DT = new DataTable();
            try
            {
                XLWorkbook vWorkbook = new XLWorkbook(FileName);
                IXLWorksheet vWorkSheet = vWorkbook.Worksheet(SheetName);
                // 定義資料起始、結束 Cell
                IXLCell vFirstCell = vWorkSheet.FirstCellUsed();
                IXLCell vLastCell = vWorkSheet.LastCellUsed();
                // 使用資料起始、結束 Cell，來定義出一個資料範圍
                IXLRange vData = vWorkSheet.Range(vFirstCell.Address, vLastCell.Address);

                IXLTable vXLDT = vData.AsTable();

                int i = 0;
                int j = 0;
                int MaxCellCount = vXLDT.Row(1).CellCount();    //依標頭取得最大的長度
                //初始化DT 
                for (i = 0; i < MaxCellCount; i++)
                {
                    DT.Columns.Add(i.ToString());
                }
                i = 0;
                foreach (var XDR in vXLDT.Rows())
                {
                    //跳過標題列
                    if (i == 0) { i++; continue; }
                    DataRow DR = DT.NewRow();
                    for (j = 1; j <= MaxCellCount; j++)
                    {
                        if (XDR.Cell(j).ValueCached == null)
                        {
                            DR[j - 1] = XDR.Cell(j).Value.ToString();
                        }
                        else
                        {
                            DR[j - 1] = XDR.Cell(j).ValueCached.ToString();
                        }

                    }
                    DT.Rows.Add(DR);
                }
                vWorkbook.Dispose();
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
            return DT;
        }


        //讀取外部檔案
        public string LoadFile(string FileName, out string ErrMsg)
        {
            string vFileText = "";
            ErrMsg = "";
            try
            {
                System.IO.StreamReader FSR = new System.IO.StreamReader(FileName, System.Text.Encoding.Default);
                vFileText = FSR.ReadToEnd();
                FSR.Close();
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }

            return vFileText;
        } 
    
    }
}
