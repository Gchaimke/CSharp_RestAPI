using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Entities;

public class Order
{
    [Key]
    public long ORD { get; set; }
    public long CUST { get; set; } = 1539;

    [Required]
    public string ORDNAME { get; set; } = "AS21000001";
    public long CURDATE { get; set; } = (long)DateTime.Now.ToOADate();
    public string REFERENCE { get; set; } = "";
    public double QPRICE { get; set; } = 0;
    public double VAT { get; set; } = 0;
    public double TOTPRICE { get; set; } = 0;
    public int CURRENCY { get; set; } = -1;
    public double DISPRICE { get; set; } = 0;
    public long AGENT { get; set; } = 2;
    public int ADJPRICEFLAG { get; set; } = 3;
    public int DOER { get; set; } = 1;
    public int ORDSTATUS { get; set; } = -1;
    public long PAY { get; set; } = 0;
    public OrderItems ORDERITEMS { get; set; }
    public ShipTo SHIPTO { get; set; }
}

public class OrderItems
{
    [Key]
    public long ORDI { get; set; }
    [Required]
    public long ORD { get; set; }
    [Required]
    public long PART { get; set; } = 0;
    public long QUANT { get; set; } = 1;
    public long DUEDATE { get; set; } = (long)DateTime.Now.ToOADate();
    public long CUST { get; set; } = 0;
    public double PRICE { get; set; } = 0;
    public double VPRICE { get; set; } = 0;
    public double QRICE { get; set; } = 0;
    public int CURRENCY { get; set; } = -1;
    public double VAT { get; set; } = 0;
    public int LINE { get; set; } = 1;
    public long PRDATE { get; set; } = (long)DateTime.Now.ToOADate();
    public int KLINE { get; set; } = 1;
}

public class ShipTo
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

