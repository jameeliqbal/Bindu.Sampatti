using System;
 using System.ComponentModel.DataAnnotations;
 using System.Threading.Tasks;
 using Bindu.Sampatti.Locations;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Bindu.Sampatti.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Bindu.Sampatti.Web.Pages.Organisation.Location
{
    public class CreateModalModel : SampattiPageModel
    {
        [BindProperty]
        public CreateLocationViewModal  NewLocation { get; set; }

        public List<SelectListItem> LocationStatus { get; set; } 
        private readonly ILocationAppService _locationAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public CreateModalModel(ILocationAppService locationAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _locationAppService = locationAppService;
            _localizer = localizer;


        }

        public void OnGet()
        {
            LocationStatus = new List<SelectListItem>();

            var active = new SelectListItem();
            active.Value = true.ToString();
            active.Text = _localizer["LocationStatusActive"];

            var inActive = new SelectListItem();
            inActive.Value = false.ToString();
            inActive.Text = _localizer["LocationStatusInActive"];

            LocationStatus.AddRange(new List<SelectListItem> { active, inActive });

            NewLocation = new CreateLocationViewModal();
            NewLocation.Status = true.ToString();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dtoNewLocation =  ObjectMapper.Map<CreateLocationViewModal, CreateLocationDto>(NewLocation);
            await _locationAppService.CreateAsync(dtoNewLocation);
            return Content(NewLocation.Name);
        }

        public class CreateLocationViewModal
        {
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
