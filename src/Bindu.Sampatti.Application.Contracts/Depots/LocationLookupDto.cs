using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Depots
{
    public class LocationLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
