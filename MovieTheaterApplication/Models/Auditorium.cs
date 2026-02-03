using System.ComponentModel.DataAnnotations;

namespace MovieTheaterApplication.Models
{
    public class Auditorium
    {
        public int Id { get; set;  }

        [Required]
        public required string Title { get; set; }

        //Consider adding seat rows and column numbers

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    }
}
