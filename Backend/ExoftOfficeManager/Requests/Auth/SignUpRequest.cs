using System.ComponentModel.DataAnnotations;

using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Requests.Auth
{
    public class SignUpRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords should match.")]
        public string PasswordConfirm { get; set; }

        [Required]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
    }
}
