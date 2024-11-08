using AutoMapper;
using TrackHub.Domain.Entities;
using TrackHub.Domain.Enums;
using TrackHub.Service.Services.ExerciseServices.Models;
namespace TrackHub.Service.Infrastructure;

internal class ServiceMapper : Profile
{
    public ServiceMapper()
    {
        CreateMap<Record, RecordListItem>()
            .ForMember(x => x.RecordType, opt => opt.MapFrom(src => src.RecordType.ToString()))
            .ForMember(x => x.Duration, opt => opt.MapFrom(src => src.PlayDuration));

        CreateMap<Exercise, ExerciseListItem>()
            .ForMember(x => x.PlayDate, opt => opt.MapFrom(src => src.GetPlayDateAsDateTime()))
            .ForMember(x => x.Records, opt => opt.MapFrom(src => src.Records));

        CreateMap<CreateRecordModel, Record>()
            .ForMember(x => x.RecordId, opt => opt.Ignore())
            .ForMember(x => x.RecordType, opt => opt.MapFrom(src => (RecordType)Enum.Parse(typeof(RecordType), src.RecordType)))
            .ForMember(x => x.PlayType, opt => opt.MapFrom(src => (PlayType)Enum.Parse(typeof(PlayType), src.PlayType)));

        CreateMap<UpdateRecordModel, Record>()
            .ForMember(x => x.RecordType, opt => opt.MapFrom(src => (RecordType)Enum.Parse(typeof(RecordType), src.RecordType)))
            .ForMember(x => x.PlayType, opt => opt.MapFrom(src => (PlayType)Enum.Parse(typeof(PlayType), src.PlayType)));
    }
}
