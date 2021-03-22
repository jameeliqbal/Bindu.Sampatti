using System;
using System.Collections.Generic;
using System.Text;
using Bindu.Sampatti.Localization;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti
{
    /* Inherit your application services from this class.
     */
    public abstract class SampattiAppService : ApplicationService
    {
        protected SampattiAppService()
        {
            LocalizationResource = typeof(SampattiResource);
        }
    }
}
