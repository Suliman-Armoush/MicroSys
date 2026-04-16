using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public RoleResponseDto Role { get; set; } = null!;
        public DepartmentResponseDto Department { get; set; } = null!;
    }
}
