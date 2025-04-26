using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SQLite;
using System.Linq;

namespace CinemaLuna
{

    public class CinemaDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        

        public CinemaDbContext() : base("CinemaDbContext") // nazwa connection stringa z App.config
        {
        }

        protected void CreateTables()
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
        }

        public List<Movie> GetAllMovies()
        {
            using (var db = new CinemaDbContext())
            {
                return db.Movies.ToList();
            }
        }

        public void AddMovie(Movie movie)
        {
            using (var db = new CinemaDbContext())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
        }

        private void OnModelCreatingTable(EntityTypeConfiguration<Movie> movies)
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
        }

        public static void SetInitializeNoCreate()
        {
            Database.SetInitializer<CinemaDbContext>(null);
        }
    }
}
