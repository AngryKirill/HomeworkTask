using HWTask.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "Password is invalid!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is invalid!")]
        [Compare("Password", ErrorMessage = "Passwords are not match!")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}
