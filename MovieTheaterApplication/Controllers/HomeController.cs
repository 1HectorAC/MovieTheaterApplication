using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

            var formatedShowings = showings.Select(i => new ShowingSelectionViewModel
            {
                Id = i.Id,
                ShowingTime = i.ShowingTime,
                AuditoriumName = (i.Auditorium != null) ? i.Auditorium.Title : ""
            }).ToList();

            ViewBag.MovieTitle = (movie != null)? movie.Title : "";

            return View(formatedShowings);
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
          
            var seatIdsForTickets = await _movieTheaterRepository.GetSeatIdsOfTicketsByShowingId(ShowingId);

            var formatedSeats = seats.Select(i => new SeatListViewModel { SeatId = i.Id, Column = i.Column, Row = i.Row, IsSelected = seatIdsForTickets.Contains(i.Id)});
            var groupedFormatedSeats = formatedSeats.GroupBy(i => i.Row);

            SeatSelectionViewModel data = new SeatSelectionViewModel
            {
                ShowingId = ShowingId,
                MovieTitle = showing!.Movie!.Title,
                ShowingTime = showing.ShowingTime,
                AuditoriumTitle = showing.Auditorium.Title,
                GroupedSeats = groupedFormatedSeats,
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> SelectShowingSeat(int[] seatsId, int showingId)
        {

            await _movieTheaterRepository.TicketsAddRange(seatsId, showingId);

            try
            {
                await _movieTheaterRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Maybe redirect to error page
                // Or Send error message viewBag
                return RedirectToAction("SeatSelection", new { ShowingId = showingId });
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
