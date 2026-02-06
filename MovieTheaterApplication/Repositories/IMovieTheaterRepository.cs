using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories
{
    public interface IMovieTheaterRepository
    {
        Task<List<Movie>> GetMovies();

        Task<List<Showing>> GetShowingsByMovieId(int movieId);

        Task<List<Seat>> GetSeatsByShowingId(int showingId);

        Task<List<int>> GetSeatIdsOfTicketsByShowingId(int showingId);

    }
}
