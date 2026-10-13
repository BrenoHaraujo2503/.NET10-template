using Boilerplate.Application.Products.CreateProduct;
using FluentAssertions;

namespace Boilerplate.UnitTests.Products;

public sealed class CreateProductValidatorTests
{
    private readonly CreateProductValidator _validator = new();

    [Fact]
    public async Task ValidateWithValidRequestShouldSucceed()
    {
        CreateProductRequest request = new("Keyboard", "Mechanical", 399.90m);

        FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateWithInvalidRequestShouldReturnExpectedErrors()
    {
        CreateProductRequest request = new(string.Empty, null, -1m);

        FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(request);

        result.Errors.Select(error => error.PropertyName)
            .Should()
            .Contain([nameof(request.Name), nameof(request.Price)]);
    }
}
