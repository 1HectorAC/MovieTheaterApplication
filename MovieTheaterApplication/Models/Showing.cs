using System.ComponentModel.DataAnnotations;

namespace MovieTheaterApplication.Models
{
    public class Showing
    {
        public int Id { get; set; }

        [Required]
        public int AuditoriumId { get; set; }

        public Auditorium? Auditorium { get; set; }

        [Required]
        public DateTime ShowingTime { get; set;  }

        [Required]
        public int MovieId { get; set;  }

        public Movie? Movie { get; set; }
       
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
