using CinemaLuna.Model;
using System;
using System.Windows;

namespace CinemaLuna.Windows
{
    
    public partial class ChoseSeatWindow : Window
    {
        private Seanse selectedSeans;

        public ChoseSeatWindow(Seanse seans)
        {
            InitializeComponent();
            selectedSeans = seans;
            DataContext = seans;

            // przykład: wyświetl datę
            DateTime.TryParse(seans.ScreeningDate, out DateTime date);
            TodayDate.Content = $"{date:dddd, dd MMMM yyyy} — Sala {seans.HallId}";
        }

    }
}
