using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Employees
{
    public class EmployeeDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Guid Designation { get; set; }

        public Guid Department { get; set; }

        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
