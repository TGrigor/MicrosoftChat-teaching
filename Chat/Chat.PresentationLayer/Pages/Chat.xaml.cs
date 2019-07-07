using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chat.BusinessLogicLayer.Managers;
using Chat.PresentationLayer.Models;

namespace Chat.PresentationLayer.Pages
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        public List<UserViewModel> UserList { get; set; }
        public UserViewModel SelectedUser { get; set; }
        private readonly UserManager _userManager = new UserManager();

        public Chat()
        {
            UserList = new List<UserViewModel>();
            SelectedUser = new UserViewModel();
            InitializeComponent();
        }

        private void ListOfUsers_OnLoaded(object sender, RoutedEventArgs e)
        {
            var users = _userManager.GetAllUsers();
            foreach (var user in users)
            {
                UserList.Add(new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName
                });
            }
        }


        private void Send_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
