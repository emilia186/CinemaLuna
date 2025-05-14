using CinemaLuna.Model;
using System;
using System.Windows.Controls;

namespace CinemaLuna.UserControls
{

    public partial class SeanseElement : UserControl
    {
        public Seanse SeansData { get; private set; }
        public SeanseElement()
        {
            InitializeComponent();
            this.MouseLeftButtonUp += OnClick;
        }

        public void SetData(Seanse seans)
        {
            SeansData = seans;
            TimeLabel.Content = $"{seans.StartTime} - {seans.EndTime}";
            SalaLabel.Content = $"Sala {seans.HallId}";
            FormatLabel.Content = "Napisy, 2D"; 
        }

        public event EventHandler<Seanse> SeansClicked;

        private void OnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SeansClicked?.Invoke(this, SeansData);
        }
    }
}
