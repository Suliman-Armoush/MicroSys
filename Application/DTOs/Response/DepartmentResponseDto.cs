using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
    public class DepartmentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DepartmentTypes Type { get; set; }
        public int DvrNum { get; set; } = 0;
    }
}
