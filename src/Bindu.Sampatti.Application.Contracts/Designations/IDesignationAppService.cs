using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti.Designations
{
    public interface IDesignationAppService : IApplicationService
    {
        Task<DesignationDto> GetAsync(Guid id);

        Task<PagedResultDto<DesignationDto>> GetListAsync(GetDesignationListDto input);

        Task<DesignationDto> CreateAsync(CreateDesignationDto input);

        Task UpdateAsync(Guid id, UpdateDesignationDto input);

        Task DeleteAsync(Guid id);
    }
}
