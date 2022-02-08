using Microsoft.AspNetCore.Mvc;
using Avdor.Api.Dtos;
using Avdor.Api.Entities;
using Avdor.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Avdor.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly SqlDbRepository _db;
    private readonly ILogger<OrdersController> logger;
    private readonly IConfiguration configuration;
    public OrdersController(SqlDbRepository db, ILogger<OrdersController> logger, IConfiguration configuration)
    {
        this._db = db;
        this.logger = logger;
        this.configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<Order> Orders()
    {
        IEnumerable<Order>? orders = _db.ORDERS;
        if (orders != null)
        {
            foreach (Order order in orders)
            {
                if (_db.NSCUST != null)
                {
                    Customer? customer = _db.NSCUST.Where(o => o.IV == order.ORD).FirstOrDefault();
                    // order.customer = customer ??= new Customer();
                }
                if (_db.ORDERITEMS != null)
                {
                    IEnumerable<OrderItem> items = _db.ORDERITEMS.Where(o => o.ORD == order.ORD).ToList();
                    // order.items = items;
                }
                if (_db.SHIPTO != null)
                {
                    Shipment? shipment = _db.SHIPTO.Where(o => o.IV == order.ORD).FirstOrDefault();
                    // order.shipment = shipment ??= new Shipment();
                }
            }
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {orders.ToList()} orders");
            return orders;
        }
        return new List<Order>();
    }

    [Route("filter")]
    public IEnumerable<Order> GetOrders(string? order = "", int? customer = 0)
    {
        if (_db.ORDERS != null)
        {
            IQueryable<Order> orders = _db.ORDERS;
            if (!string.IsNullOrWhiteSpace(order))
                orders = orders.Where(i => i.ORDNAME.Contains(order));
            if (customer != 0)
                orders = orders.Where(i => i.CUST == customer);

            foreach (Order cOrder in orders)
            {
                if (_db.NSCUST != null)
                {
                    Customer? cCustomer = _db.NSCUST.Where(o => o.IV == cOrder.ORD).FirstOrDefault();
                    cOrder.customer = cCustomer ??= new Customer();
                }
                if (_db.ORDERITEMS != null)
                {
                    IEnumerable<OrderItem> items = _db.ORDERITEMS.Where(o => o.ORD == cOrder.ORD).ToList();
                    cOrder.items = items;
                }
                if (_db.SHIPTO != null)
                {
                    Shipment? shipment = _db.SHIPTO.Where(o => o.IV == cOrder.ORD).FirstOrDefault();
                    cOrder.shipment = shipment ??= new Shipment();
                }
            }
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {orders.Count()} orders");
            return (IEnumerable<Order>)orders;
        }
        return new List<Order>();
    }

    [Route("last")]
    public Order GetLast()
    {
        if (_db.ORDERS != null)
        {
            Order order = _db.ORDERS.OrderBy(o => o.ORD).Last();
            if (_db.NSCUST != null)
            {
                Customer? cCustomer = _db.NSCUST.Where(o => o.IV == order.ORD).FirstOrDefault();
                order.customer = cCustomer ??= new Customer();
            }
            if (_db.ORDERITEMS != null)
            {
                IEnumerable<OrderItem> items = _db.ORDERITEMS.Where(o => o.ORD == order.ORD).ToList();
                order.items = items;
            }
            if (_db.SHIPTO != null)
            {
                Shipment? shipment = _db.SHIPTO.Where(o => o.IV == order.ORD).FirstOrDefault();
                order.shipment = shipment ??= new Shipment();
            }
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {order} orders");
            return order;
        }
        return new Order();
    }

    [HttpGet("{ordname}")]
    public async Task<ActionResult<OrderDto>> OrderAsync(string ordname)
    {
        if (_db.ORDERS != null)
        {
            IQueryable<Order> orders = _db.ORDERS.Where(o => o.ORDNAME.Equals(ordname));
            if (orders is null)
            {
                return NotFound();
            }
            try
            {
                if (orders != null)
                {
                    Order order = (Order)await orders.FirstAsync();
                    if (_db.NSCUST != null)
                    {
                        Customer? cCustomer = _db.NSCUST.Where(o => o.IV == order.ORD).FirstOrDefault();
                        order.customer = cCustomer ??= new Customer();
                    }
                    if (_db.ORDERITEMS != null)
                    {
                        IEnumerable<OrderItem> items = _db.ORDERITEMS.Where(o => o.ORD == order.ORD).ToList();
                        order.items = items;
                    }
                    if (_db.SHIPTO != null)
                    {
                        Shipment? shipment = _db.SHIPTO.Where(o => o.IV == order.ORD).FirstOrDefault();
                        order.shipment = shipment ??= new Shipment();
                    }
                    logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {order}");
                    return order.AsOrderDto();
                }
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        return NotFound();
    }

    [Route("create")]
    [HttpPost]
    public async Task<ActionResult> CreateOrder(CreateOrderDto orderDto)
    {

        long client = configuration.GetValue<long>("DefaultClient");
        long agent = configuration.GetValue<long>("DefaultAgent");
        if (client == 0)
            client = 1539;
        if (agent == 0)
            agent = 2;

        if (_db.ORDERS != null)
        {
            IQueryable<Order> old_orders = _db.ORDERS;
            Order last = old_orders.OrderBy(o => o.ORD).Last();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Last order {last.ORDNAME}");
            long newOrderID = last.ORD + 1;
            int newOrderName = Int32.Parse(last.ORDNAME.Substring(2));
            if (newOrderName < 20000000)
            {
                DateTime date = DateTime.Now;
                string year = date.Year.ToString().Substring(2);
                newOrderName = Int32.Parse(year + "000000");
            }
            Order order = new Order
            {
                ORDNAME = "SO" + (newOrderName + 1),
                REFERENCE = orderDto.REFERENCE,
                TOTPRICE = orderDto.TOTPRICE,
                CUST = client,
                AGENT = agent
            };


            order.SetPrice(orderDto.TOTPRICE, orderDto.vat);
            await _db.AddAsync(order);
            _db.SaveChanges();

            var customer = new Customer
            {
                IV = newOrderID,
                CUSTDES = orderDto.customer.CUSTDES,
                PHONE = orderDto.customer.PHONE,
                ADDRESS = orderDto.customer.ADDRESS,
                STATE = orderDto.customer.STATE,
                EMAIL = orderDto.customer.EMAIL,


            };

            await this.CreateCustomer(customer.AsCustomerDto());

            var shipment = new Shipment
            {
                IV = newOrderID,
                CUSTDES = orderDto.shipment.CUSTDES,
                CELLPHONE = orderDto.shipment.CELLPHONE,
                ADDRESS = orderDto.shipment.ADDRESS,
                EMAIL = orderDto.shipment.EMAIL,
                NAME = orderDto.shipment.NAME,
                STATE = orderDto.shipment.STATE
            };
            await this.CreateShipment(shipment.AsShipmentDto());
            long quant = 0;
            long items_count = 1;
            foreach (var item in orderDto.items)
            {
                OrderItem new_item = new OrderItem
                {
                    ORD = newOrderID,
                    PART = item.PART,
                    QUANT = item.QUANT,
                    PRICE = item.PRICE,
                    KLINE = items_count,
                };
                quant += new_item.QUANT;
                items_count++;
                await this.CreateItem(new_item.AsItemDto());

            }

            order.TOTQUANT = quant;
            await CreateOrderA(order);

            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: New order number {order.ORDNAME} qprice={order.TOTPRICE} qprice={order.QPRICE} vat={order.VAT}");
            return CreatedAtAction(nameof(OrderAsync), new { ordname = order.ORDNAME }, order);

        }
        return NotFound();
    }

    [NonAction]
    public async Task<ActionResult> CreateCustomer(CustomerDto customer)
    {
        Customer new_customer = new Customer
        {
            IV = customer.IV,
            CUSTDES = customer.CUSTDES,
            PHONE = customer.PHONE,
            ADDRESS = customer.ADDRESS,
            STATE = customer.STATE,
            EMAIL = customer.EMAIL
        };
        new_customer = this.ReverseEnglishString(new_customer);

        await _db.AddAsync(new_customer);
        _db.SaveChanges();
        return Ok($"Added {customer.CUSTDES}");
    }

    [NonAction]
    public async Task<ActionResult> CreateShipment(ShipmentDto shipment)
    {

        Shipment new_shipment = new Shipment
        {
            IV = shipment.IV,
            CUSTDES = shipment.CUSTDES,
            NAME = shipment.NAME,
            CELLPHONE = shipment.CELLPHONE,
            ADDRESS = shipment.ADDRESS,
            STATE = shipment.STATE,
            EMAIL = shipment.EMAIL
        };
        new_shipment = this.ReverseEnglishString(new_shipment);
        await _db.AddAsync(new_shipment);
        _db.SaveChanges();
        return Ok($"Added {new_shipment.CUSTDES}");
    }

    [NonAction]
    public async Task<ActionResult> CreateItem(ItemDto item)
    {
        OrderItem new_item = new OrderItem
        {
            ORD = item.ORD,
            PART = item.PART,
            QUANT = item.QUANT,
            PRICE = item.PRICE,
            KLINE = item.KLINE
        };
        new_item.SetPrice(item.vat);
        await _db.AddAsync(new_item);
        _db.SaveChanges();
        await this.CreateItemA(new_item);
        return Ok($"Added {new_item.PART}");
    }

    [NonAction]
    public async Task<ActionResult> CreateItemA(OrderItem item)
    {
        OrderItemA orderItema = new OrderItemA
        {
            ORDI = item.ORDI
        };
        await _db.AddAsync(orderItema);
        _db.SaveChanges();

        return Ok($"Added itemA {item.PART}");
    }


    [NonAction]
    public async Task<ActionResult> CreateOrderA(Order order)
    {
        OrderA orderA = new OrderA
        {
            ORD = order.ORD,
            TOTPURCHASEPRICE = order.TOTPRICE,
            TOTQUANT = order.TOTQUANT
        };
        await _db.AddAsync(orderA);
        _db.SaveChanges();

        return Ok($"Added new OrdersA record {order.ORD}");
    }


    [Route("update/{ordname}")]
    [HttpPut("{ordname}")]
    public async Task<ActionResult> UpdateItemAsync(string ordname, OrderDto orderDto)
    {
        if (_db.ORDERS != null)
        {
            IQueryable<Order> orders = _db.ORDERS.Where(o => o.ORDNAME.Equals(ordname));
            try
            {
                Order order = await orders.FirstAsync();
                order.TOTPRICE = orderDto.TOTPRICE;

                _db.ORDERS.Update(order);
                _db.SaveChanges();
                logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Updated {order}");
                return CreatedAtAction(nameof(OrderAsync), new { ordname = order.ORDNAME }, order);
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }
        return NotFound();
    }

    [Route("delete/{ordname}")]
    [HttpDelete("{ordname}")]
    public async Task<ActionResult> DeleteOrderAsync(string ordname)
    {
        if (_db.ORDERS != null)
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
        return NotFound();
    }

    [NonAction]
    public T ReverseEnglishString<T>(T obj)
    {

        foreach (PropertyInfo prop in obj.GetType().GetProperties())
        {
            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (type != null && type == typeof(string))
            {
                string c_prop = prop.GetValue(obj, null).ToString();
                if (c_prop != null && c_prop != "")
                {
                    if (Regex.IsMatch(c_prop, @"^[0-9\s-]*$"))
                    {
                        continue;
                    }
                    if (Regex.IsMatch(c_prop, @"^[a-zA-Z0-9\W|_]*$"))
                    {
                        char[] charArray = c_prop.ToCharArray();
                        Array.Reverse(charArray);
                        prop.SetValue(obj, new string(charArray));
                    }
                }
            }
        }

        return obj;
    }

}
