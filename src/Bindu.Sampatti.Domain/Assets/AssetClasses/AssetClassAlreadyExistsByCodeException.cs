using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bindu.Sampatti.Assets.AssetClasses
{
    public class AssetClassAlreadyExistsByCodeException: BusinessException
    {
        public AssetClassAlreadyExistsByCodeException(string code) : base(SampattiDomainErrorCodes.EmployeeByCodeAlreadyExists)
        {
            WithData("code", code);
        }
    }
}
