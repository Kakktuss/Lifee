using Hangfire;
using Lifee.RSS.Puller.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/*
 * Entity framework related configuration
 */
builder.Services.ConfigureDatabaseEngine(builder.Configuration);

/*
 * Hangfire related configuration
 */
builder.Services.ConfigureHangfire(builder.Configuration);

/*
 * Stan configuration
 */
builder.Services.ConfigureStan(builder.Configuration);

builder.Services.ConfigureDependencies();

builder.Services.ConfigureLogging(builder.Configuration, builder.Environment);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure Hangfire Dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    IgnoreAntiforgeryToken = true
});

// Configure routing
app.UseRouting();

// Configure endpoints
app.UseEndpoints(endpoints => { endpoints.MapHangfireDashboard(); });

app.Run();