using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Plants
{
    public class PlantAlreadyExistsException : BusinessException
    {
        public PlantAlreadyExistsException(string name) : base(SampattiDomainErrorCodes.PlantAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
