using Bindu.Sampatti.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Application.Services;
    
namespace Bindu.Sampatti.Depots
{
    public class DepotAppService : SampattiAppService, IDepotAppService
    {
        private readonly IDepotRepository _depotRepository;
        private readonly DepotManager _depotManager;
        private readonly ILocationRepository _locationRepository;

        public DepotAppService(IDepotRepository depotRepository, DepotManager depotManager, ILocationRepository locationRepository)
        {
            _depotRepository = depotRepository;
            _depotManager = depotManager;
            _locationRepository = locationRepository;
        }
        public async Task<DepotDto> GetAsync(Guid id)
        {
            //var depot  = await _depotRepository.GetAsync(id);
            //return ObjectMapper.Map<Depot, DepotDto>(depot);

            //Get the IQueryable<Depot> from the repository
            var queryable = await _depotRepository.GetQueryableAsync();

            //Prepare a query to join depot and locations
            var query = from depot in queryable
                        join location in _locationRepository on depot.Location equals location.Id
                        where depot.Id == id
                        select new { depot, location };

            //Execute the query and get the depot with location
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Depot), id);
            }

            var depotDto = ObjectMapper.Map<Depot, DepotDto>(queryResult.depot);
            depotDto.LocationName = queryResult.location.Name;

            return depotDto;
        }

        public async Task<PagedResultDto<DepotDto>> GetListAsync(GetDepotListDto input)
        {
            ////Set default sorting if not provided
            //if (input.Sorting.IsNullOrWhiteSpace())
            //{
            //    input.Sorting = nameof(Depot.Name);
            //}

            ////get list of depots
            //var depots = await _depotRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);
            ////convert list to dto
            //var depotsDto = ObjectMapper.Map<List<Depot>, List<DepotDto>>(depots);

            ////get total count of depots
            //var totatCount = input.Filter != null ? 
            //    await _depotRepository.CountAsync() 
            //    : await _depotRepository.CountAsync(depot=>depot.Name.Contains( input.Filter));

            ////combine total and depots list
            //var pagedResult = new PagedResultDto<DepotDto>(totatCount,depotsDto);

            ////return the list
            //return pagedResult;

            //Get the IQueryable<Depot> from the repository
            var queryable = await _depotRepository.GetQueryableAsync();

            //Prepare a query to join Depots and Locations
            var query = from depot in queryable
                        join location in _locationRepository on depot.Location equals location.Id
                        select new { depot, location };

            //set paging info
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of DepotDto objects
            var depotDtos = queryResult.Select(x =>
            {
                var depotDto = ObjectMapper.Map<Depot, DepotDto>(x.depot);
                depotDto.LocationId = x.location.Id;
                depotDto.LocationName = x.location.Name;
                return depotDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _depotRepository.GetCountAsync();

            return new PagedResultDto<DepotDto>(totalCount, depotDtos);

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

        public async Task<ListResultDto<LocationLookupDto>> GetLocationLookupAsync()
        {
            var locations = await _locationRepository.GetListAsync();
            var locationsLookupDto = ObjectMapper.Map<List<Location>, List<LocationLookupDto>>(locations);

            return new ListResultDto<LocationLookupDto>(locationsLookupDto);
        }

        private static string NormalizeSorting (string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"depot.{nameof(Depot.Name)}";
            }

            if (sorting.Contains("depotName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("depotName", "depot.Name", StringComparison.OrdinalIgnoreCase);
            }

            return $"depot.{sorting}";
        }
    }
}
