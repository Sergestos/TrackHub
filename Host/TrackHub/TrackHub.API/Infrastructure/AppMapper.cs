using AutoMapper;
using Google.Apis.Auth;
using TrackHub.Service.UserServices.Models;

namespace TrackHub.Web.Mappers;

public class AppMapper : Profile
{
    public AppMapper()
    {
        CreateMap<GoogleJsonWebSignature.Payload, SocialUser>()
            .ForMember(x => x.FullName, opt => opt.MapFrom(src => $"{src.GivenName} {src.FamilyName}"))
            .ForMember(x => x.PhotoUrl, opt => opt.MapFrom(src => src.Picture));
    }
}
