using Microsoft.EntityFrameworkCore;
using MovieTheaterApplication.Models;

namespace MovieTheaterApplication.Data
{
    public class MovieTheaterDbContext: DbContext
    {
        public MovieTheaterDbContext(DbContextOptions<MovieTheaterDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Auditorium> Auditoriums { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Showing> Showings { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

    }

}
