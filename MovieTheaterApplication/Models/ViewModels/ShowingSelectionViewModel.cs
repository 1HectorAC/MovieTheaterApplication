namespace MovieTheaterApplication.Models.ViewModels
{
    public class ShowingSelectionViewModel
    {
        public required string MovieTitle { get; set; }

        public required IEnumerable<IGrouping<DateOnly, ShowingViewModel>> GroupedShowingByDate
        { get; set; } 
    }
}
