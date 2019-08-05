using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chat.PresentationLayer.Templates
{
    public static class PageTemplates
    {
        public  static StackPanel AddMessageTemplate(string userName, string message, bool isCurrentUser)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Width = 400;

            TextBlock newUserNameTextBlock = new TextBlock();
            newUserNameTextBlock.Text       = userName;
            newUserNameTextBlock.FontSize   = 15;
            newUserNameTextBlock.Foreground = Brushes.Aqua;

            TextBlock newMessageTextBlock = new TextBlock();
            newMessageTextBlock.Text         = message;
            newMessageTextBlock.TextWrapping = TextWrapping.Wrap;

            if (!isCurrentUser)
            {
                newStackPanel.Margin              = new Thickness(10, 10, 0, 0);
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                newStackPanel.Margin               = new Thickness(0, 10, 10, 0);
                newStackPanel.HorizontalAlignment  = HorizontalAlignment.Right;
                newUserNameTextBlock.TextAlignment = TextAlignment.Right;
                newMessageTextBlock.TextAlignment  = TextAlignment.Right;
            }

            newStackPanel.Children.Add(newUserNameTextBlock);
            newStackPanel.Children.Add(newMessageTextBlock);
            return newStackPanel;
        }
    }
}
