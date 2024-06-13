﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecomm.Domain.Entities;
using System.Reflection.Emit;

namespace Ecomm.Infraestructure.Persistences.Contexts.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("UserId");

            builder.Property(e => e.Email).IsUnicode(false);
            builder.Property(e => e.Image).IsUnicode(false);
            builder.Property(e => e.Password).IsUnicode(false);
            builder.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
