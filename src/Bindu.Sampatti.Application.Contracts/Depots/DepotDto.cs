using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Depots
{
    public class DepotDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public Guid Location { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}
