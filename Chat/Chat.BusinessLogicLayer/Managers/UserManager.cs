using System;
using System.Collections.Generic;
using System.Linq;
using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Models;
using Chat.DataAccessLayer.Dto;
using Chat.DataAccessLayer.Repository;

namespace Chat.BusinessLogicLayer.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager()
        {
            _userRepository = new UserRepository();
        }

        public ResponseResult<bool> Login(UserModel user)
        {
            try
            {
                var currentUser = _userRepository.GetUser(new UserDto()
                {
                    UserName = user.UserName,
                    Password = user.Password
                });

                if (currentUser != null)
                {
                    SessionInfo.CurrentUserInfo = new UserModel()
                    {
                        Id = currentUser.Id,
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
                        if (!_userRepository.IsUserExist(user.UserName))
                        {
                            _userRepository.Add(new UserDto()
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

        public List<UserModel> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers(SessionInfo.CurrentUserInfo.Id);

            return users.Select(s => new UserModel()
            {
                Id = s.Id,
                UserName = s.UserName
            }).ToList();
        }
    }
}