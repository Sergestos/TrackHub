using AutoMapper;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;
using TrackHub.Service.ExerciseServices.Models;
namespace TrackHub.Service.Infrastructure;

internal class ServiceMapper : Profile
{
    public ServiceMapper()
    {
        CreateMap<CreateRecordModel, Record>()
            .ForMember(x => x.RecordId, opt => opt.Ignore())
            .ForMember(x => x.RecordType, opt => opt.MapFrom(src => (RecordType)Enum.Parse(typeof(RecordType), src.RecordType)))
            .ForMember(x => x.PlayType, opt => opt.MapFrom(src => (PlayType)Enum.Parse(typeof(PlayType), src.PlayType)));

        CreateMap<CreateRecordModel, UpdateRecordModel>()            
            .ForMember(x => x.RecordType, opt => opt.MapFrom(src => (RecordType)Enum.Parse(typeof(RecordType), src.RecordType)))
            .ForMember(x => x.PlayType, opt => opt.MapFrom(src => (PlayType)Enum.Parse(typeof(PlayType), src.PlayType)));
    }
}
