using System.Windows;
using System.Windows.Controls;

namespace CinemaLuna.UserControls
{

    public partial class TicketSumControl : UserControl
    {
        public string TicketType { get; private set; } = "normalny";

        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }

        public TicketSumControl(string seatText)
        {
            InitializeComponent();

            TicketType = "normalny";
            TicketTypeButton.Content = "bilet normalny";

            DataContext = new { SeatText = seatText };

        }

        private void OnTicketTypeButton(object sender, System.Windows.RoutedEventArgs e)
        {

            if (TicketType == "ulgowy")
            {
                TicketType = "normalny";
                TicketTypeButton.Content = "bilet normalny";
            }
            else
            {
                TicketType = "ulgowy";
                TicketTypeButton.Content = "bilet ulgowy";
            }

            RaiseEvent(new RoutedEventArgs(TicketTypeChangedEvent));
        }

        public static readonly RoutedEvent TicketTypeChangedEvent =
            EventManager.RegisterRoutedEvent("TicketTypeChanged", RoutingStrategy.Bubble,
            typeof(RoutedEventHandler), typeof(TicketSumControl));

        public event RoutedEventHandler TicketTypeChanged
        {
            add { AddHandler(TicketTypeChangedEvent, value); }
            remove { RemoveHandler(TicketTypeChangedEvent, value); }
        }
    }
}
