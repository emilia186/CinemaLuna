using CinemaLuna.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CinemaLuna.Windows
{

    public partial class LoginWindow : Window
    {
        private bool BuyingTicket = false;
        Seanse seansB = null;
        List<SeatSelection> selectedSeatsB = null;
        public LoginWindow(bool buyingTicket = false, Seanse seans =null, List<SeatSelection> selectedSeats=null)
        {
            InitializeComponent();
            BuyingTicket = buyingTicket;
            seansB = seans;
            selectedSeatsB = selectedSeats;
        }

        public LoginWindow() : this(false, null, null)
        {
            
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
                if (result && BuyingTicket)
                {
                    WindowManager.OpenNewWindowAndCloseOthers(new TicketSummaryWindow(seansB, selectedSeatsB));
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
