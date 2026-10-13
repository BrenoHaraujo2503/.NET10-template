using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Persistence.Repositories;

internal sealed class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Products.SingleOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        return dbContext.Products.AddAsync(product, cancellationToken).AsTask();
    }

    public void Remove(Product product)
    {
        dbContext.Products.Remove(product);
    }
}
