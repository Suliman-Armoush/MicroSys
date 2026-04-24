using Application.DTOs.Response;
using Application.Interfaces;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class ExcelService : IExcelService
    {
        // 1. التقرير المختصر (أقسام فقط - 3 شيتات)
        public byte[] GenerateMikrotikReport(
            List<DepartmentConsumptionResponse> serviceData,
            List<DepartmentConsumptionResponse> tcShopsData,
            List<DepartmentConsumptionResponse> shopsData)
        {
            using (var workbook = new XLWorkbook())
            {
                // إضافة الشيتات بالمسميات التي طلبتها
                AddSummarySheet(workbook, "خدمي", serviceData);
                AddSummarySheet(workbook, "فعاليات", tcShopsData);
                AddSummarySheet(workbook, "استثمار", shopsData);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        // 2. التقرير التفصيلي (أقسام ويوزرات - 3 شيتات)
        public byte[] GenerateDetailedExcelReport(
            List<DetailedDepartmentConsumptionResponse> serviceData,
            List<DetailedDepartmentConsumptionResponse> tcShopsData,
            List<DetailedDepartmentConsumptionResponse> shopsData)
        {
            using (var workbook = new XLWorkbook())
            {
                AddDetailedSheet(workbook, "خدمي", serviceData);
                AddDetailedSheet(workbook, "فعاليات", tcShopsData);
                AddDetailedSheet(workbook, "استثمار", shopsData);

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        // دالة بناء الشيت التفصيلي (أقسام + يوزرات)
        private void AddDetailedSheet(XLWorkbook workbook, string name, List<DetailedDepartmentConsumptionResponse> data)
        {
            var ws = workbook.Worksheets.Add(name);
            ws.RightToLeft = false;

            // العناوين
            ws.Cell(1, 1).Value = "Department / User";
            ws.Cell(1, 2).Value = "Consumption (GB)";

            var header = ws.Range(1, 1, 1, 2);
            header.Style.Font.Bold = true;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Fill.BackgroundColor = XLColor.FromHtml("#003366");
            ApplyCommonStyles(header);

            int currentRow = 2;
            foreach (var dept in data)
            {
                // سطر القسم (تمييز بصري)
                ws.Cell(currentRow, 1).Value = "DEPT: " + dept.DepartmentName;
                ws.Cell(currentRow, 2).Value = dept.TotalConsumptionGB;

                var deptRange = ws.Range(currentRow, 1, currentRow, 2);
                deptRange.Style.Font.Bold = true;
                deptRange.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
                ApplyCommonStyles(deptRange);
                currentRow++;

                // أسطر اليوزرات (تنظيف الاسم تم في الهاندلر)
                foreach (var user in dept.Users)
                {
                    ws.Cell(currentRow, 1).Value = "   • " + user.UserName;
                    ws.Cell(currentRow, 2).Value = user.UsageGB;

                    var userRange = ws.Range(currentRow, 1, currentRow, 2);
                    userRange.Style.Fill.BackgroundColor = XLColor.AliceBlue;
                    ApplyCommonStyles(userRange);
                    currentRow++;
                }
                // ترك سطر فارغ بين الأقسام لجمالية العرض
                currentRow++;
            }
            ws.Columns().AdjustToContents();
        }

        // دالة بناء الشيت المختصر (أقسام فقط)
        private void AddSummarySheet(XLWorkbook workbook, string name, List<DepartmentConsumptionResponse> data)
        {
            var ws = workbook.Worksheets.Add(name);
            ws.RightToLeft = false;

            ws.Cell(1, 1).Value = "Department Name";
            ws.Cell(1, 2).Value = "Total Consumption (GB)";

            var header = ws.Range(1, 1, 1, 2);
            header.Style.Font.Bold = true;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            ApplyCommonStyles(header);

            for (int i = 0; i < data.Count; i++)
            {
                int currentRow = i + 2;
                ws.Cell(currentRow, 1).Value = data[i].DepartmentName;
                ws.Cell(currentRow, 2).Value = data[i].TotalConsumptionGB;

                var rowRange = ws.Range(currentRow, 1, currentRow, 2);
                // تلوين الأسطر بشكل متبادل (Zebra Stripes)
                rowRange.Style.Fill.BackgroundColor = (currentRow % 2 == 0) ? XLColor.AliceBlue : XLColor.White;
                ApplyCommonStyles(rowRange);
            }
            ws.Columns().AdjustToContents();
        }

        private void ApplyCommonStyles(IXLRange range)
        {
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }
    }
}