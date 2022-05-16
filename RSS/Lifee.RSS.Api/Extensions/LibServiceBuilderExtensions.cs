using HotBrokerBus.Stan.Injection;
using Serilog;
using Serilog.Events;

namespace Lifee.RSS.Api.Extensions;

public static class LibServiceBuilderExtensions
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection serviceCollection, IConfiguration configuration, IWebHostEnvironment environment)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Environment", environment)
            .WriteTo.Console(LogEventLevel.Information,
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}")
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return serviceCollection;
    }

    public static IServiceCollection ConfigureStan(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddStanModules(options =>
            {
                options.WithConfig(configuration.GetSection("stanModules"));
            })
            .AddStanCommandBusConsumerClient()
            .AddStanEventBusConsumerClient();

        return serviceCollection;
    }
}