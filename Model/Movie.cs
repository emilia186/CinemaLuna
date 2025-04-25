using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLuna
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CoverPath { get; set; }
        //public int AgeRating { get; set; }
        //public SqlDateTime PremiereDate { get; set; }
        public string Description { get; set; }
        //public bool Subtitles { get; set; }
        //public bool Format3D { get; set; }

        //public string AgeRatingText => $"od {AgeRating} lat";
    }
}
