using Bindu.Sampatti.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Bindu.Sampatti.Designations
{
    public class EFCoreDesignationRepository : EfCoreRepository<SampattiDbContext, Designation, Guid>, IDesignationRepository
    {
        public EFCoreDesignationRepository(IDbContextProvider<SampattiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<Designation> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(depot => depot.Name == name);
        }



        public async Task<List<Designation>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            var listOfDesignations = await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), designation => designation.Name.Contains(filter))
                                    .OrderBy(sorting)
                                    .Skip(skipCount)
                                    .Take(maxResultCount)
                                    .ToListAsync();

            return listOfDesignations;

        }

    }
}
