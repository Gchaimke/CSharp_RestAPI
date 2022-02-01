using Microsoft.EntityFrameworkCore;
using Avdor.Api.Entities;

namespace Avdor.Api.Repositories;

public class SqlDbRepository : DbContext
{
    public SqlDbRepository(DbContextOptions<SqlDbRepository> options) : base(options) { }

    public DbSet<Order> ORDERS { get; set; }

}