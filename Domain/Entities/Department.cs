using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public class Department
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ArName { get; set; }
    public DepartmentTypes Type { get; set; }
    public int DvrNum { get; set; } = 0;

  }
}
