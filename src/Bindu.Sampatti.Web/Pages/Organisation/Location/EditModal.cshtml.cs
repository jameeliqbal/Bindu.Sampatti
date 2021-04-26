using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Localization;
using Bindu.Sampatti.Locations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Organisation.Location
{
    public class EditModalModel : SampattiPageModel
    {
        [BindProperty]
        public EditLocationViewModal CurrentLocation { get; set; }
        public List<SelectListItem> LocationStatus { get; set; }

        private readonly ILocationAppService _locationAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public EditModalModel(ILocationAppService locationAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _locationAppService = locationAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var locationDto = await _locationAppService.GetAsync(id);
            CurrentLocation = ObjectMapper.Map<LocationDto, EditLocationViewModal>(locationDto);

           
            LocationStatus = new List<SelectListItem>();
            var active = new SelectListItem();
            active.Value =   true.ToString();
            active.Text = _localizer["LocationStatusActive"];

            var inActive = new SelectListItem();
            inActive.Value = false.ToString();
            inActive.Text = _localizer["LocationStatusInActive"];

            
            LocationStatus.AddRange(new List<SelectListItem> { active, inActive });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentLocationDto =   ObjectMapper.Map<EditLocationViewModal, UpdateLocationDto>(CurrentLocation);
            await _locationAppService.UpdateAsnyc(CurrentLocation.Id, currentLocationDto);

            return Content(CurrentLocation.Name);
        }

        public class EditLocationViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(LocationConsts.MaxNameLength)]
            public string Name { get; set; }

            [TextArea]
            public string Notes { get; set; }

            [Required]
            public string Status { get; set; }
        }
    }
}
