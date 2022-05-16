using Lifee.Puller.RSS.DataAccess.Repositories;
using Lifee.Puller.RSS.Service;

namespace Lifee.RSS.Puller.Api.Extensions;

public static class ServiceRegistrationBuilderExtensions
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .ConfigureRepositories()
            .ConfigureServices();
    }
    
    private static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IRssFeedService, RssFeedService>();
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRssFeedRepository, RssFeedRepository>();

        return serviceCollection;
    }
}