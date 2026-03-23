using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Services
{
    public interface IMovieTheaterService
    {

        Task<List<Movie>> GetMoviesWhereShowingsInTimeWindow(DateTime start, DateTime end);


        Task<List<Showing>> GetShowingsByMovieIdWhereShowingsInTimeWindow(int movieId, DateTime start, DateTime end);

  

        Task<List<int>> GetSeatIdsOfTicketsByShowingId(int showingId);

        Task<Movie?> GetMovieById(int movieId);

        Task<Showing?> GetShowingById(int showingId);

        Task CreateTickets(int[] SeatIds, int showingId);

      
    }
}
