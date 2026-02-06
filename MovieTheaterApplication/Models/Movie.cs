using System.ComponentModel.DataAnnotations;

namespace MovieTheaterApplication.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; } = "";

        // Can add more Info like directory, actors... etc

        // Maybe add movie length

        //Maybe add field IsActive, Could also add to showing instead.

        public ICollection<Showing> Showings { get; set; } = new List<Showing>();
    }
}
