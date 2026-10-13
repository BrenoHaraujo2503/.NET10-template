using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Products.UpdateProduct;

public sealed class UpdateProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ICacheService cache,
    TimeProvider timeProvider)
{
    public async Task<ProductDto?> HandleAsync(
        Guid id,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        Product? product = await repository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return null;
        }

        product.Update(request.Name, request.Description, request.Price, timeProvider.GetUtcNow());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.RemoveAsync(ProductCacheKeys.ById(id), cancellationToken);
        await cache.RemoveAsync(ProductCacheKeys.List, cancellationToken);

        return ProductDto.FromDomain(product);
    }
}
