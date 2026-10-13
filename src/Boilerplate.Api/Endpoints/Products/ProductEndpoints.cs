using Boilerplate.Api.Validation;
using Boilerplate.Application.Products;
using Boilerplate.Application.Products.CreateProduct;
using Boilerplate.Application.Products.DeleteProduct;
using Boilerplate.Application.Products.GetProductById;
using Boilerplate.Application.Products.GetProducts;
using Boilerplate.Application.Products.UpdateProduct;

namespace Boilerplate.Api.Endpoints.Products;

internal static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        RouteGroupBuilder group = endpoints
            .MapGroup("/api/v1/products")
            .WithTags("Products");

        group.MapGet("/", GetAllAsync)
            .WithName("GetProducts")
            .Produces<IReadOnlyList<ProductDto>>();

        group.MapGet("/{id:guid}", GetByIdAsync)
            .WithName("GetProductById")
            .Produces<ProductDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", CreateAsync)
            .WithName("CreateProduct")
            .AddEndpointFilter<ValidationFilter<CreateProductRequest>>()
            .Produces<ProductDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        group.MapPut("/{id:guid}", UpdateAsync)
            .WithName("UpdateProduct")
            .AddEndpointFilter<ValidationFilter<UpdateProductRequest>>()
            .Produces<ProductDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", DeleteAsync)
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> GetAllAsync(
        GetProductsHandler handler,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<ProductDto> products = await handler.HandleAsync(cancellationToken);
        return Results.Ok(products);
    }

    private static async Task<IResult> GetByIdAsync(
        Guid id,
        GetProductByIdHandler handler,
        CancellationToken cancellationToken)
    {
        ProductDto? product = await handler.HandleAsync(id, cancellationToken);
        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    private static async Task<IResult> CreateAsync(
        CreateProductRequest request,
        CreateProductHandler handler,
        CancellationToken cancellationToken)
    {
        ProductDto product = await handler.HandleAsync(request, cancellationToken);
        return Results.CreatedAtRoute("GetProductById", new { id = product.Id }, product);
    }

    private static async Task<IResult> UpdateAsync(
        Guid id,
        UpdateProductRequest request,
        UpdateProductHandler handler,
        CancellationToken cancellationToken)
    {
        ProductDto? product = await handler.HandleAsync(id, request, cancellationToken);
        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    private static async Task<IResult> DeleteAsync(
        Guid id,
        DeleteProductHandler handler,
        CancellationToken cancellationToken)
    {
        bool deleted = await handler.HandleAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
