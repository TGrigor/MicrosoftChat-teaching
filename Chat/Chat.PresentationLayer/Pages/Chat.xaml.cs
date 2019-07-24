using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Models;

namespace Chat.PresentationLayer.Pages
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Page
    {
        public ObservableCollection<UserViewModel> UserList { get; set; }
        public ChatViewModel ChatViewModel { get; set; } = new ChatViewModel();
        public UserViewModel SelectedUser { get; set; }
        private readonly UserManager _userManager = new UserManager();
        private readonly ChatManager _chatManager = new ChatManager();

        public Chat()
        {
            UserList = new ObservableCollection<UserViewModel>();
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
            var response = _chatManager.SendMessage(new ChatModel()
            {
                Text = ChatViewModel.Text,
                UserIdTo = SelectedUser.Id
            });

            MessageBox.Show($"Type: {response.Type}|    Message: {response.Message}");
        }
    }
}
