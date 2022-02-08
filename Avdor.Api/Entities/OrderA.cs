using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

public class OrderA
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long ORD { get; set; }
    public double TOTPURCHASEPRICE { get; set; } = 0;
    public long QUANT { get; set; } = 0;
    public long TAX { get; set; } = -1;
    public double TOTQUANT { get; set; } = 1;
    public long STATUSDATE { get; set; } = (long)(DateTime.Now - DateTime.Parse("01/01/1988")).TotalDays * 1440;
    public string CHANGESTATFLAG { get; set; } = "Y";
    public long CURVERSION { get; set; } = 1;
}



