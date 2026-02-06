using System.ComponentModel.DataAnnotations;

namespace MovieTheaterApplication.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public int ShowingId { get; set; }

        public Showing? Showing { get; set; }

        [Required]
        public int SeatId { get; set; }

        public Seat? Seat { get; set; }

    }
}
