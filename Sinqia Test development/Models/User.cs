using System.ComponentModel.DataAnnotations;

namespace Sinqia_Test_development.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
