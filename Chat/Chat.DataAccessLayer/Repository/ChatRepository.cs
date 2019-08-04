using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.DataAccessLayer.Dto;
using Chat.DataAccessLayer.EntityModels;

namespace Chat.DataAccessLayer.Repository
{
    public class ChatRepository
    {
        private readonly MicrosoftChatEntities _context;

        public ChatRepository()
        {
            _context = new MicrosoftChatEntities();
        }

        public void SendMessage(ChatDto chatDto)
        {
            _context.Messages.Add(new Message()
            {
                Text = chatDto.Text,
                UserIdFrom = chatDto.UserIdFrom,
                UserIdTo = chatDto.UserIdTo,
                CreatedOn = DateTime.Now
            });

            _context.SaveChanges();
        }

        public List<ChatDto> GetMessages(int selectedUserId, int currentUserId)
        {
            var query = (from message in _context.Messages
                        where (message.UserIdFrom == currentUserId && message.UserIdTo == selectedUserId) ||
                              (message.UserIdFrom == selectedUserId && message.UserIdTo == currentUserId)
                        select new
                        {
                            message.UserIdTo,
                            message.Text,
                            message.CreatedOn
                        }).OrderBy(o => o.CreatedOn).Select(s => new ChatDto()
                                                                 {
                                                                     UserIdTo = s.UserIdTo,
                                                                     Text = s.Text
                                                                 });
            var listOfMessages = query.ToList();
            return listOfMessages;
        }
    }
}
