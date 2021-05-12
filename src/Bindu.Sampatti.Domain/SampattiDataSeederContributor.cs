using Bindu.Sampatti.Depots;
using Bindu.Sampatti.Locations;
using Bindu.Sampatti.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Bindu.Sampatti
{
    public class SampattiDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ILocationRepository _locationRepo;
        private readonly LocationManager _locationManager;
        private readonly IDepotRepository _depotRepository;
        private readonly DepotManager _depotManager;
        private readonly IPlantRepository _plantRepository;
        private readonly PlantManager _plantManager;

        public SampattiDataSeederContributor(ILocationRepository locationRepo, LocationManager locationManager,
                                                IDepotRepository depotRepository, DepotManager depotManager,
                                                IPlantRepository plantRepository, PlantManager plantManager)
        {
            _locationRepo = locationRepo;
            _locationManager = locationManager;

            _depotRepository = depotRepository;
            _depotManager = depotManager;

            _plantRepository = plantRepository;
            _plantManager = plantManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _locationRepo.GetCountAsync() <= 0)
            {
                var locationOne = await _locationManager.CreateAsync("Location ONE", true, "some notes about one");
                var locationTwo = await _locationManager.CreateAsync("Location TWO", true, "some notes about two");
                var locationThree = await _locationManager.CreateAsync("Location THREE", true, "some notes about three");

                await _locationRepo.InsertManyAsync(new List<Location>() { locationOne, locationTwo, locationThree });

            }

            if (await _depotRepository.GetCountAsync() <= 0)
            {
                var locations = await _locationRepo.GetListAsync();
 
                var depotOne = await _depotManager.CreateAsync("Depot ONE", locations[0].Id, "some notes about depot one", true);
                var depotTwo = await _depotManager.CreateAsync("Depot TWO", locations[1].Id, "some notes about depot two", true);
                var depotThree = await _depotManager.CreateAsync("Depot THREE", locations[2].Id, "some notes about depot three", true);
                var depotFour = await _depotManager.CreateAsync("Depot FOUR", locations[1].Id, "some notes about depot four", true);

                await _depotRepository.InsertManyAsync(new List<Depot> { depotOne, depotTwo, depotThree, depotFour});

            }

            if (await _plantRepository.GetCountAsync() <= 0)
            {
                var locations = await _locationRepo.GetListAsync();

                var plantOne = await _plantManager.CreateAsync("plant ONE", locations[0].Id, "some notes about plant one", true);
                var plantTwo = await _plantManager.CreateAsync("plant TWO", locations[1].Id, "some notes about plant two", true);
                var plantThree = await _plantManager.CreateAsync("plant THREE", locations[2].Id, "some notes about plant three", true);
                var plantFour = await _plantManager.CreateAsync("plant FOUR", locations[1].Id, "some notes about plant four", true);

                await _plantRepository.InsertManyAsync(new List<Plant> { plantOne, plantTwo, plantThree, plantFour });

            }
        }
    }
}
