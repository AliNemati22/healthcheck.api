namespace healthcheck.api.Services;

public interface ITseServiceProvider
{
    Task<TradeSelectedDateTradeSelectedDate> GetInstTrade(long insCode, int fromDate, int toDate);
    int GetTseLastActiveDate();
}