using CinemaLuna.UserControls;
using System.Windows;

namespace CinemaLuna.Windows
{

    public partial class UserAccountWindow : Window
    {
        public UserAccountWindow(string username)
        {
            InitializeComponent();

            usernameL.Content = $"Cześć, {username}!";

            var tickets = CinemaDbContext.GetUserTickets(SessionMenager.LoggedUser.Id);

            foreach (var ticketInfo in tickets)
            {
                var control = new TicketElementControl();

                var ticket = ticketInfo.Ticket;
                var seans = ticket.SeanseId;

                control.SetTicketInfo(
                    ticketInfo.MovieTitle,
                    seans.ScreeningDate,
                    seans.StartTime,
                    seans.EndTime,
                    ticket.SeatRow,
                    ticket.SeatNumber
                );

                TicketsContainer.Children.Add(control);
            }

        }



    }
}
