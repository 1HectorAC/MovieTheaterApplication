using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Repositories.Implementations
{
    public class ShowingRepository: IShowingRepository
    {
        public async Task<List<Showing>> GetShowingsByMovieId(int movieId)
        {
            return new List<Showing>()
            {
                new Showing {Id = 1, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,8,0,0), MovieId = movieId},
                new Showing {Id = 2, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,10,0,0), MovieId = movieId},
                new Showing {Id = 3, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,12,0,0), MovieId = movieId},
                new Showing {Id = 4, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,14,0,0), MovieId = movieId},
                new Showing {Id = 5, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,16,0,0), MovieId = movieId}
            };
        }
    }
}
