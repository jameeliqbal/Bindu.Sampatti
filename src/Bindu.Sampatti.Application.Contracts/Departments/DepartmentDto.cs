using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Departments
{
    public class DepartmentDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
