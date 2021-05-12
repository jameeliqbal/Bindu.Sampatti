using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Plants
{
    public class PlantManager : DomainService
    {
        private readonly IPlantRepository _plantRepository;
        public PlantManager(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<Plant> CreateAsync([NotNull] string name, [NotNull] Guid location, [CanBeNull] string notes, bool status)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingDepot = await _plantRepository.FindByNameAsync(name);
            if (existingDepot != null)
            {
                throw new PlantAlreadyExistsException(name);
            }

            return new Plant(GuidGenerator.Create(), name, location, notes, status);
        }

        public async Task ChangeNameAsync([NotNull] Plant plant, [NotNull] string newName)
        {
            Check.NotNull(plant, nameof(plant));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingDepot = await _plantRepository.FindByNameAsync(newName);
            if (existingDepot != null && existingDepot.Id != plant.Id)
            {
                throw new PlantAlreadyExistsException(newName);
            }

            plant.ChangeName(newName);

        }
    }
}
