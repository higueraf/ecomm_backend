using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecomm.Domain.Entities;

namespace Ecomm.Infraestructure.Persistences.Contexts.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ProductPhotoId");
            builder.Property(e => e.Url).HasMaxLength(500);
        
        }
    }
}
