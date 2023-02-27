using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class TotalTradeValueDto:BaseDto
{
    public TotalTradeValueDto(SymbolEntity SymbolEntity, double? totalTradeValue)
    {
        this.Isin = SymbolEntity.SymbolIsin;
        this.Title = SymbolEntity.SymbolName;
        if (totalTradeValue.HasValue)
        {
            this.TotalTradeValue=totalTradeValue.Value;
            this.IsOk =false;
            errorMessage = "TotalTradeValue:"+this.TotalTradeValue.ToString();
        }
    }
    public string? Isin { get; set; }
    public double TotalTradeValue { get; set;}
}