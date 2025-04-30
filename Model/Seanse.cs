namespace CinemaLuna.Model
{
    public class Seanse
    {
        public int Id { set; get; }
        public int MovieId { set; get; }
        public int HallId { set; get; }  
        public string ScreeningDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
