using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaLuna.Windows;

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

        public string CoverPath
        {
            get => (string)GetValue(CoverPathProperty);
            set => SetValue(CoverPathProperty, value);
        }

        public static readonly System.Windows.DependencyProperty CoverPathProperty =
            DependencyProperty.Register("CoverPath", typeof(string), typeof(MovieControl), new PropertyMetadata(""));



        private void MovieControlClick(object sender, MouseButtonEventArgs e)
        {
            if(DataContext is Movie movie)
            {
                var detailsWindow = new MovieDetails(movie);
                detailsWindow.ShowDialog();
            }
        }

    }
}
