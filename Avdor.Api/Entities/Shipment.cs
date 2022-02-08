using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

public class Shipment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long IV { get; set; }
    public string TYPE { get; set; } = "O";
    public string CUSTDES { get; set; } = "";
    public string NAME { get; set; } = "Internet";
    public string ADDRESS { get; set; } = "";
    public string STATE { get; set; } = "";
    public string ZIP { get; set; } = "";
    public string PHONENUM { get; set; } = "";
    public string CELLPHONE { get; set; } = "";
    public string EMAIL { get; set; } = "";

}



