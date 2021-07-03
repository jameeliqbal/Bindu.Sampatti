using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Assets.AssetClasses
{
    public interface IAssetClassRepository : IRepository<AssetClass, Guid>
    {
        Task<AssetClass> FindByAssetClassCodeAsync(string code);

        Task<AssetClass> FindByNameAsync(string name);

        Task<List<AssetClass>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

        Task<int> GetMaxValueOfSerialNumber(Guid parent);
    }
}
