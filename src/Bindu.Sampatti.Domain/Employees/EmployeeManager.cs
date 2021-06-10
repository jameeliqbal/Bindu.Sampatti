using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Employees
{
    public class EmployeeManager : DomainService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateAsync([NotNull] string name, [NotNull] string code, [CanBeNull] Guid designation, 
            [CanBeNull] Guid department,[CanBeNull] string notes, bool status)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingEmployeeByName = await _employeeRepository.FindByNameAsync(name);
            if (existingEmployeeByName != null)
            {
                throw new EmployeeAlreadyExistsByNameExcepiton(name);
            }

            Check.NotNullOrWhiteSpace(code, nameof(code));

            var existingEmployeeByCode = await _employeeRepository.FindByEmployeeCodeAsync(code);
            if (existingEmployeeByCode != null)
            {
                throw new EmployeeAlreadyExistsByCodeException(code);
            }

            return new Employee(GuidGenerator.Create(), name, code,designation,department, notes, status);
        }

        public async Task ChangeNameAsync([NotNull] Employee employee, [NotNull] string newName)
        {
            Check.NotNull(employee, nameof(employee));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingEmployee = await _employeeRepository.FindByNameAsync(newName);
            if (existingEmployee != null && existingEmployee.Id != employee.Id)
            {
                throw new EmployeeAlreadyExistsByNameExcepiton(newName);
            }

            employee.ChangeName(newName);

        }

        public async Task ChangeCodeAsync([NotNull] Employee employee, [NotNull] string newCode)
        {
            Check.NotNull(employee, nameof(employee));
            Check.NotNullOrWhiteSpace(newCode, nameof(newCode));

            var existingEmployee = await _employeeRepository.FindByEmployeeCodeAsync(newCode);
            if (existingEmployee != null && existingEmployee.Code != employee.Code)
            {
                throw new EmployeeAlreadyExistsByCodeException(newCode);
            }

            employee.ChangeCode(newCode);

        }
    }
}
