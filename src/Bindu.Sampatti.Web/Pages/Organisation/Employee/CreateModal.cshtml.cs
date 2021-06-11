using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bindu.Sampatti.Employees;
using Bindu.Sampatti.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.Organisation.Employee
{
    public class CreateModalModel : SampattiPageModel
    {
        [BindProperty]
        public CreateEmployeeViewModal NewEmployee { get; set; }
        public List<SelectListItem> EmployeeStatus { get; set; }
        public List<SelectListItem> Designations { get; set; }
        public List<SelectListItem> Departments { get; set; }

        private readonly IEmployeeAppService _employeeAppService;
        private readonly IStringLocalizer<SampattiResource> _localizer;

        public CreateModalModel(IEmployeeAppService employeeAppService, IStringLocalizer<SampattiResource> localizer)
        {
            _localizer = localizer;
            _employeeAppService = employeeAppService;
        }

        public async Task OnGetAsync()
        {
            EmployeeStatus = GetDepotStatus();

            Designations = await GetDesignations();
            Departments = await GetDepartments();

            NewEmployee = new CreateEmployeeViewModal();
            NewEmployee.Status = true.ToString();
            NewEmployee.Designation = Designations[0].Value;
            NewEmployee.Department = Departments[0].Value;

        }

        private async Task<List<SelectListItem>> GetDepartments()
        {
            List<SelectListItem> departments = new List<SelectListItem>();

            var departmentLookupItems = await _employeeAppService.GetDepartmentLookupAsync();
            departments = departmentLookupItems.Items
                           .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                           .ToList();

            return departments;
        }

        private async Task<List<SelectListItem>> GetDesignations()
        {
            List<SelectListItem> designations = new List<SelectListItem>();

            var designationLookupItems = await _employeeAppService.GetDesignationLookupAsync();
            designations = designationLookupItems.Items
                           .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                           .ToList();

            return designations;
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
            var dtoNewEmployee = ObjectMapper.Map<CreateEmployeeViewModal, CreateEmployeeDto>(NewEmployee);
            await _employeeAppService.CreateAsync(dtoNewEmployee);
            return Content(NewEmployee.Name);
        }

        public class CreateEmployeeViewModal
        {
            [Required]
            [StringLength(EmployeeConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(EmployeeConsts.MaxCodeLength)]
            public string Code { get; set; }

            [Required]
            public string Designation { get; set; }

            [Required]
            public string Department { get; set; }

            [TextArea]
            public string Notes { get; set; }

            [Required]
            public string Status { get; set; }
        }
    }
}
