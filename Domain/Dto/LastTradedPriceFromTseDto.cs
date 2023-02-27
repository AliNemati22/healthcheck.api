using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class LastTradedPriceFromTseDto:BaseDto
{
    public LastTradedPriceFromTseDto(TradeSelectedDateTradeSelectedDate itemTse, SymbolEntity symbolEntity, double? lastTradedPrice)
    {
        this.Isin = symbolEntity.SymbolIsin;
        this.Title = symbolEntity.Title;
        this.LastTradedPriceTse = itemTse.PriceYesterday;
        if (lastTradedPrice.HasValue)
        {
            this.LastTradedPrice=lastTradedPrice.Value;
            this.IsOk =false;
            errorMessage = "lastTradedPriceTse:"+this.LastTradedPriceTse.ToString() +"-----" +"lastTradedPrice:"+lastTradedPrice.ToString();
        }
    }
    public string? Isin { get; set; }
    public double LastTradedPrice { get; set;}
    public double LastTradedPriceTse { get; set; }       
}