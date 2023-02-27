using MediatR;

namespace healthcheck.api.Application.Features.WebServiceStatus.Queries;

public class GeneralWebServiceStatusQuery : IWebServiceStatusQuery
{
    public string Url { get; private set; }

    public GeneralWebServiceStatusQuery(IConfiguration configuration) => Url = configuration.GetValue<string>("Url");
    public GeneralWebServiceStatusQuery(string url) => Url = url;
}

public class GeneralWebServiceStatusQueryHandler : IRequestHandler<GeneralWebServiceStatusQuery, bool>
{
    public static async Task<bool> IsConnected(string url)
    {
        HttpClient client = new();
        try
        {
            client.Timeout = TimeSpan.FromSeconds(2);
            return (await client.GetAsync(url)).IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> Handle(GeneralWebServiceStatusQuery request, CancellationToken cancellationToken)
    {
        return await IsConnected(request.Url);
    }
}