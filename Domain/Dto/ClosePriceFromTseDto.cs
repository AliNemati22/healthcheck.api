using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class ClosePriceFromTseDto:BaseDto
{
    public ClosePriceFromTseDto(TradeSelectedDateTradeSelectedDate itemTse,  SymbolEntity symbolEntity , double? closeingPrice)
    {

        this.Isin = symbolEntity.SymbolIsin;
        this.ClosingPriceTse = (double)itemTse.PClosing;
        this.Title =symbolEntity.Title;

        if (closeingPrice.HasValue)
        {
            this.ClosingPrice=closeingPrice.Value;
            this.IsOk =false;
            errorMessage = "ClosingPriceTse:"+this.ClosingPriceTse.ToString() +"-----" +"closeingPrice:"+this.ClosingPrice.ToString();
        }

    }
    public string? Isin { get; set; }
    public double ClosingPrice { get; set; }
    public double ClosingPriceTse { get; set; }
}