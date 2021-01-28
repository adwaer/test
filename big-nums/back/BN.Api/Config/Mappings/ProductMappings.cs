using AutoMapper;
using BN.Api.Models.Responses;
using BN.Domain.Features.Products.Models;

namespace BN.Api.Config.Mappings
{
    /// <inheritdoc />
    public class ProductMappings : Profile
    {
        /// <summary>
        /// Cfg
        /// </summary>
        public ProductMappings()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}