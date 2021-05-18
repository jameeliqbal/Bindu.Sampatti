﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Departments
{
    public class DepartmentManager : DomainService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> CreateAsync([NotNull] string name,   [CanBeNull] string notes, bool status)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingDepot = await _departmentRepository.FindByNameAsync(name);
            if (existingDepot != null)
            {
                throw new DepartmentAlreadyExistsException(name);
            }

            return new Department(GuidGenerator.Create(), name, notes, status);
        }

        public async Task ChangeNameAsync([NotNull] Department department, [NotNull] string newName)
        {
            Check.NotNull(department, nameof(department));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingDepartment = await _departmentRepository.FindByNameAsync(newName);
            if (existingDepartment != null && existingDepartment.Id != department.Id)
            {
                throw new DepartmentAlreadyExistsException(newName);
            }

            department.ChangeName(newName);

        }
    }
}
