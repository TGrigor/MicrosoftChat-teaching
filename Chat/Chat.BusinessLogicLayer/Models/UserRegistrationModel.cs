using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogicLayer.Models
{
    public class UserRegistrationModel: UserModel
    {
        public string ConfPassword { get; set; }
    }
}
