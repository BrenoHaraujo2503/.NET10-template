using Boilerplate.Application.Products.CreateProduct;
using Boilerplate.Application.Products.DeleteProduct;
using Boilerplate.Application.Products.GetProductById;
using Boilerplate.Application.Products.GetProducts;
using Boilerplate.Application.Products.UpdateProduct;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
        services.AddSingleton(TimeProvider.System);

        services.AddScoped<CreateProductHandler>();
        services.AddScoped<GetProductsHandler>();
        services.AddScoped<GetProductByIdHandler>();
        services.AddScoped<UpdateProductHandler>();
        services.AddScoped<DeleteProductHandler>();

        return services;
    }
}
