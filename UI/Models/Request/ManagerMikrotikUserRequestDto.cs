using System.ComponentModel.DataAnnotations;

namespace UI.Models.Request
{
    public class ManagerMikrotikUserRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Profile is required")]
        public string Profile { get; set; } = string.Empty;

        public string UserDetails { get; set; } = string.Empty;
        public double? LimitGB { get; set; }
        public string? MacAddress { get; set; }
    }
}
