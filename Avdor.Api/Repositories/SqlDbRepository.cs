using Microsoft.EntityFrameworkCore;
using Avdor.Api.Entities;

namespace Avdor.Api.Repositories;

public class SqlDbRepository : DbContext
{
    public SqlDbRepository(DbContextOptions<SqlDbRepository> options) : base(options) { }

    public DbSet<Order>? ORDERS { get; set; }
    public DbSet<OrderItem>? ORDERITEMS { get; set; }
    public DbSet<OrderItemA>? ORDERITEMSA { get; set; }
    public DbSet<OrderCustomer>? NSCUST { get; set; }
    public DbSet<OrderShipment>? SHIPTO { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable(nameof(ORDERS))
            .HasMany(c => c.items);
        modelBuilder.Entity<OrderItem>().ToTable(nameof(ORDERITEMS));
        modelBuilder.Entity<OrderShipment>().ToTable(nameof(SHIPTO));
    }
}