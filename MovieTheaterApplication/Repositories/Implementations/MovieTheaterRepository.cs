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

        // Maybe remove, Unused
        // Maybe create seperate repositories later
        public async Task<List<Movie>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task<List<Movie>> GetMoviesWhereShowingsInTimeWindow(DateTime start, DateTime end)
        {
            var movies = await _context.Movies
                .AsNoTracking()
                .Include(movie => movie.Showings)
                .Where(movie => movie.Showings.Any(showing => showing.ShowingTime > start && showing.ShowingTime < end))
                .ToListAsync();

            return movies;
        }


        // Maybe remove, Unused
        public async Task<List<Showing>> GetShowingsByMovieId(int movieId)
        {
            var showings = await _context.Showings
                .AsNoTracking()
                .Where(i => i.MovieId == movieId)
                .Include(i => i.Auditorium)
                .ToListAsync();

            return showings;
        }

        public async Task<List<Showing>> GetShowingsByMovieIdWhereShowingsInTimeWindow(int movieId, DateTime start, DateTime end)
        {
            var showings = await _context.Showings
                .AsNoTracking()
                .Where(i => i.MovieId == movieId)
                .Include(i => i.Auditorium)
                .Where(showing => showing.ShowingTime > start && showing.ShowingTime < end)
                .ToListAsync();

            return showings;
        }


        //Maybe move to seats repository
        public async Task<List<Seat>> GetSeatsByShowingId(int showingId)
        {
            // Consider checking if auditorim for showing exits before getting.

            var showing = await _context.Showings
                .AsNoTracking()
                .Include(i => i.Auditorium)
                .ThenInclude(i => i.Seats)
                .FirstOrDefaultAsync(i => i.Id == showingId);

            if (showing is null)
                return new List<Seat>();

            var auditorium = showing.Auditorium;
            if (auditorium is null || auditorium.Seats is null)
                return new List<Seat>();

            return [.. auditorium.Seats];
        }

        public async Task<List<int>> GetSeatIdsOfTicketsByShowingId(int showingId)
        {
            var showing = await _context.Showings
                .AsNoTracking()
                .Include(i => i.Tickets)
                .FirstOrDefaultAsync(i => i.Id == showingId);

            if (showing is null)
                return new List<int>();

            var tickets = showing.Tickets;
            if (tickets is null)
                return new List<int>();

            var seatIds = tickets.Select(i => i.SeatId).ToList();
            return seatIds;
        }

        public async Task<Movie?> GetMovieById(int movieId)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == movieId);
            return movie;
        }

        public async Task<Showing?> GetShowingById(int showingId)
        {
            var showing = await _context.Showings
                .AsNoTracking()
                .Include(i => i.Movie)
                .Include(i => i.Auditorium)
                .ThenInclude(i => i.Seats)
                .FirstOrDefaultAsync(i => i.Id == showingId);

            return showing;
        }

        public async Task TicketsAddRange(int[] seatIds, int showingId)
        {
            if (seatIds.Length <= 0)
                throw new ArgumentException();

            // Check to make sure no ticket with id from seatIds + showingId exits
            foreach (var id in seatIds) {
                if (_context.Tickets.Any(i => i.SeatId == id && i.ShowingId == showingId))
                {
                    throw new Exception();
                }
            }

            var tickets = seatIds.ToList().Select(i => new Ticket { SeatId = i, ShowingId = showingId });

            await _context.AddRangeAsync(tickets);
        }

        


        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
