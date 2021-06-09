using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Employees
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        Task<Employee> FindByEmployeeCode(string code);

        Task<Employee> FindByNameAsync(string name);

        Task<List<Employee>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
