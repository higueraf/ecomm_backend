using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecomm.Domain.Entities;

namespace Ecomm.Infraestructure.Persistences.Contexts.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("CategoryId")
                .HasConversion<Guid>();

            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
