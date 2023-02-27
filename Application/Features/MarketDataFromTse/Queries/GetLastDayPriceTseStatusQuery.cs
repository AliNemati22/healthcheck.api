using domain.Entities;
using domain.Interfaces;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using healthcheck.api.Services;
using MediatR;
using static domain.Constants.LsConstants;


namespace healthcheck.api.Application.Features.MarketDataFromTse.Queries;

public class GetLastDayPriceTseStatusQuery : IRequest<IEnumerable<PrevClosePriceFromTseDto>>
{
    public GetLastDayPriceTseStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }


}

public class GetLastDayPriceTseStatusQueryHandler : IRequestHandler<GetLastDayPriceTseStatusQuery, IEnumerable<PrevClosePriceFromTseDto>>
{
    private readonly ITseServiceProvider _tseService;
    private readonly IRepository<InstrumentProminentFieldsEntity> _instrumentProminentFieldsRepository;
    private readonly IMediator _mediator;
    private readonly RedisCache _redisCache;

    public GetLastDayPriceTseStatusQueryHandler(IRepositoryAccessor repositoryAccessor, ITseServiceProvider tseService ,IMediator mediator, RedisCache redisCache)
    {
        _tseService=tseService;
        _instrumentProminentFieldsRepository=repositoryAccessor.GetRepository<InstrumentProminentFieldsEntity>(reThrowException: true);
        _mediator=mediator;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<PrevClosePriceFromTseDto>> Handle(GetLastDayPriceTseStatusQuery request, CancellationToken cancellationToken)
    {
        List<PrevClosePriceFromTseDto> output = new();
        var activeSymbols = await _mediator.Send(new GetActiveSymbolesQuery());
        var instrumentList = await _mediator.Send(new GetInstrumentProminentFieldsQuery());
            
        int tseLastActiveDate = _tseService.GetTseLastActiveDate();

        foreach (var symbol in activeSymbols)
        {
            var insCode = instrumentList.FirstOrDefault(t => t.Isin==symbol.SymbolIsin)?.InsCode;
            if (insCode==null) continue;
            var TseInsCodeInformation = await _tseService.GetInstTrade(Convert.ToInt64(insCode), tseLastActiveDate, tseLastActiveDate);

            string key = $"{ClosePriceTopic}:{symbol.SymbolIsin}";              
            var cacheValue = await _redisCache.GetCacheValue(key);
            double? prevclosingPrice = null;
            prevclosingPrice = Convert.ToDouble(cacheValue.GetValueOrDefault(PrevClosingPrice));

            PrevClosePriceFromTseDto Current = new PrevClosePriceFromTseDto(TseInsCodeInformation, symbol, prevclosingPrice);
            output.Add(Current);
        }

        if (request.ReturnAll)
        {
            output.ToList();
        }

        return output.Where(t => !t.IsOk  || t.PrevClosingprice != t.PrevClosingpriceTse).ToList();
    }
}