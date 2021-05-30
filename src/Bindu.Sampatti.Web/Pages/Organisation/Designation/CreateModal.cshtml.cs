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
    public class CreateModalModel : SampattiPageModel
    {
        [BindProperty]
        public CreateDesignationViewModal NewDesignation { get; set; }
        public List<SelectListItem> DesignationStatus { get; set; }

        private readonly IDesignationAppService _designationAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public CreateModalModel(IDesignationAppService designationAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _designationAppService = designationAppService;
        }

        public void OnGet()
        {
            DesignationStatus = GetDepartmentStatus();
            NewDesignation = new CreateDesignationViewModal();
            NewDesignation.Status = true.ToString();
        }

        private List<SelectListItem> GetDepartmentStatus()
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
            var dtoNewDesignation = ObjectMapper.Map<CreateDesignationViewModal, CreateDesignationDto>(NewDesignation);
            await _designationAppService.CreateAsync(dtoNewDesignation);
            return Content(NewDesignation.Name);
        }


        public class CreateDesignationViewModal
        {
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
