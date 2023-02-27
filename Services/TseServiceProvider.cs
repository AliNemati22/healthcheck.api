using healthcheck.api.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;
using TsePublicV2;
using static TsePublicV2.TsePublicV2SoapClient;

namespace healthcheck.api.Services;

public class TseServiceProvider : ITseServiceProvider
{
    IOptions<TseWebServiceOptions> _options;
    public TseServiceProvider(IOptions<TseWebServiceOptions> options)
    {
        _options= options;
    }
    public int GetTseLastActiveDate()
    {
        try
        {
            using TsePublicV2SoapClient tseClient = new(EndpointConfiguration.TsePublicV2Soap);
            var list = tseClient.NSCStartAsync(_options.Value.UserName, _options.Value.Password).Result;
            if (list?.Nodes?.Count==0)
            {
                return 0;
            }

            // Console.WriteLine(list?.Nodes[1]);
            var xmlTextReader = list?.Nodes[1].CreateReader();
            var xmlSerializer = new XmlSerializer(typeof(diffgram));
            var mydiffgram = xmlSerializer.Deserialize(xmlTextReader) as diffgram;
            if (mydiffgram?.TseNSCStart ==null)
                return 0;
            //   Console.WriteLine(mydiffgram.TseNSCStart);
            return  mydiffgram.TseNSCStart.Where(t =>t.NSCEnd ==1).Max(t=>t.DEven);
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public async Task<TradeSelectedDateTradeSelectedDate> GetInstTrade(long insCode, int fromDate,int toDate)
    {
        try
        {
            using TsePublicV2SoapClient tseClient = new(EndpointConfiguration.TsePublicV2Soap);
            var list = await tseClient.InstTradeAsync(_options.Value.UserName, _options.Value.Password, insCode , fromDate,toDate);
            if (list?.Nodes?.Count==0)
            {
                return new TradeSelectedDateTradeSelectedDate();
            }

            using var xmlTextReader = list?.Nodes[1].CreateReader();
            var xmlSerializer = new XmlSerializer(typeof(diffgram));
            var mydiffgram = xmlSerializer.Deserialize(xmlTextReader) as diffgram;
            if (mydiffgram?.TradeSelectedDate ==null)
                return new TradeSelectedDateTradeSelectedDate();

            return mydiffgram.TradeSelectedDate.First();
        }
        catch (Exception e)
        {
            //Console.WriteLine(e.ToString());
            return new TradeSelectedDateTradeSelectedDate();
        }
    }
}