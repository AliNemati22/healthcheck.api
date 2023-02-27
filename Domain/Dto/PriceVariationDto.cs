using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class PriceVariationDto : BaseDto
{
    public PriceVariationDto(SymbolEntity symbolEntity, double? priceVariation)
    {
        this.Isin = symbolEntity.SymbolIsin;
        this.Title = symbolEntity.Title;

        if (priceVariation.HasValue  && priceVariation>0)
        {
            this.PriceVariation = priceVariation.Value;
            this.IsOk = false;
            errorMessage = "PriceVariation:"+this.PriceVariation.ToString();
        }
    }

    public string? Isin { get; set; }
    public double PriceVariation { get; set; }
}