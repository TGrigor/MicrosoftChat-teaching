using System.Linq;
using Chat.DataAccessLayer.Dto;
using Chat.DataAccessLayer.EntityModels;

namespace Chat.DataAccessLayer.Repasitory
{
    public class UserRepasitory
    {
        private readonly MicrosoftChatEntities _context;

        public UserRepasitory()
        {
            _context = new MicrosoftChatEntities();
        }

        public bool IsUserExist(string userName) => _context.Users.Any(s => s.UserName == userName);

        public void Add(UserDto user)
        {
            _context.Users.Add(new User()
            {
                UserName = user.UserName,
                Password = user.Password
            });
            _context.SaveChanges();
        }

        public UserDto GetUser(UserDto user)
        {
            var entityUser = _context.Users.FirstOrDefault(s => s.UserName == user.UserName && s.Password == user.Password);
            if (entityUser != null)
                return new UserDto()
                {
                    UserName = entityUser.UserName,
                    Password = entityUser.Password
                };

            return null;
        }
    }
}
