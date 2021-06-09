using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Employees
{
    public class EmployeeAlreadyExistsByCodeException : BusinessException
    {
        public EmployeeAlreadyExistsByCodeException(string code) : base(SampattiDomainErrorCodes.EmployeeByCodeAlreadyExists)
        {
            WithData("code", code);
        }
    }
}
