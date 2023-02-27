using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class TotalNumberOfSharesTradedDto:BaseDto
{
    public TotalNumberOfSharesTradedDto(SymbolEntity symbolEntity,double? totalNumberOfSharesTraded)
    {
        this.Isin = symbolEntity.SymbolIsin;
        this.Title = symbolEntity.SymbolName;

        if (totalNumberOfSharesTraded.HasValue &&  totalNumberOfSharesTraded.Value>0)
        {
            this.TotalNumberOfSharesTraded=totalNumberOfSharesTraded.Value;
            this.IsOk =false;
            errorMessage = "TotalNumberOfSharesTraded:"+this.TotalNumberOfSharesTraded.ToString();
        }
    }

    public string? Isin { get; set; }
    public double TotalNumberOfSharesTraded { get; set; }
}