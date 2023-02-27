using domain.Entities;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using static domain.Constants.LsConstants;


namespace healthcheck.api.Application.Features.MarketData.Queries;

public class GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQuery : IRequest<IEnumerable<TotalNumberOfSharesTradedDto>>
{

    public GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }
}

public class GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQueryHandler : IRequestHandler<GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQuery, IEnumerable<TotalNumberOfSharesTradedDto>>
{

    private readonly IMediator _mediator;
    private readonly RedisCache _redisCache;
    public GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQueryHandler(IMediator mediator, RedisCache redisCache)
    {
        _mediator=mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<TotalNumberOfSharesTradedDto>> Handle(GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetActiveSymbolesQuery());

        List<TotalNumberOfSharesTradedDto> Output = new();
        foreach (SymbolEntity item in result)
        {
            string key = $"{PublicTradeTopic}:{item.SymbolIsin}";

            //Console.WriteLine(key);
            var cacheValue = await _redisCache.GetCacheValue(key);
            double? dTotalNumberOfSharesTraded = null;
            dTotalNumberOfSharesTraded = Convert.ToDouble(cacheValue.GetValueOrDefault(TotalNumberOfSharesTraded));
            TotalNumberOfSharesTradedDto Current = new TotalNumberOfSharesTradedDto(item, dTotalNumberOfSharesTraded);
            Output.Add(Current);
        }
        if (request.ReturnAll)
        {
            return Output;
        }
        return Output.Where(t => !t.IsOk || t.TotalNumberOfSharesTraded !=0).ToList();
    }

}