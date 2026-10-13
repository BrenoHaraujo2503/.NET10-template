using FluentValidation;

namespace Boilerplate.Api.Validation;

internal sealed class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        T request = context.Arguments.OfType<T>().Single();
        FluentValidation.Results.ValidationResult result =
            await validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (result.IsValid)
        {
            return await next(context);
        }

        Dictionary<string, string[]> errors = result.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.ErrorMessage).Distinct().ToArray());

        return Results.ValidationProblem(errors);
    }
}
