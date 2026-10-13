using Boilerplate.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id).HasColumnName("id");
        builder.Property(product => product.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(product => product.Description)
            .HasColumnName("description")
            .HasMaxLength(2_000);
        builder.Property(product => product.Price)
            .HasColumnName("price")
            .HasPrecision(18, 2);
        builder.Property(product => product.CreatedAtUtc).HasColumnName("created_at_utc");
        builder.Property(product => product.UpdatedAtUtc).HasColumnName("updated_at_utc");
    }
}
