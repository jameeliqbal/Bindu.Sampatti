using Volo.Abp.Modularity;

namespace Bindu.Sampatti
{
    [DependsOn(
        typeof(SampattiApplicationModule),
        typeof(SampattiDomainTestModule)
        )]
    public class SampattiApplicationTestModule : AbpModule
    {

    }
}