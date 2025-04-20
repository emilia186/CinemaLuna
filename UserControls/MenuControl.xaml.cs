using CinemaLuna.Windows;
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

namespace CinemaLuna.UserControls
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
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

            if (loginWin != null)
            {
                if (loginWin.WindowState == WindowState.Minimized)
                {
                    loginWin.WindowState = WindowState.Normal;
                }
                loginWin.Activate();
            }
            else
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }
    }
}
