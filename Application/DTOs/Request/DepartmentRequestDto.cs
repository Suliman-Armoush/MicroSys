using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Request
{
    public class DepartmentRequestDto
    {
        [Required(ErrorMessage = "The name is required ")]
        public string Name { get; set; }
    }
}
