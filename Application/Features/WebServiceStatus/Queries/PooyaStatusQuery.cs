using MediatR;

namespace healthcheck.api.Application.Features.WebServiceStatus.Queries;

public class PooyaStatusQuery : IWebServiceStatusQuery
{
    public string Url { get; private set; }

    public PooyaStatusQuery(IConfiguration configuration) => Url = configuration.Get<PooyaStatusQueryOptions>().RelativeUrl;
    public PooyaStatusQuery(string url) => Url = url;
        

    public class PooyaStatusQueryOptions
    {
        public string RelativeUrl { get; set; }
    }
}

public class PooyaStatusQueryHandler : IRequestHandler<PooyaStatusQuery, bool>
{
    private readonly HttpClient _client;

    public PooyaStatusQueryHandler(HttpClient client)
    {
        _client = client;
    }

    private async Task<bool> IsConnected(string url)
    {
        try
        {
            return (await _client.GetAsync(url)).IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> Handle(PooyaStatusQuery request, CancellationToken cancellationToken) =>
        await IsConnected(request.Url);
}