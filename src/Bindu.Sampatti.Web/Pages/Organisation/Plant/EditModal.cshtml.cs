using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Plants;
using Bindu.Sampatti.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
 

namespace Bindu.Sampatti.Web.Pages.Organisation.Plant
{
    public class EditModalModel : SampattiPageModel
    {
        [BindProperty]
        public EditPlantViewModal ExistingPlant { get; set; }
        public List<SelectListItem> PlantStatus { get; set; }
        public List<SelectListItem> Locations { get; set; }

        private readonly IPlantAppService _plantAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public EditModalModel(IPlantAppService plantAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _plantAppService = plantAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            PlantStatus = GetPlantStatus();
            Locations = await GetLocationLookupItems();

            var plantDto = await _plantAppService.GetAsync(id);
            ExistingPlant = ObjectMapper.Map<PlantDto, EditPlantViewModal>(plantDto);

            Locations.SingleOrDefault(l => l.Text == plantDto.LocationName).Selected = true;
         }

        private async Task<List<SelectListItem>> GetLocationLookupItems()
        {
            List<SelectListItem> locations = new List<SelectListItem>();

            var locationsLookupItems = await _plantAppService.GetLocationLookupAsync();
            locations = locationsLookupItems.Items
                           .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                           .ToList();

            return locations;
        }

        private List<SelectListItem> GetPlantStatus()
        {

            var plantStatus = new List<SelectListItem>();

            var active = new SelectListItem();
            active.Value = true.ToString();
            active.Text = _localizer["PlantStatusActive"];

            var inActive = new SelectListItem();
            inActive.Value = false.ToString();
            inActive.Text = _localizer["PlantStatusInActive"];

            plantStatus.AddRange(new List<SelectListItem> { active, inActive });
            return plantStatus;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingPlantDto = ObjectMapper.Map<EditPlantViewModal, UpdatePlantDto>(ExistingPlant);
            await _plantAppService.UpdatePlant(ExistingPlant.Id, existingPlantDto);

            return Content(ExistingPlant.Name);
        }

        public class EditPlantViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(PlantConsts.MaxNameLength)]
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
