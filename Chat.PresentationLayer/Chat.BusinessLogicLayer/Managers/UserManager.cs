using System.Collections.Generic;
using System.Linq;
using Chat.BusinessLogicLayer.Models;

namespace Chat.BusinessLogicLayer.Managers
{
    public class UserManager
    {
        List<UserModel> users =new List<UserModel>();

        public UserManager()
        {
            users.Add(new UserModel()
            {
                UserName = "User1",
                Password = "cbe0cd68cbca3868250c0ba545c48032f43eb0e8a5e6bab603d109251486f77a91e46a3146d887e37416c6bdb6cbe701bd514de778573c9b0068483c1c626aec"
            });
            users.Add(new UserModel()
            {
                UserName = "User2",
                Password = "Password2"
            });
        }

        public bool Login(UserModel user)
        {
            var userbla = users.FirstOrDefault(s => s.UserName == user.UserName && s.Password == user.Password);
            if (userbla != null)
            {
                return true;
            }
            return false;
        }
    }
}
