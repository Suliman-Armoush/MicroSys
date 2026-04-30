namespace UI.Models.Response
{
    public class DepartmentConsumptionResponseDto
    {
        public string DepartmentName { get; set; } = null!;
        public double TotalConsumptionGB { get; set; }
        public int ActiveUsersCount { get; set; }

        public int DvrNum { get; set; }
    }
}
