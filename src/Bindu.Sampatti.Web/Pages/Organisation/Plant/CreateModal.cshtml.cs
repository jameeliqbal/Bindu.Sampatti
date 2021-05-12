using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Plants;
using Bindu.Sampatti.Localization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Organisation.Plant
{
    public class CreateModalModel : SampattiPageModel

    {
        [BindProperty]
        public CreatePlantViewModal NewPlant { get; set; }
        public List<SelectListItem> PlantStatus { get; set; }
        public List<SelectListItem> Locations { get; set; }

        private readonly IPlantAppService _plantAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public CreateModalModel(IPlantAppService plantAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _plantAppService = plantAppService;
        }

        public async Task OnGetAsync()
        {
            PlantStatus = GetPlantStatus();

            Locations = await GetLocationLookupItems();

            NewPlant = new CreatePlantViewModal();
            NewPlant.Status = true.ToString();
            NewPlant.Location = Locations[0].Value;

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
            var dtoNewPlant = ObjectMapper.Map<CreatePlantViewModal, CreatePlantDto>(NewPlant);
            await _plantAppService.CreateAsync(dtoNewPlant);
            return Content(NewPlant.Name);
        }

        public class CreatePlantViewModal
        {
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
