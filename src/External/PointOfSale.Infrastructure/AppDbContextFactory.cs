using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PointOfSale.Infrastructure;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseMySql("Server=127.0.0.1;Database=pointofsaledb;User Id=root;Pwd=root;", new MariaDbServerVersion(new Version(10, 4, 28)));
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
