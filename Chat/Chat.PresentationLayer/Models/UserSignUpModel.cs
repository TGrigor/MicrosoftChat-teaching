﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.PresentationLayer.Models
{
    public class UserSignUpModel : UserSignInModel
    {
        public string ConfPassword { get; set; }
    }
}
