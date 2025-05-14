using CinemaLuna.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Input;
using System.Linq;

namespace CinemaLuna.Windows
{

    public partial class BuyTicketWindow : Window
    {
        private Movie selectedMovie;

        public BuyTicketWindow(Movie movie)
        {
            InitializeComponent();

            selectedMovie = movie;
            Title = movie.Title;
            DataContext = movie;

            LoadDaysOfWeek();

            DateTime today = DateTime.Today;
            string day = today.Day.ToString();
            string month = today.ToString("MMMM");

            TodayDate.Content = $"DZISIAJ, {day} {month}";


            
            LoadSeanseByDate(today);
        }

        private void LoadSeanseByDate(DateTime date)
        {
            UpdateDateLabel(date);

            SeanseContainer.Children.Clear();

            using (var db = new CinemaDbContext())
            {
                string dateStr = date.ToString("yyyy-MM-dd");
                var seanse = db.Seanse
                               .Where(s => s.MovieId == selectedMovie.Id && s.ScreeningDate == dateStr)
                               .OrderBy(s => s.StartTime)
                               .ToList();

                if (seanse.Count == 0)
                {
                    NoSeanseLabel.Visibility = Visibility.Visible; 
                }
                else
                {
                    NoSeanseLabel.Visibility = Visibility.Hidden; 

                    foreach (var seans in seanse)
                    {
                        var seanseElement = new SeanseElement();
                        seanseElement.SetData(seans);

                        
                        seanseElement.SeansClicked += (s, clickedSeans) =>
                        {
                            WindowManager.OpenNewWindowAndCloseOthers(new ChoseSeatWindow(clickedSeans, selectedMovie));

                        };

                        SeanseContainer.Children.Add(seanseElement);
                    }

                }
            }
        }


        private void LoadDaysOfWeek()
        {
            string[] dayAbbreviations = { "Nd", "Pn", "Wt", "Śr", "Czw", "Pt", "Sb" };
            int todayIndex = (int)DateTime.Today.DayOfWeek; // 0 = nd

            Button todayButton = CreateDayButton("Dziś");
            DaysStackPanel.Children.Add(todayButton);

            for (int i = 1; i < 7; i++)
            {
                int nextDayIndex = (todayIndex + i) % 7;
                string day = dayAbbreviations[nextDayIndex];

                Button dayButton = CreateDayButton(day);

                DaysStackPanel.Children.Add(dayButton);
            }

            Button allButton = CreateDayButton("Wszystkie seanse");
            DaysStackPanel.Children.Add(allButton);
        }

        private Button CreateDayButton(string content)
        {
            
            Button button = new Button
            {
                Content = content,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Padding = new Thickness(0),
                Margin = new Thickness(35, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            
            Style style = new Style(typeof(Button));
            
            ControlTemplate template = new ControlTemplate(typeof(Button));
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            template.VisualTree = contentPresenter;

            
            style.Setters.Add(new Setter(Button.TemplateProperty, template));
            style.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.DemiBold));
            style.Setters.Add(new Setter(Button.ForegroundProperty, new SolidColorBrush(Color.FromRgb(204, 204, 204)))); 
            style.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Transparent));
            style.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.Transparent));
            style.Setters.Add(new Setter(Button.CursorProperty, Cursors.Hand));

            Trigger hoverTrigger = new Trigger
            {
                Property = Button.IsMouseOverProperty,
                Value = true
            };
            hoverTrigger.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));

            style.Triggers.Add(hoverTrigger);

            button.Style = style;

            button.Click += (s, e) =>
            {
                string buttonContent = button.Content.ToString();
                if (buttonContent == "Dziś")
                {
                    LoadSeanseByDate(DateTime.Today);
                }
                else if (buttonContent == "Wszystkie seanse")
                {
                    LoadAllUpcomingSeanse();
                }
                else
                {
                    
                    DateTime today = DateTime.Today;
                    int todayIndex = (int)today.DayOfWeek;

                    string[] abbrevs = { "Nd", "Pn", "Wt", "Śr", "Czw", "Pt", "Sb" };
                    int targetIndex = Array.IndexOf(abbrevs, buttonContent);
                    int offset = (targetIndex - todayIndex + 7) % 7;

                    DateTime targetDate = today.AddDays(offset);
                    LoadSeanseByDate(targetDate);
                }
            };



            return button;
        }

        private void LoadAllUpcomingSeanse()
        {
            SeanseContainer.Children.Clear();
            TodayDate.Visibility = Visibility.Hidden;


            using (var db = new CinemaDbContext())
            {
                DateTime now = DateTime.Now;
                string today = now.ToString("yyyy-MM-dd");

                var seanse = db.Seanse
                               .Where(s => s.MovieId == selectedMovie.Id &&
                                           string.Compare(s.ScreeningDate, today) >= 0)
                               .ToList() 
                               .OrderBy(s => DateTime.Parse(s.ScreeningDate))
                               .ThenBy(s => TimeSpan.Parse(s.StartTime))
                               .ToList();

                if (seanse.Count == 0)
                {
                    NoSeanseLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    NoSeanseLabel.Visibility = Visibility.Hidden;

                    foreach (var seans in seanse)
                    {
                        var seanseElement = new SeanseElement();
                        seanseElement.SetData(seans);

                        seanseElement.SeansClicked += (s, clickedSeans) =>
                        {
                            WindowManager.OpenNewWindowAndCloseOthers(new ChoseSeatWindow(clickedSeans, selectedMovie));
                        };

                        SeanseContainer.Children.Add(seanseElement);

                    }
                }
            }
        }

        private void UpdateDateLabel(DateTime selectedDate)
        {
            TodayDate.Visibility = Visibility.Visible;

            string dayName = selectedDate.ToString("dddd", new System.Globalization.CultureInfo("pl-PL"));
            string day = selectedDate.Day.ToString();
            string month = selectedDate.ToString("MMMM", new System.Globalization.CultureInfo("pl-PL"));

            if (selectedDate.Date == DateTime.Today)
            {
                TodayDate.Content = $"DZISIAJ, {day} {month}";
            }
            else
            {
                TodayDate.Content = $"{char.ToUpper(dayName[0]) + dayName.Substring(1)}, {day} {month}";
            }
        }


    }
}
