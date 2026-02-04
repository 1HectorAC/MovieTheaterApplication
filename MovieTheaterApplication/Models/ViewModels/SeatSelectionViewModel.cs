namespace MovieTheaterApplication.Models.ViewModels
{
    public class SeatSelectionViewModel
    {
        public int SeatId { get; set; }

        public char Row { get; set; }

        public int Column { get; set; }

        public bool IsSelected { get; set; }

    }
}
