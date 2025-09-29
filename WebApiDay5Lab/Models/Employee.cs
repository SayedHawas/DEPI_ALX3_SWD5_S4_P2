using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDay5Lab.Models
{
    [Table("TblEmployees")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Must Enter Employee Name ")]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must Enter Job")]
        [MaxLength(100)]
        public string Job { get; set; }
        [Required]
        [Range(typeof(decimal), "0.00", "9999999.99")]
        [RegularExpression(@"^\d+.?\d{0,2}$")]
        public decimal Salary { get; set; }
        //[ForeignKey(nameof(Department))]
        [ForeignKey("Department")]
        public int departmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
