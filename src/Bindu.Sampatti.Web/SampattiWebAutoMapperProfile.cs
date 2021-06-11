using AutoMapper;
using Bindu.Sampatti.Departments;
using Bindu.Sampatti.Depots;
using Bindu.Sampatti.Designations;
using Bindu.Sampatti.Employees;
using Bindu.Sampatti.Locations;
using Bindu.Sampatti.Plants;

namespace Bindu.Sampatti.Web
{
    public class SampattiWebAutoMapperProfile : Profile
    {
        public SampattiWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Location.CreateModalModel.CreateLocationViewModal, CreateLocationDto>()
                .ForMember(dest => dest.IsEnabled,
                            opt => opt.MapFrom(src => src.Status));

            CreateMap<LocationDto, Bindu.Sampatti.Web.Pages.Organisation.Location.EditModalModel.EditLocationViewModal>()
                .ForMember(dest => dest.Status,
                            opt=>opt.MapFrom(src=>src.IsEnabled));

            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Location.EditModalModel.EditLocationViewModal, UpdateLocationDto>()
                .ForMember(dest => dest.IsEnabled,
                            opt => opt.MapFrom(src => src.Status));

            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Depot.CreateModalModel.CreateDepotViewModal, CreateDepotDto>();
            CreateMap<DepotDto, Bindu.Sampatti.Web.Pages.Organisation.Depot.EditModalModel.EditDepotViewModal>();
            //.ForMember(dest=>dest.Location,
            //            opt=>opt.MapFrom(src=>src.LocationName));
            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Depot.EditModalModel.EditDepotViewModal, UpdateDepotDto>();

            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Plant.CreateModalModel.CreatePlantViewModal, CreatePlantDto>();
            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Plant.EditModalModel.EditPlantViewModal, UpdatePlantDto>();
            CreateMap<PlantDto, Bindu.Sampatti.Web.Pages.Organisation.Plant.EditModalModel.EditPlantViewModal>();

            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Department.CreateModalModel.CreateDepartmentViewModal, CreateDepartmentDto>();
            CreateMap<DepartmentDto,Bindu.Sampatti.Web.Pages.Organisation.Department.EditModalModel.EditDepartmentViewModal>();
            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Department.EditModalModel.EditDepartmentViewModal,UpdateDepartmentDto>();

            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Designation.CreateModalModel.CreateDesignationViewModal, CreateDesignationDto>();
            CreateMap<DesignationDto, Bindu.Sampatti.Web.Pages.Organisation.Designation.EditModalModel.EditDesignationViewModal>();
            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Designation.EditModalModel.EditDesignationViewModal, UpdateDesignationDto>();

            CreateMap<Bindu.Sampatti.Web.Pages.Organisation.Employee.CreateModalModel.CreateEmployeeViewModal, CreateEmployeeDto>();
        }
    }
}
