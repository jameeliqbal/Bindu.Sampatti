using AutoMapper;
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
        }
    }
}
