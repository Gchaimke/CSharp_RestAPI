using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Entities;

public class Order
{
    [Key]
    public long ORD { get; set; }
    public long CUST { get; set; } = 30378;
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
        this.VAT = totalPrice - this.DISPRICE;
        this.QPRICE = (long)(totalPrice - this.VAT);

    }

}

public class OrderItems
{
    [Key]
    public long ORDI { get; set; }
    [Required]
    public long ORD { get; set; }
    [Required]
    public long PART { get; set; } = 0;
    public long QUANT { get; set; } = 1 * 1000;
    public long DUEDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public long CUST { get; set; } = 30378;
    public double PRICE { get; set; } = 0;
    public double VAT { get; set; } = 0; //17% of PRICE
    public double VPRICE { get; set; } = 0; //Price+VAT
    public double QPRICE { get; set; } = 0; //same as PRICE
    public double PRICEBAL { get; set; } = 0; //same as PRICE
    public int CURRENCY { get; set; } = -1;
    public int LINE { get; set; } = 1;
    public long PRDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public int KLINE { get; set; } = 1;
}

public class OrderShipment
{
    [Key]
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


public class OrderClient
{
    [Key]
    public long IV { get; set; }
    public string TYPE { get; set; } = "O";
    public string CUSTDES { get; set; } = "";
    public string ADDRESS { get; set; } = "";
    public string ADDRESS2 { get; set; } = "";
    public string ADDRESS3 { get; set; } = "";
    public string STATE { get; set; } = "";
    public string STATEA { get; set; } = "";
    public string ZIP { get; set; } = "";
    public string PHONENUM { get; set; } = "";
    public string CELLPHONE { get; set; } = "";
    public string EMAIL { get; set; } = "";

}
