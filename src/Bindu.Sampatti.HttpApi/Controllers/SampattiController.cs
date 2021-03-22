using Bindu.Sampatti.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Bindu.Sampatti.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class SampattiController : AbpController
    {
        protected SampattiController()
        {
            LocalizationResource = typeof(SampattiResource);
        }
    }
}