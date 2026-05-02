namespace UI.Models.Request
{
  public class UserRequestDto
  {
    public string? Name { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
  }
}
