using Microsoft.AspNetCore.Mvc;
using Avdor.Api.Dtos;
using Avdor.Api.Entities;
using Avdor.Api.Repositories;

namespace Avdor.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly SqlDbRepository db;
    private readonly ILogger<OrdersController> logger;
    public OrdersController(SqlDbRepository db, ILogger<OrdersController> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    [HttpGet]
    public IEnumerable<Order> GetOrders()
    {
        IEnumerable<Order> orders = db.ORDERS;
        // logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {orders.Count()} orders");
        return orders;
    }

    [Route("filter")]
    public IEnumerable<Order> GetOrders(string? name = "", int? customer = 0)
    {
        IQueryable<Order> orders = db.ORDERS;
        if (!string.IsNullOrWhiteSpace(name))
            orders = orders.Where(i => i.ORDNAME.Contains(name));
        if (customer != 0)
            orders = orders.Where(i => i.CUST == customer);
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {orders.Count()} orders");
        return orders;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrderAsync(int id)
    {
        var order = await db.ORDERS.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {order}");
        return order.AsOrderDto();
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrderAsync(CreateOrderDto orderDto)
    {
        Order order = new()
        {
            ORDNAME = orderDto.ORDNAME,
            CUST = orderDto.CUST,
        };
        await db.ORDERS.AddAsync(order);
        await db.SaveChangesAsync();
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Cerated {order}");
        return CreatedAtAction(nameof(GetOrderAsync), new { id = order.ORD }, order.AsOrderDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(int id, UpdateOrderDto orderDto)
    {
        var order = await db.ORDERS.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        order.ORDNAME = orderDto.ORDNAME;
        order.CUST = orderDto.CUST;

        db.ORDERS.Update(order);
        await db.SaveChangesAsync();
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Updated {order}");
        return CreatedAtAction(nameof(GetOrderAsync), new { id = order.ORD }, order.AsOrderDto());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrderAsync(int id)
    {
        var order = await db.ORDERS.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }
        db.ORDERS.Remove(order);
        await db.SaveChangesAsync();
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Deleted {order}");
        return Ok("Deleted");
    }
}
