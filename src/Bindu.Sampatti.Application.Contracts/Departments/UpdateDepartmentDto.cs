using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bindu.Sampatti.Departments
{
    public class UpdateDepartmentDto
    {
        [Required]
        [StringLength(DepartmentConsts.MaxNameLength)]
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
