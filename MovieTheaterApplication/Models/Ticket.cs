namespace MovieTheaterApplication.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int ShowingId { get; set; }

        public Showing? Showing { get; set; }

        public int SeatId { get; set; }

        public Seat? Seat { get; set; }

    }
}
