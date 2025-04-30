using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace CinemaLuna
{
 
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private readonly CinemaDbContext _dbService = new CinemaDbContext();
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public MainWindow()
        {
            
            InitializeComponent();
            DataContext = this;

            CinemaDbContext.SetInitializeNoCreate();

            LoadMovies();
        }

        private void LoadMovies()
        {
            Movies.Clear();
            var moviesFromDb = _dbService.GetAllMovies();
            foreach (var movie in moviesFromDb)
            {
                Movies.Add(movie);
            }
        }


        private string movieTitle;

        public event PropertyChangedEventHandler PropertyChanged;

        public string MovieTitle
        {
            get { return movieTitle; }
            set { movieTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MovieTitle"));
            }
        }
    }
}
