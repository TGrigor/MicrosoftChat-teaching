using Chat.BusinessLogicLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogicLayer.Models
{
    public class ResponseResult <T>
    {
        public T Data { get; set; }
        public ResponseType Type { get; set; }
        public string Message { get; set; }
    }
}
