using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Request
{
    public class UpdateUserRequestDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
