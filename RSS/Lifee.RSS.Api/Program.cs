using HotChocolate.AspNetCore;
using Lifee.RSS.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/*
 * Entity framework related configuration
 */
builder.Services.ConfigureDatabaseEngine(builder.Configuration);

/*
 * Stan configuration
 */
builder.Services.ConfigureStan(builder.Configuration);

builder.Services.ConfigureDependencies();

builder.Services.ConfigureLogging(builder.Configuration, builder.Environment);

/*
 * GraphQL configuration
 */
builder.Services.ConfigureGraphQlEngine();

builder.Services.AddHttpContextAccessor();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure routing
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints
        .MapGraphQL()
        .WithOptions(new GraphQLServerOptions
        {
            Tool =
            {
                Enable = true
            }
        });
    endpoints.MapBananaCakePop("/graphql/ui");
});

app.Run();