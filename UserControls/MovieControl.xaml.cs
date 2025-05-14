using CinemaLuna.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaLuna.UserControls
{

    public partial class MovieControl : UserControl
    {
        public MovieControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly System.Windows.DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MovieControl), new PropertyMetadata(""));


        private void MovieControlClick(object sender, MouseButtonEventArgs e)
        {
            if(DataContext is Movie movie)
            {
                var detailsWindow = new MovieDetails(movie);
                detailsWindow.Show();

                var mainWin = Application.Current.Windows
                        .OfType<MainWindow>()
                        .FirstOrDefault();
                mainWin.Close();
            }
        }

    }
}
