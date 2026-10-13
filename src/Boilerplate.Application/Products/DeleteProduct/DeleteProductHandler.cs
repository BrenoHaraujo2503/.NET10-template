using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Products.DeleteProduct;

public sealed class DeleteProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ICacheService cache)
{
    public async Task<bool> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Product? product = await repository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            return false;
        }

        repository.Remove(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.RemoveAsync(ProductCacheKeys.ById(id), cancellationToken);
        await cache.RemoveAsync(ProductCacheKeys.List, cancellationToken);
        return true;
    }
}
