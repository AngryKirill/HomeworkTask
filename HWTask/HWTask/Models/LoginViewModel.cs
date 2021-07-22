using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is not defined!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not defined!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
