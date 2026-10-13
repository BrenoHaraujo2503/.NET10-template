using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Infrastructure.Caching;
using Boilerplate.Infrastructure.Persistence;
using Boilerplate.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string databaseConnection = configuration.GetConnectionString("Database")
            ?? throw new InvalidOperationException("Connection string 'Database' was not configured.");
        string redisConnection = configuration.GetConnectionString("Redis")
            ?? throw new InvalidOperationException("Connection string 'Redis' was not configured.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(databaseConnection, npgsql =>
                npgsql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnection;
            options.InstanceName = "boilerplate:";
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
