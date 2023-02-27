using domain.Entities;
using domain.Interfaces;
using healthcheck.api.Application.Common;
using healthcheck.api.Application.Features.Symbol.Queries;
using healthcheck.api.Domain.Dto;
using healthcheck.api.Services;
using MediatR;
using static domain.Constants.LsConstants;

namespace healthcheck.api.Application.Features.MarketDataFromTse.Queries;

public class GetClosePriceTseStatusQuery : IRequest<IEnumerable<ClosePriceFromTseDto>>
{
    public GetClosePriceTseStatusQuery(bool returnAll)
    {
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }

}

public class GetClosePriceTseStatusQueryHandler : IRequestHandler<GetClosePriceTseStatusQuery, IEnumerable<ClosePriceFromTseDto>>
{

    private readonly IMediator _mediator;
    private readonly ITseServiceProvider _tseService;
    private readonly IRepository<InstrumentProminentFieldsEntity> _instrumentProminentFieldsRepository;
    private readonly RedisCache _redisCache;

    public GetClosePriceTseStatusQueryHandler(
        IRepositoryAccessor repositoryAccessor, ITseServiceProvider tseService, IMediator mediator, RedisCache redisCache)
    {
        _mediator=mediator;
        _redisCache = redisCache;
        _tseService=tseService;
        _instrumentProminentFieldsRepository=repositoryAccessor.GetRepository<InstrumentProminentFieldsEntity>(reThrowException: true);
    }

    public async Task<IEnumerable<ClosePriceFromTseDto>> Handle(GetClosePriceTseStatusQuery request, CancellationToken cancellationToken)
    {
        bool returnAll = request.ReturnAll;
        List<ClosePriceFromTseDto> output = new();
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
            double? closingPrice = null;
            closingPrice = Convert.ToDouble(cacheValue.GetValueOrDefault(ClosingPrice));

            //Console.WriteLine($"{++i} :{symbol.SymbolIsin}, {insCode}, close price radis: {closingPrice}, close price tse: {TseInsCodeInformation.PClosing} ");
            ClosePriceFromTseDto Current = new ClosePriceFromTseDto(TseInsCodeInformation, symbol, closingPrice);
            output.Add(Current);
        }
        if (returnAll)
        {
            return output.ToList();
        }
           
        return output.Where(t => !t.IsOk || t.ClosingPrice != t.ClosingPriceTse).ToList();
            
    }
}