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
            GetAllUsers();
            // await RunPeriodicAsync(GetAllUsers, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), CancellationToken.None);
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

        private void AddMessage(string userName, string message, bool isCurrentUser)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Width = 400;

            TextBlock newUserNameTextBlock = new TextBlock();
            newUserNameTextBlock.Text = userName;
            newUserNameTextBlock.FontSize = 15;
            newUserNameTextBlock.Foreground = Brushes.Aqua;

            TextBlock newMessageTextBlock = new TextBlock();
            newMessageTextBlock.Text = message;
            newMessageTextBlock.TextWrapping = TextWrapping.Wrap;

            if (!isCurrentUser)
            {
                newStackPanel.Margin = new Thickness(10,10,0,0);
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                newStackPanel.Margin = new Thickness(0,10,10,0);
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
                newUserNameTextBlock.TextAlignment = TextAlignment.Right;
                newMessageTextBlock.TextAlignment = TextAlignment.Right;
            }

            newStackPanel.Children.Add(newUserNameTextBlock);
            newStackPanel.Children.Add(newMessageTextBlock);

            messagesMainStackPanel.Children.Add(newStackPanel);
        }

        private async void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await RunPeriodicAsync(GetMessages, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), CancellationToken.None);
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
                    AddMessage(isSelectedUser ? SessionInfo.CurrentUserInfo.UserName : SelectedUser.UserName, message.Text, isSelectedUser);
                }
            }

        }

        private void GetAllUsers()
        {
            var users = _userManager.GetAllUsers();
            UserList.Clear();
            foreach (var user in users)
            {
                UserList.Add(new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName
                });
            }
        }

        private static async Task RunPeriodicAsync(
            Action onTick,
            TimeSpan dueTime,
            TimeSpan interval,
            CancellationToken token)
        {
            // Initial wait time before we begin the periodic loop.
            if (dueTime > TimeSpan.Zero)
                await Task.Delay(dueTime, token);

            // Repeat this loop until cancelled.
            while (!token.IsCancellationRequested)
            {
                // Call our onTick function.
                onTick?.Invoke();

                // Wait to repeat again.
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }
    }
}
