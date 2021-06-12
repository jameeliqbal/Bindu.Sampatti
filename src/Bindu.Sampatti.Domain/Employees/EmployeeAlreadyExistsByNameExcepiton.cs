using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Employees
{
    public class EmployeeAlreadyExistsByNameExcepiton : BusinessException
    {
        public EmployeeAlreadyExistsByNameExcepiton(string name) : base(SampattiDomainErrorCodes.EmployeeByNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
