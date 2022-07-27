using System.ComponentModel.DataAnnotations;

namespace UserWebApplication.Model
{
    public class UserLogin
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

    }
}
