using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories
{
    public interface IShowingRepository
    {
        Task<List<Showing>> GetShowingsByMovieId(int movieId);

        Task<List<ShowingSeat>> GetShowingSeatsByShowingId(int showingId);

        Task<List<Seat>?> GetSeatsByShowingId(int showingId);
    }
}
