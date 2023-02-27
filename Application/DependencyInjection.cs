using domain.Environments.ThirdPartyApiOptions;
using healthcheck.api.Application.Common.Behaviors;
using healthcheck.api.Application.Features.WebServiceStatus;
using healthcheck.api.Application.Features.WebServiceStatus.Queries;
using infrastructure.ThirdParty.APi.EasyChart;
using MediatR;
using System.Reflection;

namespace healthcheck.api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationInjection(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

        services.AddPooya();
        WebServiceStatusQueryResolver.AddQueryTypes(Assembly.GetExecutingAssembly());
            
        return services;
    }

    private static IServiceCollection AddPooya(this IServiceCollection services)
    {
        services.AddHttpClient<PooyaService.PooyaApiAuthenticationService>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(PooyaOptions.Default.Option.Url);
                client.Timeout = TimeSpan.FromMilliseconds(PooyaOptions.Default.Option.TimeoutMs);
            });
        services.AddTransient<PooyaService.PooyaServiceTokenHandler>();
        services.AddHttpClient<IRequestHandler<PooyaStatusQuery, bool>, PooyaStatusQueryHandler>(client =>
        {
            client.BaseAddress = new Uri(PooyaOptions.Default.Option.Url);
            client.Timeout = TimeSpan.FromMilliseconds(PooyaOptions.Default.Option.TimeoutMs);
        }).AddHttpMessageHandler<PooyaService.PooyaServiceTokenHandler>();
        return services;
    }
}