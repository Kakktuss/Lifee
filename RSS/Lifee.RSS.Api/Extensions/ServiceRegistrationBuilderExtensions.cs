using Lifee.RSS.Application.DataAccess.Repositories;
using Lifee.RSS.Application.Services;

namespace Lifee.RSS.Api.Extensions;

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
            .AddScoped<IRssFeedPullerService, RssFeedPullerService>();
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRssFeedRepository, RssFeedRepository>();

        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        return serviceCollection;
    }
}