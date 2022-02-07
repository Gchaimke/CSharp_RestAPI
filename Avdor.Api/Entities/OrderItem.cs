using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

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
    public long TBALANCE { get; set; } = 1000;
    public long TQUANT { get; set; } = 1000;
    public long TUNIT { get; set; } = -1;
    public double PRICE { get; set; }
    public long DUEDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public double QPRICE { get; set; } = 0; //same as PRICE
    public double PRICEBAL { get; set; } = 0; //same as PRICE
    public double VPRICE { get; set; } = 0; //Price+VAT
    public double QPROFIT { get; set; } = 0; //same as PRICE
    public string PRSOURCE { get; set; } = "M";
    public string VATFLAG { get; set; } = "Y";
    public string PURSOURCE { get; set; } = "A";
    public long CURRENCY { get; set; } = -1;
    public long ICURRENCY { get; set; } = -1;
    public long IEXCHANGE { get; set; } = 1;
    public long LINE { get; set; } = 1;
    public long PRDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public long KLINE { get; set; } = 1;

    [NotMapped]
    public double vat { get; set; } = 17;

    public void SetPrice(double vat)
    {
        this.QPRICE = this.PRICE * this.QUANT;
        this.PRICEBAL = this.QPRICE;
        this.QPROFIT = this.QPRICE;
        this.VPRICE = Math.Round((this.PRICE / (1 + vat / 100)), 2);
        var PQTTY = this.QUANT * 1000;
        this.QUANT = PQTTY;
        this.ABALANCE = PQTTY;
        this.PBALANCE = PQTTY;
        this.TBALANCE = PQTTY;
        this.TQUANT = PQTTY;
        this.LINE = this.KLINE;
    }
}



