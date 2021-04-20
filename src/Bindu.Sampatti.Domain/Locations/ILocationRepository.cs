using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Locations
{
    public interface ILocationRepository : IRepository<Location,Guid>
    {
        Task<Location> FindByNameAsync(string name);

        Task<List<Location>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null            
        );
         
    }
}
