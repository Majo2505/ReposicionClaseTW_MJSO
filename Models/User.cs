using System.ComponentModel.DataAnnotations;

namespace EC4clase1.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash  { get; set; }

    }
}
