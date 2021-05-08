﻿using AutoMapper;
using Bindu.Sampatti.Depots;
 

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
            
        }
    }
}
