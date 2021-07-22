using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.CoreModels
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public User Sender { get; set; }

        public User Recipient { get; set; }

        public DateTime DateUpdated { get; set; }

        public int ChatId { get; set; }

        public int CopyId { get; set; }
    }
}
