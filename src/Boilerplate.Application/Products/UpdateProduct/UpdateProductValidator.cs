using FluentValidation;

namespace Boilerplate.Application.Products.UpdateProduct;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
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
