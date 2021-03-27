using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Procurement.PurchaseRequisition
{
    public class CreateModalModel : PageModel
    {
        [BindProperty]
        public CreatePRViewModel PR { get; set; }
        public async Task OnGetAsync()
        {
        }

        public class CreatePRViewModel
        {
            public string PRNumber { get; set; }
            public DateTime Date { get; set; }
            public string Requisitioner { get; set; }
            public string Department { get; set; }
            public string Section { get; set; }
            [TextArea]
            public string Remarks { get; set; }
        }
    }
}
