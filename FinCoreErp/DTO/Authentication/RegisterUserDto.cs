using System.ComponentModel.DataAnnotations;

namespace FinCoreErp.DTO.Authentication
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }
    }
}
