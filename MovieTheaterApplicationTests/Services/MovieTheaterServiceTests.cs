using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
            }.AsQueryable();

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
            }.AsQueryable();

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


    }
}
