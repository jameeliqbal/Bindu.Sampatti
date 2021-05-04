using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Depots
{
    public class GetDepotListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        
    }
}
