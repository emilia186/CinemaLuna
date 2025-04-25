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
    }
}
