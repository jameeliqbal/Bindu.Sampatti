using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Plants
{
    public class LocationLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
