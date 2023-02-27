using domain.Entities;
using domain.Interfaces;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using healthcheck.api.Services;
using MediatR;
using static domain.Constants.LsConstants;

namespace healthcheck.api.Application.Features.MarketDataFromTse.Queries;

public class GetLastTradedPriceTseStatusQuery : IRequest<IEnumerable<LastTradedPriceFromTseDto>>
{
    public GetLastTradedPriceTseStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }
}

public class GetLastTradePriceTseStatusQueryHandler : IRequestHandler<GetLastTradedPriceTseStatusQuery, IEnumerable<LastTradedPriceFromTseDto>>
{
    private readonly IMediator _mediator;
    private readonly ITseServiceProvider _tseService;
    private readonly IRepository<InstrumentProminentFieldsEntity> _instrumentProminentFieldsRepository;
    private readonly RedisCache _redisCache;
    public GetLastTradePriceTseStatusQueryHandler(IRepositoryAccessor repositoryAccessor, ITseServiceProvider tseService , IMediator mediator, RedisCache redisCache)
    {
        _tseService=tseService;
        _instrumentProminentFieldsRepository=repositoryAccessor.GetRepository<InstrumentProminentFieldsEntity>(reThrowException: true);
        _mediator=mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<LastTradedPriceFromTseDto>> Handle(GetLastTradedPriceTseStatusQuery request, CancellationToken cancellationToken)
    {

        List<LastTradedPriceFromTseDto> output = new();
           
        var activeSymbols = await _mediator.Send(new GetActiveSymbolesQuery());
        var instrumentList = await _mediator.Send(new GetInstrumentProminentFieldsQuery());
        int tseLastActiveDate = _tseService.GetTseLastActiveDate();

        foreach (var symbol in activeSymbols)
        {

            var insCode = instrumentList.FirstOrDefault(t => t.Isin==symbol.SymbolIsin)?.InsCode;
            if (insCode==null) continue;
            var TseInsCodeInformation = await _tseService.GetInstTrade(Convert.ToInt64(insCode), tseLastActiveDate, tseLastActiveDate);

            string key = $"{PublicTradeTopic}:{symbol.SymbolIsin}";

            var cacheValue = await _redisCache.GetCacheValue(key);
            double? lastTradedPrice = null;
            lastTradedPrice = Convert.ToDouble(cacheValue.GetValueOrDefault(LastTradedPrice));
            //Console.WriteLine($"{++i} :{symbol.SymbolIsin}, {insCode}, lastTradedPrice radis: {lastTradedPrice}, lastTradedPrice: {TseInsCodeInformation.PClosing} ");
            LastTradedPriceFromTseDto current = new LastTradedPriceFromTseDto(TseInsCodeInformation, symbol, lastTradedPrice);
            output.Add(current);
        }
        if (request.ReturnAll)
        {
            return output;
        }
        return output.Where(t => !t.IsOk  || t.LastTradedPriceTse != t.LastTradedPrice).ToList();
    }
}