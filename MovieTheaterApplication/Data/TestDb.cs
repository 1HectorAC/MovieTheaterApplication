using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Data
{
    public class TestDb
    {
        public List<Auditorium> auditoriums = new List<Auditorium>()
        {
            new Auditorium{Id = 1, Title = "One", },
            new Auditorium{Id = 2, Title = "Two", },
        };

        public List<Seat> seats = new List<Seat>() {
            new Seat{Id = 1, AuditoriumId = 1, Row='A', Column = 1 },
            new Seat{Id = 2, AuditoriumId = 1, Row='A', Column = 2 },
            new Seat{Id = 3, AuditoriumId = 1, Row='A', Column = 3 },
            new Seat{Id = 4, AuditoriumId = 1, Row='B', Column = 1 },
            new Seat{Id = 5, AuditoriumId = 1, Row='B', Column = 2 },
            new Seat{Id = 6, AuditoriumId = 1, Row='B', Column = 3 },
            new Seat{Id = 7, AuditoriumId = 1, Row='C', Column = 1 },
            new Seat{Id = 8, AuditoriumId = 1, Row='C', Column = 2 },
            new Seat{Id = 9, AuditoriumId = 1, Row='C', Column = 3 },
        };

        public List<Movie> movies = new List<Movie>()
        {
            new Movie {Id = 1, Title="The Great One", Description="stuff of stuff"},
            new Movie {Id = 2, Title="Lost of Soul", Description="stuff of soul"},
            new Movie {Id = 3, Title="Funny Times", Description="stuff of funny"}
        };

        public List<Showing> showings = new List<Showing>()
        {
            new Showing {Id = 1, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,8,0,0), MovieId = 1},
                new Showing {Id = 2, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,10,0,0), MovieId = 1},
                new Showing {Id = 3, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,12,0,0), MovieId = 1},
                new Showing {Id = 4, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,14,0,0), MovieId = 1},
                new Showing {Id = 5, AuditoriumId = 1, ShowingTime = new DateTime(2026,1,1,16,0,0), MovieId = 1}
        };

        //Need to make sure showing and ticket relation to auditorium match
        public List<Ticket> ticket = new List<Ticket>()
        {
            new Ticket {Id = 1, ShowingId = 1, SeatId = 6 },
            new Ticket {Id = 2, ShowingId = 1, SeatId = 7 }
        };
    }
}
