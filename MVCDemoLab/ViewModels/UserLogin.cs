using System.ComponentModel.DataAnnotations;

namespace MVCDemoLab.ViewModels
{
    public class UserLogin
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Must Enter UserName")]
        [StringLength(100, ErrorMessage = "Must Enter 100 Letters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Must Enter Password")]
        [StringLength(100, ErrorMessage = "Must Enter 100 Letters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
