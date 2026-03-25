using Microsoft.EntityFrameworkCore;
using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Repositories;

namespace MovieTheaterApplication.Services.Implementations
{
    public class MovieTheaterService: IMovieTheaterService
    {
        private readonly IMovieTheaterRepository _repo;


        public MovieTheaterService(IMovieTheaterRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Movie>> GetMoviesWhereShowingsInTimeWindow(DateTime start, DateTime end)
        {
            var movies = _repo.GetAllMoviesWithShowings();

            var filteredMovies = await movies
                .Where(movie => movie.Showings.Any(showing => showing.ShowingTime > start && showing.ShowingTime < end))
                .ToListAsync();

            return filteredMovies;
        }


        public async Task<List<Showing>> GetShowingsByMovieIdWhereShowingsInTimeWindow(int movieId, DateTime start, DateTime end)
        {
            var showings = _repo.GetAllShowings();

            var filteredShowings = await showings
                .Include(showing => showing.Auditorium)
                .Where(showing => showing.MovieId == movieId && showing.ShowingTime > start && showing.ShowingTime < end)
                .ToListAsync();

            return filteredShowings;
        }


        public async Task<Movie?> GetMovieById(int movieId)
        {

            var movie = await _repo.GetMovieByIdAsync(movieId);
            // Maybe Throw execption instead
            if(movie is null) return null;

            return movie;
        }

        public async Task<Showing?> GetShowingById(int showingId)
        {
            var showing = await _repo.GetShowingByIdWithMovie_Auditorium_SeatsAsync(showingId);

            // Maybe Throw execption instead
            if (showing is null) return null;


            return showing;
        }

        public async Task<List<int>> GetSeatIdsOfTicketsByShowingId(int showingId)
        {

            var showing = await _repo.GetShowingByIdWithTicketsAsync(showingId);

            if (showing is null || showing.Tickets is null)
                return new List<int>();


            var seatIds = showing.Tickets.Select(i => i.SeatId).ToList();
            return seatIds;
        }


        public async Task CreateTickets(int[] seatIds, int showingId)
        {
            if (seatIds.Length <= 0)
                throw new ArgumentException();

            // Check to make sure no ticket with id from seatIds + showingId exits
            foreach (var id in seatIds)
            {
                if (await _repo.GetAllTickets().AnyAsync(ticket => ticket.SeatId == id && ticket.ShowingId == showingId))
                {
                    throw new Exception();
                }
            }

            var tickets = seatIds.ToList().Select(i => new Ticket { SeatId = i, ShowingId = showingId });

            await _repo.AddTicketRangeAsync(tickets);
            await _repo.SaveChangesAsync();
        }


    }
}

