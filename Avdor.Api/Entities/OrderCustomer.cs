using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

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



