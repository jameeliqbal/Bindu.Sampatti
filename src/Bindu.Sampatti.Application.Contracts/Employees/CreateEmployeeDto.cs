using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bindu.Sampatti.Employees
{
    public class CreateEmployeeDto
    {
        [Required]
        [StringLength(EmployeeConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(EmployeeConsts.MaxCodeLength)]
        public string Code { get; set; }

        public Guid Designation { get; set; }

        public Guid Department { get; set; }

        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
