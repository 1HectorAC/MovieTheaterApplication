using System.ComponentModel.DataAnnotations;

namespace MovieTheaterApplication.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [Required]
        public int AuditoriumId { get; set; }
        public Auditorium? Auditorium { get; set; }

        [Required]
        public char Row { get; set;  }

        [Required]
        public int Column { get; set;  }

    }
}
