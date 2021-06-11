using Bindu.Sampatti.Departments;
using Bindu.Sampatti.Designations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Bindu.Sampatti.Employees
{
    public class EmployeeAppService : SampattiAppService, IEmployeeAppService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeManager _employeeManager;
        private readonly IDesignationRepository _designationRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeAppService(IEmployeeRepository employeeRepository, EmployeeManager employeeManager,
            IDesignationRepository designationRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _designationRepository = designationRepository;
            _departmentRepository = departmentRepository;
            _employeeManager = employeeManager;
        }

        public async Task<EmployeeDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Employee> from the repository
            var queryable = await _employeeRepository.GetQueryableAsync();

            //Prepare a query to join Employee and designation and department
            var query = (from employee in queryable
                         join designation in _designationRepository on employee.Designation equals designation.Id
                            into empdesigs
                         from desig in empdesigs.DefaultIfEmpty()
                         join department in _departmentRepository on employee.Department equals department.Id
                            into empdepts
                         from dept in empdepts.DefaultIfEmpty()
                         where employee.Id == id
                         select new
                         {
                             employee,
                             designationId = desig.Id,
                             designationName = desig.Name,
                             departmentId = dept.Id,
                             departmentName = dept.Name
                         });

            //Execute the query and get the Employee with designation and department
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Employee), id);
            }

            var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(queryResult.employee);
            employeeDto.DesignationName = queryResult.designationName;
            employeeDto.DepartmentName = queryResult.departmentName;

            return employeeDto;
        }

        public async Task<EmployeeDto> GetByEmployeeCodeAsync(string employeeCode)
        {
            //Get the IQueryable<Employee> from the repository
            var queryable = await _employeeRepository.GetQueryableAsync();

            //Prepare a query to join Employee and designation and department
            var query = (from employee in queryable
                         join designation in _designationRepository on employee.Designation equals designation.Id
                            into empdesigs
                         from desig in empdesigs.DefaultIfEmpty()
                         join department in _departmentRepository on employee.Department equals department.Id
                            into empdepts
                         from dept in empdepts.DefaultIfEmpty()
                         where employee.Code == employeeCode
                         select new
                         {
                             employee,
                             designationId = desig.Id,
                             designationName = desig.Name,
                             departmentId = dept.Id,
                             departmentName = dept.Name
                         });

            //Execute the query and get the Employee with designation and department
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Employee), employeeCode);
            }

            var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(queryResult.employee);
            employeeDto.DesignationName = queryResult.designationName;
            employeeDto.DepartmentName = queryResult.departmentName;

            return employeeDto;
        }

        public async Task<PagedResultDto<EmployeeDto>> GetListAsync(GetEmployeeListDto input)
        {
            //Get the IQueryable<Employee> from the repository
            var queryable = await _employeeRepository.GetQueryableAsync();

            //Prepare a query to join Employees and designaiton and department
            var query = from employee in queryable
                        join designation in _designationRepository on employee.Designation equals designation.Id
                           into empdesigs
                        from desig in empdesigs.DefaultIfEmpty()
                        join department in _departmentRepository on employee.Department equals department.Id
                           into empdepts
                        from dept in empdepts.DefaultIfEmpty()
                        select new
                        {
                            employee,
                            designationId = desig.Id,
                            designationName = desig.Name,
                            departmentId = dept.Id,
                            departmentName = dept.Name
                        };

            //set paging info
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of EmployeeDto objects
            var employeeDtos = queryResult.Select(x =>
            {
                var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(x.employee);
                employeeDto.Designation = x.designationId;
                employeeDto.DesignationName = x.designationName;
                employeeDto.Department = x.departmentId;
                employeeDto.DepartmentName = x.departmentName;

                return employeeDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _employeeRepository.GetCountAsync();

            return new PagedResultDto<EmployeeDto>(totalCount, employeeDtos);
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto input)
        {
            var employee = await _employeeManager.CreateAsync(input.Name, input.Code, input.Designation, input.Department, input.Notes,
                                                        input.Status);

            await _employeeRepository.InsertAsync(employee);

            var employeeDto = ObjectMapper.Map<Employee, EmployeeDto>(employee);

            return employeeDto;
        }

        public async Task UpdateAsync(Guid id, UpdateEmployeeDto input)
        {
            var existingEmployee = await _employeeRepository.GetAsync(id);

            if (existingEmployee.Name != input.Name)
            {
                await _employeeManager.ChangeNameAsync(existingEmployee, input.Name);
            }

            if (existingEmployee.Code != input.Code)
            {
                await _employeeManager.ChangeCodeAsync(existingEmployee, input.Code);
            }

            existingEmployee.Designation = input.Designation;
            existingEmployee.Department = input.Department;
            existingEmployee.Notes = input.Notes;
            existingEmployee.Status = input.Status;

            await _employeeRepository.UpdateAsync(existingEmployee);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync()
        {
            var departments =await _departmentRepository.GetListAsync();
            var departmentsLookupDto = ObjectMapper.Map<List<Department>, List<DepartmentLookupDto>>(departments);

            return new ListResultDto<DepartmentLookupDto>(departmentsLookupDto);
        }

        public async Task<ListResultDto<DesignationLookupDto>> GetDesignationLookupAsync()
        {
            var designations = await _designationRepository.GetListAsync();
            var designationsLookupDto = ObjectMapper.Map<List<Designation>, List<DesignationLookupDto>>(designations);

            return new ListResultDto<DesignationLookupDto>(designationsLookupDto);
        }


        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"employee.{nameof(Employee.Name)}";
            }

            if (sorting.Contains("employeeName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("employeeName", "Employee.Name", StringComparison.OrdinalIgnoreCase);
            }

            if (sorting.Contains("designationName", StringComparison.OrdinalIgnoreCase))
            {
                //return sorting.Replace("designationName", "designationName", StringComparison.OrdinalIgnoreCase);
                return  "designationName";

            }

            if (sorting.Contains("departmentName", StringComparison.OrdinalIgnoreCase))
            {
                //return sorting.Replace("departmentName", "departmentName", StringComparison.OrdinalIgnoreCase);
                return   "departmentName";
            }
            return $"employee.{sorting}";
        }
    }
}
