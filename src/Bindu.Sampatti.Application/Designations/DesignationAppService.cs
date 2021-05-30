using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Bindu.Sampatti.Designations
{
    public class DesignationAppService : SampattiAppService, IDesignationAppService
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly DesignationManager _designationManager;

        public DesignationAppService(IDesignationRepository designationRepository, DesignationManager designationManager)
        {
            _designationRepository = designationRepository;
            _designationManager = designationManager;
        }
        public async Task<DesignationDto> GetAsync(Guid id)
        {


            var Designation = await _designationRepository.GetAsync(id);
            if (Designation == null)
            {
                throw new EntityNotFoundException(typeof(Designation), id);
            }

            var designationDto = ObjectMapper.Map<Designation, DesignationDto>(Designation);

            return designationDto;
        }

        public async Task<PagedResultDto<DesignationDto>> GetListAsync(GetDesignationListDto input)
        {
            //Get the IQueryable<Designation> from the repository
            var queryable = await _designationRepository.GetQueryableAsync();

            //Prepare a query to join Designations and Locations
            var query = from Designation in queryable
                        select new { Designation };

            //set paging info
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of DesignationDto objects
            var designationDtos = queryResult.Select(x =>
            {
                var designationDto = ObjectMapper.Map<Designation, DesignationDto>(x.Designation);
                return designationDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _designationRepository.GetCountAsync();

            return new PagedResultDto<DesignationDto>(totalCount, designationDtos);

        }

        public async Task<DesignationDto> CreateAsync(CreateDesignationDto input)
        {
            var Designation = await _designationManager.CreateAsync(input.Name, input.Notes, input.Status);

            await _designationRepository.InsertAsync(Designation);

            var designationDto = ObjectMapper.Map<Designation, DesignationDto>(Designation);

            return designationDto;
        }

        public async Task UpdateAsync(Guid id, UpdateDesignationDto input)
        {
            var existingDesignation = await _designationRepository.GetAsync(id);

            if (existingDesignation.Name != input.Name)
            {
                await _designationManager.ChangeNameAsync(existingDesignation, input.Name);
            }

            existingDesignation.Notes = input.Notes;
            existingDesignation.Status = input.Status;

            await _designationRepository.UpdateAsync(existingDesignation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _designationRepository.DeleteAsync(id);
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"designation.{nameof(Designation.Name)}";
            }

            if (sorting.Contains("designationName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("designationName", "designation.Name", StringComparison.OrdinalIgnoreCase);
            }

            return $"designation.{sorting}";
        }
    }
}
