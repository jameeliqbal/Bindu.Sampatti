using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Depots
{
    public class DepotAppService : SampattiAppService, IDepotAppService
    {
        private readonly IDepotRepository _depotRepository;
        private readonly DepotManager _depotManager;

        public DepotAppService(IDepotRepository depotRepository, DepotManager depotManager)
        {
            _depotRepository = depotRepository;
            _depotManager = depotManager;
        }
        public async Task<DepotDto> GetAsync(Guid id)
        {
            var depot  = await _depotRepository.GetAsync(id);
            return ObjectMapper.Map<Depot, DepotDto>(depot);
        }

        public async Task<PagedResultDto<DepotDto>> GetListAsync(GetDepotListDto input)
        {
            //Set default sorting if not provided
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Depot.Name);
            }

            //get list of depots
            var depots = await _depotRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);
            //convert list to dto
            var depotsDto = ObjectMapper.Map<List<Depot>, List<DepotDto>>(depots);

            //get total count of depots
            var totatCount = input.Filter != null ? 
                await _depotRepository.CountAsync() 
                : await _depotRepository.CountAsync(depot=>depot.Name.Contains( input.Filter));

            //combine total and depots list
            var pagedResult = new PagedResultDto<DepotDto>(totatCount,depotsDto);

            //return the list
            return pagedResult;
        }

       public async Task<DepotDto> CreateAsync(CreateDepotDto input)
        {
            var depot = await _depotManager.CreateAsync(input.Name, input.Location, input.Notes, input.Status);

            await _depotRepository.InsertAsync(depot);

            var depotDto = ObjectMapper.Map<Depot, DepotDto>(depot);

            return depotDto;
        }
 
        public async Task UpdateDepot(Guid id, UpdateDepotDto input)
        {
            var existingDepot = await _depotRepository.GetAsync(id);

            if (existingDepot.Name != input.Name)
            {
                await _depotManager.ChangeNameAsync(existingDepot, input.Name);
            }

            existingDepot.Location = input.Location;
            existingDepot.Notes = input.Notes;
            existingDepot.Status = input.Status;

            await _depotRepository.UpdateAsync(existingDepot);
         }

        public async Task DeleteDepot(Guid id)
        {
            await _depotRepository.DeleteAsync(id);
        }
    }
}
