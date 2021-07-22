using HWTask.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.Models
{
    public class ChatViewModel
    {
        public int Id { get; set; }

        public int FriendChatId { get; set; }

        public int FriendId { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public Message NewMessage { get; set; }

        public string UserKey { get; set; }
    }
}
