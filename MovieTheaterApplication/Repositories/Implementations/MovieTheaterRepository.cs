using Microsoft.EntityFrameworkCore;
using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories.Implementations
{
    public class MovieTheaterRepository: IMovieTheaterRepository
    {

        private readonly MovieTheaterDbContext _context;
        public MovieTheaterRepository(MovieTheaterDbContext context)
        {
            _context = context;
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return _context.Movies.AsNoTracking();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(movie => movie.Id == id);
        }

        public IQueryable<Showing> GetAllShowings()
        {
            return _context.Showings.AsNoTracking();
        }

        public async Task<Showing?> GetShowingByIdWithMovie_Auditorium_SeatsAsync(int id)
        {
            return await _context.Showings
                .AsNoTracking()
                .Include(showing => showing.Movie)
                .Include(showing => showing.Auditorium)
                .ThenInclude(auditorium => auditorium.Seats)
                .FirstOrDefaultAsync(showing => showing.Id == id);
        }

        public async Task<Showing?> GetShowingByIdWithTicketsAsync(int id)
        {
            return await _context.Showings
                .AsNoTracking()
                .Include(showing => showing.Tickets)
                .FirstOrDefaultAsync(showing => showing.Id == id);
        }

        public IQueryable<Ticket> GetAllTickets()
        {
            return _context.Tickets;
        }

        public async Task AddTicketRangeAsync(IEnumerable<Ticket> tickets)
        {
            await _context.AddRangeAsync(tickets);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
