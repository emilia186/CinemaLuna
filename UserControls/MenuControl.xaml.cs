using CinemaLuna.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CinemaLuna.UserControls
{
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
        }

        private void OnUserAccountButton(object sender, RoutedEventArgs e)
        {
            //sprawdzanie czy okno logowania juz nie istnieje
            var loginWin = Application.Current.Windows
                        .OfType<LoginWindow>()
                        .FirstOrDefault();

            CheckWindow<LoginWindow>(loginWin);
        }

        private void OnPriceListButton(object sender, RoutedEventArgs e)
        {
            //sprawdzanie czy cennik juz nie istnieje
            var priceWin = Application.Current.Windows
                        .OfType<PriceListWindow>()
                        .FirstOrDefault();

            var mainWin = Application.Current.Windows
                        .OfType<MainWindow>()
                        .FirstOrDefault();

            CheckWindow<PriceListWindow>(priceWin, mainWin);
        }

        private static void CheckWindow<T>(Window window, Window closeWin = null) where T : Window, new()
        {
            if (window != null)
            {
                if (window.WindowState == WindowState.Minimized)
                {
                    window.WindowState = WindowState.Normal;
                }
                window.Activate();
            }
            else
            {
                T newWindow = new T();
                newWindow.Show();

                if(closeWin != null)
                {
                    closeWin.Close();
                }
            }
        }
    }
}
