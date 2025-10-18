using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDay5Lab.Models
{
    [Table("TblCustomers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Must Enter FirstName ....")]
        [MaxLength(100, ErrorMessage = "Must Enter Only 100 letters.")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Must Enter Email ....")]
        [MaxLength(200, ErrorMessage = "Must Enter Only 200 letters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool IsActive { get; set; }
        // Navigation Properties
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
