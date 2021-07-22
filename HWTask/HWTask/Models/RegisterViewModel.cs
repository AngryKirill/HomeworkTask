using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is not defined")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is not defined!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is invalid!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords are not match!")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}
