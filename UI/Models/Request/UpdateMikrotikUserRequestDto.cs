namespace UI.Models.Request
{
    public class UpdateMikrotikUserRequestDto
    {
        public string? NewUsername { get; set; }
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string? Server { get; set; }
        public int? DepartmentId { get; set; }
        public string? UserDetails { get; set; }
        public double? LimitGB { get; set; }
    }
}
