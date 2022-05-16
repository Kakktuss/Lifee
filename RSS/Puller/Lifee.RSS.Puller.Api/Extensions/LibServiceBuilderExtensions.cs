using Hangfire;
using Hangfire.SqlServer;
using HotBrokerBus.Stan.Injection;
using Lifee.Puller.RSS.IntegrationCommands.Commands;
using Lifee.Puller.RSS.IntegrationCommands.Handlers;
using Lifee.Puller.RSS.IntegrationEvents.Events;
using Lifee.Puller.RSS.IntegrationEvents.Handlers;
using Serilog;
using Serilog.Events;

namespace Lifee.RSS.Puller.Api.Extensions;

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
    
    public static IServiceCollection ConfigureHangfire(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddHangfire(hangfireConfiguration =>
        {
            hangfireConfiguration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnectionString"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
        });

        serviceCollection.AddHangfireServer();

        return serviceCollection;
    }
    
    public static IServiceCollection ConfigureStan(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddStanModules(options =>
            {
                options.WithConfig(configuration.GetSection("stanModules"));
            })
            .AddStanCommandBusSubscriberClient(options =>
            {
                options.WithConfig(configuration.GetSection("stanCommandBus"));

                options.ConfigureSubscriptions(subscriptionOptions =>
                {
                    subscriptionOptions
                        .Bind<TriggerRssFeedPullIntegrationCommand, TriggerRssFeedPullIntegrationCommandHandler>();
                });
            })
            .AddStanEventBus(options =>
            {
                options.WithConfig(configuration.GetSection("stanEventBus"));

                options.ConfigureSubscriptions(subscriptionOptions =>
                {
                    subscriptionOptions.Bind<RssFeedCreatedIntegrationEvent, RssFeedCreatedIntegrationEventHandler>();
                });
            });

        return serviceCollection;
    }
}