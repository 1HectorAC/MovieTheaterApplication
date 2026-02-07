using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieTheaterApplication.Models
{
    public class Auditorium
    {
        public int Id { get; set;  }

        [Required]
        public required string Title { get; set; }

        //Consider adding seat rows and column numbers

        [JsonIgnore]
        public ICollection<Showing> Showings { get; set; } = new List<Showing>();

        [JsonIgnore]
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    }
}
