using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class MessageEditViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int MessageId { get; set; }

        public int CopyId { get; set; }

        public string UserKey { get; set; }

        public int FriendId { get; set; }
    }
}
