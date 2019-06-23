﻿using System;
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

namespace Chat.PresentationLayer.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void RegistrationButton(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).main.Content = new Registration();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            var userName = userNameBox.Text;
            var password = passwordBox.Password;

            MessageBox.Show(userName);
            MessageBox.Show(password);
        }
    }
}
