using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieStoreApi.Repository
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Data Source=RECEP;Initial Catalog=MovieStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
