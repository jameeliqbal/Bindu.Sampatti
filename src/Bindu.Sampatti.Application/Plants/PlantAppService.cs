using Bindu.Sampatti.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Bindu.Sampatti.Plants
{
    public class PlantAppService : SampattiAppService, IPlantAppService
    {
        private readonly IPlantRepository _plantRepository;
        private readonly PlantManager _plantManager;
        private readonly ILocationRepository _locationRepository;

        public PlantAppService(IPlantRepository plantRepository, PlantManager plantManager, ILocationRepository locationRepository)
        {
            _plantRepository = plantRepository;
            _plantManager = plantManager;
            _locationRepository = locationRepository;
        }
        public async Task<PlantDto> GetAsync(Guid id)
        {
             

            //Get the IQueryable<Plant> from the repository
            var queryable = await _plantRepository.GetQueryableAsync();

            //Prepare a query to join Plant and locations
            var query = from plant in queryable
                        join location in _locationRepository on plant.Location equals location.Id
                        where plant.Id == id
                        select new { plant, location };

            //Execute the query and get the Plant with location
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Plant), id);
            }

            var plantDto = ObjectMapper.Map<Plant, PlantDto>(queryResult.plant);
            plantDto.LocationName = queryResult.location.Name;

            return plantDto;
        }

        public async Task<PagedResultDto<PlantDto>> GetListAsync(GetPlantListDto input)
        {
            //Get the IQueryable<Plant> from the repository
            var queryable = await _plantRepository.GetQueryableAsync();

            //Prepare a query to join Plants and Locations
            var query = from plant in queryable
                        join location in _locationRepository on plant.Location equals location.Id
                        select new { plant, location };

            //set paging info
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of PlantDto objects
            var PlantDtos = queryResult.Select(x =>
            {
                var PlantDto = ObjectMapper.Map<Plant, PlantDto>(x.plant);
                PlantDto.LocationId = x.location.Id;
                PlantDto.LocationName = x.location.Name;
                return PlantDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _plantRepository.GetCountAsync();

            return new PagedResultDto<PlantDto>(totalCount, PlantDtos);

        }

        public async Task<PlantDto> CreateAsync(CreatePlantDto input)
        {
            var Plant = await _plantManager.CreateAsync(input.Name, input.Location, input.Notes, input.Status);

            await _plantRepository.InsertAsync(Plant);

            var PlantDto = ObjectMapper.Map<Plant, PlantDto>(Plant);

            return PlantDto;
        }

        public async Task UpdatePlant(Guid id, UpdatePlantDto input)
        {
            var existingPlant = await _plantRepository.GetAsync(id);

            if (existingPlant.Name != input.Name)
            {
                await _plantManager.ChangeNameAsync(existingPlant, input.Name);
            }

            existingPlant.Location = input.Location;
            existingPlant.Notes = input.Notes;
            existingPlant.Status = input.Status;

            await _plantRepository.UpdateAsync(existingPlant);
        }

        public async Task DeletePlant(Guid id)
        {
            await _plantRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<LocationLookupDto>> GetLocationLookupAsync()
        {
            var locations = await _locationRepository.GetListAsync();
            var locationsLookupDto = ObjectMapper.Map<List<Location>, List<LocationLookupDto>>(locations);

            return new ListResultDto<LocationLookupDto>(locationsLookupDto);    
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"plant.{nameof(Plant.Name)}";
            }

            if (sorting.Contains("plantName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("plantName", "plant.Name", StringComparison.OrdinalIgnoreCase);
            }

            if (sorting.Contains("locationName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("locationName", "plant.Location", StringComparison.OrdinalIgnoreCase);
            }
            return $"plant.{sorting}";
        }
    }
}
