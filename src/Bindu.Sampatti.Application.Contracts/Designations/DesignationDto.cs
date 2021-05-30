using System;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Designations
{
    public class DesignationDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
