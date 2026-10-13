namespace Boilerplate.Domain.Products;

public sealed class Product
{
    private Product()
    {
    }

    private Product(Guid id, string name, string? description, decimal price, DateTimeOffset createdAtUtc)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        Update(name, description, price);
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public decimal Price { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; private set; }

    public DateTimeOffset? UpdatedAtUtc { get; private set; }

    public static Product Create(
        string name,
        string? description,
        decimal price,
        DateTimeOffset createdAtUtc)
    {
        return new Product(Guid.NewGuid(), name, description, price, createdAtUtc);
    }

    public void Update(string name, string? description, decimal price, DateTimeOffset? updatedAtUtc = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegative(price);

        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Price = price;
        UpdatedAtUtc = updatedAtUtc;
    }
}
