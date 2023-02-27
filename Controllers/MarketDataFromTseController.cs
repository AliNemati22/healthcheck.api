using api.common;
using healthcheck.api.Application.Features.MarketDataFromTse.Queries;
using healthcheck.api.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace healthcheck.api.Controllers;


public class MarketDataFromTseController : EasyControllerBase
{
    

    [HttpGet("Prev-closing-price-compare-with-Tse")]
    public  Task<IEnumerable<PrevClosePriceFromTseDto>> GetPrevClosingPriceCompareWithTse(bool returnAll = false)
        => Mediator.Send(new GetLastDayPriceTseStatusQuery(returnAll));

    [HttpGet("traded-price-compare-With-tse")]
    public  Task<IEnumerable<LastTradedPriceFromTseDto>> GetLastTradedPriceCompareWithTse(bool returnAll = false)
       => Mediator.Send(new GetLastTradedPriceTseStatusQuery(returnAll));
        
    [HttpGet("close-price-compare-with-Tse")]
    public  Task<IEnumerable<ClosePriceFromTseDto>> GetClosePriceCompareWithTse(bool returnAll=false)
        => Mediator.Send(new GetClosePriceTseStatusQuery(returnAll));
    
}