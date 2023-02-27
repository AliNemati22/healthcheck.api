using domain.Entities;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using static domain.Constants.LsConstants;

namespace healthcheck.api.Application.Features.MarketData.Queries;

public class GetStateChangeStatusQuery : IRequest<IEnumerable<StateChangeDto>>
{
    public GetStateChangeStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }
}

public class GetStateChangeStatusQueryHandler : IRequestHandler<GetStateChangeStatusQuery, IEnumerable<StateChangeDto>>
{
    private readonly IMediator _mediator;
    private readonly RedisCache _redisCache;
    public GetStateChangeStatusQueryHandler(IMediator mediator, RedisCache redisCache)
    {
        _mediator = mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<StateChangeDto>> Handle(GetStateChangeStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetActiveSymbolesQuery());

        List<StateChangeDto> Output = new();
        foreach (SymbolEntity item in result)
        {
            string key = $"{PublicTradeTopic}:{item.SymbolIsin}";

            // Console.WriteLine(key);
            var cacheValue = await _redisCache.GetCacheValue(key);

            var Current = new StateChangeDto(item, cacheValue.GetValueOrDefault(StateCode));
            Output.Add(Current);

        }
        if (request.ReturnAll)
        {
            return Output;
        }
        return Output.Where(t => !t.IsOk || String.IsNullOrEmpty(t.StateCode)).ToList();
    }
}