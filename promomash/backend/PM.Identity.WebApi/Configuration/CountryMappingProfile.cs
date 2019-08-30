using AutoMapper;
using PM.Identity.WebApi.Models;
using PM.Models;

namespace PM.Identity.WebApi.Configuration
{
    /// <summary>
    /// Country Mapping profile
    /// </summary>
    public class CountryMappingProfile : Profile
    {
        /// <summary>
        /// Config
        /// </summary>
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}