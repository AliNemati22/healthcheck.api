using api.common;
using healthcheck.api.Application.Features.AllStatus.Queries;
using healthcheck.api.Application.Features.MarketData.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace healthcheck.api.Controllers;


public class MarketDataController : EasyControllerBase
{

    [HttpGet("total-number-Of-shares-traded")]
    public   Task<IEnumerable<TotalNumberOfSharesTradedDto>> GetTotalNumberOfSharesTraded(bool returnAll = false)
            => Mediator.Send(new GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQuery(returnAll));
    

    [HttpGet("total-trade-value")]
    public  Task<IEnumerable<TotalTradeValueDto>> GeTotalTradeValue(bool returnAll = false)
       => Mediator.Send(new GetTradeValueInAllActiveSymbolStatusQuery(returnAll));

    [HttpGet("Price-variation")]
    public  Task<IEnumerable<PriceVariationDto>> GetPriceVariation(bool returnAll = false)
         => Mediator.Send(new GetPriceVariationInAllActiveSymbolStatusQuery(returnAll));

    [HttpGet("closing-price-percent")]
    public  Task<IEnumerable<ClosePricePersentDto>> GetClosingPricePercent(bool returnAll = false)
        => Mediator.Send(new GetClosingPricePercentAllActiveSymbolStatusQuery(returnAll));

    [HttpGet("state-change")]
    public  Task<IEnumerable<StateChangeDto>> GetStateChange(bool returnAll = false)
        =>  Mediator.Send(new GetStateChangeStatusQuery(returnAll));

}