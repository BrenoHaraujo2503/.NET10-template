using Boilerplate.Domain.Products;
using FluentAssertions;

namespace Boilerplate.UnitTests.Products;

public sealed class ProductTests
{
    [Fact]
    public void CreateShouldNormalizeValues()
    {
        DateTimeOffset now = new(2026, 10, 13, 12, 0, 0, TimeSpan.Zero);

        Product product = Product.Create("  Keyboard  ", "  Mechanical  ", 399.90m, now);

        product.Name.Should().Be("Keyboard");
        product.Description.Should().Be("Mechanical");
        product.Price.Should().Be(399.90m);
        product.CreatedAtUtc.Should().Be(now);
    }

    [Fact]
    public void CreateWithNegativePriceShouldThrow()
    {
        Action action = () => Product.Create("Keyboard", null, -1m, DateTimeOffset.UtcNow);

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}
