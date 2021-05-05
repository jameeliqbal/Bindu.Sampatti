using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti.Depots
{
    public interface IDepotAppService : IApplicationService
    {
        Task<DepotDto> GetAsync(Guid id);
        Task<PagedResultDto<DepotDto>> GetListAsync(GetDepotListDto input);
        Task<DepotDto> CreateAsync(CreateDepotDto input);
        Task UpdateDepot(Guid id, UpdateDepotDto input);
        Task DeleteDepot(Guid id);

    }
}
