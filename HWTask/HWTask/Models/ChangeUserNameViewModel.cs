using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class ChangeUserNameViewModel
    {
        [Required(ErrorMessage = "Username is not defined")]
        public string UserName { get; set; }
    }
}
