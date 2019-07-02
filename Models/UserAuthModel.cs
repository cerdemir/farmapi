using System.ComponentModel.DataAnnotations;

namespace farmapi.Models
{
    public class UserAuthModel
    {
        /// <summary>
        /// username
        /// </summary>
        /// <value></value>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// password
        /// </summary>
        /// <value></value>
        [Required]
        public string Password { get; set; }
    }
}