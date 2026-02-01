using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories.Implementations
{
    public class MovieRepository: IMovieRepository
    {
        private readonly TestDb _context;
        public MovieRepository(TestDb context) {
            _context = context;
        }
        public async Task<List<Movie>> GetMovies()
        {
            var movies = _context.movies;
            return movies;
        }
    }
}
