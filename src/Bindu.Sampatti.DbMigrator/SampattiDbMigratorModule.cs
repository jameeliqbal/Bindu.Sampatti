using Bindu.Sampatti.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Bindu.Sampatti.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SampattiEntityFrameworkCoreDbMigrationsModule),
        typeof(SampattiApplicationContractsModule)
        )]
    public class SampattiDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
