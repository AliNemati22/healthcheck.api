namespace healthcheck.api.Domain.Dto;

public class WebServiceStatusDto:BaseDto
{
    public  WebServiceStatusDto(string title, string webServiceAddress , bool isConnected)
    {
        WebServiceAddress = webServiceAddress;
        Title = title;
        IsOk = isConnected;
        if (!isConnected)
        {
            this.errorMessage ="عدم اتصال به سرویس";

        }

    }
    public string WebServiceAddress { get; private set; }
    
}