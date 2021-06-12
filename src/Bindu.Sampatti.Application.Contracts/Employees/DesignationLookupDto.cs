using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bindu.Sampatti.Employees
{
    public class DesignationLookupDto:EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
