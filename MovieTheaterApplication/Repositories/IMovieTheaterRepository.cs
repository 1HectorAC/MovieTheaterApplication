using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories
{
    public interface IMovieTheaterRepository
    {
        Task<List<Movie>> GetMovies();

        Task<List<Movie>> GetMoviesWhereShowingsInTimeWindow(DateTime start, DateTime end);

        Task<List<Showing>> GetShowingsByMovieId(int movieId);

        Task<List<Showing>> GetShowingsByMovieIdWhereShowingsInTimeWindow(int movieId, DateTime start, DateTime end);

        Task<List<Seat>> GetSeatsByShowingId(int showingId);

        Task<List<int>> GetSeatIdsOfTicketsByShowingId(int showingId);

        Task<Movie?> GetMovieById(int movieId);

        Task<Showing?> GetShowingById(int showingId);

        Task TicketsAddRange(int[] SeatIds, int showingId);

        Task SaveChanges();
    }
}
