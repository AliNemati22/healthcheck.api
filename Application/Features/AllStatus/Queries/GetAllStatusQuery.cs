using healthcheck.api.Application.Features.MarketData.Queries;
using healthcheck.api.Application.Features.MarketDataFromTse.Queries;
using healthcheck.api.Application.Features.WebServiceStatus.Queries;
using healthcheck.api.Domain.Dto;
using MediatR;

namespace healthcheck.api.Application.Features.AllStatus.Queries;

public class GetAllStatusQuery : IRequest<AllStatusDto>
{
    public GetAllStatusQuery(bool returnAll)
    { 
        ReturnAll = returnAll;
    }
    public bool ReturnAll { get; }
}

public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, AllStatusDto>
{
    private readonly IMediator _mediator;

    public GetAllStatusQueryHandler(IMediator mediator)
    {
        _mediator=mediator;
    }

    public async Task<AllStatusDto> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        bool returnAll = request.ReturnAll;
        var starttime=DateTime.Now;
        Console.WriteLine($"Start time :{DateTime.Now}");
        AllStatusDto allStatusDto = new ();

        List<Task> tasks = new ();
        //TSE
        tasks.Add( _mediator.Send(new GetClosePriceTseStatusQuery(returnAll)).ContinueWith(t => allStatusDto.ClosePriceFromTseDtoes =  t.Result));
        tasks.Add( _mediator.Send(new GetLastDayPriceTseStatusQuery(returnAll)).ContinueWith(t=> allStatusDto.PrevClosePriceFromTseDtoes = t.Result));
        tasks.Add( _mediator.Send(new GetLastTradedPriceTseStatusQuery(returnAll)).ContinueWith(t=> allStatusDto.LastTradedPriceFromTseDtoes = t.Result));

        //Mofid DataBases
        tasks.Add( _mediator.Send(new GetClosingPricePercentAllActiveSymbolStatusQuery(returnAll)).ContinueWith(t => allStatusDto.ClosePricePersentDtoes = t.Result));
        tasks.Add( _mediator.Send(new GetPriceVariationInAllActiveSymbolStatusQuery(returnAll)).ContinueWith(t=> allStatusDto.PriceVariationDtoes =  t.Result));
        tasks.Add( _mediator.Send(new GetStateChangeStatusQuery(returnAll)).ContinueWith(t=> allStatusDto.StateChangeDto =  t.Result));
        tasks.Add( _mediator.Send(new GetTotalNumberOfSharesTradedInAllActiveSymbolStatusQuery(returnAll)).ContinueWith(t => allStatusDto.TotalNumberOfSharesTradedDtoes = t.Result));
        tasks.Add( _mediator.Send(new GetTradeValueInAllActiveSymbolStatusQuery(returnAll)).ContinueWith(t => allStatusDto.TotalTradeValueDtoes = t.Result));

        //Web Services
        tasks.Add( _mediator.Send(new GetWebServicesStatusQuery(returnAll)).ContinueWith(t => allStatusDto.WebServiceStatusDtoes = t.Result));
            
        await Task.WhenAll(tasks);
        Console.WriteLine($" GetAllStatusQuery Duration :{DateTime.Now -  starttime}");

        return allStatusDto;
    }
}