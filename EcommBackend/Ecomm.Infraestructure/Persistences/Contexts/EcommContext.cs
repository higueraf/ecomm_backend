using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ecomm.Domain.Entities;

namespace Ecomm.Infraestructure.Persistences.Contexts;

public partial class EcommContext : DbContext
{
    public EcommContext()
    {
    }

    public EcommContext(DbContextOptions<EcommContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Category> Category { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
    public virtual DbSet<Order> Order { get; set; }
    public virtual DbSet<OrderDetail> OrderDetail { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<ProductPhoto> ProductPhoto { get; set; }
    public virtual DbSet<User> User { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
