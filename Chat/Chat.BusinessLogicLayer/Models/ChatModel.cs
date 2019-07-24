using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogicLayer.Models
{
    public class ChatModel
    {
        public string Text { get; set; }
        public int UserIdTo { get; set; }
    }
}
