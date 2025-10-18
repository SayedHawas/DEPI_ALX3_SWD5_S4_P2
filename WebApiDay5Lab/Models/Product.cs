using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDay5Lab.Models
{
    [Table("TblProducts")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Must Enter Name ....")]
        [MaxLength(150, ErrorMessage = "Must Enter Only 150 letters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must Enter price ....")]
        [Range(0.00, 99999.00)]
        public decimal Price { get; set; }
        [MaxLength(300, ErrorMessage = "Must Enter Only 300 letters.")]
        public string? Description { get; set; }
        [NotMapped]
        public byte[] Image { get; set; }
        [MaxLength(255)]
        public string? ImagePath { get; set; }
        [ForeignKey("Category")]
        public int CategotyId { get; set; }
        // Navigation Properties
        public virtual Category Category { get; set; }
    }
}
