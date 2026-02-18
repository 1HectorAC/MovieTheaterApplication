namespace MovieTheaterApplication.Models.ViewModels
{
    public class ShowingSelectionViewModel
    {
        public int Id { get; set; }

        public DateTime ShowingTime { get; set; }

        public required string AuditoriumName { get; set;  }
    }
}
