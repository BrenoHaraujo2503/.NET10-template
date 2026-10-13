namespace Boilerplate.Application.Products;

internal static class ProductCacheKeys
{
    public const string List = "products:all";

    public static string ById(Guid id) => $"products:{id}";
}
