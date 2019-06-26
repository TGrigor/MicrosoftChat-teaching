﻿using System.Windows;
using System.Windows.Controls;
using Chat.PresentationLayer.Models;
using Chat.BusinessLogicLayer.Managers;
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Utilities;

namespace Chat.PresentationLayer.Pages
{
    public partial class SignIn : Page
    {
        private readonly UserManager _userManager = new UserManager();
        public UserSignInModel UserSignIn { get; set; } = new UserSignInModel();

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
            var currentUser = new UserModel()
            {
                UserName = UserSignIn.UserName,
                Password = UserSignIn.Password
            };

            var isUserExists = _userManager.Login(currentUser);

            if (isUserExists)
            {
                MessageBox.Show("Sign In Success!");
            }
            else
            {
                MessageBox.Show("Fail!");
            }
        }

        private void PasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSignIn.Password = HashingUtilities.GetHash512(passwordBox.Password);
        }
    }
}