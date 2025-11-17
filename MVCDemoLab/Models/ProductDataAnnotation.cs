using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MVCDemoLab.Models
{
    [ModelMetadataType(typeof(ProductDataAnnotation))]
    public partial class Product
    {

    }

    public class ProductDataAnnotation
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Must Enter Name ....")]
        [MaxLength(150, ErrorMessage = "Must Enter Only 150 letters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must Enter price ....")]
        [Range(0.00, 99999.00)]
        //[CustomValidation(typeof(ValidatePriceAttribute), "ValidatePrice")]
        [Remote("CheckPrice", "FullProducts", ErrorMessage = "Price Must Less than 19999")]
        public decimal Price { get; set; }
        [MaxLength(300, ErrorMessage = "Must Enter Only 300 letters.")]
        public string? Description { get; set; }
        [MaxLength(255)]
        [DisplayName("Photo")]
        public string? ImagePath { get; set; }

    }
}
