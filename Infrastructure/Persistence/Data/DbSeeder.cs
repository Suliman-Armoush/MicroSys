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
                    UserName = "IT",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("it@123456"),
                    RoleId = adminRole.Id,
                    CreatePerm = true,
                    UpdatePerm = true,
                    ChangePerm = true


                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }

            if (!context.SysInfos.Any())
            {
                context.SysInfos.AddRange(
                    new SysInfo { MikroTikIp = "10.10.10.88", Username = "it", Password = "it@123456", DvrPrice = 10000 }
                );

                await context.SaveChangesAsync();
            }

        }
    }

}
