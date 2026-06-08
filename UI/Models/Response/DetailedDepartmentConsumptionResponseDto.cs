namespace UI.Models.Response
{
    public class DetailedDepartmentConsumptionResponseDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public string ArName { get; set; } = string.Empty;
        public double TotalConsumptionGB { get; set; }
        public List<UserConsumptionDetailDto> Users { get; set; } = new();
        public string Type { get; set; } = string.Empty;
        public int DvrNum { get; set; }
    }

    public class UserConsumptionDetailDto
    {
        public string UserName { get; set; } = string.Empty;
        public double UsageGB { get; set; }
    }
}
