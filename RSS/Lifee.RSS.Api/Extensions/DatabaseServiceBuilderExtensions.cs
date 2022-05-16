using Lifee.RSS.Application.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lifee.RSS.Api.Extensions;

public static class DatabaseServiceBuilderExtensions
{
    public static IServiceCollection ConfigureDatabaseEngine(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddEntityFrameworkSqlServer();

        serviceCollection.AddDbContext<LifeeDbContext>((serviceProvider, optionsBuilder) =>
        {
            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            10,
                            TimeSpan.FromSeconds(30),
                            null);
                    });
        });

        return serviceCollection;
    }
}