using Application.DTOs.Response;
using Application.Interfaces;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class ExcelService : IExcelService
    {
        public byte[] GenerateMikrotikReport(
            List<DepartmentConsumptionResponse> atData,
            List<DepartmentConsumptionResponse> hashData,
            List<DepartmentConsumptionResponse> normalData)
        {
            using (var workbook = new XLWorkbook())
            {
                AddSheet(workbook, "خدمي", atData);
                AddSheet(workbook, "فعاليات ", hashData);
                AddSheet(workbook, "الاستثمار", normalData);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        private void AddSheet(XLWorkbook workbook, string name, List<DepartmentConsumptionResponse> data)
        {
            var ws = workbook.Worksheets.Add(name);

            // 1. العناوين
            ws.Cell(1, 1).Value = "اسم القسم";
            ws.Cell(1, 2).Value = "إجمالي الاستهلاك (GB)";

            // تنسيق العناوين
            var header = ws.Range(1, 1, 1, 2);
            header.Style.Font.Bold = true;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // إضافة حدود غليظة للعنوان
            header.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            header.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // 2. تعبئة البيانات وتنسيق الصفوف
            for (int i = 0; i < data.Count; i++)
            {
                int currentRow = i + 2;
                ws.Cell(currentRow, 1).Value = data[i].DepartmentName;
                ws.Cell(currentRow, 2).Value = data[i].TotalConsumptionGB;

                // تحديد النطاق (السطر الحالي) لتطبيق التنسيقات عليه
                var rowRange = ws.Range(currentRow, 1, currentRow, 2);

                // تلوين الأسطر بشكل تبادلي
                if (currentRow % 2 == 0)
                {
                    rowRange.Style.Fill.BackgroundColor = XLColor.AliceBlue;
                }
                else
                {
                    rowRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                }

                rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // حدود خارجية للسطر
                rowRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;  // حدود بين الأعمدة داخل السطر
                rowRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;    // حد علوي
                rowRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin; // حد سفلي
                rowRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;   // حد أيسر
                rowRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;  // حد أيمن

                rowRange.Style.Border.OutsideBorderColor = XLColor.Black;
                rowRange.Style.Border.InsideBorderColor = XLColor.Black;
            }

            ws.Columns(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Columns().AdjustToContents();
        }
    }
}
