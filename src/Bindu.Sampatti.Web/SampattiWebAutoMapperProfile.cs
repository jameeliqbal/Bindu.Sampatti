using AutoMapper;
using Bindu.Sampatti.Depots;
using Bindu.Sampatti.Locations;
 

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
        }
    }
}
