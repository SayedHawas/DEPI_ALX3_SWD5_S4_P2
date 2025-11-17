using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityWithMvcDemo.Models
{
    public class ApplicationUsers : IdentityUser
    {
        [MaxLength(200)]
        public string? City { get; set; }
    }
}
