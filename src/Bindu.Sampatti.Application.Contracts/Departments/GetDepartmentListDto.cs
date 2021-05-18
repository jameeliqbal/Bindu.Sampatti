using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Departments
{
    public class GetDepartmentListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
