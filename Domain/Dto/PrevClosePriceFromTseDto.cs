using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class PrevClosePriceFromTseDto:BaseDto
{
    public PrevClosePriceFromTseDto(TradeSelectedDateTradeSelectedDate itemTse, SymbolEntity symbolEntity, double? PrevClosingPrice)
    {

        this.Isin = symbolEntity.SymbolIsin;
        this.Title =symbolEntity.Title;
        this.PrevClosingpriceTse = (double)itemTse.PriceYesterday;
        if (PrevClosingPrice.HasValue)
        {
            this.PrevClosingprice=PrevClosingPrice.Value;
            this.IsOk =false;
            errorMessage = "PrevClosingpriceTse:"+this.PrevClosingpriceTse.ToString() +"-----" +"PrevClosingprice:"+PrevClosingprice.ToString();
        }
    }
    public string? Isin { get; set; }
    public double PrevClosingprice { get; set;}
    public double PrevClosingpriceTse { get; set; }
}