using HWTask.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class FriendsViewModel
    {
        public Dictionary<User, bool> ConfirmsFriends { get; set; }
    }
}
