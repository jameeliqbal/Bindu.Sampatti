using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Assets.AssetClasses
{
    public class AssetClassAlreadyExistsByNameExcepiton : BusinessException
    {
        public AssetClassAlreadyExistsByNameExcepiton(string name) : base(SampattiDomainErrorCodes.EmployeeByNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
