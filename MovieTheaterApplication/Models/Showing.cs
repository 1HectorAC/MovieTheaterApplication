namespace MovieTheaterApplication.Models
{
    public class Showing
    {
        public int Id { get; set; }

        public int AuditoriumId { get; set; }

        public Auditorium? Auditorium { get; set; }

        public DateTime ShowingTime { get; set;  }

        public int MovieId { get; set;  }

        public Movie? Movie { get; set; }

        public List<ShowingSeat> ShowingSeats { get; set; } = new List<ShowingSeat>();
    }
}
