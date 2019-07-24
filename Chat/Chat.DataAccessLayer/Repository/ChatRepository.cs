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
    }
}
