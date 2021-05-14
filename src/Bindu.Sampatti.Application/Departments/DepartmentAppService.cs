using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Bindu.Sampatti.Departments
{
   public class DepartmentAppService : SampattiAppService, IDepartmentAppService
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly DepartmentManager _DepartmentManager;

        public DepartmentAppService(IDepartmentRepository DepartmentRepository, DepartmentManager DepartmentManager)
        {
            _DepartmentRepository = DepartmentRepository;
            _DepartmentManager = DepartmentManager;
        }
        public async Task<DepartmentDto> GetAsync(Guid id)
        {

             
            var department = await _DepartmentRepository.GetAsync(id);
            if (department == null)
            {
                throw new EntityNotFoundException(typeof(Department), id);
            }
            
            var DepartmentDto = ObjectMapper.Map<Department, DepartmentDto>(department);

            return DepartmentDto;
        }

        public async Task<PagedResultDto<DepartmentDto>> GetListAsync(GetDepartmentListDto input)
        {
            //Get the IQueryable<Department> from the repository
            var queryable = await _DepartmentRepository.GetQueryableAsync();

            //Prepare a query to join Departments and Locations
            var query = from Department in queryable
                        select new { Department };

            //set paging info
            query = query
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of DepartmentDto objects
            var DepartmentDtos = queryResult.Select(x =>
            {
                var DepartmentDto = ObjectMapper.Map<Department, DepartmentDto>(x.Department);
                return DepartmentDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _DepartmentRepository.GetCountAsync();

            return new PagedResultDto<DepartmentDto>(totalCount, DepartmentDtos);

        }

        public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto input)
        {
            var Department = await _DepartmentManager.CreateAsync(input.Name,   input.Notes, input.Status);

            await _DepartmentRepository.InsertAsync(Department);

            var DepartmentDto = ObjectMapper.Map<Department, DepartmentDto>(Department);

            return DepartmentDto;
        }

        public async Task UpdateAsync(Guid id, UpdateDepartmentDto input)
        {
            var existingDepartment = await _DepartmentRepository.GetAsync(id);

            if (existingDepartment.Name != input.Name)
            {
                await _DepartmentManager.ChangeNameAsync(existingDepartment, input.Name);
            }

             existingDepartment.Notes = input.Notes;
            existingDepartment.Status = input.Status;

            await _DepartmentRepository.UpdateAsync(existingDepartment);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _DepartmentRepository.DeleteAsync(id);
        }

        
    }
}
