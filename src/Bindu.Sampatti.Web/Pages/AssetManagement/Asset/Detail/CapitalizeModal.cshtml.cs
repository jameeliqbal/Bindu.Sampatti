using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.AssetManagement.Asset.Detail
{
    public class CapitalizeModalModel : PageModel
    {
        public CapitalizeAssetViewModal CapitalizeAsset { get; set; }

        public void OnGet()
        {
        }

        public class CapitalizeAssetViewModal
        {
            public DateTime CapitalizationDate { get; set; }
            public string Location { get; set; }
            public string Custodian { get; set; }
             
            public string Certificate { get; set; }
        }
    }
    
    
}
