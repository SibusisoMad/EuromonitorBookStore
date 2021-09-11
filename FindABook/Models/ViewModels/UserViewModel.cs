using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindABook.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        [Display(Description ="When set to true, requires the username to match the email address or mobile number")]
        public bool UsernameFromEmailOrMobile { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
