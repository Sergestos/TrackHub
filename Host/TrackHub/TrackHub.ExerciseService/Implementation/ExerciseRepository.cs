using AutoMapper;
using Microsoft.Azure.Cosmos;
using TrackHub.Contract;
using TrackHub.Contract.Inputs;
using TrackHub.Contract.ViewModels;
using TrackHub.CosmosDb;
using TrackHub.Domain.Entities;

namespace TrackHub.ExerciseService.Implementation;

internal class ExerciseRepository : IExerciseRepository
{
    private readonly ICosmosDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExerciseRepository(ICosmosDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task CreateAsync(ExerciseCreateModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Exercise>(model);
        entity.Id = Guid.NewGuid().ToString();

        await _dbContext.Container.CreateItemAsync(entity, new PartitionKey(model.UserId), null, cancellationToken);
    }

    public Task<IEnumerable<ExericeViewModel>> GetAllByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ExerciseDetailsViewModel> GetByIdAsync(string exerciseId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ExerciseUpdateModel model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
