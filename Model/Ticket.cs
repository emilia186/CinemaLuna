﻿namespace CinemaLuna.Model
{
    public class Ticket
    {
        public int Id { set; get; }
        public User UserId { set; get; }
        public Seanse SeanseId { set; get; }
        public int SeatRow { set; get; }
        public int SeatNumber { set; get; }
        public string TicketType { set; get; }
        public string PurchaseDate { set; get; }

    }
}
