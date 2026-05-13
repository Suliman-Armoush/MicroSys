using System.ComponentModel.DataAnnotations;

namespace UI.Models.Request
{
    public class CreateMikrotikUserRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Profile is required")]
        public string Profile { get; set; } = string.Empty;

        [Required(ErrorMessage = "Server is required")]
        public string Server { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Selected department is invalid")]
        public int DepartmentId { get; set; }

        public string UserDetails { get; set; } = string.Empty;

        public double? LimitGB { get; set; }

        // 👇 أضف هذه الخاصية لتخزين MAC للمستخدم (اختياري)
        public string? MacAddress { get; set; }
    }
}
