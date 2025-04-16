using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Transactions.Core.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("SERVER=DESKTOP-U7410KN; DATABASE=Transaction; Trusted_Connection=true; TrustServerCertificate=true;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}
