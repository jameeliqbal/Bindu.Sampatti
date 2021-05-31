 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Designations
{
    public interface IDesignationRepository : IRepository<Designation, Guid>
    {
        Task<Designation> FindByNameAsync(string name);

        Task<List<Designation>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
    }
}
