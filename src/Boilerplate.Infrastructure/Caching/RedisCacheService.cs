using System.Text.Json;
using Boilerplate.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Boilerplate.Infrastructure.Caching;

internal sealed class RedisCacheService(IDistributedCache cache) : ICacheService
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? value = await cache.GetAsync(key, cancellationToken);
        return value is null ? default : JsonSerializer.Deserialize<T>(value, SerializerOptions);
    }

    public Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, SerializerOptions);
        DistributedCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = expiration,
        };

        return cache.SetAsync(key, bytes, options, cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return cache.RemoveAsync(key, cancellationToken);
    }
}
