using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DepartmentTypes Type { get; set; }
        public int DvrNum { get; set; } = 0;

    }
}
