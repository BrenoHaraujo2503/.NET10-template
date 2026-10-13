using Boilerplate.Application.Abstractions.Caching;
using Boilerplate.Application.Abstractions.Persistence;
using Boilerplate.Application.Products;
using Boilerplate.Application.Products.CreateProduct;
using Boilerplate.Domain.Products;
using FluentAssertions;
using NSubstitute;

namespace Boilerplate.UnitTests.Products;

public sealed class CreateProductHandlerTests
{
    [Fact]
    public async Task HandleShouldPersistAndInvalidateListCache()
    {
        IProductRepository repository = Substitute.For<IProductRepository>();
        IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
        ICacheService cache = Substitute.For<ICacheService>();
        DateTimeOffset now = new(2026, 10, 13, 12, 0, 0, TimeSpan.Zero);
        CreateProductHandler handler = new(repository, unitOfWork, cache, new FixedTimeProvider(now));
        CreateProductRequest request = new("Keyboard", "Mechanical", 399.90m);

        ProductDto result = await handler.HandleAsync(request);

        result.Name.Should().Be("Keyboard");
        result.CreatedAtUtc.Should().Be(now);
        await repository.Received(1).AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        await unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await cache.Received(1).RemoveAsync("products:all", Arg.Any<CancellationToken>());
    }

    private sealed class FixedTimeProvider(DateTimeOffset value) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => value;
    }
}
