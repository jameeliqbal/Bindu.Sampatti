using Bindu.Sampatti.Locations;
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
        public SampattiDataSeederContributor(ILocationRepository locationRepo, LocationManager locationManager)
        {
            _locationRepo = locationRepo;
            _locationManager = locationManager;
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
        }
    }
}
