using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace Lifee.RSS.Api.Extensions;

public static class HttpServiceBuilderExtensions
{
    public static IServiceCollection ConfigureGraphQlEngine(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddGraphQLServer()
            .ConfigureEngine()
            .AddBaseTypes()
            .AddRssFeedTypes();

        return serviceCollection;
    }
}