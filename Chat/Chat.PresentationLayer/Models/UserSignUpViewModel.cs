using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.PresentationLayer.Models
{
    public class UserSignUpViewModel : UserSignInViewModel
    {
        public string ConfPassword { get; set; }
    }
}
