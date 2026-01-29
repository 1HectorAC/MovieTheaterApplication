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

        public async Task<List<ShowingSeat>> GetShowingSeatsByShowingId(int showingId)
        {
            var showingSeats = new List<ShowingSeat>()
            {
                new ShowingSeat {Id = 1, SeatId = 1, Seat = new Seat {Id=1, AuditoriumId=1, Row = 'A', Column=1}, ShowingId = showingId},
                new ShowingSeat {Id = 2, SeatId = 2, Seat = new Seat {Id=2, AuditoriumId=1, Row = 'A', Column=2}, ShowingId = showingId},
                new ShowingSeat {Id = 3, SeatId = 3, Seat = new Seat {Id=3, AuditoriumId=1, Row = 'A', Column=3}, ShowingId = showingId},

                new ShowingSeat {Id = 4, SeatId = 4, Seat = new Seat {Id=4, AuditoriumId=1, Row = 'B', Column=1}, ShowingId = showingId},
                new ShowingSeat {Id = 5, SeatId = 5, Seat = new Seat {Id=5, AuditoriumId=1, Row = 'B', Column=2}, ShowingId = showingId},
                new ShowingSeat {Id = 6, SeatId = 6, Seat = new Seat {Id=6, AuditoriumId=1, Row = 'B', Column=3}, ShowingId = showingId},

                new ShowingSeat {Id = 7, SeatId = 7, Seat = new Seat {Id=7, AuditoriumId=1, Row = 'C', Column=1}, ShowingId = showingId},
                new ShowingSeat {Id = 8, SeatId = 8, Seat = new Seat {Id=8, AuditoriumId=1, Row = 'C', Column=2}, ShowingId = showingId},
                new ShowingSeat {Id = 9, SeatId = 9, Seat = new Seat {Id=9, AuditoriumId=1, Row = 'C', Column=3}, ShowingId = showingId},
            };
            return showingSeats;
        }
    }
}
