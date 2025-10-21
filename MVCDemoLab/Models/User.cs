using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemoLab.Models
{
    [Table("TblUsers")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Must Enter UserName")]
        [StringLength(100, ErrorMessage = "Must Enter 100 Letters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Must Enter Password")]
        [StringLength(100, ErrorMessage = "Must Enter 100 Letters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Must Enter EmailAddress")]
        [StringLength(150, ErrorMessage = "Must Enter 150 Letters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
