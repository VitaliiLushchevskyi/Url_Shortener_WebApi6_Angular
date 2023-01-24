using UrlShortener.Data.Entities;
using UrlShortener.ViewModels;
using AutoMapper;

namespace UrlShortener.Data
{
    public class UrlShortenerMapProfile : Profile
    {
        public UrlShortenerMapProfile()
        {
            CreateMap<AppUser, AppUserViewModel>()
                .ForMember(u => u.Id, p => p.MapFrom(u => u.Id))
                .ReverseMap();
            CreateMap<AppUser, SendUserViewModel>()
                .ForMember(u => u.Id, p => p.MapFrom(u => u.Id))
                .ReverseMap();
            CreateMap<UrlDetail, UrlDetailViewModel>()
                .ForMember(u => u.UrlId, p => p.MapFrom(u => u.Id))
                .ForMember(u => u.User, p=> p.MapFrom(u=>u.User))
                .ReverseMap();
        }
    }
}
