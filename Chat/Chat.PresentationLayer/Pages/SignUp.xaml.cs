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
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Models;
using Chat.PresentationLayer.Utilities;

namespace Chat.PresentationLayer.Pages
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        private readonly UserManager _userManager = new UserManager();
        public UserSignUpModel UserSignUp { get; set; } = new UserSignUpModel();

        public SignUp()
        {
            InitializeComponent();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SignIn());
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            var result = _userManager.Registrate(new UserRegistrationModel()
            {
                UserName = UserSignUp.UserName,
                Password = UserSignUp.Password,
                ConfPassword = UserSignUp.ConfPassword
            });

            if (result)
            {
                MessageBox.Show("Success");
                NavigationService.Navigate(new SignIn());
            }
            else
            {
                MessageBox.Show("Failed");
                       
            }
        }

        private void PasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSignUp.Password = HashingUtilities.GetHash512(passwordBox.Password);
        }

        private void ConfPasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSignUp.ConfPassword = HashingUtilities.GetHash512(confPasswordBox.Password);
        }
    }
}
