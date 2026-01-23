using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();
    }
}
