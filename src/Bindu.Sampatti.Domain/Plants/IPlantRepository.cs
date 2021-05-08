using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Plants
{
    public interface IPlantRepository : IRepository<Plant, Guid>
    {
        Task<Plant> FindByNameAsync(string name);

        Task<List<Plant>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
