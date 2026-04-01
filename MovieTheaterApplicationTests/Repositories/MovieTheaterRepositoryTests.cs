using Microsoft.EntityFrameworkCore;
using MovieTheaterApplication.Data;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTheaterApplicationTests.Repositories
{
    public class MovieTheaterRepositoryTests
    {
        [Fact]
        public async Task GetAllMoviesWithShowings_SomeMoviesWithNoShowings_ReturnAllMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie {Id = 1, Title="a", Description="aaaa"},
                new Movie {Id = 2, Title="b", Description="aaaa"},
                new Movie {Id = 3, Title="c", Description="aaaa"},
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Movies.AddRange(movies);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);

            // Act
            var result = repo.GetAllMoviesWithShowings().ToList();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetMovieByIdAsync_SomeMovies_ReturnMovieWithId()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie {Id = 1, Title="a", Description="aaaa"},
                new Movie {Id = 2, Title="b", Description="aaaa"},
                new Movie {Id = 3, Title="c", Description="aaaa"},
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Movies.AddRange(movies);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);


            // Act
            var movieId = 2;
            var result = await repo.GetMovieByIdAsync(movieId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
        }

        [Fact]
        public async Task GetAllShowings_SomeShowings_ReturnAllShowings()
        {
            // Arrange
            var showings = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId = 1, ShowingTime = new DateTime(2026,1,1)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId = 1, ShowingTime = new DateTime(2026,1,2)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId = 2, ShowingTime = new DateTime(2026,1,1)}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Showings.AddRange(showings);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);

            // Act
            var result = repo.GetAllShowings().ToList();

            // Assert
            Assert.Equal(3, result.Count);
        }


        /*
        [Fact]
        public async Task GetShowingByIdWithMovieAuditoriumSeatsAsync_OnlySomeShowings_ReturnShowingWithId()
        {
            // Arrange
            var seats = new List<Seat>
            {
                new Seat {Id = 1, AuditoriumId=1, Column=1, Row = 'a'},
                new Seat {Id = 2, AuditoriumId=1, Column=1, Row = 'a'},
            };
            var auditorium = new Auditorium { Id = 1, Title = "one"};
            var showings = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId = 1, ShowingTime = new DateTime(2026,1,1)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId = 1, ShowingTime = new DateTime(2026,1,2)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId = 2, ShowingTime = new DateTime(2026,1,1)}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Auditoriums.Add(auditorium);
            context.Seats.AddRange(seats);
            context.Showings.AddRange(showings);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);

            // Act
            var showingId = 2;
            var result = await repo.GetShowingByIdWithMovie_Auditorium_SeatsAsync(showingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(showingId, result.Id);
        }
        */

        [Fact]
        public async Task GetShowingByIdWithTicketsAsync_OnlySomeShowings_ReturnShowingWithId()
        {
            // Arrange
            var showings = new List<Showing>
            {
                new Showing {Id = 1, AuditoriumId = 1, MovieId = 1, ShowingTime = new DateTime(2026,1,1)},
                new Showing {Id = 2, AuditoriumId = 1, MovieId = 1, ShowingTime = new DateTime(2026,1,2)},
                new Showing {Id = 3, AuditoriumId = 1, MovieId = 2, ShowingTime = new DateTime(2026,1,1)}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Showings.AddRange(showings);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);

            // Act
            var showingId = 2;
            var result = await repo.GetShowingByIdWithTicketsAsync(showingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(showingId, result.Id);
        }

        [Fact]
        public async Task GetAllTickets_SomeTickets_ReturnAllTickets()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket {Id = 1, SeatId = 1, ShowingId = 1},
                new Ticket {Id = 2, SeatId = 2, ShowingId = 1},
                new Ticket {Id = 3, SeatId = 3, ShowingId = 1}
            };

            var options = new DbContextOptionsBuilder<MovieTheaterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MovieTheaterDbContext(options);
            context.Tickets.AddRange(tickets);
            context.SaveChanges();

            var repo = new MovieTheaterRepository(context);

            // Act
            var result = repo.GetAllTickets().ToList();

            // Assert
            Assert.Equal(3, result.Count);
        }



    }
}
