using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Departments;
using Bindu.Sampatti.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Organisation.Department
{
    public class CreateModalModel : SampattiPageModel
    {
        [BindProperty]
        public CreateDepartmentViewModal NewDepartment { get; set; }
        public List<SelectListItem> DepartmentStatus { get; set; }

        private readonly IDepartmentAppService _departmentAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public CreateModalModel(IDepartmentAppService departmentAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _departmentAppService = departmentAppService;
        }

        public void OnGet()
        {
            DepartmentStatus = GetDepartmentStatus();
            NewDepartment = new CreateDepartmentViewModal();
            NewDepartment.Status = true.ToString();
        }

        private List<SelectListItem> GetDepartmentStatus()
        {
            var departmentStatus = new List<SelectListItem>();

            var active = new SelectListItem();
            active.Value = true.ToString();
            active.Text = _localizer["DepartmentStatusActive"];

            var inActive = new SelectListItem();
            inActive.Value = false.ToString();
            inActive.Text = _localizer["DepartmentStatusInActive"];

            departmentStatus.AddRange(new List<SelectListItem> { active, inActive });
            return departmentStatus;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dtoNewDepartment = ObjectMapper.Map<CreateDepartmentViewModal, CreateDepartmentDto>(NewDepartment);
            await _departmentAppService.CreateAsync(dtoNewDepartment);
            return Content(NewDepartment.Name);
        }

        public class CreateDepartmentViewModal
        {
            [Required]
            [StringLength(DepartmentConsts.MaxNameLength)]
            public string Name { get; set; }

            [TextArea]
            public string Notes { get; set; }

            [Required]
            public string Status { get; set; }
        }

    }
}
