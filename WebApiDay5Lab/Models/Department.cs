using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDay5Lab.Models
{
    [Table("TblDepartments")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation Property
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
