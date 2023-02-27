using domain.Entities;

namespace healthcheck.api.Domain.Dto;

public class ClosePricePersentDto:BaseDto
{
    public ClosePricePersentDto(SymbolEntity symbolEntity, Dictionary<string, string> closePrice)
    {
        Isin=symbolEntity.SymbolIsin;
        Title=symbolEntity.Title;
        if (closePrice != null)
        {
            ClosingPrice = Convert.ToDouble(closePrice.GetValueOrDefault(domain.Constants.LsConstants.ClosingPrice));
            PrevClosingPrice = Convert.ToDouble(closePrice.GetValueOrDefault(domain.Constants.LsConstants.PrevClosingPrice));
            if (PrevClosingPrice==0)
            {
                // Console.WriteLine($"closing: {ClosingPrice} and prevclosingPrice:{PrevClosingPrice}");
                ChangePersent = -1;
            }
            else
            {
                ChangePersent= (ClosingPrice-PrevClosingPrice)/PrevClosingPrice*100;
                IsOk =false;
                errorMessage = "ChangePersent:"+ChangePersent;
            }

        }
    }
    public string? Isin { get; set; }
    public double ChangePersent { get; set; }
    public double ClosingPrice { get; }
    public double PrevClosingPrice { get; }
}