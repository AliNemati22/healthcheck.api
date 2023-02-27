namespace healthcheck.api.Domain.Dto;

public class AllStatusDto
{

    public IEnumerable<ClosePriceFromTseDto> ClosePriceFromTseDtoes { get; set; }

    public IEnumerable<ClosePricePersentDto> ClosePricePersentDtoes { get; set; }

    public IEnumerable<LastTradedPriceFromTseDto> LastTradedPriceFromTseDtoes { get; set; }

    public IEnumerable<PrevClosePriceFromTseDto> PrevClosePriceFromTseDtoes { get; set; }

    public IEnumerable<PriceVariationDto> PriceVariationDtoes { get; set; }

    public IEnumerable<StateChangeDto> StateChangeDto { get; set; }

    public IEnumerable<TotalNumberOfSharesTradedDto> TotalNumberOfSharesTradedDtoes { get; set; }

    public IEnumerable<TotalTradeValueDto> TotalTradeValueDtoes { get; set; }

    public IEnumerable<WebServiceStatusDto> WebServiceStatusDtoes { get; set; }

}