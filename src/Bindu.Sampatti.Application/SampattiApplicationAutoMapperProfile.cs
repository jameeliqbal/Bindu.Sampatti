﻿using AutoMapper;
using Bindu.Sampatti.Locations;

namespace Bindu.Sampatti
{
    public class SampattiApplicationAutoMapperProfile : Profile
    {
        public SampattiApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Location, LocationDto>();
        }
    }
}
