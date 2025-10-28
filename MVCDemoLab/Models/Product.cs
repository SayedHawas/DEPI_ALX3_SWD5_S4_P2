using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemoLab.Models
{

    public partial class Product
    {
        //POCO
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        [ForeignKey("Category")]
        public int CategotyId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
