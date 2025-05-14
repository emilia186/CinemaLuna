using System.Windows.Controls;

namespace CinemaLuna.UserControls
{
    public partial class TicketElementControl : UserControl
    {
        public TicketElementControl()
        {
            InitializeComponent();
        }

        public void SetTicketInfo(string title, string date, string startTime, string endTime, int row, int seat)
        {
            titleLabel.Content = title;
            dateLabel.Content = date;
            timeLabel.Content = $"{startTime} - {endTime}";
            seatLabel.Content = $"Rząd {row}, Miejsce {seat}";
        }

    }
}
