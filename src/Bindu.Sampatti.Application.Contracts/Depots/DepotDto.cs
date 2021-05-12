﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Depots
{
    public class DepotDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
    }
}