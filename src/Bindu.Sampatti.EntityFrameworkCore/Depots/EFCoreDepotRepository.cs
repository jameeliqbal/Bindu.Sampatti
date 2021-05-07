using Bindu.Sampatti.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Bindu.Sampatti.Depots
{
    public class EFCoreDepotRepository : EfCoreRepository<SampattiDbContext, Depot, Guid>, IDepotRepository
    {
        public EFCoreDepotRepository(IDbContextProvider<SampattiDbContext> dbContextProvider): base(dbContextProvider)
        {
        }


        public async Task<Depot> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(depot => depot.Name == name);
        }



        public async Task<List<Depot>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            var listOfDepots = await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), depot => depot.Name.Contains(filter))
                                    .OrderBy(sorting)
                                    .Skip(skipCount)
                                    .Take(maxResultCount)
                                    .ToListAsync();

            return listOfDepots;

        }
    }
}
