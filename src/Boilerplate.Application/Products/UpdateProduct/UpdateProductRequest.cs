namespace Boilerplate.Application.Products.UpdateProduct;

public sealed record UpdateProductRequest(string Name, string? Description, decimal Price);
