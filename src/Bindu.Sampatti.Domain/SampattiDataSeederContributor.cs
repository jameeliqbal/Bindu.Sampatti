using Bindu.Sampatti.Departments;
using Bindu.Sampatti.Depots;
using Bindu.Sampatti.Designations;
using Bindu.Sampatti.Employees;
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
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentManager _departmentManager;
        private readonly IDesignationRepository _designationRepository;
        private readonly DesignationManager _designationManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeManager _employeeManager;

        public SampattiDataSeederContributor(ILocationRepository locationRepo, LocationManager locationManager,
                                                IDepotRepository depotRepository, DepotManager depotManager,
                                                IPlantRepository plantRepository, PlantManager plantManager,
                                                IDepartmentRepository departmentRepository, DepartmentManager departmentManager,
                                                IDesignationRepository designationRepository, DesignationManager designationManager,
                                                IEmployeeRepository employeeRepository, EmployeeManager employeeManager
            )
        {
            _locationRepo = locationRepo;
            _locationManager = locationManager;

            _depotRepository = depotRepository;
            _depotManager = depotManager;

            _plantRepository = plantRepository;
            _plantManager = plantManager;

            _departmentManager = departmentManager;
            _departmentRepository = departmentRepository;

            _designationManager = designationManager;
            _designationRepository = designationRepository;

            _employeeManager = employeeManager;
            _employeeRepository = employeeRepository;

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

            if (await _departmentRepository.GetCountAsync() <=0)
            {
                //var departmentOne = await _departmentManager.CreateAsync("Department ONE", "some notes about Dept 1", true);
                //var departmentTwo = await _departmentManager.CreateAsync("Department TWO", "some notes about Dept 2", true);
                //var departmentThree = await _departmentManager.CreateAsync("Department THREE", "some notes about Dept 3", true);
                var designationUnknown = await _departmentManager.CreateAsync("Un-Assigned", "Value assigned when department is unknown", true);

                await _departmentRepository.InsertManyAsync(new List<Department> { designationUnknown });// departmentOne, departmentTwo, departmentThree });
            }

            if (await _designationRepository.GetCountAsync() <= 0)
            {
                //var designationOne = await _designationManager.CreateAsync("Designation ONE", "some notes about Designation 1", true);
                //var designationTwo = await _designationManager.CreateAsync("Designation TWO", "some notes about Designation 2", true);
                //var designationThree = await _designationManager.CreateAsync("Designation THREE", "some notes about Designation 3", true);
                var designationUnknown = await _designationManager.CreateAsync("Un-Assigned", "Value assigned when designation is unknown", true);

                await _designationRepository.InsertManyAsync(new List<Designation> { designationUnknown });//, designationOne, designationTwo, designationThree  });
            }

            if (await _employeeRepository.GetCountAsync() <= 0)
            {
                var designation = _designationRepository.SingleOrDefault(d => d.Name == "Un-Assigned").Id;
                var department = _departmentRepository.SingleOrDefault(d => d.Name == "Un-Assigned").Id;

                var employeeOne = await _employeeManager.CreateAsync("Employee ONE", "1001", designation, department, "some notes about employee ONE", true);
                var employeeTwo = await _employeeManager.CreateAsync("Employee TWO", "1002", designation, department, "some notes about employee TWO", true);
                var employeeThree = await _employeeManager.CreateAsync("Employee THREE", "1003", designation, department, "some notes about employee THREE", true);
            }
        }
    }
}
