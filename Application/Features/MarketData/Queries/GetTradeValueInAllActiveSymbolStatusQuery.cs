using domain.Entities;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using static domain.Constants.LsConstants;


namespace healthcheck.api.Application.Features.MarketData.Queries;

public class GetTradeValueInAllActiveSymbolStatusQuery : IRequest<IEnumerable<TotalTradeValueDto>>
{

    public GetTradeValueInAllActiveSymbolStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }
}

public class GetTradeValueInAllActiveSymbolStatusQueryHandler : IRequestHandler<GetTradeValueInAllActiveSymbolStatusQuery, IEnumerable<TotalTradeValueDto>>
{

    private readonly IMediator _mediator;
    private readonly RedisCache _redisCache;
    public GetTradeValueInAllActiveSymbolStatusQueryHandler(IMediator mediator, RedisCache redisCache)
    {
        _mediator=mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<TotalTradeValueDto>> Handle(GetTradeValueInAllActiveSymbolStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetActiveSymbolesQuery());

        List<TotalTradeValueDto> output = new();
        foreach (SymbolEntity item in result)
        {
            string key = $"{ClosePriceTopic}:{item.SymbolIsin}";

            // Console.WriteLine(key);
            var cacheValue = await _redisCache.GetCacheValue(key);
            double? totalTradeValue = null;
            totalTradeValue = Convert.ToDouble(cacheValue.GetValueOrDefault(TotalTradeValue));
            TotalTradeValueDto Current = new TotalTradeValueDto(item, totalTradeValue);
            output.Add(Current);
        }

        if (request.ReturnAll)
        {
            return output;
        }
        return output.Where(t => !t.IsOk || t.TotalTradeValue !=0).ToList();
    }
}