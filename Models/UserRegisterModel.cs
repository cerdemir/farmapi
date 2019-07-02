using System.ComponentModel.DataAnnotations;

namespace farmapi.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}