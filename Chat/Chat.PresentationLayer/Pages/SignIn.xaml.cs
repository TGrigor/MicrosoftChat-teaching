using System.Windows;
using System.Windows.Controls;
using Chat.BusinessLogicLayer.Enums;
using Chat.PresentationLayer.Models;
using Chat.BusinessLogicLayer.Managers;
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Utilities;

namespace Chat.PresentationLayer.Pages
{
    public partial class SignIn : Page
    {
        private readonly UserManager _userManager = new UserManager();
        public UserSignInViewModel UserSignIn { get; set; } = new UserSignInViewModel();

        public SignIn()
        {
            InitializeComponent();
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SignUp());
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            var response = _userManager.Login(new UserModel()
            {
                UserName = UserSignIn.UserName,
                Password = UserSignIn.Password
            });

            if (response.Type == ResponseType.Success)
            {
                NavigationService.Navigate(new Chat());
            }

            MessageBox.Show($"Type: {response.Type}|    Message: {response.Message}");
        }

        private void PasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSignIn.Password = HashingUtilities.GetHash512(passwordBox.Password);
        }
    }
}
