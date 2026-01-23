using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories
{
    public interface IShowingRepository
    {
        Task<List<Showing>> GetShowingsByMovieId(int movieId);
    }
}
