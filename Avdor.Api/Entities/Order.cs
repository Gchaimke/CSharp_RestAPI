using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

public class Order
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public long ORD { get; set; }
    public long CUST { get; set; } = 1539;

    [Display(Name = "Order Name")]
    public string ORDNAME { get; set; } = "SO21000001";
    public long CURDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public long UDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public string REFERENCE { get; set; } = "";
    public double TOTPRICE { get; set; } = 0;
    public double QPRICE { get; set; } = 0;
    public double DISPRICE { get; set; } = 0;
    public double VAT { get; set; } = 0;
    public long CURRENCY { get; set; } = -1;
    public long AGENT { get; set; } = 2;
    public string ADJPRICEFLAG { get; set; } = "3";
    public long DOER { get; set; } = 1;
    public long ORDSTATUS { get; set; } = -1;
    public long PAY { get; set; } = 24;

    public void SetPrice(double totalPrice, double vat)
    {
        this.DISPRICE = Math.Round((totalPrice / (1 + vat / 100)), 2);
        this.VAT = Math.Round((totalPrice - this.DISPRICE), 2);
        this.QPRICE = (long)(totalPrice - this.VAT);
    }

    [ForeignKey("ORD")]
    [DisplayFormat(NullDisplayText = "No items")]
    public IEnumerable<OrderItem> items { get; set; } = new List<OrderItem>();

    [ForeignKey("ORD")]
    [DisplayFormat(NullDisplayText = "No castomer")]
    public OrderCustomer customer { get; set; }

    [ForeignKey("ORD")]
    [DisplayFormat(NullDisplayText = "No shipment")]
    public OrderShipment shipment { get; set; }
}



