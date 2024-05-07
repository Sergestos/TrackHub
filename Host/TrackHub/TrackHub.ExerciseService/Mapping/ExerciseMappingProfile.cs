using AutoMapper;
using TrackHub.Contract.Inputs;
using TrackHub.Domain.Entities;

namespace TrackHub.ExerciseService.Mapping;

internal class ExerciseMappingProfile : Profile
{
    public ExerciseMappingProfile()
    {
        CreateMap<RecordCreateModel, Record>();
        CreateMap<ExerciseCreateModel, Exercise>();        
    }
}
