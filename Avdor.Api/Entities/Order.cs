using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Entities;

public class Order
{
    [Key]
    public long ORD { get; set; }
    public long CUST { get; set; } = 0;
    [Required]
    public string ORDNAME { get; set; } = "AS2500000";
    public long CURDATE { get; set; } = DateTime.UtcNow.Ticks;
    public double QPRICE { get; set; }
    public double VAT { get; set; }
    public double TOTPRICE { get; set; }
    public long CURRENCY { get; set; } = -1;
    public double DISPRICE { get; set; }
    public long PAY { get; set; } = 8;
}
