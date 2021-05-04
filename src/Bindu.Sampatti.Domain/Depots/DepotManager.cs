using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Depots
{
    public class DepotManager : DomainService
    {
        private readonly IDepotRepository _depotRepository;
        public DepotManager(IDepotRepository depotRepository)
        {
            _depotRepository = depotRepository;
        }

        public async Task<Depot> CreateAsync([NotNull]string name, [NotNull]Guid location, [CanBeNull]string notes, bool status)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingDepot = await _depotRepository.FindByNameAsync(name);
            if (existingDepot != null)
            {
                throw new DepotAlreadyExistsException(name);
            }

            return new Depot(GuidGenerator.Create(), name, location, notes, status);
        }

        public async Task ChangeNameAsync([NotNull]Depot depot, [NotNull]string newName)
        {
            Check.NotNull(depot, nameof(depot));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingDepot =await _depotRepository.FindByNameAsync(newName);
            if(existingDepot !=null && existingDepot.Id != depot.Id)
            {
                throw new DepotAlreadyExistsException(newName);
            }

            depot.ChangeName(newName);

        }
    }
}
