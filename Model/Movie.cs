namespace CinemaLuna
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CoverPath { get; set; }
        public string AgeRating { get; set; }
        public string ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Subtitles { get; set; }
        public string Format3D { get; set; }

        public string AgeRatingText => $"od {AgeRating} lat";
        public string ReleaseDateText => $"Premiera:\n{ReleaseDate}";
    }
}
