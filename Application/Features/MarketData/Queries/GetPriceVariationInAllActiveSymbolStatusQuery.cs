using domain.Entities;
using domain.Interfaces;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using static domain.Constants.LsConstants;

namespace healthcheck.api.Application.Features.MarketData.Queries;

public class GetPriceVariationInAllActiveSymbolStatusQuery : IRequest<IEnumerable<PriceVariationDto>>
{
    public GetPriceVariationInAllActiveSymbolStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }

}

public class GetPriceVariationInAllActiveSymbolStatusQueryHandler : IRequestHandler<GetPriceVariationInAllActiveSymbolStatusQuery, IEnumerable<PriceVariationDto>>
{
    private readonly IMediator _mediator;
    private readonly RedisCache _redisCache;

    public GetPriceVariationInAllActiveSymbolStatusQueryHandler(IMediator mediator, RedisCache redisCache)
    {
        _mediator=mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<PriceVariationDto>> Handle(GetPriceVariationInAllActiveSymbolStatusQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetActiveSymbolesQuery());

        List<PriceVariationDto> Output = new();
        foreach (SymbolEntity item in result)
        {
            string key = $"{PublicTradeTopic}:{item.SymbolIsin}";

            // Console.WriteLine(key);
            var cacheValue = await _redisCache.GetCacheValue(key); 
            double? priceVariation = null;
            priceVariation = Convert.ToDouble(cacheValue.GetValueOrDefault(PriceVar));
            PriceVariationDto Current = new PriceVariationDto(item, priceVariation);
            Output.Add(Current);
        }

        if (request.ReturnAll)
        {
            return Output;
        }
        return Output.Where(t => !t.IsOk || t.PriceVariation !=0).ToList();
    }

}