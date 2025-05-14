using CinemaLuna.Data;
using System.Windows;

namespace CinemaLuna.Windows
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();

        }


        private void OnAddMovie(object sender, RoutedEventArgs e)
        {
            if (AddMoviePanel.Visibility == Visibility.Hidden) AddMoviePanel.Visibility = Visibility.Visible;

            

        }
    }
}
