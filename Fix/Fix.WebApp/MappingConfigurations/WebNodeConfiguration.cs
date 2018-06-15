using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using Fix.Domain;
using Fix.WebApp.Models;

namespace Fix.WebApp.MappingConfigurations
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class WebNodeConfiguration : Profile
	{
		public WebNodeConfiguration()
		{
			CreateMap<WebNode, WebNodeEditViewModel>()
				.ForMember(dest => dest.Id, src => src.MapFrom(opt => opt.Id))
				.ForMember(dest => dest.IsAvailable, src => src.MapFrom(opt => opt.IsAvailable))
				.ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.Name))
				.ForMember(dest => dest.Url, src => src.MapFrom(opt => opt.Url))
				.ForMember(dest => dest.Interval, src => src.MapFrom(opt => opt.Interval));

			CreateMap<WebNodeEditViewModel, WebNode>()
				.ForMember(dest => dest.Id, src => src.MapFrom(opt => opt.Id))
				.ForMember(dest => dest.IsAvailable, src => src.UseValue(false))
				.ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.Name))
				.ForMember(dest => dest.Url, src => src.MapFrom(opt => opt.Url))
				.ForMember(dest => dest.Interval, src => src.MapFrom(opt => opt.Interval))
				.ForMember(dest => dest.Histories, src => src.Ignore());

			CreateMap<WebNode, WebNodeViewModel>()
				.ForMember(dest => dest.IsAvailable, src => src.MapFrom(opt => opt.IsAvailable))
				.ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.Name))
				.ForMember(dest => dest.Url, src => src.MapFrom(opt => opt.Url))
				.ForMember(dest => dest.HaveHistory, src => src.MapFrom(opt => opt.Histories.Any()));
		}
	}
}