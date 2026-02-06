using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories.Implementations
{
    public class MovieTheaterRepository: IMovieTheaterRepository
    {
        private readonly TestDb _context;
        public MovieTheaterRepository(TestDb context)
        {
            _context = context;
        }

        // Maybe create seperate repositories later
        public async Task<List<Movie>> GetMovies()
        {
            var movies = _context.movies;
            return movies;
        }

        public async Task<List<Showing>> GetShowingsByMovieId(int movieId)
        {
            var showings = _context.showings.Where(i => i.MovieId == movieId).ToList();
            return showings;
        }


        //Maybe move to seats repository
        public async Task<List<Seat>> GetSeatsByShowingId(int showingId)
        {
            var showing = _context.showings.FirstOrDefault(i => i.Id == showingId);
            if (showing is null)
                return new List<Seat>();
            var auditorium = showing.Auditorium;
            if (auditorium is null || auditorium.Seats is null)
                return new List<Seat>();
            return [.. auditorium.Seats];
        }

        public async Task<List<int>> GetSeatIdsOfTicketsByShowingId(int showingId)
        {
            var showing = _context.showings.FirstOrDefault(i => i.Id == showingId);
            if (showing is null)
                return new List<int>();
            var tickets = showing.Tickets;
            if (tickets is null)
                return new List<int>();
            var seatIds = tickets.Select(i => i.SeatId).ToList();
            return seatIds;
        }
    }
}
