using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Designations;
using Bindu.Sampatti.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Organisation.Designation
{
    public class EditModalModel : SampattiPageModel
    {
        [BindProperty]
        public EditDesignationViewModal ExistingDesignation { get; set; }
        public List<SelectListItem> DesignationStatus { get; set; }

        private readonly IDesignationAppService _designationAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public EditModalModel(IDesignationAppService designationAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _designationAppService = designationAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            DesignationStatus = GetDesignationStatus();

            var designationDto = await _designationAppService.GetAsync(id);
            ExistingDesignation = ObjectMapper.Map<DesignationDto, EditDesignationViewModal>(designationDto);

        }


        private List<SelectListItem> GetDesignationStatus()
        {

            var designationStatus = new List<SelectListItem>();

            var active = new SelectListItem();
            active.Value = true.ToString();
            active.Text = _localizer["DesignationStatusActive"];

            var inActive = new SelectListItem();
            inActive.Value = false.ToString();
            inActive.Text = _localizer["DesignationStatusInActive"];

            designationStatus.AddRange(new List<SelectListItem> { active, inActive });
            return designationStatus;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingDesignationDto = ObjectMapper.Map<EditDesignationViewModal, UpdateDesignationDto>(ExistingDesignation);
            await _designationAppService.UpdateAsync(ExistingDesignation.Id, existingDesignationDto);

            return Content(ExistingDesignation.Name);
        }

        public class EditDesignationViewModal
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(DesignationConsts.MaxNameLength)]
            public string Name { get; set; }

            [TextArea]
            public string Notes { get; set; }

            [Required]
            public string Status { get; set; }
        }
    }
}
 
