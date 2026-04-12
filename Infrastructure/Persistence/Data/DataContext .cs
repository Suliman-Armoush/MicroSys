using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data
{
  public class DataContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
    : base(options) { }

  }
}
