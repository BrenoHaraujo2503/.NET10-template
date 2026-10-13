using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Products.GetProductById;

public sealed class GetProductByIdHandler(IProductRepository repository, ICacheService cache)
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public async Task<ProductDto?> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string cacheKey = ProductCacheKeys.ById(id);
        ProductDto? cached = await cache.GetAsync<ProductDto>(cacheKey, cancellationToken);

        if (cached is not null)
        {
            return cached;
        }

        Product? product = await repository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        ProductDto result = ProductDto.FromDomain(product);
        await cache.SetAsync(cacheKey, result, CacheDuration, cancellationToken);
        return result;
    }
}
