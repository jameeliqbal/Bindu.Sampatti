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

namespace Bindu.Sampatti.Departments
{
    public class EFCoreDepartmentRepository : EfCoreRepository<SampattiDbContext, Department, Guid>, IDepartmentRepository
    {
        public EFCoreDepartmentRepository(IDbContextProvider<SampattiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<Department> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(depot => depot.Name == name);
        }



        public async Task<List<Department>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            var listOfPlants = await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), depot => depot.Name.Contains(filter))
                                    .OrderBy(sorting)
                                    .Skip(skipCount)
                                    .Take(maxResultCount)
                                    .ToListAsync();

            return listOfPlants;

        }

    }
}
