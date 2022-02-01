using System.ComponentModel.DataAnnotations;

namespace Avdor.Api.Entities;

public class Order
{
    [Key]
    public long ORD { get; set; }

    [Required]
    public long CUST { get; set; } = 0;
    public string ORDNAME { get; set; } = "AS25000001";
    public long CURDATE { get; set; } = 17798400;
    public double QPRICE { get; set; } = 0;
    public double VAT { get; set; } = 0;
    public double TOTPRICE { get; set; } = 0;
    public long CURRENCY { get; set; } = -1;
    public double DISPRICE { get; set; } = 0;
    public long PAY { get; set; } = 0;
}
