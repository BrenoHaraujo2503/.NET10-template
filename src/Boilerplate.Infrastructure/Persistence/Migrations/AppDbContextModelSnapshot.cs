using Boilerplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Persistence.Migrations;

[DbContext(typeof(AppDbContext))]
internal sealed class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "10.0.12")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        modelBuilder.Entity("Boilerplate.Domain.Products.Product", entity =>
        {
            entity.Property<Guid>("Id")
                .HasColumnType("uuid")
                .HasColumnName("id");

            entity.Property<DateTimeOffset>("CreatedAtUtc")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at_utc");

            entity.Property<string>("Description")
                .HasMaxLength(2000)
                .HasColumnType("character varying(2000)")
                .HasColumnName("description");

            entity.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)")
                .HasColumnName("name");

            entity.Property<decimal>("Price")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)")
                .HasColumnName("price");

            entity.Property<DateTimeOffset?>("UpdatedAtUtc")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at_utc");

            entity.HasKey("Id");
            entity.ToTable("products");
        });
#pragma warning restore 612, 618
    }
}
