using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Models;
using Chat.DataAccessLayer.Dto;
using Chat.DataAccessLayer.Repository;

namespace Chat.BusinessLogicLayer.Managers
{
    public class ChatManager
    {
        private readonly ChatRepository _chatRepository = new ChatRepository();

        public ChatManager()
        {
            
        }

        public ResponseResult<bool> SendMessage(ChatModel chatModel)
        {
            try
            {
                _chatRepository.SendMessage(new ChatDto()
                {
                    Text = chatModel.Text,
                    UserIdTo = chatModel.UserIdTo,
                    UserIdFrom = SessionInfo.CurrentUserInfo.Id
                });

                return new ResponseResult<bool>()
                {
                    Message = "Success",
                    Type = ResponseType.Success,
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<bool>()
                {
                    Message = e.Message,
                    Type = ResponseType.Error,
                };
            }
           
        }

        public List<ChatModel> GetMessages(int selectedUserId)
        {
            return _chatRepository.GetMessages(selectedUserId, SessionInfo.CurrentUserInfo.Id).Select(s =>
                new ChatModel()
                {
                    Text = s.Text,
                    UserIdTo = s.UserIdTo
                }).ToList();
        }
    }
}
