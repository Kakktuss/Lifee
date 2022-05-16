using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Configuration;
using Lifee.RSS.Api.GraphObjects.InputTypes.RssFeed;
using Lifee.RSS.Api.GraphObjects.Mutations;
using Lifee.RSS.Api.GraphObjects.Mutations.Results.RssFeed;
using Lifee.RSS.Api.GraphObjects.ObjectTypes;
using Lifee.RSS.Api.GraphObjects.Queries;

namespace Lifee.RSS.Api.Extensions;

public static class SchemaBuilderExtensions
{
    public static IRequestExecutorBuilder AddBaseTypes(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddMutationType(e => e.Name("Mutations"))
            .AddQueryType(e => e.Name("Queries"))
            .AddType<WorkaroundQueries>();
    }

    public static IRequestExecutorBuilder AddRssFeedTypes(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddType<RssFeedItemObjectType>()
                .AddType<PullUserRssFeedInputType>()
            // Mutations
            .AddTypeExtension<RssFeedMutations>() 
                // Result types
                .AddType<PullUserRssFeedMutationResultType>();
    }

    public static IRequestExecutorBuilder ConfigureEngine(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddConvention<IFilterConvention>(new FilterConvention(x => x.AddDefaults()))
            .AddConvention<ISortConvention>(new SortConvention(x => x.AddDefaults()))
            .ModifyOptions(options =>
            {
                options.StrictValidation = false;
            });
    }
}