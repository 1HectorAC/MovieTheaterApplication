using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApplication.DTO;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Models.ViewModels;
using MovieTheaterApplication.Repositories;
using MovieTheaterApplication.Repositories.Implementations;

namespace MovieTheaterApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieTheaterRepository _movieTheaterRepository;

        public HomeController( IMovieTheaterRepository movieTheaterRepository)
        {
            _movieTheaterRepository = movieTheaterRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Might want to narrow to showings to today or add range somehow
            var movies = await _movieTheaterRepository.GetMovies();


            return View(movies);
        }

        public async Task<IActionResult> ShowingSelection(int MovieId)
        {
            var showings = await _movieTheaterRepository.GetShowingsByMovieId(MovieId);
            var movie = await _movieTheaterRepository.GetMovieById(MovieId);

            // Get DateTime for an hour ago to filter out old showings.
            var currentDateTime = DateTime.Now.AddHours(-1);

            var formatedShowings = showings.Where(i => i.ShowingTime > currentDateTime).Select(i => new ShowingViewModel
            {
                Id = i.Id,
                ShowingTime = i.ShowingTime
            }).OrderBy(i => i.ShowingTime).ToList();

            var groupedShowings = formatedShowings.GroupBy(i => DateOnly.FromDateTime(i.ShowingTime));

            ShowingSelectionViewModel data = new ShowingSelectionViewModel
            {
                MovieTitle = (movie != null) ? movie.Title : "",
                GroupedShowingByDate = groupedShowings
            };

            return View(data);
        }

        public async Task<IActionResult> SeatSelection(int ShowingId)
        {
            var showing = await _movieTheaterRepository.GetShowingById(ShowingId);

            // Null value check of showing fields
            if (showing is null || showing.Movie is null || showing.Auditorium is null)
            {
                RedirectToAction("Index");
            }

            var seats = showing!.Auditorium!.Seats;
          
            // Get seatIds for tickets of a the specified showing. Used to filter out seats that are open (has no ticket)
            var seatIdsForTickets = await _movieTheaterRepository.GetSeatIdsOfTicketsByShowingId(ShowingId);
            var formatedSeats = seats.Select(i => new SeatListViewModel { SeatId = i.Id, Column = i.Column, Row = i.Row, IsSelected = seatIdsForTickets.Contains(i.Id)});
            
            // Data is grouped to dispay data by row and column easier
            var groupedFormatedSeats = formatedSeats.GroupBy(i => i.Row);

            SeatSelectionViewModel data = new SeatSelectionViewModel
            {
                ShowingId = ShowingId,
                MovieTitle = showing!.Movie!.Title,
                MovieId = showing!.MovieId,
                ShowingTime = showing.ShowingTime,
                AuditoriumTitle = showing.Auditorium.Title,
                GroupedSeats = groupedFormatedSeats,
                TotalAvailableSeats = seats.Count - seatIdsForTickets.Count
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> SelectShowingSeat([FromForm] CreateTicketsDto dto )
        {
            try
            {
                await _movieTheaterRepository.TicketsAddRange(dto.SeatIds, dto.ShowingId);
                await _movieTheaterRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Maybe redirect to error page
                TempData["ErrorMessage"] = "error with stuff";
                return RedirectToAction("SeatSelection", new { ShowingId = dto.ShowingId });
            }
            
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
