using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Products.GetProducts;

public sealed class GetProductsHandler(IProductRepository repository, ICacheService cache)
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public async Task<IReadOnlyList<ProductDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<ProductDto>? cached =
            await cache.GetAsync<IReadOnlyList<ProductDto>>(ProductCacheKeys.List, cancellationToken);

        if (cached is not null)
        {
            return cached;
        }

        IReadOnlyList<Product> products = await repository.ListAsync(cancellationToken);
        ProductDto[] result = products.Select(ProductDto.FromDomain).ToArray();

        await cache.SetAsync(ProductCacheKeys.List, result, CacheDuration, cancellationToken);
        return result;
    }
}
