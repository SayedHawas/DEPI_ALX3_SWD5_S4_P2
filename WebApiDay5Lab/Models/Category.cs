using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDay5Lab.Models
{
    [Table("TblCategories")]
    public class Category
    {
        [Key]
        public int CategotyId { get; set; }
        [Required(ErrorMessage = "Must Enter Name ....")]
        [MaxLength(150, ErrorMessage = "Must Enter Only 150 letters.")]
        public string Name { get; set; }
        [MaxLength(300, ErrorMessage = "Must Enter Only 300 letters.")]
        [DisplayName("Notes")]
        public string? Description { get; set; }
        //Nagivation 
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
