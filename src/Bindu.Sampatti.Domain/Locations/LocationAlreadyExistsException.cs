using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Locations
{
    public class LocationAlreadyExistsException : BusinessException
    {
        public LocationAlreadyExistsException(string name) : base(SampattiDomainErrorCodes.LocationAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
