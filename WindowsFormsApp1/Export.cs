using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Data;
using GemBox.Spreadsheet;
namespace WindowsFormsApp1
{

    class Export
    {
        public static void DtExport(System.Data.DataTable dt, string path)
        {
            // If using Professional version, put your serial key below.
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.Add("output");

            // Insert DataTable to an Excel worksheet.
            worksheet.InsertDataTable(dt,
                new InsertDataTableOptions()
                {
                    ColumnHeaders = true,
                    StartRow = 0
                });

            workbook.Save(path);
        }
    }
}
