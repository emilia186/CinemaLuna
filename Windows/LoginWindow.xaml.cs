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
                Username.Visibility = Visibility.Visible;
                LOtherOpt.Content = "Masz już konto?";
                FirstButton.Content = "Zarejestruj się";
                SecondButton.Content = "Zaloguj się";
                InfoLabel.Content = "";
            }
            else
            {
                LlogTitle.Content = "Logowanie";
                SPReaPass.Visibility = Visibility.Hidden;
                Username.Visibility = Visibility.Hidden;
                LOtherOpt.Content = "Nie masz jeszcze konta?";
                FirstButton.Content = "Zaloguj się";
                SecondButton.Content = "Zarejestruj się";
                InfoLabel.Content = "";
            }
        }

        private void OnFirstButton(object sender, RoutedEventArgs e)
        {
            InfoLabel.Foreground = new SolidColorBrush(Colors.OrangeRed);

            //proces logowania
            if (LlogTitle.Content.ToString() == "Logowanie") {
                bool result = CinemaDbContext.CheckIfUser(EmailText,PasswordText);
                if (result)
                {
                    
                    var mainWin = Application.Current.Windows
                        .OfType<MainWindow>()
                        .FirstOrDefault();
                    mainWin.Close();

                    this.Close();
                }
                else
                {
                    InfoLabel.Content = "Niepoprawny email lub hasło.";
                }
            }

            //proces rejestracji
            else {
                if(PasswordText.Password == RPasswordText.Password)
                {
                    bool signUpResult = CinemaDbContext.AddUser(NameText, EmailText, PasswordText, InfoLabel);
                    if(signUpResult) {
                        LlogTitle.Content = "Logowanie";
                        SPReaPass.Visibility = Visibility.Hidden;
                        Username.Visibility = Visibility.Hidden;
                        LOtherOpt.Content = "Nie masz jeszcze konta?";
                        FirstButton.Content = "Zaloguj się";
                        SecondButton.Content = "Zarejestruj się";
                    }
                }
                else
                {
                    InfoLabel.Content = "Hasła się różnią.";
                }
            
            }

        }
    }
}
