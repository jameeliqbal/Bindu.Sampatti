using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Bindu.Sampatti.Web
{
    [Dependency(ReplaceServices = true)]
    public class SampattiBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Sampatti";
    }
}
