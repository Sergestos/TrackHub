namespace TrackHub.Contract;

public interface IStatsQuery
{
    Task<int> Count();
}
