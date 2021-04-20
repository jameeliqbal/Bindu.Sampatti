using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Bindu.Sampatti.Locations
{
    public class LocationManager : DomainService
    {
        private ILocationRepository _locationRepository;

        public LocationManager(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> CreateAsync([NotNull]string name, [CanBeNull]string notes =null, bool isEnabled)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingLocation = await _locationRepository.FindByNameAsync(name);
            if (existingLocation != null)
            {
                throw new LocationAlreadyExistsException(name);
            }

            return new Location(
                GuidGenerator.Create(),
                name,
                notes,
                isEnabled
            );
        }
    
        public async Task ChangeNameAsync(
                [NotNull]Location location, 
                [NotNull]string newName
            )
        {
            Check.NotNull(location, nameof(location));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingLocation = await _locationRepository.FindByNameAsync(newName);
            if (existingLocation !=null && existingLocation.Id != location.Id)
            {
                throw new LocationAlreadyExistsException(newName);
            }

            location.ChangeName(newName);
        }
    }
}
