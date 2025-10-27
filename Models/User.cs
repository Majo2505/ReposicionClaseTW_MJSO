using System.ComponentModel.DataAnnotations;

namespace EC4clase1.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; } 
        [Required]
        public string PasswordHash  { get; set; }
        public string Role { get; set; } = "User";
    }
}
