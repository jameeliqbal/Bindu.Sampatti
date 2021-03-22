using Bindu.Sampatti.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Bindu.Sampatti.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class SampattiPageModel : AbpPageModel
    {
        protected SampattiPageModel()
        {
            LocalizationResourceType = typeof(SampattiResource);
        }
    }
}