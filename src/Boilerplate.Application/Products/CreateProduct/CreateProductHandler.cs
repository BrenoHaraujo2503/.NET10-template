using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Products.CreateProduct;

public sealed class CreateProductHandler(
    IProductRepository repository,
    IUnitOfWork unitOfWork,
    ICacheService cache,
    TimeProvider timeProvider)
{
    public async Task<ProductDto> HandleAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        Product product = Product.Create(
            request.Name,
            request.Description,
            request.Price,
            timeProvider.GetUtcNow());

        await repository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cache.RemoveAsync(ProductCacheKeys.List, cancellationToken);

        return ProductDto.FromDomain(product);
    }
}
