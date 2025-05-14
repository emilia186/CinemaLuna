using System.Windows;

namespace CinemaLuna.Windows
{

    public partial class MovieAnnouncementsWindow : Window
    {
        public MovieAnnouncementsWindow()
        {
            InitializeComponent();
            LoadUpcomingMovies();

        }

        private void LoadUpcomingMovies()
        {
            using (var db = new CinemaDbContext())
            {
                var upcomingMovies = db.GetUpcomingMovies(); 

                MoviesItemsControl.ItemsSource = upcomingMovies; 
            }
        }
    }
}
