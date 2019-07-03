using System;
using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Models;
using Chat.DataAccessLayer.Dto;
using Chat.DataAccessLayer.Repasitory;

namespace Chat.BusinessLogicLayer.Managers
{
    public class UserManager
    {
        private readonly UserRepasitory _userRepasitory;

        public UserManager()
        {
            _userRepasitory = new UserRepasitory();
        }

        public ResponseResult<bool> Login(UserModel user)
        {
            try
            {
                var currentUser = _userRepasitory.GetUser(new UserDto()
                {
                    UserName = user.UserName,
                    Password = user.Password
                });

                if (currentUser != null)
                {
                    SessionInfo.CurrentUserInfo = new UserModel()
                    {
                        UserName = currentUser.UserName,
                        Password = currentUser.Password
                    };
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
                        if (!_userRepasitory.IsUserExist(user.UserName))
                        {
                            _userRepasitory.Add(new UserDto()
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