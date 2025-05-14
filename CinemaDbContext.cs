using CinemaLuna.Model;
using CinemaLuna.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace CinemaLuna
{

    public class CinemaDbContext : DbContext
    {


        public DbSet<Movie> Movies { get; set; }

        public DbSet<Seanse> Seanse { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public CinemaDbContext() : base("CinemaDbContext") // nazwa connection stringa z App.config
        {
        }


        public List<Movie> GetAllMovies()
        {
            using (var db = new CinemaDbContext())
            {
                return db.Movies.ToList();
            }
        }

        public List<Movie> GetUpcomingMovies()
        {
            using (var db = new CinemaDbContext())
            {
                var todayPlus7 = DateTime.Now.AddDays(7);

                var upcomingMovies = db.Movies
                    .ToList() 
                    .Where(movie =>
                    {
                        if (DateTime.TryParse(movie.ReleaseDate, out DateTime releaseDate))
                        {
                            return releaseDate > todayPlus7;
                        }
                        return false;
                    })
                    .ToList();

                return upcomingMovies;
            }
        }

        public List<Seanse> GetTodaySeanse()
        {
            using (var db = new CinemaDbContext())
            {
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                return db.Set<Seanse>().Where(s => s.ScreeningDate == today).ToList();
            }
        }


        public static bool CheckIfUser(TextBox EmailTB, PasswordBox PasswordPB)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string Email = EmailTB.Text;
                string Password = PasswordPB.Password;

                string query = "SELECT Id, Name FROM users WHERE Email = @Email AND Password = @Password";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            int id = reader.GetInt32(0); 
                            string name = reader.GetString(1);
                            string email = Email;

                            
                            User authenticatedUser = new User
                            {
                                Id = id,
                                Name = name,
                                Email = email
                            };

                            
                            SessionMenager.LoggedUser = authenticatedUser;

                            WindowManager.OpenNewWindowAndCloseOthers(new UserAccountWindow(SessionMenager.LoggedUser.Name));

                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
        }

        public static bool AddUser(TextBox NameTB, TextBox EmailTB, PasswordBox PasswordPB, Label InfoLabel)
        {
            if (!CheckIfUserExist(EmailTB))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string Name = NameTB.Text;
                    string Email = EmailTB.Text;
                    string Password = PasswordPB.Password;

                    string query = "INSERT INTO users (Name, Email, Password) VALUES (@Name, @Email, @Password);";
                    
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", Password);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            InfoLabel.Foreground = new SolidColorBrush(Colors.LightGray);
                            InfoLabel.Content = "Rejestracja sie powiodła. Zaloguj się na swoje konto.";
                            return true;
                        }
                        else
                        {
                            InfoLabel.Content = "Nie udało sie dodać użytkownika";
                            return false;
                        }
                    }
                }
            }
            else
            {
                InfoLabel.Content = "Użytkownik o podanym adresie email już istnieje.";
                return false;
            }
        }

        public static bool CheckIfUserExist(TextBox EmailTB)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string Email = EmailTB.Text;

                string query = "SELECT COUNT(1) FROM users WHERE Email = @Email";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static void SaveTicket(User user, Seanse seans, int seatRow, int seatNumber, string ticketType)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO tickets (UserId, SeanseId, SeatRow, SeatNumber, TicketType, PurchaseDate)
                         VALUES (@UserId, @SeanseId, @SeatRow, @SeatNumber, @TicketType, @PurchaseDate);";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@SeanseId", seans.Id);
                    command.Parameters.AddWithValue("@SeatRow", seatRow);
                    command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                    command.Parameters.AddWithValue("@TicketType", ticketType);
                    command.Parameters.AddWithValue("@PurchaseDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<TicketInfo> GetUserTickets(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;
            var ticketInfos = new List<TicketInfo>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT t.Id, t.SeatRow, t.SeatNumber, t.TicketType, t.PurchaseDate,
                                s.Id as SeanseId, s.ScreeningDate, s.StartTime, s.EndTime, s.HallId, s.MovieId,
                                m.Title,
                                u.Id, u.Name, u.Email
                         FROM tickets t
                         JOIN seanses s ON t.SeanseId = s.Id
                         JOIN movies m ON s.MovieId = m.Id
                         JOIN users u ON t.UserId = u.Id
                         WHERE t.UserId = @UserId
                         ORDER BY t.PurchaseDate DESC";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ticket = new Ticket
                            {
                                Id = reader.GetInt32(0),
                                SeatRow = reader.GetInt32(1),
                                SeatNumber = reader.GetInt32(2),
                                TicketType = reader.GetString(3),
                                PurchaseDate = reader.GetString(4),
                                SeanseId = new Seanse
                                {
                                    Id = reader.GetInt32(5),
                                    ScreeningDate = reader.GetString(6),
                                    StartTime = reader.GetString(7),
                                    EndTime = reader.GetString(8),
                                    HallId = reader.GetInt32(9),
                                    MovieId = reader.GetInt32(10),
                                },
                                UserId = new User
                                {
                                    Id = reader.GetInt32(12),
                                    Name = reader.GetString(13),
                                    Email = reader.GetString(14)
                                }
                            };

                            var ticketInfo = new TicketInfo
                            {
                                Ticket = ticket,
                                MovieTitle = reader.GetString(11)
                            };

                            ticketInfos.Add(ticketInfo);
                        }
                    }
                }
            }

            return ticketInfos;
        }

        public static List<SeatSelection> GetTakenSeats(int seansId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;
            var takenSeats = new List<SeatSelection>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT SeatRow, SeatNumber FROM tickets WHERE SeanseId = @SeanseId";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SeanseId", seansId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            takenSeats.Add(new SeatSelection
                            {
                                Row = reader.GetInt32(0),
                                SeatNumber = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }

            return takenSeats;
        }


        public static void SetInitializeNoCreate()
        {
            Database.SetInitializer<CinemaDbContext>(null);
        }

        public class TicketInfo
        {
            public Ticket Ticket { get; set; }
            public string MovieTitle { get; set; }
        }
    }
}
