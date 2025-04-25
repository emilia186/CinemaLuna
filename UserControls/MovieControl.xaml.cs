using System.Data.SqlTypes;
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

        public string Category
        {
            get => (string)GetValue(CategoryProperty);
            set => SetValue(CategoryProperty, value);
        }

        public static readonly System.Windows.DependencyProperty CategoryProperty =
            DependencyProperty.Register("Category", typeof(string), typeof(MovieControl), new PropertyMetadata(""));

        public string CoverPath
        {
            get => (string)GetValue(CoverPathProperty);
            set => SetValue(CoverPathProperty, value);
        }

        public static readonly System.Windows.DependencyProperty CoverPathProperty =
            DependencyProperty.Register("CoverPath", typeof(string), typeof(MovieControl), new PropertyMetadata(""));

/*        public int AgeRating
        {
            get => (int)GetValue(AgeRatingProperty);
            set => SetValue(AgeRatingProperty, value);
        }


        public static readonly System.Windows.DependencyProperty AgeRatingProperty =
            DependencyProperty.Register("AgeRating", typeof(int), typeof(MovieControl), new PropertyMetadata(""));*/

        

        /*public SqlDateTime PremiereDate
        {
            get => (SqlDateTime)GetValue(PremiereDateProperty);
            set => SetValue(PremiereDateProperty, value);
        }

        public static readonly System.Windows.DependencyProperty PremiereDateProperty =
            DependencyProperty.Register("PremiereDate", typeof(SqlDateTime), typeof(MovieControl), new PropertyMetadata(""));*/

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly System.Windows.DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(MovieControl), new PropertyMetadata(""));

        /*public bool Subtitles
        {
            get => (bool)GetValue(SubtitlesProperty);
            set => SetValue(SubtitlesProperty, value);
        }

        public static readonly System.Windows.DependencyProperty SubtitlesProperty =
            DependencyProperty.Register("Subtitles", typeof(bool), typeof(MovieControl), new PropertyMetadata(""));

        public bool Format3D
        {
            get => (bool)GetValue(Format3DProperty);
            set => SetValue(Format3DProperty, value);
        }

        public static readonly System.Windows.DependencyProperty Format3DProperty =
            DependencyProperty.Register("Format3D", typeof(bool), typeof(MovieControl), new PropertyMetadata(""));*/



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
