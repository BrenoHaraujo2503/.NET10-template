using FluentValidation;

namespace Boilerplate.Application.Products.CreateProduct;

public sealed class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(request => request.Description)
            .MaximumLength(2_000);

        RuleFor(request => request.Price)
            .GreaterThanOrEqualTo(0);
    }
}
