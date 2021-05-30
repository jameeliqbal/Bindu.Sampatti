using Bindu.Sampatti.Departments;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti.Designations
{
    public interface IDesignationAppService : IApplicationService
    {
        Task<DesignationDto> GetAsync(Guid id);

        Task<PagedResultDto<DesignationDto>> GetListAsync(GetDepartmentListDto input);

        Task<DesignationDto> CreateAsync(CreateDepartmentDto input);

        Task UpdateAsync(Guid id, UpdateDepartmentDto input);

        Task DeleteAsync(Guid id);
    }
}
