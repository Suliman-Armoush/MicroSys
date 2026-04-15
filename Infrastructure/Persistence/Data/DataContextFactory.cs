namespace MicroSys.Infrastructure.Persistence.Data
{
    using global::Infrastructure.Persistence.Data;
    using global::MicroSys.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    namespace Infrastructure.Persistence.Data
    {
        public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
        {
            public DataContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

                optionsBuilder.UseSqlServer(
                                        "Data Source=HOMAMNASSER; Initial Catalog = MikroTikDb; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False; Command Timeout = 30");

                return new DataContext(optionsBuilder.Options);
            }
        }
    }
}
