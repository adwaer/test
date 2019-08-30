using AutoMapper;
using PM.Identity.WebApi.Models;
using PM.Models;

namespace PM.Identity.WebApi.Configuration
{
    /// <summary>
    /// Province mapping profile
    /// </summary>
    public class ProvinceMappingProfile : Profile
    {
        /// <summary>
        /// Config
        /// </summary>
        public ProvinceMappingProfile()
        {
            CreateMap<Province, ProvinceViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}