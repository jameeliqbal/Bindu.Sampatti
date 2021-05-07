using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Depots;
using Bindu.Sampatti.Localization;
using Microsoft.AspNetCore.Mvc;
 
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Organisation.Depot
{
    public class CreateModalModel :  SampattiPageModel
    {
        [BindProperty]
        public CreateDepotViewModal NewDepot { get; set; }
        public List<SelectListItem> DepotStatus { get; set; }
        public List<SelectListItem> Locations { get; set; }

        private readonly IDepotAppService _depotAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public CreateModalModel(IDepotAppService depotAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _depotAppService = depotAppService;
        }

        public async Task OnGetAsync()
        {
            DepotStatus = GetDepotStatus();

            Locations = await GetLocationLookupItems();

            NewDepot = new CreateDepotViewModal();
            NewDepot.Status = true.ToString();
            NewDepot.Location = Locations[0].Value;

        }

        private async Task<List<SelectListItem>> GetLocationLookupItems()
        {
            List<SelectListItem> locations = new List<SelectListItem>();

            var locationsLookupItems = await _depotAppService.GetLocationLookupAsync();
            locations = locationsLookupItems.Items
                           .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                           .ToList();

            return locations;
        }

        private List<SelectListItem> GetDepotStatus()
        {

           var depotStatus = new List<SelectListItem>();
            
            var active = new SelectListItem();
            active.Value = true.ToString();
            active.Text = _localizer["DepotStatusActive"];

            var inActive = new SelectListItem();
            inActive.Value = false.ToString();
            inActive.Text = _localizer["DepotStatusInActive"];

            depotStatus.AddRange(new List<SelectListItem> { active, inActive });
            return depotStatus;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dtoNewDepot = ObjectMapper.Map<CreateDepotViewModal, CreateDepotDto>(NewDepot);
            await _depotAppService.CreateAsync(dtoNewDepot);
            return Content(NewDepot.Name);
        }

        public class CreateDepotViewModal
        {
            [Required]
            [StringLength(DepotConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            public string Location { get; set; }

            [TextArea]
            public string Notes { get; set; }

            [Required]
            public string Status { get; set; }
        }
    }
}
