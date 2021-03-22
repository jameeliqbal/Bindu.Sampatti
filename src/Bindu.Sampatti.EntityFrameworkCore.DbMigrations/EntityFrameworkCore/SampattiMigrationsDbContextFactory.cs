using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Bindu.Sampatti.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class SampattiMigrationsDbContextFactory : IDesignTimeDbContextFactory<SampattiMigrationsDbContext>
    {
        public SampattiMigrationsDbContext CreateDbContext(string[] args)
        {
            SampattiEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SampattiMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new SampattiMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Bindu.Sampatti.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
