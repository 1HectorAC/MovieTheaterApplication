namespace MovieTheaterApplication.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public int AuditoriumId { get; set; }
        public Auditorium? Auditorium { get; set; }

        public char Row { get; set;  }

        public int Column { get; set;  }

    }
}
