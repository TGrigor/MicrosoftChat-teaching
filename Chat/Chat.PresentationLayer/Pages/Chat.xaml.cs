using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Managers;
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Models;
using Chat.PresentationLayer.Templates;
using Chat.PresentationLayer.Utilities;

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

        private async void ListOfUsers_OnLoaded(object sender, RoutedEventArgs e)
        {
            await PeriodicTask.Run(GetAllUsers, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), CancellationToken.None);
        }

        private async void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await PeriodicTask.Run(GetMessages, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), CancellationToken.None);
        }

        private void Send_OnClick(object sender, RoutedEventArgs e)
        {
            var response = _chatManager.SendMessage(new ChatModel()
            {
                Text = ChatViewModel.Text,
                UserIdTo = SelectedUser.Id
            });
            GetMessages();
            if (response.Type != ResponseType.Success)
            {
                MessageBox.Show($"Type: {response.Type}|    Message: {response.Message}");
            }
        }

        private void GetMessages()
        {
            messagesMainStackPanel.Children.Clear();
            var messages = _chatManager.GetMessages(SelectedUser.Id);
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    var isSelectedUser = message.UserIdTo == SelectedUser.Id;
                    messagesMainStackPanel.Children.Add(PageTemplates.AddMessageTemplate(isSelectedUser ? SessionInfo.CurrentUserInfo.UserName : SelectedUser.UserName, message.Text, isSelectedUser));
                }
            }
        }

        private void GetAllUsers()
        {
            var users = _userManager.GetAllUsers();
            var newUsers = users.Where(s => !UserList.Select(ss => ss.Id).Contains(s.Id)).ToList();
            foreach (var user in newUsers)
            {
                UserList.Add(new UserViewModel()
                             {
                                 Id       = user.Id,
                                 UserName = user.UserName
                             });
            }
        }
    }
}
