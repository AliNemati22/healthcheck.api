namespace healthcheck.api.Domain.Dto;

public class BaseDto
{
    public bool IsOk { get; set; } = true;
    public string Title { get; set; }
    public string errorMessage { get; set; }


}