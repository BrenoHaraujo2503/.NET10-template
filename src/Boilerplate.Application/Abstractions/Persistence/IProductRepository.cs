using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Abstractions.Persistence;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> ListAsync(CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    void Remove(Product product);
}
