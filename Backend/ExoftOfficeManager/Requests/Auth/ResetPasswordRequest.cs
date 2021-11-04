﻿using System.ComponentModel.DataAnnotations;

namespace ExoftOfficeManager.Requests.Auth
{
    public class ResetPasswordRequest
    {
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

        public string Token { get; set; }
    }
}
