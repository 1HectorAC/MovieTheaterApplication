using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Repositories;
using MovieTheaterApplication.Repositories.Implementations;
using MovieTheaterApplication.Services.Implementations;

namespace MovieTheaterApplicationTests.Services
{
    public class MovieTheaterServiceTests
    {
        [Fact]
        public async Task GetMoviesWhereShowingInTimeWindow_ListOfMoviesInTimeWindow_ReturnAllMoviesInTimeFrame()
        {
            // Arrange
            var ShowingData = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 1,2)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId=2, ShowingTime = new DateTime(2025, 1,10)}
            };

            var movieData = new List<Movie>
            {
                new Movie {Id = 1, Title="aaa", Description="aaa" },
                new Movie {Id = 2, Title="bbv", Description="bbb" }
            };

            //Note: Use In-memoryDb instead of mock, since service need certain ef core funtionality
            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Movies.AddRange(movieData);
            context.Showings.AddRange(ShowingData);
            
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);
            var service = new MovieTheaterService(repo);


            //Act
            var startTime = new DateTime(2025, 1, 1);
            var endTime = new DateTime(2025, 2, 1);
            var movies = await service.GetMoviesWhereShowingsInTimeWindow(startTime, endTime);

            //Assert
            Assert.Equal(2, movies.Count);

            Assert.All(movies, movie => movie.Showings.All(showing => showing.ShowingTime > startTime && showing.ShowingTime < endTime));
           
        }

        [Fact]
        public async Task GetMoviesWhereShowingInTimeWindow_MoviesBeforeAndInTimeWindow_ReturnMoviesInWindow()
        {
            // Arrange
            var ShowingData = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 1,2)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId=2, ShowingTime = new DateTime(2025, 2,10)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId=3, ShowingTime = new DateTime(2025, 2,15)}
            };

            var movieData = new List<Movie>
            {
                new Movie {Id = 1, Title="aaa", Description="aaa" },
                new Movie {Id = 2, Title="bbv", Description="bbb" },
                new Movie {Id = 3, Title="ccc", Description="ccc" }
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Movies.AddRange(movieData);
            context.Showings.AddRange(ShowingData);

            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);
            var service = new MovieTheaterService(repo);

            //Act
            var startTime = new DateTime(2025, 2, 1);
            var endTime = new DateTime(2025, 3, 1);
            var movies = await service.GetMoviesWhereShowingsInTimeWindow(startTime, endTime);

            //Assert
            Assert.Equal(2, movies.Count);
            Assert.All(movies, movie => movie.Showings.All(showing => showing.ShowingTime > startTime && showing.ShowingTime < endTime));

        }

        [Fact]
        public async Task GetMoviesWhereShowingInTimeWindow_MoviesAfterAndInTimeWindow_ReturnMoviesInWindow()
        {
            // Arrange
            var ShowingData = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,2)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId=2, ShowingTime = new DateTime(2025, 2,10)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId=3, ShowingTime = new DateTime(2025, 3,15)}
            };

            var movieData = new List<Movie>
            {
                new Movie {Id = 1, Title="aaa", Description="aaa" },
                new Movie {Id = 2, Title="bbv", Description="bbb" },
                new Movie {Id = 3, Title="ccc", Description="ccc" }
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Movies.AddRange(movieData);
            context.Showings.AddRange(ShowingData);

            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);
            var service = new MovieTheaterService(repo);

            //Act
            var startTime = new DateTime(2025, 2, 1);
            var endTime = new DateTime(2025, 3, 1);
            var movies = await service.GetMoviesWhereShowingsInTimeWindow(startTime, endTime);

            //Assert
            Assert.Equal(2, movies.Count);
            Assert.All(movies, movie => movie.Showings.All(showing => showing.ShowingTime > startTime && showing.ShowingTime < endTime));

        }

        [Fact]
        public async Task GetShowingsByMovieIdWhereShowingInTimeWindow_ShowingsInTimeWindowOfSpecifiedMovie_ReturnMoviesInWindow()
        {
            var showingData = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,2)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,10)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,15)}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Showings.AddRange(showingData);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);
            var service = new MovieTheaterService(repo);

            //act
            var startTime = new DateTime(2025, 2, 1);
            var endTime = new DateTime(2025, 3, 1);
            var showings = await service.GetShowingsByMovieIdWhereShowingsInTimeWindow(1,startTime, endTime);

            Assert.Equal(3, showings.Count);
            foreach (var s in showings){
                Assert.InRange(s.ShowingTime, startTime, endTime);
            }
        }

        [Fact]
        public async Task GetShowingsByMovieIdWhereShowingInTimeWindow_ShowingsBeforeAndInTimeWindowOfSpecifiedMovie_ReturnMoviesInWindow()
        {
            var showingData = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 1,2)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,10)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,15)}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Showings.AddRange(showingData);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);
            var service = new MovieTheaterService(repo);

            //act
            var startTime = new DateTime(2025, 2, 1);
            var endTime = new DateTime(2025, 3, 1);
            var showings = await service.GetShowingsByMovieIdWhereShowingsInTimeWindow(1, startTime, endTime);

            Assert.Equal(2, showings.Count);
            foreach (var s in showings)
            {
                Assert.InRange(s.ShowingTime, startTime, endTime);
            }
        }

        [Fact]
        public async Task GetShowingsByMovieIdWhereShowingInTimeWindow_ShowingsInAndAfterTimeWindowOfSpecifiedMovie_ReturnMoviesInWindow()
        {
            var showingData = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 3,2)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,10)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId=1, ShowingTime = new DateTime(2025, 2,15)}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Showings.AddRange(showingData);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);
            var service = new MovieTheaterService(repo);

            //act
            var startTime = new DateTime(2025, 2, 1);
            var endTime = new DateTime(2025, 3, 1);
            var showings = await service.GetShowingsByMovieIdWhereShowingsInTimeWindow(1, startTime, endTime);

            Assert.Equal(2, showings.Count);
            foreach (var s in showings)
            {
                Assert.InRange(s.ShowingTime, startTime, endTime);
            }
        }

        [Fact]
        public async Task GetSeatIdsOfTicketsByShowingId_AllTicketsBelongingToSameShowing_ReturnAllSeatIds()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket {Id = 1, ShowingId = 1, SeatId=1},
                new Ticket {Id = 2, ShowingId = 1, SeatId=2},
                new Ticket {Id = 3, ShowingId = 1, SeatId=3}
            };
            var showing = new Showing
            {
                Id = 1,
                MovieId = 1,
                ShowingTime = new DateTime(2025, 3, 2),
                Tickets = tickets
            };

            var repo = new Mock<IMovieTheaterRepository>();
            repo.Setup(r => r.GetShowingByIdWithTicketsAsync(1)).ReturnsAsync(showing
                );
            var service = new MovieTheaterService(repo.Object);

            // Act
            var result = await service.GetSeatIdsOfTicketsByShowingId(1);


            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetSeatIdsOfTicketsByShowingId_MostTicketsBelongingToSameShowing_ReturnAllSeatIdsOfShowing()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket {Id = 1, ShowingId = 1, SeatId=1},
                new Ticket {Id = 2, ShowingId = 1, SeatId=2},
                new Ticket {Id = 3, ShowingId = 2, SeatId=3}
            };
            var showing = new Showing
            {
                Id = 1,
                MovieId = 1,
                ShowingTime = new DateTime(2025, 3, 2),
                Tickets = tickets.Where(ticket => ticket.ShowingId == 1).ToArray()
            };

            var showingId = 1;
            var repo = new Mock<IMovieTheaterRepository>();
            repo.Setup(r => r.GetShowingByIdWithTicketsAsync(showingId)).ReturnsAsync(showing
                );
            var service = new MovieTheaterService(repo.Object);

            // Act
            var result = await service.GetSeatIdsOfTicketsByShowingId(1);


            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, seatId => tickets.Any(ticket => ticket.SeatId == seatId && ticket.ShowingId == showingId));
        }



    }
}
