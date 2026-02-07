using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories.Implementations
{
    public class ShowingRepository: IShowingRepository
    {
        private readonly TestDb _context;
        public ShowingRepository(TestDb context)
        {
            _context = context;
        }

        public async Task<List<Showing>> GetShowingsByMovieId(int movieId)
        {
            var showings = _context.showings;
            return showings;
        }

       

        //Maybe move to seats repository
        public async Task<List<Seat>?> GetSeatsByShowingId(int showingId)
        {
            var showing = _context.showings.FirstOrDefault(i => i.Id == showingId);
            if (showing is null)
                return null;
            var auditorium = showing.Auditorium;
            if (auditorium is null || auditorium.Seats is null)
                return null;
            return [.. auditorium.Seats];
        }
    }
}
