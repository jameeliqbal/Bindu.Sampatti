using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Locations
{
    public class GetLocationListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}
