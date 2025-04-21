using CinemaLuna.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite.EF6;

namespace CinemaLuna.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDbContext") {
            
        }

        public DbSet<Movie> Movies { get; set; }

        protected void CreateTables()
        {
            const string sqlTextCreateTables = @"
                    CREATE TABLE IF NOT EXISTS Movie
                    (
                        Id INTEGER PRIMARY KEY NOT NULL,
                        Title TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Age_rating TEXT NOT NULL,
                        Premiere_date DATE,
                        Description TEXT NOT NULL,
                        Subtitles BOOLEAN,
                        Format3D BOOLEAN,
                        Cover_img_url TEXT NOT NULL
                    )";
            var connectionString = this.Database.Connection.ConnectionString;
            using (var dbConnection = new System.Data.SQLite.SQLiteConnection(connectionString))
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
            this.CreateTables(); 

            // Dodatkowe ustawienia dla EF
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Movie>().HasKey(m => m.Id);
            modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Subtitles).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Format3D).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        // Wyłączenie migracji EF (dla SQLite)
        public static void DisableEFInitializer()
        {
            Database.SetInitializer<MyDbContext>(null);
        }
    }

}
