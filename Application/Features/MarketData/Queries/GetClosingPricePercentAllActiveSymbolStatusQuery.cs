using domain.Entities;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using static domain.Constants.LsConstants;

namespace healthcheck.api.Application.Features.MarketData.Queries;

public class GetClosingPricePercentAllActiveSymbolStatusQuery : IRequest<IEnumerable<ClosePricePersentDto>>
{
    public GetClosingPricePercentAllActiveSymbolStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }
}
public class GetClosingPricePercentAllActiveSymbolStatusQueryHandler : IRequestHandler<GetClosingPricePercentAllActiveSymbolStatusQuery, IEnumerable<ClosePricePersentDto>>
{
      
    private readonly IMediator _mediator;
    private readonly RedisCache _redisCache;

    public GetClosingPricePercentAllActiveSymbolStatusQueryHandler(IMediator mediator, RedisCache redisCache)
    {
        _mediator=mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<ClosePricePersentDto>> Handle(GetClosingPricePercentAllActiveSymbolStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetActiveSymbolesQuery());

        List<ClosePricePersentDto> Output = new();
       
        foreach (SymbolEntity item in result)
        {               
            string key = $"{ClosePriceTopic}:{item.SymbolIsin}";

            var cacheValue = await _redisCache.GetCacheValue(key);
            ClosePricePersentDto Current = new ClosePricePersentDto(item, cacheValue);
            Output.Add(Current);
        }
       

        if (request.ReturnAll)
        {
            return Output;
        }
        return Output.Where(t => !t.IsOk|| t.ChangePersent !=0).ToList();
    }

}