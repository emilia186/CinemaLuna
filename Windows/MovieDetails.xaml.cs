using System.Linq;
using System.Windows;

namespace CinemaLuna.Windows
{
    
    public partial class MovieDetails : Window
    {
        public MovieDetails(Movie movie)
        {
            InitializeComponent();

            Title = movie.Title;
            DataContext = movie;
        }

        private void OnBuyTicketButton(object sender, RoutedEventArgs e)
        {
            if (DataContext is Movie movie)
            {
                WindowManager.OpenNewWindowAndCloseOthers(new BuyTicketWindow(movie));
            }

            this.Close();
        }
    }
}
