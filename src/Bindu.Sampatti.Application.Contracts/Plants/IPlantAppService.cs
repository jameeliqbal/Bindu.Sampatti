using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bindu.Sampatti.Plants
{
    public interface IPlantAppService : IApplicationService
    {
        Task<PlantDto> GetAsync(Guid id);
        Task<PagedResultDto<PlantDto>> GetListAsync(GetPlantListDto input);
        Task<PlantDto> CreateAsync(CreatePlantDto input);
        Task UpdatePlant(Guid id, UpdatePlantDto input);
        Task DeletePlant(Guid id);
        Task<ListResultDto<LocationLookupDto>> GetLocationLookupAsync();
    }
}
