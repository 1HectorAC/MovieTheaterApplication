using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories.Implementations
{
    public class MovieRepository: IMovieRepository
    {
        public async Task<List<Movie>> GetMovies()
        {

            return new List<Movie>()
            {
                new Movie {Id = 1, Title="The Great One", Description="stuff of stuff"},
                new Movie {Id = 2, Title="Lost of Soul", Description="stuff of soul"},
                new Movie {Id = 3, Title="Funny Times", Description="stuff of funny"}

            };
        }
    }
}
