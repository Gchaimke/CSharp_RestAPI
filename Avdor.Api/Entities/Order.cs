using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

public class Order
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public long ORD { get; set; }
    public long CUST { get; set; } = 30378;

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

[Table("ORDERITEMS")]
public class OrderItem
{
    [Key]
    public long ORDI { get; set; }
    public long ORD { get; set; }
    public long PART { get; set; }
    public long QUANT { get; set; }
    public long ABALANCE { get; set; }
    public long PBALANCE { get; set; }
    public double PRICE { get; set; }
    public long DUEDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public double QPRICE { get; set; } = 0; //same as PRICE
    public double PRICEBAL { get; set; } = 0; //same as PRICE
    public double VPRICE { get; set; } = 0; //Price+VAT
    public string PRSOURCE { get; set; } = "M";
    public long CURRENCY { get; set; } = -1;
    public long LINE { get; set; } = 1;
    public long PRDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public long KLINE { get; set; } = 1;

    [NotMapped]
    public double vat { get; set; } = 17;

    public void SetPrice(double vat)
    {
        this.QPRICE = this.PRICE * this.QUANT;
        this.PRICEBAL = this.QPRICE;
        this.VPRICE = Math.Round((this.PRICE / (1 + vat / 100)), 2);
        this.QUANT = this.ABALANCE = this.PBALANCE = this.QUANT * 1000;
        this.LINE = this.KLINE;
    }
}


public class OrderCustomer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long IV { get; set; }
    public string TYPE { get; set; } = "O";
    public string CUSTDES { get; set; } = "";
    public string ADDRESS { get; set; } = "";
    public string ADDRESS2 { get; set; } = "";
    public string ADDRESS3 { get; set; } = "";
    public string STATE { get; set; } = "";
    public string STATEA { get; set; } = "";
    public string ZIP { get; set; } = "";
    public string PHONE { get; set; } = "";
    public string EMAIL { get; set; } = "";

}

public class OrderShipment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long IV { get; set; }
    public string TYPE { get; set; } = "O";
    public string CUSTDES { get; set; } = "";
    public string NAME { get; set; } = "";
    public string ADDRESS { get; set; } = "";
    public string STATE { get; set; } = "";
    public string ZIP { get; set; } = "";
    public string PHONENUM { get; set; } = "";
    public string CELLPHONE { get; set; } = "";
    public string EMAIL { get; set; } = "";

}



