using System.Reflection;

namespace healthcheck.api.Application.Features.WebServiceStatus;

public static class WebServiceStatusQueryResolver
{
    private static Dictionary<string, Type> _queryTypes = new();

    public static void AddQueryTypes(params Assembly[] assembliesToScan)
    {
        foreach (Assembly assembly in assembliesToScan)
        {
            string queryInterfaceName = nameof(IWebServiceStatusQuery);
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterface(queryInterfaceName) != null)
                .ToDictionary(t => t.Name, t => t);
            _queryTypes = _queryTypes.Union(types).ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryType"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">when <paramref name="queryType"/> was not found </exception>
    public static Type GetQueryType(string queryType) => _queryTypes[queryType];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryType"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">when <paramref name="queryType"/> was not found or is not implementing <see cref="IWebServiceStatusQuery"/></exception>
    public static IWebServiceStatusQuery CreateQuery(string queryType, IConfiguration options)
    {
        Type type;
        try
        {
            type = GetQueryType(queryType);
        }
        catch (KeyNotFoundException e)
        {
            throw new ArgumentException($"RequestType of {queryType} not Found", e);
        }

        var query = Activator.CreateInstance(type, args: options) as IWebServiceStatusQuery;
        if (query == null)
        {
            throw new ArgumentException($"Type of {queryType} is not implementing {nameof(IWebServiceStatusQuery)}");
        }
        
        return query;
    }
}