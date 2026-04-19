using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Request
{
    public class DepartmentRequestDto
    {
        public required string Name { get; set; }
    }
}
