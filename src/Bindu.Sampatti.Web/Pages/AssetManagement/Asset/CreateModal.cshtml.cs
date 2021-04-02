using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.AssetManagement.Asset
{
    public class CreateModalModel : PageModel
    {
        public CreateAssetViewModel Asset { get; set; }
        public void OnGet()
        {
        }


        public class CreateAssetViewModel
        {
            public string AssetCode { get; set; }
            public string AssetClass { get; set; }

            [TextArea]
            public string Description { get; set; }
            public string InvoiceNumber { get; set; }
            public DateTime? InvoiceDate { get; set; }
            public float? InvoiceValue { get; set; }
            public string LedgerAccount { get; set; }


        }
    }
}
