using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Bindu.Sampatti.Locations
{
    public class LocationAppService : SampattiAppService, ILocationAppService
    {
        private readonly ILocationRepository _locationRepo;
        private readonly LocationManager _locationManager;

        public LocationAppService(ILocationRepository locationRepo, LocationManager locationManager)
        {
            _locationRepo = locationRepo;
            _locationManager = locationManager;
        }


        public async Task<LocationDto> GetAsync(Guid id)
        {
            var location =await _locationRepo.GetAsync(id);
            return    ObjectMapper.Map<Location, LocationDto>(location);
        }

        public async Task<PagedResultDto<LocationDto>> GetListAsync(GetLocationListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Location.Name);
            }

            var locations = await _locationRepo.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null ? await _locationRepo.CountAsync() : await _locationRepo.CountAsync(
                                    location => location.Name.Contains(input.Filter));

            var locationsDto = ObjectMapper.Map<List<Location>, List<LocationDto>>(locations);

            return new PagedResultDto<LocationDto>(totalCount, locationsDto);

        }

        public async Task<LocationDto> CreateAsync(CreateLocationDto input)
        {
            var newLocation = await _locationManager.CreateAsync(
                input.Name,
                input.IsEnabled,
                input.Notes
            );

            await _locationRepo.InsertAsync(newLocation);

            return ObjectMapper.Map<Location, LocationDto>(newLocation);
        }

        public async Task UpdateAsnyc(Guid id, UpdateLocationDto input)
        {
            var existingLocation = await _locationRepo.GetAsync(id);

            if (existingLocation.Name != input.Name)
            {
                await _locationManager.ChangeNameAsync(existingLocation, input.Name);
            }

            existingLocation.Notes = input.Notes;
            existingLocation.IsEnabled = input.IsEnabled;

            await _locationRepo.UpdateAsync(existingLocation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _locationRepo.DeleteAsync(id);
        }

    }
}
