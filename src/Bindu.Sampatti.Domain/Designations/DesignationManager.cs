using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Designations
{
    public class DesignationManager : DomainService
    {
        private readonly IDesignationRepository _designationRepository;
        public DesignationManager(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<Designation> CreateAsync([NotNull] string name, [CanBeNull] string notes, bool status)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingDepot = await _designationRepository.FindByNameAsync(name);
            if (existingDepot != null)
            {
                throw new DesignationAlreadyExistsException(name);
            }

            return new Designation(GuidGenerator.Create(), name, notes, status);
        }

        public async Task ChangeNameAsync([NotNull] Designation designation, [NotNull] string newName)
        {
            Check.NotNull(designation, nameof(designation));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingDepartment = await _designationRepository.FindByNameAsync(newName);
            if (existingDepartment != null && existingDepartment.Id != designation.Id)
            {
                throw new DesignationAlreadyExistsException(newName);
            }

            designation.ChangeName(newName);

        }
    }
}
