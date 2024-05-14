using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecomm.Domain.Entities;

namespace Ecomm.Infraestructure.Persistences.Contexts.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("PaymentMethodId");

            builder.Property(e => e.Name).HasMaxLength(100);

        }
    }
}
