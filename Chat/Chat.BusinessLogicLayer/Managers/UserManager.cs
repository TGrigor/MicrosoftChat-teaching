using System;
using System.Collections.Generic;
using System.Linq;
using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Models;

namespace Chat.BusinessLogicLayer.Managers
{
    public class UserManager
    {
        private static List<UserModel> users =new List<UserModel>();

        public UserManager()
        {
            if (!users.Any())
            {
                users.Add(new UserModel()
                {
                    UserName = "User",
                    Password = "cbe0cd68cbca3868250c0ba545c48032f43eb0e8a5e6bab603d109251486f77a91e46a3146d887e37416c6bdb6cbe701bd514de778573c9b0068483c1c626aec"
                });
            }
        }

        public ResponseResult<bool> Login(UserModel user)
        {
            try
            {
                var currentUser = users.FirstOrDefault(s => s.UserName == user.UserName && s.Password == user.Password);
                if (currentUser != null)
                {
                    SessionInfo.CurrentUserInfo = currentUser;
                    return new ResponseResult<bool>()
                    {
                        Type = ResponseType.Success,
                        Message = "The user Sign In successfully."
                    };
                }

                return new ResponseResult<bool>
                {
                    Type = ResponseType.Information,
                    Message = "User Name or Password incorrect!"
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<bool>()
                {
                    Type = ResponseType.Error,
                    Message = e.Message
                };
            }
        }

        public ResponseResult<bool> Registrate(UserRegistrationModel user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
                {
                    if (user.Password.Equals(user.ConfPassword))
                    {
                        if (users.Any(s => s.UserName != user.UserName))
                        {
                            users.Add(new UserModel()
                            {
                                UserName = user.UserName,
                                Password = user.Password
                            });
                            return new ResponseResult<bool>()
                            {
                                Type = ResponseType.Success,
                                Message = "The user Sign Up successfully!"
                            };
                        }

                        return new ResponseResult<bool>
                        {
                            Type = ResponseType.Information,
                            Message = "The username already exists!"
                        };
                    }
                    return new ResponseResult<bool>()
                    {
                        Type = ResponseType.Information,
                        Message = "Your passwords doesn't match!"
                    };
                }

                return new ResponseResult<bool>()
                {
                    Type = ResponseType.Error,
                    Message = "Please fill username and password!"
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<bool>()
                {
                    Type = ResponseType.Error,
                    Message = e.Message
                };
            }
        }
    }
}