using Microsoft.EntityFrameworkCore;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public class SqlDbRepository : DbContext
{
    private readonly SqlDbRepository _db;
    private readonly DbSet<Item> itemsCollection;
    public SqlDbRepository(DbContextOptions<SqlDbRepository> options) : base(options) {}

    public DbSet<Item> Items { get; set; }
    public DbSet<Order> ORDERS { get; set; }

}