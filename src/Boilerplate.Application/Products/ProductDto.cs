using Boilerplate.Domain.Products;

namespace Boilerplate.Application.Products;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? UpdatedAtUtc)
{
    public static ProductDto FromDomain(Product product)
    {
        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CreatedAtUtc,
            product.UpdatedAtUtc);
    }
}
