using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Depots
{
    public class DepotAlreadyExistsException : BusinessException
    {
        public DepotAlreadyExistsException(string name): base(SampattiDomainErrorCodes.DepotAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
