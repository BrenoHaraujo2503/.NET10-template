using Boilerplate.Api.Endpoints.Products;
using Boilerplate.Application;
using Boilerplate.Infrastructure;
using Boilerplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services));

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.MapProductEndpoints();

if (app.Configuration.GetValue<bool>("Database:ApplyMigrations"))
{
    await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

await app.RunAsync();

public partial class Program;
