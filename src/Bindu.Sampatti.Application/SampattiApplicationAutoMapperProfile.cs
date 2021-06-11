using AutoMapper;
using Bindu.Sampatti.Departments;
using Bindu.Sampatti.Depots;
using Bindu.Sampatti.Designations;
using Bindu.Sampatti.Employees;

namespace Bindu.Sampatti
{
    public class SampattiApplicationAutoMapperProfile : Profile
    {
        public SampattiApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Bindu.Sampatti.Locations.Location, Bindu.Sampatti.Locations.LocationDto>();
            CreateMap<Depot, DepotDto>();
            CreateMap<Bindu.Sampatti.Locations.Location, Bindu.Sampatti.Depots.LocationLookupDto>();
            CreateMap<Bindu.Sampatti.Plants.Plant, Bindu.Sampatti.Plants.PlantDto>();
            CreateMap<Bindu.Sampatti.Locations.Location, Bindu.Sampatti.Plants.LocationLookupDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Designation,DesignationDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Designation, DesignationLookupDto>();
            CreateMap<Department, DepartmentLookupDto>();
        }
    }
}
