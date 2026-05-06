using Application.DTOs.Response;
using Application.Interfaces;
using ClosedXML.Excel;
using Infrastructure.Persistence.Data;

namespace Infrastructure.Persistence.Repositories
{
    public class ExcelService : IExcelService
    {
        private readonly DataContext _context;

        public ExcelService(DataContext context)
        {
            _context = context;
        }

        private double GetPricePerGB(double usageGB)
        {
            var sysInfo = _context.SysInfos.FirstOrDefault();
            if (sysInfo == null) return 0;

            if (usageGB <= 25) return sysInfo.segment1;
            else if (usageGB <= 50) return sysInfo.segment2;
            else if (usageGB <= 75) return sysInfo.segment3;
            else if (usageGB <= 100) return sysInfo.segment4;
            else if (usageGB <= 125) return sysInfo.segment5;
            else return sysInfo.segment6;
        }

        private double RoundToNearestThousand(double value)
        {
            return Math.Round(value / 1000.0, 0, MidpointRounding.AwayFromZero) * 1000.0;
        }

        public byte[] GenerateMikrotikReport(
            List<DepartmentConsumptionResponse> serviceData,
            List<DepartmentConsumptionResponse> tcShopsData,
            List<DepartmentConsumptionResponse> shopsData)
        {
            using (var workbook = new XLWorkbook())
            {
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

        private void AddDetailedSheet(XLWorkbook workbook, string name, List<DetailedDepartmentConsumptionResponse> data)
        {
            var sysInfo = _context.SysInfos.FirstOrDefault();
            var ws = workbook.Worksheets.Add(name);
            ws.RightToLeft = true;

            var dateCell = ws.Cell(1, 1);
            dateCell.Value = $"تاريخ تصدير التقرير: {DateTime.Now:yyyy-MM-dd HH:mm}";

            var dateRange = ws.Range(1, 1, 2, 7);
            dateRange.Merge().Style
                .Font.SetBold()
                .Font.SetFontSize(14)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                .Fill.SetBackgroundColor(XLColor.LightGray);

            int headerRow = 3;
            ws.Cell(headerRow, 1).Value = "Department / User";
            ws.Cell(headerRow, 2).Value = "ArName";
            ws.Cell(headerRow, 3).Value = "Consumption (GB)";
            ws.Cell(headerRow, 4).Value = "Price per GB (Dept)";
            ws.Cell(headerRow, 5).Value = "Total Cost";
            ws.Cell(headerRow, 6).Value = "DVR Count";
            ws.Cell(headerRow, 7).Value = "Total Cost (With DVR)";

            var header = ws.Range(headerRow, 1, headerRow, 7);
            header.Style.Font.Bold = true;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Fill.BackgroundColor = XLColor.FromHtml("#003366");
            ApplyCommonStyles(header);

            int currentRow = headerRow + 1;
            foreach (var dept in data)
            {
                var deptPrice = GetPricePerGB(dept.TotalConsumptionGB);

                ws.Cell(currentRow, 1).Value = "DEPT: " + dept.DepartmentName;
                ws.Cell(currentRow, 2).Value = dept.ArName;
                ws.Cell(currentRow, 3).Value = dept.TotalConsumptionGB;
                ws.Cell(currentRow, 4).Value = deptPrice;
                ws.Cell(currentRow, 5).Value = RoundToNearestThousand(dept.TotalConsumptionGB * deptPrice);
                ws.Cell(currentRow, 6).Value = dept.DvrNum;
                ws.Cell(currentRow, 7).Value = RoundToNearestThousand(dept.TotalConsumptionGB * deptPrice + (dept.DvrNum * (sysInfo?.DvrPrice ?? 0)));

                var deptRange = ws.Range(currentRow, 1, currentRow, 7);
                deptRange.Style.Font.Bold = true;
                deptRange.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;
                ApplyCommonStyles(deptRange);
                currentRow++;

                foreach (var user in dept.Users)
                {
                    ws.Cell(currentRow, 1).Value = "   • " + user.UserName;
                    ws.Cell(currentRow, 3).Value = user.UsageGB;
                    ws.Cell(currentRow, 5).Value = RoundToNearestThousand(user.UsageGB * deptPrice);

                    var userRange = ws.Range(currentRow, 1, currentRow, 7);
                    userRange.Style.Fill.BackgroundColor = XLColor.AliceBlue;
                    ApplyCommonStyles(userRange);
                    currentRow++;
                }
                currentRow++;
            }
            ws.Columns().AdjustToContents();
        }

        private void AddSummarySheet(XLWorkbook workbook, string name, List<DepartmentConsumptionResponse> data)
        {
            var sysInfo = _context.SysInfos.FirstOrDefault();
            var ws = workbook.Worksheets.Add(name);
            ws.RightToLeft = true;

            var dateCell = ws.Cell(1, 1);
            dateCell.Value = $"تاريخ تصدير التقرير: {DateTime.Now:yyyy-MM-dd HH:mm}";

            var dateRange = ws.Range(1, 1, 2, 7);
            dateRange.Merge().Style
                .Font.SetBold()
                .Font.SetFontSize(14)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                .Fill.SetBackgroundColor(XLColor.LightGray);

            int headerRow = 3;
            ws.Cell(headerRow, 1).Value = "Department Name";
            ws.Cell(headerRow, 2).Value = "ArName";
            ws.Cell(headerRow, 3).Value = "Total Consumption (GB)";
            ws.Cell(headerRow, 4).Value = "Price per GB";
            ws.Cell(headerRow, 5).Value = "Total Cost";
            ws.Cell(headerRow, 6).Value = "DVR Count";
            ws.Cell(headerRow, 7).Value = "Total Cost (With DVR)";

            var header = ws.Range(headerRow, 1, headerRow, 7);
            header.Style.Font.Bold = true;
            header.Style.Font.FontColor = XLColor.White;
            header.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            ApplyCommonStyles(header);

            for (int i = 0; i < data.Count; i++)
            {
                int currentRow = i + headerRow + 1;
                var deptPrice = GetPricePerGB(data[i].TotalConsumptionGB);

                ws.Cell(currentRow, 1).Value = data[i].DepartmentName;
                ws.Cell(currentRow, 2).Value = data[i].ArName;
                ws.Cell(currentRow, 3).Value = data[i].TotalConsumptionGB;
                ws.Cell(currentRow, 4).Value = deptPrice;
                ws.Cell(currentRow, 5).Value = RoundToNearestThousand(data[i].TotalConsumptionGB * deptPrice);
                ws.Cell(currentRow, 6).Value = data[i].DvrNum;
                ws.Cell(currentRow, 7).Value = RoundToNearestThousand(data[i].TotalConsumptionGB * deptPrice + (data[i].DvrNum * (sysInfo?.DvrPrice ?? 0)));

                var rowRange = ws.Range(currentRow, 1, currentRow, 7);
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