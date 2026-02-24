namespace MovieTheaterApplication.Models.ViewModels
{
    public class SeatListViewModel
    {
        public int SeatId { get; set; }

        public char Row { get; set; }

        public int Column { get; set; }

        public bool IsSelected { get; set; }

    }
}
