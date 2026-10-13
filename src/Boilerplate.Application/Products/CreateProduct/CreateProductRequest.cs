namespace Boilerplate.Application.Products.CreateProduct;

public sealed record CreateProductRequest(string Name, string? Description, decimal Price);
