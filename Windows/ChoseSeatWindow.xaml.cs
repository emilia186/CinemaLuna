using CinemaLuna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CinemaLuna.Windows
{
    
    public partial class ChoseSeatWindow : Window
    {
        private Seanse selectedSeans;

        private List<SeatSelection> selectedSeats = new List<SeatSelection>();
        private List<SeatSelection> takenSeats = new List<SeatSelection>();


        static int buySeatCount = 0;

        public ChoseSeatWindow(Seanse seans, Movie movie)
        {
            InitializeComponent();
            selectedSeans = seans;
            DataContext = seans;

            BuyTicketButton.IsEnabled = false;

            Title.Content =  movie.Title;
            

            DateTime.TryParse(seans.ScreeningDate, out DateTime date);
            TodayDate.Content = $"{date:dddd, dd MMMM yyyy} — Sala {seans.HallId}";

            takenSeats = CinemaDbContext.GetTakenSeats(seans.Id);



            GenerateSeats(10, 15);
        }

        private void GenerateSeats(int rows, int columns)
        {
            int seatNumber = 1;
            int row = 1;
            bool isLabel = true; 

            for (int i = 0; i < rows * columns; i++)
            {

                if(seatNumber == 1 && isLabel)
                {
                    Label rowNumber = new Label
                    {
                        Content = Convert.ToString(row),
                        Foreground = Brushes.DimGray,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };

                    isLabel = false;
                    HallGrid.Children.Add(rowNumber);
                }

                bool isTaken = takenSeats.Any(s => s.Row == row && s.SeatNumber == seatNumber);

                Button seats = new Button
                {
                    Content = Convert.ToString(seatNumber),
                    Width = 25,
                    Height = 25,
                    Margin = new Thickness(5),
                    Background = isTaken ? Brushes.DimGray : Brushes.LightGray,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Tag = new SeatSelection { Row = row, SeatNumber = seatNumber },
                    IsHitTestVisible = !isTaken, 
                    Focusable = !isTaken
                };

                if (!isTaken)
                {
                    seats.Click += OnSeatButton;
                }

                HallGrid.Children.Add(seats);


                if (seatNumber == columns-1)
                {
                    seatNumber = 1;
                    row++;
                    isLabel = true;
                }
                else if(!isLabel)
                {
                    seatNumber++;
                }

            }
        }

        private void OnSeatButton(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            SeatSelection seat = (SeatSelection)btn.Tag;

            var alreadySelected = selectedSeats.Any(s => s.Row == seat.Row && s.SeatNumber == seat.SeatNumber);

            if (!alreadySelected)
            {
                btn.Background = Brushes.LightSkyBlue;
                selectedSeats.Add(seat);
            }
            else
            {
                btn.Background = Brushes.LightGray;
                selectedSeats.RemoveAll(s => s.Row == seat.Row && s.SeatNumber == seat.SeatNumber);
            }

            BuyTicketButton.IsEnabled = selectedSeats.Count > 0;
            BuyTicketButton.Background = selectedSeats.Count > 0 ? Brushes.LightGray : Brushes.DimGray;
        }

        private void OnBuyTicketButton(object sender, RoutedEventArgs e)
        {
            if (DataContext is Seanse seans)
            {
                if (SessionMenager.LoggedUser == null)
                {
                    // Użytkownik niezalogowany
                    var loginWindow = new LoginWindow(true, seans, selectedSeats);
                    loginWindow.Show();

                }
                else
                {
                    WindowManager.OpenNewWindowAndCloseOthers(new TicketSummaryWindow(seans, selectedSeats));

                    this.Close();
                    
                }
            }
        }
    }


    public class SeatSelection
    {
        public int Row { get; set; }
        public int SeatNumber { get; set; }
    }

}
