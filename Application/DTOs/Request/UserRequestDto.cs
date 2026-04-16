using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Request
{
    public class UserRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
    }
}
