using System.ComponentModel.DataAnnotations;

namespace FinDashboard.API.Models.DTOs.UserDto
{
    public class AddUserDto
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string UserName { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Please provide a valid email address in the format: example@domain.com.")]
        public string Email { get; set; } = string.Empty;
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W]).{6,}$", ErrorMessage = "Password must be at least 6 characters long and include at least one letter, one number, and one special character.")]
        public string HashPassword { get; set; } = string.Empty;

    }
}
