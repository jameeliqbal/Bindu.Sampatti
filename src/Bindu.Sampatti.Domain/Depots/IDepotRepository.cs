using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Depots
{
    public interface IDepotRepository : IRepository<Depot,Guid>
    {
        Task<Depot> FindByNameAsync(string name);

        Task<List<Depot>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter=null);
    }
}
