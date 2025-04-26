using CinemaLuna.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Configuration;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Controls;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Windows.Media;
using CinemaLuna.Windows;
//using System.Windows.Forms;


namespace CinemaLuna
{

    public class CinemaDbContext : DbContext
    {


        public DbSet<Movie> Movies { get; set; }


        public CinemaDbContext() : base("CinemaDbContext") // nazwa connection stringa z App.config
        {
        }

        /*protected void CreateTables()
        {
            const string sqlTextCreateTables = @"  
            CREATE TABLE IF NOT EXISTS Movies 
                (Id INTEGER PRIMARY KEY NOT NULL,
                Title TEXT NOT NULL,
                Category TEXT NOT NULL,
                CoverPath TEXT NOT NULL,
                AgeRating TEXT NOT NULL,
                ReleaseDate TEXT NOT NULL,
                Description TEXT NOT NULL,
                Subtitles TEXT NOT NULL,
                Format3D TEXT NOT NULL
                );";


            using (var dbConnection = new SQLiteConnection(this.Database.Connection.ConnectionString))
            {
                dbConnection.Open();
                using (var dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = sqlTextCreateTables;
                    dbCommand.ExecuteNonQuery();
                }
            }
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.CreateTables(); // ręczne tworzenie tabel

            this.OnModelCreatingTable(modelBuilder.Entity<Movie>());

            base.OnModelCreating(modelBuilder);
        }*/

        public List<Movie> GetAllMovies()
        {
            using (var db = new CinemaDbContext())
            {
                return db.Movies.ToList();
            }
        }




        /*public static bool CheckIfUser(TextBox EmailTB)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CinemaDbContext"].ConnectionString;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

            }

                return false;
        }*/

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

        /*public void AddMovie(Movie movie)
        {
            using (var db = new CinemaDbContext())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }*/

        /*private void OnModelCreatingTable(EntityTypeConfiguration<Movie> movies)
        {
            movies.ToTable("Movies").HasKey(a => a.Id);

            movies.Property(a => a.Title).IsRequired();
            movies.Property(a => a.Category).IsRequired();
            movies.Property(a => a.CoverPath).IsRequired();
            movies.Property(a => a.AgeRating).IsRequired();
            movies.Property(a => a.ReleaseDate).IsRequired();
            movies.Property(a => a.Description).IsRequired();
            movies.Property(a => a.Subtitles).IsRequired();
            movies.Property(a => a.Format3D).IsRequired();
        }*/

        public static void SetInitializeNoCreate()
        {
            Database.SetInitializer<CinemaDbContext>(null);
        }
    }
}
