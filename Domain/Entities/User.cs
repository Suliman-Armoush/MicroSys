using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public class User
  {
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }
    public required string Phone { get; set; }

  }
}
 