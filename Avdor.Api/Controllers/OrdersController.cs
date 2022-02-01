using Microsoft.AspNetCore.Mvc;
using Avdor.Api.Dtos;
using Avdor.Api.Entities;
using Avdor.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Avdor.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly SqlDbRepository _db;
    private readonly ILogger<OrdersController> logger;
    public OrdersController(SqlDbRepository db, ILogger<OrdersController> logger)
    {
        this._db = db;
        this.logger = logger;
    }

    [HttpGet]
    public IEnumerable<Order> GetOrders()
    {
        IEnumerable<Order> orders = _db.ORDERS;
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {orders.Count()} orders");
        return orders;
    }

    [Route("filter")]
    public IEnumerable<Order> GetOrders(string? order = "", int? customer = 0)
    {
        IQueryable<Order> orders = _db.ORDERS;
        if (!string.IsNullOrWhiteSpace(order))
            orders = orders.Where(i => i.ORDNAME.Contains(order));
        if (customer != 0)
            orders = orders.Where(i => i.CUST == customer);
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {orders.Count()} orders");
        return orders;
    }

    [Route("last")]
    public Order GetLast()
    {
        Order order = _db.ORDERS.OrderBy(o => o.ORD).Last();
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {order} orders");
        return order;
    }

    [HttpGet("{ordname}")]
    public async Task<ActionResult<OrderDto>> GetOrderAsync(string ordname)
    {
        IQueryable<Order> orders = _db.ORDERS.Where(o => o.ORDNAME.Equals(ordname));
        if (orders is null)
        {
            return NotFound();
        }
        try
        {
            Order order = await orders.FirstAsync();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {order}");
            return order.AsOrderDto();
        }
        catch (System.InvalidOperationException)
        {
            return NotFound();
        }
    }

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderDto orderDto)
    {
        IQueryable<Order> orders = _db.ORDERS;
        Order last = orders.OrderBy(o => o.ORD).Last();
        int newOrd = Int32.Parse(last.ORDNAME.Substring(2));
        if (newOrd < 20000000)
        {
            DateTime date = DateTime.Now;
            string year = date.Year.ToString().Substring(2);
            newOrd = Int32.Parse(year + "000000");
        }
        Order order = new()
        {
            ORDNAME = "AS" + (newOrd + 1),
            CUST = orderDto.CUST,
            QPRICE = orderDto.QPRICE,
        };
        try
        {
            _db.ORDERS.Add(order);
            await _db.SaveChangesAsync();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Cerated {order}");
            return CreatedAtAction(nameof(GetOrderAsync), new { ordname = order.ORDNAME }, order.AsOrderDto());

        }
        catch (System.InvalidOperationException)
        {
            return NotFound();
        }

    }

    [Route("update/{ordname}")]
    [HttpPut("{ordname}")]
    public async Task<ActionResult> UpdateItemAsync(string ordname, UpdateOrderDto orderDto)
    {
        IQueryable<Order> orders = _db.ORDERS.Where(o => o.ORDNAME.Equals(ordname));
        try
        {
            Order order = await orders.FirstAsync();
            order.CUST = orderDto.CUST;
            order.QPRICE = orderDto.QPRICE;

            _db.ORDERS.Update(order);
            _db.SaveChanges();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Updated {order}");
            return CreatedAtAction(nameof(GetOrderAsync), new { ordname = order.ORDNAME }, order.AsOrderDto());
        }
        catch (System.InvalidOperationException)
        {
            return NotFound();
        }
    }

    [Route("delete/{ordname}")]
    [HttpDelete("{ordname}")]
    public async Task<ActionResult> DeleteOrderAsync(string ordname)
    {
        IQueryable<Order> orders = _db.ORDERS.Where(o => o.ORDNAME.Equals(ordname));
        try
        {
            Order order = await orders.FirstAsync();
            _db.ORDERS.Remove(order);
            _db.SaveChanges();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Deleted {order}");
            return Ok("Deleted");
        }
        catch (System.InvalidOperationException)
        {
            return NotFound();
        }

    }

}
