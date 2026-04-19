using Azure.Core;
using Domain.Entities;

namespace Infrastructure.Persistence.Data
{
  public static class DbSeeder
  {
    public static async Task SeedAsync(DataContext context)
    {
      if (!context.Roles.Any())
      {
        context.Roles.AddRange(
            new Role { Name = "Admin" },
            new Role { Name = "Manager" },
            new Role { Name = "ACC" }
        );

        await context.SaveChangesAsync();
      }

      // Seed Admin User
      if (!context.Users.Any(u => u.UserName == "Admin"))
      {
        var adminRole = context.Roles.First(r => r.Name == "Admin");

        var admin = new User
        {
          Name = "Administrator",
          UserName = "Admin",
          PasswordHash = BCrypt.Net.BCrypt.HashPassword("it@123456"),
          RoleId = adminRole.Id,
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();
      }
    }
  }

}
