using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWTask.CoreModels
{
    public class Chat
    {
        public int Id { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
    }
}
