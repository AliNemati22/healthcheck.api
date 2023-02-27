using MediatR;

namespace healthcheck.api.Application.Features.WebServiceStatus;

public interface IWebServiceStatusQuery : IRequest<bool>
{
}