using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Designations
{
    public class DesignationAlreadyExistsException : BusinessException
    {
        public DesignationAlreadyExistsException(string name) : base(SampattiDomainErrorCodes.DesignationAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
