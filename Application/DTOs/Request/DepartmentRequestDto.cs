using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Request
{
    public class DepartmentRequestDto
    {
        public string Name { get; set; }
        public DepartmentTypes Type { get; set; }
        public int DvrNum { get; set; } = 0;
    }
}
