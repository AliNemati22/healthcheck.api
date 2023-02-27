using domain.Extensions;
using healthcheck.api.Application;
using healthcheck.api.Domain.Settings;
using healthcheck.api.Services;
using infrastructure;

CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] strings) =>
    MtsHost
        .CreateHostBuilder(strings)
        .ConfigureWebHostDefaults(hostBuilder => hostBuilder
            .ConfigureServices((context, services) =>
                services.AddControllers()
                    .Services.AddEndpointsApiExplorer()
                    .AddSwaggerGen()
                    .AddApiInfrastructure()
                    .AddApplicationInjection()
                    .AddCacheStorage(context.Configuration)
                    .AddSingleton<ITseServiceProvider, TseServiceProvider>()
                    .Configure<TseWebServiceOptions>(
                        context.Configuration.GetSection(TseWebServiceOptions.ConfigurationSectionName))
                    .Configure<WebServicesOptions>(
                        context.Configuration.GetSection(WebServicesOptions.ConfigurationSectionName))
            )
            .Configure((context, app) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    app.UseSwagger()
                        .UseSwaggerUI();
                }

                app.UseHttpsRedirection()
                    .UseAuthorization()
                    .UseRouting()
                    .UseEndpoints(endpoints => endpoints.MapControllers());
            })
        );