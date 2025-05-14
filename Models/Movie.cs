using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLuna.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Age_rating { get; set; }
        public DateTime Premiere_date { get; set; }
        public string Description { get; set; }
        public bool Subtitles { get; set; }
        public bool Format3D { get; set; }      //maybe better enum
        public string Cover_img_url { get; set; }
    }
}
