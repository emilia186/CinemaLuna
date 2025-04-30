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
                    .ToList() // pobieramy wszystko
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

                            UserAccountWindow accountWindow = new UserAccountWindow(name);
                            accountWindow.Show();


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


        public static void SetInitializeNoCreate()
        {
            Database.SetInitializer<CinemaDbContext>(null);
        }
    }
}
