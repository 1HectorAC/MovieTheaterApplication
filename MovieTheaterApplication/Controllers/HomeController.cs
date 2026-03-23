using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieTheaterApplication.DTO;
using MovieTheaterApplication.Models;
using MovieTheaterApplication.Models.ViewModels;
using MovieTheaterApplication.Repositories;
using MovieTheaterApplication.Repositories.Implementations;
using MovieTheaterApplication.Services;

namespace MovieTheaterApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieTheaterService _service;

        public HomeController( IMovieTheaterService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var currentDateTime = DateTime.Now;

            // Filter to only show movies with showing from an hour ago to 30 days from now.
            var movies = await _service.GetMoviesWhereShowingsInTimeWindow(currentDateTime.AddHours(-1), currentDateTime.AddDays(30));


            return View(movies);
        }

        public async Task<IActionResult> ShowingSelection(int MovieId)
        {
            var currentDateTime = DateTime.Now;

            var showings = await _service.GetShowingsByMovieIdWhereShowingsInTimeWindow(MovieId, currentDateTime.AddHours(-1), currentDateTime.AddDays(30));
            var movie = await _service.GetMovieById(MovieId);
            

            var formatedShowings = showings
                .Select(i => new ShowingViewModel
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
            var showing = await _service.GetShowingById(ShowingId);

            // Null value check of showing fields
            if (showing is null || showing.Movie is null || showing.Auditorium is null)
            {
                RedirectToAction("Index");
            }

            var seats = showing!.Auditorium!.Seats;
          
            // Get seatIds for tickets of a the specified showing. Used to filter out seats that are open (has no ticket)
            var seatIdsForTickets = await _service.GetSeatIdsOfTicketsByShowingId(ShowingId);
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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SeatSelection", new { ShowingId = dto.ShowingId });
            }

            try
            {
                await _service.CreateTickets(dto.SeatIds, dto.ShowingId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Consider redirect to error page instead.
                TempData["ErrorMessage"] = "Error making tickets.";
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
