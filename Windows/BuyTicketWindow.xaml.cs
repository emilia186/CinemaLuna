﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaLuna.Windows
{
    /// <summary>
    /// Interaction logic for BuyTicketWindow.xaml
    /// </summary>
    public partial class BuyTicketWindow : Window
    {
        public BuyTicketWindow(Movie movie)
        {
            InitializeComponent();

            Title = movie.Title;
            DataContext = movie;
        }
    }
}
