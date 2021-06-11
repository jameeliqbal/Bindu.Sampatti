using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti.Employees
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<EmployeeDto> GetAsync(Guid id);
        Task<EmployeeDto> GetByEmployeeCodeAsync(string employeeCode);
        Task<PagedResultDto<EmployeeDto>> GetListAsync(GetEmployeeListDto input);
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto input);
        Task UpdateAsync(Guid id, UpdateEmployeeDto input);
        Task DeleteAsync(Guid id);
        Task<ListResultDto<DesignationLookupDto>> GetDesignationLookupAsync();
        Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync();

    }
}
