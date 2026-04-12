using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Response
{
  public class UserDto
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public int? Age { get; set; }
  public string? Phone { get; set; }
}
}
