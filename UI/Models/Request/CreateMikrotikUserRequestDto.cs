using System.ComponentModel.DataAnnotations;

namespace UI.Models.Request
{
    public class CreateMikrotikUserRequestDto
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "يجب اختيار بروفايل")]
        public string Profile { get; set; } = string.Empty;

        [Required(ErrorMessage = "يجب اختيار سيرفر")]
        public string Server { get; set; } = string.Empty;

        [Required(ErrorMessage = "يجب تحديد القسم")]
        [Range(1, int.MaxValue, ErrorMessage = "القسم المختار غير صحيح")]
        public int DepartmentId { get; set; }

        public string UserDetails { get; set; } = string.Empty;

        public double? LimitGB { get; set; }
    }
}