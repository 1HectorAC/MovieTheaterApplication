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

            modelBuilder.Entity<Ticket>().HasOne(t => t.Seat).WithMany(s => s.Tickets).HasForeignKey(t => t.SeatId).OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Ticket>().HasOne(t => t.Showing).WithMany(sh => sh.Tickets).HasForeignKey(t => t.ShowingId).OnDelete(DeleteBehavior.Restrict);


        }

        public DbSet<Auditorium> Auditoriums { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Showing> Showings { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

    }

}
