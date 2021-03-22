using Bindu.Sampatti.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Bindu.Sampatti
{
    [DependsOn(
        typeof(SampattiEntityFrameworkCoreTestModule)
        )]
    public class SampattiDomainTestModule : AbpModule
    {

    }
}