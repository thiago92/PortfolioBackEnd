using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Coloque sua connection string do MySQL aqui
            optionsBuilder.UseMySql(
                "server=auth-db1437.hstgr.io;database=u204118466_portfolio;user=u204118466_portfoliodb;password=*060592Po;",
                new MySqlServerVersion(new Version(8, 0, 33)) // ou a versão que você estiver usando
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
