using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Display Order")]
        [Range(1, 1000, ErrorMessage = "Range between 1 and 1000")]
        public int DisplayOrder { get; set; } = 1;
        public DateTime CreateDateTime { get; set; } = DateTime.UtcNow;

    }
}