namespace MovieTheaterApplication.Models.ViewModels
{
    public class SeatSelectionViewModel
    {
        public int ShowingId { get; set; }
        public required string MovieTitle { get; set; }

        public DateTime ShowingTime { get; set; }

        public required string AuditoriumTitle { get; set; }

        public required IEnumerable<IGrouping<char, SeatListViewModel>> GroupedSeats { get; set; }
    }
}
