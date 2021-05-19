using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Departments
{
    public class DepartmentAlreadyExistsException : BusinessException
    {
        public DepartmentAlreadyExistsException(string name) : base(SampattiDomainErrorCodes.DepartmentAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
