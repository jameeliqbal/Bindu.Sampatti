using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Bindu.Sampatti.EntityFrameworkCore
{
    [DependsOn(
        typeof(SampattiEntityFrameworkCoreModule)
        )]
    public class SampattiEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SampattiMigrationsDbContext>();
        }
    }
}
