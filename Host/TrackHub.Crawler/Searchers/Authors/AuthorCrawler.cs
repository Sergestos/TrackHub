
using TrackHub.Domain.Repositories;

namespace TrackHub.Crawler.Searchers.Authors;

internal class AuthorCrawler : IAuthorCrawler
{
    private readonly IRecordRepository _recordRepository;

    public AuthorCrawler(IRecordRepository recordRepository)
    {
        _recordRepository = recordRepository;
    }

    public async Task<IEnumerable<string>> SearchAsync(string authorName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(authorName) || authorName.Length < 3)
            return Enumerable.Empty<string>();

        var result = await _recordRepository.SearchAuthorsByNameAsync(CapitalizeFirstLetter(authorName), cancellationToken);

        return result;
    }

    private string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
