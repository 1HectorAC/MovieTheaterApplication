using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories
{
    public interface IMovieTheaterRepository
    {
        

        IQueryable<Movie> GetAllMoviesWithShowings();

        Task<Movie?> GetMovieByIdAsync(int id);


        IQueryable<Showing> GetAllShowings();

        Task<Showing?> GetShowingByIdWithMovie_Auditorium_SeatsAsync(int id);

        Task<Showing?> GetShowingByIdWithTicketsAsync(int id);

        IQueryable<Ticket> GetAllTickets();

        Task AddTicketRangeAsync(IEnumerable<Ticket> tickets);

        Task SaveChangesAsync();

    }
}
