using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccessLayer.Dto
{
    public class ChatDto
    {
        public string Text { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
    }
}
