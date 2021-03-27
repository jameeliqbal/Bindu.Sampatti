using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Procurement.PurchaseRequisition
{
    public class EditModalModel : PageModel
    {
        [BindProperty]
        public EditPRViewModel PR { get; set; } = new EditPRViewModel();

        public async Task OnGetAsync(string prNumber)
        {

            var prs = GetPRS();
            var pr = prs.SingleOrDefault(pr => pr.Contains(prNumber));
            pr = pr.Replace("\"", "");
            pr = pr.Replace(",[", "");
            pr = pr.Replace("]", "");
            var prArr = pr.Split(',').Skip(1).ToArray();
            PR.PRNumber = prArr[0];
            PR.Date = DateTime.Parse(prArr[1]);
            PR.Requisitioner = prArr[2]; ;
            PR.Department = prArr[3]; ;
            PR.Section = prArr[4]; ;
            PR.Remarks = ""; ;



        }

        public class EditPRViewModel
        {
            public string PRNumber { get; set; }
            public DateTime Date { get; set; }
            public string Requisitioner { get; set; }
            public string Department { get; set; }
            public string Section { get; set; }
            [TextArea]
            public string Remarks { get; set; }
        }

        public IEnumerable<string> GetPRS()
        {
            var list = new List<string>();
            list.Add("[\"\",\"PR0001\", \"2021/03/20 09:00\", \"Manager One\", \"Department One\",\"Section One\",\"Waiting for Approval\"]");
            list.Add(",[\"\",\"PR0002\", \"2021/03-21 09:00\", \"Manager Three\", \"Department Three\",\"Section Three\",\"Approved\"]");
            list.Add(",[\"\",\"PR0003\", \"2021/03/22 09:00\", \"Manager Two\", \"Department Two\",\"Section Two\",\"Rejected\"]");
            list.Add(",[\"\",\"PR0004\", \"2021/03/22 09:00\", \"Manager Three\", \"Department Three\",\"Section Three\",\"Approved Partially\"]");
            ////Laptop 4GB RAM, 256 SSD, Windows 10, 15\" screen
            //var data = "\"data\":[" + list + "]";

            //var count = 4;

            //var result = $"{{\"draw\": 1,\"recordsTotal\": {count},\"recordsFiltered\":{count}," + data + "}";
            return list;

        }
    }


}
