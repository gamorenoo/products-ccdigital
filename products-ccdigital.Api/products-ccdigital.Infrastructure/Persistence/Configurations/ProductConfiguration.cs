using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using products_ccdigital.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace products_ccdigital.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey("Id");

            builder.Property(e => e.Id)
                .HasColumnType("uniqueidentifier");

            builder.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .IsUnicode(false);

            builder.Property(e => e.Stock)
                .HasColumnType("int")
                .IsUnicode(false);

        }
    }
}
