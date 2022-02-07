using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avdor.Api.Entities;

public class OrderItemA
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long ORDI { get; set; }

}



