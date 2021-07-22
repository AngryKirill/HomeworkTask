using HWTask.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }
        
        public int UserId { get; set; }

        public int FriendIndex { get; set; }
    }
}
