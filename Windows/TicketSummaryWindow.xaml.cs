using CinemaLuna.Model;
using CinemaLuna.UserControls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace CinemaLuna.Windows
{
    public partial class TicketSummaryWindow : Window
    {
        private Seanse seans;

        private List<SeatSelection> selectedSeats;


        float Sum = 0;
        public TicketSummaryWindow(Seanse seans, List<SeatSelection> selectedSeats)
        {
            InitializeComponent();

            this.seans = seans;
            this.selectedSeats = selectedSeats;

            BuyTicketSum.IsEnabled = false;

            foreach (var seat in selectedSeats)
            {
                string seatText = $"Rząd {seat.Row}, Miejsce {seat.SeatNumber}";

                var ticketControl = new TicketSumControl(seatText)
                {
                    SeatRow = seat.Row,
                    SeatNumber = seat.SeatNumber
                };

                TicketsPanel.Children.Add(ticketControl);

                ticketControl.TicketTypeChanged += (s, e) => UpdateSum();
            }


            UpdateSum();
        }

        private void OnBuyTicketSumButton(object sender, RoutedEventArgs e)
        {
            if(VoucherTB.Text == "qwerty") //kod kuponu
            {

                foreach (var seat in selectedSeats)
                {
                    string ticketType = "normalny"; 

                    foreach (TicketSumControl control in TicketsPanel.Children)
                    {
                        if (control.SeatRow == seat.Row && control.SeatNumber == seat.SeatNumber)
                        {
                            ticketType = control.TicketType;
                            break;
                        }
                    }

                    CinemaDbContext.SaveTicket(SessionMenager.LoggedUser, seans, seat.Row, seat.SeatNumber, ticketType);
                }
                
                WindowManager.OpenNewWindowAndCloseOthers(new UserAccountWindow(SessionMenager.LoggedUser.Name));

            }
            else
            {
                VoucherInfoL.Content = "Nieprawidłowy kod kuponu";
                VoucherTB.Clear();
            }
        }

        private void VoucherTB_textChange(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(VoucherTB.Text))
            {
                BuyTicketSum.IsEnabled = true;
                BuyTicketSum.Background = Brushes.LightGray;
            }
            else
            {
                BuyTicketSum.IsEnabled = false;
                BuyTicketSum.Background = Brushes.DimGray;
            }
        }

        private void UpdateSum()
        {
            float sum = 0;

            if (!DateTime.TryParse(seans.ScreeningDate, out DateTime screeningDate))
            {
                return;
            }

            bool isWeekend = screeningDate.DayOfWeek == DayOfWeek.Saturday || screeningDate.DayOfWeek == DayOfWeek.Sunday;

            foreach (TicketSumControl control in TicketsPanel.Children)
            {
                if (control.TicketType == "ulgowy")
                    sum += isWeekend ? 28 : 22;
                else
                    sum += isWeekend ? 30 : 25;
            }

            Sum = sum;
            SumLabel.Content = $"Suma: {Sum} zł";
        }


    }
}
