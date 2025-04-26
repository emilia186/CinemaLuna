using CinemaLuna.UserControls;
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

    public partial class UserAccountWindow : Window
    {
        public UserAccountWindow(string username)
        {
            InitializeComponent();

            usernameL.Content = $"Cześć, {username}!";
        }

    }
}
