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
using System.Windows.Shapes;

namespace CinemaLuna.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnSecondButton(object sender, RoutedEventArgs e)
        {
            if(LlogTitle.Content.ToString() == "Logowanie")
            {
                LlogTitle.Content = "Rejestracja";
                SPReaPass.Visibility = Visibility.Visible;
                LOtherOpt.Content = "Masz już konto?";
                FirstButton.Content = "Zarejestruj się";
                SecondButton.Content = "Zaloguj się";
            }
            else
            {
                LlogTitle.Content = "Logowanie";
                SPReaPass.Visibility = Visibility.Hidden;
                LOtherOpt.Content = "Nie masz jeszcze konta?";
                FirstButton.Content = "Zaloguj się";
                SecondButton.Content = "Zarejestruj się";
            }
        }

        private void OnFirstButton(object sender, RoutedEventArgs e)
        {
            //proces logowania
            if (LlogTitle.Content.ToString() == "Logowanie") { }
            //proces rejestracjji
            else { }

        }
    }
}
