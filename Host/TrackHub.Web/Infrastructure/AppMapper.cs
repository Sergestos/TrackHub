using AutoMapper;
using Google.Apis.Auth;
using TrackHub.Domain.Entities;
using TrackHub.Service.UserServices.Models;
using TrackHub.Web.Models;

namespace TrackHub.Web.Mappers;

public class AppMapper : Profile
{
    public AppMapper()
    {
        CreateMap<GoogleJsonWebSignature.Payload, SocialUserModel>()
            .ForMember(x => x.FullName, opt => opt.MapFrom(src => $"{src.GivenName} {src.FamilyName}"))
            .ForMember(x => x.PhotoUrl, opt => opt.MapFrom(src => src.Picture));        

        CreateMap<Record, RecordDto>()
            .ForMember(x => x.RecordType, opt => opt.MapFrom(src => src.RecordType.ToString()))
            .ForMember(x => x.PlayType, opt => opt.MapFrom(src => src.PlayType.ToString()));
    }
}
