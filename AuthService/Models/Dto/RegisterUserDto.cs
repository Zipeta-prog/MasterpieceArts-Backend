using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Dto
{
    public class RegisterUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Image { get; set; } = "https://res.cloudinary.com/du1zkniut/image/upload/v1705995594/dvhjhdztb1j3i6jstwzs.png";

        public string? Role { get; set; } = "User";
    }
}
