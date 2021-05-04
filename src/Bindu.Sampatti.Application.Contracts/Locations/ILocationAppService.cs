using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti.Locations
{
    public interface ILocationAppService : IApplicationService
    {
        Task<LocationDto> GetAsync(Guid id);
        Task<PagedResultDto<LocationDto>> GetListAsync(GetLocationListDto input);
        Task<LocationDto> CreateAsync(CreateLocationDto input);
        Task UpdateAsnyc(Guid id, UpdateLocationDto input);
        Task DeleteAsync(Guid id);
    }
}
