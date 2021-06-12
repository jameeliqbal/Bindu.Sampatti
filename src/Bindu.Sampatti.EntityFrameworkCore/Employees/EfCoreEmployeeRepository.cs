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

namespace Bindu.Sampatti.Employees
{
    public class EfCoreEmployeeRepository : EfCoreRepository<SampattiDbContext, Employee, Guid>, IEmployeeRepository
    {
        public EfCoreEmployeeRepository(IDbContextProvider<SampattiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<Employee> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(employee => employee.Name == name);
        }

        public async Task<Employee> FindByEmployeeCodeAsync(string code)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(employee => employee.Code == code);
        }

        public async Task<List<Employee>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();

            var listOfEmployees = await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), employee => employee.Name.Contains(filter))
                                    .OrderBy(sorting)
                                    .Skip(skipCount)
                                    .Take(maxResultCount)
                                    .ToListAsync();

            return listOfEmployees;

        }
    }
}
