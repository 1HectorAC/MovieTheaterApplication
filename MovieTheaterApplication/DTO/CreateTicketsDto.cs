using System.ComponentModel.DataAnnotations;

namespace MovieTheaterApplication.DTO
{
    public class CreateTicketsDto
    {

        [Required]
        public int[] SeatIds { get; set; } = new int[0];

        [Required]
        public int ShowingId { get; set;  }

    }
}
