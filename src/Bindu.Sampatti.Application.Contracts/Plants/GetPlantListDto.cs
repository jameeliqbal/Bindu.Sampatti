using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Plants
{
    public class GetPlantListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}
