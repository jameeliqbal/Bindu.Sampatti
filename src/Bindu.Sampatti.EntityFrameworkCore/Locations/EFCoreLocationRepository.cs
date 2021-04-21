using Bindu.Sampatti.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Bindu.Sampatti.Locations
{
    public class EFCoreLocationRepository : EfCoreRepository<SampattiDbContext,Location,Guid>,ILocationRepository
    {
        public EFCoreLocationRepository(IDbContextProvider<SampattiDbContext> dbContextProvider):base(dbContextProvider)
        {
        }

        public async Task<Location> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(location => location.Name == name);
        }

        public async Task<List<Location>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(),
                    location => location.Name.Contains(filter)
                    )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
              
                 
        }



    }
}
