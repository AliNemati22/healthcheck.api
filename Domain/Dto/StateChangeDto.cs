using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class StateChangeDto : BaseDto
{
    public StateChangeDto(SymbolEntity symbolEntity, string? stateCode)
    {
        this.isin = symbolEntity.SymbolIsin;
        this.Title = symbolEntity.Title;

        if (stateCode != null)
        {
            this.IsOk = false;
            this.StateCode = stateCode;
            errorMessage = "stateCode:"+this.StateCode.ToString();
        }
    }

    public string isin { get; set; }
    public string? StateCode { get; set; }
    
}