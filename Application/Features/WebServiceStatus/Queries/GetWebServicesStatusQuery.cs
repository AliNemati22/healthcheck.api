using healthcheck.api.Domain.Dto;
using healthcheck.api.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace healthcheck.api.Application.Features.WebServiceStatus.Queries;

public class GetWebServicesStatusQuery : IRequest<IEnumerable<WebServiceStatusDto>>
{
    public GetWebServicesStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }

    public bool ReturnAll { get; }
}

public class GetWebServicesStatusHandler : IRequestHandler<GetWebServicesStatusQuery, IEnumerable<WebServiceStatusDto>>
{
    private readonly WebServicesOptions _options;
    private readonly IMediator _mediator;

    public GetWebServicesStatusHandler(IOptions<WebServicesOptions> options, IMediator mediator)
    {
        _options = options.Value;
        _mediator = mediator;
    }

    public async Task<IEnumerable<WebServiceStatusDto>> Handle(GetWebServicesStatusQuery request,
        CancellationToken cancellationToken)
    {
        List<WebServiceStatusDto> output = new();
        foreach (var webService in _options.WebServices)
        {
            string webServiceRequestType = webService.RequestType ?? _options.DefaultRequestType;
            try
            {
                var req = WebServiceStatusQueryResolver.CreateQuery(webServiceRequestType, webService.Options);
                bool isConnected = await _mediator.Send(req, cancellationToken);
                if (request.ReturnAll)
                {
                    output.Add(new WebServiceStatusDto(webService.Name, string.Empty, isConnected));
                }
                else if (!isConnected)
                {
                    output.Add(new WebServiceStatusDto(webService.Name, string.Empty, isConnected));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return output;
    }
}